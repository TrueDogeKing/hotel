---
type: community
cohesion: 0.18
members: 20
---

# CampCenter.UnitTests / Services (3)

**Cohesion:** 0.18 - loosely connected
**Members:** 20 nodes

## Members
- [[.A_one_night_stay_is_an_arrival_on_one_day_and_a_departure_on_the_next()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.A_room_being_vacated_today_is_a_departure()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.A_room_occupied_through_the_day_is_left_alone()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.A_room_taken_today_by_a_group_is_an_arrival()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.Booking()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.Departures_and_unrelated_arrivals_on_one_day_are_separate_jobs()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.Each_room_of_a_multi_room_group_gets_its_own_job()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.ForDay()]] - code - src/CampCenter.Application/Services/HousekeepingPlanner.cs
- [[.One_group_out_and_the_next_in_is_a_single_turnaround()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.The_day_before_a_departure_is_not_the_departure_day()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[DateOnly_20]] - code
- [[DateOnly_32]] - code
- [[Fact_11]] - code
- [[Guid_63]] - code
- [[HousekeepingJob]] - code - src/CampCenter.Application/Services/HousekeepingPlanner.cs
- [[HousekeepingPlanner]] - code - src/CampCenter.Application/Services/HousekeepingPlanner.cs
- [[HousekeepingPlanner.cs]] - code - src/CampCenter.Application/Services/HousekeepingPlanner.cs
- [[HousekeepingPlannerTests]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[IEnumerable]] - code
- [[List_26]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/CampCenterUnitTests_/_Services_3
SORT file.name ASC
```

## Connections to other communities
- 10 edges to [[_COMMUNITY_Booking Persistence & Entities (2)]]
- 3 edges to [[_COMMUNITY_CampCenter.Application  Services (4)]]
- 2 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (5)]]
- 1 edge to [[_COMMUNITY_Domain & Infra Namespaces]]

## Top bridge nodes
- [[.ForDay()]] - degree 16, connects to 2 communities
- [[HousekeepingPlanner.cs]] - degree 4, connects to 2 communities
- [[HousekeepingPlannerTests]] - degree 12, connects to 1 community
- [[.A_one_night_stay_is_an_arrival_on_one_day_and_a_departure_on_the_next()]] - degree 4, connects to 1 community
- [[.A_room_being_vacated_today_is_a_departure()]] - degree 4, connects to 1 community