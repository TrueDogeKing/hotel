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
