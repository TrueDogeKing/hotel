---
type: community
cohesion: 0.16
members: 35
---

# Admin Booking & Notifications (1)

**Cohesion:** 0.16 - loosely connected
**Members:** 35 nodes

## Members
- [[.ApplyCancellationAsync()]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.BuildAssignmentsAsync()]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.CancelAsync()_1]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.ComputeHoldExpiry()]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.CreateAsync()_10]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.FinalDueDate()]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.GetAsync()_1]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.GetDashboardAsync()_1]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.GetOccupancyAsync()_1]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.GetOrThrowAsync()_1]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.ListAsync()_5]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.ParseStatus()]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.ReassignAsync()_1]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.SaveChangesAsync()_11]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.SetStatusAsync()_4]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.ToDto()_3]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.UpdateDietaryNotesAsync()_1]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[AdminBookingDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[AdminBookingService]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[BookingRoomAssignment_1]] - code
- [[BookingSettings_1]] - code
- [[CancellationToken_22]] - code
- [[DateOnly_18]] - code
- [[DateTime_13]] - code
- [[Guid_10]] - code
- [[IAvailabilityService_2]] - code
- [[IClosureRepository_2]] - code
- [[IEmailSender_1]] - code
- [[ILogger_2]] - code
- [[IRoomRepository_1]] - code
- [[IRoomTaskRepository_1]] - code
- [[ITokenService_1]] - code
- [[List_5]] - code
- [[PaymentKind_1]] - code
- [[Task_21]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Admin_Booking__Notifications_1
SORT file.name ASC
```

## Connections to other communities
- 17 edges to [[_COMMUNITY_Admin Booking & Notifications (2)]]
- 13 edges to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 7 edges to [[_COMMUNITY_Booking Persistence & Entities (2)]]
- 4 edges to [[_COMMUNITY_Public Booking Service (2)]]
- 3 edges to [[_COMMUNITY_DTOs  Schedule (1)]]
- 2 edges to [[_COMMUNITY_Admin Booking & Notifications (3)]]
- 1 edge to [[_COMMUNITY_Room Mix Calculator Tests]]
- 1 edge to [[_COMMUNITY_Rate Limiting & Startup]]

## Top bridge nodes
- [[AdminBookingService]] - degree 28, connects to 4 communities
- [[.CreateAsync()_10]] - degree 13, connects to 4 communities
- [[.SaveChangesAsync()_11]] - degree 12, connects to 3 communities
- [[.SetStatusAsync()_4]] - degree 14, connects to 2 communities
- [[.GetOrThrowAsync()_1]] - degree 11, connects to 2 communities