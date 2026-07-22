---
source_file: "src/CampCenter.Application/Interfaces/IBookingService.cs"
type: "code"
community: "Public Booking Service"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Service
---

# IBookingService.cs

## Context

_Source: `src/CampCenter.Application/Interfaces/IBookingService.cs` (defined near L1; showing L1–L22 of 22)._

```csharp
using CampCenter.Application.DTOs.Public;

namespace CampCenter.Application.Interfaces;

public interface IBookingService
{
    /// Creates a booking with auto-assigned concrete rooms and emails the manage
    /// link. Retries once when a concurrent booking grabs a selected room.
    Task<CreateBookingResponseDto> CreateAsync(
        CreateBookingRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Booking details for the manage page; token is the plaintext manage token.
    Task<BookingDetailsDto> GetByTokenAsync(
        string token,
        CancellationToken cancellationToken = default
    );

    /// Booker-initiated cancel; allowed only while the deposit is unpaid.
    Task CancelByTokenAsync(string token, CancellationToken cancellationToken = default);
}
```

## Connections
- [[CampCenter.Application.DTOs.Public]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `contains` [EXTRACTED]
- [[IBookingService]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Service