using System.Net;
using System.Net.Http.Json;
using CampCenter.Application.DTOs.Closures;
using CampCenter.Application.DTOs.Public;
using CampCenter.Application.DTOs.Rooms;

namespace CampCenter.IntegrationTests;

public class PublicBookingApiTests : IntegrationTestBase
{
    // Default per-night pricing from appsettings.json (grosze per person per night).
    private const long PricePerNight = 12_000;
    private const long DepositPerNight = 3_000;

    public PublicBookingApiTests(CampCenterApiFactory factory)
        : base(factory) { }

    /// Provisions rooms; the shared inventory is isolated per test by unique capacities.
    private async Task SetUpRoomsAsync(
        HttpClient admin,
        string roomPrefix,
        (int Capacity, int Count)[] rooms
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
    }

    private static string AvailabilityUrl(DateOnly start, DateOnly end, int? headcount = null) =>
        $"/api/public/availability?start={start:yyyy-MM-dd}&end={end:yyyy-MM-dd}"
        + (headcount is null ? "" : $"&headcount={headcount}");

    private static CreateBookingRequestDto BookingRequest(
        DateOnly start,
        DateOnly end,
        int headcount,
        Dictionary<int, int> counts,
        string email = "org@example.com"
    ) =>
        new(
            start,
            end,
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
        await SetUpRoomsAsync(admin, "PB1", [(6, 3), (2, 2)]);
        var client = CreateClient();
        var start = new DateOnly(2031, 7, 1);
        var end = new DateOnly(2031, 7, 14); // 13 nights

        var avail = (
            await client.GetFromJsonAsync<AvailabilityDto>(AvailabilityUrl(start, end, 9))
        )!;
        Assert.True(avail.Fits);
        Assert.False(avail.CenterClosed);
        var initialCapacity = avail.RemainingCapacity;
        Assert.Equal(3, avail.FreeRoomsByCapacity[6]);
        Assert.Equal(13, avail.Nights);

        // Book 9 people into 1×6 + 2×2 (tight, no redundant room).
        var create = await client.PostAsJsonAsync(
            "/api/public/bookings",
            BookingRequest(start, end, 9, new Dictionary<int, int> { [6] = 1, [2] = 2 })
        );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var booking = (await create.Content.ReadFromJsonAsync<CreateBookingResponseDto>())!;
        Assert.False(string.IsNullOrWhiteSpace(booking.ManageToken));

        // Availability shrank by exactly the 10 booked beds (1×6 + 2×2).
        var afterBooking = (
            await client.GetFromJsonAsync<AvailabilityDto>(AvailabilityUrl(start, end))
        )!;
        Assert.Equal(initialCapacity - 10, afterBooking.RemainingCapacity);
        Assert.Equal(2, afterBooking.FreeRoomsByCapacity[6]);
        Assert.False(afterBooking.FreeRoomsByCapacity.ContainsKey(2));

        // Manage page shows amounts computed from the per-night rate over 13 nights.
        var details = (
            await client.GetFromJsonAsync<BookingDetailsDto>(
                $"/api/public/bookings/{booking.ManageToken}"
            )
        )!;
        Assert.Equal("PendingDeposit", details.Status);
        Assert.Equal(9 * 13 * PricePerNight, details.TotalGrosze);
        Assert.Equal(9 * 13 * DepositPerNight, details.DepositGrosze);
        Assert.NotNull(details.HoldExpiresAt);

        // Cancel → rooms free again.
        var cancel = await client.PostAsync(
            $"/api/public/bookings/{booking.ManageToken}/cancel",
            null
        );
        Assert.Equal(HttpStatusCode.NoContent, cancel.StatusCode);
        var afterCancel = (
            await client.GetFromJsonAsync<AvailabilityDto>(AvailabilityUrl(start, end))
        )!;
        Assert.Equal(initialCapacity, afterCancel.RemainingCapacity);
    }

