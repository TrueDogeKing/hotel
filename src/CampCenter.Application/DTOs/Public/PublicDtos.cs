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

/// One night, as the booking calendar needs to draw it: whether a group could
/// sleep here, and why not when it could not.
///
/// The date is the night *starting* on it. A stay's departure day is not a night,
/// so the calendar may accept a closed day as a checkout even though it is
/// unavailable as an arrival.
public record AvailabilityDayDto(
    DateOnly Date,
    /// The whole center is closed that night; nothing can be booked.
    bool Closed,
    string? ClosureReason,
    /// Beds in rooms that are neither booked nor blocked that night.
    int FreeBeds,
    /// Enough free beds for the headcount asked about. True for any open night
    /// with a bed left when no headcount was given.
    bool Fits
);

/// Night-by-night availability over a span, for the booking calendar.
public record AvailabilityCalendarDto(
    DateOnly Start,
    DateOnly End,
    int? Headcount,
    List<AvailabilityDayDto> Days
);

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
