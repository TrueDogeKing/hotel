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
    private readonly ICampSessionRepository _sessions;
    private readonly IRoomRepository _rooms;
    private readonly IRoomTaskRepository _tasks;
    private readonly IEmailSender _email;
    private readonly BookingSettings _settings;
    private readonly ILogger<AdminBookingService> _logger;

    public AdminBookingService(
        IBookingRepository bookings,
        ICampSessionRepository sessions,
        IRoomRepository rooms,
        IRoomTaskRepository tasks,
        IEmailSender email,
        IOptions<BookingSettings> settings,
        ILogger<AdminBookingService> logger
    )
    {
        _bookings = bookings;
        _sessions = sessions;
        _rooms = rooms;
        _tasks = tasks;
        _email = email;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<List<AdminBookingDto>> ListAsync(
        Guid? campSessionId,
        BookingStatus? status,
        CancellationToken cancellationToken = default
    )
    {
        var bookings = await _bookings.ListAsync(campSessionId, status, cancellationToken);
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
            await _email.SendAsync(
                EmailTemplates.BookingCancelled(booking, booking.CampSession!),
                cancellationToken
            );
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

        // Requested rooms must exist and be free within the session — or already
        // belong to this booking.
        var own = booking.RoomAssignments.Select(a => a.RoomId).ToHashSet();
        var taken = (
            await _bookings.GetLiveAssignedRoomIdsAsync(booking.CampSessionId, cancellationToken)
        )
            .Where(roomId => !own.Contains(roomId))
            .ToHashSet();
        foreach (var entry in request.Assignments)
        {
            var room =
                await _rooms.GetByIdAsync(entry.RoomId, cancellationToken)
                ?? throw new NotFoundException($"Room {entry.RoomId} not found.");
            if (!room.IsActive)
            {
                throw new BusinessRuleViolationException($"Room {room.Number} is inactive.");
            }

            if (taken.Contains(entry.RoomId))
            {
                throw new ConflictException($"Room {room.Number} is already booked.");
            }
        }

        // Diff against the current assignments instead of delete-all + reinsert:
        // EF may order inserts before deletes in one batch, which would trip the
        // unique (session, room) index for rooms the booking keeps.
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
                CampSessionId = booking.CampSessionId,
                PeopleCount = entry.PeopleCount,
            };
            booking.RoomAssignments.Add(assignment);
            await _bookings.AddAssignmentAsync(assignment, cancellationToken);
        }

        await _bookings.SaveChangesAsync(cancellationToken);
        return await GetAsync(id, cancellationToken);
    }

    public async Task<SessionOccupancyDto> GetOccupancyAsync(
        Guid campSessionId,
        CancellationToken cancellationToken = default
    )
    {
        var session =
            await _sessions.GetByIdAsync(campSessionId, cancellationToken)
            ?? throw new NotFoundException("Camp session not found.");

        var rooms = await _rooms.GetAllAsync(cancellationToken);
        var bookings = await _bookings.ListAsync(campSessionId, null, cancellationToken);
        var liveAssignments = bookings
            .Where(b =>
                b.Status
                    is BookingStatus.PendingDeposit
                        or BookingStatus.Confirmed
                        or BookingStatus.Completed
            )
            .SelectMany(b => b.RoomAssignments.Select(a => (Booking: b, Assignment: a)))
            .ToDictionary(x => x.Assignment.RoomId);
        var openTasks = await _tasks.CountOpenByRoomAsync(cancellationToken);

        var roomDtos = rooms
            .Select(room =>
            {
                liveAssignments.TryGetValue(room.Id, out var hit);
                return new RoomOccupancyDto(
                    room.Id,
                    room.Number,
                    room.Capacity,
                    room.IsActive,
                    hit.Booking?.Id,
                    hit.Booking?.OrganizationName,
                    hit.Booking?.Status.ToString(),
                    hit.Assignment?.PeopleCount,
                    openTasks.GetValueOrDefault(room.Id)
                );
            })
            .ToList();

        return new SessionOccupancyDto(
            session.Id,
            session.Name,
            session.StartDate,
            session.EndDate,
            rooms.Where(r => r.IsActive).Sum(r => r.Capacity),
            roomDtos.Sum(r => r.PeopleCount ?? 0),
            roomDtos
        );
    }

    public async Task<DashboardDto> GetDashboardAsync(CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var upcoming = await _sessions.GetPublishedUpcomingAsync(today, cancellationToken);
        var totalBeds = (await _rooms.GetActiveAsync(cancellationToken)).Sum(r => r.Capacity);

        var sessionDtos = new List<DashboardSessionDto>();
        foreach (var session in upcoming)
        {
            var bookings = (await _bookings.ListAsync(session.Id, null, cancellationToken))
                .Where(b => b.Status is BookingStatus.PendingDeposit or BookingStatus.Confirmed)
                .ToList();
            sessionDtos.Add(
                new DashboardSessionDto(
                    session.Id,
                    session.Name,
                    session.StartDate,
                    session.EndDate,
                    totalBeds,
                    bookings.Sum(b => b.RoomAssignments.Sum(a => a.PeopleCount)),
                    bookings.Count
                )
            );
        }

        var pending = await _bookings.ListAsync(
            null,
            BookingStatus.PendingDeposit,
            cancellationToken
        );
        var confirmed = await _bookings.ListAsync(null, BookingStatus.Confirmed, cancellationToken);
        var paid = await _bookings.GetCompletedPaymentKindsAsync(
            confirmed.Select(b => b.Id).ToList(),
            cancellationToken
        );
        var overdue = confirmed.Count(b =>
            b.CampSession is not null
            && !(paid.GetValueOrDefault(b.Id) ?? []).Contains(PaymentKind.Final)
            && b.CampSession.StartDate.AddDays(-_settings.FinalPaymentDueDays) < today
        );

        return new DashboardDto(
            sessionDtos,
            pending.Count,
            overdue,
            await _tasks.CountOpenAsync(cancellationToken)
        );
    }

    private async Task<Booking> GetOrThrowAsync(Guid id, CancellationToken cancellationToken) =>
        await _bookings.GetByIdAsync(id, cancellationToken)
        ?? throw new NotFoundException("Booking not found.");

    private AdminBookingDto ToDto(Booking b, List<PaymentKind> completedKinds)
    {
        var session = b.CampSession!;
        var finalDue = session.StartDate.AddDays(-_settings.FinalPaymentDueDays);
        var depositPaid = completedKinds.Contains(PaymentKind.Deposit);
        var finalPaid = completedKinds.Contains(PaymentKind.Final);
        return new AdminBookingDto(
            b.Id,
            session.Name,
            b.CampSessionId,
            session.StartDate,
            session.EndDate,
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
