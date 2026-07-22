---
source_file: "src/CampCenter.Infrastructure/Repositories/RoomTaskRepository.cs"
type: "code"
community: "Room Task Management"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Task_Management
---

# RoomTaskRepository

## Context

_Source: `src/CampCenter.Infrastructure/Repositories/RoomTaskRepository.cs` (defined near L8; showing L6–L53 of 61)._

```csharp
namespace CampCenter.Infrastructure.Repositories;

public class RoomTaskRepository : IRoomTaskRepository
{
    private readonly AppDbContext _db;

    public RoomTaskRepository(AppDbContext db) => _db = db;

    public Task<List<RoomTask>> ListAsync(
        RoomTaskStatus? status,
        Guid? bookingId,
        CancellationToken cancellationToken = default
    )
    {
        var query = _db.RoomTasks.Include(t => t.Room).AsQueryable();
        if (status is not null)
        {
            query = query.Where(t => t.Status == status);
        }

        if (bookingId is not null)
        {
            query = query.Where(t => t.BookingId == bookingId);
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

```

## Connections
- [[.AddAsync()_9]] - `method` [EXTRACTED]
- [[.CountOpenAsync()_1]] - `method` [EXTRACTED]
- [[.CountOpenByRoomAsync()_1]] - `method` [EXTRACTED]
- [[.GetByIdAsync()_9]] - `method` [EXTRACTED]
- [[.ListAsync()_7]] - `method` [EXTRACTED]
- [[.Remove()_5]] - `method` [EXTRACTED]
- [[.SaveChangesAsync()_11]] - `method` [EXTRACTED]
- [[AppDbContext]] - `references` [EXTRACTED]
- [[IRoomTaskRepository]] - `implements` [EXTRACTED]
- [[RoomTaskRepository.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Task_Management