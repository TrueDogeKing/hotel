---
type: community
cohesion: 0.14
members: 34
---

# Domain & Infra Namespaces

**Cohesion:** 0.14 - loosely connected
**Members:** 34 nodes

## Members
- [[AdminUserRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs
- [[AppDbContext.cs]] - code - src/CampCenter.Infrastructure/Persistence/AppDbContext.cs
- [[BookingMaintenanceService.cs]] - code - src/CampCenter.Api/Background/BookingMaintenanceService.cs
- [[BookingMealTime.cs]] - code - src/CampCenter.Domain/Entities/BookingMealTime.cs
- [[BookingMealTimeRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/BookingMealTimeRepository.cs
- [[BookingRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[BookingService.cs]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[CampCenter.Api.Background]] - code - src/CampCenter.Api/Background/BookingMaintenanceService.cs
- [[CampCenter.Domain.Entities]] - code - src/CampCenter.Domain/Entities/AdminUser.cs
- [[CampCenter.Domain.Repositories]] - code - src/CampCenter.Domain/Repositories/IAdminUserRepository.cs
- [[CampCenter.Infrastructure.Persistence]] - code - src/CampCenter.Infrastructure/Persistence/AppDbContext.cs
- [[CampCenter.Infrastructure.Repositories]] - code - src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs
- [[CampSessionService.cs]] - code - src/CampCenter.Application/Services/CampSessionService.cs
- [[ClosureRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/ClosureRepository.cs
- [[ClosureService.cs]] - code - src/CampCenter.Application/Services/ClosureService.cs
- [[DependencyInjection.cs_1]] - code - src/CampCenter.Infrastructure/DependencyInjection.cs
- [[IAdminUserRepository.cs]] - code - src/CampCenter.Domain/Repositories/IAdminUserRepository.cs
- [[IBookingMealTimeRepository.cs]] - code - src/CampCenter.Domain/Repositories/IBookingMealTimeRepository.cs
- [[IBookingRepository.cs]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[IClosureRepository.cs]] - code - src/CampCenter.Domain/Repositories/IClosureRepository.cs
- [[IMealTimeDefaultRepository.cs]] - code - src/CampCenter.Domain/Repositories/IMealTimeDefaultRepository.cs
- [[IRefreshTokenRepository.cs]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[IRoomCleaningRepository.cs]] - code - src/CampCenter.Domain/Repositories/IRoomCleaningRepository.cs
- [[IRoomRepository.cs]] - code - src/CampCenter.Domain/Repositories/IRoomRepository.cs
- [[IRoomTaskRepository.cs]] - code - src/CampCenter.Domain/Repositories/IRoomTaskRepository.cs
- [[IScheduleEntryRepository.cs]] - code - src/CampCenter.Domain/Repositories/IScheduleEntryRepository.cs
- [[MealTimeDefaultRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/MealTimeDefaultRepository.cs
- [[MealTimeService.cs]] - code - src/CampCenter.Application/Services/MealTimeService.cs
- [[PaymentService.cs]] - code - src/CampCenter.Application/Services/PaymentService.cs
- [[RefreshTokenRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/RefreshTokenRepository.cs
- [[RoomCleaningRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/RoomCleaningRepository.cs
- [[RoomRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/RoomRepository.cs
- [[RoomTaskRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/RoomTaskRepository.cs
- [[ScheduleEntryRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/ScheduleEntryRepository.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Domain__Infra_Namespaces
SORT file.name ASC
```

## Connections to other communities
- 20 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 14 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (5)]]
- 10 edges to [[_COMMUNITY_Rate Limiting & Startup]]
- 5 edges to [[_COMMUNITY_tests  CampCenter.IntegrationTests (2)]]
- 4 edges to [[_COMMUNITY_Admin User & Token Config]]
- 4 edges to [[_COMMUNITY_Room Closure Management]]
- 4 edges to [[_COMMUNITY_Room Management]]
- 4 edges to [[_COMMUNITY_Room Task Management (1)]]
- 3 edges to [[_COMMUNITY_Booking Persistence & Entities (2)]]
- 3 edges to [[_COMMUNITY_CampCenter.Application  Services (3)]]
- 3 edges to [[_COMMUNITY_Refresh Token Repository]]
- 3 edges to [[_COMMUNITY_Persistence  Configurations]]
- 3 edges to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (1)]]
- 3 edges to [[_COMMUNITY_CampCenter.Domain  Repositories (2)]]
- 2 edges to [[_COMMUNITY_Camp Session Management]]
- 2 edges to [[_COMMUNITY_Validator Unit Tests]]
- 2 edges to [[_COMMUNITY_Booking Persistence & Entities (4)]]
- 2 edges to [[_COMMUNITY_Booking Persistence & Entities (3)]]
- 2 edges to [[_COMMUNITY_CampCenter.Domain  Entities]]
- 2 edges to [[_COMMUNITY_CampCenter.Domain  Repositories (1)]]
- 2 edges to [[_COMMUNITY_EF Core Migrations (1)]]
- 1 edge to [[_COMMUNITY_Domain Exceptions]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Services (3)]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Services (1)]]
- 1 edge to [[_COMMUNITY_DTOs  Schedule (1)]]
- 1 edge to [[_COMMUNITY_Auth Service & Tokens]]
- 1 edge to [[_COMMUNITY_EF Core Migrations (5)]]
- 1 edge to [[_COMMUNITY_Booking Maintenance Background Service]]
- 1 edge to [[_COMMUNITY_Public Booking Service (2)]]
- 1 edge to [[_COMMUNITY_CampCenter.Application  Services (1)]]
- 1 edge to [[_COMMUNITY_Admin Booking & Notifications (3)]]
- 1 edge to [[_COMMUNITY_Admin Booking & Notifications (2)]]
- 1 edge to [[_COMMUNITY_CampCenter.Domain  Repositories (3)]]
- 1 edge to [[_COMMUNITY_Infrastructure DI Registration]]
- 1 edge to [[_COMMUNITY_Persistence  Migrations (8)]]
- 1 edge to [[_COMMUNITY_Persistence  Migrations (9)]]
- 1 edge to [[_COMMUNITY_Persistence  Migrations (10)]]
- 1 edge to [[_COMMUNITY_Persistence  Migrations (11)]]
- 1 edge to [[_COMMUNITY_Persistence  Migrations (12)]]
- 1 edge to [[_COMMUNITY_Persistence  Migrations (13)]]
- 1 edge to [[_COMMUNITY_EF Core Migrations (3)]]
- 1 edge to [[_COMMUNITY_Booking Persistence & Entities (1)]]
- 1 edge to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (2)]]
- 1 edge to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (3)]]

## Top bridge nodes
- [[CampCenter.Domain.Entities]] - degree 75, connects to 20 communities
- [[CampCenter.Infrastructure.Persistence]] - degree 25, connects to 11 communities
- [[BookingService.cs]] - degree 7, connects to 4 communities
- [[CampSessionService.cs]] - degree 7, connects to 4 communities
- [[ClosureService.cs]] - degree 6, connects to 4 communities