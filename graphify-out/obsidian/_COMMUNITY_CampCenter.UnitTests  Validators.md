---
type: community
cohesion: 0.08
members: 56
---

# CampCenter.UnitTests / Validators

**Cohesion:** 0.08 - loosely connected
**Members:** 56 nodes

## Members
- [[.ActivityWithoutMealKind_Passes()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.Create()_3]] - code - src/CampCenter.Api/Controllers/Admin/MealTimesController.cs
- [[.CreateAsync()_9]] - code - src/CampCenter.Application/Interfaces/IMealTimeService.cs
- [[.Delete()_2]] - code - src/CampCenter.Api/Controllers/Admin/MealTimesController.cs
- [[.DeleteAsync()_7]] - code - src/CampCenter.Application/Interfaces/IMealTimeService.cs
- [[.EmptyBookingId_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.EmptyLabel_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.EmptyTitle_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.EndBeforeStart_Fails()_1]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.EndTimeBeforeStartTime_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.EqualStartAndEndTime_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.GetAll()_2]] - code - src/CampCenter.Api/Controllers/Admin/MealTimesController.cs
- [[.GetAllAsync()_9]] - code - src/CampCenter.Application/Interfaces/IMealTimeService.cs
- [[.KindIsCaseInsensitive()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.MealWithUnknownMealKind_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.MealWithoutMealKind_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.NegativeSortOrder_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.NullParticipantCount_Passes()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.OverlongMenuOrPrepNotes_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.OverlongTitle_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.UnknownKind_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.UnknownMealKind_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.Update()_2]] - code - src/CampCenter.Api/Controllers/Admin/MealTimesController.cs
- [[.UpdateAsync()_5]] - code - src/CampCenter.Application/Interfaces/IMealTimeService.cs
- [[.UpdateValidator_EnforcesTheSameRules()_1]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.UpdateValidator_EnforcesTheSameRules()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.Valid()_1]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.ValidActivity()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.ValidActivity_Passes()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.ValidMeal()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.ValidMeal_Passes()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.ValidSlot_Passes()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.ZeroOrNegativeParticipantCount_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[CancellationToken_48]] - code
- [[CancellationToken_54]] - code
- [[CreateMealTimeDefaultRequestDto]] - code - src/CampCenter.Application/DTOs/Schedule/MealTimeDtos.cs
- [[CreateMealTimeDefaultRequestValidator]] - code - src/CampCenter.Application/Validators/MealTimeValidators.cs
- [[CreateScheduleEntryRequestDto]] - code - src/CampCenter.Application/DTOs/Schedule/ScheduleDtos.cs
- [[DeleteMealTimeDefaultResultDto]] - code - src/CampCenter.Application/DTOs/Schedule/MealTimeDtos.cs
- [[Fact_16]] - code
- [[Guid_40]] - code
- [[Guid_44]] - code
- [[HttpDelete_4]] - code
- [[HttpGet_9]] - code
- [[HttpPost_8]] - code
- [[HttpPut_5]] - code
- [[IActionResult_11]] - code
- [[IMealTimeService]] - code - src/CampCenter.Application/Interfaces/IMealTimeService.cs
- [[IValidator_6]] - code
- [[List_23]] - code
- [[MealTimeValidatorsTests]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[MealTimesController]] - code - src/CampCenter.Api/Controllers/Admin/MealTimesController.cs
- [[ProducesResponseType_11]] - code
- [[ScheduleEntryValidatorsTests]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[Task_53]] - code
- [[Task_59]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/CampCenterUnitTests_/_Validators
SORT file.name ASC
```

## Connections to other communities
- 6 edges to [[_COMMUNITY_DTOs  Schedule (1)]]
- 6 edges to [[_COMMUNITY_CampCenter.Application  Services (3)]]
- 4 edges to [[_COMMUNITY_DTOs  Schedule (2)]]
- 3 edges to [[_COMMUNITY_Validator Unit Tests]]
- 2 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 1 edge to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 1 edge to [[_COMMUNITY_CampCenter.Application  Services (2)]]
- 1 edge to [[_COMMUNITY_tests  CampCenter.IntegrationTests (1)]]

## Top bridge nodes
- [[CreateScheduleEntryRequestDto]] - degree 8, connects to 3 communities
- [[ScheduleEntryValidatorsTests]] - degree 20, connects to 2 communities
- [[MealTimesController]] - degree 8, connects to 2 communities
- [[IMealTimeService]] - degree 7, connects to 2 communities
- [[.UpdateAsync()_5]] - degree 7, connects to 2 communities