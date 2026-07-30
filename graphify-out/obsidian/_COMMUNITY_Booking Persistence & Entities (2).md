---
type: community
cohesion: 0.12
members: 19
---

# Booking Persistence & Entities (2)

**Cohesion:** 0.12 - loosely connected
**Members:** 19 nodes

## Members
- [[.Configure()_7]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/BookingConfiguration.cs
- [[.Detach()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.RemoveAssignments()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.ToCalendarDto()]] - code - src/CampCenter.Application/Services/ScheduleService.cs
- [[Booking]] - code - src/CampCenter.Domain/Entities/Booking.cs
- [[Booking.cs]] - code - src/CampCenter.Domain/Entities/Booking.cs
- [[BookingCancelReason]] - code - src/CampCenter.Domain/Entities/Booking.cs
- [[BookingConfiguration]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/BookingConfiguration.cs
- [[BookingConfiguration.cs]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/BookingConfiguration.cs
- [[BookingRoomAssignment_2]] - code
- [[BookingStatus_1]] - code - src/CampCenter.Domain/Entities/Booking.cs
- [[BookingStatuses_1]] - code - src/CampCenter.Domain/Entities/BookingStatuses.cs
- [[BookingStatuses.cs]] - code - src/CampCenter.Domain/Entities/BookingStatuses.cs
- [[DateOnly_3]] - code
- [[DateTime_4]] - code
- [[EntityTypeBuilder_1]] - code
- [[Guid_18]] - code
- [[List_12]] - code
- [[ScheduleCalendarBookingDto]] - code - src/CampCenter.Application/DTOs/Schedule/ScheduleDtos.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Booking_Persistence__Entities_2
SORT file.name ASC
```

## Connections to other communities
- 14 edges to [[_COMMUNITY_Booking Persistence & Entities (1)]]
- 11 edges to [[_COMMUNITY_Admin Booking & Notifications (2)]]
- 10 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (3)]]
- 10 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (2)]]
- 7 edges to [[_COMMUNITY_Admin Booking & Notifications (1)]]
- 6 edges to [[_COMMUNITY_CampCenter.Application  Services (2)]]
- 5 edges to [[_COMMUNITY_Public Booking Service (2)]]
- 3 edges to [[_COMMUNITY_Admin Booking & Notifications (4)]]
- 3 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 2 edges to [[_COMMUNITY_Persistence  Configurations]]
- 2 edges to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 2 edges to [[_COMMUNITY_CampCenter.Domain  Repositories (2)]]
- 1 edge to [[_COMMUNITY_Booking Persistence & Entities (3)]]
- 1 edge to [[_COMMUNITY_Room Task Management (1)]]
- 1 edge to [[_COMMUNITY_DTOs  Schedule (1)]]
- 1 edge to [[_COMMUNITY_CampCenter.Domain  Repositories (1)]]
- 1 edge to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (1)]]

## Top bridge nodes
- [[Booking]] - degree 77, connects to 13 communities
- [[BookingStatus_1]] - degree 10, connects to 4 communities
- [[BookingConfiguration.cs]] - degree 3, connects to 2 communities
- [[Booking.cs]] - degree 4, connects to 1 community
- [[BookingConfiguration]] - degree 4, connects to 1 community