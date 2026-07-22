---
type: community
cohesion: 0.10
members: 41
---

# Admin Bookings Controller & DTOs

**Cohesion:** 0.10 - loosely connected
**Members:** 41 nodes

## Members
- [[.Cancel()]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[.CancelAsync()]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[.Get()]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[.Get()_1]] - code - src/CampCenter.Api/Controllers/Admin/DashboardController.cs
- [[.GetAsync()]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[.GetDashboardAsync()]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[.GetOccupancyAsync()]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[.List()]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[.ListAsync()]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[.Reassign()]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[.ReassignAsync()]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[AdminAssignmentDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[AdminPanelDtos.cs]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[BookingsController]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[CancellationToken_1]] - code
- [[CancellationToken_2]] - code
- [[CancellationToken_12]] - code
- [[ControllerBase]] - code
- [[CreateRoomTaskRequestDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[DashboardController]] - code - src/CampCenter.Api/Controllers/Admin/DashboardController.cs
- [[DashboardDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[DashboardSessionDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[Guid]] - code
- [[Guid_5]] - code
- [[HttpGet]] - code
- [[HttpGet_1]] - code
- [[HttpPost]] - code
- [[HttpPut]] - code
- [[IActionResult]] - code
- [[IActionResult_1]] - code
- [[IAdminBookingService]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[List]] - code
- [[ProducesResponseType]] - code
- [[ProducesResponseType_1]] - code
- [[ReassignBookingRequestDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[ReassignmentEntryDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[RoomOccupancyDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[SessionOccupancyDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[Task_1]] - code
- [[Task_2]] - code
- [[Task_11]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Admin_Bookings_Controller__DTOs
SORT file.name ASC
```

## Connections to other communities
- 8 edges to [[_COMMUNITY_Admin Booking & Notifications]]
- 6 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 5 edges to [[_COMMUNITY_Room Task Management]]
- 3 edges to [[_COMMUNITY_Camp Session Management]]
- 2 edges to [[_COMMUNITY_Booking Persistence & Entities]]
- 1 edge to [[_COMMUNITY_Room Management]]
- 1 edge to [[_COMMUNITY_Auth Controller]]
- 1 edge to [[_COMMUNITY_Public Booking Service]]

## Top bridge nodes
- [[ControllerBase]] - degree 9, connects to 6 communities
- [[AdminPanelDtos.cs]] - degree 11, connects to 3 communities
- [[IAdminBookingService]] - degree 11, connects to 3 communities
- [[.ListAsync()]] - degree 8, connects to 2 communities
- [[.List()]] - degree 9, connects to 1 community