---
source_file: "tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs"
type: "code"
community: "Integration Test Harness"
location: "L10"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Integration_Test_Harness
---

# AdminPanelApiTests

## Context

_Source: `tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs` (defined near L10; showing L8–L55 of 164)._

```csharp

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
```

## Connections
- [[.Occupancy_Reassign_Tasks_And_Dashboard_Work()]] - `method` [EXTRACTED]
- [[AdminPanelApiTests.cs]] - `contains` [EXTRACTED]
- [[IntegrationTestBase]] - `inherits` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Integration_Test_Harness