using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.Services;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;
using NSubstitute;

namespace CampCenter.UnitTests.Services;

/// The service around the planner: ordering the round, merging stored progress into the
/// derived list, and refusing to record progress against a room that has nothing to do.
public class HousekeepingServiceTests
{
    private static readonly DateOnly Day = new(2033, 5, 10);
    private static readonly Guid AdminId = Guid.Parse("dddddddd-0000-0000-0000-000000000009");

    private readonly IBookingRepository _bookings = Substitute.For<IBookingRepository>();
    private readonly IRoomRepository _rooms = Substitute.For<IRoomRepository>();
    private readonly IRoomCleaningRepository _cleanings = Substitute.For<IRoomCleaningRepository>();
    private readonly IRoomTaskRepository _tasks = Substitute.For<IRoomTaskRepository>();
    private readonly IClosureRepository _closures = Substitute.For<IClosureRepository>();
    private readonly HousekeepingService _service;

    public HousekeepingServiceTests()
    {
        _service = new HousekeepingService(_bookings, _rooms, _cleanings, _tasks, _closures);
        _tasks.CountOpenByRoomAsync(Arg.Any<CancellationToken>()).Returns([]);
        _closures
            .GetOverlappingAsync(
                Arg.Any<DateOnly>(),
                Arg.Any<DateOnly>(),
                Arg.Any<CancellationToken>()
            )
            .Returns([]);
        _cleanings.ListForDateAsync(Arg.Any<DateOnly>(), Arg.Any<CancellationToken>()).Returns([]);
    }

    private static Room Room(string number, int capacity = 4) =>
        new()
        {
            Id = Guid.NewGuid(),
            Number = number,
            Capacity = capacity,
        };

