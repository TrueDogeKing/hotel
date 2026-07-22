---
source_file: "src/CampCenter.Application/DTOs/Public/PublicDtos.cs"
type: "code"
community: "Public Booking Service"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Service
---

# PublicDtos.cs

## Context

_Source: `src/CampCenter.Application/DTOs/Public/PublicDtos.cs` (defined near L1; showing L1–L46 of 69)._

```csharp
namespace CampCenter.Application.DTOs.Public;

/// Availability for a requested date range. When a headcount was given,
/// Fits/SuggestedMix say whether and how the group can be housed, and the
/// Total/Deposit amounts are computed for that headcount over the nights.
public record AvailabilityDto(
    DateOnly StartDate,
    DateOnly EndDate,
    int Nights,
    /// True when the whole center is closed for part of the range (no booking possible).
    bool CenterClosed,
    string? CenterClosedReason,
    long PricePerPersonPerNightGrosze,
    long DepositPerPersonPerNightGrosze,
    int RemainingCapacity,
    Dictionary<int, int> FreeRoomsByCapacity,
    bool? Fits,
    Dictionary<int, int>? SuggestedMix,
    long? TotalGrosze,
    long? DepositGrosze
);

/// A center-wide closure as the public site advertises it.
public record PublicClosureDto(string Reason, DateOnly StartDate, DateOnly EndDate);

public record CreateBookingRequestDto(
    DateOnly StartDate,
    DateOnly EndDate,
    int Headcount,
    Dictionary<int, int> RoomCounts,
    string OrganizationName,
    string ContactName,
    string Email,
    string Phone,
    string? Notes,
    string Language
);

/// The manage token is returned exactly once, here; only its hash is stored.
public record CreateBookingResponseDto(Guid BookingId, string ManageToken);

public record BookingPaymentDto(
    Guid Id,
    string Kind,
    string Status,
    long AmountGrosze,
```

## Connections
- [[BookingDetailsDto]] - `contains` [EXTRACTED]
- [[BookingPaymentDto]] - `contains` [EXTRACTED]
- [[CampCenter.Application.DTOs.Public]] - `contains` [EXTRACTED]
- [[CreateBookingRequestDto]] - `contains` [EXTRACTED]
- [[CreateBookingResponseDto]] - `contains` [EXTRACTED]
- [[PublicSessionDto]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Service