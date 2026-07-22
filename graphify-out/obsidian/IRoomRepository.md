---
source_file: "src/CampCenter.Domain/Repositories/IRoomRepository.cs"
type: "code"
community: "Room Management"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Management
---

# IRoomRepository

## Context

_Source: `src/CampCenter.Domain/Repositories/IRoomRepository.cs` (defined near L5; showing L3–L23 of 23)._

```csharp
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
```

## Connections
- [[.AddAsync()_3]] - `method` [EXTRACTED]
- [[.GetActiveAsync()]] - `method` [EXTRACTED]
- [[.GetAllAsync()_5]] - `method` [EXTRACTED]
- [[.GetByIdAsync()_3]] - `method` [EXTRACTED]
- [[.GetByNumberAsync()]] - `method` [EXTRACTED]
- [[.HasAssignmentsAsync()]] - `method` [EXTRACTED]
- [[.Remove()_1]] - `method` [EXTRACTED]
- [[.SaveChangesAsync()_4]] - `method` [EXTRACTED]
- [[AdminBookingService]] - `references` [EXTRACTED]
- [[AvailabilityService]] - `references` [EXTRACTED]
- [[BookingService]] - `references` [EXTRACTED]
- [[IRoomRepository.cs]] - `contains` [EXTRACTED]
- [[RoomRepository]] - `implements` [EXTRACTED]
- [[RoomService]] - `references` [EXTRACTED]
- [[RoomTaskService]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Management