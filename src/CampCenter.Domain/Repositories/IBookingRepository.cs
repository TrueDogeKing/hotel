using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IBookingRepository
{
    /// Room ids assigned within the session by "live" bookings (PendingDeposit,
    /// Confirmed, Completed) — i.e. rooms unavailable to new bookings.
    Task<List<Guid>> GetLiveAssignedRoomIdsAsync(
        Guid campSessionId,
        CancellationToken cancellationToken = default
    );

    Task AddAsync(Booking booking, CancellationToken cancellationToken = default);

    /// Booking by id with session and assignments (incl. rooms) loaded.
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// Admin listing, newest first, with session and assignments loaded.
    Task<List<Booking>> ListAsync(
        Guid? campSessionId,
        BookingStatus? status,
        CancellationToken cancellationToken = default
    );

    /// Kinds of Completed payments per booking (for payment-status badges).
    Task<Dictionary<Guid, List<PaymentKind>>> GetCompletedPaymentKindsAsync(
        IReadOnlyCollection<Guid> bookingIds,
        CancellationToken cancellationToken = default
    );

    /// Booking by manage-token hash, with session, assignments (incl. rooms) and payments loaded.
    Task<Booking?> GetByTokenHashAsync(
        string tokenHash,
        CancellationToken cancellationToken = default
    );

    Task AddPaymentAsync(Payment payment, CancellationToken cancellationToken = default);

    /// Payment by its P24 sessionId, with the booking (incl. session and
    /// assignments) loaded — the webhook works off this.
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

    /// Confirmed bookings whose session already ended.
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

    /// Persists changes. Throws ConflictException when the unique
    /// (CampSessionId, RoomId) index rejects a concurrently grabbed room.
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
