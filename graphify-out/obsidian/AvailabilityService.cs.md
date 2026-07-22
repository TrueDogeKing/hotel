---
source_file: "src/CampCenter.Application/Services/AvailabilityService.cs"
type: "code"
community: "Application DTO Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_DTO_Namespaces
---

# AvailabilityService.cs

## Context

_Source: `src/CampCenter.Application/Services/AvailabilityService.cs` (defined near L1; showing L1–L46 of 125)._

```csharp
using CampCenter.Application.DTOs.Public;
using CampCenter.Application.Interfaces;
using CampCenter.Application.Models;
using CampCenter.Domain.Repositories;
using Microsoft.Extensions.Options;

namespace CampCenter.Application.Services;

public class AvailabilityService : IAvailabilityService
{
    private readonly IRoomRepository _rooms;
    private readonly IBookingRepository _bookings;
    private readonly IClosureRepository _closures;
    private readonly BookingSettings _settings;

    public AvailabilityService(
        IRoomRepository rooms,
        IBookingRepository bookings,
        IClosureRepository closures,
        IOptions<BookingSettings> settings
    )
    {
        _rooms = rooms;
        _bookings = bookings;
        _closures = closures;
        _settings = settings.Value;
    }

    public async Task<AvailabilityDto> GetAvailabilityAsync(
        DateOnly start,
        DateOnly end,
        int? headcount,
        CancellationToken cancellationToken = default
    )
    {
        var nights = end.DayNumber - start.DayNumber;
        var centerReason = await GetCenterClosureReasonAsync(start, end, cancellationToken);
        var free = await GetFreeRoomsByCapacityAsync(start, end, null, cancellationToken);
        var remaining = (int)RoomMixCalculator.TotalCapacity(free);

        Dictionary<int, int>? mix = null;
        bool? fits = null;
        long? total = null;
        long? deposit = null;
        if (headcount is > 0)
        {
```

## Connections
- [[AvailabilityService]] - `contains` [EXTRACTED]
- [[CampCenter.Application.DTOs.Public]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Services]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_DTO_Namespaces