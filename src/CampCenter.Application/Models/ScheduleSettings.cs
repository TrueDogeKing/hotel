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
}
