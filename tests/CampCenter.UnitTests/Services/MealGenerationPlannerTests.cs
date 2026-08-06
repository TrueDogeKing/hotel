using CampCenter.Application.Services;
using CampCenter.Domain.Entities;

namespace CampCenter.UnitTests.Services;

public class MealGenerationPlannerTests
{
    private static readonly TimeOnly ArrivalFrom = new(15, 0);
    private static readonly TimeOnly DepartureUntil = new(11, 0);

    private static MealTimeDefault Default(
        MealKind kind,
        string label,
        int startHour,
        int endHour
    ) =>
        new()
        {
            Id = Guid.NewGuid(),
            MealKind = kind,
            Label = label,
            StartTime = new TimeOnly(startHour, 0),
            EndTime = new TimeOnly(endHour, 0),
        };

    private static List<MealTimeDefault> SeededDefaults() =>
        [
            Default(MealKind.Breakfast, "Śniadanie", 8, 9),
            Default(MealKind.Lunch, "Obiad", 13, 14),
            Default(MealKind.Dinner, "Kolacja", 18, 19),
        ];

    private static List<MealSlot> SeededSlots() =>
        MealGenerationPlanner.EffectiveSlots(SeededDefaults(), []);

    [Fact]
    public void Plan_FiveNightStay_GivesDinnerOnArrival_AllMiddleDays_BreakfastOnDeparture()
    {
        var slots = SeededSlots();
        var start = new DateOnly(2033, 5, 1);
        var end = new DateOnly(2033, 5, 6); // 5 nights, 6 schedule days

        var plan = MealGenerationPlanner
            .Plan(start, end, slots, ArrivalFrom, DepartureUntil)
            .ToList();

        // Arrival day: only the dinner slot starts after 15:00.
        var arrival = plan.Where(p => p.Date == start).ToList();
        Assert.Single(arrival);
        Assert.Equal(MealKind.Dinner, arrival[0].Slot.Kind);

        // Departure day: only breakfast ends before 11:00.
        var departure = plan.Where(p => p.Date == end).ToList();
        Assert.Single(departure);
        Assert.Equal(MealKind.Breakfast, departure[0].Slot.Kind);

        // Four middle days get all three slots.
        Assert.Equal(4 * 3, plan.Count(p => p.Date > start && p.Date < end));
        Assert.Equal(1 + 12 + 1, plan.Count);
    }

    [Fact]
    public void Plan_OneNightStay_HasNoMiddleDays()
    {
        var start = new DateOnly(2033, 5, 1);
        var end = new DateOnly(2033, 5, 2);

        var plan = MealGenerationPlanner
            .Plan(start, end, SeededSlots(), ArrivalFrom, DepartureUntil)
            .ToList();

        Assert.Equal(2, plan.Count);
        Assert.Equal(MealKind.Dinner, plan[0].Slot.Kind);
        Assert.Equal(MealKind.Breakfast, plan[1].Slot.Kind);
    }

    [Fact]
    public void Plan_NoActiveDefaults_YieldsNothing()
    {
        var plan = MealGenerationPlanner.Plan(
            new DateOnly(2033, 5, 1),
            new DateOnly(2033, 5, 6),
            [],
            ArrivalFrom,
            DepartureUntil
        );

        Assert.Empty(plan);
    }

    [Fact]
    public void Plan_SlotStraddlingBothCutoffs_IsSkippedOnArrivalAndDeparture()
    {
        // Lunch starts before 15:00 and ends after 11:00, so it is excluded on both
        // the arrival and the departure day but present on every middle day.
        var slots = MealGenerationPlanner.EffectiveSlots(
            [Default(MealKind.Lunch, "Obiad", 13, 14)],
            []
        );
        var start = new DateOnly(2033, 5, 1);
        var end = new DateOnly(2033, 5, 4);

        var plan = MealGenerationPlanner
            .Plan(start, end, slots, ArrivalFrom, DepartureUntil)
            .ToList();

        Assert.Equal(2, plan.Count);
        Assert.All(plan, p => Assert.True(p.Date > start && p.Date < end));
    }

