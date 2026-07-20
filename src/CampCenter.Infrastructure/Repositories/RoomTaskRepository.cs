using CampCenter.Domain.Entities;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CampCenter.Infrastructure.Repositories;

public class RoomTaskRepository : IRoomTaskRepository
{
    private readonly AppDbContext _db;

    public RoomTaskRepository(AppDbContext db) => _db = db;

    public Task<List<RoomTask>> ListAsync(
        RoomTaskStatus? status,
        Guid? campSessionId,
        CancellationToken cancellationToken = default
    )
    {
        var query = _db.RoomTasks.Include(t => t.Room).AsQueryable();
        if (status is not null)
        {
            query = query.Where(t => t.Status == status);
        }

        if (campSessionId is not null)
        {
            query = query.Where(t => t.CampSessionId == campSessionId);
        }

        return query
            .OrderBy(t => t.Status)
            .ThenByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public Task<RoomTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.RoomTasks.Include(t => t.Room).FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task<Dictionary<Guid, int>> CountOpenByRoomAsync(
        CancellationToken cancellationToken = default
    ) =>
        (
            await _db
                .RoomTasks.Where(t => t.Status == RoomTaskStatus.Open)
                .GroupBy(t => t.RoomId)
                .Select(g => new { g.Key, Count = g.Count() })
                .ToListAsync(cancellationToken)
        ).ToDictionary(x => x.Key, x => x.Count);

    public Task<int> CountOpenAsync(CancellationToken cancellationToken = default) =>
        _db.RoomTasks.CountAsync(t => t.Status == RoomTaskStatus.Open, cancellationToken);

    public async Task AddAsync(RoomTask task, CancellationToken cancellationToken = default) =>
        await _db.RoomTasks.AddAsync(task, cancellationToken);

    public void Remove(RoomTask task) => _db.RoomTasks.Remove(task);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _db.SaveChangesAsync(cancellationToken);
}
