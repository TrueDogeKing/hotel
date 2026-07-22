---
source_file: "src/CampCenter.Infrastructure/Repositories/ClosureRepository.cs"
type: "code"
community: "Room Closure Management"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Closure_Management
---

# ClosureRepository

## Context

_Source: `src/CampCenter.Infrastructure/Repositories/ClosureRepository.cs` (defined near L8; showing L6–L50 of 50)._

```csharp
namespace CampCenter.Infrastructure.Repositories;

public class ClosureRepository : IClosureRepository
{
    private readonly AppDbContext _db;

    public ClosureRepository(AppDbContext db) => _db = db;

    public Task<List<Closure>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _db
            .Closures.Include(c => c.Room)
            .OrderBy(c => c.StartDate)
            .ToListAsync(cancellationToken);

    public Task<Closure?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.Closures.Include(c => c.Room).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public Task<List<Closure>> GetOverlappingAsync(
        DateOnly start,
        DateOnly end,
        CancellationToken cancellationToken = default
    ) =>
        // The closure blocks days [StartDate, EndDate]; the stay occupies nights
        // [start, end). They intersect when start <= EndDate && StartDate < end.
        _db
            .Closures.Where(c => c.StartDate < end && start <= c.EndDate)
            .ToListAsync(cancellationToken);

    public Task<List<Closure>> GetUpcomingCenterWideAsync(
        DateOnly today,
        CancellationToken cancellationToken = default
    ) =>
        _db
            .Closures.Where(c => c.RoomId == null && c.EndDate >= today)
            .OrderBy(c => c.StartDate)
            .ToListAsync(cancellationToken);

    public async Task AddAsync(Closure closure, CancellationToken cancellationToken = default) =>
        await _db.Closures.AddAsync(closure, cancellationToken);

    public void Remove(Closure closure) => _db.Closures.Remove(closure);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _db.SaveChangesAsync(cancellationToken);
}
```

## Connections
- [[.AddAsync()_6]] - `method` [EXTRACTED]
- [[.GetAllAsync()_6]] - `method` [EXTRACTED]
- [[.GetByIdAsync()_7]] - `method` [EXTRACTED]
- [[.GetOverlappingAsync()_1]] - `method` [EXTRACTED]
- [[.GetUpcomingCenterWideAsync()_1]] - `method` [EXTRACTED]
- [[.Remove()_3]] - `method` [EXTRACTED]
- [[.SaveChangesAsync()_8]] - `method` [EXTRACTED]
- [[AppDbContext]] - `references` [EXTRACTED]
- [[ClosureRepository.cs]] - `contains` [EXTRACTED]
- [[IClosureRepository]] - `implements` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Closure_Management