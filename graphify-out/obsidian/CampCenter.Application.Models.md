---
source_file: "src/CampCenter.Application/Models/AccessToken.cs"
type: "code"
community: "Auth DTOs & Models"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_DTOs__Models
---

# CampCenter.Application.Models

## Context

_Source: `src/CampCenter.Application/Models/AccessToken.cs` (defined near L1; showing L1–L3 of 3)._

```csharp
namespace CampCenter.Application.Models;

public record AccessToken(string Token, DateTime ExpiresAtUtc);
```

## Connections
- [[AccessToken.cs]] - `contains` [EXTRACTED]
- [[AdminBookingService.cs]] - `imports` [EXTRACTED]
- [[AuthController.cs]] - `imports` [EXTRACTED]
- [[AuthResult.cs]] - `contains` [EXTRACTED]
- [[AuthService.cs]] - `imports` [EXTRACTED]
- [[BookingService.cs]] - `imports` [EXTRACTED]
- [[BookingSettings.cs]] - `contains` [EXTRACTED]
- [[IAuthService.cs]] - `imports` [EXTRACTED]
- [[ITokenService.cs]] - `imports` [EXTRACTED]
- [[JwtTokenService.cs]] - `imports` [EXTRACTED]
- [[PaymentService.cs]] - `imports` [EXTRACTED]
- [[RefreshTokenInfo.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_DTOs__Models