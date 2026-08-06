using CampCenter.Application.Services;

namespace CampCenter.UnitTests.Services;

/// The supervisor split: the kadra sleep in their own rooms, preferably small
/// ones, and never share with the children.
public class RoomMixCalculatorSplitTests
{
    private static Dictionary<int, int> Free(params (int Capacity, int Count)[] rooms) =>
        rooms.ToDictionary(r => r.Capacity, r => r.Count);

    [Fact]
    public void NoSupervisors_BehavesExactlyLikeTheOrdinaryMix()
    {
        var free = Free((4, 100), (3, 10), (2, 10));

        var split = RoomMixCalculator.SuggestSplitMix(301, 0, free)!;
        var plain = RoomMixCalculator.SuggestMix(301, free)!;

        Assert.Empty(split.SupervisorMix);
        Assert.Equal(plain, split.CamperMix);
        Assert.Equal(plain, split.Combined);
    }

    [Fact]
    public void Supervisors_TakeTheSmallestRoomsThatHoldThem()
    {
        // Three supervisors with 2s and 4s free: two doubles, not a quad.
        var split = RoomMixCalculator.SuggestSplitMix(20, 3, Free((2, 3), (4, 10)))!;

        Assert.Equal(2, split.SupervisorMix[2]);
        Assert.False(split.SupervisorMix.ContainsKey(4));
    }

    [Fact]
    public void Supervisors_AndCampers_NeverShareARoom()
    {
        var split = RoomMixCalculator.SuggestSplitMix(20, 3, Free((2, 3), (4, 10)))!;

        // Combined is the two halves added together, so every room in it belongs
        // to exactly one cohort.
        var expectedRooms = split.SupervisorMix.Values.Sum() + split.CamperMix.Values.Sum();
        Assert.Equal(expectedRooms, split.Combined.Values.Sum());
        Assert.True(RoomMixCalculator.TotalCapacity(split.SupervisorMix) >= 3);
        Assert.True(RoomMixCalculator.TotalCapacity(split.CamperMix) >= 20);
    }

    [Fact]
    public void Supervisors_TakeALargeRoom_WhenThatIsAllThereIs()
    {
        // Only quads free: the kadra cannot have a small room, but they still get
        // one to themselves rather than sharing with the children.
        var split = RoomMixCalculator.SuggestSplitMix(8, 2, Free((4, 3)))!;

        Assert.Equal(1, split.SupervisorMix[4]);
        Assert.Equal(2, split.CamperMix[4]);
    }

    [Fact]
    public void ReturnsNull_WhenTheKadraCannotBeSeparated()
    {
        // One ten-bed room holds all thirteen… nobody. Capacity is short anyway,
        // but the point is that no split exists.
        Assert.Null(RoomMixCalculator.SuggestSplitMix(7, 3, Free((10, 1))));
    }

    [Fact]
    public void ReturnsNull_WhenOnlyOneRoomIsFree_AndBothCohortsNeedIt()
    {
        // Enough beds for everyone, but only in a single room — which would mean
        // putting the children in with the staff.
        Assert.Null(RoomMixCalculator.SuggestSplitMix(3, 2, Free((8, 1))));
    }

    [Fact]
    public void StaffOnlyBooking_LeavesNoCamperRooms()
    {
        var split = RoomMixCalculator.SuggestSplitMix(0, 3, Free((2, 3), (4, 2)))!;

        Assert.Empty(split.CamperMix);
        Assert.Equal(2, split.SupervisorMix[2]);
        Assert.Equal(split.SupervisorMix, split.Combined);
    }

    [Fact]
    public void ValidateSplitMix_AcceptsAKadraRoom_ThatLooksRedundantAgainstTheTotal()
    {
        // 18 campers in 5×4 (20 beds — the fifth room is half empty) and 2
        // supervisors in a double. Judged against the combined headcount of 20 the
        // kadra room is redundant: drop it and 20 beds still cover 20 people.
        // Judged per cohort it is exactly what the two supervisors need.
        var free = Free((4, 10), (2, 3));
        var campers = Free((4, 5));
        var supervisors = Free((2, 1));

        Assert.Null(RoomMixCalculator.ValidateSplitMix(18, 2, campers, supervisors, free));
        Assert.Equal(
            "mix-redundant-room",
            RoomMixCalculator.ValidateMix(20, Free((4, 5), (2, 1)), free)
        );
    }

    [Fact]
    public void ValidateSplitMix_RejectsHalvesThatJointlyOverclaimARoomType()
    {
        // Two 4s free; each half asks for two.
        var error = RoomMixCalculator.ValidateSplitMix(
            5,
            5,
            Free((4, 2)),
            Free((4, 2)),
            Free((4, 2))
        );

        Assert.Equal("mix-unavailable", error);
    }

    [Fact]
    public void ValidateSplitMix_RejectsAHalfThatCannotHoldItsCohort()
    {
        var error = RoomMixCalculator.ValidateSplitMix(
            20,
            5,
            Free((4, 5)),
            Free((2, 1)),
            Free((4, 10), (2, 3))
        );

        Assert.Equal("mix-too-small", error);
    }

    [Fact]
    public void ValidateSplitMix_RejectsKadraRooms_ForAGroupWithNoKadra()
    {
        var error = RoomMixCalculator.ValidateSplitMix(
            20,
            0,
            Free((4, 5)),
            Free((2, 1)),
            Free((4, 10), (2, 3))
        );

        Assert.Equal("mix-redundant-room", error);
    }

    [Fact]
    public void DistributePeople_HandlesEachHalfSeparately()
    {
        var split = RoomMixCalculator.SuggestSplitMix(9, 3, Free((4, 10), (2, 3)))!;

        var supervisorLoads = RoomMixCalculator.DistributePeople(3, split.SupervisorMix);
        var camperLoads = RoomMixCalculator.DistributePeople(9, split.CamperMix);

        Assert.Equal(3, supervisorLoads.Sum(l => l.PeopleCount));
        Assert.Equal(9, camperLoads.Sum(l => l.PeopleCount));
    }
}
