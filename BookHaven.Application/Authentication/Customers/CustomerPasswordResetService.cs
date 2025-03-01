using BookHaven.Application.Authentication.Common;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Auth;
using BookHaven.Domain.Specification;
using BookHaven.Domain.Specification.Customers;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace BookHaven.Application.Authentication.Customers;
public class CustomerPasswordResetService(IUnitOfWork unitOfWork, IOptions<EmailSettings> emailSettings, IPasswordHasher passwordHasher, INotificationService notificationService, IWebHostEnvironment env, IJwtTokenGenerator jwtTokenGenerator)
    : ICustomerPasswordResetService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IWebHostEnvironment _env = env;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    public async Task<Result<bool>> ChangePassword(PasswordChangeRequest request, CancellationToken? cancellationToken)
    {
        Result<bool> result = await VerifyToken(request.Id, cancellationToken);
        if (!result.IsSuccess)
            return result;

        request.SetPassword(_passwordHasher.Hash(request.Password));
        await _unitOfWork.UserSecurity.UpdatePassowordResetToken(request, cancellationToken);
        Customer? customer = await GetCustomer(new GetCustomerById(request.Id), cancellationToken);
        await EmailHelpers.SendPasswordChangedNotify(customer!.EmailAddress, _emailSettings.ResetPasswordURL, _env, cancellationToken, _notificationService);

        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> ResetPassword(string emailAddress, CancellationToken? cancellationToken)
    {
        Customer? customer = await GetCustomer(new GetCustomerByEmailAddress(emailAddress), cancellationToken);
        if (customer is null || customer is not null and { IsActive: false })
            return Result<bool>.Failure(new Error("Customer Doesn't With this Exists.", BadRequest, "Invalid Customer"));

        ResetPasswordResult resetPassword = _jwtTokenGenerator.GeneratePasswordResetToken(emailAddress);
        await _unitOfWork.UserSecurity.SavePassowordResetToken(resetPassword, cancellationToken);
        await EmailHelpers.SendResetPasswordLink(customer!.EmailAddress, customer.Id, _emailSettings.ResetPasswordURL, _env, cancellationToken, _notificationService);
        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> VerifyToken(Guid id, CancellationToken? cancellationToken = null)
    {
        Customer? customer = await GetCustomer(new GetCustomerById(id), cancellationToken);
        if (customer is null || customer is not null and { IsActive: false })
            return Result<bool>.Failure(new Error("Customer With this Email Address Doesn't Exists.", BadRequest, "Invalid Customer"));

        if (!customer!.HasValidRestPasswordToken())
            return Result<bool>.Failure(new Error("Token Has Expired, Please request password change again.", BadRequest, "Invalid Token"));

        return Result<bool>.Success(true);
    }
    private async Task<T?> GetCustomer<T>(Specification<T> specification, CancellationToken? cancellationToken)
        => await _unitOfWork.Customers.GetBy(specification, cancellationToken);
}