    [Fact]
    public async Task Booking_RedundantRoomSelection_IsRejected()
    {
        var admin = await CreateAuthenticatedClientAsync();
        await SetUpRoomsAsync(admin, "PB2", [(4, 5)]);
        var client = CreateClient();

        // 3×4 = 12 beds for 5 people — one room is redundant.
        var response = await client.PostAsJsonAsync(
            "/api/public/bookings",
            BookingRequest(
                new DateOnly(2031, 8, 1),
                new DateOnly(2031, 8, 10),
                5,
                new Dictionary<int, int> { [4] = 3 }
            )
        );

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ParallelBookings_ForTheLastRooms_ExactlyOneWins()
    {
        var admin = await CreateAuthenticatedClientAsync();
        // Capacity 5 is unique to this test — exactly one such room exists.
        await SetUpRoomsAsync(admin, "PB3", [(5, 1)]);
        var client = CreateClient();
        var start = new DateOnly(2031, 9, 1);
        var end = new DateOnly(2031, 9, 10);

        // Two concurrent bookings both want the single 5-person room for the same range.
        var tasks = Enumerable
            .Range(0, 2)
            .Select(i =>
                client.PostAsJsonAsync(
                    "/api/public/bookings",
                    BookingRequest(
                        start,
                        end,
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
    public async Task Booking_WhenCenterClosed_IsRejected()
    {
        var admin = await CreateAuthenticatedClientAsync();
        await SetUpRoomsAsync(admin, "PB4", [(11, 1)]);
        var start = new DateOnly(2031, 10, 1);
        var end = new DateOnly(2031, 10, 8);

        // Close the whole center over the requested range.
        var closure = await admin.PostAsJsonAsync(
            "/api/admin/closures",
            new CreateClosureRequestDto("Remont", start, end.AddDays(-1), RoomId: null)
        );
        Assert.Equal(HttpStatusCode.Created, closure.StatusCode);

        var client = CreateClient();
        var avail = (
            await client.GetFromJsonAsync<AvailabilityDto>(AvailabilityUrl(start, end, 8))
        )!;
        Assert.True(avail.CenterClosed);
        Assert.False(avail.Fits);

        var response = await client.PostAsJsonAsync(
            "/api/public/bookings",
            BookingRequest(start, end, 8, new Dictionary<int, int> { [11] = 1 })
        );
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UnknownManageToken_Returns404()
    {
        var client = CreateClient();

        var response = await client.GetAsync("/api/public/bookings/not-a-real-token");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private static string CalendarUrl(DateOnly from, DateOnly to, int? headcount = null) =>
        $"/api/public/availability/calendar?start={from:yyyy-MM-dd}&end={to:yyyy-MM-dd}"
        + (headcount is null ? "" : $"&headcount={headcount}");

    /// What the booking calendar greys out has to be exactly what the booking
    /// endpoint would refuse: a closed night, and a night short of beds.
    [Fact]
    public async Task Calendar_MarksClosedAndBookedNights_PerNight()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var client = CreateClient();
        // Capacity 17 is unique to this test (the room inventory is shared).
        await SetUpRoomsAsync(admin, "CAL", [(17, 2)]);

        var start = new DateOnly(2036, 6, 1);
        var end = start.AddDays(9);

        // Nothing booked yet: every night is open, both ends of the span included.
        var empty = (
            await client.GetFromJsonAsync<AvailabilityCalendarDto>(CalendarUrl(start, end))
        )!;
        Assert.Equal(10, empty.Days.Count);
        Assert.Equal(start, empty.Days[0].Date);
        Assert.Equal(end, empty.Days[^1].Date);
        Assert.All(empty.Days, d => Assert.False(d.Closed));
        // This test's own 34 beds, plus whatever other tests have left free.
        Assert.All(empty.Days, d => Assert.True(d.FreeBeds >= 34));

        // A group takes one of the two rooms for three nights…
        var booking = await client.PostAsJsonAsync(
            "/api/public/bookings",
            BookingRequest(
                start.AddDays(2),
                start.AddDays(5),
                17,
                new Dictionary<int, int> { [17] = 1 },
                "cal@example.com"
            )
        );
        Assert.Equal(HttpStatusCode.Created, booking.StatusCode);

        // …and the centre closes for two days later in the window.
        var closure = await admin.PostAsJsonAsync(
            "/api/admin/closures",
            new CreateClosureRequestDto(
                "Przerwa techniczna",
                start.AddDays(7),
                start.AddDays(8),
                RoomId: null
            )
        );
        Assert.Equal(HttpStatusCode.Created, closure.StatusCode);

        var after = (
            await client.GetFromJsonAsync<AvailabilityCalendarDto>(CalendarUrl(start, end, 17))
        )!;
        var byDate = after.Days.ToDictionary(d => d.Date);

        // The closed days: no beds, nothing fits, and a reason to show on hover.
        foreach (var day in new[] { start.AddDays(7), start.AddDays(8) })
        {
            Assert.True(byDate[day].Closed, $"{day:yyyy-MM-dd} should be closed");
            Assert.Equal(0, byDate[day].FreeBeds);
            Assert.False(byDate[day].Fits);
            Assert.Equal("Przerwa techniczna", byDate[day].ClosureReason);
        }

        // The booked nights lost 17 beds; the checkout day did not, because a stay
        // ends in the morning and the room is free again that night.
        var beforeBooking = byDate[start.AddDays(1)].FreeBeds;
        Assert.Equal(beforeBooking - 17, byDate[start.AddDays(2)].FreeBeds);
        Assert.Equal(beforeBooking - 17, byDate[start.AddDays(4)].FreeBeds);
        Assert.Equal(beforeBooking, byDate[start.AddDays(5)].FreeBeds);

        // Headcount is what decides "fits": a group larger than anything left over
        // fits on no night at all.
        var huge = (
            await client.GetFromJsonAsync<AvailabilityCalendarDto>(
                CalendarUrl(start, end, beforeBooking + 1)
            )
        )!;
        Assert.All(huge.Days, d => Assert.False(d.Fits));
    }

    /// An anonymous caller does not get to ask for a decade of nights, or for a
    /// span that runs backwards.
    [Fact]
    public async Task Calendar_RefusesAnUnreasonableSpan()
    {
        var client = CreateClient();
        var start = new DateOnly(2036, 1, 1);

        var tooWide = await client.GetAsync(CalendarUrl(start, start.AddYears(5)));
        Assert.Equal(HttpStatusCode.BadRequest, tooWide.StatusCode);

        var backwards = await client.GetAsync(CalendarUrl(start, start.AddDays(-1)));
        Assert.Equal(HttpStatusCode.BadRequest, backwards.StatusCode);
    }
}
