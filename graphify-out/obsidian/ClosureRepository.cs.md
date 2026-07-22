---
source_file: "src/CampCenter.Infrastructure/Repositories/ClosureRepository.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# ClosureRepository.cs

## Context

_Source: `src/CampCenter.Infrastructure/Repositories/ClosureRepository.cs` (defined near L1; showing L1–L46 of 50)._

```csharp
using CampCenter.Domain.Entities;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
```

## Connections
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Persistence]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Repositories]] - `contains` [EXTRACTED]
- [[ClosureRepository]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces