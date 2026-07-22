---
source_file: "src/CampCenter.Application/Common/LoginNormalizer.cs"
type: "code"
community: "Login Normalizer"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Login_Normalizer
---

# LoginNormalizer

## Context

_Source: `src/CampCenter.Application/Common/LoginNormalizer.cs` (defined near L5; showing L3–L8 of 8)._

```csharp
/// Canonical form for logins so lookups and the unique index are case-insensitive
/// in practice (applied at login).
public static class LoginNormalizer
{
    public static string Normalize(string login) => login.Trim().ToLowerInvariant();
}
```

## Connections
- [[.Normalize()]] - `method` [EXTRACTED]
- [[LoginNormalizer.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Login_Normalizer