using CampCenter.Application.DTOs.Public;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;

namespace CampCenter.Application.Services;

public class AvailabilityService : IAvailabilityService
{
    private readonly IRoomRepository _rooms;
    private readonly IBookingRepository _bookings;
    private readonly IClosureRepository _closures;
    private readonly IPricingService _pricing;

    public AvailabilityService(
        IRoomRepository rooms,
        IBookingRepository bookings,
        IClosureRepository closures,
        IPricingService pricing
    )
    {
        _rooms = rooms;
        _bookings = bookings;
        _closures = closures;
        _pricing = pricing;
    }

    public async Task<AvailabilityDto> GetAvailabilityAsync(
        DateOnly start,
        DateOnly end,
        int? headcount,
        int? supervisors,
        CancellationToken cancellationToken = default
    )
    {
        var nights = end.DayNumber - start.DayNumber;
        var rates = await _pricing.GetAsync(cancellationToken);
        var centerReason = await GetCenterClosureReasonAsync(start, end, cancellationToken);
        var free = await GetFreeRoomsByCapacityAsync(start, end, null, cancellationToken);
        var remaining = (int)RoomMixCalculator.TotalCapacity(free);

        Dictionary<int, int>? mix = null;
        Dictionary<int, int>? supervisorMix = null;
        bool? fits = null;
        long? total = null;
        long? deposit = null;
        if (headcount is > 0)
        {
            // The kadra are quoted and housed separately, so a group that brings
            // them is told up front whether the centre can seat them apart.
            var staff = Math.Clamp(supervisors ?? 0, 0, headcount.Value);
            var campers = headcount.Value - staff;
            var split = RoomMixCalculator.SuggestSplitMix(campers, staff, free);
            mix = split?.CamperMix;
            supervisorMix = split?.SupervisorMix;
            fits = centerReason is null && split is not null;
            total =
                (rates.PricePerPersonPerNightGrosze * campers * nights)
                + (rates.SupervisorPricePerPersonPerNightGrosze * staff * nights);
            deposit = rates.DepositPerPersonPerNightGrosze * headcount.Value * nights;
        }

        return new AvailabilityDto(
            start,
            end,
            nights,
            centerReason is not null,
            centerReason,
            rates.PricePerPersonPerNightGrosze,
            rates.SupervisorPricePerPersonPerNightGrosze,
            rates.DepositPerPersonPerNightGrosze,
            remaining,
            free,
            fits,
            mix,
            supervisorMix,
            total,
            deposit
        );
    }

    /// Widest span the calendar may ask about at once. A six-week grid is 42 days;
    /// this leaves room for a year of paging without letting an anonymous caller
    /// ask for a decade in one request.
    private const int MaxCalendarDays = 400;

    public async Task<AvailabilityCalendarDto> GetCalendarAsync(
        DateOnly start,
        DateOnly end,
        int? headcount,
        CancellationToken cancellationToken = default
    )
    {
        if (end < start)
        {
            throw new BusinessRuleViolationException("The end of the range is before its start.");
        }

        if (end.DayNumber - start.DayNumber > MaxCalendarDays)
        {
            throw new BusinessRuleViolationException(
                $"A calendar may span at most {MaxCalendarDays} days."
            );
        }

        // One read each, then everything below is in memory. The window runs to
        // end + 1 because the last day drawn is itself a night.
        var lastNightEnd = end.AddDays(1);
        var activeRooms = await _rooms.GetActiveAsync(cancellationToken);
        var closures = await _closures.GetOverlappingAsync(start, lastNightEnd, cancellationToken);
        var assignments = await _bookings.ListAssignmentsInRangeAsync(
            start,
            lastNightEnd,
            cancellationToken
        );

        var days = new List<AvailabilityDayDto>();
        for (var date = start; date <= end; date = date.AddDays(1))
        {
            // Closures cover whole days, both ends included; assignments are
            // half-open, so a checkout day is already free for the next group.
            var centerClosure = closures.FirstOrDefault(c =>
                c.RoomId is null && c.StartDate <= date && date <= c.EndDate
            );

            var blocked = closures
                .Where(c => c.RoomId is not null && c.StartDate <= date && date <= c.EndDate)
                .Select(c => c.RoomId!.Value)
                .Concat(
                    assignments
                        .Where(a => a.StartDate <= date && date < a.EndDate)
                        .Select(a => a.RoomId)
                )
                .ToHashSet();

            var freeBeds = centerClosure is not null
                ? 0
                : activeRooms.Where(r => !blocked.Contains(r.Id)).Sum(r => r.Capacity);

            days.Add(
                new AvailabilityDayDto(
                    date,
                    centerClosure is not null,
                    centerClosure?.Reason,
                    freeBeds,
                    headcount is > 0 ? freeBeds >= headcount : freeBeds > 0
                )
            );
        }

        return new AvailabilityCalendarDto(start, end, headcount, days);
    }

    public async Task<Dictionary<int, int>> GetFreeRoomsByCapacityAsync(
        DateOnly start,
        DateOnly end,
        Guid? excludeBookingId = null,
        CancellationToken cancellationToken = default
    )
    {
        var activeRooms = await _rooms.GetActiveAsync(cancellationToken);
        var blocked = await GetBlockedRoomIdsAsync(start, end, excludeBookingId, cancellationToken);

        return activeRooms
            .Where(r => !blocked.Contains(r.Id))
            .GroupBy(r => r.Capacity)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public async Task<string?> GetCenterClosureReasonAsync(
        DateOnly start,
        DateOnly end,
        CancellationToken cancellationToken = default
    ) =>
        (await _closures.GetOverlappingAsync(start, end, cancellationToken))
            .FirstOrDefault(c => c.RoomId is null)
            ?.Reason;

    public async Task<HashSet<Guid>> GetBlockedRoomIdsAsync(
        DateOnly start,
        DateOnly end,
        Guid? excludeBookingId = null,
        CancellationToken cancellationToken = default
    )
    {
        var overlappingClosures = await _closures.GetOverlappingAsync(
            start,
            end,
            cancellationToken
        );

        // A center-wide closure (RoomId null) blocks every active room.
        if (overlappingClosures.Any(c => c.RoomId is null))
        {
            return (await _rooms.GetActiveAsync(cancellationToken)).Select(r => r.Id).ToHashSet();
        }

        var blocked = (
            await _bookings.GetBookedRoomIdsInRangeAsync(
                start,
                end,
                excludeBookingId,
                cancellationToken
            )
        ).ToHashSet();
        foreach (var closure in overlappingClosures.Where(c => c.RoomId is not null))
        {
            blocked.Add(closure.RoomId!.Value);
        }

        return blocked;
    }
}
