---
source_file: "src/CampCenter.Application/Services/AdminBookingService.cs"
type: "code"
community: "Application Namespaces & DTOs"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_Namespaces__DTOs
---

# AdminBookingService.cs

## Context

_Source: `src/CampCenter.Application/Services/AdminBookingService.cs` (defined near L1; showing L1–L46 of 346)._

```csharp
using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.Interfaces;
using CampCenter.Application.Models;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;
using Microsoft.Extensions.Logging;
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
```

## Connections
- [[AdminBookingService]] - `contains` [EXTRACTED]
- [[CampCenter.Application.DTOs.AdminPanel]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Models]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Services]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Exceptions]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_Namespaces__DTOs