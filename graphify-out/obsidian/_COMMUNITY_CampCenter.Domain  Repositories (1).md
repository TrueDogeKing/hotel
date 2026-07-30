---
type: community
cohesion: 0.15
members: 33
---

# CampCenter.Domain / Repositories (1)

**Cohesion:** 0.15 - loosely connected
**Members:** 33 nodes

## Members
- [[.AddAsync()_12]] - code - src/CampCenter.Domain/Repositories/IScheduleEntryRepository.cs
- [[.AddRangeAsync()]] - code - src/CampCenter.Domain/Repositories/IScheduleEntryRepository.cs
- [[.CountByDateAndKindAsync()]] - code - src/CampCenter.Domain/Repositories/IScheduleEntryRepository.cs
- [[.GetByIdAsync()_10]] - code - src/CampCenter.Domain/Repositories/IScheduleEntryRepository.cs
- [[.ListForBookingAsync()_1]] - code - src/CampCenter.Domain/Repositories/IScheduleEntryRepository.cs
- [[.ListForDateAsync()_1]] - code - src/CampCenter.Domain/Repositories/IScheduleEntryRepository.cs
- [[.ListFullySuppressedSlotsAsync()]] - code - src/CampCenter.Domain/Repositories/IScheduleEntryRepository.cs
- [[.ListGeneratedSlotsAsync()]] - code - src/CampCenter.Domain/Repositories/IScheduleEntryRepository.cs
- [[.ListLocationsAsync()]] - code - src/CampCenter.Domain/Repositories/IScheduleEntryRepository.cs
- [[.ListVisibleSlotSpansAsync()]] - code - src/CampCenter.Domain/Repositories/IScheduleEntryRepository.cs
- [[.Remove()_8]] - code - src/CampCenter.Domain/Repositories/IScheduleEntryRepository.cs
- [[BookingId]] - code
- [[CancellationToken_63]] - code
- [[Count_1]] - code
- [[Date_1]] - code
- [[DateOnly_25]] - code
- [[DateOnly_27]] - code
- [[DateTime_17]] - code
- [[End_1]] - code
- [[Guid_53]] - code
- [[Guid_57]] - code
- [[IReadOnlyCollection_4]] - code
- [[IScheduleEntryRepository]] - code - src/CampCenter.Domain/Repositories/IScheduleEntryRepository.cs
- [[Kind]] - code
- [[List_34]] - code
- [[MealTimeDefaultId]] - code
- [[ScheduleEntry_1]] - code - src/CampCenter.Domain/Entities/ScheduleEntry.cs
- [[ScheduleEntry.cs]] - code - src/CampCenter.Domain/Entities/ScheduleEntry.cs
- [[ScheduleEntryKind_1]] - code - src/CampCenter.Domain/Entities/ScheduleEntry.cs
- [[Start_1]] - code
- [[Task_68]] - code
- [[TimeOnly_6]] - code
- [[TimeOnly_7]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/CampCenterDomain_/_Repositories_1
SORT file.name ASC
```

## Connections to other communities
- 22 edges to [[_COMMUNITY_CampCenter.Application  Services (2)]]
- 9 edges to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (1)]]
- 5 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (4)]]
- 2 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 2 edges to [[_COMMUNITY_Public Booking Service (2)]]
- 2 edges to [[_COMMUNITY_CampCenter.Domain  Repositories (2)]]
- 2 edges to [[_COMMUNITY_CampCenter.Application  Services (3)]]
- 2 edges to [[_COMMUNITY_Persistence  Configurations]]
- 1 edge to [[_COMMUNITY_Booking Persistence & Entities (2)]]

## Top bridge nodes
- [[ScheduleEntry_1]] - degree 28, connects to 6 communities
- [[IScheduleEntryRepository]] - degree 17, connects to 5 communities
- [[ScheduleEntryKind_1]] - degree 8, connects to 3 communities
- [[.ListForBookingAsync()_1]] - degree 10, connects to 2 communities
- [[.ListFullySuppressedSlotsAsync()]] - degree 10, connects to 2 communities