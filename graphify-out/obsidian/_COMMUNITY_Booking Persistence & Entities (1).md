---
type: community
cohesion: 0.19
members: 30
---

# Booking Persistence & Entities (1)

**Cohesion:** 0.19 - loosely connected
**Members:** 30 nodes

## Members
- [[.AddAssignmentAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.AddAsync()_14]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.AddPaymentAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetBookedRoomIdsInRangeAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetByIdAsync()_11]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetByTokenHashAsync()_3]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetCompletedPaymentKindsAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetConfirmedEndedAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetExpiredPendingAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetPaymentByP24SessionIdAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.GetPaymentsAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.ListAsync()_7]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.ListLiveChangingOverAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.ListLiveInRangeAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.ListLivePresentInAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.ListUpcomingAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.RemoveAssignment()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.SaveChangesAsync()_16]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[BookingRepository]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[BookingRoomAssignment_5]] - code
- [[CancellationToken_40]] - code
- [[DateOnly_8]] - code
- [[DateTime_11]] - code
- [[Dictionary_5]] - code
- [[Guid_32]] - code
- [[IReadOnlyCollection_1]] - code
- [[List_17]] - code
- [[Payment_3]] - code
- [[PaymentKind_3]] - code
- [[Task_39]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Booking_Persistence__Entities_1
SORT file.name ASC
```

## Connections to other communities
- 14 edges to [[_COMMUNITY_Booking Persistence & Entities (2)]]
- 1 edge to [[_COMMUNITY_Admin Booking & Notifications (2)]]
- 1 edge to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (1)]]
- 1 edge to [[_COMMUNITY_Domain & Infra Namespaces]]

## Top bridge nodes
- [[BookingRepository]] - degree 24, connects to 4 communities
- [[.AddAsync()_14]] - degree 6, connects to 1 community
- [[.GetConfirmedEndedAsync()_1]] - degree 6, connects to 1 community
- [[.GetExpiredPendingAsync()_1]] - degree 6, connects to 1 community
- [[.ListAsync()_7]] - degree 6, connects to 1 community