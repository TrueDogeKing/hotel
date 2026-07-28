using CampCenter.Domain.Entities;

namespace CampCenter.Application.Services;

/// A center meal slot as it applies to one group: the center default, or the
/// group's own override of it. Generation and the travel-day cutoffs work on these
/// effective times, so a group whose breakfast is at 09:00 is judged on 09:00.
public record MealSlot(Guid DefaultId, MealKind Kind, string Label, TimeOnly Start, TimeOnly End)
{
    public static MealSlot FromDefault(MealTimeDefault slot) =>
        new(slot.Id, slot.MealKind, slot.Label, slot.StartTime, slot.EndTime);

    /// The center slot with this group's times substituted in.
    public static MealSlot FromOverride(MealTimeDefault slot, BookingMealTime bookingTime) =>
        new(slot.Id, slot.MealKind, slot.Label, bookingTime.StartTime, bookingTime.EndTime);
}

/// Decides which (day, meal slot) pairs a stay should have. Pure and static so it
/// is unit-testable without a database — same shape as RoomMixCalculator.
public static class MealGenerationPlanner
{
    /// Resolves each active center slot against a group's overrides.
    public static List<MealSlot> EffectiveSlots(
        IReadOnlyList<MealTimeDefault> defaults,
        IReadOnlyCollection<BookingMealTime> overrides
    )
    {
        var byDefaultId = overrides.ToDictionary(o => o.MealTimeDefaultId);
        return
        [
            .. defaults.Select(slot =>
                byDefaultId.TryGetValue(slot.Id, out var own)
                    ? MealSlot.FromOverride(slot, own)
                    : MealSlot.FromDefault(slot)
            ),
        ];
    }

    /// Every (day, slot) pair a stay should get.
    ///
    /// Days run [start, end] INCLUSIVE: the departure day is a real schedule day
    /// even though it is not a night stayed. Arrival and departure days only get
    /// the slots that fit around travel, so a typical stay yields dinner on the
    /// arrival day, all slots on the middle days, and breakfast on the departure day.
    public static IEnumerable<(DateOnly Date, MealSlot Slot)> Plan(
        DateOnly start,
        DateOnly end,
        IReadOnlyList<MealSlot> slots,
        TimeOnly arrivalFrom,
        TimeOnly departureUntil
    )
    {
        for (var date = start; date <= end; date = date.AddDays(1))
        {
            foreach (var slot in slots)
            {
                if (date == start && slot.Start < arrivalFrom)
                {
                    continue;
                }

                if (date == end && slot.End > departureUntil)
                {
                    continue;
                }

                yield return (date, slot);
            }
        }
    }
}
