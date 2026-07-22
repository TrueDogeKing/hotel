---
source_file: "src/CampCenter.Infrastructure/Auth/BcryptPasswordHasher.cs"
type: "code"
community: "Password Hashing (bcrypt)"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Password_Hashing_bcrypt
---

# BcryptPasswordHasher

## Context

_Source: `src/CampCenter.Infrastructure/Auth/BcryptPasswordHasher.cs` (defined near L5; showing L3–L11 of 11)._

```csharp
namespace CampCenter.Infrastructure.Auth;

public class BcryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, string passwordHash) =>
        BCrypt.Net.BCrypt.Verify(password, passwordHash);
}
```

## Connections
- [[.Hash()_1]] - `method` [EXTRACTED]
- [[.Verify()_1]] - `method` [EXTRACTED]
- [[BcryptPasswordHasher.cs]] - `contains` [EXTRACTED]
- [[IPasswordHasher]] - `implements` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Password_Hashing_bcrypt