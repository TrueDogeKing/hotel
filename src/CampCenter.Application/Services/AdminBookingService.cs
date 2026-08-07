using System.Text.Json;
using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.DTOs.Schedule;
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
    private readonly ITokenService _tokenService;
    private readonly IScheduleService _schedule;
    private readonly IPricingService _pricing;
    private readonly BookingSettings _settings;
    private readonly ILogger<AdminBookingService> _logger;

    public AdminBookingService(
        IBookingRepository bookings,
        IRoomRepository rooms,
        IRoomTaskRepository tasks,
        IClosureRepository closures,
        IAvailabilityService availability,
        IEmailSender email,
        ITokenService tokenService,
        IScheduleService schedule,
        IPricingService pricing,
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
        _tokenService = tokenService;
        _schedule = schedule;
        _pricing = pricing;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<List<AdminBookingDto>> ListAsync(
        BookingStatus? status,
        CancellationToken cancellationToken = default
    )
    {
        var bookings = await _bookings.ListAsync(status, cancellationToken);
        return bookings.Select(ToDto).ToList();
    }

    public async Task<AdminBookingDto> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default
    ) => ToDto(await GetOrThrowAsync(id, cancellationToken));

    public async Task<AdminBookingDto> CreateAsync(
        CreateAdminBookingRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
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

        // Unlike the public wizard this deliberately allows a start date in the
        // past: staff record groups that already arrived, and backfill old ones.
        var status = ParseStatus(request.Status) ?? BookingStatus.Confirmed;

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

        var free = await _availability.GetFreeRoomsByCapacityAsync(
            request.StartDate,
            request.EndDate,
            null,
            cancellationToken
        );
        var supervisors = request.SupervisorCount;
        var campers = request.Headcount - supervisors;
        var split =
            RoomMixCalculator.SuggestSplitMix(campers, supervisors, free)
            ?? throw new ConflictException(
                supervisors > 0
                    ? "The free rooms cannot house this group with the supervisors in rooms of their own."
                    : "The free rooms cannot house this group over the selected range."
            );

        var nights = request.EndDate.DayNumber - request.StartDate.DayNumber;
        var rates = await _pricing.GetAsync(cancellationToken);
        var camperRate = request.PricePerPersonPerNightGrosze ?? rates.PricePerPersonPerNightGrosze;
        var supervisorRate =
            request.SupervisorPricePerPersonPerNightGrosze
            ?? rates.SupervisorPricePerPersonPerNightGrosze;
        var total =
            request.TotalGrosze
            ?? (camperRate * campers * nights) + (supervisorRate * supervisors * nights);
        var deposit =
            request.DepositGrosze
            ?? rates.DepositPerPersonPerNightGrosze * request.Headcount * nights;
        GuardPricing(camperRate, supervisorRate, total, deposit);
        var token = _tokenService.GenerateRefreshToken();
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
            Status = status,
            // A manage token is minted so the group can still be given a
            // self-service link later; nothing is emailed here.
            ManageTokenHash = token.TokenHash,
            HoldExpiresAt =
                status == BookingStatus.PendingDeposit
                    ? ComputeHoldExpiry(request.StartDate)
                    : null,
            SupervisorCount = supervisors,
            // Taken from the request where the owner set one, otherwise from the
            // centre's current rates, and snapshotted here either way: this one
            // group can be re-priced later without touching any other.
            PricePerPersonPerNightGrosze = camperRate,
            SupervisorPricePerPersonPerNightGrosze = supervisorRate,
            TotalGrosze = total,
            DepositGrosze = deposit,
            RequestedRoomCounts = JsonSerializer.Serialize(split.Combined),
            Language = request.Language == "en" ? "en" : "pl",
            CreatedAt = DateTime.UtcNow,
        };

        booking.RoomAssignments.AddRange(
            await BuildAssignmentsAsync(booking, split, cancellationToken)
        );

        await _bookings.AddAsync(booking, cancellationToken);
        try
        {
            await _bookings.SaveChangesAsync(cancellationToken);
        }
        catch (ConflictException)
        {
            // A concurrent booking grabbed one of the picked rooms between the
            // availability read and the insert.
            _bookings.Detach(booking);
            throw;
        }

        // Fill the stay's meals straight away, so a new group arrives with its
        // programme already laid out instead of waiting on a manual generate step.
        await GenerateMealsSafelyAsync(booking.Id, "admin-created", cancellationToken);

        return await GetAsync(booking.Id, cancellationToken);
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

        await ApplyCancellationAsync(booking, cancellationToken);
    }

    public async Task<AdminBookingDto> SetStatusAsync(
        Guid id,
        SetBookingStatusRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var booking = await GetOrThrowAsync(id, cancellationToken);
        var target =
            ParseStatus(request.Status)
            ?? throw new BusinessRuleViolationException(
                $"Unknown booking status '{request.Status}'."
            );

        if (booking.Status == target)
        {
            return await GetAsync(id, cancellationToken);
        }

        // Cancelling always goes down the same path as the cancel endpoint, so the
        // rooms are freed and the group notified however the change was triggered.
        // No status guard here: this override may cancel a completed stay too.
        if (target == BookingStatus.Cancelled)
        {
            await ApplyCancellationAsync(booking, cancellationToken);
            return await GetAsync(id, cancellationToken);
        }

        // Leaving Cancelled means the rooms released on cancel have to be taken
        // back, and someone else may hold them by now.
        if (booking.Status == BookingStatus.Cancelled)
        {
            // The rooms were released on cancel and may be gone, so the split is
            // recomputed against what is free now rather than replayed from the
            // historical mix — which is also how the supervisors get their own
            // rooms back without the mix having to remember which ones they were.
            var free = await _availability.GetFreeRoomsByCapacityAsync(
                booking.StartDate,
                booking.EndDate,
                booking.Id,
                cancellationToken
            );
            var split =
                RoomMixCalculator.SuggestSplitMix(
                    booking.CamperCount,
                    booking.SupervisorCount,
                    free
                )
                ?? throw new ConflictException(
                    "The rooms this group had are no longer free over its dates."
                );
            foreach (
                var assignment in await BuildAssignmentsAsync(booking, split, cancellationToken)
            )
            {
                booking.RoomAssignments.Add(assignment);
                await _bookings.AddAssignmentAsync(assignment, cancellationToken);
            }
        }

        booking.Status = target;
        booking.CancelReason = null;
        booking.HoldExpiresAt =
            target == BookingStatus.PendingDeposit ? ComputeHoldExpiry(booking.StartDate) : null;

        await _bookings.SaveChangesAsync(cancellationToken);

        // Confirming a group is what puts it on the kitchen's timetable: the meals
        // are seated into a sitting of their own and written across the stay. A
        // booking made through the public wizard has none until this moment — it
        // was only ever generated on create or when a payment came in, so a group
        // confirmed by hand used to end up confirmed with an empty programme.
        //
        // Generation is idempotent and leaves deleted meals deleted, so a booking
        // that already has its programme is unaffected.
        if (target == BookingStatus.Confirmed)
        {
            await GenerateMealsSafelyAsync(id, "confirmed by status change", cancellationToken);
        }

        return await GetAsync(id, cancellationToken);
    }

    /// Lays the stay's meals onto the timetable, and never lets that failure take
    /// down the change that triggered it: the booking is already saved by this
    /// point, and the schedule page's "generate missing meals" action is the
    /// recovery path.
    private async Task GenerateMealsSafelyAsync(
        Guid bookingId,
        string context,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _schedule.GenerateMealsForBookingAsync(bookingId, cancellationToken);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(
                ex,
                "Meal generation failed for booking {BookingId} ({Context}).",
                bookingId,
                context
            );
        }
    }

    /// A rate above this is a typo (10 000 zł per person per night), not a price.
    private const long MaxRateGrosze = 1_000_000;

    /// Ceiling on a single group's bill (1 000 000 zł), for the same reason.
    private const long MaxTotalGrosze = 100_000_000;

    /// The same money rules whether the amounts arrive with a new booking or as an
    /// edit to an existing one.
    private static void GuardPricing(long camperRate, long supervisorRate, long total, long deposit)
    {
        if (camperRate is < 0 or > MaxRateGrosze || supervisorRate is < 0 or > MaxRateGrosze)
        {
            throw new BusinessRuleViolationException(
                "A rate must be between 0 and 10 000 zł per person per night."
            );
        }

        if (total is < 0 or > MaxTotalGrosze)
        {
            throw new BusinessRuleViolationException(
                "The total must be between 0 and 1 000 000 zł."
            );
        }

        if (deposit < 0 || deposit > total)
        {
            throw new BusinessRuleViolationException(
                "The deposit must be between 0 and the total."
            );
        }
    }

    public async Task<AdminBookingDto> UpdatePeopleAsync(
        Guid id,
        UpdateBookingPeopleRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var booking = await GetOrThrowAsync(id, cancellationToken);

        if (request.Headcount is < 1 or > 2000)
        {
            throw new BusinessRuleViolationException("A group must be between 1 and 2000 people.");
        }

        if (request.SupervisorCount < 0 || request.SupervisorCount > request.Headcount)
        {
            throw new BusinessRuleViolationException(
                "There cannot be more supervisors than people in the group."
            );
        }

        // Just the counts. The price stays as it stands and so do the rooms — both
        // are the owner's to settle separately, and a group that has lost someone
        // is not necessarily a group that owes less or sleeps anywhere else.
        booking.Headcount = request.Headcount;
        booking.SupervisorCount = request.SupervisorCount;
        await _bookings.SaveChangesAsync(cancellationToken);
        return await GetAsync(id, cancellationToken);
    }

    public async Task<AdminBookingDto> UpdatePricingAsync(
        Guid id,
        UpdateBookingPricingRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var booking = await GetOrThrowAsync(id, cancellationToken);

        // A flat total the owner typed wins over the arithmetic — a negotiated price
        // or a discount is exactly the case the rates cannot express.
        var total =
            request.TotalGrosze
            ?? (request.PricePerPersonPerNightGrosze * booking.CamperCount * booking.Nights)
                + (
                    request.SupervisorPricePerPersonPerNightGrosze
                    * booking.SupervisorCount
                    * booking.Nights
                );
        GuardPricing(
            request.PricePerPersonPerNightGrosze,
            request.SupervisorPricePerPersonPerNightGrosze,
            total,
            request.DepositGrosze
        );

        booking.SupervisorPricePerPersonPerNightGrosze =
            request.SupervisorPricePerPersonPerNightGrosze;
        booking.PricePerPersonPerNightGrosze = request.PricePerPersonPerNightGrosze;
        booking.TotalGrosze = total;
        booking.DepositGrosze = request.DepositGrosze;
        await _bookings.SaveChangesAsync(cancellationToken);
        return await GetAsync(id, cancellationToken);
    }

    public async Task<AdminBookingDto> SetStateAsync(
        Guid id,
        SetBookingStateRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        if (!Enum.TryParse<BookingState>(request.State, out var state))
        {
            throw new BusinessRuleViolationException($"Unknown booking state '{request.State}'.");
        }

        // Cancelling and completing are moves on the stay, and keep every rule the
        // status endpoint enforces — freeing rooms, notifying the group, taking the
        // rooms back when a cancelled booking is revived.
        if (state is BookingState.Cancelled or BookingState.Completed)
        {
            var status =
                state == BookingState.Cancelled ? BookingStatus.Cancelled : BookingStatus.Completed;
            return await SetStatusAsync(
                id,
                new SetBookingStatusRequestDto(status.ToString()),
                cancellationToken
            );
        }

        var payment = state switch
        {
            BookingState.Paid => BookingPaymentState.Paid,
            BookingState.DepositPaid => BookingPaymentState.DepositPaid,
            _ => BookingPaymentState.Unpaid,
        };

        // Coming back to a live state from a cancelled booking is a status move
        // first — the rooms have to be taken again, and may be gone.
        var booking = await GetOrThrowAsync(id, cancellationToken);
        if (booking.Status is BookingStatus.Cancelled or BookingStatus.Completed)
        {
            var revived =
                payment is BookingPaymentState.Unpaid
                    ? BookingStatus.PendingDeposit
                    : BookingStatus.Confirmed;
            await SetStatusAsync(
                id,
                new SetBookingStatusRequestDto(revived.ToString()),
                cancellationToken
            );
        }

        return await SetPaymentStateAsync(
            id,
            new SetBookingPaymentStateRequestDto(payment.ToString()),
            cancellationToken
        );
    }

    public async Task<AdminBookingDto> SetPaymentStateAsync(
        Guid id,
        SetBookingPaymentStateRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var booking = await GetOrThrowAsync(id, cancellationToken);
        if (!Enum.TryParse<BookingPaymentState>(request.PaymentState, out var state))
        {
            throw new BusinessRuleViolationException(
                $"Unknown payment state '{request.PaymentState}'."
            );
        }

        booking.PaymentState = state;

        // Money in hand is what a booking was ever waiting for, so recording it
        // confirms the group and stops the sweeper releasing its rooms. The reverse
        // is deliberately not automatic: un-ticking a payment made in error should
        // not quietly un-confirm a group that has already been told it has a place.
        var confirmedNow =
            state is not BookingPaymentState.Unpaid
            && booking.Status == BookingStatus.PendingDeposit;
        if (confirmedNow)
        {
            booking.Status = BookingStatus.Confirmed;
            booking.HoldExpiresAt = null;
        }

        await _bookings.SaveChangesAsync(cancellationToken);

        if (confirmedNow)
        {
            // Confirming used to happen in the payment webhook, which generated the
            // stay's meals and told the group it had a place. Both still have to
            // happen. Neither may fail the save: the schedule page can backfill
            // meals, and a missed email is recoverable by hand.
            await GenerateMealsSafelyAsync(
                booking.Id,
                "confirmed by payment record",
                cancellationToken
            );

            try
            {
                await _email.SendAsync(EmailTemplates.BookingConfirmed(booking), cancellationToken);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(
                    ex,
                    "Failed to send the confirmation email for booking {BookingId}.",
                    booking.Id
                );
            }
        }

        return await GetAsync(id, cancellationToken);
    }

    private async Task ApplyCancellationAsync(Booking booking, CancellationToken cancellationToken)
    {
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

    /// Picks concrete free rooms for a mix (lowest room numbers first within each
    /// capacity) and spreads the headcount over them. The assignments are returned
    /// unattached so the caller decides how to track them.
    private async Task<List<BookingRoomAssignment>> BuildAssignmentsAsync(
        Booking booking,
        RoomMixCalculator.SplitMix split,
        CancellationToken cancellationToken
    )
    {
        var mix = split.Combined;
        if (mix.Count == 0 || RoomMixCalculator.TotalCapacity(mix) < booking.Headcount)
        {
            throw new ConflictException("No room mix on record that fits this group.");
        }

        var active = await _rooms.GetActiveAsync(cancellationToken);
        var blocked = await _availability.GetBlockedRoomIdsAsync(
            booking.StartDate,
            booking.EndDate,
            booking.Id,
            cancellationToken
        );

        var byCapacity = new Dictionary<int, Queue<Room>>();
        foreach (var (capacity, count) in mix.Where(kv => kv.Value > 0))
        {
            var picked = active
                .Where(r => r.Capacity == capacity && !blocked.Contains(r.Id))
                .OrderBy(r => r.Number, RoomNumberComparer.Instance)
                .Take(count)
                .ToList();
            if (picked.Count < count)
            {
                throw new ConflictException(
                    $"Only {picked.Count} of {count} rooms for {capacity} people are free in this range."
                );
            }

            byCapacity[capacity] = new Queue<Room>(picked);
        }

        // One queue per capacity, drained by the supervisors first and the campers
        // after, so a room can never be handed to both cohorts.
        BookingRoomAssignment Assign((int Capacity, int PeopleCount) load, bool supervisors) =>
            new()
            {
                Id = Guid.NewGuid(),
                BookingId = booking.Id,
                RoomId = byCapacity[load.Capacity].Dequeue().Id,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                PeopleCount = load.PeopleCount,
                IsSupervisorRoom = supervisors,
            };

        return
        [
            .. RoomMixCalculator
                .DistributePeople(booking.SupervisorCount, split.SupervisorMix)
                .Select(load => Assign(load, true)),
            .. RoomMixCalculator
                .DistributePeople(booking.CamperCount, split.CamperMix)
                .Select(load => Assign(load, false)),
        ];
    }

    /// Deposit hold: the standard window, cut short so it always expires before
    /// arrival, and never dated in the past for a stay that already started.
    private DateTime ComputeHoldExpiry(DateOnly startDate)
    {
        var now = DateTime.UtcNow;
        var dayBeforeArrival = startDate
            .ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)
            .AddDays(-1);
        var expiry = now.AddDays(_settings.DepositHoldDays);
        if (expiry > dayBeforeArrival)
        {
            expiry = dayBeforeArrival;
        }

        return expiry < now ? now.AddDays(_settings.DepositHoldDays) : expiry;
    }

    private static BookingStatus? ParseStatus(string? value) =>
        Enum.TryParse<BookingStatus>(value, ignoreCase: true, out var status) ? status : null;

    public async Task<AdminBookingDto> UpdateDietaryNotesAsync(
        Guid id,
        UpdateDietaryNotesRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var booking = await GetOrThrowAsync(id, cancellationToken);
        if (booking.RowVersion != request.RowVersion)
        {
            throw new ConcurrencyConflictException(
                "The booking was modified by someone else. Reload and try again."
            );
        }

        booking.DietaryNotes = string.IsNullOrWhiteSpace(request.DietaryNotes)
            ? null
            : request.DietaryNotes.Trim();

        await _bookings.SaveChangesAsync(cancellationToken);
        return ToDto(booking);
    }

    public async Task<List<AssignableRoomDto>> GetAssignableRoomsAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var booking = await GetOrThrowAsync(id, cancellationToken);
        // Excluding this booking is what makes its own rooms candidates: they are
        // "taken" only by the group being moved, which is free to keep or leave them.
        var blocked = await _availability.GetBlockedRoomIdsAsync(
            booking.StartDate,
            booking.EndDate,
            booking.Id,
            cancellationToken
        );
        var assigned = booking.RoomAssignments.Select(a => a.RoomId).ToHashSet();

        return
        [
            .. (await _rooms.GetActiveAsync(cancellationToken))
                .Where(room => !blocked.Contains(room.Id))
                .OrderBy(room => room.Number, RoomNumberComparer.Instance)
                .Select(room => new AssignableRoomDto(
                    room.Id,
                    room.Number,
                    room.Capacity,
                    assigned.Contains(room.Id)
                )),
        ];
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

        // The two cohorts are placed separately, so they have to add up separately
        // too — otherwise a room could be relabelled without anyone moving.
        if (
            request.Assignments.Where(a => a.IsSupervisorRoom).Sum(a => a.PeopleCount)
            != booking.SupervisorCount
        )
        {
            throw new BusinessRuleViolationException(
                "The supervisor rooms must hold exactly the booking's supervisors."
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
                existing.IsSupervisorRoom = kept.IsSupervisorRoom;
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
                IsSupervisorRoom = entry.IsSupervisorRoom,
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
                b.SupervisorCount,
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
            && FinalDueDate(b) < today
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

    /// Page size ceiling. A caller asking for everything at once would defeat the
    /// point of paging these lists, and the inactive one has no upper bound.
    private const int MaxGroupPageSize = 100;

    public async Task<BookingGroupPageDto> GetGroupPageAsync(
        BookingGroupCategory category,
        int skip,
        int take,
        CancellationToken cancellationToken = default
    )
    {
        var (items, total) = await _bookings.ListByCategoryAsync(
            category,
            DateOnly.FromDateTime(DateTime.UtcNow),
            Math.Max(0, skip),
            Math.Clamp(take, 1, MaxGroupPageSize),
            cancellationToken
        );

        return new BookingGroupPageDto(
            category.ToString(),
            total,
            Math.Max(0, skip),
            [
                .. items.Select(b => new DashboardBookingDto(
                    b.Id,
                    b.OrganizationName,
                    b.StartDate,
                    b.EndDate,
                    b.Headcount,
                    b.SupervisorCount,
                    b.RoomAssignments.Sum(a => a.PeopleCount),
                    b.Status.ToString()
                )),
            ]
        );
    }

    private async Task<Booking> GetOrThrowAsync(Guid id, CancellationToken cancellationToken) =>
        await _bookings.GetByIdAsync(id, cancellationToken)
        ?? throw new NotFoundException("Booking not found.");

    /// Rows stored with a Postgres -infinity date arrive as DateOnly.MinValue, and
    /// subtracting the due-days window from that overflows — which took the whole
    /// bookings list down with a 500 rather than just mislabelling one row.
    private DateOnly FinalDueDate(Booking b) =>
        b.StartDate.DayNumber >= _settings.FinalPaymentDueDays
            ? b.StartDate.AddDays(-_settings.FinalPaymentDueDays)
            : DateOnly.MinValue;

    private AdminBookingDto ToDto(Booking b)
    {
        var finalDue = FinalDueDate(b);
        var depositPaid = b.PaymentState is not BookingPaymentState.Unpaid;
        var finalPaid = b.PaymentState is BookingPaymentState.Paid;
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
            b.SupervisorCount,
            b.Notes,
            b.DietaryNotes,
            b.Status.ToString(),
            b.CancelReason?.ToString(),
            b.TotalGrosze,
            b.DepositGrosze,
            b.PricePerPersonPerNightGrosze,
            b.SupervisorPricePerPersonPerNightGrosze,
            b.PaymentState.ToString(),
            BookingStates.Of(b.Status, b.PaymentState).ToString(),
            depositPaid,
            finalPaid,
            b.Status == BookingStatus.Confirmed
                && !finalPaid
                && finalDue < DateOnly.FromDateTime(DateTime.UtcNow),
            finalDue,
            b.CreatedAt,
            b.RoomAssignments.OrderBy(a => a.Room?.Number, RoomNumberComparer.Instance)
                .Select(a => new AdminAssignmentDto(
                    a.Id,
                    a.RoomId,
                    a.Room?.Number ?? "?",
                    a.Room?.Capacity ?? 0,
                    a.PeopleCount,
                    a.IsSupervisorRoom
                ))
                .ToList()
        );
    }
}
