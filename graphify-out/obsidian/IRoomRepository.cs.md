---
source_file: "src/CampCenter.Domain/Repositories/IRoomRepository.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# IRoomRepository.cs

## Context

_Source: `src/CampCenter.Domain/Repositories/IRoomRepository.cs` (defined near L1; showing L1–L23 of 23)._

```csharp
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
```

## Connections
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `contains` [EXTRACTED]
- [[IRoomRepository]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces