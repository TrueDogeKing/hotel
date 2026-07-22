---
source_file: "src/CampCenter.Domain/Repositories/IAdminUserRepository.cs"
type: "code"
community: "Admin User Repository Contract"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_User_Repository_Contract
---

# IAdminUserRepository

## Context

_Source: `src/CampCenter.Domain/Repositories/IAdminUserRepository.cs` (defined near L5; showing L3–L12 of 12)._

```csharp
namespace CampCenter.Domain.Repositories;

public interface IAdminUserRepository
{
    Task<AdminUser?> GetByLoginAsync(string login, CancellationToken cancellationToken = default);

    Task<AdminUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
```

## Connections
- [[.GetByIdAsync()]] - `method` [EXTRACTED]
- [[.GetByLoginAsync()]] - `method` [EXTRACTED]
- [[.SaveChangesAsync()]] - `method` [EXTRACTED]
- [[AdminUserRepository]] - `implements` [EXTRACTED]
- [[AuthService]] - `references` [EXTRACTED]
- [[IAdminUserRepository.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_User_Repository_Contract