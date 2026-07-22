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
- [[.Get()_2]] - code - src/CampCenter.Api/Controllers/Public/PublicSessionsController.cs
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
- [[CancellationToken_9]] - code
- [[CancellationToken_14]] - code
- [[CancellationToken_24]] - code
- [[Capacity]] - code
- [[Dictionary]] - code
- [[Dictionary_1]] - code
- [[Dictionary_2]] - code
- [[Fact_6]] - code
- [[Guid_6]] - code
- [[Guid_12]] - code
- [[HttpGet_6]] - code
- [[IActionResult_8]] - code
- [[IAvailabilityService]] - code - src/CampCenter.Application/Interfaces/IAvailabilityService.cs
- [[IAvailabilityService.cs]] - code - src/CampCenter.Application/Interfaces/IAvailabilityService.cs
- [[ICampSessionRepository_1]] - code
- [[IReadOnlyDictionary]] - code
- [[List_1]] - code
- [[List_6]] - code
- [[List_9]] - code
- [[PeopleCount]] - code
- [[ProducesResponseType_8]] - code
- [[PublicSessionDto]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[RoomMixCalculator]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[RoomMixCalculator.cs]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[RoomMixCalculatorTests]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[Task_9]] - code
- [[Task_13]] - code
- [[Task_23]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Room_Mix_Calculator_Tests
SORT file.name ASC
```

## Connections to other communities
- 5 edges to [[_COMMUNITY_Public Booking Service]]
- 3 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 2 edges to [[_COMMUNITY_Application DTO Namespaces]]
- 2 edges to [[_COMMUNITY_Room Management]]
- 1 edge to [[_COMMUNITY_Auth DTOs & Models]]
- 1 edge to [[_COMMUNITY_Admin Booking & Notifications]]
- 1 edge to [[_COMMUNITY_Przelewy24 Payment Client]]

## Top bridge nodes
- [[AvailabilityService]] - degree 7, connects to 3 communities
- [[IAvailabilityService]] - degree 6, connects to 2 communities
- [[IAvailabilityService.cs]] - degree 3, connects to 2 communities
- [[RoomMixCalculatorTests]] - degree 10, connects to 1 community
- [[.ValidateMix()]] - degree 8, connects to 1 community