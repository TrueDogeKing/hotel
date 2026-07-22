---
source_file: "src/CampCenter.Domain/Repositories/IRoomTaskRepository.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# IRoomTaskRepository.cs

## Context

_Source: `src/CampCenter.Domain/Repositories/IRoomTaskRepository.cs` (defined near L1; showing L1–L26 of 26)._

```csharp
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
```

## Connections
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `contains` [EXTRACTED]
- [[IRoomTaskRepository]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces