using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
  public RegisterRequestValidator()
  {
    RuleFor(register => register.Email)
      .NotEmpty().WithMessage("Email is required.")
      .EmailAddress().WithMessage("Invalid email address format.");

    RuleFor(register => register.Password)
      .NotEmpty().WithMessage("Password is required.")
      .Length(5,25).WithMessage("Password must be between 5 and 25 characters.");

    RuleFor(register => register.PersonName)
      .NotEmpty().WithMessage("PersonName is required.")
      .Length(2, 50).WithMessage("PersonName must be between 2 and 50 characters.");
  }
}
