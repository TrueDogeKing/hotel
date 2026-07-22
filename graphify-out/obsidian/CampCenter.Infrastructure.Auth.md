---
source_file: "src/CampCenter.Infrastructure/Auth/BcryptPasswordHasher.cs"
type: "code"
community: "Rate Limiting & Startup"
location: "L3"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Rate_Limiting__Startup
---

# CampCenter.Infrastructure.Auth

## Context

_Source: `src/CampCenter.Infrastructure/Auth/BcryptPasswordHasher.cs` (defined near L3; showing L1–L11 of 11)._

```csharp
using CampCenter.Application.Interfaces;

namespace CampCenter.Infrastructure.Auth;

public class BcryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, string passwordHash) =>
        BCrypt.Net.BCrypt.Verify(password, passwordHash);
}
```

## Connections
- [[AuthController.cs]] - `imports` [EXTRACTED]
- [[BcryptPasswordHasher.cs]] - `contains` [EXTRACTED]
- [[DependencyInjection.cs_1]] - `imports` [EXTRACTED]
- [[JwtSettings.cs]] - `contains` [EXTRACTED]
- [[JwtTokenService.cs]] - `contains` [EXTRACTED]
- [[Program.cs]] - `imports` [EXTRACTED]
- [[RefreshTokenSettings.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Rate_Limiting__Startup