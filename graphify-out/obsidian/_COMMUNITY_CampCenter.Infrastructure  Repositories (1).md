---
type: community
cohesion: 0.10
members: 40
---

# CampCenter.Infrastructure / Repositories (1)

**Cohesion:** 0.10 - loosely connected
**Members:** 40 nodes

## Members
- [[.AddAsync()_17]] - code - src/CampCenter.Infrastructure/Repositories/ScheduleEntryRepository.cs
- [[.AddRangeAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/ScheduleEntryRepository.cs
- [[.CountByDateAndKindAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/ScheduleEntryRepository.cs
- [[.CreateDbContext()]] - code - src/CampCenter.Infrastructure/Persistence/DesignTimeDbContextFactory.cs
- [[.GetByIdAsync()_13]] - code - src/CampCenter.Infrastructure/Repositories/ScheduleEntryRepository.cs
- [[.ListForBookingAsync()_3]] - code - src/CampCenter.Infrastructure/Repositories/ScheduleEntryRepository.cs
- [[.ListForDateAsync()_3]] - code - src/CampCenter.Infrastructure/Repositories/ScheduleEntryRepository.cs
- [[.ListFullySuppressedSlotsAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/ScheduleEntryRepository.cs
- [[.ListGeneratedSlotsAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/ScheduleEntryRepository.cs
- [[.ListLocationsAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/ScheduleEntryRepository.cs
- [[.ListVisibleSlotSpansAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/ScheduleEntryRepository.cs
- [[.OnModelCreating()]] - code - src/CampCenter.Infrastructure/Persistence/AppDbContext.cs
- [[.Remove()_11]] - code - src/CampCenter.Infrastructure/Repositories/ScheduleEntryRepository.cs
- [[.SaveChangesAsync()_19]] - code - src/CampCenter.Infrastructure/Repositories/ScheduleEntryRepository.cs
- [[AdminUser_1]] - code
- [[AppDbContext]] - code - src/CampCenter.Infrastructure/Persistence/AppDbContext.cs
- [[BookingId_1]] - code
- [[BookingRoomAssignment_4]] - code
- [[CancellationToken_67]] - code
- [[Count_2]] - code
- [[Date_2]] - code
- [[DateOnly_29]] - code
- [[DbContext]] - code
- [[DbSet]] - code
- [[DesignTimeDbContextFactory]] - code - src/CampCenter.Infrastructure/Persistence/DesignTimeDbContextFactory.cs
- [[DesignTimeDbContextFactory.cs]] - code - src/CampCenter.Infrastructure/Persistence/DesignTimeDbContextFactory.cs
- [[End_2]] - code
- [[Guid_61]] - code
- [[IDesignTimeDbContextFactory]] - code
- [[IReadOnlyCollection_6]] - code
- [[Kind_1]] - code
- [[List_38]] - code
- [[MealTimeDefaultId_1]] - code
- [[ModelBuilder]] - code
- [[Payment_2]] - code
- [[RefreshToken_1]] - code
- [[ScheduleEntryRepository]] - code - src/CampCenter.Infrastructure/Repositories/ScheduleEntryRepository.cs
- [[Start_2]] - code
- [[Task_72]] - code
- [[TimeOnly_8]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/CampCenterInfrastructure_/_Repositories_1
SORT file.name ASC
```

## Connections to other communities
- 9 edges to [[_COMMUNITY_CampCenter.Domain  Repositories (1)]]
- 3 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 2 edges to [[_COMMUNITY_CampCenter.Domain  Repositories (2)]]
- 1 edge to [[_COMMUNITY_Admin User & Token Config]]
- 1 edge to [[_COMMUNITY_Room Closure Management]]
- 1 edge to [[_COMMUNITY_Refresh Token Repository]]
- 1 edge to [[_COMMUNITY_Room Management]]
- 1 edge to [[_COMMUNITY_Room Task Management (1)]]
- 1 edge to [[_COMMUNITY_Booking Persistence & Entities (2)]]
- 1 edge to [[_COMMUNITY_CampCenter.Application  Services (3)]]
- 1 edge to [[_COMMUNITY_CampCenter.Domain  Entities]]
- 1 edge to [[_COMMUNITY_Booking Persistence & Entities (1)]]
- 1 edge to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (2)]]
- 1 edge to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (3)]]

## Top bridge nodes
- [[AppDbContext]] - degree 25, connects to 14 communities
- [[ScheduleEntryRepository]] - degree 15, connects to 2 communities
- [[.CountByDateAndKindAsync()_1]] - degree 11, connects to 1 community
- [[.ListForBookingAsync()_3]] - degree 6, connects to 1 community
- [[.ListForDateAsync()_3]] - degree 6, connects to 1 community