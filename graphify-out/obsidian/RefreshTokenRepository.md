---
source_file: "src/CampCenter.Infrastructure/Repositories/RefreshTokenRepository.cs"
type: "code"
community: "Refresh Token Repository"
location: "L9"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Refresh_Token_Repository
---

# RefreshTokenRepository

## Context

_Source: `src/CampCenter.Infrastructure/Repositories/RefreshTokenRepository.cs` (defined near L9; showing L7–L45 of 45)._

```csharp

/// Implementation of <see cref="IRefreshTokenRepository"/> using <see cref="AppDbContext"/>.
public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _db;

    /// Creates repository with database context.
    public RefreshTokenRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(RefreshToken token, CancellationToken cancellationToken = default) =>
        await _db.RefreshTokens.AddAsync(token, cancellationToken);

    public Task<RefreshToken?> GetByTokenHashAsync(
        string tokenHash,
        CancellationToken cancellationToken = default
    ) =>
        _db
            .RefreshTokens.Include(t => t.AdminUser)
            .FirstOrDefaultAsync(t => t.TokenHash == tokenHash, cancellationToken);

    public async Task RevokeAllActiveForUserAsync(
        Guid userId,
        DateTime revokedAtUtc,
        CancellationToken cancellationToken = default
    )
    {
        var activeTokens = await _db
            .RefreshTokens.Where(t => t.AdminUserId == userId && t.RevokedAtUtc == null)
            .ToListAsync(cancellationToken);

        foreach (var token in activeTokens)
        {
            token.RevokedAtUtc = revokedAtUtc;
        }
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _db.SaveChangesAsync(cancellationToken);
}
```

## Connections
- [[.AddAsync()_7]] - `method` [EXTRACTED]
- [[.GetByTokenHashAsync()_3]] - `method` [EXTRACTED]
- [[.RevokeAllActiveForUserAsync()_1]] - `method` [EXTRACTED]
- [[.SaveChangesAsync()_9]] - `method` [EXTRACTED]
- [[AppDbContext]] - `references` [EXTRACTED]
- [[IRefreshTokenRepository]] - `implements` [EXTRACTED]
- [[RefreshTokenRepository.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Refresh_Token_Repository