---
source_file: "src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs"
type: "code"
community: "Refresh Token Contract"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Refresh_Token_Contract
---

# IRefreshTokenRepository

## Context

_Source: `src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs` (defined near L5; showing L3–L29 of 29)._

```csharp
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
- [[.AddAsync()_2]] - `method` [EXTRACTED]
- [[.GetByTokenHashAsync()_1]] - `method` [EXTRACTED]
- [[.RevokeAllActiveForUserAsync()]] - `method` [EXTRACTED]
- [[.SaveChangesAsync()_3]] - `method` [EXTRACTED]
- [[AuthService]] - `references` [EXTRACTED]
- [[IRefreshTokenRepository.cs]] - `contains` [EXTRACTED]
- [[RefreshTokenRepository]] - `implements` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Refresh_Token_Contract