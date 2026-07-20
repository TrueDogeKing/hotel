using System.Text.Json;
using CampCenter.Application.DTOs.Public;
using CampCenter.Application.Interfaces;
using CampCenter.Application.Models;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CampCenter.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookings;
    private readonly ICampSessionRepository _sessions;
    private readonly IRoomRepository _rooms;
    private readonly IAvailabilityService _availability;
    private readonly ITokenService _tokenService;
    private readonly IEmailSender _email;
    private readonly BookingSettings _settings;
    private readonly ILogger<BookingService> _logger;

    public BookingService(
        IBookingRepository bookings,
        ICampSessionRepository sessions,
        IRoomRepository rooms,
        IAvailabilityService availability,
        ITokenService tokenService,
        IEmailSender email,
        IOptions<BookingSettings> settings,
        ILogger<BookingService> logger
    )
    {
        _bookings = bookings;
        _sessions = sessions;
        _rooms = rooms;
        _availability = availability;
        _tokenService = tokenService;
        _email = email;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<CreateBookingResponseDto> CreateAsync(
        CreateBookingRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var session =
            await _sessions.GetByIdAsync(request.CampSessionId, cancellationToken)
            ?? throw new NotFoundException("Camp session not found.");
        if (session.Status != CampSessionStatus.Published)
        {
            throw new BusinessRuleViolationException("The session is not open for booking.");
        }

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        if (session.StartDate <= today)
        {
            throw new BusinessRuleViolationException("The session has already started.");
        }

        try
        {
            return await TryCreateAsync(request, session, cancellationToken);
        }
        catch (ConflictException)
        {
            // A concurrent booking grabbed one of our rooms between the free-room
            // read and the insert; one retry re-reads availability and re-validates.
            return await TryCreateAsync(request, session, cancellationToken);
        }
    }

    private async Task<CreateBookingResponseDto> TryCreateAsync(
        CreateBookingRequestDto request,
        CampSession session,
        CancellationToken cancellationToken
    )
    {
        var free = await _availability.GetFreeRoomsByCapacityAsync(session.Id, cancellationToken);
        var mixError = RoomMixCalculator.ValidateMix(request.Headcount, request.RoomCounts, free);
        if (mixError is not null)
        {
            throw new BusinessRuleViolationException($"Invalid room selection ({mixError}).");
        }

        var now = DateTime.UtcNow;
        var sessionStartUtc = session.StartDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        var holdExpires = now.AddDays(_settings.DepositHoldDays);
        if (holdExpires > sessionStartUtc.AddDays(-1))
        {
            holdExpires = sessionStartUtc.AddDays(-1);
        }

        var token = _tokenService.GenerateRefreshToken(); // 32-byte URL-safe secret + SHA-256 hash

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            CampSessionId = session.Id,
            OrganizationName = request.OrganizationName.Trim(),
            ContactName = request.ContactName.Trim(),
            Email = request.Email.Trim(),
            Phone = request.Phone.Trim(),
            Headcount = request.Headcount,
            Notes = string.IsNullOrWhiteSpace(request.Notes) ? null : request.Notes.Trim(),
            ManageTokenHash = token.TokenHash,
            HoldExpiresAt = holdExpires,
            TotalGrosze = session.PricePerPersonGrosze * request.Headcount,
            DepositGrosze = session.DepositPerPersonGrosze * request.Headcount,
            RequestedRoomCounts = JsonSerializer.Serialize(
                request
                    .RoomCounts.Where(kv => kv.Value > 0)
                    .ToDictionary(kv => kv.Key, kv => kv.Value)
            ),
            Language = request.Language == "en" ? "en" : "pl",
            CreatedAt = now,
        };

        AssignRooms(booking, request, await PickRoomsAsync(session.Id, request, cancellationToken));

        await _bookings.AddAsync(booking, cancellationToken);
        try
        {
            await _bookings.SaveChangesAsync(cancellationToken);
        }
        catch (ConflictException)
        {
            _bookings.Detach(booking);
            throw;
        }

        await SendSafelyAsync(
            EmailTemplates.BookingCreated(
                booking,
                session,
                ManageUrl(token.RawToken),
                FinalDueDate(session)
            ),
            cancellationToken
        );

        return new CreateBookingResponseDto(booking.Id, token.RawToken);
    }

    /// Picks concrete free rooms matching the requested counts: lowest room
    /// numbers first within each capacity.
    private async Task<List<Room>> PickRoomsAsync(
        Guid sessionId,
        CreateBookingRequestDto request,
        CancellationToken cancellationToken
    )
    {
        var active = await _rooms.GetActiveAsync(cancellationToken);
        var assigned = (
            await _bookings.GetLiveAssignedRoomIdsAsync(sessionId, cancellationToken)
        ).ToHashSet();

        var picked = new List<Room>();
        foreach (var (capacity, count) in request.RoomCounts.Where(kv => kv.Value > 0))
        {
            picked.AddRange(
                active
                    .Where(r => r.Capacity == capacity && !assigned.Contains(r.Id))
                    .OrderBy(r => r.Number, StringComparer.OrdinalIgnoreCase)
                    .Take(count)
            );
        }

        return picked;
    }

    private static void AssignRooms(
        Booking booking,
        CreateBookingRequestDto request,
        List<Room> rooms
    )
    {
        // Distribute people: full rooms first (largest capacity first), remainder in the last.
        var loads = RoomMixCalculator.DistributePeople(request.Headcount, request.RoomCounts);
        var byCapacity = rooms
            .GroupBy(r => r.Capacity)
            .ToDictionary(g => g.Key, g => new Queue<Room>(g));

        foreach (var (capacity, peopleCount) in loads)
        {
            var room = byCapacity[capacity].Dequeue();
            booking.RoomAssignments.Add(
                new BookingRoomAssignment
                {
                    Id = Guid.NewGuid(),
                    BookingId = booking.Id,
                    RoomId = room.Id,
                    CampSessionId = booking.CampSessionId,
                    PeopleCount = peopleCount,
                }
            );
        }
    }

    public async Task<BookingDetailsDto> GetByTokenAsync(
        string token,
        CancellationToken cancellationToken = default
    )
    {
        var booking = await FindByTokenAsync(token, cancellationToken);
        var payments = await _bookings.GetPaymentsAsync(booking.Id, cancellationToken);
        var session = booking.CampSession!;

        return new BookingDetailsDto(
            booking.Id,
            booking.Status.ToString(),
            booking.CancelReason?.ToString(),
            session.Name,
            session.StartDate,
            session.EndDate,
            booking.OrganizationName,
            booking.ContactName,
            booking.Email,
            booking.Phone,
            booking.Headcount,
            JsonSerializer.Deserialize<Dictionary<int, int>>(booking.RequestedRoomCounts) ?? [],
            booking.TotalGrosze,
            booking.DepositGrosze,
            booking.Status == BookingStatus.PendingDeposit ? booking.HoldExpiresAt : null,
            FinalDueDate(session),
            payments
                .Select(p => new BookingPaymentDto(
                    p.Id,
                    p.Kind.ToString(),
                    p.Status.ToString(),
                    p.AmountGrosze,
                    p.CreatedAt,
                    p.CompletedAt
                ))
                .ToList()
        );
    }

    public async Task CancelByTokenAsync(
        string token,
        CancellationToken cancellationToken = default
    )
    {
        var booking = await FindByTokenAsync(token, cancellationToken);
        if (booking.Status != BookingStatus.PendingDeposit)
        {
            throw new BusinessRuleViolationException(
                "Only bookings awaiting the deposit can be cancelled online. Contact the center."
            );
        }

        booking.Status = BookingStatus.Cancelled;
        booking.CancelReason = BookingCancelReason.ByBooker;
        booking.HoldExpiresAt = null;
        _bookings.RemoveAssignments(booking);
        await _bookings.SaveChangesAsync(cancellationToken);

        await SendSafelyAsync(
            EmailTemplates.BookingCancelled(booking, booking.CampSession!),
            cancellationToken
        );
    }

    private async Task<Booking> FindByTokenAsync(string token, CancellationToken cancellationToken)
    {
        var hash = _tokenService.HashRefreshToken(token);
        return await _bookings.GetByTokenHashAsync(hash, cancellationToken)
            ?? throw new NotFoundException("Booking not found.");
    }

    private DateOnly FinalDueDate(CampSession session) =>
        session.StartDate.AddDays(-_settings.FinalPaymentDueDays);

    private string ManageUrl(string rawToken) =>
        $"{_settings.PublicBaseUrl.TrimEnd('/')}/rezerwacja/{rawToken}";

    /// Email failures must never lose a booking that is already persisted.
    private async Task SendSafelyAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        try
        {
            await _email.SendAsync(message, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send booking email to {Email}.", message.To);
        }
    }
}
