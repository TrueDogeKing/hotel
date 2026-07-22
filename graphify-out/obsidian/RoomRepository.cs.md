---
source_file: "src/CampCenter.Infrastructure/Repositories/RoomRepository.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# RoomRepository.cs

## Context

_Source: `src/CampCenter.Infrastructure/Repositories/RoomRepository.cs` (defined near L1; showing L1–L38 of 38)._

```csharp
using CampCenter.Domain.Entities;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CampCenter.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _db;

    public RoomRepository(AppDbContext db) => _db = db;

    public Task<List<Room>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _db.Rooms.OrderBy(r => r.Number).ToListAsync(cancellationToken);

    public Task<List<Room>> GetActiveAsync(CancellationToken cancellationToken = default) =>
        _db.Rooms.Where(r => r.IsActive).OrderBy(r => r.Number).ToListAsync(cancellationToken);

    public Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.Rooms.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public Task<Room?> GetByNumberAsync(
        string number,
        CancellationToken cancellationToken = default
    ) => _db.Rooms.FirstOrDefaultAsync(r => r.Number == number, cancellationToken);

    public Task<bool> HasAssignmentsAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.BookingRoomAssignments.AnyAsync(a => a.RoomId == id, cancellationToken);

    public async Task AddAsync(Room room, CancellationToken cancellationToken = default) =>
        await _db.Rooms.AddAsync(room, cancellationToken);

    public void Remove(Room room) => _db.Rooms.Remove(room);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _db.SaveChangesAsync(cancellationToken);
}
```

## Connections
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Persistence]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Repositories]] - `contains` [EXTRACTED]
- [[RoomRepository]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces