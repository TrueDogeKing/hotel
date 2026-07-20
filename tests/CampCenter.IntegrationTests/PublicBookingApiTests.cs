using System.Net;
using System.Net.Http.Json;
using CampCenter.Application.DTOs.Public;
using CampCenter.Application.DTOs.Rooms;
using CampCenter.Application.DTOs.Sessions;

namespace CampCenter.IntegrationTests;

public class PublicBookingApiTests : IntegrationTestBase
{
    public PublicBookingApiTests(CampCenterApiFactory factory)
        : base(factory) { }

    /// Provisions rooms + a published session and returns the session id.
    private async Task<Guid> SetUpSessionAsync(
        HttpClient admin,
        string roomPrefix,
        (int Capacity, int Count)[] rooms,
        DateOnly start,
        DateOnly end
    )
    {
        var index = 0;
        foreach (var (capacity, count) in rooms)
        {
            for (var i = 0; i < count; i++)
            {
                var response = await admin.PostAsJsonAsync(
                    "/api/admin/rooms",
                    new CreateRoomRequestDto($"{roomPrefix}-{++index}", capacity, null)
                );
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }

        var create = await admin.PostAsJsonAsync(
            "/api/admin/sessions",
            new CreateCampSessionRequestDto($"Turnus {roomPrefix}", start, end, 100_000, 25_000)
        );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var session = (await create.Content.ReadFromJsonAsync<CampSessionDto>())!;
        var publish = await admin.PostAsync($"/api/admin/sessions/{session.Id}/publish", null);
        Assert.Equal(HttpStatusCode.OK, publish.StatusCode);
        return session.Id;
    }

    private static CreateBookingRequestDto BookingRequest(
        Guid sessionId,
        int headcount,
        Dictionary<int, int> counts,
        string email = "org@example.com"
    ) =>
        new(
            sessionId,
            headcount,
            counts,
            "Test Org",
            "Jan Kowalski",
            email,
            "+48 600 700 800",
            null,
            "pl"
        );

    [Fact]
    public async Task BookingFlow_AvailabilityShrinks_AndCancelFreesRooms()
    {
        var admin = await CreateAuthenticatedClientAsync();
        // Capacity 6 is unique to this test — the room inventory is shared across
        // the suite, so assertions work with deltas and per-capacity counts.
        var sessionId = await SetUpSessionAsync(
            admin,
            "PB1",
            [(6, 3), (2, 2)],
            new DateOnly(2031, 7, 1),
            new DateOnly(2031, 7, 14)
        );
        var client = CreateClient();

        var sessions = (
            await client.GetFromJsonAsync<List<PublicSessionDto>>(
                "/api/public/sessions?headcount=9"
            )
        )!;
        var s = sessions.Single(x => x.Id == sessionId);
        Assert.True(s.Fits);
        var initialCapacity = s.RemainingCapacity;
        Assert.Equal(3, s.FreeRoomsByCapacity[6]);

        // Book 9 people into 1×6 + 2×2 (tight, no redundant room).
        var create = await client.PostAsJsonAsync(
            "/api/public/bookings",
            BookingRequest(sessionId, 9, new Dictionary<int, int> { [6] = 1, [2] = 2 })
        );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var booking = (await create.Content.ReadFromJsonAsync<CreateBookingResponseDto>())!;
        Assert.False(string.IsNullOrWhiteSpace(booking.ManageToken));

        // Availability shrank by exactly the 10 booked beds (1×6 + 2×2).
        sessions = (await client.GetFromJsonAsync<List<PublicSessionDto>>("/api/public/sessions"))!;
        var afterBooking = sessions.Single(x => x.Id == sessionId);
        Assert.Equal(initialCapacity - 10, afterBooking.RemainingCapacity);
        Assert.Equal(2, afterBooking.FreeRoomsByCapacity[6]);
        Assert.False(afterBooking.FreeRoomsByCapacity.ContainsKey(2));

        // Manage page shows amounts snapshotted from the session prices.
        var details = (
            await client.GetFromJsonAsync<BookingDetailsDto>(
                $"/api/public/bookings/{booking.ManageToken}"
            )
        )!;
        Assert.Equal("PendingDeposit", details.Status);
        Assert.Equal(9 * 100_000, details.TotalGrosze);
        Assert.Equal(9 * 25_000, details.DepositGrosze);
        Assert.NotNull(details.HoldExpiresAt);

        // Cancel → rooms free again.
        var cancel = await client.PostAsync(
            $"/api/public/bookings/{booking.ManageToken}/cancel",
            null
        );
        Assert.Equal(HttpStatusCode.NoContent, cancel.StatusCode);
        sessions = (await client.GetFromJsonAsync<List<PublicSessionDto>>("/api/public/sessions"))!;
        Assert.Equal(initialCapacity, sessions.Single(x => x.Id == sessionId).RemainingCapacity);
    }

    [Fact]
    public async Task Booking_RedundantRoomSelection_IsRejected()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var sessionId = await SetUpSessionAsync(
            admin,
            "PB2",
            [(4, 5)],
            new DateOnly(2031, 8, 1),
            new DateOnly(2031, 8, 10)
        );
        var client = CreateClient();

        // 3×4 = 12 beds for 5 people — one room is redundant.
        var response = await client.PostAsJsonAsync(
            "/api/public/bookings",
            BookingRequest(sessionId, 5, new Dictionary<int, int> { [4] = 3 })
        );

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ParallelBookings_ForTheLastRooms_ExactlyOneWins()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var sessionId = await SetUpSessionAsync(
            admin,
            "PB3",
            [(5, 1)], // capacity 5 is unique to this test — exactly one such room exists
            new DateOnly(2031, 9, 1),
            new DateOnly(2031, 9, 10)
        );
        var client = CreateClient();

        // Two concurrent bookings both want the single 4-person room.
        var tasks = Enumerable
            .Range(0, 2)
            .Select(i =>
                client.PostAsJsonAsync(
                    "/api/public/bookings",
                    BookingRequest(
                        sessionId,
                        5,
                        new Dictionary<int, int> { [5] = 1 },
                        $"race{i}@example.com"
                    )
                )
            )
            .ToArray();
        var responses = await Task.WhenAll(tasks);

        Assert.Single(responses, r => r.StatusCode == HttpStatusCode.Created);
        Assert.Single(
            responses,
            r => r.StatusCode is HttpStatusCode.BadRequest or HttpStatusCode.Conflict
        );
    }

    [Fact]
    public async Task UnknownManageToken_Returns404()
    {
        var client = CreateClient();

        var response = await client.GetAsync("/api/public/bookings/not-a-real-token");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
