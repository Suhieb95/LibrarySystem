using FluentValidation;
using LibrarySystem.API.Helpers;
using LibrarySystem.Domain.DTOs.Customers;

namespace LibrarySystem.API.Validations.Customers;
public class CustomerLoginValidator : AbstractValidator<CustomerLoginRequest>
{
    public CustomerLoginValidator()
    {
        RuleFor(s => s.EmailAddress).EmailAddress()
                                                        .WithMessage("A valid email is required");
        RuleFor(x => x.Password).NotEmpty()
                                                        .WithMessage("Password Is required.")
                                                        .Password();
    }
}