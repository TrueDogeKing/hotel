---
source_file: "src/CampCenter.Domain/Repositories/IAdminUserRepository.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# IAdminUserRepository.cs

## Context

_Source: `src/CampCenter.Domain/Repositories/IAdminUserRepository.cs` (defined near L1; showing L1–L12 of 12)._

```csharp
using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IAdminUserRepository
{
    Task<AdminUser?> GetByLoginAsync(string login, CancellationToken cancellationToken = default);

    Task<AdminUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
```

## Connections
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `contains` [EXTRACTED]
- [[IAdminUserRepository]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces