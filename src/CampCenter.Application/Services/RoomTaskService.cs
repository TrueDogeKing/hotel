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
        Guid? bookingId,
        CancellationToken cancellationToken = default
    ) => (await _tasks.ListAsync(status, bookingId, cancellationToken)).Select(ToDto).ToList();

    public async Task<RoomTaskDto> CreateAsync(
        CreateRoomTaskRequestDto request,
        Guid createdByAdminUserId,
        CancellationToken cancellationToken = default
    )
    {
        var text = NormalizeText(request.Text);
        var room = await ResolveRoomAsync(request.RoomId, cancellationToken);

        var task = new RoomTask
        {
            Id = Guid.NewGuid(),
            RoomId = room?.Id,
            Room = room,
            BookingId = request.BookingId,
            Text = text,
            CreatedByAdminUserId = createdByAdminUserId,
            CreatedAt = DateTime.UtcNow,
        };

        await _tasks.AddAsync(task, cancellationToken);
        await _tasks.SaveChangesAsync(cancellationToken);
        return ToDto(task);
    }

    public async Task<RoomTaskDto> UpdateAsync(
        Guid id,
        UpdateRoomTaskRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var text = NormalizeText(request.Text);
        var room = await ResolveRoomAsync(request.RoomId, cancellationToken);

        var task =
            await _tasks.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Task not found.");
        task.Text = text;
        task.RoomId = room?.Id;
        task.Room = room;
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

    private static string NormalizeText(string text)
    {
        var trimmed = text.Trim();
        return trimmed.Length is 0 or > 1000
            ? throw new BusinessRuleViolationException("Task text must be 1–1000 characters.")
            : trimmed;
    }

    private async Task<Room?> ResolveRoomAsync(Guid? roomId, CancellationToken cancellationToken)
    {
        if (roomId is not { } id)
        {
            return null;
        }

        return await _rooms.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Room not found.");
    }

    private static RoomTaskDto ToDto(RoomTask t) =>
        new(
            t.Id,
            t.RoomId,
            t.Room?.Number,
            t.BookingId,
            t.Text,
            t.Status.ToString(),
            t.CreatedAt,
            t.DoneAt
        );
}
