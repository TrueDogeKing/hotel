---
source_file: "tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs"
type: "code"
community: "Application DTO Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_DTO_Namespaces
---

# AdminPanelApiTests.cs

## Context

_Source: `tests/CampCenter.IntegrationTests/AdminPanelApiTests.cs` (defined near L1; showing L1–L46 of 164)._

```csharp
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
```

## Connections
- [[AdminPanelApiTests]] - `contains` [EXTRACTED]
- [[CampCenter.Application.DTOs.AdminPanel]] - `imports` [EXTRACTED]
- [[CampCenter.Application.DTOs.Public]] - `imports` [EXTRACTED]
- [[CampCenter.Application.DTOs.Rooms]] - `imports` [EXTRACTED]
- [[CampCenter.Application.DTOs.Sessions]] - `imports` [EXTRACTED]
- [[CampCenter.IntegrationTests]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_DTO_Namespaces