---
source_file: "src/CampCenter.Infrastructure/Auth/RefreshTokenSettings.cs"
type: "code"
community: "Auth Controller"
location: "L3"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_Controller
---

# RefreshTokenSettings

## Context

_Source: `src/CampCenter.Infrastructure/Auth/RefreshTokenSettings.cs` (defined near L3; showing L1–L18 of 18)._

```csharp
namespace CampCenter.Infrastructure.Auth;

public class RefreshTokenSettings
{
    public const string SectionName = "RefreshToken";

    public int ExpiryDays { get; set; } = 7;

    public string CookieName { get; set; } = "refreshToken";

    /// Whether the cookie has the Secure flag (requires HTTPS). Always true in production.
    public bool CookieSecure { get; set; } = true;

    /// Cookie SameSite mode ("Strict", "Lax", or "None").
    public string CookieSameSite { get; set; } = "Strict";

    public string CookiePath { get; set; } = "/api/auth";
}
```

## Connections
- [[AuthController]] - `references` [EXTRACTED]
- [[JwtTokenService]] - `references` [EXTRACTED]
- [[RefreshTokenSettings.cs]] - `contains` [EXTRACTED]
- [[string_4]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_Controller