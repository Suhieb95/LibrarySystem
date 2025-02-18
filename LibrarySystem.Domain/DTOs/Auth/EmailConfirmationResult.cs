namespace LibrarySystem.Domain.DTOs.Auth;
public record EmailConfirmationResult(Guid UserId,
    string EmailAddressConfirmationToken,
    DateTime EmailAddressConfirmationTokenExpiry
    );