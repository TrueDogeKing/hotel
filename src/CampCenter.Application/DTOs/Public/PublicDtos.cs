namespace CampCenter.Application.DTOs.Public;

/// A published session as the public booking flow sees it. When a headcount was
/// given, Fits/SuggestedMix say whether and how the group can be housed.
public record PublicSessionDto(
    Guid Id,
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    long PricePerPersonGrosze,
    long DepositPerPersonGrosze,
    int RemainingCapacity,
    Dictionary<int, int> FreeRoomsByCapacity,
    bool? Fits,
    Dictionary<int, int>? SuggestedMix
);

public record CreateBookingRequestDto(
    Guid CampSessionId,
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
    string SessionName,
    DateOnly StartDate,
    DateOnly EndDate,
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
