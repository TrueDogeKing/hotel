---
source_file: "src/CampCenter.Infrastructure/Auth/BcryptPasswordHasher.cs"
type: "code"
community: "Rate Limiting & Startup"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Rate_Limiting__Startup
---

# BcryptPasswordHasher.cs

## Context

_Source: `src/CampCenter.Infrastructure/Auth/BcryptPasswordHasher.cs` (defined near L1; showing L1–L11 of 11)._

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
- [[BcryptPasswordHasher]] - `contains` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Auth]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Rate_Limiting__Startup