---
type: community
members: 8
---

# Admin Booking & Notifications (2)

**Members:** 8 nodes

## Members
- [[.CancelByTokenAsync()]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[.CreateAsync()_1]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[.GetByTokenAsync()]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[.GetScheduleByTokenAsync()]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[CancellationToken_20]] - code
- [[IBookingService]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[PublicScheduleDto]] - code - src/CampCenter.Application/DTOs/Schedule/ScheduleDtos.cs
- [[Task_20]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Admin_Booking__Notifications_2
SORT file.name ASC
```

## Connections to other communities
- 5 edges to [[_COMMUNITY_BookingSettings]]
- 3 edges to [[_COMMUNITY_Public Booking Service (1)]]
- 2 edges to [[_COMMUNITY_Persistence  Migrations (2)]]
- 1 edge to [[_COMMUNITY_DTOs  Schedule (1)]]
- 1 edge to [[_COMMUNITY_CampCenter.Application  Services (2)]]
- 1 edge to [[_COMMUNITY_CampCenter.Application.DTOs.Public]]

## Top bridge nodes
- [[IBookingService]] - degree 7, connects to 3 communities
- [[.CreateAsync()_1]] - degree 6, connects to 3 communities
- [[PublicScheduleDto]] - degree 4, connects to 3 communities
- [[.GetByTokenAsync()]] - degree 5, connects to 2 communities
- [[.GetScheduleByTokenAsync()]] - degree 5, connects to 1 community