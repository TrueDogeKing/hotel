using CampCenter.Application.DTOs.Public;
using FluentValidation;

namespace CampCenter.Application.Validators;

public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequestDto>
{
    public CreateBookingRequestValidator()
    {
        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("The departure date must be after the arrival date.");
        RuleFor(x => x.Headcount).InclusiveBetween(1, 2000);
        RuleFor(x => x.SupervisorCount)
            .InclusiveBetween(0, 2000)
            .LessThanOrEqualTo(x => x.Headcount)
            .WithMessage("There cannot be more supervisors than people in the group.");
        RuleFor(x => x.RoomCounts)
            .NotEmpty()
            .Must(BeSaneCounts)
            .WithMessage("Invalid room counts.");
        // Empty is legitimate — a group with no supervisors picks no rooms for them.
        RuleFor(x => x.SupervisorRoomCounts)
            .Must(BeSaneCounts)
            .WithMessage("Invalid room counts.");
        RuleFor(x => x.OrganizationName).NotEmpty().MaximumLength(256);
        RuleFor(x => x.ContactName).NotEmpty().MaximumLength(128);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(32)
            .Matches(@"^[0-9+\-() ]+$")
            .WithMessage("Invalid phone number.");
        RuleFor(x => x.Notes).MaximumLength(2000);
        RuleFor(x => x.Language)
            .Must(l => l is "pl" or "en")
            .WithMessage("Language must be 'pl' or 'en'.");
    }

    private static bool BeSaneCounts(Dictionary<int, int>? counts) =>
        counts is null || counts.All(kv => kv.Key is > 0 and <= 20 && kv.Value is >= 0 and <= 500);
}
