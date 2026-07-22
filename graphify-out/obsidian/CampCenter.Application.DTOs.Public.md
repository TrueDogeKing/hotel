---
source_file: "src/CampCenter.Application/DTOs/Public/PublicDtos.cs"
type: "code"
community: "Application DTO Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_DTO_Namespaces
---

# CampCenter.Application.DTOs.Public

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
- [[AdminPanelApiTests.cs]] - `imports` [EXTRACTED]
- [[AvailabilityService.cs]] - `imports` [EXTRACTED]
- [[BookingService.cs]] - `imports` [EXTRACTED]
- [[CreateBookingRequestValidator.cs]] - `imports` [EXTRACTED]
- [[IAvailabilityService.cs]] - `imports` [EXTRACTED]
- [[IBookingService.cs]] - `imports` [EXTRACTED]
- [[IPaymentService.cs]] - `imports` [EXTRACTED]
- [[PaymentsApiTests.cs]] - `imports` [EXTRACTED]
- [[PublicBookingApiTests.cs]] - `imports` [EXTRACTED]
- [[PublicBookingsController.cs]] - `imports` [EXTRACTED]
- [[PublicDtos.cs]] - `contains` [EXTRACTED]
- [[PublicSessionsController.cs]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_DTO_Namespaces