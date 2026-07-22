---
source_file: "src/CampCenter.Application/Services/AdminBookingService.cs"
type: "code"
community: "Admin Booking & Notifications"
location: "L12"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Booking__Notifications
---

# AdminBookingService

## Context

_Source: `src/CampCenter.Application/Services/AdminBookingService.cs` (defined near L12; showing L10–L57 of 346)._

```csharp
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

    public async Task<AdminBookingDto> GetAsync(
```

## Connections
- [[.CancelAsync()_1]] - `method` [EXTRACTED]
- [[.GetAsync()_1]] - `method` [EXTRACTED]
- [[.GetDashboardAsync()_1]] - `method` [EXTRACTED]
- [[.GetOccupancyAsync()_1]] - `method` [EXTRACTED]
- [[.GetOrThrowAsync()]] - `method` [EXTRACTED]
- [[.ListAsync()_2]] - `method` [EXTRACTED]
- [[.ReassignAsync()_1]] - `method` [EXTRACTED]
- [[.ToDto()]] - `method` [EXTRACTED]
- [[AdminBookingService.cs]] - `contains` [EXTRACTED]
- [[BookingSettings]] - `references` [EXTRACTED]
- [[IAdminBookingService]] - `implements` [EXTRACTED]
- [[IBookingRepository]] - `references` [EXTRACTED]
- [[ICampSessionRepository]] - `references` [EXTRACTED]
- [[IEmailSender]] - `references` [EXTRACTED]
- [[ILogger_2]] - `references` [EXTRACTED]
- [[IRoomRepository]] - `references` [EXTRACTED]
- [[IRoomTaskRepository]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Booking__Notifications