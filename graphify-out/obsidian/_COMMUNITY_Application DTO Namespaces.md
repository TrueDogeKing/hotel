---
type: community
cohesion: 0.38
members: 10
---

# Application DTO Namespaces

**Cohesion:** 0.38 - loosely connected
**Members:** 10 nodes

## Members
- [[AdminPanelApiTests.cs]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[AvailabilityService.cs]] - code - src/CampCenter.Application/Services/AvailabilityService.cs
- [[CampCenter.Application.DTOs.Public]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[CampCenter.Application.DTOs.Rooms]] - code - src/CampCenter.Application/DTOs/Rooms/RoomDtos.cs
- [[CampCenter.Application.DTOs.Sessions]] - code - src/CampCenter.Application/DTOs/Sessions/CampSessionDtos.cs
- [[CampCenter.IntegrationTests]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[PaymentsApiTests.cs]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[PublicBookingApiTests.cs]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[RoomService.cs]] - code - src/CampCenter.Application/Services/RoomService.cs
- [[RoomsAndSessionsApiTests.cs]] - code - tests/CampCenter.IntegrationTests/RoomsAndSessionsApiTests.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Application_DTO_Namespaces
SORT file.name ASC
```

## Connections to other communities
- 8 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 5 edges to [[_COMMUNITY_Integration Test Harness]]
- 4 edges to [[_COMMUNITY_Auth DTOs & Models]]
- 4 edges to [[_COMMUNITY_Room Management]]
- 3 edges to [[_COMMUNITY_Public Booking Service]]
- 3 edges to [[_COMMUNITY_Validator Unit Tests]]
- 3 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 2 edges to [[_COMMUNITY_Room Mix Calculator Tests]]
- 2 edges to [[_COMMUNITY_Camp Session Management]]
- 2 edges to [[_COMMUNITY_Payment Gateway Integration Tests]]
- 1 edge to [[_COMMUNITY_Domain Exceptions]]
- 1 edge to [[_COMMUNITY_Rate Limiting & Startup]]

## Top bridge nodes
- [[CampCenter.Application.DTOs.Public]] - degree 12, connects to 5 communities
- [[RoomService.cs]] - degree 7, connects to 5 communities
- [[CampCenter.Application.DTOs.Sessions]] - degree 10, connects to 4 communities
- [[AvailabilityService.cs]] - degree 5, connects to 4 communities
- [[CampCenter.Application.DTOs.Rooms]] - degree 9, connects to 2 communities