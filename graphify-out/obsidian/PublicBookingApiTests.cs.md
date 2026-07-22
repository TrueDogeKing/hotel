---
source_file: "tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs"
type: "code"
community: "Application DTO Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_DTO_Namespaces
---

# PublicBookingApiTests.cs

## Context

_Source: `tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs` (defined near L1; showing L1–L46 of 217)._

```csharp
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
```

## Connections
- [[CampCenter.Application.DTOs.Public]] - `imports` [EXTRACTED]
- [[CampCenter.Application.DTOs.Rooms]] - `imports` [EXTRACTED]
- [[CampCenter.Application.DTOs.Sessions]] - `imports` [EXTRACTED]
- [[CampCenter.IntegrationTests]] - `contains` [EXTRACTED]
- [[PublicBookingApiTests]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_DTO_Namespaces