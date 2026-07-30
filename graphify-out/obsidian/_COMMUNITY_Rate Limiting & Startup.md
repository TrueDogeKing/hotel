---
type: community
cohesion: 0.09
members: 26
---

# Rate Limiting & Startup

**Cohesion:** 0.09 - loosely connected
**Members:** 26 nodes

## Members
- [[AdminBookingService.cs]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[AuthApiTests.cs]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[AuthController.cs]] - code - src/CampCenter.Api/Controllers/AuthController.cs
- [[AuthService.cs]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[BcryptPasswordHasher.cs]] - code - src/CampCenter.Infrastructure/Auth/BcryptPasswordHasher.cs
- [[BookingSettings]] - code - src/CampCenter.Application/Models/BookingSettings.cs
- [[BookingSettings.cs]] - code - src/CampCenter.Application/Models/BookingSettings.cs
- [[CampCenter.Api.Controllers]] - code - src/CampCenter.Api/Controllers/AuthController.cs
- [[CampCenter.Api.RateLimiting]] - code - src/CampCenter.Api/RateLimiting/RateLimitPolicies.cs
- [[CampCenter.Application.DTOs.Auth]] - code - src/CampCenter.Application/DTOs/Auth/LoginRequestDto.cs
- [[CampCenter.Application.Models]] - code - src/CampCenter.Application/Models/AccessToken.cs
- [[CampCenter.Infrastructure.Auth]] - code - src/CampCenter.Infrastructure/Auth/BcryptPasswordHasher.cs
- [[IAuthService.cs]] - code - src/CampCenter.Application/Interfaces/IAuthService.cs
- [[ITokenService.cs]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[JwtTokenService.cs]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[LoginResponseDto]] - code - src/CampCenter.Application/DTOs/Auth/LoginResponseDto.cs
- [[LoginResponseDto.cs]] - code - src/CampCenter.Application/DTOs/Auth/LoginResponseDto.cs
- [[RateLimitPolicies]] - code - src/CampCenter.Api/RateLimiting/RateLimitPolicies.cs
- [[RateLimitPolicies.cs]] - code - src/CampCenter.Api/RateLimiting/RateLimitPolicies.cs
- [[ScheduleService.cs]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[ScheduleSettings]] - code - src/CampCenter.Application/Models/ScheduleSettings.cs
- [[ScheduleSettings.cs]] - code - src/CampCenter.Application/Models/ScheduleSettings.cs
- [[TimeOnly]] - code
- [[string_1]] - code
- [[string_2]] - code
- [[string_9]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Rate_Limiting__Startup
SORT file.name ASC
```

## Connections to other communities
- 11 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 10 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 6 edges to [[_COMMUNITY_JWT Token Service]]
- 4 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (5)]]
- 2 edges to [[_COMMUNITY_Auth Controller (2)]]
- 2 edges to [[_COMMUNITY_Validator Unit Tests]]
- 2 edges to [[_COMMUNITY_Integration Test Harness (2)]]
- 2 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 2 edges to [[_COMMUNITY_CampCenter.Application  Services (2)]]
- 1 edge to [[_COMMUNITY_Auth Controller (1)]]
- 1 edge to [[_COMMUNITY_Login Normalizer]]
- 1 edge to [[_COMMUNITY_Password Hashing (bcrypt)]]
- 1 edge to [[_COMMUNITY_tests  CampCenter.IntegrationTests (2)]]
- 1 edge to [[_COMMUNITY_Admin Booking & Notifications (1)]]

## Top bridge nodes
- [[AuthService.cs]] - degree 8, connects to 5 communities
- [[CampCenter.Application.Models]] - degree 15, connects to 4 communities
- [[AdminBookingService.cs]] - degree 8, connects to 4 communities
- [[ScheduleService.cs]] - degree 7, connects to 4 communities
- [[CampCenter.Application.DTOs.Auth]] - degree 9, connects to 3 communities