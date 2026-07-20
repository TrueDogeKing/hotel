using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Domain.Entities;

namespace CampCenter.Application.Interfaces;

public interface IRoomTaskService
{
    Task<List<RoomTaskDto>> ListAsync(
        RoomTaskStatus? status,
        Guid? campSessionId,
        CancellationToken cancellationToken = default
    );

    Task<RoomTaskDto> CreateAsync(
        CreateRoomTaskRequestDto request,
        Guid createdByAdminUserId,
        CancellationToken cancellationToken = default
    );

    Task<RoomTaskDto> SetStatusAsync(
        Guid id,
        RoomTaskStatus status,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
