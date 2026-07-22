---
source_file: "src/CampCenter.Infrastructure/Repositories/RoomTaskRepository.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# RoomTaskRepository.cs

## Context

_Source: `src/CampCenter.Infrastructure/Repositories/RoomTaskRepository.cs` (defined near L1; showing L1–L46 of 61)._

```csharp
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
```

## Connections
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Persistence]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Repositories]] - `contains` [EXTRACTED]
- [[RoomTaskRepository]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces