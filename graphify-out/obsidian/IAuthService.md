---
source_file: "src/CampCenter.Application/Interfaces/IAuthService.cs"
type: "code"
community: "Auth Controller"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_Controller
---

# IAuthService

## Context

_Source: `src/CampCenter.Application/Interfaces/IAuthService.cs` (defined near L6; showing L4–L24 of 24)._

```csharp
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
- [[.LoginAsync()]] - `method` [EXTRACTED]
- [[.LogoutAsync()]] - `method` [EXTRACTED]
- [[.RefreshAsync()]] - `method` [EXTRACTED]
- [[AuthController]] - `references` [EXTRACTED]
- [[AuthService]] - `implements` [EXTRACTED]
- [[IAuthService.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_Controller