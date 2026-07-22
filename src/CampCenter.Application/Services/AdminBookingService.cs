using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.Interfaces;
using CampCenter.Application.Models;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CampCenter.Application.Services;

public class AdminBookingService : IAdminBookingService
{
    private readonly IBookingRepository _bookings;
    private readonly IRoomRepository _rooms;
    private readonly IRoomTaskRepository _tasks;
    private readonly IClosureRepository _closures;
    private readonly IAvailabilityService _availability;
    private readonly IEmailSender _email;
    private readonly BookingSettings _settings;
    private readonly ILogger<AdminBookingService> _logger;

    public AdminBookingService(
        IBookingRepository bookings,
        IRoomRepository rooms,
        IRoomTaskRepository tasks,
        IClosureRepository closures,
        IAvailabilityService availability,
        IEmailSender email,
        IOptions<BookingSettings> settings,
        ILogger<AdminBookingService> logger
    )
    {
        _bookings = bookings;
        _rooms = rooms;
        _tasks = tasks;
        _closures = closures;
        _availability = availability;
        _email = email;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<List<AdminBookingDto>> ListAsync(
        BookingStatus? status,
        CancellationToken cancellationToken = default
    )
    {
        var bookings = await _bookings.ListAsync(status, cancellationToken);
        var paid = await _bookings.GetCompletedPaymentKindsAsync(
            bookings.Select(b => b.Id).ToList(),
            cancellationToken
        );
        return bookings.Select(b => ToDto(b, paid.GetValueOrDefault(b.Id) ?? [])).ToList();
    }

    public async Task<AdminBookingDto> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var booking = await GetOrThrowAsync(id, cancellationToken);
        var paid = await _bookings.GetCompletedPaymentKindsAsync([id], cancellationToken);
        return ToDto(booking, paid.GetValueOrDefault(id) ?? []);
    }

    public async Task CancelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var booking = await GetOrThrowAsync(id, cancellationToken);
        if (booking.Status is BookingStatus.Cancelled or BookingStatus.Completed)
        {
            throw new BusinessRuleViolationException(
                "Only pending or confirmed bookings can be cancelled."
            );
        }

        booking.Status = BookingStatus.Cancelled;
        booking.CancelReason = BookingCancelReason.ByAdmin;
        booking.HoldExpiresAt = null;
        _bookings.RemoveAssignments(booking);
        await _bookings.SaveChangesAsync(cancellationToken);

