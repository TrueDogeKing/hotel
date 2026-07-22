---
source_file: "src/CampCenter.Domain/Entities/RefreshToken.cs"
type: "code"
community: "Refresh Token EF Config"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Refresh_Token_EF_Config
---

# RefreshToken

## Context

_Source: `src/CampCenter.Domain/Entities/RefreshToken.cs` (defined near L5; showing L3–L28 of 28)._

```csharp
/// Refresh token bound to an admin session. Stored in the database only as a hash;
/// the plaintext value is delivered once to the client (HttpOnly cookie).
public class RefreshToken
{
    public Guid Id { get; set; }

    public Guid AdminUserId { get; set; }

    public AdminUser? AdminUser { get; set; }

    /// SHA-256 hash of the plaintext token value. The plaintext is never stored.
    public required string TokenHash { get; set; }

    public DateTime ExpiresAtUtc { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? RevokedAtUtc { get; set; }

    /// Hash of the token that replaced this one during rotation (if applicable).
    /// Enables tracing the rotation chain and detecting reuse.
    public string? ReplacedByTokenHash { get; set; }

    /// Token is active if not revoked and not expired.
    public bool IsActive => RevokedAtUtc is null && DateTime.UtcNow < ExpiresAtUtc;
}
```

## Connections
- [[.AddAsync()_2]] - `references` [EXTRACTED]
- [[.AddAsync()_7]] - `references` [EXTRACTED]
- [[.Configure()_5]] - `references` [EXTRACTED]
- [[.CreateTokenEntity()]] - `references` [EXTRACTED]
- [[.GetByTokenHashAsync()_1]] - `references` [EXTRACTED]
- [[.GetByTokenHashAsync()_3]] - `references` [EXTRACTED]
- [[AdminUser]] - `references` [EXTRACTED]
- [[AppDbContext]] - `references` [EXTRACTED]
- [[DateTime_7]] - `references` [EXTRACTED]
- [[Guid_22]] - `references` [EXTRACTED]
- [[RefreshToken.cs]] - `contains` [EXTRACTED]
- [[RefreshTokenConfiguration]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Refresh_Token_EF_Config