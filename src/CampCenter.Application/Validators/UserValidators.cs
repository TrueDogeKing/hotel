using CampCenter.Application.DTOs.Users;
using CampCenter.Domain.Entities;
using FluentValidation;

namespace CampCenter.Application.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequestDto>
{
    public CreateUserRequestValidator()
    {
        RuleFor(r => r.Login)
            .NotEmpty()
            .WithMessage("Login is required.")
            .MaximumLength(32)
            .WithMessage("Login may be at most 32 characters long.")
            // Logins are normalised to lowercase before they are stored, so what is
            // allowed here is what a login can consist of — no spaces, which would
            // be invisible in a sign-in field.
            .Matches("^[A-Za-z0-9._-]+$")
            .WithMessage("Login may contain only letters, digits, dots, dashes and underscores.");

        RuleFor(r => r.Password).ValidPassword();

        RuleFor(r => r.Role).ValidRole();
    }
}

public class SetUserRoleRequestValidator : AbstractValidator<SetUserRoleRequestDto>
{
    public SetUserRoleRequestValidator() => RuleFor(r => r.Role).ValidRole();
}

internal static class RoleRules
{
    /// One of the AdminUserRole names. Checked here as well as in the service, so a
    /// bad role comes back as a field-level validation error rather than a bare 400.
    public static IRuleBuilderOptions<T, string> ValidRole<T>(this IRuleBuilder<T, string> rule) =>
        rule.NotEmpty()
            .WithMessage("Role is required.")
            .Must(role => Enum.TryParse<AdminUserRole>(role, ignoreCase: true, out _))
            .WithMessage(
                $"Role must be one of: {string.Join(", ", Enum.GetNames<AdminUserRole>())}."
            );
}
