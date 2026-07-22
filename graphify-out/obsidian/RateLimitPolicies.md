---
source_file: "src/CampCenter.Api/RateLimiting/RateLimitPolicies.cs"
type: "code"
community: "Rate Limiting & Startup"
location: "L4"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Rate_Limiting__Startup
---

# RateLimitPolicies

## Context

_Source: `src/CampCenter.Api/RateLimiting/RateLimitPolicies.cs` (defined near L4; showing L2–L11 of 11)._

```csharp

/// Names of the rate limiting policies configured in Program.cs.
public static class RateLimitPolicies
{
    /// Brute-force protection for the authentication endpoints (login).
    public const string Auth = "auth";

    /// Stricter per-IP limit on public booking create/lookup endpoints.
    public const string PublicBooking = "public-booking";
}
```

## Connections
- [[RateLimitPolicies.cs]] - `contains` [EXTRACTED]
- [[string_1]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Rate_Limiting__Startup