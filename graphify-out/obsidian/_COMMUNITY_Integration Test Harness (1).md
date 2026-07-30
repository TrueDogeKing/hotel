---
type: community
members: 13
---

# Integration Test Harness (1)

**Members:** 13 nodes

## Members
- [[AdminPanelApiTests.cs]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[AvailabilityService.cs]] - code - src/CampCenter.Application/Services/AvailabilityService.cs
- [[BookingService.cs]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[CampCenter.Application.DTOs.Closures]] - code - src/CampCenter.Application/DTOs/Closures/ClosureDtos.cs
- [[CampCenter.Application.DTOs.Public]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[CampCenter.Application.DTOs.Rooms]] - code - src/CampCenter.Application/DTOs/Rooms/RoomDtos.cs
- [[CampCenter.IntegrationTests]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[IAvailabilityService.cs]] - code - src/CampCenter.Application/Interfaces/IAvailabilityService.cs
- [[IBookingService.cs]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[PaymentsApiTests.cs]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[PublicBookingApiTests.cs]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[RoomsAndClosuresApiTests.cs]] - code - tests/CampCenter.IntegrationTests/RoomsAndClosuresApiTests.cs
- [[ScheduleApiTests.cs]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Integration_Test_Harness_1
SORT file.name ASC
```

## Connections to other communities
- 12 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 6 edges to [[_COMMUNITY_tests  CampCenter.IntegrationTests (1)]]
- 4 edges to [[_COMMUNITY_Public Booking Service (1)]]
- 4 edges to [[_COMMUNITY_Room Management]]
- 3 edges to [[_COMMUNITY_Public Booking Service (2)]]
- 3 edges to [[_COMMUNITY_Room Closure Management]]
- 3 edges to [[_COMMUNITY_Domain Exceptions]]
- 3 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 2 edges to [[_COMMUNITY_Rate Limiting & Startup]]
- 2 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (5)]]
- 2 edges to [[_COMMUNITY_CampCenter.Application  Services (1)]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Services (3)]]
- 1 edge to [[_COMMUNITY_Payment Gateway Integration Tests (1)]]
- 1 edge to [[_COMMUNITY_CampCenter.Domain  Repositories (1)]]

## Top bridge nodes
- [[BookingService.cs]] - degree 9, connects to 6 communities
- [[AvailabilityService.cs]] - degree 6, connects to 5 communities
- [[CampCenter.Application.DTOs.Public]] - degree 13, connects to 4 communities
- [[CampCenter.Application.DTOs.Closures]] - degree 8, connects to 4 communities
- [[CampCenter.Application.DTOs.Rooms]] - degree 10, connects to 3 communities