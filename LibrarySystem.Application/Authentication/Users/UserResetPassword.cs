using LibrarySystem.Application.Authentication.Common;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.Specification;
using LibrarySystem.Domain.Specification.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
namespace LibrarySystem.Application.Authentication.Users;
public class UserResetPassword(IUnitOfWork _unitOfWork, IOptions<EmailSettings> emailSettings, IPasswordHasher _passwordHasher, INotificationService _notificationService, IWebHostEnvironment _env, IJwtTokenGenerator _jwtTokenGenerator) : IUserResetPassword
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    public async Task<Result<bool>> ChangePassword(PasswordChangeRequest request, CancellationToken? cancellationToken = null)
    {
        Result<bool> result = await VerifyToken(request.UserId, cancellationToken);
        if (!result.IsSuccess)
            return result;

        request.SetPassword(_passwordHasher.Hash(request.Password));
        await _unitOfWork.Users.UpdatePassowordResetToken(request, cancellationToken);
        User? user = await GetUsers(new GetUserByIdSpecification(request.UserId), cancellationToken);
        await EmailHelpers.SendPasswordChangedNotify(user!.EmailAddress, _emailSettings.ResetPasswordURL, _env, cancellationToken, _notificationService);

        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> GenerateResetPasswordLink(string emailAddress, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }
    public async Task<Result<bool>> ResetPassword(string emailAddress, CancellationToken? cancellationToken = null)
    {
        User? user = await GetUsers(new GetUserByEmailAddress(emailAddress), cancellationToken);
        if (user is null || user is not null and { IsVerified: false })
            return Result<bool>.Failure(new("User Doesn't With this Exists.", BadRequest, "Invalid User"));

        ResetPasswordResult resetPassword = _jwtTokenGenerator.GeneratePasswordResetToken(emailAddress);
        await _unitOfWork.Users.SavePassowordResetToken(resetPassword, cancellationToken);
        await EmailHelpers.SendResetPasswordLink(user!.EmailAddress, user.Id, _emailSettings.ResetPasswordURL, _env, cancellationToken, _notificationService);
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> VerifyToken(Guid id, CancellationToken? cancellationToken = null)
    {
        User? user = await GetUsers(new GetUserByIdSpecification(id), cancellationToken);
        if (user is null || user is not null and { IsVerified: false })
            return Result<bool>.Failure(new("User Doesn't Exists.", BadRequest, "Invalid User"));

        if (!user!.HasValidRestPasswordToken())
            return Result<bool>.Failure(new("Token Has Expired, Please request password change again.", BadRequest, "Invalid Token"));

        return Result<bool>.Success(true);
    }
    private async Task<User?> GetUsers(Specification specification, CancellationToken? cancellationToken)
       => (await _unitOfWork.Users.GetAll(specification, cancellationToken)).FirstOrDefault();
}