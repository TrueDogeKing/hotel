---
source_file: "src/CampCenter.Application/Services/AdminBookingService.cs"
type: "code"
community: "Auth DTOs & Models"
location: "L10"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_DTOs__Models
---

# CampCenter.Application.Services

## Context

_Source: `src/CampCenter.Application/Services/AdminBookingService.cs` (defined near L10; showing L8–L55 of 346)._

```csharp
using Microsoft.Extensions.Options;

namespace CampCenter.Application.Services;

public class AdminBookingService : IAdminBookingService
{
    private readonly IBookingRepository _bookings;
    private readonly IRoomRepository _rooms;
    private readonly IRoomTaskRepository _tasks;
    private readonly IClosureRepository _closures;
    private readonly IAvailabilityService _availability;
    private readonly IEmailSender _email;
    private readonly BookingSettings _settings;
    private readonly ILogger<AdminBookingService> _logger;

    public AdminBookingService(
        IBookingRepository bookings,
        IRoomRepository rooms,
        IRoomTaskRepository tasks,
        IClosureRepository closures,
        IAvailabilityService availability,
        IEmailSender email,
        IOptions<BookingSettings> settings,
        ILogger<AdminBookingService> logger
    )
    {
        _bookings = bookings;
        _rooms = rooms;
        _tasks = tasks;
        _closures = closures;
        _availability = availability;
        _email = email;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<List<AdminBookingDto>> ListAsync(
        BookingStatus? status,
        CancellationToken cancellationToken = default
    )
    {
        var bookings = await _bookings.ListAsync(status, cancellationToken);
        var paid = await _bookings.GetCompletedPaymentKindsAsync(
            bookings.Select(b => b.Id).ToList(),
            cancellationToken
        );
        return bookings.Select(b => ToDto(b, paid.GetValueOrDefault(b.Id) ?? [])).ToList();
    }
```

## Connections
- [[AdminBookingService.cs]] - `contains` [EXTRACTED]
- [[AuthService.cs]] - `contains` [EXTRACTED]
- [[AvailabilityService.cs]] - `contains` [EXTRACTED]
- [[BookingMaintenanceService.cs]] - `imports` [EXTRACTED]
- [[BookingService.cs]] - `contains` [EXTRACTED]
- [[CampSessionService.cs]] - `contains` [EXTRACTED]
- [[DependencyInjection.cs]] - `imports` [EXTRACTED]
- [[EmailTemplates.cs]] - `contains` [EXTRACTED]
- [[PaymentService.cs]] - `contains` [EXTRACTED]
- [[RoomMixCalculator.cs]] - `contains` [EXTRACTED]
- [[RoomMixCalculatorTests.cs]] - `imports` [EXTRACTED]
- [[RoomService.cs]] - `contains` [EXTRACTED]
- [[RoomTaskService.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_DTOs__Models