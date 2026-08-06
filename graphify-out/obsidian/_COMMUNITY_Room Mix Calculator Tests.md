---
type: community
members: 45
---

# Room Mix Calculator Tests

**Members:** 45 nodes

## Members
- [[.DistributePeople()]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[.DistributePeople_FillsAllButLastRoom()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.DistributePeople_HandlesEachHalfSeparately()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorSplitTests.cs
- [[.Free()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorSplitTests.cs
- [[.Merge()]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[.NoSupervisors_BehavesExactlyLikeTheOrdinaryMix()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorSplitTests.cs
- [[.ReturnsNull_WhenOnlyOneRoomIsFree_AndBothCohortsNeedIt()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorSplitTests.cs
- [[.ReturnsNull_WhenTheKadraCannotBeSeparated()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorSplitTests.cs
- [[.StaffOnlyBooking_LeavesNoCamperRooms()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorSplitTests.cs
- [[.Subtract()]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[.SuggestMix()]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[.SuggestMixSmallestFirst()]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[.SuggestMix_ExactFit_NoShrink()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.SuggestMix_FallsBackToLargerRoom_WhenNoSmallFits()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.SuggestMix_ReturnsNull_WhenCapacityInsufficient()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.SuggestMix_UsesShrinkPass_ForRemainder()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.SuggestSplitMix()]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[.Supervisors_AndCampers_NeverShareARoom()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorSplitTests.cs
- [[.Supervisors_TakeALargeRoom_WhenThatIsAllThereIs()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorSplitTests.cs
- [[.Supervisors_TakeTheSmallestRoomsThatHoldThem()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorSplitTests.cs
- [[.TotalCapacity()]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[.ValidateMix()]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[.ValidateMix_Accepts_TightSelection()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.ValidateMix_RejectsInsufficientCoverage()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.ValidateMix_RejectsOverAvailability()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.ValidateMix_RejectsRedundantRoom()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[.ValidateSplitMix()]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[.ValidateSplitMix_AcceptsAKadraRoom_ThatLooksRedundantAgainstTheTotal()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorSplitTests.cs
- [[.ValidateSplitMix_RejectsAHalfThatCannotHoldItsCohort()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorSplitTests.cs
- [[.ValidateSplitMix_RejectsHalvesThatJointlyOverclaimARoomType()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorSplitTests.cs
- [[.ValidateSplitMix_RejectsKadraRooms_ForAGroupWithNoKadra()]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorSplitTests.cs
- [[Capacity]] - code
- [[Dictionary_3]] - code
- [[Dictionary_12]] - code
- [[Fact_13]] - code
- [[Fact_14]] - code
- [[IReadOnlyDictionary_1]] - code
- [[List_14]] - code
- [[PeopleCount]] - code
- [[RoomMixCalculator]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[RoomMixCalculator.cs]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs
- [[RoomMixCalculatorSplitTests]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorSplitTests.cs
- [[RoomMixCalculatorTests]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs
- [[SplitMix]] - code
- [[SplitMix_1]] - code - src/CampCenter.Application/Services/RoomMixCalculator.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Room_Mix_Calculator_Tests
SORT file.name ASC
```

## Connections to other communities
- 5 edges to [[_COMMUNITY_Admin Booking & Notifications (1)]]
- 3 edges to [[_COMMUNITY_Exception]]
- 2 edges to [[_COMMUNITY_Room Management]]
- 2 edges to [[_COMMUNITY_Public Booking Service (1)]]

## Top bridge nodes
- [[.SuggestSplitMix()]] - degree 18, connects to 2 communities
- [[.DistributePeople()]] - degree 9, connects to 2 communities
- [[.TotalCapacity()]] - degree 8, connects to 2 communities
- [[RoomMixCalculatorSplitTests]] - degree 14, connects to 1 community
- [[RoomMixCalculatorTests]] - degree 10, connects to 1 community