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

    /// Booking by manage-token hash, with session, assignments (incl. rooms) and payments loaded.
    Task<Booking?> GetByTokenHashAsync(
        string tokenHash,
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

    /// Persists changes. Throws ConflictException when the unique
    /// (CampSessionId, RoomId) index rejects a concurrently grabbed room.
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
