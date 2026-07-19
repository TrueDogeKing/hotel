using CampCenter.Application.DTOs.Rooms;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;

namespace CampCenter.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _rooms;

    public RoomService(IRoomRepository rooms) => _rooms = rooms;

    public async Task<List<RoomDto>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await _rooms.GetAllAsync(cancellationToken)).Select(ToDto).ToList();

    public async Task<RoomDto> CreateAsync(
        CreateRoomRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var number = request.Number.Trim();
        if (await _rooms.GetByNumberAsync(number, cancellationToken) is not null)
        {
            throw new ConflictException("A room with this number already exists.");
        }

        var room = new Room
        {
            Id = Guid.NewGuid(),
            Number = number,
            Capacity = request.Capacity,
            Description = request.Description,
        };

        await _rooms.AddAsync(room, cancellationToken);
        await _rooms.SaveChangesAsync(cancellationToken);
        return ToDto(room);
    }

    public async Task<RoomDto> UpdateAsync(
        Guid id,
        UpdateRoomRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var room =
            await _rooms.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Room not found.");

        var number = request.Number.Trim();
        var duplicate = await _rooms.GetByNumberAsync(number, cancellationToken);
        if (duplicate is not null && duplicate.Id != id)
        {
            throw new ConflictException("A room with this number already exists.");
        }

        // Capacity changes are blocked once the room has assignment history:
        // past bookings were priced and allocated against the old capacity.
        if (
            room.Capacity != request.Capacity
            && await _rooms.HasAssignmentsAsync(id, cancellationToken)
        )
        {
            throw new BusinessRuleViolationException(
                "Cannot change the capacity of a room that has booking assignments."
            );
        }

        room.Number = number;
        room.Capacity = request.Capacity;
        room.IsActive = request.IsActive;
        room.Description = request.Description;

        await SaveWithConcurrencyCheckAsync(room, request.RowVersion, cancellationToken);
        return ToDto(room);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var room =
            await _rooms.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Room not found.");

        if (await _rooms.HasAssignmentsAsync(id, cancellationToken))
        {
            // History must survive: deactivate instead of deleting.
            room.IsActive = false;
            await _rooms.SaveChangesAsync(cancellationToken);
            return false;
        }

        _rooms.Remove(room);
        await _rooms.SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task SaveWithConcurrencyCheckAsync(
        Room room,
        uint expectedRowVersion,
        CancellationToken cancellationToken
    )
    {
        if (room.RowVersion != expectedRowVersion)
        {
            throw new ConcurrencyConflictException(
                "The room was modified by someone else. Reload and try again."
            );
        }

        await _rooms.SaveChangesAsync(cancellationToken);
    }

    private static RoomDto ToDto(Room room) =>
        new(room.Id, room.Number, room.Capacity, room.IsActive, room.Description, room.RowVersion);
}
