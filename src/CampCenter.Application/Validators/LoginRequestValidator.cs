using CampCenter.Application.DTOs.Auth;
using FluentValidation;

namespace CampCenter.Application.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty().WithMessage("Login is required.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}
