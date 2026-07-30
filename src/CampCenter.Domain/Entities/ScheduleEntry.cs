namespace CampCenter.Domain.Entities;

public enum ScheduleEntryKind
{
    Meal,
    Activity,
}

/// One hour-to-hour item in a group's camp programme: a meal or an activity.
///
/// Meals and activities share one entity because every view of the schedule (day
/// timetable, group programme, the booker's read-only copy) needs them merged into
/// a single chronological stream — splitting the table would mean repeating that
/// merge in three places.
public class ScheduleEntry
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }

    public Booking? Booking { get; set; }

    public ScheduleEntryKind Kind { get; set; }

    /// Set iff Kind == Meal. This — not Title — is the grouping key behind
    /// "Śniadanie 08:00–09:00 · 3 grupy": grouping by title breaks as soon as one
    /// group's breakfast gets renamed.
    public MealKind? MealKind { get; set; }

    /// The default this entry was generated from; null for hand-added entries.
    /// Doubles as the idempotency key for meal generation (see the filtered unique
    /// index in ScheduleEntryConfiguration).
    public Guid? MealTimeDefaultId { get; set; }

    public MealTimeDefault? MealTimeDefault { get; set; }

    /// A day within [Booking.StartDate, Booking.EndDate] — INCLUSIVE at both ends.
    ///
    /// This deliberately differs from room occupancy, which is half-open: the group
    /// is physically present on the departure day (they eat breakfast before the
    /// coach leaves), so EndDate is a valid schedule day even though it is not a
    /// night stayed. Never reason about the schedule with the half-open predicates
    /// used for availability.
    public DateOnly Date { get; set; }

    /// EndTime is always > StartTime — an entry never crosses midnight, otherwise
    /// "which day does this belong to?" becomes ambiguous and every ordering and
    /// positioning computation in the day view breaks. A night hike is entered as
    /// two entries.
    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public required string Title { get; set; }

    /// What is served. Meals only ("Rosół, kotlet, kompot").
    public string? Menu { get; set; }

    /// Internal kitchen/staff note — what has to be prepared. Never exposed on the
    /// public token endpoint.
    public string? PrepNotes { get; set; }

    public string? Location { get; set; }

    /// How many of the group actually take part in this meal or activity —
    /// defaults to the group's full Headcount but can be reduced (a subset opts
    /// out) or raised (guests from another group join in). Null means "the whole
    /// group"; auto-generated meals are left null until an admin overrides one.
    public int? ParticipantCount { get; set; }

    /// Set when an admin moves this one entry to a different time. Re-timing the
    /// whole stay (via the group's meal times) deliberately skips these, so a
    /// one-off change — a trip day with an early lunch — survives a later bulk edit.
    public bool TimesCustomized { get; set; }

    /// Set when an admin deletes a *generated* meal. The row is kept so the
    /// (booking, date, slot) pair stays occupied and meal generation will not
    /// recreate it — a group that skips lunch on trip day must stay skipped.
    /// Suppressed entries are excluded from every read. Hand-added entries have
    /// no slot to occupy and are hard-deleted instead.
    public bool IsSuppressed { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public uint RowVersion { get; set; }
}
