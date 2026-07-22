---
source_file: "src/CampCenter.Application/DTOs/Public/PublicDtos.cs"
type: "code"
community: "Public Booking Service"
location: "L18"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Service
---

# CreateBookingRequestDto

## Context

_Source: `src/CampCenter.Application/DTOs/Public/PublicDtos.cs` (defined near L18; showing L16–L63 of 69)._

```csharp
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
```

## Connections
- [[.AssignRooms()]] - `references` [EXTRACTED]
- [[.BookingRequest()]] - `references` [EXTRACTED]
- [[.Create()_3]] - `references` [EXTRACTED]
- [[.CreateAsync()]] - `references` [EXTRACTED]
- [[.CreateAsync()_4]] - `references` [EXTRACTED]
- [[.PickRoomsAsync()]] - `references` [EXTRACTED]
- [[.TryCreateAsync()]] - `references` [EXTRACTED]
- [[CreateBookingRequestValidator]] - `references` [EXTRACTED]
- [[PublicDtos.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Service