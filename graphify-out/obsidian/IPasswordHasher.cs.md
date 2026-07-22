---
source_file: "src/CampCenter.Application/Interfaces/IPasswordHasher.cs"
type: "code"
community: "Password Hashing (bcrypt)"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Password_Hashing_bcrypt
---

# IPasswordHasher.cs

## Context

_Source: `src/CampCenter.Application/Interfaces/IPasswordHasher.cs` (defined near L1; showing L1–L8 of 8)._

```csharp
namespace CampCenter.Application.Interfaces;

public interface IPasswordHasher
{
    string Hash(string password);

    bool Verify(string password, string passwordHash);
}
```

## Connections
- [[CampCenter.Application.Interfaces]] - `contains` [EXTRACTED]
- [[IPasswordHasher]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Password_Hashing_bcrypt