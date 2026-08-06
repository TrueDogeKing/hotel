using System.Net;
using System.Net.Http.Json;
using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.DTOs.Public;
using CampCenter.Application.DTOs.Rooms;

namespace CampCenter.IntegrationTests;

/// A group arrives as children plus kadra: the two are counted apart, housed
/// apart, and charged at their own rates.
public class AdminSupervisorRoomsApiTests : IntegrationTestBase
{
    public AdminSupervisorRoomsApiTests(CampCenterApiFactory factory)
        : base(factory) { }

    private async Task<HttpClient> WithRoomsAsync(
        string prefix,
        params (int Capacity, int Count)[] rooms
    )
    {
        var admin = await CreateAuthenticatedClientAsync();
        var index = 0;
        foreach (var (capacity, count) in rooms)
        {
            for (var i = 0; i < count; i++)
            {
                var response = await admin.PostAsJsonAsync(
                    "/api/admin/rooms",
                    new CreateRoomRequestDto($"{prefix}-{++index}", capacity, null)
                );
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }

        return admin;
    }

    [Fact]
    public async Task Create_HousesTheSupervisorsInRoomsOfTheirOwn()
    {
        var admin = await WithRoomsAsync("SR", (20, 3), (8, 2));

        var create = await admin.PostAsJsonAsync(
            "/api/admin/bookings",
            new CreateAdminBookingRequestDto(
                new DateOnly(2034, 6, 1),
                new DateOnly(2034, 6, 5), // 4 nights
                "Supervisor Org",
                "Anna Testowa",
                "supervisors@example.com",
                "+48 610 610 610",
                25,
                3,
                null,
                null,
                "pl",
                null,
                null,
                null,
                null
            )
        );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var booking = (await create.Content.ReadFromJsonAsync<AdminBookingDto>())!;

        Assert.Equal(3, booking.SupervisorCount);

        var kadraRooms = booking.Assignments.Where(a => a.IsSupervisorRoom).ToList();
        var camperRooms = booking.Assignments.Where(a => !a.IsSupervisorRoom).ToList();
        Assert.NotEmpty(kadraRooms);
        Assert.Equal(3, kadraRooms.Sum(a => a.PeopleCount));
        Assert.Equal(22, camperRooms.Sum(a => a.PeopleCount));

        // No room does double duty. Which capacity the kadra land in depends on
        // the whole centre's free rooms, which this test does not own — that the
        // smallest are preferred is RoomMixCalculatorSplitTests' to assert.
        Assert.Equal(
            booking.Assignments.Count,
            booking.Assignments.Select(a => a.RoomId).Distinct().Count()
        );
    }

    [Fact]
    public async Task Create_TakesTheRatesItIsGiven_AndFallsBackToTheCentres()
    {
        var admin = await WithRoomsAsync("SP", (10, 8));

        await admin.PutAsJsonAsync(
            "/api/admin/pricing",
            new UpdatePricingDefaultsRequestDto(10_000, 5_000, 2_000)
        );

        DateOnly start = new(2034, 7, 1),
            end = new(2034, 7, 3); // 2 nights

        // Rates left out: 20 campers at 100 zł and 3 kadra at 50 zł, over 2 nights.
        var byDefaults = await admin.PostAsJsonAsync(
            "/api/admin/bookings",
            new CreateAdminBookingRequestDto(
                start,
                end,
                "Default Rates",
                "Piotr Testowy",
                "defaults@example.com",
                "+48 620 620 620",
                23,
                3,
                null,
                null,
                "pl",
                null,
                null,
                null,
                null
            )
        );
        Assert.Equal(HttpStatusCode.Created, byDefaults.StatusCode);
        var defaulted = (await byDefaults.Content.ReadFromJsonAsync<AdminBookingDto>())!;
        Assert.Equal(10_000, defaulted.PricePerPersonPerNightGrosze);
        Assert.Equal(5_000, defaulted.SupervisorPricePerPersonPerNightGrosze);
        Assert.Equal((10_000 * 20 * 2) + (5_000 * 3 * 2), defaulted.TotalGrosze);
        Assert.Equal(2_000 * 23 * 2, defaulted.DepositGrosze);

        // Rates given: they are used verbatim, in the same call that makes the
        // booking — a group is never briefly on the books at the wrong price.
        var byRequest = await admin.PostAsJsonAsync(
            "/api/admin/bookings",
            new CreateAdminBookingRequestDto(
                start,
                end,
                "Given Rates",
                "Piotr Testowy",
                "given@example.com",
                "+48 620 620 621",
                23,
                3,
                null,
                null,
                "pl",
                12_000,
                0,
                null,
                50_000
            )
        );
        Assert.Equal(HttpStatusCode.Created, byRequest.StatusCode);
        var given = (await byRequest.Content.ReadFromJsonAsync<AdminBookingDto>())!;
        Assert.Equal(12_000, given.PricePerPersonPerNightGrosze);
        // The kadra come free here, so only the campers are on the bill.
        Assert.Equal(0, given.SupervisorPricePerPersonPerNightGrosze);
        Assert.Equal(12_000 * 20 * 2, given.TotalGrosze);
        Assert.Equal(50_000, given.DepositGrosze);
    }

    [Fact]
    public async Task Reassign_RequiresEachCohortToAddUpOnItsOwn()
    {
        var admin = await WithRoomsAsync("SM", (14, 2), (15, 2));

        var create = await admin.PostAsJsonAsync(
            "/api/admin/bookings",
            new CreateAdminBookingRequestDto(
                new DateOnly(2034, 9, 1),
                new DateOnly(2034, 9, 3),
                "Reassign Org",
                "Zofia Testowa",
                "reassign@example.com",
                "+48 630 630 630",
                26,
                2,
                null,
                null,
                "pl",
                null,
                null,
                null,
                null
            )
        );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var booking = (await create.Content.ReadFromJsonAsync<AdminBookingDto>())!;
        var kadraRoom = Assert.Single(booking.Assignments, a => a.IsSupervisorRoom);
        var camperRoom = booking.Assignments.First(a => !a.IsSupervisorRoom);

        // The headcount adds up, but one of the two supervisors has been left in
        // with the children.
        var wrong = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/assignments",
            new ReassignBookingRequestDto([
                new ReassignmentEntryDto(kadraRoom.RoomId, 1, true),
                new ReassignmentEntryDto(camperRoom.RoomId, 25, false),
            ])
        );
        Assert.Equal(HttpStatusCode.BadRequest, wrong.StatusCode);

        // Both cohorts adding up: accepted, and the flags come back as sent.
        var right = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/assignments",
            new ReassignBookingRequestDto([
                new ReassignmentEntryDto(kadraRoom.RoomId, 2, true),
                new ReassignmentEntryDto(camperRoom.RoomId, 24, false),
            ])
        );
        Assert.Equal(HttpStatusCode.OK, right.StatusCode);
        var moved = (await right.Content.ReadFromJsonAsync<AdminBookingDto>())!;
        Assert.Equal(2, Assert.Single(moved.Assignments, a => a.IsSupervisorRoom).PeopleCount);
        Assert.Equal(24, Assert.Single(moved.Assignments, a => !a.IsSupervisorRoom).PeopleCount);
    }

    [Fact]
    public async Task People_CanBeCorrected_WithoutTouchingThePriceOrTheRooms()
    {
        var admin = await WithRoomsAsync("SC", (11, 3));

        var create = await admin.PostAsJsonAsync(
            "/api/admin/bookings",
            new CreateAdminBookingRequestDto(
                new DateOnly(2035, 6, 1),
                new DateOnly(2035, 6, 4),
                "Correction Org",
                "Ola Testowa",
                "correction@example.com",
                "+48 660 660 660",
                20,
                2,
                null,
                null,
                "pl",
                null,
                null,
                null,
                null
            )
        );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var booking = (await create.Content.ReadFromJsonAsync<AdminBookingDto>())!;
        var roomsBefore = booking
            .Assignments.Select(a => (a.RoomId, a.PeopleCount, a.IsSupervisorRoom))
            .OrderBy(a => a.RoomId)
            .ToList();

        // A camper is sent home: one fewer, and nothing else moves.
        var corrected = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/people",
            new UpdateBookingPeopleRequestDto(19, 2)
        );
        Assert.Equal(HttpStatusCode.OK, corrected.StatusCode);
        var after = (await corrected.Content.ReadFromJsonAsync<AdminBookingDto>())!;

        Assert.Equal(19, after.Headcount);
        Assert.Equal(2, after.SupervisorCount);
        // The price was agreed, not derived — it stays until the owner changes it.
        Assert.Equal(booking.TotalGrosze, after.TotalGrosze);
        Assert.Equal(booking.DepositGrosze, after.DepositGrosze);
        // So do the rooms, which the owner rearranges separately.
        Assert.Equal(
            roomsBefore,
            after
                .Assignments.Select(a => (a.RoomId, a.PeopleCount, a.IsSupervisorRoom))
                .OrderBy(a => a.RoomId)
                .ToList()
        );

        // More supervisors than people is refused.
        var tooMany = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/people",
            new UpdateBookingPeopleRequestDto(5, 6)
        );
        Assert.Equal(HttpStatusCode.BadRequest, tooMany.StatusCode);

        // And so is an empty group.
        var empty = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{booking.Id}/people",
            new UpdateBookingPeopleRequestDto(0, 0)
        );
        Assert.Equal(HttpStatusCode.BadRequest, empty.StatusCode);
    }

    [Fact]
    public async Task PublicBooking_CarriesItsOwnSupervisorRooms()
    {
        var admin = await WithRoomsAsync("SB", (18, 2), (16, 1));
        var client = CreateClient();

        DateOnly start = new(2034, 10, 1),
            end = new(2034, 10, 4);

        var availability = (
            await client.GetFromJsonAsync<AvailabilityDto>(
                $"/api/public/availability?start={start:yyyy-MM-dd}&end={end:yyyy-MM-dd}&headcount=28&supervisors=2"
            )
        )!;
        Assert.True(availability.Fits);
        // Rooms of their own are offered for the kadra, enough to hold them.
        Assert.NotNull(availability.SuggestedSupervisorMix);
        Assert.True(availability.SuggestedSupervisorMix!.Sum(kv => (long)kv.Key * kv.Value) >= 2);

        var create = await client.PostAsJsonAsync(
            "/api/public/bookings",
            new CreateBookingRequestDto(
                start,
                end,
                28,
                2,
                new Dictionary<int, int> { [18] = 2 },
                new Dictionary<int, int> { [16] = 1 },
                "Public Kadra",
                "Ewa Testowa",
                "kadra@example.com",
                "+48 640 640 640",
                null,
                "pl"
            )
        );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);

        var booking = Assert.Single(
            (await admin.GetFromJsonAsync<List<AdminBookingDto>>("/api/admin/bookings"))!,
            b => b.OrganizationName == "Public Kadra"
        );
        Assert.Equal(2, booking.SupervisorCount);
        var kadra = Assert.Single(booking.Assignments, a => a.IsSupervisorRoom);
        Assert.Equal(2, kadra.PeopleCount);
        Assert.Equal(16, kadra.Capacity);
    }

    [Fact]
    public async Task PublicBooking_RejectsAMixThatSharesRoomsBetweenCohorts()
    {
        await WithRoomsAsync("SX", (19, 2));
        var client = CreateClient();

        // Both cohorts pointed at the same single room type, more than exists.
        var create = await client.PostAsJsonAsync(
            "/api/public/bookings",
            new CreateBookingRequestDto(
                new DateOnly(2034, 11, 1),
                new DateOnly(2034, 11, 4),
                30,
                2,
                new Dictionary<int, int> { [19] = 2 },
                new Dictionary<int, int> { [19] = 2 },
                "Overclaim Org",
                "Ewa Testowa",
                "overclaim@example.com",
                "+48 650 650 650",
                null,
                "pl"
            )
        );
        Assert.Equal(HttpStatusCode.BadRequest, create.StatusCode);
    }
}
