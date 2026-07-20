using System.Net;
using System.Net.Http.Json;
using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.DTOs.Public;
using CampCenter.Application.DTOs.Rooms;
using CampCenter.Application.DTOs.Sessions;

namespace CampCenter.IntegrationTests;

public class AdminPanelApiTests : IntegrationTestBase
{
    public AdminPanelApiTests(CampCenterApiFactory factory)
        : base(factory) { }

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

        var sessionResponse = await admin.PostAsJsonAsync(
            "/api/admin/sessions",
            new CreateCampSessionRequestDto(
                "Turnus AP",
                new DateOnly(2032, 7, 1),
                new DateOnly(2032, 7, 10),
                90_000,
                20_000
            )
        );
        var session = (await sessionResponse.Content.ReadFromJsonAsync<CampSessionDto>())!;
        await admin.PostAsync($"/api/admin/sessions/{session.Id}/publish", null);

        // Public booking: 13 people in 2×7 rooms.
        var create = await CreateClient()
            .PostAsJsonAsync(
                "/api/public/bookings",
                new CreateBookingRequestDto(
                    session.Id,
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

        // Occupancy grid: two AP rooms occupied (7 + 6 people), one free.
        var occupancy = (
            await admin.GetFromJsonAsync<SessionOccupancyDto>(
                $"/api/admin/sessions/{session.Id}/occupancy"
            )
        )!;
        var apRooms = occupancy.Rooms.Where(r => roomIds.Contains(r.RoomId)).ToList();
        Assert.Equal(2, apRooms.Count(r => r.BookingId is not null));
        Assert.Equal(13, apRooms.Sum(r => r.PeopleCount ?? 0));
        Assert.Contains(apRooms, r => r.PeopleCount == 7);
        Assert.Contains(apRooms, r => r.PeopleCount == 6);

        // Bookings overview shows the booking with unpaid deposit.
        var list = (
            await admin.GetFromJsonAsync<List<AdminBookingDto>>(
                $"/api/admin/bookings?sessionId={session.Id}"
            )
        )!;
        var booking = Assert.Single(list);
        Assert.False(booking.DepositPaid);
        Assert.Equal(2, booking.Assignments.Count);

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
            new CreateRoomTaskRequestDto(roomIds[0], "Dostawka — 1 łóżko", session.Id, booking.Id)
        );
        Assert.Equal(HttpStatusCode.Created, taskCreate.StatusCode);
        var task = (await taskCreate.Content.ReadFromJsonAsync<RoomTaskDto>())!;

        occupancy = (
            await admin.GetFromJsonAsync<SessionOccupancyDto>(
                $"/api/admin/sessions/{session.Id}/occupancy"
            )
        )!;
        Assert.Equal(1, occupancy.Rooms.Single(r => r.RoomId == roomIds[0]).OpenTaskCount);

        var done = await admin.PostAsync($"/api/admin/tasks/{task.Id}/done", null);
        Assert.Equal(HttpStatusCode.OK, done.StatusCode);
        var tasks = (
            await admin.GetFromJsonAsync<List<RoomTaskDto>>("/api/admin/tasks?status=Open")
        )!;
        Assert.DoesNotContain(tasks, t => t.Id == task.Id);

        // Dashboard aggregates include this session and its pending deposit.
        var dashboard = (await admin.GetFromJsonAsync<DashboardDto>("/api/admin/dashboard"))!;
        Assert.Contains(dashboard.UpcomingSessions, s => s.Id == session.Id);
        Assert.True(dashboard.PendingDepositCount >= 1);

        // Admin cancel frees the rooms.
        var cancel = await admin.PostAsync($"/api/admin/bookings/{booking.Id}/cancel", null);
        Assert.Equal(HttpStatusCode.NoContent, cancel.StatusCode);
        occupancy = (
            await admin.GetFromJsonAsync<SessionOccupancyDto>(
                $"/api/admin/sessions/{session.Id}/occupancy"
            )
        )!;
        Assert.All(
            occupancy.Rooms.Where(r => roomIds.Contains(r.RoomId)),
            r => Assert.Null(r.BookingId)
        );
    }
}
