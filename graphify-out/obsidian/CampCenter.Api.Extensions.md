---
source_file: "src/CampCenter.Api/Extensions/ClaimsPrincipalExtensions.cs"
type: "code"
community: "Claims Principal Extensions"
location: "L4"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Claims_Principal_Extensions
---

# CampCenter.Api.Extensions

## Context

_Source: `src/CampCenter.Api/Extensions/ClaimsPrincipalExtensions.cs` (defined near L4; showing L2–L18 of 18)._

```csharp
using Microsoft.IdentityModel.JsonWebTokens;

namespace CampCenter.Api.Extensions;

/// Helpers for reading the authenticated admin's identity from JWT claims.
public static class ClaimsPrincipalExtensions
{
    /// The admin user id from the "sub"/NameIdentifier claim. Throws for unauthenticated principals.
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var value =
            principal.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? principal.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? throw new InvalidOperationException("The current principal has no user id claim.");
        return Guid.Parse(value);
    }
}
```

## Connections
- [[ClaimsPrincipalExtensions.cs]] - `contains` [EXTRACTED]
- [[TasksController.cs]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Claims_Principal_Extensions