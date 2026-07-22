---
source_file: "src/CampCenter.Application/Interfaces/IBookingService.cs"
type: "code"
community: "Public Booking Service"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Service
---

# IBookingService

## Context

_Source: `src/CampCenter.Application/Interfaces/IBookingService.cs` (defined near L5; showing L3–L22 of 22)._

```csharp
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
- [[.CancelByTokenAsync()]] - `method` [EXTRACTED]
- [[.CreateAsync()]] - `method` [EXTRACTED]
- [[.GetByTokenAsync()]] - `method` [EXTRACTED]
- [[BookingService]] - `implements` [EXTRACTED]
- [[IBookingService.cs]] - `contains` [EXTRACTED]
- [[PublicBookingsController]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Service