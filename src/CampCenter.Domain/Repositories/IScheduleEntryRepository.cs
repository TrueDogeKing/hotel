using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IScheduleEntryRepository
{
    Task<ScheduleEntry?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// Every entry on one day for live bookings, with the booking loaded (one join,
    /// so the day view never needs a second round-trip for group names).
    /// Ordered by start time, then organization name.
    Task<List<ScheduleEntry>> ListForDateAsync(
        DateOnly date,
        CancellationToken cancellationToken = default
    );

    /// A single group's visible programme, ordered chronologically.
    Task<List<ScheduleEntry>> ListForBookingAsync(
        Guid bookingId,
        CancellationToken cancellationToken = default
    );

    /// The (day, meal slot) pairs this booking has already been generated for,
    /// INCLUDING suppressed ones. This is what makes generation idempotent and
    /// stops it resurrecting a meal the admin deliberately deleted.
    Task<List<(DateOnly Date, Guid MealTimeDefaultId)>> ListGeneratedSlotsAsync(
        Guid bookingId,
        CancellationToken cancellationToken = default
    );

    /// The (booking, meal slot) pairs whose meals have all been deleted — the group
    /// opted out of that meal entirely. Such a group must not go on reserving a
    /// sitting other groups could use.
    Task<List<(Guid BookingId, Guid MealTimeDefaultId)>> ListFullySuppressedSlotsAsync(
        IReadOnlyCollection<Guid> bookingIds,
        CancellationToken cancellationToken = default
    );

    /// The span each group's remaining meals actually occupy, per (booking, slot).
    /// This is the truth about when a group eats — the centre's window may have been
    /// edited since those meals were generated, so it cannot be inferred from it.
    Task<
        List<(Guid BookingId, Guid MealTimeDefaultId, TimeOnly Start, TimeOnly End)>
    > ListVisibleSlotSpansAsync(
        IReadOnlyCollection<Guid> bookingIds,
        CancellationToken cancellationToken = default
    );

    /// Every place already used anywhere in the schedule, alphabetically. Feeds the
    /// entry form's suggestions: a place typed two ways is two places as far as clash
    /// detection is concerned, so picking beats re-typing.
    Task<List<string>> ListLocationsAsync(CancellationToken cancellationToken = default);

    /// Per-day meal/activity counts over [start, end] for the given bookings —
    /// one grouped aggregate, used for the calendar's day badges.
    Task<List<(DateOnly Date, ScheduleEntryKind Kind, int Count)>> CountByDateAndKindAsync(
        DateOnly start,
        DateOnly end,
        IReadOnlyCollection<Guid> bookingIds,
        CancellationToken cancellationToken = default
    );

    Task AddRangeAsync(
        IReadOnlyCollection<ScheduleEntry> entries,
        CancellationToken cancellationToken = default
    );

    Task AddAsync(ScheduleEntry entry, CancellationToken cancellationToken = default);

    void Remove(ScheduleEntry entry);

    /// Persists changes. Throws ConflictException when the generation idempotency
    /// index rejects a duplicate (concurrent P24 notification retries).
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
