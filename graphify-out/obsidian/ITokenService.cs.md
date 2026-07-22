---
source_file: "src/CampCenter.Application/Interfaces/ITokenService.cs"
type: "code"
community: "Auth DTOs & Models"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_DTOs__Models
---

# ITokenService.cs

## Context

_Source: `src/CampCenter.Application/Interfaces/ITokenService.cs` (defined near L1; showing L1–L15 of 15)._

```csharp
using CampCenter.Application.Models;
using CampCenter.Domain.Entities;

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
- [[CampCenter.Application.Interfaces]] - `contains` [EXTRACTED]
- [[CampCenter.Application.Models]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[ITokenService]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_DTOs__Models