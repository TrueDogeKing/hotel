---
type: community
members: 27
---

# CampCenter.UnitTests / Services (1)

**Members:** 27 nodes

## Members
- [[.Default()]] - code - tests/CampCenter.UnitTests/Services/MealGenerationPlannerTests.cs
- [[.EffectiveSlots()]] - code - src/CampCenter.Application/Services/MealGenerationPlanner.cs
- [[.EffectiveSlots_AppliesTheGroupsOwnTimes_LeavingOtherSlotsAlone()]] - code - tests/CampCenter.UnitTests/Services/MealGenerationPlannerTests.cs
- [[.EffectiveSlots_WithoutOverrides_UsesCenterTimes()]] - code - tests/CampCenter.UnitTests/Services/MealGenerationPlannerTests.cs
- [[.FromDefault()]] - code - src/CampCenter.Application/Services/MealGenerationPlanner.cs
- [[.FromOverride()]] - code - src/CampCenter.Application/Services/MealGenerationPlanner.cs
- [[.Plan()]] - code - src/CampCenter.Application/Services/MealGenerationPlanner.cs
- [[.Plan_CoversDepartureDay_NotJustNightsStayed()]] - code - tests/CampCenter.UnitTests/Services/MealGenerationPlannerTests.cs
- [[.Plan_FiveNightStay_GivesDinnerOnArrival_AllMiddleDays_BreakfastOnDeparture()]] - code - tests/CampCenter.UnitTests/Services/MealGenerationPlannerTests.cs
- [[.Plan_NoActiveDefaults_YieldsNothing()]] - code - tests/CampCenter.UnitTests/Services/MealGenerationPlannerTests.cs
- [[.Plan_OneNightStay_HasNoMiddleDays()]] - code - tests/CampCenter.UnitTests/Services/MealGenerationPlannerTests.cs
- [[.Plan_OverrideCanRemoveTheArrivalDayMeal()]] - code - tests/CampCenter.UnitTests/Services/MealGenerationPlannerTests.cs
- [[.Plan_SlotStraddlingBothCutoffs_IsSkippedOnArrivalAndDeparture()]] - code - tests/CampCenter.UnitTests/Services/MealGenerationPlannerTests.cs
- [[.Plan_UsesOverriddenTimes_ForTheTravelDayCutoffs()]] - code - tests/CampCenter.UnitTests/Services/MealGenerationPlannerTests.cs
- [[.SeededDefaults()]] - code - tests/CampCenter.UnitTests/Services/MealGenerationPlannerTests.cs
- [[.SeededSlots()]] - code - tests/CampCenter.UnitTests/Services/MealGenerationPlannerTests.cs
- [[Date]] - code
- [[DateOnly_15]] - code
- [[Fact_8]] - code
- [[IEnumerable_1]] - code
- [[IReadOnlyList]] - code
- [[List_11]] - code
- [[List_36]] - code
- [[MealGenerationPlannerTests]] - code - tests/CampCenter.UnitTests/Services/MealGenerationPlannerTests.cs
- [[MealSlot]] - code - src/CampCenter.Application/Services/MealGenerationPlanner.cs
- [[Slot]] - code
- [[TimeOnly_9]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/CampCenterUnitTests_/_Services_1
SORT file.name ASC
```

## Connections to other communities
- 6 edges to [[_COMMUNITY_Admin Booking & Notifications (3)]]
- 4 edges to [[_COMMUNITY_Booking Persistence & Entities (2)]]
- 2 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 2 edges to [[_COMMUNITY_CampCenter.Domain  Repositories (2)]]
- 2 edges to [[_COMMUNITY_CampCenter.Application  Services (2)]]

## Top bridge nodes
- [[.EffectiveSlots()]] - degree 16, connects to 4 communities
- [[.Plan()]] - degree 16, connects to 2 communities
- [[.FromOverride()]] - degree 4, connects to 2 communities
- [[MealGenerationPlannerTests]] - degree 14, connects to 1 community
- [[.SeededDefaults()]] - degree 9, connects to 1 community