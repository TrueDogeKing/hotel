using CampCenter.Application.DTOs.Closures;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;

namespace CampCenter.Application.Services;

public class ClosureService : IClosureService
{
    private readonly IClosureRepository _closures;
    private readonly IRoomRepository _rooms;
    private readonly IBookingRepository _bookings;

    public ClosureService(
        IClosureRepository closures,
        IRoomRepository rooms,
        IBookingRepository bookings
    )
    {
        _closures = closures;
        _rooms = rooms;
        _bookings = bookings;
    }

    public async Task<List<ClosureDto>> GetAllAsync(
        CancellationToken cancellationToken = default
    ) => (await _closures.GetAllAsync(cancellationToken)).Select(ToDto).ToList();

    public async Task<ClosureDto> CreateAsync(
        CreateClosureRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        await GuardRoomExistsAsync(request.RoomId, cancellationToken);
        await GuardNoLiveBookingsAsync(
            request.StartDate,
            request.EndDate,
            request.RoomId,
            cancellationToken
        );

        var closure = new Closure
        {
            Id = Guid.NewGuid(),
            Reason = request.Reason.Trim(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            RoomId = request.RoomId,
            CreatedAt = DateTime.UtcNow,
        };

        await _closures.AddAsync(closure, cancellationToken);
        await _closures.SaveChangesAsync(cancellationToken);
        return ToDto(await GetOrThrowAsync(closure.Id, cancellationToken));
    }

    public async Task<ClosureDto> UpdateAsync(
        Guid id,
        UpdateClosureRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var closure = await GetOrThrowAsync(id, cancellationToken);
        if (closure.RowVersion != request.RowVersion)
        {
            throw new ConcurrencyConflictException(
                "The closure was modified by someone else. Reload and try again."
            );
        }

        await GuardRoomExistsAsync(request.RoomId, cancellationToken);
        await GuardNoLiveBookingsAsync(
            request.StartDate,
            request.EndDate,
            request.RoomId,
            cancellationToken
        );

        closure.Reason = request.Reason.Trim();
        closure.StartDate = request.StartDate;
        closure.EndDate = request.EndDate;
        closure.RoomId = request.RoomId;

        await _closures.SaveChangesAsync(cancellationToken);
        return ToDto(await GetOrThrowAsync(id, cancellationToken));
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var closure = await GetOrThrowAsync(id, cancellationToken);
        _closures.Remove(closure);
        await _closures.SaveChangesAsync(cancellationToken);
    }

    private async Task GuardRoomExistsAsync(Guid? roomId, CancellationToken cancellationToken)
    {
        if (roomId is null)
        {
            return;
        }

        _ =
            await _rooms.GetByIdAsync(roomId.Value, cancellationToken)
            ?? throw new NotFoundException("Room not found.");
    }

    /// A closure must not swallow rooms that a live booking already holds over the
    /// same days — those bookings would be double-committed.
    private async Task GuardNoLiveBookingsAsync(
        DateOnly start,
        DateOnly end,
        Guid? roomId,
        CancellationToken cancellationToken
    )
    {
        // Closure blocks days [start, end]; compare against stays as [start, end+1).
        var bookedRoomIds = await _bookings.GetBookedRoomIdsInRangeAsync(
            start,
            end.AddDays(1),
            excludeBookingId: null,
            cancellationToken
        );

        var conflict = roomId is null
            ? bookedRoomIds.Count > 0
            : bookedRoomIds.Contains(roomId.Value);
        if (conflict)
        {
            throw new BusinessRuleViolationException(
                roomId is null
                    ? "There are active bookings in this range; cancel them before closing the center."
                    : "This room has an active booking in the range and cannot be blocked."
            );
        }
    }

    private async Task<Closure> GetOrThrowAsync(Guid id, CancellationToken cancellationToken) =>
        await _closures.GetByIdAsync(id, cancellationToken)
        ?? throw new NotFoundException("Closure not found.");

    private static ClosureDto ToDto(Closure c) =>
        new(c.Id, c.Reason, c.StartDate, c.EndDate, c.RoomId, c.Room?.Number, c.RowVersion);
}
