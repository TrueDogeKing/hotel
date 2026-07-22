---
source_file: "src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# CampCenter.Infrastructure.Repositories

## Context

_Source: `src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs` (defined near L6; showing L4–L26 of 26)._

```csharp
using Microsoft.EntityFrameworkCore;

namespace CampCenter.Infrastructure.Repositories;

/// Implementacja <see cref="IAdminUserRepository"/> oparta o <see cref="AppDbContext"/>.
public class AdminUserRepository : IAdminUserRepository
{
    private readonly AppDbContext _db;

    /// Creates repository with database context.
    public AdminUserRepository(AppDbContext db) => _db = db;

    public Task<AdminUser?> GetByLoginAsync(
        string login,
        CancellationToken cancellationToken = default
    ) => _db.AdminUsers.FirstOrDefaultAsync(u => u.Login == login, cancellationToken);

    public Task<AdminUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.AdminUsers.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _db.SaveChangesAsync(cancellationToken);
}
```

## Connections
- [[AdminUserRepository.cs]] - `contains` [EXTRACTED]
- [[BookingRepository.cs]] - `contains` [EXTRACTED]
- [[ClosureRepository.cs]] - `contains` [EXTRACTED]
- [[DependencyInjection.cs_1]] - `imports` [EXTRACTED]
- [[RefreshTokenRepository.cs]] - `contains` [EXTRACTED]
- [[RoomRepository.cs]] - `contains` [EXTRACTED]
- [[RoomTaskRepository.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces