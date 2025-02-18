using LibrarySystem.Domain.BaseModels.User;

namespace LibrarySystem.Domain.DTOs.Customers;

public class CustomerRegisterResponse(string emailAddress, string userName, string token, string imageurl, Guid id)
 : AuthenticatedUserBase(emailAddress, userName, imageurl, token, id);
