---
type: community
cohesion: 0.14
members: 29
---

# Application Namespaces & DTOs

**Cohesion:** 0.14 - loosely connected
**Members:** 29 nodes

## Members
- [[BookingsController.cs]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[CampCenter.Api.Controllers.Admin]] - code - src/CampCenter.Api/Controllers/Admin/BookingsController.cs
- [[CampCenter.Api.Controllers.Public]] - code - src/CampCenter.Api/Controllers/Public/PublicAvailabilityController.cs
- [[CampCenter.Application.DTOs.AdminPanel]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[CampCenter.Application.DTOs.Rooms]] - code - src/CampCenter.Application/DTOs/Rooms/RoomDtos.cs
- [[CampCenter.Application.DTOs.Schedule]] - code - src/CampCenter.Application/DTOs/Schedule/MealTimeDtos.cs
- [[CampCenter.Application.Interfaces]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[ClosuresController.cs]] - code - src/CampCenter.Api/Controllers/Admin/ClosuresController.cs
- [[DashboardController.cs]] - code - src/CampCenter.Api/Controllers/Admin/DashboardController.cs
- [[HousekeepingController.cs]] - code - src/CampCenter.Api/Controllers/Admin/HousekeepingController.cs
- [[HousekeepingService.cs]] - code - src/CampCenter.Application/Services/HousekeepingService.cs
- [[IAdminBookingService.cs]] - code - src/CampCenter.Application/Interfaces/IAdminBookingService.cs
- [[IBookingService.cs]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[IHousekeepingService.cs]] - code - src/CampCenter.Application/Interfaces/IHousekeepingService.cs
- [[IMealTimeService.cs]] - code - src/CampCenter.Application/Interfaces/IMealTimeService.cs
- [[IRoomService.cs]] - code - src/CampCenter.Application/Interfaces/IRoomService.cs
- [[IRoomTaskService.cs]] - code - src/CampCenter.Application/Interfaces/IRoomTaskService.cs
- [[IScheduleService.cs]] - code - src/CampCenter.Application/Interfaces/IScheduleService.cs
- [[MealTimesController.cs]] - code - src/CampCenter.Api/Controllers/Admin/MealTimesController.cs
- [[OccupancyController.cs]] - code - src/CampCenter.Api/Controllers/Admin/OccupancyController.cs
- [[PublicAvailabilityController.cs]] - code - src/CampCenter.Api/Controllers/Public/PublicAvailabilityController.cs
- [[PublicBookingsController.cs]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[PublicPaymentsController.cs]] - code - src/CampCenter.Api/Controllers/Public/PublicPaymentsController.cs
- [[RoomDeleteResultDto]] - code - src/CampCenter.Api/Controllers/Admin/RoomsController.cs
- [[RoomService.cs]] - code - src/CampCenter.Application/Services/RoomService.cs
- [[RoomTaskService.cs]] - code - src/CampCenter.Application/Services/RoomTaskService.cs
- [[RoomsController.cs]] - code - src/CampCenter.Api/Controllers/Admin/RoomsController.cs
- [[ScheduleController.cs]] - code - src/CampCenter.Api/Controllers/Admin/ScheduleController.cs
- [[TasksController.cs]] - code - src/CampCenter.Api/Controllers/Admin/TasksController.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Application_Namespaces__DTOs
SORT file.name ASC
```

## Connections to other communities
- 20 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 11 edges to [[_COMMUNITY_Rate Limiting & Startup]]
- 7 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (5)]]
- 7 edges to [[_COMMUNITY_tests  CampCenter.IntegrationTests (2)]]
- 6 edges to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 5 edges to [[_COMMUNITY_Room Management]]
- 4 edges to [[_COMMUNITY_DTOs  Schedule (1)]]
- 3 edges to [[_COMMUNITY_Payment Gateway Integration Tests (1)]]
- 3 edges to [[_COMMUNITY_Przelewy24 Payment Client]]
- 3 edges to [[_COMMUNITY_Validator Unit Tests]]
- 2 edges to [[_COMMUNITY_Room Task Management (1)]]
- 2 edges to [[_COMMUNITY_Domain Exceptions]]
- 2 edges to [[_COMMUNITY_CampCenter.UnitTests  Validators]]
- 2 edges to [[_COMMUNITY_Public Booking Service (1)]]
- 2 edges to [[_COMMUNITY_DTOs  AdminPanel]]
- 1 edge to [[_COMMUNITY_Room Mix Calculator Tests]]
- 1 edge to [[_COMMUNITY_Camp Session Management]]
- 1 edge to [[_COMMUNITY_Admin Booking & Notifications (4)]]
- 1 edge to [[_COMMUNITY_Password Hashing (bcrypt)]]
- 1 edge to [[_COMMUNITY_Admin Booking & Notifications (5)]]
- 1 edge to [[_COMMUNITY_CampCenter.Application  Services (1)]]
- 1 edge to [[_COMMUNITY_Controllers  Admin]]
- 1 edge to [[_COMMUNITY_Room Task Management (2)]]
- 1 edge to [[_COMMUNITY_Integration Test Harness (1)]]
- 1 edge to [[_COMMUNITY_DTOs  Schedule (2)]]
- 1 edge to [[_COMMUNITY_CampCenter.Application  Services (4)]]

## Top bridge nodes
- [[CampCenter.Application.Interfaces]] - degree 53, connects to 11 communities
- [[CampCenter.Application.DTOs.Schedule]] - degree 20, connects to 7 communities
- [[CampCenter.Application.DTOs.AdminPanel]] - degree 16, connects to 6 communities
- [[RoomService.cs]] - degree 7, connects to 4 communities
- [[RoomTaskService.cs]] - degree 7, connects to 4 communities