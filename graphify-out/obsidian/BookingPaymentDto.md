---
source_file: "src/CampCenter.Application/DTOs/Public/PublicDtos.cs"
type: "code"
community: "Public Booking Service"
location: "L33"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Service
---

# BookingPaymentDto

## Context

_Source: `src/CampCenter.Application/DTOs/Public/PublicDtos.cs` (defined near L33; showing L31–L69 of 69)._

```csharp
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
    DateTime CreatedAt,
    DateTime? CompletedAt
);

public record BookingDetailsDto(
    Guid Id,
    string Status,
    string? CancelReason,
    DateOnly StartDate,
    DateOnly EndDate,
    int Nights,
    string OrganizationName,
    string ContactName,
    string Email,
    string Phone,
    int Headcount,
    Dictionary<int, int> RoomCounts,
    long TotalGrosze,
    long DepositGrosze,
    DateTime? HoldExpiresAt,
    DateOnly FinalPaymentDueDate,
    List<BookingPaymentDto> Payments
);
```

## Connections
- [[PublicDtos.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Service