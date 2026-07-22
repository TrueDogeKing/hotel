---
source_file: "src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs"
type: "code"
community: "Admin User Repository"
location: "L9"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_User_Repository
---

# AdminUserRepository

## Context

_Source: `src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs` (defined near L9; showing L7–L26 of 26)._

```csharp

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
- [[.GetByIdAsync()_5]] - `method` [EXTRACTED]
- [[.GetByLoginAsync()_1]] - `method` [EXTRACTED]
- [[.SaveChangesAsync()_6]] - `method` [EXTRACTED]
- [[AdminUserRepository.cs]] - `contains` [EXTRACTED]
- [[AppDbContext]] - `references` [EXTRACTED]
- [[IAdminUserRepository]] - `implements` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_User_Repository