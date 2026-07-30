---
type: community
cohesion: 0.06
members: 71
---

# Admin Bookings Controller & DTOs

**Cohesion:** 0.06 - loosely connected
**Members:** 71 nodes

## Members
- [[.Cancel()]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[.CancelAsync()]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[.Closures()]] - code - src/CampCenter.Api/Controllers/Public/PublicAvailabilityController.cs
- [[.Create()_1]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[.CreateAsync()_6]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[.Get()_1]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[.Get()]] - code - src/CampCenter.Api/Controllers/Admin/DashboardController.cs
- [[.Get()_2]] - code - src/CampCenter.Api/Controllers/Admin/OccupancyController.cs
- [[.Get()_3]] - code - src/CampCenter.Api/Controllers/Public/PublicAvailabilityController.cs
- [[.GetAsync()]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[.GetDashboardAsync()]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[.GetOccupancyAsync()]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[.List()]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[.ListAsync()_4]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[.Reassign()]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[.ReassignAsync()]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[.SetStatus()]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[.SetStatusAsync()_2]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[.UpdateDietaryNotes()]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[.UpdateDietaryNotesAsync()]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[AdminAssignmentDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[AdminPanelDtos.cs]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[BookingsController]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[CancellationToken_1]] - code
- [[CancellationToken_2]] - code
- [[CancellationToken_49]] - code
- [[CancellationToken_51]] - code
- [[CancellationToken_12]] - code
- [[ControllerBase]] - code
- [[CreateAdminBookingRequestDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[CreateAdminBookingRequestValidator]] - code - src/CampCenter.Application/Validators/AdminBookingValidators.cs
- [[CreateRoomTaskRequestDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[DashboardBookingDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[DashboardController]] - code - src/CampCenter.Api/Controllers/Admin/DashboardController.cs
- [[DashboardDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[DateOnly_12]] - code
- [[DateOnly_14]] - code
- [[DateOnly_15]] - code
- [[Guid]] - code
- [[Guid_5]] - code
- [[HttpGet]] - code
- [[HttpGet_1]] - code
- [[HttpGet_10]] - code
- [[HttpGet_12]] - code
- [[HttpPost]] - code
- [[HttpPut]] - code
- [[IActionResult]] - code
- [[IActionResult_1]] - code
- [[IActionResult_12]] - code
- [[IActionResult_14]] - code
- [[IAdminBookingService]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[IAvailabilityService_1]] - code
- [[IClosureRepository_1]] - code
- [[IValidator_4]] - code
- [[List]] - code
- [[OccupancyController]] - code - src/CampCenter.Api/Controllers/Admin/OccupancyController.cs
- [[OccupancyDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[ProducesResponseType]] - code
- [[ProducesResponseType_1]] - code
- [[ProducesResponseType_12]] - code
- [[ProducesResponseType_14]] - code
- [[PublicAvailabilityController]] - code - src/CampCenter.Api/Controllers/Public/PublicAvailabilityController.cs
- [[ReassignBookingRequestDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[ReassignmentEntryDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[RoomOccupancyDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[SetBookingStatusRequestDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[Task_1]] - code
- [[Task_2]] - code
- [[Task_54]] - code
- [[Task_56]] - code
- [[Task_11]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Admin_Bookings_Controller__DTOs
SORT file.name ASC
```

## Connections to other communities
- 13 edges to [[_COMMUNITY_Admin Booking & Notifications (1)]]
- 6 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 4 edges to [[_COMMUNITY_DTOs  Schedule (1)]]
- 3 edges to [[_COMMUNITY_Room Task Management (1)]]
- 2 edges to [[_COMMUNITY_Room Task Management (2)]]
- 2 edges to [[_COMMUNITY_Booking Persistence & Entities (2)]]
- 1 edge to [[_COMMUNITY_CampCenter.Application  Services (1)]]
- 1 edge to [[_COMMUNITY_Controllers  Admin]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Validators]]
- 1 edge to [[_COMMUNITY_Room Management]]
- 1 edge to [[_COMMUNITY_Auth Controller (1)]]
- 1 edge to [[_COMMUNITY_Public Booking Service (1)]]
- 1 edge to [[_COMMUNITY_Payment Gateway Integration Tests (1)]]
- 1 edge to [[_COMMUNITY_Validator Unit Tests]]

## Top bridge nodes
- [[ControllerBase]] - degree 13, connects to 9 communities
- [[AdminPanelDtos.cs]] - degree 13, connects to 3 communities
- [[IAdminBookingService]] - degree 14, connects to 2 communities
- [[.ListAsync()_4]] - degree 7, connects to 2 communities
- [[.UpdateDietaryNotesAsync()]] - degree 7, connects to 2 communities