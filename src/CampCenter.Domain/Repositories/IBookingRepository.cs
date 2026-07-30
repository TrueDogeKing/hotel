using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IBookingRepository
{
    /// Room ids held by "live" bookings (PendingDeposit, Confirmed, Completed)
    /// whose stay overlaps [start, end) — i.e. rooms unavailable over that range.
    /// <paramref name="excludeBookingId"/> ignores one booking's own rooms (reassign).
    Task<List<Guid>> GetBookedRoomIdsInRangeAsync(
        DateOnly start,
        DateOnly end,
        Guid? excludeBookingId = null,
        CancellationToken cancellationToken = default
    );

    Task AddAsync(Booking booking, CancellationToken cancellationToken = default);

    /// Booking by id with assignments (incl. rooms) loaded.
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// Admin listing, newest first, with assignments loaded.
    Task<List<Booking>> ListAsync(
        BookingStatus? status,
        CancellationToken cancellationToken = default
    );

    /// Live bookings whose stay overlaps [start, end), with assignments (incl.
    /// rooms) loaded — the source for the occupancy grid over a date range.
    Task<List<Booking>> ListLiveInRangeAsync(
        DateOnly start,
        DateOnly end,
        CancellationToken cancellationToken = default
    );

    /// Live bookings *present* on any day in [firstDay, lastDay] — INCLUSIVE at
    /// both ends, which is the schedule's window rather than the half-open window
    /// used for room occupancy. Use this, not ListLiveInRangeAsync, for anything
    /// schedule-related: the half-open predicate drops a group on its departure day.
    /// No assignments loaded — the calendar and day views don't need rooms.
    Task<List<Booking>> ListLivePresentInAsync(
        DateOnly firstDay,
        DateOnly lastDay,
        CancellationToken cancellationToken = default
    );

    /// Live bookings with an assignment that *starts or ends* somewhere in
    /// [firstDay, lastDay], with assignments (incl. rooms) loaded — the changeovers in
    /// that window, which is what housekeeping works from.
    ///
    /// Deliberately not ListLiveInRangeAsync: that one asks who is *staying* over a
    /// half-open range and so excludes a group whose EndDate is the first day — exactly
    /// the group whose rooms have to be cleaned that morning.
    Task<List<Booking>> ListLiveChangingOverAsync(
        DateOnly firstDay,
        DateOnly lastDay,
        CancellationToken cancellationToken = default
    );

    /// Live bookings starting on/after <paramref name="from"/>, earliest first.
    Task<List<Booking>> ListUpcomingAsync(
        DateOnly from,
        int take,
        CancellationToken cancellationToken = default
    );

    /// One page of the bookings in a category, with the category's total so the
    /// caller knows how much is left. Paged in the database rather than filtered in
    /// memory: the dashboard opens these lists on demand and the inactive one grows
    /// without bound, so the page must never depend on loading the whole history.
    ///
    /// Ordered by nearness to today: Current by departure (who leaves first),
    /// Upcoming by arrival (who comes first), Inactive by arrival descending (most
    /// recently here first).
    Task<(List<Booking> Items, int Total)> ListByCategoryAsync(
        BookingGroupCategory category,
        DateOnly today,
        int skip,
        int take,
        CancellationToken cancellationToken = default
    );

    /// Kinds of Completed payments per booking (for payment-status badges).
    Task<Dictionary<Guid, List<PaymentKind>>> GetCompletedPaymentKindsAsync(
        IReadOnlyCollection<Guid> bookingIds,
        CancellationToken cancellationToken = default
    );

    /// Booking by manage-token hash, with assignments (incl. rooms) and payments loaded.
    Task<Booking?> GetByTokenHashAsync(
        string tokenHash,
        CancellationToken cancellationToken = default
    );

    Task AddPaymentAsync(Payment payment, CancellationToken cancellationToken = default);

    /// Payment by its P24 sessionId, with the booking (incl. assignments) loaded —
    /// the webhook works off this.
    Task<Payment?> GetPaymentByP24SessionIdAsync(
        string p24SessionId,
        CancellationToken cancellationToken = default
    );

    Task<List<Payment>> GetPaymentsAsync(
        Guid bookingId,
        CancellationToken cancellationToken = default
    );

    /// PendingDeposit bookings whose hold expired and which have no Pending payment
    /// created after <paramref name="paymentGraceCutoffUtc"/> (in-flight P24 sessions).
    Task<List<Booking>> GetExpiredPendingAsync(
        DateTime nowUtc,
        DateTime paymentGraceCutoffUtc,
        CancellationToken cancellationToken = default
    );

    /// Confirmed bookings whose stay already ended.
    Task<List<Booking>> GetConfirmedEndedAsync(
        DateOnly today,
        CancellationToken cancellationToken = default
    );

    /// Detaches a failed booking (and its assignments) from the unit of work so a
    /// retry with fresh rooms starts clean.
    void Detach(Booking booking);

    /// Deletes the booking's room assignments — this is what frees the rooms.
    void RemoveAssignments(Booking booking);

    /// Deletes a single assignment (used when diffing a reassignment).
    void RemoveAssignment(BookingRoomAssignment assignment);

    /// Tracks a new assignment as Added. Required when attaching to an
    /// already-tracked booking: entities discovered via navigation fixup with
    /// client-set keys would be tracked as Modified and UPDATE a missing row.
    Task AddAssignmentAsync(
        BookingRoomAssignment assignment,
        CancellationToken cancellationToken = default
    );

    /// Persists changes. Throws ConflictException when the overlap exclusion
    /// constraint rejects a concurrently grabbed room.
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
