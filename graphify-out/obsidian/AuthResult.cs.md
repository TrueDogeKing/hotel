---
source_file: "src/CampCenter.Application/Models/AuthResult.cs"
type: "code"
community: "Auth Service & Tokens"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_Service__Tokens
---

# AuthResult.cs

## Context

_Source: `src/CampCenter.Application/Models/AuthResult.cs` (defined near L1; showing L1–L14 of 14)._

```csharp
namespace CampCenter.Application.Models;

/// <param name="AccessToken">Access token (JWT).</param>
/// <param name="AccessTokenExpiresAtUtc">Access token expiration time (UTC).</param>
/// <param name="Login">Login of the authenticated user.</param>
/// <param name="RefreshToken">Plaintext refresh token value.</param>
/// <param name="RefreshTokenExpiresAtUtc">Refresh token expiration time (UTC).</param>
public record AuthResult(
    string AccessToken,
    DateTime AccessTokenExpiresAtUtc,
    string Login,
    string RefreshToken,
    DateTime RefreshTokenExpiresAtUtc
);
```

## Connections
- [[AuthResult]] - `contains` [EXTRACTED]
- [[CampCenter.Application.Models]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_Service__Tokens