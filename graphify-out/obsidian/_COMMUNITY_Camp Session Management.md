---
type: community
members: 21
---

# Camp Session Management

**Members:** 21 nodes

## Members
- [[.ActivityWithoutMealKind_Passes()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.EmptyBookingId_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.EmptyTitle_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.EndTimeBeforeStartTime_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.EqualStartAndEndTime_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.KindIsCaseInsensitive()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.MealWithUnknownMealKind_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.MealWithoutMealKind_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.NullParticipantCount_Passes()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.OverlongMenuOrPrepNotes_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.OverlongTitle_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.UnknownKind_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.UpdateValidator_EnforcesTheSameRules()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.ValidActivity()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.ValidActivity_Passes()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.ValidMeal()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.ValidMeal_Passes()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[.ZeroOrNegativeParticipantCount_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs
- [[CreateScheduleEntryRequestDto]] - code - src/CampCenter.Application/DTOs/Schedule/ScheduleDtos.cs
- [[Fact_15]] - code
- [[ScheduleEntryValidatorsTests]] - code - tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Camp_Session_Management
SORT file.name ASC
```

## Connections to other communities
- 6 edges to [[_COMMUNITY_MealTimeValidatorsTests]]
- 3 edges to [[_COMMUNITY_DTOs  Schedule (1)]]
- 3 edges to [[_COMMUNITY_CampCenter.UnitTests  Validators]]
- 1 edge to [[_COMMUNITY_CampCenter.Application  Services (2)]]
- 1 edge to [[_COMMUNITY_tests  CampCenter.IntegrationTests (1)]]

## Top bridge nodes
- [[CreateScheduleEntryRequestDto]] - degree 8, connects to 4 communities
- [[Fact_15]] - degree 22, connects to 1 community
- [[ScheduleEntryValidatorsTests]] - degree 20, connects to 1 community