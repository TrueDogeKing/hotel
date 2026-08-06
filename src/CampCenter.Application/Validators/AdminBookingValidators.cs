using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Domain.Entities;
using FluentValidation;

namespace CampCenter.Application.Validators;

/// Mirrors CreateBookingRequestValidator, minus the room mix (staff-entered groups
/// get their rooms picked automatically) and minus the future-date rule, so a group
/// that already arrived can still be recorded.
public class CreateAdminBookingRequestValidator : AbstractValidator<CreateAdminBookingRequestDto>
{
    public CreateAdminBookingRequestValidator()
    {
        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("The departure date must be after the arrival date.");
        RuleFor(x => x.Headcount).InclusiveBetween(1, 2000);
        RuleFor(x => x.SupervisorCount)
            .InclusiveBetween(0, 2000)
            .LessThanOrEqualTo(x => x.Headcount)
            .WithMessage("There cannot be more supervisors than people in the group.");
        // Money arrives in grosze and is bounded here as well as in the service, so
        // an absurd figure is refused before any rooms are picked.
        RuleFor(x => x.PricePerPersonPerNightGrosze)
            .InclusiveBetween(0, 1_000_000)
            .When(x => x.PricePerPersonPerNightGrosze.HasValue);
        RuleFor(x => x.SupervisorPricePerPersonPerNightGrosze)
            .InclusiveBetween(0, 1_000_000)
            .When(x => x.SupervisorPricePerPersonPerNightGrosze.HasValue);
        RuleFor(x => x.TotalGrosze)
            .InclusiveBetween(0, 100_000_000)
            .When(x => x.TotalGrosze.HasValue);
        RuleFor(x => x.DepositGrosze)
            .InclusiveBetween(0, 100_000_000)
            .When(x => x.DepositGrosze.HasValue);

        RuleFor(x => x.OrganizationName).NotEmpty().MaximumLength(256);
        RuleFor(x => x.ContactName).NotEmpty().MaximumLength(128);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(32)
            .Matches(@"^[0-9+\-() ]+$")
            .WithMessage("Invalid phone number.");
        RuleFor(x => x.Notes).MaximumLength(2000);
        RuleFor(x => x.Status)
            .Must(s => s is null || Enum.TryParse<BookingStatus>(s, ignoreCase: true, out _))
            .WithMessage("Unknown booking status.");
        RuleFor(x => x.Language)
            .Must(l => l is null or "pl" or "en")
            .WithMessage("Language must be 'pl' or 'en'.");
    }
}
