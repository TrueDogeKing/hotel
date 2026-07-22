---
source_file: "src/CampCenter.Infrastructure/Repositories/RoomRepository.cs"
type: "code"
community: "Room Management"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Management
---

# RoomRepository

## Context

_Source: `src/CampCenter.Infrastructure/Repositories/RoomRepository.cs` (defined near L8; showing L6–L38 of 38)._

```csharp
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
- [[.AddAsync()_8]] - `method` [EXTRACTED]
- [[.GetActiveAsync()_1]] - `method` [EXTRACTED]
- [[.GetAllAsync()_7]] - `method` [EXTRACTED]
- [[.GetByIdAsync()_8]] - `method` [EXTRACTED]
- [[.GetByNumberAsync()_1]] - `method` [EXTRACTED]
- [[.HasAssignmentsAsync()_1]] - `method` [EXTRACTED]
- [[.Remove()_4]] - `method` [EXTRACTED]
- [[.SaveChangesAsync()_10]] - `method` [EXTRACTED]
- [[AppDbContext]] - `references` [EXTRACTED]
- [[IRoomRepository]] - `implements` [EXTRACTED]
- [[RoomRepository.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Management