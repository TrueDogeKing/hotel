using CampCenter.Application.Services;
using CampCenter.Domain.Entities;

namespace CampCenter.UnitTests.Services;

/// Which rooms housekeeping is sent to on a day, and why. Assignment dates are
/// half-open — EndDate is the checkout day — so the day a group leaves is the day its
/// room needs doing, and these tests pin that down at both ends of a stay.
public class HousekeepingPlannerTests
{
    private static readonly DateOnly Day = new(2033, 5, 10);
    private static readonly Guid RoomA = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000001");
    private static readonly Guid RoomB = Guid.Parse("bbbbbbbb-0000-0000-0000-000000000002");

    private static Booking Booking(
        string name,
        DateOnly start,
        DateOnly end,
        params (Guid RoomId, int People)[] rooms
    )
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            OrganizationName = name,
            StartDate = start,
            EndDate = end,
            ContactName = "X",
            Email = "x@example.com",
            Phone = "123",
            Language = "pl",
            ManageTokenHash = "hash",
            RequestedRoomCounts = "{}",
        };

        booking.RoomAssignments =
        [
            .. rooms.Select(r => new BookingRoomAssignment
            {
                Id = Guid.NewGuid(),
                BookingId = booking.Id,
                RoomId = r.RoomId,
                StartDate = start,
                EndDate = end,
                PeopleCount = r.People,
            }),
        ];

        return booking;
    }

    [Fact]
    public void A_room_being_vacated_today_is_a_departure()
    {
        var leaving = Booking("Grupa A", Day.AddDays(-5), Day, (RoomA, 4));

        var job = Assert.Single(HousekeepingPlanner.ForDay([leaving], Day));

        Assert.Equal(RoomA, job.RoomId);
        Assert.Equal(RoomCleaningKind.Departure, job.Kind);
        Assert.Equal("Grupa A", job.Outgoing?.OrganizationName);
        Assert.Null(job.Incoming);
    }

    [Fact]
    public void A_room_taken_today_by_a_group_is_an_arrival()
    {
        var arriving = Booking("Grupa B", Day, Day.AddDays(4), (RoomA, 3));

        var job = Assert.Single(HousekeepingPlanner.ForDay([arriving], Day));

        Assert.Equal(RoomCleaningKind.Arrival, job.Kind);
        Assert.Equal("Grupa B", job.Incoming?.OrganizationName);
        Assert.Null(job.Outgoing);
        Assert.Equal(3, job.IncomingAssignment?.PeopleCount);
    }

    [Fact]
    public void One_group_out_and_the_next_in_is_a_single_turnaround()
    {
        var leaving = Booking("Grupa A", Day.AddDays(-3), Day, (RoomA, 4));
        var arriving = Booking("Grupa B", Day, Day.AddDays(3), (RoomA, 2));

        // One job, not two: it is one room to clean, however many groups touch it.
        var job = Assert.Single(HousekeepingPlanner.ForDay([leaving, arriving], Day));

        Assert.Equal(RoomCleaningKind.Turnaround, job.Kind);
        Assert.Equal("Grupa A", job.Outgoing?.OrganizationName);
        Assert.Equal("Grupa B", job.Incoming?.OrganizationName);
        // Beds to strip and beds to make up — both sides are carried.
        Assert.Equal(4, job.OutgoingAssignment?.PeopleCount);
        Assert.Equal(2, job.IncomingAssignment?.PeopleCount);
    }

    [Fact]
    public void A_room_occupied_through_the_day_is_left_alone()
    {
        var staying = Booking("Grupa A", Day.AddDays(-2), Day.AddDays(2), (RoomA, 4));

        Assert.Empty(HousekeepingPlanner.ForDay([staying], Day));
    }

    [Fact]
    public void The_day_before_a_departure_is_not_the_departure_day()
    {
        var leaving = Booking("Grupa A", Day.AddDays(-4), Day.AddDays(1), (RoomA, 4));

        Assert.Empty(HousekeepingPlanner.ForDay([leaving], Day));
    }

    [Fact]
    public void Each_room_of_a_multi_room_group_gets_its_own_job()
    {
        var leaving = Booking("Grupa A", Day.AddDays(-5), Day, (RoomA, 4), (RoomB, 2));

        var jobs = HousekeepingPlanner.ForDay([leaving], Day);

        Assert.Equal(2, jobs.Count);
        Assert.All(jobs, job => Assert.Equal(RoomCleaningKind.Departure, job.Kind));
        Assert.Equal([RoomA, RoomB], jobs.Select(j => j.RoomId).OrderBy(id => id).ToList());
    }

    [Fact]
    public void Departures_and_unrelated_arrivals_on_one_day_are_separate_jobs()
    {
        var leaving = Booking("Grupa A", Day.AddDays(-5), Day, (RoomA, 4));
        var arriving = Booking("Grupa B", Day, Day.AddDays(2), (RoomB, 2));

        var jobs = HousekeepingPlanner.ForDay([leaving, arriving], Day);

        Assert.Equal(2, jobs.Count);
        Assert.Equal(RoomCleaningKind.Departure, jobs.Single(j => j.RoomId == RoomA).Kind);
        Assert.Equal(RoomCleaningKind.Arrival, jobs.Single(j => j.RoomId == RoomB).Kind);
    }

    [Fact]
    public void A_one_night_stay_is_an_arrival_on_one_day_and_a_departure_on_the_next()
    {
        var overnight = Booking("Grupa A", Day, Day.AddDays(1), (RoomA, 4));

        Assert.Equal(
            RoomCleaningKind.Arrival,
            Assert.Single(HousekeepingPlanner.ForDay([overnight], Day)).Kind
        );
        Assert.Equal(
            RoomCleaningKind.Departure,
            Assert.Single(HousekeepingPlanner.ForDay([overnight], Day.AddDays(1))).Kind
        );
    }
}
