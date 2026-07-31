---
type: community
members: 22
---

# Room Mix Calculator Tests

**Members:** 22 nodes

## Members
- [[.DistributePeople()]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[.DistributePeople_FillsAllButLastRoom()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
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
- [[Capacity]] - code
- [[Dictionary_2]] - code
- [[Fact_11]] - code
- [[IReadOnlyDictionary_1]] - code
- [[List_14]] - code
- [[PeopleCount]] - code
- [[RoomMixCalculator]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[RoomMixCalculator.cs]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[RoomMixCalculatorTests]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Room_Mix_Calculator_Tests
SORT file.name ASC
```

## Connections to other communities
- 3 edges to [[_COMMUNITY_Admin Booking & Notifications (1)]]
- 2 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 2 edges to [[_COMMUNITY_Validator Unit Tests]]
- 2 edges to [[_COMMUNITY_Public Booking Service (1)]]

## Top bridge nodes
- [[.SuggestMix()]] - degree 10, connects to 2 communities
- [[.DistributePeople()]] - degree 8, connects to 2 communities
- [[.TotalCapacity()]] - degree 6, connects to 2 communities
- [[RoomMixCalculatorTests]] - degree 10, connects to 1 community
- [[.ValidateMix()]] - degree 8, connects to 1 community