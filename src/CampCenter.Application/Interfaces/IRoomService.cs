using CampCenter.Application.DTOs.Rooms;

namespace CampCenter.Application.Interfaces;

public interface IRoomService
{
    Task<List<RoomDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<RoomDto> CreateAsync(
        CreateRoomRequestDto request,
        CancellationToken cancellationToken = default
    );

    Task<RoomDto> UpdateAsync(
        Guid id,
        UpdateRoomRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Hard-deletes an unreferenced room; a room with assignment history is
    /// deactivated instead (returns false to signal deactivation, true for delete).
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
