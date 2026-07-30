using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IRoomCleaningRepository
{
    /// Progress recorded for every room on one day. Rooms nobody has touched yet have
    /// no row at all, so the caller treats a missing room as Pending.
    Task<List<RoomCleaning>> ListForDateAsync(
        DateOnly date,
        CancellationToken cancellationToken = default
    );

    /// How many rooms were marked Done per day over [from, to] — the counts behind the
    /// day picker, so a glance shows which days are still outstanding.
    Task<Dictionary<DateOnly, int>> CountDoneByDateAsync(
        DateOnly from,
        DateOnly to,
        CancellationToken cancellationToken = default
    );

    Task<RoomCleaning?> GetAsync(
        Guid roomId,
        DateOnly date,
        CancellationToken cancellationToken = default
    );

    Task AddAsync(RoomCleaning cleaning, CancellationToken cancellationToken = default);

    /// Persists changes. Throws ConflictException when the one-row-per-room-per-day
    /// index rejects a duplicate — two people ticking the same room at once.
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
