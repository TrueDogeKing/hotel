---
source_file: "src/CampCenter.Application/Services/BookingService.cs"
type: "code"
community: "Public Booking Service"
location: "L13"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Service
---

# BookingService

## Context

_Source: `src/CampCenter.Application/Services/BookingService.cs` (defined near L13; showing L11–L58 of 305)._

```csharp
namespace CampCenter.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookings;
    private readonly IRoomRepository _rooms;
    private readonly IAvailabilityService _availability;
    private readonly ITokenService _tokenService;
    private readonly IEmailSender _email;
    private readonly BookingSettings _settings;
    private readonly ILogger<BookingService> _logger;

    public BookingService(
        IBookingRepository bookings,
        IRoomRepository rooms,
        IAvailabilityService availability,
        ITokenService tokenService,
        IEmailSender email,
        IOptions<BookingSettings> settings,
        ILogger<BookingService> logger
    )
    {
        _bookings = bookings;
        _rooms = rooms;
        _availability = availability;
        _tokenService = tokenService;
        _email = email;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<CreateBookingResponseDto> CreateAsync(
        CreateBookingRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        if (request.StartDate <= today)
        {
            throw new BusinessRuleViolationException("The stay must start in the future.");
        }

        if (request.EndDate <= request.StartDate)
        {
            throw new BusinessRuleViolationException("The departure date must be after arrival.");
        }

        if (request.EndDate.DayNumber - request.StartDate.DayNumber > _settings.MaxNights)
```

## Connections
- [[.AssignRooms()]] - `method` [EXTRACTED]
- [[.CancelByTokenAsync()_1]] - `method` [EXTRACTED]
- [[.CreateAsync()_4]] - `method` [EXTRACTED]
- [[.FinalDueDate()]] - `method` [EXTRACTED]
- [[.FindByTokenAsync()]] - `method` [EXTRACTED]
- [[.GetByTokenAsync()_1]] - `method` [EXTRACTED]
- [[.ManageUrl()]] - `method` [EXTRACTED]
- [[.PickRoomsAsync()]] - `method` [EXTRACTED]
- [[.SendSafelyAsync()]] - `method` [EXTRACTED]
- [[.TryCreateAsync()]] - `method` [EXTRACTED]
- [[BookingService.cs]] - `contains` [EXTRACTED]
- [[BookingSettings]] - `references` [EXTRACTED]
- [[IAvailabilityService]] - `references` [EXTRACTED]
- [[IBookingRepository]] - `references` [EXTRACTED]
- [[IBookingService]] - `implements` [EXTRACTED]
- [[ICampSessionRepository_2]] - `references` [EXTRACTED]
- [[IEmailSender]] - `references` [EXTRACTED]
- [[ILogger_3]] - `references` [EXTRACTED]
- [[IRoomRepository]] - `references` [EXTRACTED]
- [[ITokenService]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Service