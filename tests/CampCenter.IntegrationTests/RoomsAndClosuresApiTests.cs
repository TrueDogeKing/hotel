using System.Net;
using System.Net.Http.Json;
using CampCenter.Application.DTOs.Closures;
using CampCenter.Application.DTOs.Rooms;

namespace CampCenter.IntegrationTests;

public class RoomsAndClosuresApiTests : IntegrationTestBase
{
    public RoomsAndClosuresApiTests(CampCenterApiFactory factory)
        : base(factory) { }

    [Fact]
    public async Task AdminEndpoints_WithoutToken_ReturnUnauthorized()
    {
        var client = CreateClient();

        var rooms = await client.GetAsync("/api/admin/rooms");
        var closures = await client.GetAsync("/api/admin/closures");

        Assert.Equal(HttpStatusCode.Unauthorized, rooms.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, closures.StatusCode);
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
    public async Task Closures_CrudRoundtrip_Works()
    {
        var client = await CreateAuthenticatedClientAsync();

        // Center-wide closure.
        var create = await client.PostAsJsonAsync(
            "/api/admin/closures",
            new CreateClosureRequestDto(
                "Przerwa zimowa",
                new DateOnly(2035, 12, 20),
                new DateOnly(2036, 1, 5),
                RoomId: null
            )
        );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var closure = (await create.Content.ReadFromJsonAsync<ClosureDto>())!;
        Assert.Null(closure.RoomId);

        // Update the reason.
        var update = await client.PutAsJsonAsync(
            $"/api/admin/closures/{closure.Id}",
            new UpdateClosureRequestDto(
                "Przerwa zimowa 2035/36",
                closure.StartDate,
                closure.EndDate,
                RoomId: null,
                closure.RowVersion
            )
        );
        Assert.Equal(HttpStatusCode.OK, update.StatusCode);
        var updated = (await update.Content.ReadFromJsonAsync<ClosureDto>())!;
        Assert.Equal("Przerwa zimowa 2035/36", updated.Reason);

        // Invalid range (end before start) is rejected.
        var invalid = await client.PostAsJsonAsync(
            "/api/admin/closures",
            new CreateClosureRequestDto(
                "Bad",
                new DateOnly(2035, 8, 10),
                new DateOnly(2035, 8, 1),
                RoomId: null
            )
        );
        Assert.Equal(HttpStatusCode.BadRequest, invalid.StatusCode);

        var delete = await client.DeleteAsync($"/api/admin/closures/{closure.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delete.StatusCode);
    }
}
