---
type: community
members: 15
---

# Public Booking Service (2)

**Members:** 15 nodes

## Members
- [[CampCenter.Api.Controllers.Public]] - code - src/CampCenter.Api/Controllers/Public/PublicAvailabilityController.cs
- [[CampCenter.Api.Errors]] - code - src/CampCenter.Api/Errors/GlobalExceptionHandler.cs
- [[CampCenter.Api.RateLimiting]] - code - src/CampCenter.Api/RateLimiting/RateLimitPolicies.cs
- [[CampCenter.Infrastructure.Persistence.Seed]] - code - src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs
- [[CampCenterApiFactory.cs]] - code - tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs
- [[DataSeeder.cs]] - code - src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs
- [[GlobalExceptionHandler.cs]] - code - src/CampCenter.Api/Errors/GlobalExceptionHandler.cs
- [[Program]] - code - src/CampCenter.Api/Program.cs
- [[Program.cs]] - code - src/CampCenter.Api/Program.cs
- [[PublicAvailabilityController.cs]] - code - src/CampCenter.Api/Controllers/Public/PublicAvailabilityController.cs
- [[PublicBookingsController.cs]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[PublicPaymentsController.cs]] - code - src/CampCenter.Api/Controllers/Public/PublicPaymentsController.cs
- [[RateLimitPolicies]] - code - src/CampCenter.Api/RateLimiting/RateLimitPolicies.cs
- [[RateLimitPolicies.cs]] - code - src/CampCenter.Api/RateLimiting/RateLimitPolicies.cs
- [[string_1]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Public_Booking_Service_2
SORT file.name ASC
```

## Connections to other communities
- 5 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 4 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 3 edges to [[_COMMUNITY_Integration Test Harness (1)]]
- 3 edges to [[_COMMUNITY_tests  CampCenter.IntegrationTests (1)]]
- 2 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (5)]]
- 1 edge to [[_COMMUNITY_Room Closure Management]]
- 1 edge to [[_COMMUNITY_Public Booking Service (1)]]
- 1 edge to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 1 edge to [[_COMMUNITY_Domain Exceptions]]
- 1 edge to [[_COMMUNITY_Global Exception Handler]]
- 1 edge to [[_COMMUNITY_OpenAPI Security Scheme]]
- 1 edge to [[_COMMUNITY_CampCenter.Application  Services (1)]]
- 1 edge to [[_COMMUNITY_Przelewy24 Payment Client]]

## Top bridge nodes
- [[Program.cs]] - degree 9, connects to 5 communities
- [[PublicAvailabilityController.cs]] - degree 5, connects to 4 communities
- [[PublicBookingsController.cs]] - degree 6, connects to 3 communities
- [[DataSeeder.cs]] - degree 4, connects to 3 communities
- [[CampCenterApiFactory.cs]] - degree 4, connects to 3 communities