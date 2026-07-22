---
type: community
cohesion: 0.05
members: 75
---

# Booking Persistence & Entities

**Cohesion:** 0.05 - loosely connected
**Members:** 75 nodes

## Members
- [[.AddAssignmentAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.AddAsync()_5]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.AddPaymentAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.Configure()_1]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/BookingConfiguration.cs
- [[.Configure()_2]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/BookingRoomAssignmentConfiguration.cs
- [[.Configure()_4]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/PaymentConfiguration.cs
- [[.Configure()_7]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/RoomTaskConfiguration.cs
- [[.CreateDbContext()]] - code - src/CampCenter.Infrastructure/Persistence/DesignTimeDbContextFactory.cs
- [[.Detach()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.Detach()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetBookedRoomIdsInRangeAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetByIdAsync()_6]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetByTokenHashAsync()_2]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetCompletedPaymentKindsAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetConfirmedEndedAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetExpiredPendingAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetPaymentByP24SessionIdAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetPaymentsAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.ListAsync()_6]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.ListLiveInRangeAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.ListUpcomingAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.OnModelCreating()]] - code - src/CampCenter.Infrastructure/Persistence/AppDbContext.cs
- [[.RemoveAssignment()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.RemoveAssignment()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.RemoveAssignments()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.RemoveAssignments()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.SaveChangesAsync()_7]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[AppDbContext]] - code - src/CampCenter.Infrastructure/Persistence/AppDbContext.cs
- [[Booking]] - code - src/CampCenter.Domain/Entities/Booking.cs
- [[Booking.cs]] - code - src/CampCenter.Domain/Entities/Booking.cs
- [[BookingCancelReason]] - code - src/CampCenter.Domain/Entities/Booking.cs
- [[BookingConfiguration]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/BookingConfiguration.cs
- [[BookingConfiguration.cs]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/BookingConfiguration.cs
- [[BookingRepository]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[BookingRoomAssignment]] - code - src/CampCenter.Domain/Entities/BookingRoomAssignment.cs
- [[BookingRoomAssignment.cs]] - code - src/CampCenter.Domain/Entities/BookingRoomAssignment.cs
- [[BookingRoomAssignmentConfiguration]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/BookingRoomAssignmentConfiguration.cs
- [[BookingRoomAssignmentConfiguration.cs]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/BookingRoomAssignmentConfiguration.cs
- [[BookingStatus]] - code - src/CampCenter.Domain/Entities/Booking.cs
- [[CampCenter.Infrastructure.Persistence.Configurations]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/AdminUserConfiguration.cs
- [[CancellationToken_40]] - code
- [[DateOnly_3]] - code
- [[DateOnly_4]] - code
- [[DateOnly_8]] - code
- [[DateTime_4]] - code
- [[DateTime_6]] - code
- [[DateTime_11]] - code
- [[DbContext]] - code
- [[DbSet]] - code
- [[DesignTimeDbContextFactory]] - code - src/CampCenter.Infrastructure/Persistence/DesignTimeDbContextFactory.cs
- [[DesignTimeDbContextFactory.cs]] - code - src/CampCenter.Infrastructure/Persistence/DesignTimeDbContextFactory.cs
- [[Dictionary_5]] - code
- [[EntityTypeBuilder_1]] - code
- [[EntityTypeBuilder_2]] - code
- [[EntityTypeBuilder_4]] - code
- [[EntityTypeBuilder_7]] - code
- [[Guid_18]] - code
- [[Guid_19]] - code
- [[Guid_21]] - code
- [[Guid_32]] - code
- [[IDesignTimeDbContextFactory]] - code
- [[IEntityTypeConfiguration]] - code
- [[IReadOnlyCollection_1]] - code
- [[List_12]] - code
- [[List_17]] - code
- [[ModelBuilder]] - code
- [[Payment]] - code - src/CampCenter.Domain/Entities/Payment.cs
- [[Payment.cs]] - code - src/CampCenter.Domain/Entities/Payment.cs
- [[PaymentConfiguration]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/PaymentConfiguration.cs
- [[PaymentConfiguration.cs]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/PaymentConfiguration.cs
- [[PaymentKind]] - code - src/CampCenter.Domain/Entities/Payment.cs
- [[PaymentStatus]] - code - src/CampCenter.Domain/Entities/Payment.cs
- [[RoomTaskConfiguration]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/RoomTaskConfiguration.cs
- [[RoomTaskConfiguration.cs]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/RoomTaskConfiguration.cs
- [[Task_39]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Booking_Persistence__Entities
SORT file.name ASC
```

## Connections to other communities
- 27 edges to [[_COMMUNITY_Admin Booking & Notifications]]
- 10 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 5 edges to [[_COMMUNITY_Room Task Management]]
- 5 edges to [[_COMMUNITY_Room Management]]
- 4 edges to [[_COMMUNITY_Public Booking Service]]
- 4 edges to [[_COMMUNITY_Room Closure Management]]
- 3 edges to [[_COMMUNITY_Admin User & Token Config]]
- 3 edges to [[_COMMUNITY_Refresh Token EF Config]]
- 2 edges to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 1 edge to [[_COMMUNITY_Admin User Repository]]
- 1 edge to [[_COMMUNITY_Refresh Token Repository]]

## Top bridge nodes
- [[AppDbContext]] - degree 20, connects to 8 communities
- [[CampCenter.Infrastructure.Persistence.Configurations]] - degree 8, connects to 4 communities
- [[IEntityTypeConfiguration]] - degree 8, connects to 4 communities
- [[Booking]] - degree 40, connects to 3 communities
- [[BookingRepository]] - degree 22, connects to 2 communities