---
type: community
members: 12
---

# Public Booking Service (2)

**Members:** 12 nodes

## Members
- [[AuthController.cs]] - code - src/CampCenter.Api/Controllers/AuthController.cs
- [[BcryptPasswordHasher.cs]] - code - src/CampCenter.Infrastructure/Auth/BcryptPasswordHasher.cs
- [[CampCenter.Api.Controllers]] - code - src/CampCenter.Api/Controllers/AuthController.cs
- [[CampCenter.Api.Controllers.Public]] - code - src/CampCenter.Api/Controllers/Public/PublicAvailabilityController.cs
- [[CampCenter.Api.RateLimiting]] - code - src/CampCenter.Api/RateLimiting/RateLimitPolicies.cs
- [[CampCenter.Infrastructure.Auth]] - code - src/CampCenter.Infrastructure/Auth/BcryptPasswordHasher.cs
- [[JwtTokenService.cs]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[PublicAvailabilityController.cs]] - code - src/CampCenter.Api/Controllers/Public/PublicAvailabilityController.cs
- [[PublicBookingsController.cs]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[RateLimitPolicies]] - code - src/CampCenter.Api/RateLimiting/RateLimitPolicies.cs
- [[RateLimitPolicies.cs]] - code - src/CampCenter.Api/RateLimiting/RateLimitPolicies.cs
- [[string_2]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Public_Booking_Service_2
SORT file.name ASC
```

## Connections to other communities
- 6 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 4 edges to [[_COMMUNITY_ClosureService]]
- 3 edges to [[_COMMUNITY_Exception]]
- 3 edges to [[_COMMUNITY_CampCenter.Application  Services (1)]]
- 2 edges to [[_COMMUNITY_CampCenter.Application.DTOs.Public]]
- 1 edge to [[_COMMUNITY_.GetBlockedRoomIdsAsync]]
- 1 edge to [[_COMMUNITY_Auth Controller (1)]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Services (3)]]
- 1 edge to [[_COMMUNITY_BookingSettings]]
- 1 edge to [[_COMMUNITY_Admin Booking & Notifications (2)]]

## Top bridge nodes
- [[AuthController.cs]] - degree 7, connects to 4 communities
- [[PublicAvailabilityController.cs]] - degree 5, connects to 4 communities
- [[PublicBookingsController.cs]] - degree 6, connects to 3 communities
- [[JwtTokenService.cs]] - degree 5, connects to 3 communities
- [[CampCenter.Infrastructure.Auth]] - degree 7, connects to 2 communities