    private static Booking Booking(string name, DateOnly start, DateOnly end, params Room[] rooms)
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
            .. rooms.Select(room => new BookingRoomAssignment
            {
                Id = Guid.NewGuid(),
                BookingId = booking.Id,
                RoomId = room.Id,
                StartDate = start,
                EndDate = end,
                PeopleCount = room.Capacity,
            }),
        ];

        return booking;
    }

    private void Setup(IEnumerable<Room> rooms, params Booking[] bookings)
    {
        _rooms.GetAllAsync(Arg.Any<CancellationToken>()).Returns([.. rooms]);
        _bookings
            .ListLiveChangingOverAsync(
                Arg.Any<DateOnly>(),
                Arg.Any<DateOnly>(),
                Arg.Any<CancellationToken>()
            )
            .Returns([.. bookings]);
    }

    [Fact]
    public async Task Puts_turnarounds_before_departures_and_arrivals()
    {
        var turnaround = Room("10");
        var departure = Room("11");
        var arrival = Room("12");
        Setup(
            [arrival, departure, turnaround],
            Booking("Wychodzi", Day.AddDays(-3), Day, turnaround, departure),
            Booking("Wchodzi", Day, Day.AddDays(3), turnaround, arrival)
        );

        var day = await _service.GetDayAsync(Day);

        Assert.Equal(["Turnaround", "Departure", "Arrival"], day.Rooms.Select(r => r.Kind));
        Assert.Equal(1, day.TurnaroundCount);
        Assert.Equal(1, day.DepartureCount);
        Assert.Equal(1, day.ArrivalCount);
        Assert.Equal(0, day.DoneCount);
    }

    [Fact]
    public async Task A_room_nobody_has_touched_is_pending()
    {
        var room = Room("10");
        Setup([room], Booking("Wychodzi", Day.AddDays(-2), Day, room));

        var day = await _service.GetDayAsync(Day);

        Assert.Equal("Pending", Assert.Single(day.Rooms).Status);
    }

    [Fact]
    public async Task Stored_progress_is_merged_into_the_derived_list()
    {
        var room = Room("10");
        Setup([room], Booking("Wychodzi", Day.AddDays(-2), Day, room));
        _cleanings
            .ListForDateAsync(Day, Arg.Any<CancellationToken>())
            .Returns([
                new RoomCleaning
                {
                    Id = Guid.NewGuid(),
                    RoomId = room.Id,
                    Date = Day,
                    Kind = RoomCleaningKind.Departure,
                    Status = RoomCleaningStatus.Done,
                    Note = "Zbita szyba",
                    DoneAt = new DateTime(2033, 5, 10, 9, 30, 0, DateTimeKind.Utc),
                },
            ]);

        var day = await _service.GetDayAsync(Day);

        var dto = Assert.Single(day.Rooms);
        Assert.Equal("Done", dto.Status);
        Assert.Equal("Zbita szyba", dto.Note);
        Assert.NotNull(dto.DoneAt);
        Assert.Equal(1, day.DoneCount);
    }

    [Fact]
    public async Task A_turnaround_carries_both_groups_and_both_bed_counts()
    {
        var room = Room("10", capacity: 4);
        Setup(
            [room],
            Booking("Wychodzi", Day.AddDays(-3), Day, room),
            Booking("Wchodzi", Day, Day.AddDays(3), room)
        );

        var dto = Assert.Single((await _service.GetDayAsync(Day)).Rooms);

        Assert.Equal("Wychodzi", dto.OutgoingOrganizationName);
        Assert.Equal("Wchodzi", dto.IncomingOrganizationName);
        Assert.Equal(4, dto.OutgoingPeopleCount);
        Assert.Equal(4, dto.IncomingPeopleCount);
    }

    [Fact]
    public async Task Marking_a_room_done_creates_its_row_and_stamps_who_and_when()
    {
        var room = Room("10");
        Setup([room], Booking("Wychodzi", Day.AddDays(-2), Day, room));
        _cleanings
            .GetAsync(room.Id, Day, Arg.Any<CancellationToken>())
            .Returns((RoomCleaning?)null);

        RoomCleaning? added = null;
        await _cleanings.AddAsync(
            Arg.Do<RoomCleaning>(c => added = c),
            Arg.Any<CancellationToken>()
        );

        await _service.SetStatusAsync(
            room.Id,
            Day,
            new SetRoomCleaningRequestDto("Done", " ok "),
            AdminId
        );

        Assert.NotNull(added);
        Assert.Equal(RoomCleaningStatus.Done, added!.Status);
        Assert.Equal(RoomCleaningKind.Departure, added.Kind);
        Assert.Equal("ok", added.Note);
        Assert.NotNull(added.DoneAt);
        Assert.Equal(AdminId, added.DoneByAdminUserId);
        await _cleanings.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Reopening_a_room_clears_who_finished_it()
    {
        var room = Room("10");
        Setup([room], Booking("Wychodzi", Day.AddDays(-2), Day, room));
        var existing = new RoomCleaning
        {
            Id = Guid.NewGuid(),
            RoomId = room.Id,
            Date = Day,
            Status = RoomCleaningStatus.Done,
            DoneAt = DateTime.UtcNow,
            DoneByAdminUserId = AdminId,
        };
        _cleanings.GetAsync(room.Id, Day, Arg.Any<CancellationToken>()).Returns(existing);

        await _service.SetStatusAsync(
            room.Id,
            Day,
            new SetRoomCleaningRequestDto("Pending", null),
            AdminId
        );

        Assert.Equal(RoomCleaningStatus.Pending, existing.Status);
        Assert.Null(existing.DoneAt);
        Assert.Null(existing.DoneByAdminUserId);
        Assert.NotNull(existing.UpdatedAt);
    }

    [Fact]
    public async Task Refuses_a_room_with_nothing_to_do_that_day()
    {
        var busy = Room("10");
        var idle = Room("11");
        Setup([busy, idle], Booking("Wychodzi", Day.AddDays(-2), Day, busy));

        // A page left open while the booking moved must not write progress nobody will
        // ever see again — the list is derived, so such a row would be invisible.
        await Assert.ThrowsAsync<BusinessRuleViolationException>(() =>
            _service.SetStatusAsync(
                idle.Id,
                Day,
                new SetRoomCleaningRequestDto("Done", null),
                AdminId
            )
        );
        await _cleanings.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Rejects_an_unknown_status()
    {
        var room = Room("10");
        Setup([room], Booking("Wychodzi", Day.AddDays(-2), Day, room));

        await Assert.ThrowsAsync<BusinessRuleViolationException>(() =>
            _service.SetStatusAsync(
                room.Id,
                Day,
                new SetRoomCleaningRequestDto("Sparkling", null),
                AdminId
            )
        );
    }

    [Fact]
    public async Task The_day_strip_counts_rooms_and_rooms_done_per_day()
    {
        var room = Room("10");
        var other = Room("11");
        Setup(
            [room, other],
            // Leaves room 10 on the 10th, and room 11 is taken on the 11th.
            Booking("Wychodzi", Day.AddDays(-2), Day, room),
            Booking("Wchodzi", Day.AddDays(1), Day.AddDays(4), other)
        );
        _cleanings
            .CountDoneByDateAsync(
                Arg.Any<DateOnly>(),
                Arg.Any<DateOnly>(),
                Arg.Any<CancellationToken>()
            )
            .Returns(new Dictionary<DateOnly, int> { [Day] = 1 });

        var range = await _service.GetRangeAsync(Day, Day.AddDays(2));

        Assert.Equal(3, range.Days.Count);
        Assert.Equal((1, 1), (range.Days[0].RoomCount, range.Days[0].DoneCount));
        Assert.Equal((1, 0), (range.Days[1].RoomCount, range.Days[1].DoneCount));
        Assert.Equal((0, 0), (range.Days[2].RoomCount, range.Days[2].DoneCount));
    }

    [Fact]
    public async Task Rejects_a_range_that_ends_before_it_starts()
    {
        await Assert.ThrowsAsync<BusinessRuleViolationException>(() =>
            _service.GetRangeAsync(Day, Day.AddDays(-1))
        );
    }
}
