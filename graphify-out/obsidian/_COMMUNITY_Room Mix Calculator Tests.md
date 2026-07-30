---
type: community
cohesion: 0.07
members: 47
---

# Room Mix Calculator Tests

**Cohesion:** 0.07 - loosely connected
**Members:** 47 nodes

## Members
- [[.DistributePeople()]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[.DistributePeople_FillsAllButLastRoom()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.GetFreeRoomsByCapacityAsync()]] - code - src/CampCenter.Application/Interfaces/IAvailabilityService.cs
- [[.GetFreeRoomsByCapacityAsync()_1]] - code - src/CampCenter.Application/Services/AvailabilityService.cs
- [[.GetPublicSessionsAsync()]] - code - src/CampCenter.Application/Interfaces/IAvailabilityService.cs
- [[.GetPublicSessionsAsync()_1]] - code - src/CampCenter.Application/Services/AvailabilityService.cs
- [[.SuggestMix()]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[.SuggestMix_ExactFit_NoShrink()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.SuggestMix_FallsBackToLargerRoom_WhenNoSmallFits()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.SuggestMix_ReturnsNull_WhenCapacityInsufficient()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.SuggestMix_UsesShrinkPass_ForRemainder()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.TotalCapacity()]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[.ValidateMix()]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[.ValidateMix_Accepts_TightSelection()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.ValidateMix_RejectsInsufficientCoverage()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.ValidateMix_RejectsOverAvailability()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.ValidateMix_RejectsRedundantRoom()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[AvailabilityService]] - code - src/CampCenter.Application/Services/AvailabilityService.cs
- [[BookingDetailsDto]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[BookingPaymentDto]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[CampCenter.Application.DTOs.Public]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[CancellationToken_14]] - code
- [[CancellationToken_24]] - code
- [[Capacity]] - code
- [[CreateBookingRequestDto]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[CreateBookingResponseDto]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[Dictionary]] - code
- [[Dictionary_1]] - code
- [[Dictionary_2]] - code
- [[Fact_6]] - code
- [[Guid_6]] - code
- [[Guid_12]] - code
- [[IAvailabilityService]] - code - src/CampCenter.Application/Interfaces/IAvailabilityService.cs
- [[IAvailabilityService.cs]] - code - src/CampCenter.Application/Interfaces/IAvailabilityService.cs
- [[ICampSessionRepository_1]] - code
- [[IReadOnlyDictionary]] - code
- [[List_1]] - code
- [[List_6]] - code
- [[List_9]] - code
- [[PeopleCount]] - code
- [[PublicDtos.cs]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[PublicSessionDto]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[RoomMixCalculator]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[RoomMixCalculator.cs]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[RoomMixCalculatorTests]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[Task_13]] - code
- [[Task_23]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Room_Mix_Calculator_Tests
SORT file.name ASC
```

## Connections to other communities
- 4 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (5)]]
- 2 edges to [[_COMMUNITY_Room Management]]
- 1 edge to [[_COMMUNITY_Payment Gateway Integration Tests (1)]]
- 1 edge to [[_COMMUNITY_Application Namespaces & DTOs]]
- 1 edge to [[_COMMUNITY_Admin Booking & Notifications (2)]]
- 1 edge to [[_COMMUNITY_Admin Booking & Notifications (1)]]

## Top bridge nodes
- [[AvailabilityService]] - degree 7, connects to 3 communities
- [[CampCenter.Application.DTOs.Public]] - degree 4, connects to 2 communities
- [[RoomMixCalculatorTests]] - degree 10, connects to 1 community
- [[.GetFreeRoomsByCapacityAsync()_1]] - degree 7, connects to 1 community
- [[IReadOnlyDictionary]] - degree 5, connects to 1 community