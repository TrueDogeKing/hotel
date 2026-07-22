---
source_file: "tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs"
type: "code"
community: "Integration Test Harness"
location: "L9"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Integration_Test_Harness
---

# PublicBookingApiTests

## Context

_Source: `tests/CampCenter.IntegrationTests/PublicBookingApiTests.cs` (defined near L9; showing L7–L54 of 217)._

```csharp
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
```

## Connections
- [[.BookingFlow_AvailabilityShrinks_AndCancelFreesRooms()]] - `method` [EXTRACTED]
- [[.BookingRequest()]] - `method` [EXTRACTED]
- [[.Booking_RedundantRoomSelection_IsRejected()]] - `method` [EXTRACTED]
- [[.ParallelBookings_ForTheLastRooms_ExactlyOneWins()]] - `method` [EXTRACTED]
- [[.SetUpSessionAsync()]] - `method` [EXTRACTED]
- [[.UnknownManageToken_Returns404()]] - `method` [EXTRACTED]
- [[IntegrationTestBase]] - `inherits` [EXTRACTED]
- [[PublicBookingApiTests.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Integration_Test_Harness