---
type: community
cohesion: 0.23
members: 20
---

# Domain & Infra Namespaces

**Cohesion:** 0.23 - loosely connected
**Members:** 20 nodes

## Members
- [[AdminUserRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs
- [[AppDbContext.cs]] - code - src/CampCenter.Infrastructure/Persistence/AppDbContext.cs
- [[BookingMaintenanceService.cs]] - code - src/CampCenter.Api/Background/BookingMaintenanceService.cs
- [[BookingRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[CampCenter.Api.Background]] - code - src/CampCenter.Api/Background/BookingMaintenanceService.cs
- [[CampCenter.Domain.Entities]] - code - src/CampCenter.Domain/Entities/AdminUser.cs
- [[CampCenter.Domain.Repositories]] - code - src/CampCenter.Domain/Repositories/IAdminUserRepository.cs
- [[CampCenter.Infrastructure.Persistence]] - code - src/CampCenter.Infrastructure/Persistence/AppDbContext.cs
- [[CampCenter.Infrastructure.Repositories]] - code - src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs
- [[ClosureRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/ClosureRepository.cs
- [[DependencyInjection.cs_1]] - code - src/CampCenter.Infrastructure/DependencyInjection.cs
- [[IAdminUserRepository.cs]] - code - src/CampCenter.Domain/Repositories/IAdminUserRepository.cs
- [[IBookingRepository.cs]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[IClosureRepository.cs]] - code - src/CampCenter.Domain/Repositories/IClosureRepository.cs
- [[IRefreshTokenRepository.cs]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[IRoomRepository.cs]] - code - src/CampCenter.Domain/Repositories/IRoomRepository.cs
- [[IRoomTaskRepository.cs]] - code - src/CampCenter.Domain/Repositories/IRoomTaskRepository.cs
- [[RefreshTokenRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/RefreshTokenRepository.cs
- [[RoomRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/RoomRepository.cs
- [[RoomTaskRepository.cs]] - code - src/CampCenter.Infrastructure/Repositories/RoomTaskRepository.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Domain__Infra_Namespaces
SORT file.name ASC
```

## Connections to other communities
- 11 edges to [[_COMMUNITY_Auth DTOs & Models]]
- 10 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 10 edges to [[_COMMUNITY_Booking Persistence & Entities]]
- 6 edges to [[_COMMUNITY_Rate Limiting & Startup]]
- 4 edges to [[_COMMUNITY_Room Closure Management]]
- 4 edges to [[_COMMUNITY_Room Management]]
- 3 edges to [[_COMMUNITY_Application DTO Namespaces]]
- 3 edges to [[_COMMUNITY_Room Task Management]]
- 3 edges to [[_COMMUNITY_EF Core Migrations]]
- 2 edges to [[_COMMUNITY_Admin User & Token Config]]
- 2 edges to [[_COMMUNITY_Refresh Token EF Config]]
- 2 edges to [[_COMMUNITY_Admin Booking & Notifications]]
- 1 edge to [[_COMMUNITY_Booking Maintenance Background Service]]
- 1 edge to [[_COMMUNITY_Domain Exceptions]]
- 1 edge to [[_COMMUNITY_Admin User Repository Contract]]
- 1 edge to [[_COMMUNITY_Refresh Token Contract]]
- 1 edge to [[_COMMUNITY_Przelewy24 Payment Client]]
- 1 edge to [[_COMMUNITY_Infrastructure DI Registration]]
- 1 edge to [[_COMMUNITY_Admin User Repository]]
- 1 edge to [[_COMMUNITY_Refresh Token Repository]]

## Top bridge nodes
- [[CampCenter.Domain.Entities]] - degree 45, connects to 10 communities
- [[DependencyInjection.cs_1]] - degree 9, connects to 5 communities
- [[CampCenter.Domain.Repositories]] - degree 22, connects to 3 communities
- [[CampCenter.Infrastructure.Persistence]] - degree 14, connects to 3 communities
- [[BookingMaintenanceService.cs]] - degree 6, connects to 3 communities