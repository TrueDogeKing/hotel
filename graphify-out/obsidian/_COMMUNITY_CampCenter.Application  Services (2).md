---
type: community
cohesion: 0.19
members: 38
---

# CampCenter.Application / Services (2)

**Cohesion:** 0.19 - loosely connected
**Members:** 38 nodes

## Members
- [[.CheckConflictsAsync()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.CreateEntryAsync()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.DeleteBookingMealsAsync()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.DeleteEntryAsync()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.GenerateMealsForBookingAsync()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.GenerateMissingMealsAsync()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.GetBookingMealTimesAsync()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.GetBookingOrThrowAsync()]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.GetCalendarAsync()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.GetDayAsync()_3]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.GetEntryOrThrowAsync()]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.GetForBookingAsync()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.GetLocationsAsync()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.GuardBookingIsLive()]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.GuardDateWithinStay()]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.GuardTimes()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.Minutes()]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.Normalize()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.ParseKind()]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.ParseMealKind()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.ResetBookingMealTimeAsync()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.RetimeGeneratedMealsAsync()]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.SaveChangesAsync()_14]] - code - src/CampCenter.Domain/Repositories/IScheduleEntryRepository.cs
- [[.SetBookingMealTimeAsync()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.ToDto()_6]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.ToPublicDto()]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[.UpdateEntryAsync()_1]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[BookingMealTimeDto]] - code - src/CampCenter.Application/DTOs/Schedule/MealTimeDtos.cs
- [[CancellationToken_59]] - code
- [[DateOnly_23]] - code
- [[Guid_49]] - code
- [[List_30]] - code
- [[ScheduleEntryDto]] - code - src/CampCenter.Application/DTOs/Schedule/ScheduleDtos.cs
- [[ScheduleService]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[Skipped]] - code
- [[Task_64]] - code
- [[TimeOnly_3]] - code
- [[Updated]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/CampCenterApplication_/_Services_2
SORT file.name ASC
```

## Connections to other communities
- 22 edges to [[_COMMUNITY_CampCenter.Domain  Repositories (1)]]
- 16 edges to [[_COMMUNITY_DTOs  Schedule (1)]]
- 15 edges to [[_COMMUNITY_CampCenter.Domain  Repositories (2)]]
- 7 edges to [[_COMMUNITY_CampCenter.Application  Services (3)]]
- 6 edges to [[_COMMUNITY_Booking Persistence & Entities (2)]]
- 6 edges to [[_COMMUNITY_Admin Booking & Notifications (2)]]
- 2 edges to [[_COMMUNITY_DTOs  Schedule (2)]]
- 2 edges to [[_COMMUNITY_tests  CampCenter.IntegrationTests (1)]]
- 2 edges to [[_COMMUNITY_Rate Limiting & Startup]]
- 2 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (1)]]
- 2 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (4)]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Validators]]
- 1 edge to [[_COMMUNITY_Public Booking Service (1)]]
- 1 edge to [[_COMMUNITY_Public Booking Service (2)]]

## Top bridge nodes
- [[ScheduleService]] - degree 36, connects to 8 communities
- [[.ToDto()_6]] - degree 15, connects to 5 communities
- [[.GenerateMealsForBookingAsync()_1]] - degree 17, connects to 4 communities
- [[.GetBookingMealTimesAsync()_1]] - degree 13, connects to 4 communities
- [[.ToPublicDto()]] - degree 6, connects to 4 communities