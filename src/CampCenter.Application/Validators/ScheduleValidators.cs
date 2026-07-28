using CampCenter.Application.DTOs.Schedule;
using CampCenter.Domain.Entities;
using FluentValidation;

namespace CampCenter.Application.Validators;

public class CreateScheduleEntryRequestValidator : AbstractValidator<CreateScheduleEntryRequestDto>
{
    public CreateScheduleEntryRequestValidator()
    {
        RuleFor(x => x.BookingId).NotEmpty();
        RuleFor(x => x.Kind).NotEmpty().Must(ScheduleRules.BeAKind).WithMessage(ScheduleRules.KindMessage);
        RuleFor(x => x.MealKind)
            .Must(ScheduleRules.BeAMealKind)
            .When(x => ScheduleRules.IsMeal(x.Kind))
            .WithMessage(ScheduleRules.MealKindMessage);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Menu).MaximumLength(2000);
        RuleFor(x => x.PrepNotes).MaximumLength(2000);
        RuleFor(x => x.Location).MaximumLength(200);
        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage(ScheduleRules.TimeOrderMessage);
    }
}

public class UpdateScheduleEntryRequestValidator : AbstractValidator<UpdateScheduleEntryRequestDto>
{
    public UpdateScheduleEntryRequestValidator()
    {
        RuleFor(x => x.Kind).NotEmpty().Must(ScheduleRules.BeAKind).WithMessage(ScheduleRules.KindMessage);
        RuleFor(x => x.MealKind)
            .Must(ScheduleRules.BeAMealKind)
            .When(x => ScheduleRules.IsMeal(x.Kind))
            .WithMessage(ScheduleRules.MealKindMessage);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Menu).MaximumLength(2000);
        RuleFor(x => x.PrepNotes).MaximumLength(2000);
        RuleFor(x => x.Location).MaximumLength(200);
        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage(ScheduleRules.TimeOrderMessage);
    }
}

public class UpdateDietaryNotesRequestValidator : AbstractValidator<UpdateDietaryNotesRequestDto>
{
    public UpdateDietaryNotesRequestValidator() =>
        RuleFor(x => x.DietaryNotes).MaximumLength(2000);
}

/// Shared between the create and update validators so the two can never drift.
internal static class ScheduleRules
{
    public const string KindMessage = "Kind must be either Meal or Activity.";
    public const string MealKindMessage =
        "A meal must have a meal kind (Breakfast, Lunch, Dinner or Snack).";
    public const string TimeOrderMessage =
        "End time must be after the start time; an entry cannot cross midnight.";

    public static bool IsMeal(string? kind) =>
        string.Equals(kind, nameof(ScheduleEntryKind.Meal), StringComparison.OrdinalIgnoreCase);

    public static bool BeAKind(string? kind) =>
        Enum.TryParse<ScheduleEntryKind>(kind, ignoreCase: true, out _);

    public static bool BeAMealKind(string? mealKind) =>
        Enum.TryParse<MealKind>(mealKind, ignoreCase: true, out _);
}
