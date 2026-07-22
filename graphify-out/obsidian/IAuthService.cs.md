---
source_file: "src/CampCenter.Application/Interfaces/IAuthService.cs"
type: "code"
community: "Auth DTOs & Models"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_DTOs__Models
---

# IAuthService.cs

## Context

_Source: `src/CampCenter.Application/Interfaces/IAuthService.cs` (defined near L1; showing L1–L24 of 24)._

```csharp
using CampCenter.Application.DTOs.Auth;
using CampCenter.Application.Models;

namespace CampCenter.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResult?> LoginAsync(
        LoginRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Exchanges a refresh token for a new token pair (rotation). Returns null if the token
    /// is unknown, expired, or revoked. Reuse of a rotated token is treated as theft and
    /// revokes all sessions of that admin.
    /// <param name="rawRefreshToken">Plaintext refresh token value.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<AuthResult?> RefreshAsync(
        string? rawRefreshToken,
        CancellationToken cancellationToken = default
    );

    Task LogoutAsync(string? rawRefreshToken, CancellationToken cancellationToken = default);
}
```

## Connections
- [[CampCenter.Application.DTOs.Auth]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `contains` [EXTRACTED]
- [[CampCenter.Application.Models]] - `imports` [EXTRACTED]
- [[IAuthService]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_DTOs__Models