    [Fact]
    public void Plan_CoversDepartureDay_NotJustNightsStayed()
    {
        // The stay is half-open for rooms but inclusive for the schedule: a group
        // arriving on the 1st and leaving on the 4th has 4 schedule days.
        var start = new DateOnly(2033, 5, 1);
        var end = new DateOnly(2033, 5, 4);

        var days = MealGenerationPlanner
            .Plan(start, end, SeededSlots(), ArrivalFrom, DepartureUntil)
            .Select(p => p.Date)
            .Distinct()
            .ToList();

        Assert.Equal(4, days.Count);
        Assert.Contains(end, days);
    }

    // --- Per-group overrides ---------------------------------------------

    [Fact]
    public void EffectiveSlots_WithoutOverrides_UsesCenterTimes()
    {
        var defaults = SeededDefaults();

        var slots = MealGenerationPlanner.EffectiveSlots(defaults, []);

        Assert.Equal(3, slots.Count);
        Assert.All(
            slots,
            slot =>
            {
                var source = defaults.Single(d => d.Id == slot.DefaultId);
                Assert.Equal(source.StartTime, slot.Start);
                Assert.Equal(source.EndTime, slot.End);
            }
        );
    }

    [Fact]
    public void EffectiveSlots_AppliesTheGroupsOwnTimes_LeavingOtherSlotsAlone()
    {
        var defaults = SeededDefaults();
        var breakfast = defaults.Single(d => d.MealKind == MealKind.Breakfast);

        var slots = MealGenerationPlanner.EffectiveSlots(
            defaults,
            [
                new BookingMealTime
                {
                    MealTimeDefaultId = breakfast.Id,
                    StartTime = new TimeOnly(9, 30),
                    EndTime = new TimeOnly(10, 15),
                },
            ]
        );

        var shifted = slots.Single(s => s.DefaultId == breakfast.Id);
        Assert.Equal(new TimeOnly(9, 30), shifted.Start);
        Assert.Equal(new TimeOnly(10, 15), shifted.End);
        // The label and kind still come from the center slot.
        Assert.Equal("Śniadanie", shifted.Label);
        Assert.Equal(MealKind.Breakfast, shifted.Kind);

        var lunch = slots.Single(s => s.Kind == MealKind.Lunch);
        Assert.Equal(new TimeOnly(13, 0), lunch.Start);
    }

    /// The travel-day cutoffs must be judged on the group's own times: a group
    /// whose breakfast is pushed past 11:00 should not get one on departure day.
    [Fact]
    public void Plan_UsesOverriddenTimes_ForTheTravelDayCutoffs()
    {
        var defaults = SeededDefaults();
        var breakfast = defaults.Single(d => d.MealKind == MealKind.Breakfast);
        var start = new DateOnly(2033, 5, 1);
        var end = new DateOnly(2033, 5, 4);

        var lateBreakfast = MealGenerationPlanner.EffectiveSlots(
            defaults,
            [
                new BookingMealTime
                {
                    MealTimeDefaultId = breakfast.Id,
                    StartTime = new TimeOnly(11, 0),
                    EndTime = new TimeOnly(12, 0), // ends after the 11:00 cutoff
                },
            ]
        );

        var plan = MealGenerationPlanner
            .Plan(start, end, lateBreakfast, ArrivalFrom, DepartureUntil)
            .ToList();

        // Departure day now has no meal at all — its breakfast runs too late.
        Assert.DoesNotContain(plan, p => p.Date == end);
    }

    /// The mirror case: an early dinner override pulls the arrival day's meal out.
    [Fact]
    public void Plan_OverrideCanRemoveTheArrivalDayMeal()
    {
        var defaults = SeededDefaults();
        var dinner = defaults.Single(d => d.MealKind == MealKind.Dinner);
        var start = new DateOnly(2033, 5, 1);

        var earlyDinner = MealGenerationPlanner.EffectiveSlots(
            defaults,
            [
                new BookingMealTime
                {
                    MealTimeDefaultId = dinner.Id,
                    StartTime = new TimeOnly(14, 30), // starts before the 15:00 cutoff
                    EndTime = new TimeOnly(15, 30),
                },
            ]
        );

        var plan = MealGenerationPlanner
            .Plan(start, new DateOnly(2033, 5, 4), earlyDinner, ArrivalFrom, DepartureUntil)
            .ToList();

        Assert.DoesNotContain(plan, p => p.Date == start);
    }
}
