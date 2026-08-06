---
type: community
members: 12
---

# .GetBlockedRoomIdsAsync

**Members:** 12 nodes

## Members
- [[.GetAvailabilityAsync()]] - code - src/CampCenter.Application/Interfaces/IAvailabilityService.cs
- [[.GetCalendarAsync()]] - code - src/CampCenter.Application/Interfaces/IAvailabilityService.cs
- [[.GetCenterClosureReasonAsync()]] - code - src/CampCenter.Application/Interfaces/IAvailabilityService.cs
- [[.GetFreeRoomsByCapacityAsync()]] - code - src/CampCenter.Application/Interfaces/IAvailabilityService.cs
- [[AvailabilityCalendarDto]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[AvailabilityDto]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[CancellationToken_19]] - code
- [[DateOnly_5]] - code
- [[Dictionary]] - code
- [[Guid_10]] - code
- [[IAvailabilityService]] - code - src/CampCenter.Application/Interfaces/IAvailabilityService.cs
- [[Task_19]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/GetBlockedRoomIdsAsync
SORT file.name ASC
```

## Connections to other communities
- 8 edges to [[_COMMUNITY_Admin Booking & Notifications (1)]]
- 5 edges to [[_COMMUNITY_Public Booking Service (1)]]
- 3 edges to [[_COMMUNITY_.Calendar]]
- 3 edges to [[_COMMUNITY_useAuth]]
- 1 edge to [[_COMMUNITY_CampCenter.Application.DTOs.Public]]

## Top bridge nodes
- [[IAvailabilityService]] - degree 10, connects to 5 communities
- [[.GetFreeRoomsByCapacityAsync()]] - degree 8, connects to 2 communities
- [[.GetCenterClosureReasonAsync()]] - degree 6, connects to 2 communities
- [[AvailabilityDto]] - degree 3, connects to 2 communities
- [[AvailabilityCalendarDto]] - degree 3, connects to 2 communities