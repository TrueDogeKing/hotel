namespace CampCenter.Application.Models;

/// Camp-schedule policy, bound from the "Schedule" configuration section.
public class ScheduleSettings
{
    public const string SectionName = "Schedule";

    /// On the arrival day, meal slots starting before this are not generated —
    /// the group is still travelling. With the seeded defaults this leaves dinner.
    public TimeOnly ArrivalMealsFrom { get; set; } = new(15, 0);

    /// On the departure day, meal slots ending after this are not generated.
    /// With the seeded defaults this leaves breakfast.
    public TimeOnly DepartureMealsUntil { get; set; } = new(11, 0);

    /// Changeover between two groups' sittings: time to clear and relay the tables.
    /// Two groups are never seated at once, and consecutive sittings are spaced by
    /// the slot's duration plus this gap.
    public int MealGapMinutes { get; set; } = 15;

    /// Where meals are served. Stamped onto every generated meal, which is what makes
    /// "two groups in the same place at once" catch clashing sittings as well: the
    /// canteen is a place like any other, so meal clashes need no separate rule in
    /// the UI. Blank switches the stamping off.
    public string MealLocation { get; set; } = "Stołówka";
}
