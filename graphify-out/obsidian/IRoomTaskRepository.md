---
source_file: "src/CampCenter.Domain/Repositories/IRoomTaskRepository.cs"
type: "code"
community: "Room Task Management"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Task_Management
---

# IRoomTaskRepository

## Context

_Source: `src/CampCenter.Domain/Repositories/IRoomTaskRepository.cs` (defined near L5; showing L3–L26 of 26)._

```csharp
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
```

## Connections
- [[.AddAsync()_4]] - `method` [EXTRACTED]
- [[.CountOpenAsync()]] - `method` [EXTRACTED]
- [[.CountOpenByRoomAsync()]] - `method` [EXTRACTED]
- [[.GetByIdAsync()_4]] - `method` [EXTRACTED]
- [[.ListAsync()_5]] - `method` [EXTRACTED]
- [[.Remove()_2]] - `method` [EXTRACTED]
- [[.SaveChangesAsync()_5]] - `method` [EXTRACTED]
- [[AdminBookingService]] - `references` [EXTRACTED]
- [[IRoomTaskRepository.cs]] - `contains` [EXTRACTED]
- [[RoomTaskRepository]] - `implements` [EXTRACTED]
- [[RoomTaskService]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Task_Management