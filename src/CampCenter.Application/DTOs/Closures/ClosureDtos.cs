namespace CampCenter.Application.DTOs.Closures;

public record ClosureDto(
    Guid Id,
    string Reason,
    DateOnly StartDate,
    DateOnly EndDate,
    /// Null = whole center is closed; otherwise the blocked room.
    Guid? RoomId,
    string? RoomNumber,
    uint RowVersion
);

public record CreateClosureRequestDto(
    string Reason,
    DateOnly StartDate,
    DateOnly EndDate,
    Guid? RoomId
);

public record UpdateClosureRequestDto(
    string Reason,
    DateOnly StartDate,
    DateOnly EndDate,
    Guid? RoomId,
    uint RowVersion
);
