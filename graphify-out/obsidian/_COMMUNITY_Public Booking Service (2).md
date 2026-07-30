---
type: community
cohesion: 0.15
members: 28
---

# Public Booking Service (2)

**Cohesion:** 0.15 - loosely connected
**Members:** 28 nodes

## Members
- [[.AssignRooms()]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.CancelByTokenAsync()_1]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.CreateAsync()_11]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.Detach()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.FinalDueDate()_1]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.FindByTokenAsync()]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.GetByTokenAsync()_1]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.GetScheduleByTokenAsync()_1]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.ManageUrl()]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.PickRoomsAsync()]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.RemoveAssignments()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.SendSafelyAsync()]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.TryCreateAsync()]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[BookingDetailsDto_2]] - code
- [[BookingService]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[BookingSettings_2]] - code
- [[CancellationToken_25]] - code
- [[CreateBookingRequestDto_3]] - code
- [[CreateBookingResponseDto_2]] - code
- [[DateOnly]] - code
- [[EmailMessage_1]] - code
- [[IAvailabilityService_3]] - code
- [[IEmailSender_2]] - code
- [[ILogger_3]] - code
- [[IRoomRepository_2]] - code
- [[ITokenService_2]] - code
- [[List_7]] - code
- [[Task_24]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Public_Booking_Service_2
SORT file.name ASC
```

## Connections to other communities
- 6 edges to [[_COMMUNITY_Admin Booking & Notifications (2)]]
- 5 edges to [[_COMMUNITY_Booking Persistence & Entities (2)]]
- 4 edges to [[_COMMUNITY_Admin Booking & Notifications (1)]]
- 2 edges to [[_COMMUNITY_Public Booking Service (1)]]
- 2 edges to [[_COMMUNITY_CampCenter.Domain  Repositories (1)]]
- 1 edge to [[_COMMUNITY_Domain & Infra Namespaces]]
- 1 edge to [[_COMMUNITY_CampCenter.Application  Services (2)]]

## Top bridge nodes
- [[BookingService]] - degree 21, connects to 4 communities
- [[.GetScheduleByTokenAsync()_1]] - degree 7, connects to 3 communities
- [[.Detach()]] - degree 4, connects to 3 communities
- [[.RemoveAssignments()]] - degree 4, connects to 3 communities
- [[.TryCreateAsync()]] - degree 14, connects to 2 communities