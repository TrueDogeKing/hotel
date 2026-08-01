---
type: community
members: 28
---

# Public Booking Service (2)

**Members:** 28 nodes

## Members
- [[AdminUserRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs
- [[AppDbContext.cs]] - code - src/CampCenter.Infrastructure/Persistence/AppDbContext.cs
- [[BookingMaintenanceService.cs]] - code - src/CampCenter.Api/Background/BookingMaintenanceService.cs
- [[BookingMealTimeRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/BookingMealTimeRepository.cs
- [[BookingRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[CampCenter.Api.Background]] - code - src/CampCenter.Api/Background/BookingMaintenanceService.cs
- [[CampCenter.Domain.Entities]] - code - src/CampCenter.Domain/Entities/AdminUser.cs
- [[CampCenter.Domain.Repositories]] - code - src/CampCenter.Domain/Repositories/IAdminUserRepository.cs
- [[CampCenter.Infrastructure.Persistence]] - code - src/CampCenter.Infrastructure/Persistence/AppDbContext.cs
- [[CampCenter.Infrastructure.Repositories]] - code - src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs
- [[ClosureRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/ClosureRepository.cs
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
- [[RefreshTokenRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/RefreshTokenRepository.cs
- [[RoomCleaningRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/RoomCleaningRepository.cs
- [[RoomRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/RoomRepository.cs
- [[RoomTaskRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/RoomTaskRepository.cs
- [[ScheduleEntryRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/ScheduleEntryRepository.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Public_Booking_Service_2
SORT file.name ASC
```

## Connections to other communities
- 27 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 15 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 6 edges to [[_COMMUNITY_.CreateClient]]
- 5 edges to [[_COMMUNITY_CampCenter.Application  Services (2)]]
- 4 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (1)]]
- 4 edges to [[_COMMUNITY_Admin User & Token Config]]
- 3 edges to [[_COMMUNITY_Integration Test Harness (1)]]
- 3 edges to [[_COMMUNITY_CampCenter.UnitTests  Validators]]
- 3 edges to [[_COMMUNITY_Validator Unit Tests]]
- 3 edges to [[_COMMUNITY_Room Management]]
- 3 edges to [[_COMMUNITY_Room Task Management (1)]]
- 3 edges to [[_COMMUNITY_IEntityTypeConfiguration]]
- 3 edges to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (1)]]
- 2 edges to [[_COMMUNITY_CampCenter.Application.DTOs.Public]]
- 2 edges to [[_COMMUNITY_Refresh Token Repository]]
- 2 edges to [[_COMMUNITY_src  api (2)]]
- 2 edges to [[_COMMUNITY_EF Core Migrations (3)]]
- 1 edge to [[_COMMUNITY_WriteRequiresAdministratorHandler]]
- 1 edge to [[_COMMUNITY_Booking Maintenance Background Service]]
- 1 edge to [[_COMMUNITY_PasswordRules]]
- 1 edge to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 1 edge to [[_COMMUNITY_components  admin]]
- 1 edge to [[_COMMUNITY_Payment]]
- 1 edge to [[_COMMUNITY_JWT Token Service]]
- 1 edge to [[_COMMUNITY_BookingConfiguration]]
- 1 edge to [[_COMMUNITY_Persistence  Configurations]]
- 1 edge to [[_COMMUNITY_BookingRoomAssignmentConfiguration]]
- 1 edge to [[_COMMUNITY_PaymentConfiguration]]
- 1 edge to [[_COMMUNITY_RoomConfiguration]]
- 1 edge to [[_COMMUNITY_.CreateWorkerAsync]]
- 1 edge to [[_COMMUNITY_ScheduleEntryConfiguration]]
- 1 edge to [[_COMMUNITY_Integration Test Harness (2)]]
- 1 edge to [[_COMMUNITY_Auth Service & Tokens]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Services (2)]]
- 1 edge to [[_COMMUNITY_SmtpEmailSender]]
- 1 edge to [[_COMMUNITY_Payment_1]]
- 1 edge to [[_COMMUNITY_Rate Limiting & Startup]]
- 1 edge to [[_COMMUNITY_EF Core Migrations (1)]]
- 1 edge to [[_COMMUNITY_EF Core Migrations (5)]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Services (3)]]
- 1 edge to [[_COMMUNITY_Persistence  Migrations (9)]]
- 1 edge to [[_COMMUNITY_Persistence  Migrations (10)]]
- 1 edge to [[_COMMUNITY_20260728105506_PerGroupMealTimes.Designer.cs]]
- 1 edge to [[_COMMUNITY_Persistence  Migrations (13)]]
- 1 edge to [[_COMMUNITY_20260729224623_RoomCleanings.Designer.cs]]
- 1 edge to [[_COMMUNITY_20260730211855_AdminUserRole.Designer.cs]]
- 1 edge to [[_COMMUNITY_CampCenter.Domain  Repositories (1)]]
- 1 edge to [[_COMMUNITY_RoomCleaningRepository]]

## Top bridge nodes
- [[CampCenter.Domain.Entities]] - degree 81, connects to 28 communities
- [[CampCenter.Infrastructure.Persistence]] - degree 26, connects to 12 communities
- [[DependencyInjection.cs_1]] - degree 9, connects to 5 communities
- [[CampCenter.Domain.Repositories]] - degree 37, connects to 3 communities
- [[BookingMaintenanceService.cs]] - degree 6, connects to 3 communities