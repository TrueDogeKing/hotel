---
source_file: "src/CampCenter.Domain/Repositories/IClosureRepository.cs"
type: "code"
community: "Room Closure Management"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Closure_Management
---

# IClosureRepository

## Context

_Source: `src/CampCenter.Domain/Repositories/IClosureRepository.cs` (defined near L5; showing L3–L32 of 32)._

```csharp
namespace CampCenter.Domain.Repositories;

public interface IClosureRepository
{
    /// All closures ordered by start date, with the target room (if any) loaded.
    Task<List<Closure>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Closure?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// Closures whose [StartDate, EndDate] span intersects the stay [start, end)
    /// — i.e. those that could block a booking over that range. Includes both
    /// center-wide (RoomId null) and per-room closures.
    Task<List<Closure>> GetOverlappingAsync(
        DateOnly start,
        DateOnly end,
        CancellationToken cancellationToken = default
    );

    /// Center-wide upcoming closures (RoomId null, not yet ended), for the public site.
    Task<List<Closure>> GetUpcomingCenterWideAsync(
        DateOnly today,
        CancellationToken cancellationToken = default
    );

    Task AddAsync(Closure closure, CancellationToken cancellationToken = default);

    void Remove(Closure closure);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
```

## Connections
- [[.AddAsync()_1]] - `method` [EXTRACTED]
- [[.GetAllAsync()_4]] - `method` [EXTRACTED]
- [[.GetByIdAsync()_2]] - `method` [EXTRACTED]
- [[.GetOverlappingAsync()]] - `method` [EXTRACTED]
- [[.GetUpcomingCenterWideAsync()]] - `method` [EXTRACTED]
- [[.Remove()]] - `method` [EXTRACTED]
- [[.SaveChangesAsync()_2]] - `method` [EXTRACTED]
- [[ClosureRepository]] - `implements` [EXTRACTED]
- [[IClosureRepository.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Closure_Management