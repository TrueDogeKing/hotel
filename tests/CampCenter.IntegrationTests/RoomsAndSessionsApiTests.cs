using System.Net;
using System.Net.Http.Json;
using CampCenter.Application.DTOs.Rooms;
using CampCenter.Application.DTOs.Sessions;

namespace CampCenter.IntegrationTests;

public class RoomsAndSessionsApiTests : IntegrationTestBase
{
    public RoomsAndSessionsApiTests(CampCenterApiFactory factory)
        : base(factory) { }

    [Fact]
    public async Task AdminEndpoints_WithoutToken_ReturnUnauthorized()
    {
        var client = CreateClient();

        var rooms = await client.GetAsync("/api/admin/rooms");
        var sessions = await client.GetAsync("/api/admin/sessions");

        Assert.Equal(HttpStatusCode.Unauthorized, rooms.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, sessions.StatusCode);
    }

    [Fact]
    public async Task Rooms_CrudRoundtrip_Works()
    {
        var client = await CreateAuthenticatedClientAsync();

        var create = await client.PostAsJsonAsync(
            "/api/admin/rooms",
            new CreateRoomRequestDto("T-101", 4, "Test room")
        );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var room = (await create.Content.ReadFromJsonAsync<RoomDto>())!;

        // Duplicate number is rejected.
        var duplicate = await client.PostAsJsonAsync(
            "/api/admin/rooms",
            new CreateRoomRequestDto("T-101", 2, null)
        );
        Assert.Equal(HttpStatusCode.Conflict, duplicate.StatusCode);

        var update = await client.PutAsJsonAsync(
            $"/api/admin/rooms/{room.Id}",
            new UpdateRoomRequestDto("T-102", 3, true, null, room.RowVersion)
        );
        Assert.Equal(HttpStatusCode.OK, update.StatusCode);
        var updated = (await update.Content.ReadFromJsonAsync<RoomDto>())!;
        Assert.Equal("T-102", updated.Number);

        // A stale RowVersion is rejected with 409 (xmin optimistic concurrency).
        var stale = await client.PutAsJsonAsync(
            $"/api/admin/rooms/{room.Id}",
            new UpdateRoomRequestDto("T-103", 3, true, null, room.RowVersion)
        );
        Assert.Equal(HttpStatusCode.Conflict, stale.StatusCode);

        var delete = await client.DeleteAsync($"/api/admin/rooms/{room.Id}");
        Assert.Equal(HttpStatusCode.OK, delete.StatusCode);
    }

    [Fact]
    public async Task Sessions_LifecycleAndOverlapGuard_Work()
    {
        var client = await CreateAuthenticatedClientAsync();

        var create = await client.PostAsJsonAsync(
            "/api/admin/sessions",
            new CreateCampSessionRequestDto(
                "Turnus A",
                new DateOnly(2030, 7, 1),
                new DateOnly(2030, 7, 14),
                120_000,
                30_000
            )
        );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var sessionA = (await create.Content.ReadFromJsonAsync<CampSessionDto>())!;
        Assert.Equal("Draft", sessionA.Status);

        var publish = await client.PostAsync($"/api/admin/sessions/{sessionA.Id}/publish", null);
        Assert.Equal(HttpStatusCode.OK, publish.StatusCode);

        // A second session overlapping the published one cannot be published.
        var createB = await client.PostAsJsonAsync(
            "/api/admin/sessions",
            new CreateCampSessionRequestDto(
                "Turnus B",
                new DateOnly(2030, 7, 10),
                new DateOnly(2030, 7, 20),
                120_000,
                30_000
            )
        );
        var sessionB = (await createB.Content.ReadFromJsonAsync<CampSessionDto>())!;
        var publishB = await client.PostAsync($"/api/admin/sessions/{sessionB.Id}/publish", null);
        Assert.Equal(HttpStatusCode.BadRequest, publishB.StatusCode);

        // Invalid dates are rejected up front.
        var invalid = await client.PostAsJsonAsync(
            "/api/admin/sessions",
            new CreateCampSessionRequestDto(
                "Turnus C",
                new DateOnly(2030, 8, 10),
                new DateOnly(2030, 8, 1),
                120_000,
                30_000
            )
        );
        Assert.Equal(HttpStatusCode.BadRequest, invalid.StatusCode);

        // Cleanup keeps the shared database reusable for other tests.
        Assert.Equal(
            HttpStatusCode.NoContent,
            (await client.DeleteAsync($"/api/admin/sessions/{sessionA.Id}")).StatusCode
        );
        Assert.Equal(
            HttpStatusCode.NoContent,
            (await client.DeleteAsync($"/api/admin/sessions/{sessionB.Id}")).StatusCode
        );
    }
}
