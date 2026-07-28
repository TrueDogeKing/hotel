namespace CampCenter.Domain.Entities;

public enum MealKind
{
    Breakfast,
    Lunch,
    Dinner,
    Snack,
}

/// A center-wide default meal slot. When a booking is confirmed, one ScheduleEntry
/// per active default per day of the stay is generated from these; the admin can
/// then move, re-menu or delete an individual group's meal.
///
/// Editing a default affects future generation only — it never rewrites entries
/// that already exist.
public class MealTimeDefault
{
    public Guid Id { get; set; }

    public MealKind MealKind { get; set; }

    /// Admin-entered label used as the generated entry's Title (e.g. "Śniadanie").
    /// Free text rather than a translation key, matching Closure.Reason / RoomTask.Text.
    public required string Label { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    /// Display order in the meal-times list and in generated schedules.
    public int SortOrder { get; set; }

    /// Inactive defaults stop being generated but keep their history (entries
    /// already generated from them still reference them).
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public uint RowVersion { get; set; }
}
