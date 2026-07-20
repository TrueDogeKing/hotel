using CampCenter.Application.Services;

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

        Assert.Equal(3, mix[4]);
        Assert.False(mix.ContainsKey(2));
    }

    [Fact]
    public void ValidateMix_Accepts_TightSelection()
    {
        var free = new Dictionary<int, int> { [4] = 10, [2] = 10 };
        var counts = new Dictionary<int, int> { [4] = 2, [2] = 1 }; // 10 beds for 9 people

        Assert.Null(RoomMixCalculator.ValidateMix(9, counts, free));
    }

    [Fact]
    public void ValidateMix_RejectsRedundantRoom()
    {
        var free = new Dictionary<int, int> { [4] = 10, [2] = 10 };
        var counts = new Dictionary<int, int> { [4] = 3 }; // 12 beds for 5 people — one 4 is redundant

        Assert.Equal("mix-redundant-room", RoomMixCalculator.ValidateMix(5, counts, free));
    }

    [Fact]
    public void ValidateMix_RejectsOverAvailability()
    {
        var free = new Dictionary<int, int> { [4] = 1 };
        var counts = new Dictionary<int, int> { [4] = 2 };

        Assert.Equal("mix-unavailable", RoomMixCalculator.ValidateMix(8, counts, free));
    }

    [Fact]
    public void ValidateMix_RejectsInsufficientCoverage()
    {
        var free = new Dictionary<int, int> { [4] = 5 };
        var counts = new Dictionary<int, int> { [4] = 1 };

        Assert.Equal("mix-too-small", RoomMixCalculator.ValidateMix(6, counts, free));
    }

    [Fact]
    public void DistributePeople_FillsAllButLastRoom()
    {
        var counts = new Dictionary<int, int> { [4] = 2, [3] = 1 };

        var loads = RoomMixCalculator.DistributePeople(9, counts);

        Assert.Equal([(4, 4), (4, 4), (3, 1)], loads);
    }
}
