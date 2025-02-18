using LibrarySystem.Domain.DTOs.BaseModels;

namespace LibrarySystem.Domain.DTOs.Customers;

public class Customer(string emailAddress, string userName, string imagePath) : PersonBase(emailAddress, userName, imagePath)
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }
    public string? ResetPasswordToken { get; set; }
    public DateTime LastLogin { get; set; }
    public DateTime ResetPasswordTokenExpiry { get; set; }
    public DateTime CreatedAt { get; set; }
};
