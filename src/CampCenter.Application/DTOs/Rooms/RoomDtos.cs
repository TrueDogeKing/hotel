namespace CampCenter.Application.DTOs.Rooms;

public record RoomDto(
    Guid Id,
    string Number,
    int Capacity,
    bool IsActive,
    string? Description,
    uint RowVersion
);

public record CreateRoomRequestDto(string Number, int Capacity, string? Description);

/// RowVersion carries the xmin the client last saw; a mismatch means someone
/// else edited the room in the meantime (409).
public record UpdateRoomRequestDto(
    string Number,
    int Capacity,
    bool IsActive,
    string? Description,
    uint RowVersion
);
