---
source_file: "src/CampCenter.Application/DTOs/Public/PublicDtos.cs"
type: "code"
community: "Room Mix Calculator Tests"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Mix_Calculator_Tests
---

# PublicSessionDto

## Context

_Source: `src/CampCenter.Application/DTOs/Public/PublicDtos.cs` (defined near L5; showing L3–L50 of 69)._

```csharp
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
    DateTime CreatedAt,
    DateTime? CompletedAt
);

```

## Connections
- [[.GetPublicSessionsAsync()]] - `references` [EXTRACTED]
- [[.GetPublicSessionsAsync()_1]] - `references` [EXTRACTED]
- [[PublicDtos.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Mix_Calculator_Tests