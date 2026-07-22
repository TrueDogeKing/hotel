---
type: community
cohesion: 0.18
members: 19
---

# Application Namespaces & DTOs

**Cohesion:** 0.18 - loosely connected
**Members:** 19 nodes

## Members
- [[AdminBookingService.cs]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[BookingsController.cs]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[CampCenter.Api.Controllers.Admin]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[CampCenter.Api.Controllers.Public]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[CampCenter.Application.DTOs.AdminPanel]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[CampCenter.Application.Interfaces]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[DashboardController.cs]] - code - src/CampCenter.Api/Controllers/Admin/DashboardController.cs
- [[IAdminBookingService.cs]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[IRoomTaskService.cs]] - code - src/CampCenter.Application/Interfaces/IRoomTaskService.cs
- [[PublicBookingsController.cs]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[PublicPaymentsController]] - code - src/CampCenter.Api/Controllers/Public/PublicPaymentsController.cs
- [[PublicPaymentsController.cs]] - code - src/CampCenter.Api/Controllers/Public/PublicPaymentsController.cs
- [[PublicSessionsController]] - code - src/CampCenter.Api/Controllers/Public/PublicSessionsController.cs
- [[PublicSessionsController.cs]] - code - src/CampCenter.Api/Controllers/Public/PublicSessionsController.cs
- [[RoomDeleteResultDto]] - code - src/CampCenter.Api/Controllers/Admin/RoomsController.cs
- [[RoomTaskService.cs]] - code - src/CampCenter.Application/Services/RoomTaskService.cs
- [[RoomsController.cs]] - code - src/CampCenter.Api/Controllers/Admin/RoomsController.cs
- [[SessionsController.cs]] - code - src/CampCenter.Api/Controllers/Admin/SessionsController.cs
- [[TasksController.cs]] - code - src/CampCenter.Api/Controllers/Admin/TasksController.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Application_Namespaces__DTOs
SORT file.name ASC
```

## Connections to other communities
- 10 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 10 edges to [[_COMMUNITY_Auth DTOs & Models]]
- 8 edges to [[_COMMUNITY_Application DTO Namespaces]]
- 6 edges to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 5 edges to [[_COMMUNITY_Rate Limiting & Startup]]
- 4 edges to [[_COMMUNITY_Public Booking Service]]
- 3 edges to [[_COMMUNITY_Room Task Management]]
- 3 edges to [[_COMMUNITY_Room Mix Calculator Tests]]
- 3 edges to [[_COMMUNITY_Admin Booking & Notifications]]
- 3 edges to [[_COMMUNITY_Przelewy24 Payment Client]]
- 2 edges to [[_COMMUNITY_Room Management]]
- 2 edges to [[_COMMUNITY_Camp Session Management]]
- 2 edges to [[_COMMUNITY_Payment Gateway Integration Tests]]
- 2 edges to [[_COMMUNITY_Domain Exceptions]]
- 1 edge to [[_COMMUNITY_Claims Principal Extensions]]
- 1 edge to [[_COMMUNITY_Application DI Registration]]
- 1 edge to [[_COMMUNITY_Password Hashing (bcrypt)]]

## Top bridge nodes
- [[CampCenter.Application.Interfaces]] - degree 41, connects to 13 communities
- [[AdminBookingService.cs]] - degree 8, connects to 4 communities
- [[RoomTaskService.cs]] - degree 7, connects to 4 communities
- [[TasksController.cs]] - degree 6, connects to 3 communities
- [[PublicBookingsController.cs]] - degree 5, connects to 3 communities