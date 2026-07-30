---
type: community
cohesion: 0.24
members: 14
---

# DTOs / AdminPanel

**Cohesion:** 0.24 - loosely connected
**Members:** 14 nodes

## Members
- [[.GetDayAsync()]] - code - src/CampCenter.Application/Interfaces/IHousekeepingService.cs
- [[.GetRangeAsync()]] - code - src/CampCenter.Application/Interfaces/IHousekeepingService.cs
- [[.SetStatusAsync()_3]] - code - src/CampCenter.Application/Interfaces/IHousekeepingService.cs
- [[CancellationToken_53]] - code
- [[DateOnly_16]] - code
- [[Guid_43]] - code
- [[HousekeepingDayDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/HousekeepingDtos.cs
- [[HousekeepingDaySummaryDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/HousekeepingDtos.cs
- [[HousekeepingDtos.cs]] - code - src/CampCenter.Application/DTOs/AdminPanel/HousekeepingDtos.cs
- [[HousekeepingRangeDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/HousekeepingDtos.cs
- [[HousekeepingRoomDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/HousekeepingDtos.cs
- [[IHousekeepingService]] - code - src/CampCenter.Application/Interfaces/IHousekeepingService.cs
- [[SetRoomCleaningRequestDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/HousekeepingDtos.cs
- [[Task_58]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/DTOs_/_AdminPanel
SORT file.name ASC
```

## Connections to other communities
- 5 edges to [[_COMMUNITY_Controllers  Admin]]
- 5 edges to [[_COMMUNITY_CampCenter.Application  Services (4)]]
- 2 edges to [[_COMMUNITY_Application Namespaces & DTOs]]

## Top bridge nodes
- [[IHousekeepingService]] - degree 6, connects to 3 communities
- [[SetRoomCleaningRequestDto]] - degree 4, connects to 2 communities
- [[.SetStatusAsync()_3]] - degree 8, connects to 1 community
- [[HousekeepingDtos.cs]] - degree 6, connects to 1 community
- [[.GetDayAsync()]] - degree 6, connects to 1 community