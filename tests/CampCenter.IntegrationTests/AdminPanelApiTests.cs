using System.Net;
using System.Net.Http.Json;
using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.DTOs.Public;
using CampCenter.Application.DTOs.Rooms;

namespace CampCenter.IntegrationTests;

public class AdminPanelApiTests : IntegrationTestBase
{
    public AdminPanelApiTests(CampCenterApiFactory factory)
        : base(factory) { }

    private static string OccupancyUrl(DateOnly start, DateOnly end) =>
        $"/api/admin/occupancy?start={start:yyyy-MM-dd}&end={end:yyyy-MM-dd}";

    [Fact]
    public async Task Occupancy_Reassign_Tasks_And_Dashboard_Work()
    {
        var admin = await CreateAuthenticatedClientAsync();

        // Rooms with capacity 7 are unique to this test (shared inventory).
        var roomIds = new List<Guid>();
        for (var i = 1; i <= 3; i++)
        {
            var response = await admin.PostAsJsonAsync(
                "/api/admin/rooms",
                new CreateRoomRequestDto($"AP-{i}", 7, null)
            );
            roomIds.Add((await response.Content.ReadFromJsonAsync<RoomDto>())!.Id);
        }

        var start = new DateOnly(2032, 7, 1);
        var end = new DateOnly(2032, 7, 10); // 9 nights

        // Public booking: 13 people in 2×7 rooms.
        var create = await CreateClient()
            .PostAsJsonAsync(
                "/api/public/bookings",
                new CreateBookingRequestDto(
                    start,
                    end,
                    13,
                    new Dictionary<int, int> { [7] = 2 },
                    "AP Org",
                    "Ola Testowa",
                    "ap@example.com",
                    "+48 500 500 500",
                    null,
                    "pl"
                )
            );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);

        // Occupancy grid over the stay: two AP rooms occupied (7 + 6 people), one free.
        var occupancy = (
            await admin.GetFromJsonAsync<OccupancyDto>(OccupancyUrl(start, end))
        )!;
        var apRooms = occupancy.Rooms.Where(r => roomIds.Contains(r.RoomId)).ToList();
        Assert.Equal(2, apRooms.Count(r => r.BookingId is not null));
        Assert.Equal(13, apRooms.Sum(r => r.PeopleCount ?? 0));
        Assert.Contains(apRooms, r => r.PeopleCount == 7);
        Assert.Contains(apRooms, r => r.PeopleCount == 6);

        // Bookings overview shows the booking with unpaid deposit.
        var list = (
            await admin.GetFromJsonAsync<List<AdminBookingDto>>(
                "/api/admin/bookings?status=PendingDeposit"
            )
        )!;
        var booking = Assert.Single(list, b => b.OrganizationName == "AP Org");
        Assert.False(booking.DepositPaid);
        Assert.Equal(2, booking.Assignments.Count);
        Assert.Equal(9, booking.Nights);

