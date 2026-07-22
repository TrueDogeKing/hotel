---
source_file: "src/CampCenter.Infrastructure/Auth/JwtSettings.cs"
type: "code"
community: "JWT Token Service"
location: "L3"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/JWT_Token_Service
---

# JwtSettings

## Context

_Source: `src/CampCenter.Infrastructure/Auth/JwtSettings.cs` (defined near L3; showing L1–L15 of 15)._

```csharp
namespace CampCenter.Infrastructure.Auth;

public class JwtSettings
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    /// <Signature Key (symetric, min. 32 characters for HMAC-SHA256).
    public string Key { get; set; } = string.Empty;

    public int ExpiryMinutes { get; set; } = 60;
}
```

## Connections
- [[JwtSettings.cs]] - `contains` [EXTRACTED]
- [[JwtTokenService]] - `references` [EXTRACTED]
- [[string_3]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/JWT_Token_Service