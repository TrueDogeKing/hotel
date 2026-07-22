---
source_file: "src/CampCenter.Application/Interfaces/ITokenService.cs"
type: "code"
community: "Auth Service & Tokens"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_Service__Tokens
---

# ITokenService

## Context

_Source: `src/CampCenter.Application/Interfaces/ITokenService.cs` (defined near L6; showing L4–L15 of 15)._

```csharp
namespace CampCenter.Application.Interfaces;

public interface ITokenService
{
    AccessToken CreateAccessToken(AdminUser user);

    RefreshTokenInfo GenerateRefreshToken();

    /// Returns the hash (SHA-256) of the raw refresh token value.
    /// <param name="rawToken">The raw refresh token value.</param>
    string HashRefreshToken(string rawToken);
}
```

## Connections
- [[.CreateAccessToken()]] - `method` [EXTRACTED]
- [[.GenerateRefreshToken()]] - `method` [EXTRACTED]
- [[.HashRefreshToken()]] - `method` [EXTRACTED]
- [[AuthService]] - `references` [EXTRACTED]
- [[BookingService]] - `references` [EXTRACTED]
- [[ITokenService.cs]] - `contains` [EXTRACTED]
- [[JwtTokenService]] - `implements` [EXTRACTED]
- [[PaymentService]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_Service__Tokens