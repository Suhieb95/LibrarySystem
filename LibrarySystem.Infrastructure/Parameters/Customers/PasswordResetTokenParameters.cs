namespace LibrarySystem.Infrastructure.Parameters.Customers;
internal class PasswordResetTokenParameters
{
    public required string EmailAddress { get; init; }
    public required string ResetPasswordToken { get; init; }
    public required DateTime ResetPasswordTokenExpiry { get; init; }
}
