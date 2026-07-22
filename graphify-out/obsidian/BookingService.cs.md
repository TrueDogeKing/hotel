---
source_file: "src/CampCenter.Application/Services/BookingService.cs"
type: "code"
community: "Auth DTOs & Models"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_DTOs__Models
---

# BookingService.cs

## Context

_Source: `src/CampCenter.Application/Services/BookingService.cs` (defined near L1; showing L1–L46 of 305)._

```csharp
using System.Text.Json;
using CampCenter.Application.DTOs.Public;
using CampCenter.Application.Interfaces;
using CampCenter.Application.Models;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
```

## Connections
- [[BookingService]] - `contains` [EXTRACTED]
- [[CampCenter.Application.DTOs.Public]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Models]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Services]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Exceptions]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_DTOs__Models