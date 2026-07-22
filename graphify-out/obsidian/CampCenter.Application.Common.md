---
source_file: "src/CampCenter.Application/Common/LoginNormalizer.cs"
type: "code"
community: "Login Normalizer"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Login_Normalizer
---

# CampCenter.Application.Common

## Context

_Source: `src/CampCenter.Application/Common/LoginNormalizer.cs` (defined near L1; showing L1–L8 of 8)._

```csharp
namespace CampCenter.Application.Common;

/// Canonical form for logins so lookups and the unique index are case-insensitive
/// in practice (applied at login).
public static class LoginNormalizer
{
    public static string Normalize(string login) => login.Trim().ToLowerInvariant();
}
```

## Connections
- [[AuthService.cs]] - `imports` [EXTRACTED]
- [[LoginNormalizer.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Login_Normalizer