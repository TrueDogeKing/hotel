using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;

namespace CampCenter.Application.Services;

public class RoomTaskService : IRoomTaskService
{
    private readonly IRoomTaskRepository _tasks;
    private readonly IRoomRepository _rooms;

    public RoomTaskService(IRoomTaskRepository tasks, IRoomRepository rooms)
    {
        _tasks = tasks;
        _rooms = rooms;
    }

    public async Task<List<RoomTaskDto>> ListAsync(
        RoomTaskStatus? status,
        Guid? campSessionId,
        CancellationToken cancellationToken = default
    ) => (await _tasks.ListAsync(status, campSessionId, cancellationToken)).Select(ToDto).ToList();

    public async Task<RoomTaskDto> CreateAsync(
        CreateRoomTaskRequestDto request,
        Guid createdByAdminUserId,
        CancellationToken cancellationToken = default
    )
    {
        var text = request.Text.Trim();
        if (text.Length is 0 or > 1000)
        {
            throw new BusinessRuleViolationException("Task text must be 1–1000 characters.");
        }

        var room =
            await _rooms.GetByIdAsync(request.RoomId, cancellationToken)
            ?? throw new NotFoundException("Room not found.");

        var task = new RoomTask
        {
            Id = Guid.NewGuid(),
            RoomId = room.Id,
            Room = room,
            CampSessionId = request.CampSessionId,
            BookingId = request.BookingId,
            Text = text,
            CreatedByAdminUserId = createdByAdminUserId,
            CreatedAt = DateTime.UtcNow,
        };

        await _tasks.AddAsync(task, cancellationToken);
        await _tasks.SaveChangesAsync(cancellationToken);
        return ToDto(task);
    }

    public async Task<RoomTaskDto> SetStatusAsync(
        Guid id,
        RoomTaskStatus status,
        CancellationToken cancellationToken = default
    )
    {
        var task =
            await _tasks.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Task not found.");
        task.Status = status;
        task.DoneAt = status == RoomTaskStatus.Done ? DateTime.UtcNow : null;
        await _tasks.SaveChangesAsync(cancellationToken);
        return ToDto(task);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task =
            await _tasks.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Task not found.");
        _tasks.Remove(task);
        await _tasks.SaveChangesAsync(cancellationToken);
    }

    private static RoomTaskDto ToDto(RoomTask t) =>
        new(
            t.Id,
            t.RoomId,
            t.Room?.Number ?? "?",
            t.CampSessionId,
            t.BookingId,
            t.Text,
            t.Status.ToString(),
            t.CreatedAt,
            t.DoneAt
        );
}
