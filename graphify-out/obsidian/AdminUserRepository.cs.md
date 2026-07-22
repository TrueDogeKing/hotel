---
source_file: "src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# AdminUserRepository.cs

## Context

_Source: `src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs` (defined near L1; showing L1–L26 of 26)._

```csharp
using CampCenter.Domain.Entities;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Persistence;
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
- [[AdminUserRepository]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Persistence]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Repositories]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces