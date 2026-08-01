---
type: community
members: 104
---

# tests / CampCenter.IntegrationTests (1)

**Members:** 104 nodes

## Members
- [[.AdminEndpoints_WithoutToken_ReturnUnauthorized()]] - code - tests/CampCenter.IntegrationTests/RoomsAndClosuresApiTests.cs
- [[.Administrator_CanAdd_ChangeRole_AndDelete_Accounts()]] - code - tests/CampCenter.IntegrationTests/UsersAndRolesApiTests.cs
- [[.Administrator_CanResetAPassword_AndItEndsThatAccountsSessions()]] - code - tests/CampCenter.IntegrationTests/UsersAndRolesApiTests.cs
- [[.AssignableRooms_OffersOwnAndFreeRooms_ButNotTakenOrClosedOnes()]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[.AvailabilityUrl()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.BookingFlow_AvailabilityShrinks_AndCancelFreesRooms()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.BookingMealTimes_DefaultToTheCenterTimes()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.BookingRequest()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.Booking_RedundantRoomSelection_IsRejected()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.Booking_WhenCenterClosed_IsRejected()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.BreakfastsAsync()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.BulkRetime_PreservesADayChangedIndividually()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.CalendarUrl()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.Calendar_BarSpansFullStayInclusive()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.Calendar_MarksClosedAndBookedNights_PerNight()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.Calendar_RefusesAnUnreasonableSpan()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.Closure_BlocksRoom_InOccupancyGrid()]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[.Closures_CrudRoundtrip_Works()]] - code - tests/CampCenter.IntegrationTests/RoomsAndClosuresApiTests.cs
- [[.CreateAuthenticatedClientAsync()]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[.CreateBookingAsync()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.CreateClient()]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[.CreateClientForAsync()]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[.CreateWorkerAsync()]] - code - tests/CampCenter.IntegrationTests/UsersAndRolesApiTests.cs
- [[.DashboardGroups_SplitByCategory_AndPage()]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[.DayView_IncludesDepartingGroup()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.DietaryNotes_AreStoredAndReturnedOnTheGroupSchedule()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.DuplicateLogin_AndWeakPassword_AreRejected()]] - code - tests/CampCenter.IntegrationTests/UsersAndRolesApiTests.cs
- [[.EntryOn()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.GenerateMealsAsync()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.GenerateMeals_CoversTheStay_AndIsIdempotent()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.GenerateMeals_DoesNotResurrectADeletedMeal()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.GenerationUsesTheGroupsOwnTimes_ForMealsAddedLater()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.GetMealTimesAsync()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
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
- [[.RoleChange_EndsTheAffectedAccountsSessions()]] - code - tests/CampCenter.IntegrationTests/UsersAndRolesApiTests.cs
- [[.Rooms_CrudRoundtrip_Works()]] - code - tests/CampCenter.IntegrationTests/RoomsAndClosuresApiTests.cs
- [[.ScheduleEndpoints_WithoutToken_ReturnUnauthorized()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.ScheduleEntries_CrudRoundtrip_Works()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.ScheduleEntry_EndBeforeStart_IsRejected()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.ScheduleEntry_OnDepartureDay_IsAccepted_ButNotOutsideTheStay()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.SelfDelete_AndLastAdministrator_AreRefused()]] - code - tests/CampCenter.IntegrationTests/UsersAndRolesApiTests.cs
- [[.SetUpRoomsAsync()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.SettingAGroupsMealTime_RejectsEndBeforeStart_AndStaleRowVersion()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.SettingAGroupsMealTime_RetimesTheWholeStay_ButNotOtherGroups()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.UniqueLogin()]] - code - tests/CampCenter.IntegrationTests/UsersAndRolesApiTests.cs
- [[.UnknownManageToken_Returns404()]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[.Worker_ReadsEverySection_ButCannotWriteAnywhere()]] - code - tests/CampCenter.IntegrationTests/UsersAndRolesApiTests.cs
- [[Admin]] - code
- [[AdminPanelApiTests]] - code - tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs
- [[ApiCollection]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[ApiCollection.cs]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[AuthApiTests]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[AuthApiTests.cs]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[BookingId_2]] - code
- [[Capacity_1]] - code
- [[Count_2]] - code
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
- [[Fact_6]] - code
- [[Guid_60]] - code
- [[HttpClient_1]] - code
- [[HttpClient_3]] - code
- [[HttpClient_4]] - code
- [[HttpClient_5]] - code
- [[ICollectionFixture]] - code
- [[IntegrationTestBase]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[List_39]] - code
- [[PublicBookingApiTests]] - code - tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs
- [[RoomsAndClosuresApiTests]] - code - tests/CampCenter.IntegrationTests/RoomsAndClosuresApiTests.cs
- [[ScheduleApiTests]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[Start_3]] - code
- [[Task_66]] - code
- [[Task_67]] - code
- [[Task_68]] - code
- [[Task_71]] - code
- [[Task_72]] - code
- [[Task_73]] - code
- [[Task_74]] - code
- [[Token_1]] - code
- [[UsersAndRolesApiTests]] - code - tests/CampCenter.IntegrationTests/UsersAndRolesApiTests.cs
- [[Worker]] - code
- [[int_5]] - code
- [[long_1]] - code
- [[string_13]] - code
- [[string_14]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/tests_/_CampCenterIntegrationTests_1
SORT file.name ASC
```

## Connections to other communities
- 6 edges to [[_COMMUNITY_CampCenter.Application.DTOs.Public]]
- 2 edges to [[_COMMUNITY_CampCenter.UnitTests  Validators]]
- 2 edges to [[_COMMUNITY_CampCenter.Application  Services (2)]]
- 2 edges to [[_COMMUNITY_Payment Gateway Integration Tests (2)]]
- 1 edge to [[_COMMUNITY_Public Booking Service (1)]]
- 1 edge to [[_COMMUNITY_Camp Session Management]]
- 1 edge to [[_COMMUNITY_tests  CampCenter.IntegrationTests (2)]]
- 1 edge to [[_COMMUNITY_Payment Gateway Integration Tests (1)]]
- 1 edge to [[_COMMUNITY_PasswordRules]]

## Top bridge nodes
- [[IntegrationTestBase]] - degree 13, connects to 2 communities
- [[ApiCollection.cs]] - degree 4, connects to 2 communities
- [[AuthApiTests.cs]] - degree 3, connects to 2 communities
- [[ScheduleApiTests]] - degree 26, connects to 1 community
- [[PublicBookingApiTests]] - degree 14, connects to 1 community