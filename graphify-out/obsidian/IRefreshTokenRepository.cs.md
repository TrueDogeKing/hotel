---
source_file: "src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# IRefreshTokenRepository.cs

## Context

_Source: `src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs` (defined near L1; showing L1–L29 of 29)._

```csharp
using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IRefreshTokenRepository
{
    /// Adds a new token to the context. Changes are persisted only by <see cref="SaveChangesAsync"/>.
    /// <param name="token">Token to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task AddAsync(RefreshToken token, CancellationToken cancellationToken = default);

    /// Returns the token with the given hash (including the related user) or null.
    /// <param name="tokenHash">Token hash.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<RefreshToken?> GetByTokenHashAsync(
        string tokenHash,
        CancellationToken cancellationToken = default
    );

    Task RevokeAllActiveForUserAsync(
        Guid userId,
        DateTime revokedAtUtc,
        CancellationToken cancellationToken = default
    );

    /// Persists changes to the database.
    /// <param name="cancellationToken">Cancellation token.</param>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
```

## Connections
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `contains` [EXTRACTED]
- [[IRefreshTokenRepository]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces