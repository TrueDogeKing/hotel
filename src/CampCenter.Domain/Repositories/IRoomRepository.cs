using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IRoomRepository
{
    Task<List<Room>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<List<Room>> GetActiveAsync(CancellationToken cancellationToken = default);

    Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Room?> GetByNumberAsync(string number, CancellationToken cancellationToken = default);

    /// True when any booking assignment references the room (past or present).
    Task<bool> HasAssignmentsAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(Room room, CancellationToken cancellationToken = default);

    void Remove(Room room);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
