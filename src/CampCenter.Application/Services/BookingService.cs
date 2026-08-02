using System.Text.Json;
using CampCenter.Application.DTOs.Public;
using CampCenter.Application.DTOs.Schedule;
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
    private readonly IRoomRepository _rooms;
    private readonly IAvailabilityService _availability;
    private readonly ITokenService _tokenService;
    private readonly IEmailSender _email;
    private readonly IScheduleEntryRepository _scheduleEntries;
    private readonly BookingSettings _settings;
    private readonly ILogger<BookingService> _logger;

    public BookingService(
        IBookingRepository bookings,
        IRoomRepository rooms,
        IAvailabilityService availability,
        ITokenService tokenService,
        IEmailSender email,
        IScheduleEntryRepository scheduleEntries,
        IOptions<BookingSettings> settings,
        ILogger<BookingService> logger
    )
    {
        _bookings = bookings;
        _rooms = rooms;
        _availability = availability;
        _tokenService = tokenService;
        _email = email;
        _scheduleEntries = scheduleEntries;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<CreateBookingResponseDto> CreateAsync(
        CreateBookingRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        if (request.StartDate <= today)
        {
            throw new BusinessRuleViolationException("The stay must start in the future.");
        }

        if (request.EndDate <= request.StartDate)
        {
            throw new BusinessRuleViolationException("The departure date must be after arrival.");
        }

        if (request.EndDate.DayNumber - request.StartDate.DayNumber > _settings.MaxNights)
        {
            throw new BusinessRuleViolationException(
                $"A stay may be at most {_settings.MaxNights} nights."
            );
        }

        var centerClosed = await _availability.GetCenterClosureReasonAsync(
            request.StartDate,
            request.EndDate,
            cancellationToken
        );
        if (centerClosed is not null)
        {
            throw new BusinessRuleViolationException(
                $"The center is closed in the selected range ({centerClosed})."
            );
        }

        try
        {
            return await TryCreateAsync(request, cancellationToken);
        }
        catch (ConflictException)
        {
            // A concurrent booking grabbed one of our rooms between the free-room
            // read and the insert; one retry re-reads availability and re-validates.
            return await TryCreateAsync(request, cancellationToken);
        }
    }

    private async Task<CreateBookingResponseDto> TryCreateAsync(
        CreateBookingRequestDto request,
        CancellationToken cancellationToken
    )
    {
        var free = await _availability.GetFreeRoomsByCapacityAsync(
            request.StartDate,
            request.EndDate,
            null,
            cancellationToken
        );
        var mixError = RoomMixCalculator.ValidateMix(request.Headcount, request.RoomCounts, free);
        if (mixError is not null)
        {
            throw new BusinessRuleViolationException($"Invalid room selection ({mixError}).");
        }

        var nights = request.EndDate.DayNumber - request.StartDate.DayNumber;
        var now = DateTime.UtcNow;
        var startUtc = request.StartDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        var holdExpires = now.AddDays(_settings.DepositHoldDays);
        if (holdExpires > startUtc.AddDays(-1))
        {
            holdExpires = startUtc.AddDays(-1);
        }

        var token = _tokenService.GenerateRefreshToken(); // 32-byte URL-safe secret + SHA-256 hash

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            OrganizationName = request.OrganizationName.Trim(),
            ContactName = request.ContactName.Trim(),
            Email = request.Email.Trim(),
            Phone = request.Phone.Trim(),
            Headcount = request.Headcount,
            Notes = string.IsNullOrWhiteSpace(request.Notes) ? null : request.Notes.Trim(),
            ManageTokenHash = token.TokenHash,
            HoldExpiresAt = holdExpires,
            TotalGrosze = _settings.PricePerPersonPerNightGrosze * request.Headcount * nights,
            DepositGrosze = _settings.DepositPerPersonPerNightGrosze * request.Headcount * nights,
            RequestedRoomCounts = JsonSerializer.Serialize(
                request
                    .RoomCounts.Where(kv => kv.Value > 0)
                    .ToDictionary(kv => kv.Key, kv => kv.Value)
            ),
            Language = request.Language == "en" ? "en" : "pl",
            CreatedAt = now,
        };

        AssignRooms(booking, request, await PickRoomsAsync(request, cancellationToken));

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
            EmailTemplates.BookingCreated(booking, ManageUrl(token.RawToken), FinalDueDate(booking)),
            cancellationToken
        );

        return new CreateBookingResponseDto(booking.Id, token.RawToken);
    }

    /// Picks concrete free rooms matching the requested counts: lowest room
    /// numbers first within each capacity.
    private async Task<List<Room>> PickRoomsAsync(
        CreateBookingRequestDto request,
        CancellationToken cancellationToken
    )
    {
        var active = await _rooms.GetActiveAsync(cancellationToken);
        var blocked = await _availability.GetBlockedRoomIdsAsync(
            request.StartDate,
            request.EndDate,
            null,
            cancellationToken
        );

        var picked = new List<Room>();
        foreach (var (capacity, count) in request.RoomCounts.Where(kv => kv.Value > 0))
        {
            picked.AddRange(
                active
                    .Where(r => r.Capacity == capacity && !blocked.Contains(r.Id))
                    .OrderBy(r => r.Number, RoomNumberComparer.Instance)
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
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate,
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

        return new BookingDetailsDto(
            booking.Id,
            booking.Status.ToString(),
            booking.CancelReason?.ToString(),
            booking.StartDate,
            booking.EndDate,
            booking.Nights,
            booking.OrganizationName,
            booking.ContactName,
            booking.Email,
            booking.Phone,
            booking.Headcount,
            JsonSerializer.Deserialize<Dictionary<int, int>>(booking.RequestedRoomCounts) ?? [],
            booking.TotalGrosze,
            booking.DepositGrosze,
            booking.Status == BookingStatus.PendingDeposit ? booking.HoldExpiresAt : null,
            FinalDueDate(booking),
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

    public async Task<PublicScheduleDto> GetScheduleByTokenAsync(
        string token,
        CancellationToken cancellationToken = default
    )
    {
        // Token hashing stays in the one place that already does it.
        var booking = await FindByTokenAsync(token, cancellationToken);
        var entries = await _scheduleEntries.ListForBookingAsync(booking.Id, cancellationToken);
        return ScheduleService.ToPublicDto(booking, entries);
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

        await SendSafelyAsync(EmailTemplates.BookingCancelled(booking), cancellationToken);
    }

    private async Task<Booking> FindByTokenAsync(string token, CancellationToken cancellationToken)
    {
        var hash = _tokenService.HashRefreshToken(token);
        return await _bookings.GetByTokenHashAsync(hash, cancellationToken)
            ?? throw new NotFoundException("Booking not found.");
    }

    private DateOnly FinalDueDate(Booking booking) =>
        booking.StartDate.AddDays(-_settings.FinalPaymentDueDays);

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
