using CampCenter.Application.DTOs.Schedule;
using CampCenter.Domain.Entities;
using FluentValidation;

namespace CampCenter.Application.Validators;

public class CreateMealTimeDefaultRequestValidator
    : AbstractValidator<CreateMealTimeDefaultRequestDto>
{
    public CreateMealTimeDefaultRequestValidator()
    {
        RuleFor(x => x.MealKind)
            .NotEmpty()
            .Must(MealTimeRules.BeAMealKind)
            .WithMessage(MealTimeRules.MealKindMessage);
        RuleFor(x => x.Label).NotEmpty().MaximumLength(128);
        RuleFor(x => x.DurationMinutes)
            .InclusiveBetween(5, 480)
            .WithMessage(MealTimeRules.DurationMessage);
        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage(MealTimeRules.TimeOrderMessage);
    }
}

public class UpdateMealTimeDefaultRequestValidator
    : AbstractValidator<UpdateMealTimeDefaultRequestDto>
{
    public UpdateMealTimeDefaultRequestValidator()
    {
        RuleFor(x => x.MealKind)
            .NotEmpty()
            .Must(MealTimeRules.BeAMealKind)
            .WithMessage(MealTimeRules.MealKindMessage);
        RuleFor(x => x.Label).NotEmpty().MaximumLength(128);
        RuleFor(x => x.DurationMinutes)
            .InclusiveBetween(5, 480)
            .WithMessage(MealTimeRules.DurationMessage);
        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage(MealTimeRules.TimeOrderMessage);
    }
}

public class SetBookingMealTimeRequestValidator : AbstractValidator<SetBookingMealTimeRequestDto>
{
    public SetBookingMealTimeRequestValidator() =>
        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage(MealTimeRules.TimeOrderMessage);
}

internal static class MealTimeRules
{
    public const string MealKindMessage = "Meal kind must be Breakfast, Lunch, Dinner or Snack.";
    public const string TimeOrderMessage = "End time must be after the start time.";
    public const string DurationMessage = "A sitting must last between 5 and 480 minutes.";

    public static bool BeAMealKind(string? mealKind) =>
        Enum.TryParse<MealKind>(mealKind, ignoreCase: true, out _);
}
