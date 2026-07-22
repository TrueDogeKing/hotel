---
type: community
cohesion: 0.06
members: 57
---

# Integration Test Harness

**Cohesion:** 0.06 - loosely connected
**Members:** 57 nodes

## Members
- [[.AdminEndpoints_WithoutToken_ReturnUnauthorized()]] - code - tests/CampCenter.IntegrationTests/RoomsAndSessionsApiTests.cs
- [[.BookingFlow_AvailabilityShrinks_AndCancelFreesRooms()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.BookingRequest()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.Booking_RedundantRoomSelection_IsRejected()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.ConfigureWebHost()]] - code - tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs
- [[.CreateAuthenticatedClientAsync()]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[.CreateClient()]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[.DisposeAsync()]] - code - tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs
- [[.InitializeAsync()]] - code - tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs
- [[.Login_ExceedingRateLimit_ReturnsTooManyRequests()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithInvalidPayload_ReturnsBadRequest()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithUnknownLogin_ReturnsUnauthorized()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithValidAdminCredentials_ReturnsTokenAndSetsRefreshCookie()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithWrongPassword_ReturnsUnauthorized()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Occupancy_Reassign_Tasks_And_Dashboard_Work()]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[.ParallelBookings_ForTheLastRooms_ExactlyOneWins()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.Rooms_CrudRoundtrip_Works()]] - code - tests/CampCenter.IntegrationTests/RoomsAndSessionsApiTests.cs
- [[.SeedAdminUserAsync()]] - code - src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs
- [[.Sessions_LifecycleAndOverlapGuard_Work()]] - code - tests/CampCenter.IntegrationTests/RoomsAndSessionsApiTests.cs
- [[.SetUpSessionAsync()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.UnknownManageToken_Returns404()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[AdminPanelApiTests]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[ApiCollection]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[ApiCollection.cs]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[AuthApiTests]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[AuthApiTests.cs]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[CampCenterApiFactory]] - code - tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs
- [[CancellationToken_38]] - code
- [[Capacity_1]] - code
- [[Count]] - code
- [[DataSeeder]] - code - src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs
- [[DateOnly_10]] - code
- [[Dictionary_7]] - code
- [[Fact]] - code
- [[Fact_1]] - code
- [[Fact_3]] - code
- [[Fact_4]] - code
- [[Guid_37]] - code
- [[HttpClient_1]] - code
- [[HttpClient_3]] - code
- [[IAsyncLifetime]] - code
- [[ICollectionFixture]] - code
- [[IServiceProvider]] - code
- [[IWebHostBuilder]] - code
- [[IntegrationTestBase]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[PostgreSqlContainer]] - code
- [[PublicBookingApiTests]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[RoomsAndSessionsApiTests]] - code - tests/CampCenter.IntegrationTests/RoomsAndSessionsApiTests.cs
- [[Task_37]] - code
- [[Task_44]] - code
- [[Task_45]] - code
- [[Task_46]] - code
- [[Task_47]] - code
- [[Task_49]] - code
- [[Task_50]] - code
- [[WebApplicationFactory]] - code
- [[string_8]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Integration_Test_Harness
SORT file.name ASC
```

## Connections to other communities
- 5 edges to [[_COMMUNITY_Application DTO Namespaces]]
- 3 edges to [[_COMMUNITY_Rate Limiting & Startup]]
- 2 edges to [[_COMMUNITY_Auth DTOs & Models]]
- 1 edge to [[_COMMUNITY_Public Booking Service]]
- 1 edge to [[_COMMUNITY_Payment Gateway Integration Tests]]

## Top bridge nodes
- [[ApiCollection.cs]] - degree 4, connects to 2 communities
- [[AuthApiTests.cs]] - degree 3, connects to 2 communities
- [[IntegrationTestBase]] - degree 10, connects to 1 community
- [[CampCenterApiFactory]] - degree 10, connects to 1 community
- [[PublicBookingApiTests]] - degree 8, connects to 1 community