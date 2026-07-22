using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IRoomTaskRepository
{
    /// Tasks with rooms loaded, Open first then newest.
    Task<List<RoomTask>> ListAsync(
        RoomTaskStatus? status,
        Guid? bookingId,
        CancellationToken cancellationToken = default
    );

    Task<RoomTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// Open-task counts per room (for the occupancy grid badges).
    Task<Dictionary<Guid, int>> CountOpenByRoomAsync(CancellationToken cancellationToken = default);

    Task<int> CountOpenAsync(CancellationToken cancellationToken = default);

    Task AddAsync(RoomTask task, CancellationToken cancellationToken = default);

    void Remove(RoomTask task);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
