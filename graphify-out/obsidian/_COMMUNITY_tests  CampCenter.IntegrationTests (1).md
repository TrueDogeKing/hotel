---
type: community
cohesion: 0.19
members: 35
---

# tests / CampCenter.IntegrationTests (1)

**Cohesion:** 0.19 - loosely connected
**Members:** 35 nodes

## Members
- [[.BookingMealTimes_DefaultToTheCenterTimes()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.BreakfastsAsync()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.BulkRetime_PreservesADayChangedIndividually()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.Calendar_BarSpansFullStayInclusive()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.CreateBookingAsync()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.DayView_IncludesDepartingGroup()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.DietaryNotes_AreStoredAndReturnedOnTheGroupSchedule()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.EntryOn()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.GenerateMealsAsync()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.GenerateMeals_CoversTheStay_AndIsIdempotent()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.GenerateMeals_DoesNotResurrectADeletedMeal()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.GenerationUsesTheGroupsOwnTimes_ForMealsAddedLater()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.GetMealTimesAsync()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.MealTimeDefaults_CrudRoundtrip_Works()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.Meal_WithoutMealKind_IsRejected()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.PublicSchedule_ByToken_ShowsMenu_ButHidesPrepNotes()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.ResettingAGroupsMealTime_RestoresTheCenterTime()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.ScheduleEndpoints_WithoutToken_ReturnUnauthorized()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.ScheduleEntries_CrudRoundtrip_Works()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.ScheduleEntry_EndBeforeStart_IsRejected()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.ScheduleEntry_OnDepartureDay_IsAccepted_ButNotOutsideTheStay()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.SettingAGroupsMealTime_RejectsEndBeforeStart_AndStaleRowVersion()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[.SettingAGroupsMealTime_RetimesTheWholeStay_ButNotOtherGroups()]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[BookingId_2]] - code
- [[DateOnly_31]] - code
- [[End_3]] - code
- [[Fact_10]] - code
- [[Guid_62]] - code
- [[HttpClient_4]] - code
- [[List_39]] - code
- [[ScheduleApiTests]] - code - tests/CampCenter.IntegrationTests/ScheduleApiTests.cs
- [[Start_3]] - code
- [[Task_74]] - code
- [[Token_2]] - code
- [[int_3]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/tests_/_CampCenterIntegrationTests_1
SORT file.name ASC
```

## Connections to other communities
- 2 edges to [[_COMMUNITY_CampCenter.Application  Services (2)]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Validators]]
- 1 edge to [[_COMMUNITY_Integration Test Harness (1)]]
- 1 edge to [[_COMMUNITY_tests  CampCenter.IntegrationTests (2)]]

## Top bridge nodes
- [[ScheduleApiTests]] - degree 26, connects to 2 communities
- [[.GetMealTimesAsync()]] - degree 12, connects to 1 community
- [[.BreakfastsAsync()]] - degree 10, connects to 1 community
- [[.EntryOn()]] - degree 5, connects to 1 community