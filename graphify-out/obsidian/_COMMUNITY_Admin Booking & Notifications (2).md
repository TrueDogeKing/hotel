---
type: community
cohesion: 0.19
members: 29
---

# Admin Booking & Notifications (2)

**Cohesion:** 0.19 - loosely connected
**Members:** 29 nodes

## Members
- [[.AddAssignmentAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.AddAsync()_9]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.AddPaymentAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.GetBookedRoomIdsInRangeAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.GetByIdAsync()_8]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.GetByTokenHashAsync()_2]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.GetCompletedPaymentKindsAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.GetConfirmedEndedAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.GetExpiredPendingAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.GetPaymentByP24SessionIdAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.GetPaymentsAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.ListAsync()_6]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.ListLiveChangingOverAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.ListLiveInRangeAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.ListLivePresentInAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.ListUpcomingAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.RemoveAssignment()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[BookingRoomAssignment_3]] - code
- [[CancellationToken_31]] - code
- [[DateOnly_6]] - code
- [[DateTime_9]] - code
- [[Dictionary_3]] - code
- [[Guid_26]] - code
- [[IBookingRepository]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[IReadOnlyCollection]] - code
- [[List_13]] - code
- [[Payment_1]] - code
- [[PaymentKind_2]] - code
- [[Task_30]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Admin_Booking__Notifications_2
SORT file.name ASC
```

## Connections to other communities
- 17 edges to [[_COMMUNITY_Admin Booking & Notifications (1)]]
- 11 edges to [[_COMMUNITY_Booking Persistence & Entities (2)]]
- 6 edges to [[_COMMUNITY_Public Booking Service (2)]]
- 6 edges to [[_COMMUNITY_CampCenter.Application  Services (2)]]
- 5 edges to [[_COMMUNITY_Admin Booking & Notifications (3)]]
- 3 edges to [[_COMMUNITY_CampCenter.Application  Services (4)]]
- 2 edges to [[_COMMUNITY_CampCenter.Application  Services (1)]]
- 2 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (2)]]
- 1 edge to [[_COMMUNITY_Room Mix Calculator Tests]]
- 1 edge to [[_COMMUNITY_CampCenter.Domain  Repositories (2)]]
- 1 edge to [[_COMMUNITY_Domain & Infra Namespaces]]
- 1 edge to [[_COMMUNITY_Booking Persistence & Entities (1)]]

## Top bridge nodes
- [[IBookingRepository]] - degree 30, connects to 10 communities
- [[.ListLivePresentInAsync()]] - degree 11, connects to 3 communities
- [[.ListLiveChangingOverAsync()]] - degree 9, connects to 3 communities
- [[.GetByIdAsync()_8]] - degree 7, connects to 3 communities
- [[.AddAsync()_9]] - degree 6, connects to 3 communities