        // Reassign into all three AP rooms (5+4+4 = 13).
        var reassign = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/assignments",
            new ReassignBookingRequestDto([
                new ReassignmentEntryDto(roomIds[0], 5),
                new ReassignmentEntryDto(roomIds[1], 4),
                new ReassignmentEntryDto(roomIds[2], 4),
            ])
        );
        Assert.True(
            reassign.StatusCode == HttpStatusCode.OK,
            $"Reassign failed: {reassign.StatusCode} {await reassign.Content.ReadAsStringAsync()}"
        );
        var reassigned = (await reassign.Content.ReadFromJsonAsync<AdminBookingDto>())!;
        Assert.Equal(3, reassigned.Assignments.Count);

        // Wrong headcount sum is rejected.
        var badReassign = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/assignments",
            new ReassignBookingRequestDto([new ReassignmentEntryDto(roomIds[0], 5)])
        );
        Assert.Equal(HttpStatusCode.BadRequest, badReassign.StatusCode);

        // Housekeeping: add a task ("extra bed"), see it in the occupancy grid, mark done.
        var taskCreate = await admin.PostAsJsonAsync(
            "/api/admin/tasks",
            new CreateRoomTaskRequestDto(roomIds[0], "Dostawka — 1 łóżko", booking.Id)
        );
        Assert.Equal(HttpStatusCode.Created, taskCreate.StatusCode);
        var task = (await taskCreate.Content.ReadFromJsonAsync<RoomTaskDto>())!;

        occupancy = (await admin.GetFromJsonAsync<OccupancyDto>(OccupancyUrl(start, end)))!;
        Assert.Equal(1, occupancy.Rooms.Single(r => r.RoomId == roomIds[0]).OpenTaskCount);

        var done = await admin.PostAsync($"/api/admin/tasks/{task.Id}/done", null);
        Assert.Equal(HttpStatusCode.OK, done.StatusCode);
        var tasks = (
            await admin.GetFromJsonAsync<List<RoomTaskDto>>("/api/admin/tasks?status=Open")
        )!;
        Assert.DoesNotContain(tasks, t => t.Id == task.Id);

        // Dashboard aggregates include upcoming bookings and the pending deposit.
        var dashboard = (await admin.GetFromJsonAsync<DashboardDto>("/api/admin/dashboard"))!;
        Assert.NotEmpty(dashboard.UpcomingBookings);
        Assert.True(dashboard.PendingDepositCount >= 1);

        // Admin cancel frees the rooms.
        var cancel = await admin.PostAsync($"/api/admin/bookings/{booking.Id}/cancel", null);
        Assert.Equal(HttpStatusCode.NoContent, cancel.StatusCode);
        occupancy = (await admin.GetFromJsonAsync<OccupancyDto>(OccupancyUrl(start, end)))!;
        Assert.All(
            occupancy.Rooms.Where(r => roomIds.Contains(r.RoomId)),
            r => Assert.Null(r.BookingId)
        );
    }

    /// The room-move panel offers exactly what a reassign would accept: the group's
    /// own rooms, plus rooms free for the whole stay — never a room another group
    /// holds or a closure blocks.
    [Fact]
    public async Task AssignableRooms_OffersOwnAndFreeRooms_ButNotTakenOrClosedOnes()
    {
        var admin = await CreateAuthenticatedClientAsync();

        // Capacity 11 is unique to this test (the room inventory is shared).
        var roomIds = new List<Guid>();
        foreach (var number in new[] { "AR-1", "AR-2", "AR-3", "AR-4" })
        {
            var created = await admin.PostAsJsonAsync(
                "/api/admin/rooms",
                new CreateRoomRequestDto(number, 11, null)
            );
            roomIds.Add((await created.Content.ReadFromJsonAsync<RoomDto>())!.Id);
        }

        var start = new DateOnly(2035, 6, 1);
        var end = new DateOnly(2035, 6, 8);

        // The group under test takes one 11-bed room…
        var create = await CreateClient()
            .PostAsJsonAsync(
                "/api/public/bookings",
                new CreateBookingRequestDto(
                    start,
                    end,
                    11,
                    new Dictionary<int, int> { [11] = 1 },
                    "AR Org",
                    "Ola Testowa",
                    "ar@example.com",
                    "+48 500 500 501",
                    null,
                    "pl"
                )
            );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);

        // …a second group takes another over an overlapping range…
        var rival = await CreateClient()
            .PostAsJsonAsync(
                "/api/public/bookings",
                new CreateBookingRequestDto(
                    start.AddDays(2),
                    end.AddDays(2),
                    11,
                    new Dictionary<int, int> { [11] = 1 },
                    "AR Rival",
                    "Jan Testowy",
                    "ar-rival@example.com",
                    "+48 500 500 502",
                    null,
                    "pl"
                )
            );
        Assert.Equal(HttpStatusCode.Created, rival.StatusCode);

        var bookings = (
            await admin.GetFromJsonAsync<List<AdminBookingDto>>("/api/admin/bookings")
        )!;
        var booking = Assert.Single(bookings, b => b.OrganizationName == "AR Org");
        var ownRoomId = Assert.Single(booking.Assignments).RoomId;
        var rivalRoomId = Assert
            .Single(bookings, b => b.OrganizationName == "AR Rival")
            .Assignments.Single()
            .RoomId;

        // …a third is closed for maintenance across part of the stay, and a fourth is
        // left free, so there is something to move into.
        var spare = roomIds.Where(id => id != ownRoomId && id != rivalRoomId).ToList();
        Assert.Equal(2, spare.Count);
        var closureRoomId = spare[0];
        var freeRoomId = spare[1];
        var closure = await admin.PostAsJsonAsync(
            "/api/admin/closures",
            new CampCenter.Application.DTOs.Closures.CreateClosureRequestDto(
                "Remont",
                start.AddDays(1),
                start.AddDays(3),
                RoomId: closureRoomId
            )
        );
        Assert.Equal(HttpStatusCode.Created, closure.StatusCode);

        var assignable = (
            await admin.GetFromJsonAsync<List<AssignableRoomDto>>(
                $"/api/admin/bookings/{booking.Id}/assignable-rooms"
            )
        )!;

        // Its own room is offered and flagged as held; the rival's and the closed one
        // are not offered at all.
        var own = Assert.Single(assignable, r => r.RoomId == ownRoomId);
        Assert.True(own.Assigned);
        Assert.Equal(11, own.Capacity);
        Assert.DoesNotContain(assignable, r => r.RoomId == rivalRoomId);
        Assert.DoesNotContain(assignable, r => r.RoomId == closureRoomId);

        // Everything offered is genuinely assignable: moving into one is accepted.
        var target = Assert.Single(assignable, r => r.RoomId == freeRoomId);
        Assert.False(target.Assigned);
        var move = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/assignments",
            new ReassignBookingRequestDto([new ReassignmentEntryDto(target.RoomId, 11)])
        );
        Assert.True(
            move.StatusCode == HttpStatusCode.OK,
            $"Move failed: {move.StatusCode} {await move.Content.ReadAsStringAsync()}"
        );
        var moved = (await move.Content.ReadFromJsonAsync<AdminBookingDto>())!;
        Assert.Equal(target.RoomId, Assert.Single(moved.Assignments).RoomId);

        // The room just vacated is offered again, no longer flagged as held.
        var after = (
            await admin.GetFromJsonAsync<List<AssignableRoomDto>>(
                $"/api/admin/bookings/{booking.Id}/assignable-rooms"
            )
        )!;
        Assert.False(Assert.Single(after, r => r.RoomId == ownRoomId).Assigned);
        Assert.True(Assert.Single(after, r => r.RoomId == target.RoomId).Assigned);
    }

    [Fact]
    public async Task Closure_BlocksRoom_InOccupancyGrid()
    {
        var admin = await CreateAuthenticatedClientAsync();

        var roomResponse = await admin.PostAsJsonAsync(
            "/api/admin/rooms",
            new CreateRoomRequestDto("APC-1", 13, null)
        );
        var roomId = (await roomResponse.Content.ReadFromJsonAsync<RoomDto>())!.Id;

        var start = new DateOnly(2034, 5, 1);
        var end = new DateOnly(2034, 5, 8);

        // Block just this room for maintenance.
        var closure = await admin.PostAsJsonAsync(
            "/api/admin/closures",
            new CampCenter.Application.DTOs.Closures.CreateClosureRequestDto(
                "Remont pokoju",
                start,
                end.AddDays(-1),
                RoomId: roomId
            )
        );
        Assert.Equal(HttpStatusCode.Created, closure.StatusCode);

        var occupancy = (await admin.GetFromJsonAsync<OccupancyDto>(OccupancyUrl(start, end)))!;
        var row = occupancy.Rooms.Single(r => r.RoomId == roomId);
        Assert.True(row.Closed);
        Assert.Equal("Remont pokoju", row.ClosureReason);
        Assert.Null(row.BookingId);
    }
}
