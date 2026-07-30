---
type: community
cohesion: 0.12
members: 18
---

# tests / CampCenter.IntegrationTests (2)

**Cohesion:** 0.12 - loosely connected
**Members:** 18 nodes

## Members
- [[.AddApplication()]] - code - src/CampCenter.Application/DependencyInjection.cs
- [[CampCenter.Application]] - code - src/CampCenter.Application/DependencyInjection.cs
- [[CampCenter.Application.DTOs.Closures]] - code - src/CampCenter.Application/DTOs/Closures/ClosureDtos.cs
- [[CampCenter.Infrastructure]] - code - src/CampCenter.Infrastructure/DependencyInjection.cs
- [[CampCenter.Infrastructure.Persistence.Seed]] - code - src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs
- [[CampCenter.IntegrationTests]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[CampCenterApiFactory.cs]] - code - tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs
- [[DataSeeder.cs]] - code - src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs
- [[DependencyInjection]] - code - src/CampCenter.Application/DependencyInjection.cs
- [[DependencyInjection.cs]] - code - src/CampCenter.Application/DependencyInjection.cs
- [[IClosureService.cs]] - code - src/CampCenter.Application/Interfaces/IClosureService.cs
- [[IServiceCollection]] - code
- [[PaymentsApiTests.cs]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[Program]] - code - src/CampCenter.Api/Program.cs
- [[Program.cs]] - code - src/CampCenter.Api/Program.cs
- [[PublicBookingApiTests.cs]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[RoomsAndClosuresApiTests.cs]] - code - tests/CampCenter.IntegrationTests/RoomsAndClosuresApiTests.cs
- [[ScheduleApiTests.cs]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/tests_/_CampCenterIntegrationTests_2
SORT file.name ASC
```

## Connections to other communities
- 7 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 5 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 4 edges to [[_COMMUNITY_Integration Test Harness (2)]]
- 3 edges to [[_COMMUNITY_CampCenter.Application  Services (1)]]
- 3 edges to [[_COMMUNITY_Integration Test Harness (1)]]
- 2 edges to [[_COMMUNITY_Payment Gateway Integration Tests (2)]]
- 1 edge to [[_COMMUNITY_Rate Limiting & Startup]]
- 1 edge to [[_COMMUNITY_Validator Unit Tests]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Services (5)]]
- 1 edge to [[_COMMUNITY_tests  CampCenter.IntegrationTests (1)]]

## Top bridge nodes
- [[CampCenter.Application.DTOs.Closures]] - degree 8, connects to 4 communities
- [[CampCenter.IntegrationTests]] - degree 8, connects to 3 communities
- [[DataSeeder.cs]] - degree 4, connects to 3 communities
- [[PaymentsApiTests.cs]] - degree 5, connects to 2 communities
- [[DependencyInjection.cs]] - degree 4, connects to 2 communities