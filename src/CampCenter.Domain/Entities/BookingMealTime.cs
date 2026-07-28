namespace CampCenter.Domain.Entities;

/// One group's own time for a center meal slot — so the 8:00 breakfast sitting can
/// be split across groups instead of everyone arriving at once.
///
/// Absence of a row means "use the center default". Overrides are per (booking,
/// slot) pair and drive both future generation and the bulk re-time of a stay's
/// existing meals.
public class BookingMealTime
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }

    public Booking? Booking { get; set; }

    /// The center slot being overridden. Deleting the slot deletes the override —
    /// there is nothing left to override.
    public Guid MealTimeDefaultId { get; set; }

    public MealTimeDefault? MealTimeDefault { get; set; }

    /// EndTime is always > StartTime, like ScheduleEntry — a meal never crosses midnight.
    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public uint RowVersion { get; set; }
}
