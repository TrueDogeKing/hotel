---
type: community
members: 99
---

# tests / CampCenter.IntegrationTests (1)

**Members:** 99 nodes

## Members
- [[.AdminEndpoints_WithoutToken_ReturnUnauthorized()]] - code - tests/CampCenter.IntegrationTests/RoomsAndClosuresApiTests.cs
- [[.AssignableRooms_OffersOwnAndFreeRooms_ButNotTakenOrClosedOnes()]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[.AvailabilityUrl()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.BookingFlow_AvailabilityShrinks_AndCancelFreesRooms()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.BookingMealTimes_DefaultToTheCenterTimes()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.BookingRequest()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.Booking_RedundantRoomSelection_IsRejected()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.Booking_WhenCenterClosed_IsRejected()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.BreakfastsAsync()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.BulkRetime_PreservesADayChangedIndividually()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.Calendar_BarSpansFullStayInclusive()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.Closure_BlocksRoom_InOccupancyGrid()]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[.Closures_CrudRoundtrip_Works()]] - code - tests/CampCenter.IntegrationTests/RoomsAndClosuresApiTests.cs
- [[.ConfigureWebHost()]] - code - tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs
- [[.CreateAuthenticatedClientAsync()]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[.CreateBookingAsync()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.CreateClient()]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[.DayView_IncludesDepartingGroup()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.DietaryNotes_AreStoredAndReturnedOnTheGroupSchedule()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.DisposeAsync()]] - code - tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs
- [[.EntryOn()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.GenerateMealsAsync()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.GenerateMeals_CoversTheStay_AndIsIdempotent()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.GenerateMeals_DoesNotResurrectADeletedMeal()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.GenerationUsesTheGroupsOwnTimes_ForMealsAddedLater()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.GetMealTimesAsync()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.InitializeAsync()]] - code - tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs
- [[.Login_ExceedingRateLimit_ReturnsTooManyRequests()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithInvalidPayload_ReturnsBadRequest()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithUnknownLogin_ReturnsUnauthorized()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithValidAdminCredentials_ReturnsTokenAndSetsRefreshCookie()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithWrongPassword_ReturnsUnauthorized()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.MealTimeDefaults_CrudRoundtrip_Works()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.Meal_WithoutMealKind_IsRejected()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.OccupancyUrl()]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[.Occupancy_Reassign_Tasks_And_Dashboard_Work()]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[.ParallelBookings_ForTheLastRooms_ExactlyOneWins()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.PublicSchedule_ByToken_ShowsMenu_ButHidesPrepNotes()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.ResettingAGroupsMealTime_RestoresTheCenterTime()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.Rooms_CrudRoundtrip_Works()]] - code - tests/CampCenter.IntegrationTests/RoomsAndClosuresApiTests.cs
- [[.ScheduleEndpoints_WithoutToken_ReturnUnauthorized()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.ScheduleEntries_CrudRoundtrip_Works()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.ScheduleEntry_EndBeforeStart_IsRejected()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.ScheduleEntry_OnDepartureDay_IsAccepted_ButNotOutsideTheStay()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.SeedAdminUserAsync()]] - code - src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs
- [[.SeedMealTimeDefaultsAsync()]] - code - src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs
- [[.SetUpRoomsAsync()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.SettingAGroupsMealTime_RejectsEndBeforeStart_AndStaleRowVersion()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.SettingAGroupsMealTime_RetimesTheWholeStay_ButNotOtherGroups()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.UnknownManageToken_Returns404()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[AdminPanelApiTests]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[ApiCollection]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[ApiCollection.cs]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[AuthApiTests]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[AuthApiTests.cs]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[BookingId_2]] - code
- [[CampCenterApiFactory]] - code - tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs
- [[CancellationToken_52]] - code
- [[Capacity_1]] - code
- [[Count_2]] - code
- [[DataSeeder]] - code - src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs
- [[DateOnly_30]] - code
- [[DateOnly_31]] - code
- [[DateOnly_32]] - code
- [[Dictionary_9]] - code
- [[End_3]] - code
- [[Fact]] - code
- [[Fact_1]] - code
- [[Fact_3]] - code
- [[Fact_4]] - code
- [[Fact_5]] - code
- [[Guid_57]] - code
- [[HttpClient_1]] - code
- [[HttpClient_3]] - code
- [[HttpClient_4]] - code
- [[IAsyncLifetime]] - code
- [[ICollectionFixture]] - code
- [[IServiceProvider]] - code
- [[IWebHostBuilder]] - code
- [[IntegrationTestBase]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[List_35]] - code
- [[PostgreSqlContainer]] - code
- [[PublicBookingApiTests]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[RoomsAndClosuresApiTests]] - code - tests/CampCenter.IntegrationTests/RoomsAndClosuresApiTests.cs
- [[ScheduleApiTests]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[Start_3]] - code
- [[Task_51]] - code
- [[Task_62]] - code
- [[Task_63]] - code
- [[Task_64]] - code
- [[Task_65]] - code
- [[Task_67]] - code
- [[Task_68]] - code
- [[Task_69]] - code
- [[Token_1]] - code
- [[WebApplicationFactory]] - code
- [[int_3]] - code
- [[long_1]] - code
- [[string_11]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/tests_/_CampCenterIntegrationTests_1
SORT file.name ASC
```

## Connections to other communities
- 6 edges to [[_COMMUNITY_Integration Test Harness (1)]]
- 3 edges to [[_COMMUNITY_Public Booking Service (2)]]
- 2 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (5)]]
- 2 edges to [[_COMMUNITY_CampCenter.Application  Services (2)]]
- 1 edge to [[_COMMUNITY_Public Booking Service (1)]]
- 1 edge to [[_COMMUNITY_DTOs  Schedule (2)]]
- 1 edge to [[_COMMUNITY_CampCenter.Domain  Repositories (1)]]

## Top bridge nodes
- [[ApiCollection.cs]] - degree 4, connects to 2 communities
- [[AuthApiTests.cs]] - degree 3, connects to 2 communities
- [[ScheduleApiTests]] - degree 26, connects to 1 community
- [[.GetMealTimesAsync()]] - degree 12, connects to 1 community
- [[IntegrationTestBase]] - degree 11, connects to 1 community