---
source_file: "src/CampCenter.Application/Interfaces/IAvailabilityService.cs"
type: "code"
community: "Room Mix Calculator Tests"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Mix_Calculator_Tests
---

# IAvailabilityService.cs

## Context

_Source: `src/CampCenter.Application/Interfaces/IAvailabilityService.cs` (defined near L1; showing L1–L43 of 43)._

```csharp
using CampCenter.Application.DTOs.Public;

namespace CampCenter.Application.Interfaces;

public interface IAvailabilityService
{
    /// Availability for a requested date range [start, end); when a headcount is
    /// given it also carries Fits, a SuggestedMix and the computed amounts.
    Task<AvailabilityDto> GetAvailabilityAsync(
        DateOnly start,
        DateOnly end,
        int? headcount,
        CancellationToken cancellationToken = default
    );

    /// Free (active, unassigned, not-closed) room counts by capacity for the
    /// stay [start, end). <paramref name="excludeBookingId"/> ignores one
    /// booking's own rooms (used when reassigning).
    Task<Dictionary<int, int>> GetFreeRoomsByCapacityAsync(
        DateOnly start,
        DateOnly end,
        Guid? excludeBookingId = null,
        CancellationToken cancellationToken = default
    );

    /// The reason the whole center is closed on some day of [start, end), or null
    /// if it is open throughout.
    Task<string?> GetCenterClosureReasonAsync(
        DateOnly start,
        DateOnly end,
        CancellationToken cancellationToken = default
    );

    /// Room ids that cannot be used for the stay [start, end) — already booked or
    /// blocked by a closure (all active rooms when the whole center is closed).
    /// <paramref name="excludeBookingId"/> ignores one booking's own rooms.
    Task<HashSet<Guid>> GetBlockedRoomIdsAsync(
        DateOnly start,
        DateOnly end,
        Guid? excludeBookingId = null,
        CancellationToken cancellationToken = default
    );
}
```

## Connections
- [[CampCenter.Application.DTOs.Public]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `contains` [EXTRACTED]
- [[IAvailabilityService]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Mix_Calculator_Tests