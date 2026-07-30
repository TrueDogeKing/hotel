---
type: community
cohesion: 0.11
members: 33
---

# Integration Test Harness (1)

**Cohesion:** 0.11 - loosely connected
**Members:** 33 nodes

## Members
- [[.AdminEndpoints_WithoutToken_ReturnUnauthorized()]] - code - tests/CampCenter.IntegrationTests/RoomsAndClosuresApiTests.cs
- [[.AvailabilityUrl()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.BookingFlow_AvailabilityShrinks_AndCancelFreesRooms()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.BookingRequest()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.Booking_RedundantRoomSelection_IsRejected()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.Booking_WhenCenterClosed_IsRejected()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.Closure_BlocksRoom_InOccupancyGrid()]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[.Closures_CrudRoundtrip_Works()]] - code - tests/CampCenter.IntegrationTests/RoomsAndClosuresApiTests.cs
- [[.OccupancyUrl()]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[.Occupancy_Reassign_Tasks_And_Dashboard_Work()]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[.ParallelBookings_ForTheLastRooms_ExactlyOneWins()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.Rooms_CrudRoundtrip_Works()]] - code - tests/CampCenter.IntegrationTests/RoomsAndClosuresApiTests.cs
- [[.SetUpRoomsAsync()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.UnknownManageToken_Returns404()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[AdminPanelApiTests]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[AdminPanelApiTests.cs]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[Capacity_2]] - code
- [[Count_3]] - code
- [[CreateBookingRequestDto_5]] - code
- [[DateOnly_30]] - code
- [[DateOnly_10]] - code
- [[Dictionary_7]] - code
- [[Fact]] - code
- [[Fact_3]] - code
- [[Fact_9]] - code
- [[HttpClient_3]] - code
- [[IntegrationTestBase_1]] - code
- [[PublicBookingApiTests]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[RoomsAndClosuresApiTests]] - code - tests/CampCenter.IntegrationTests/RoomsAndClosuresApiTests.cs
- [[Task_44]] - code
- [[Task_49]] - code
- [[Task_73]] - code
- [[long_1]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Integration_Test_Harness_1
SORT file.name ASC
```

## Connections to other communities
- 3 edges to [[_COMMUNITY_tests  CampCenter.IntegrationTests (2)]]
- 1 edge to [[_COMMUNITY_Application Namespaces & DTOs]]
- 1 edge to [[_COMMUNITY_Payment Gateway Integration Tests (2)]]
- 1 edge to [[_COMMUNITY_tests  CampCenter.IntegrationTests (1)]]

## Top bridge nodes
- [[IntegrationTestBase_1]] - degree 5, connects to 2 communities
- [[AdminPanelApiTests.cs]] - degree 3, connects to 2 communities
- [[PublicBookingApiTests]] - degree 11, connects to 1 community
- [[RoomsAndClosuresApiTests]] - degree 5, connects to 1 community