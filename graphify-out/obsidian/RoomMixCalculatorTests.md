---
source_file: "tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs"
type: "code"
community: "Room Mix Calculator Tests"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Mix_Calculator_Tests
---

# RoomMixCalculatorTests

## Context

_Source: `tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs` (defined near L5; showing L3–L50 of 100)._

```csharp
namespace CampCenter.UnitTests.Services;

public class RoomMixCalculatorTests
{
    [Fact]
    public void SuggestMix_UsesShrinkPass_ForRemainder()
    {
        // 301 people, plenty of 4s plus smaller rooms: 75×4 + 1×2 beats 76×4.
        var free = new Dictionary<int, int>
        {
            [4] = 100,
            [3] = 10,
            [2] = 10,
        };

        var mix = RoomMixCalculator.SuggestMix(301, free)!;

        Assert.Equal(75, mix[4]);
        Assert.Equal(1, mix[2]);
        Assert.False(mix.ContainsKey(3));
    }

    [Fact]
    public void SuggestMix_FallsBackToLargerRoom_WhenNoSmallFits()
    {
        // 5 people with only 4-person rooms → two rooms.
        var free = new Dictionary<int, int> { [4] = 10 };

        var mix = RoomMixCalculator.SuggestMix(5, free)!;

        Assert.Equal(2, mix[4]);
    }

    [Fact]
    public void SuggestMix_ReturnsNull_WhenCapacityInsufficient()
    {
        var free = new Dictionary<int, int> { [4] = 2, [2] = 1 };

        Assert.Null(RoomMixCalculator.SuggestMix(11, free));
    }

    [Fact]
    public void SuggestMix_ExactFit_NoShrink()
    {
        var free = new Dictionary<int, int> { [4] = 3, [2] = 5 };

        var mix = RoomMixCalculator.SuggestMix(12, free)!;

```

## Connections
- [[.DistributePeople_FillsAllButLastRoom()]] - `method` [EXTRACTED]
- [[.SuggestMix_ExactFit_NoShrink()]] - `method` [EXTRACTED]
- [[.SuggestMix_FallsBackToLargerRoom_WhenNoSmallFits()]] - `method` [EXTRACTED]
- [[.SuggestMix_ReturnsNull_WhenCapacityInsufficient()]] - `method` [EXTRACTED]
- [[.SuggestMix_UsesShrinkPass_ForRemainder()]] - `method` [EXTRACTED]
- [[.ValidateMix_Accepts_TightSelection()]] - `method` [EXTRACTED]
- [[.ValidateMix_RejectsInsufficientCoverage()]] - `method` [EXTRACTED]
- [[.ValidateMix_RejectsOverAvailability()]] - `method` [EXTRACTED]
- [[.ValidateMix_RejectsRedundantRoom()]] - `method` [EXTRACTED]
- [[RoomMixCalculatorTests.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Mix_Calculator_Tests