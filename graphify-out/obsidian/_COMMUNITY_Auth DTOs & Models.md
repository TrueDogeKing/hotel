---
type: community
cohesion: 0.21
members: 12
---

# Auth DTOs & Models

**Cohesion:** 0.21 - loosely connected
**Members:** 12 nodes

## Members
- [[AuthService.cs]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[BookingService.cs]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[CampCenter.Application.DTOs.Auth]] - code - src/CampCenter.Application/DTOs/Auth/LoginRequestDto.cs
- [[CampCenter.Application.Models]] - code - src/CampCenter.Application/Models/AccessToken.cs
- [[CampCenter.Application.Services]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[CampSessionService.cs]] - code - src/CampCenter.Application/Services/CampSessionService.cs
- [[EmailTemplates.cs]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[IAuthService.cs]] - code - src/CampCenter.Application/Interfaces/IAuthService.cs
- [[ITokenService.cs]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[LoginResponseDto]] - code - src/CampCenter.Application/DTOs/Auth/LoginResponseDto.cs
- [[LoginResponseDto.cs]] - code - src/CampCenter.Application/DTOs/Auth/LoginResponseDto.cs
- [[PaymentService.cs]] - code - src/CampCenter.Application/Services/PaymentService.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Auth_DTOs__Models
SORT file.name ASC
```

## Connections to other communities
- 11 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 10 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 4 edges to [[_COMMUNITY_Application DTO Namespaces]]
- 3 edges to [[_COMMUNITY_Rate Limiting & Startup]]
- 3 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 3 edges to [[_COMMUNITY_Admin Booking & Notifications]]
- 3 edges to [[_COMMUNITY_Domain Exceptions]]
- 2 edges to [[_COMMUNITY_Auth Controller]]
- 2 edges to [[_COMMUNITY_Validator Unit Tests]]
- 2 edges to [[_COMMUNITY_Integration Test Harness]]
- 1 edge to [[_COMMUNITY_Login Normalizer]]
- 1 edge to [[_COMMUNITY_Application DI Registration]]
- 1 edge to [[_COMMUNITY_Admin User & Token Config]]
- 1 edge to [[_COMMUNITY_JWT Token Service]]
- 1 edge to [[_COMMUNITY_Room Mix Calculator Tests]]
- 1 edge to [[_COMMUNITY_Przelewy24 Payment Client]]
- 1 edge to [[_COMMUNITY_Public Booking Service]]
- 1 edge to [[_COMMUNITY_Camp Session Management]]

## Top bridge nodes
- [[CampCenter.Application.Services]] - degree 13, connects to 6 communities
- [[CampCenter.Application.Models]] - degree 12, connects to 6 communities
- [[BookingService.cs]] - degree 8, connects to 5 communities
- [[CampSessionService.cs]] - degree 7, connects to 5 communities
- [[CampCenter.Application.DTOs.Auth]] - degree 9, connects to 4 communities