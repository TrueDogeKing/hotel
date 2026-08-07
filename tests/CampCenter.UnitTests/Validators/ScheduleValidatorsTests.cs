using CampCenter.Application.DTOs.Schedule;
using CampCenter.Application.Validators;

namespace CampCenter.UnitTests.Validators;

public class ScheduleEntryValidatorsTests
{
    private readonly CreateScheduleEntryRequestValidator _validator = new();

    private static CreateScheduleEntryRequestDto ValidActivity() =>
        new(
            Guid.NewGuid(),
            "Activity",
            null,
            new DateOnly(2026, 8, 5),
            new TimeOnly(10, 0),
            new TimeOnly(12, 0),
            "Spływ kajakowy",
            null,
            "Kamizelki dla 12 osób",
            "Przystań",
            12
        );

    private static CreateScheduleEntryRequestDto ValidMeal() =>
        ValidActivity() with
        {
            Kind = "Meal",
            MealKind = "Lunch",
            Title = "Obiad",
            Menu = "Rosół, kotlet, kompot",
        };

    [Fact]
    public void ValidActivity_Passes() => Assert.True(_validator.Validate(ValidActivity()).IsValid);

    [Fact]
    public void ValidMeal_Passes() => Assert.True(_validator.Validate(ValidMeal()).IsValid);

    [Fact]
    public void EndTimeBeforeStartTime_Fails()
    {
        var dto = ValidActivity() with { EndTime = new TimeOnly(9, 0) };
        Assert.False(_validator.Validate(dto).IsValid);
    }

    /// An entry may not cross midnight — otherwise "which day is this on?" is
    /// ambiguous and the day view cannot position it.
    [Fact]
    public void EqualStartAndEndTime_Fails()
    {
        var dto = ValidActivity() with { EndTime = ValidActivity().StartTime };
        Assert.False(_validator.Validate(dto).IsValid);
    }

    [Fact]
    public void MealWithoutMealKind_Fails()
    {
        var dto = ValidMeal() with { MealKind = null };
        Assert.False(_validator.Validate(dto).IsValid);
    }

    [Fact]
    public void MealWithUnknownMealKind_Fails()
    {
        var dto = ValidMeal() with { MealKind = "Brunch" };
        Assert.False(_validator.Validate(dto).IsValid);
    }

    /// Meal kind is only required for meals; an activity may leave it unset.
    [Fact]
    public void ActivityWithoutMealKind_Passes() =>
        Assert.True(_validator.Validate(ValidActivity() with { MealKind = null }).IsValid);

    [Fact]
    public void UnknownKind_Fails()
    {
        var dto = ValidActivity() with { Kind = "Excursion" };
        Assert.False(_validator.Validate(dto).IsValid);
    }

    [Fact]
    public void KindIsCaseInsensitive() =>
        Assert.True(_validator.Validate(ValidActivity() with { Kind = "activity" }).IsValid);

    [Fact]
    public void EmptyTitle_Fails()
    {
        var dto = ValidActivity() with { Title = "" };
        Assert.False(_validator.Validate(dto).IsValid);
    }

    [Fact]
    public void OverlongTitle_Fails()
    {
        var dto = ValidActivity() with { Title = new string('x', 201) };
        Assert.False(_validator.Validate(dto).IsValid);
    }

    [Fact]
    public void OverlongMenuOrPrepNotes_Fails()
    {
        var longText = new string('x', 2001);
        Assert.False(_validator.Validate(ValidMeal() with { Menu = longText }).IsValid);
        Assert.False(_validator.Validate(ValidActivity() with { PrepNotes = longText }).IsValid);
    }

    [Fact]
    public void EmptyBookingId_Fails()
    {
        var dto = ValidActivity() with { BookingId = Guid.Empty };
        Assert.False(_validator.Validate(dto).IsValid);
    }

    [Fact]
    public void ZeroOrNegativeParticipantCount_Fails()
    {
        Assert.False(_validator.Validate(ValidActivity() with { ParticipantCount = 0 }).IsValid);
        Assert.False(_validator.Validate(ValidActivity() with { ParticipantCount = -1 }).IsValid);
    }

    [Fact]
    public void NullParticipantCount_Passes() =>
        Assert.True(_validator.Validate(ValidActivity() with { ParticipantCount = null }).IsValid);

    /// The update validator must enforce the same rules as create, or a rule could
    /// be bypassed by creating a valid entry and then editing it.
    [Fact]
    public void UpdateValidator_EnforcesTheSameRules()
    {
        var update = new UpdateScheduleEntryRequestValidator();
        var valid = new UpdateScheduleEntryRequestDto(
            "Meal",
            "Lunch",
            new DateOnly(2026, 8, 5),
            new TimeOnly(13, 0),
            new TimeOnly(14, 0),
            "Obiad",
            "Rosół",
            null,
            null,
            null,
            RowVersion: 1
        );

        Assert.True(update.Validate(valid).IsValid);
        Assert.False(update.Validate(valid with { MealKind = null }).IsValid);
        Assert.False(update.Validate(valid with { EndTime = new TimeOnly(12, 0) }).IsValid);
        Assert.False(update.Validate(valid with { Title = "" }).IsValid);
        Assert.False(update.Validate(valid with { Kind = "Excursion" }).IsValid);
    }
}

public class MealTimeValidatorsTests
{
    private readonly CreateMealTimeDefaultRequestValidator _validator = new();

    private static CreateMealTimeDefaultRequestDto Valid() =>
        new("Breakfast", "Śniadanie", new TimeOnly(8, 0), new TimeOnly(9, 0), 30);

    [Fact]
    public void ValidSlot_Passes() => Assert.True(_validator.Validate(Valid()).IsValid);

    [Fact]
    public void EndBeforeStart_Fails() =>
        Assert.False(_validator.Validate(Valid() with { EndTime = new TimeOnly(7, 0) }).IsValid);

    [Fact]
    public void UnknownMealKind_Fails() =>
        Assert.False(_validator.Validate(Valid() with { MealKind = "Brunch" }).IsValid);

    [Fact]
    public void EmptyLabel_Fails() =>
        Assert.False(_validator.Validate(Valid() with { Label = "" }).IsValid);

    [Fact]
    public void UpdateValidator_EnforcesTheSameRules()
    {
        var update = new UpdateMealTimeDefaultRequestValidator();
        var valid = new UpdateMealTimeDefaultRequestDto(
            "Dinner",
            "Kolacja",
            new TimeOnly(18, 0),
            new TimeOnly(19, 0),
            DurationMinutes: 30,
            IsActive: true,
            RowVersion: 1
        );

        Assert.True(update.Validate(valid).IsValid);
        Assert.False(update.Validate(valid with { EndTime = new TimeOnly(17, 0) }).IsValid);
        Assert.False(update.Validate(valid with { MealKind = "Supper" }).IsValid);
        Assert.False(update.Validate(valid with { Label = "" }).IsValid);
    }
}
