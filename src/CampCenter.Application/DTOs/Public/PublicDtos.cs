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
