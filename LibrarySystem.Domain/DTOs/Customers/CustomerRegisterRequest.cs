using LibrarySystem.Domain.DTOs.BaseModels;

namespace LibrarySystem.Domain.DTOs.Customers;
public class CustomerRegisterRequest(string emailAddress, string password, string userName) : RegisterBaseRequest(emailAddress, userName)
{
    public string Password { get; private set; } = password;
    public void SetPassword(string password) => Password = password;
}