        try
        {
            await _email.SendAsync(EmailTemplates.BookingCancelled(booking), cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send admin-cancellation email for booking {BookingId}.",
                booking.Id
            );
        }
    }

    public async Task<AdminBookingDto> ReassignAsync(
        Guid id,
        ReassignBookingRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var booking = await GetOrThrowAsync(id, cancellationToken);
        if (booking.Status is BookingStatus.Cancelled or BookingStatus.Completed)
        {
            throw new BusinessRuleViolationException(
                "Cannot reassign rooms of a cancelled or completed booking."
            );
        }

        if (request.Assignments.Count == 0)
        {
            throw new BusinessRuleViolationException("At least one room is required.");
        }

        if (
            request.Assignments.Select(a => a.RoomId).Distinct().Count()
            != request.Assignments.Count
        )
        {
            throw new BusinessRuleViolationException("Duplicate rooms in the assignment.");
        }

        if (request.Assignments.Any(a => a.PeopleCount < 1))
        {
            throw new BusinessRuleViolationException("Each room must house at least one person.");
        }

        if (request.Assignments.Sum(a => a.PeopleCount) != booking.Headcount)
        {
            throw new BusinessRuleViolationException(
                "People counts must add up to the booking's headcount."
            );
        }

        // Requested rooms must exist and be free over the booking's dates — booked
        // by another booking or blocked by a closure disqualifies them. This
        // booking's own rooms are excluded from the blocked set.
        var blocked = await _availability.GetBlockedRoomIdsAsync(
            booking.StartDate,
            booking.EndDate,
            booking.Id,
            cancellationToken
        );
        foreach (var entry in request.Assignments)
        {
            var room =
                await _rooms.GetByIdAsync(entry.RoomId, cancellationToken)
                ?? throw new NotFoundException($"Room {entry.RoomId} not found.");
            if (!room.IsActive)
            {
                throw new BusinessRuleViolationException($"Room {room.Number} is inactive.");
            }

            if (blocked.Contains(entry.RoomId))
            {
                throw new ConflictException($"Room {room.Number} is unavailable in this range.");
            }
        }

        // Diff against the current assignments instead of delete-all + reinsert:
        // EF may order inserts before deletes in one batch, which would trip the
        // overlap exclusion constraint for rooms the booking keeps.
        var requestedByRoom = request.Assignments.ToDictionary(a => a.RoomId);
        foreach (var existing in booking.RoomAssignments.ToList())
        {
            if (requestedByRoom.TryGetValue(existing.RoomId, out var kept))
            {
                existing.PeopleCount = kept.PeopleCount;
                requestedByRoom.Remove(existing.RoomId);
            }
            else
            {
                booking.RoomAssignments.Remove(existing);
                _bookings.RemoveAssignment(existing);
            }
        }

        foreach (var entry in requestedByRoom.Values)
        {
            var assignment = new BookingRoomAssignment
            {
                Id = Guid.NewGuid(),
                BookingId = booking.Id,
                RoomId = entry.RoomId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                PeopleCount = entry.PeopleCount,
            };
            booking.RoomAssignments.Add(assignment);
            await _bookings.AddAssignmentAsync(assignment, cancellationToken);
        }

        await _bookings.SaveChangesAsync(cancellationToken);
        return await GetAsync(id, cancellationToken);
    }

    public async Task<OccupancyDto> GetOccupancyAsync(
        DateOnly start,
        DateOnly end,
        CancellationToken cancellationToken = default
    )
    {
        if (end <= start)
        {
            throw new BusinessRuleViolationException("The end date must be after the start date.");
        }

        var rooms = await _rooms.GetAllAsync(cancellationToken);
        var bookings = await _bookings.ListLiveInRangeAsync(start, end, cancellationToken);

        // First live booking touching each room over the range (earliest arrival).
        var byRoom = new Dictionary<Guid, (Booking Booking, BookingRoomAssignment Assignment)>();
        foreach (var booking in bookings)
        {
            foreach (var assignment in booking.RoomAssignments)
            {
                byRoom.TryAdd(assignment.RoomId, (booking, assignment));
            }
        }

        var closures = await _closures.GetOverlappingAsync(start, end, cancellationToken);
        var centerClosure = closures.FirstOrDefault(c => c.RoomId is null);
        var roomClosures = closures
            .Where(c => c.RoomId is not null)
            .GroupBy(c => c.RoomId!.Value)
            .ToDictionary(g => g.Key, g => g.First().Reason);

        var openTasks = await _tasks.CountOpenByRoomAsync(cancellationToken);

        var roomDtos = rooms
            .Select(room =>
            {
                byRoom.TryGetValue(room.Id, out var hit);
                var closed = centerClosure is not null || roomClosures.ContainsKey(room.Id);
                var closureReason =
                    centerClosure?.Reason ?? roomClosures.GetValueOrDefault(room.Id);
                return new RoomOccupancyDto(
                    room.Id,
                    room.Number,
                    room.Capacity,
                    room.IsActive,
                    hit.Booking?.Id,
                    hit.Booking?.OrganizationName,
                    hit.Booking?.Status.ToString(),
                    hit.Assignment?.PeopleCount,
                    closed,
                    closureReason,
                    openTasks.GetValueOrDefault(room.Id)
                );
            })
            .ToList();

        return new OccupancyDto(
            start,
            end,
            rooms.Where(r => r.IsActive).Sum(r => r.Capacity),
            roomDtos.Sum(r => r.PeopleCount ?? 0),
            roomDtos
        );
    }

    public async Task<DashboardDto> GetDashboardAsync(CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var upcoming = await _bookings.ListUpcomingAsync(today, 8, cancellationToken);
        var upcomingDtos = upcoming
            .Select(b => new DashboardBookingDto(
                b.Id,
                b.OrganizationName,
                b.StartDate,
                b.EndDate,
                b.Headcount,
                b.RoomAssignments.Sum(a => a.PeopleCount),
                b.Status.ToString()
            ))
            .ToList();

        var pending = await _bookings.ListAsync(BookingStatus.PendingDeposit, cancellationToken);
        var confirmed = await _bookings.ListAsync(BookingStatus.Confirmed, cancellationToken);
        var paid = await _bookings.GetCompletedPaymentKindsAsync(
            confirmed.Select(b => b.Id).ToList(),
            cancellationToken
        );
        var overdue = confirmed.Count(b =>
            !(paid.GetValueOrDefault(b.Id) ?? []).Contains(PaymentKind.Final)
            && b.StartDate.AddDays(-_settings.FinalPaymentDueDays) < today
        );

        var activeClosures = (await _closures.GetAllAsync(cancellationToken)).Count(c =>
            c.EndDate >= today
        );

        return new DashboardDto(
            upcomingDtos,
            pending.Count,
            overdue,
            await _tasks.CountOpenAsync(cancellationToken),
            activeClosures
        );
    }

    private async Task<Booking> GetOrThrowAsync(Guid id, CancellationToken cancellationToken) =>
        await _bookings.GetByIdAsync(id, cancellationToken)
        ?? throw new NotFoundException("Booking not found.");

    private AdminBookingDto ToDto(Booking b, List<PaymentKind> completedKinds)
    {
        var finalDue = b.StartDate.AddDays(-_settings.FinalPaymentDueDays);
        var depositPaid = completedKinds.Contains(PaymentKind.Deposit);
        var finalPaid = completedKinds.Contains(PaymentKind.Final);
        return new AdminBookingDto(
            b.Id,
            b.StartDate,
            b.EndDate,
            b.Nights,
            b.OrganizationName,
            b.ContactName,
            b.Email,
            b.Phone,
            b.Headcount,
            b.Notes,
            b.Status.ToString(),
            b.CancelReason?.ToString(),
            b.TotalGrosze,
            b.DepositGrosze,
            depositPaid,
            finalPaid,
            b.Status == BookingStatus.Confirmed
                && !finalPaid
                && finalDue < DateOnly.FromDateTime(DateTime.UtcNow),
            finalDue,
            b.CreatedAt,
            b.RoomAssignments.OrderBy(a => a.Room?.Number)
                .Select(a => new AdminAssignmentDto(
                    a.Id,
                    a.RoomId,
                    a.Room?.Number ?? "?",
                    a.Room?.Capacity ?? 0,
                    a.PeopleCount
                ))
                .ToList()
        );
    }
}
