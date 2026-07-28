using CampCenter.Application.DTOs.Schedule;
using CampCenter.Application.Interfaces;
using CampCenter.Application.Models;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;
using Microsoft.Extensions.Options;

namespace CampCenter.Application.Services;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleEntryRepository _entries;
    private readonly IMealTimeDefaultRepository _mealTimes;
    private readonly IBookingMealTimeRepository _bookingMealTimes;
    private readonly IBookingRepository _bookings;
    private readonly ScheduleSettings _settings;

    public ScheduleService(
        IScheduleEntryRepository entries,
        IMealTimeDefaultRepository mealTimes,
        IBookingMealTimeRepository bookingMealTimes,
        IBookingRepository bookings,
        IOptions<ScheduleSettings> settings
    )
    {
        _entries = entries;
        _mealTimes = mealTimes;
        _bookingMealTimes = bookingMealTimes;
        _bookings = bookings;
        _settings = settings.Value;
    }

    // --- Reads ------------------------------------------------------------

    public async Task<ScheduleCalendarDto> GetCalendarAsync(
        DateOnly start,
        DateOnly end,
        CancellationToken cancellationToken = default
    )
    {
        if (end < start)
        {
            throw new BusinessRuleViolationException("End date cannot be before the start date.");
        }

        var bookings = await _bookings.ListLivePresentInAsync(start, end, cancellationToken);
        var counts = await _entries.CountByDateAndKindAsync(
            start,
            end,
            [.. bookings.Select(b => b.Id)],
            cancellationToken
        );

        var mealsByDate = counts
            .Where(c => c.Kind == ScheduleEntryKind.Meal)
            .ToDictionary(c => c.Date, c => c.Count);
        var activitiesByDate = counts
            .Where(c => c.Kind == ScheduleEntryKind.Activity)
            .ToDictionary(c => c.Date, c => c.Count);

        var days = new List<ScheduleCalendarDayDto>();
        for (var date = start; date <= end; date = date.AddDays(1))
        {
            // Presence is inclusive of the departure day — computed in memory from
            // the bookings already loaded, so no extra query per day.
            var present = bookings.Where(b => b.StartDate <= date && date <= b.EndDate).ToList();
            days.Add(
                new ScheduleCalendarDayDto(
                    date,
                    present.Count,
                    present.Sum(b => b.Headcount),
                    mealsByDate.GetValueOrDefault(date),
                    activitiesByDate.GetValueOrDefault(date)
                )
            );
        }

        return new ScheduleCalendarDto(
            start,
            end,
            [.. bookings.Select(ToCalendarDto)],
            days
        );
    }

    public async Task<ScheduleDayDto> GetDayAsync(
        DateOnly date,
        CancellationToken cancellationToken = default
    )
    {
        var bookings = await _bookings.ListLivePresentInAsync(date, date, cancellationToken);
        var entries = await _entries.ListForDateAsync(date, cancellationToken);

        var groups = bookings
            .Select(b => new ScheduleDayGroupDto(
                b.Id,
                b.OrganizationName,
                b.Headcount,
                b.Status.ToString(),
                b.StartDate == date,
                b.EndDate == date,
                b.DietaryNotes
            ))
            .ToList();

        return new ScheduleDayDto(date, groups, [.. entries.Select(ToDto)]);
    }

    public async Task<BookingScheduleDto> GetForBookingAsync(
        Guid bookingId,
        CancellationToken cancellationToken = default
    )
    {
        var booking = await GetBookingOrThrowAsync(bookingId, cancellationToken);
        var entries = await _entries.ListForBookingAsync(bookingId, cancellationToken);
        var byDate = entries.GroupBy(e => e.Date).ToDictionary(g => g.Key, g => g.ToList());

        var days = new List<BookingScheduleDayDto>();
        for (var date = booking.StartDate; date <= booking.EndDate; date = date.AddDays(1))
        {
            var forDay = byDate.GetValueOrDefault(date) ?? [];
            days.Add(
                new BookingScheduleDayDto(
                    date,
                    date == booking.StartDate,
                    date == booking.EndDate,
                    [.. forDay.Select(e => ToDto(e, booking))]
                )
            );
        }

        return new BookingScheduleDto(
            booking.Id,
            booking.OrganizationName,
            booking.ContactName,
            booking.Email,
            booking.Phone,
            booking.StartDate,
            booking.EndDate,
            booking.Nights,
            booking.Headcount,
            booking.Status.ToString(),
            booking.Notes,
            booking.DietaryNotes,
            booking.RowVersion,
            days
        );
    }

    // --- Writes -----------------------------------------------------------

    public async Task<ScheduleEntryDto> CreateEntryAsync(
        CreateScheduleEntryRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var booking = await GetBookingOrThrowAsync(request.BookingId, cancellationToken);
        GuardBookingIsLive(booking);

        var kind = ParseKind(request.Kind);
        var mealKind = ParseMealKind(request.MealKind, kind);
        GuardTimes(request.StartTime, request.EndTime);
        GuardDateWithinStay(request.Date, booking);

        var entry = new ScheduleEntry
        {
            Id = Guid.NewGuid(),
            BookingId = booking.Id,
            Kind = kind,
            MealKind = mealKind,
            Date = request.Date,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Title = request.Title.Trim(),
            Menu = kind == ScheduleEntryKind.Meal ? Normalize(request.Menu) : null,
            PrepNotes = Normalize(request.PrepNotes),
            Location = Normalize(request.Location),
            CreatedAt = DateTime.UtcNow,
        };

        await _entries.AddAsync(entry, cancellationToken);
        await _entries.SaveChangesAsync(cancellationToken);
        return ToDto(entry, booking);
    }

    public async Task<ScheduleEntryDto> UpdateEntryAsync(
        Guid id,
        UpdateScheduleEntryRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var entry = await GetEntryOrThrowAsync(id, cancellationToken);

        // The entity was loaded fresh in this request, so EF's own concurrency
        // check would never fire — compare explicitly, like ClosureService does.
        if (entry.RowVersion != request.RowVersion)
        {
            throw new ConcurrencyConflictException(
                "The schedule entry was modified by someone else. Reload and try again."
            );
        }

        var booking =
            entry.Booking ?? await GetBookingOrThrowAsync(entry.BookingId, cancellationToken);
        GuardBookingIsLive(booking);

        var kind = ParseKind(request.Kind);
        var mealKind = ParseMealKind(request.MealKind, kind);
        GuardTimes(request.StartTime, request.EndTime);
        GuardDateWithinStay(request.Date, booking);

        // Moving this one entry marks it as a deliberate one-day exception, so a
        // later bulk re-time of the group's meal slot leaves it where it is.
        if (entry.StartTime != request.StartTime || entry.EndTime != request.EndTime)
        {
            entry.TimesCustomized = true;
        }

        entry.Kind = kind;
        entry.MealKind = mealKind;
        entry.Date = request.Date;
        entry.StartTime = request.StartTime;
        entry.EndTime = request.EndTime;
        entry.Title = request.Title.Trim();
        entry.Menu = kind == ScheduleEntryKind.Meal ? Normalize(request.Menu) : null;
        entry.PrepNotes = Normalize(request.PrepNotes);
        entry.Location = Normalize(request.Location);
        entry.UpdatedAt = DateTime.UtcNow;

        await _entries.SaveChangesAsync(cancellationToken);
        return ToDto(entry, booking);
    }

    public async Task DeleteEntryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entry = await GetEntryOrThrowAsync(id, cancellationToken);

        if (entry.MealTimeDefaultId is null)
        {
            // Hand-added: nothing generation could recreate, so remove the row.
            _entries.Remove(entry);
        }
        else
        {
            // Generated from a meal slot: keep the row but suppress it, so the
            // slot stays occupied and "generate missing meals" won't bring the
            // meal back after the admin deliberately removed it.
            entry.IsSuppressed = true;
            entry.UpdatedAt = DateTime.UtcNow;
        }

        await _entries.SaveChangesAsync(cancellationToken);
    }

    // --- Per-group meal times ---------------------------------------------

    public async Task<List<BookingMealTimeDto>> GetBookingMealTimesAsync(
        Guid bookingId,
        CancellationToken cancellationToken = default
    )
    {
        _ = await GetBookingOrThrowAsync(bookingId, cancellationToken);
        var defaults = await _mealTimes.GetActiveAsync(cancellationToken);
        var overrides = await _bookingMealTimes.ListForBookingAsync(bookingId, cancellationToken);
        var byDefaultId = overrides.ToDictionary(o => o.MealTimeDefaultId);

        return
        [
            .. defaults.Select(slot =>
                ToDto(slot, byDefaultId.GetValueOrDefault(slot.Id))
            ),
        ];
    }

    public async Task<ApplyBookingMealTimeResultDto> SetBookingMealTimeAsync(
        Guid bookingId,
        Guid mealTimeDefaultId,
        SetBookingMealTimeRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var booking = await GetBookingOrThrowAsync(bookingId, cancellationToken);
        GuardBookingIsLive(booking);
        GuardTimes(request.StartTime, request.EndTime);

        var slot =
            await _mealTimes.GetByIdAsync(mealTimeDefaultId, cancellationToken)
            ?? throw new NotFoundException("Meal slot not found.");

        var existing = await _bookingMealTimes.GetAsync(
            bookingId,
            mealTimeDefaultId,
            cancellationToken
        );

        if (existing is null)
        {
            existing = new BookingMealTime
            {
                Id = Guid.NewGuid(),
                BookingId = bookingId,
                MealTimeDefaultId = mealTimeDefaultId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                CreatedAt = DateTime.UtcNow,
            };
            await _bookingMealTimes.AddAsync(existing, cancellationToken);
        }
        else
        {
            // RowVersion 0 means "first time setting this" — the caller was looking
            // at a slot that had no override row yet, so there is nothing to clash with.
            if (request.RowVersion != 0 && existing.RowVersion != request.RowVersion)
            {
                throw new ConcurrencyConflictException(
                    "The group's meal time was modified by someone else. Reload and try again."
                );
            }

            existing.StartTime = request.StartTime;
            existing.EndTime = request.EndTime;
            existing.UpdatedAt = DateTime.UtcNow;
        }

        await _bookingMealTimes.SaveChangesAsync(cancellationToken);

        var (updated, skipped) = request.ApplyToExisting
            ? await RetimeGeneratedMealsAsync(
                bookingId,
                mealTimeDefaultId,
                request.StartTime,
                request.EndTime,
                cancellationToken
            )
            : (0, 0);

        // Re-timing only moves meals that already exist, so choosing a time for a
        // stay that has none would have left the group empty until someone pressed
        // "generate missing meals". Seed them here instead. Idempotent, and meals an
        // admin deleted on purpose still stay deleted.
        var created = await GenerateMealsForBookingAsync(bookingId, cancellationToken);

        return new ApplyBookingMealTimeResultDto(ToDto(slot, existing), updated, skipped, created);
    }

    public async Task<ApplyBookingMealTimeResultDto> ResetBookingMealTimeAsync(
        Guid bookingId,
        Guid mealTimeDefaultId,
        bool applyToExisting,
        CancellationToken cancellationToken = default
    )
    {
        _ = await GetBookingOrThrowAsync(bookingId, cancellationToken);
        var slot =
            await _mealTimes.GetByIdAsync(mealTimeDefaultId, cancellationToken)
            ?? throw new NotFoundException("Meal slot not found.");

        var existing = await _bookingMealTimes.GetAsync(
            bookingId,
            mealTimeDefaultId,
            cancellationToken
        );
        if (existing is not null)
        {
            _bookingMealTimes.Remove(existing);
            await _bookingMealTimes.SaveChangesAsync(cancellationToken);
        }

        var (updated, skipped) = applyToExisting
            ? await RetimeGeneratedMealsAsync(
                bookingId,
                mealTimeDefaultId,
                slot.StartTime,
                slot.EndTime,
                cancellationToken
            )
            : (0, 0);

        var created = await GenerateMealsForBookingAsync(bookingId, cancellationToken);

        return new ApplyBookingMealTimeResultDto(ToDto(slot, null), updated, skipped, created);
    }

    /// Moves every meal this group has from one slot to new times in one go.
    /// Suppressed entries stay suppressed, and entries an admin moved for a single
    /// day are left untouched — that exception is the whole point of the flag.
    private async Task<(int Updated, int Skipped)> RetimeGeneratedMealsAsync(
        Guid bookingId,
        Guid mealTimeDefaultId,
        TimeOnly start,
        TimeOnly end,
        CancellationToken cancellationToken
    )
    {
        var entries = await _entries.ListForBookingAsync(bookingId, cancellationToken);
        var fromSlot = entries.Where(e => e.MealTimeDefaultId == mealTimeDefaultId).ToList();

        var updated = 0;
        var skipped = 0;
        var now = DateTime.UtcNow;

        foreach (var entry in fromSlot)
        {
            if (entry.TimesCustomized)
            {
                skipped++;
                continue;
            }

            if (entry.StartTime == start && entry.EndTime == end)
            {
                continue;
            }

            entry.StartTime = start;
            entry.EndTime = end;
            entry.UpdatedAt = now;
            updated++;
        }

        if (updated > 0)
        {
            await _entries.SaveChangesAsync(cancellationToken);
        }

        return (updated, skipped);
    }

    // --- Meal generation --------------------------------------------------

    public async Task<int> GenerateMealsForBookingAsync(
        Guid bookingId,
        CancellationToken cancellationToken = default
    )
    {
        var booking = await GetBookingOrThrowAsync(bookingId, cancellationToken);
        if (booking.Status == BookingStatus.Cancelled)
        {
            return 0;
        }

        var defaults = await _mealTimes.GetActiveAsync(cancellationToken);
        if (defaults.Count == 0)
        {
            return 0;
        }

        // Generate at the group's own times where it has them, so the travel-day
        // cutoffs are judged on the times this group actually eats.
        var overrides = await _bookingMealTimes.ListForBookingAsync(bookingId, cancellationToken);
        var slots = MealGenerationPlanner.EffectiveSlots(defaults, overrides);

        // Keyed exactly like the filtered unique index, and deliberately including
        // suppressed rows — that is what stops a meal the admin deleted from
        // coming back on the next run.
        var existing = (await _entries.ListGeneratedSlotsAsync(bookingId, cancellationToken))
            .ToHashSet();

        var now = DateTime.UtcNow;
        var created = new List<ScheduleEntry>();

        foreach (
            var (date, slot) in MealGenerationPlanner.Plan(
                booking.StartDate,
                booking.EndDate,
                slots,
                _settings.ArrivalMealsFrom,
                _settings.DepartureMealsUntil
            )
        )
        {
            if (!existing.Add((date, slot.DefaultId)))
            {
                continue;
            }

            created.Add(
                new ScheduleEntry
                {
                    Id = Guid.NewGuid(),
                    BookingId = bookingId,
                    Kind = ScheduleEntryKind.Meal,
                    MealKind = slot.Kind,
                    MealTimeDefaultId = slot.DefaultId,
                    Date = date,
                    StartTime = slot.Start,
                    EndTime = slot.End,
                    Title = slot.Label,
                    CreatedAt = now,
                }
            );
        }

        if (created.Count == 0)
        {
            return 0;
        }

        await _entries.AddRangeAsync(created, cancellationToken);
        try
        {
            await _entries.SaveChangesAsync(cancellationToken);
        }
        catch (ConflictException)
        {
            // A concurrent generation (a retried P24 notification) inserted the
            // same rows first. The unique index did its job; nothing left to do.
            return 0;
        }

        return created.Count;
    }

    public async Task<GenerateMissingMealsResultDto> GenerateMissingMealsAsync(
        DateOnly from,
        DateOnly to,
        CancellationToken cancellationToken = default
    )
    {
        if (to < from)
        {
            throw new BusinessRuleViolationException("End date cannot be before the start date.");
        }

        var bookings = await _bookings.ListLivePresentInAsync(from, to, cancellationToken);
        var created = 0;
        foreach (var booking in bookings)
        {
            created += await GenerateMealsForBookingAsync(booking.Id, cancellationToken);
        }

        return new GenerateMissingMealsResultDto(bookings.Count, created);
    }

    // --- Guards and mapping ----------------------------------------------

    private static void GuardTimes(TimeOnly start, TimeOnly end)
    {
        if (end <= start)
        {
            throw new BusinessRuleViolationException("End time must be after the start time.");
        }
    }

    /// The stay window is inclusive at both ends: the group is still here on the
    /// departure day, so entries on Booking.EndDate are valid.
    private static void GuardDateWithinStay(DateOnly date, Booking booking)
    {
        if (date < booking.StartDate || date > booking.EndDate)
        {
            throw new BusinessRuleViolationException(
                "The date must fall within the group's stay."
            );
        }
    }

    private static void GuardBookingIsLive(Booking booking)
    {
        if (booking.Status == BookingStatus.Cancelled)
        {
            throw new BusinessRuleViolationException(
                "The booking is cancelled; its schedule cannot be changed."
            );
        }
    }

    private static ScheduleEntryKind ParseKind(string kind) =>
        Enum.TryParse<ScheduleEntryKind>(kind, ignoreCase: true, out var parsed)
            ? parsed
            : throw new BusinessRuleViolationException($"Unknown schedule entry kind '{kind}'.");

    private static MealKind? ParseMealKind(string? mealKind, ScheduleEntryKind kind)
    {
        if (kind != ScheduleEntryKind.Meal)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(mealKind))
        {
            throw new BusinessRuleViolationException("A meal must have a meal kind.");
        }

        return Enum.TryParse<MealKind>(mealKind, ignoreCase: true, out var parsed)
            ? parsed
            : throw new BusinessRuleViolationException($"Unknown meal kind '{mealKind}'.");
    }

    private static string? Normalize(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    private async Task<Booking> GetBookingOrThrowAsync(
        Guid bookingId,
        CancellationToken cancellationToken
    ) =>
        await _bookings.GetByIdAsync(bookingId, cancellationToken)
        ?? throw new NotFoundException("Booking not found.");

    private async Task<ScheduleEntry> GetEntryOrThrowAsync(
        Guid id,
        CancellationToken cancellationToken
    ) =>
        await _entries.GetByIdAsync(id, cancellationToken)
        ?? throw new NotFoundException("Schedule entry not found.");

    private static ScheduleCalendarBookingDto ToCalendarDto(Booking b) =>
        new(
            b.Id,
            b.OrganizationName,
            b.StartDate,
            b.EndDate,
            b.Nights,
            b.Headcount,
            b.Status.ToString()
        );

    private static BookingMealTimeDto ToDto(MealTimeDefault slot, BookingMealTime? own) =>
        new(
            slot.Id,
            slot.MealKind.ToString(),
            slot.Label,
            slot.SortOrder,
            slot.StartTime,
            slot.EndTime,
            own?.StartTime ?? slot.StartTime,
            own?.EndTime ?? slot.EndTime,
            own is not null,
            own?.RowVersion ?? 0
        );

    private static ScheduleEntryDto ToDto(ScheduleEntry e) => ToDto(e, e.Booking);

    private static ScheduleEntryDto ToDto(ScheduleEntry e, Booking? booking) =>
        new(
            e.Id,
            e.BookingId,
            booking?.OrganizationName ?? string.Empty,
            booking?.Headcount ?? 0,
            e.Kind.ToString(),
            e.MealKind?.ToString(),
            e.Date,
            e.StartTime,
            e.EndTime,
            e.Title,
            e.Menu,
            e.PrepNotes,
            e.Location,
            e.TimesCustomized,
            e.RowVersion
        );

    /// The booker's read-only copy. PrepNotes is intentionally not mapped.
    public static PublicScheduleDto ToPublicDto(Booking booking, List<ScheduleEntry> entries)
    {
        var byDate = entries.GroupBy(e => e.Date).ToDictionary(g => g.Key, g => g.ToList());
        var days = new List<PublicScheduleDayDto>();
        for (var date = booking.StartDate; date <= booking.EndDate; date = date.AddDays(1))
        {
            var forDay = (byDate.GetValueOrDefault(date) ?? [])
                .OrderBy(e => e.StartTime)
                .ThenBy(e => e.Title)
                .Select(e => new PublicScheduleEntryDto(
                    e.Kind.ToString(),
                    e.MealKind?.ToString(),
                    e.StartTime,
                    e.EndTime,
                    e.Title,
                    e.Menu,
                    e.Location
                ))
                .ToList();
            days.Add(new PublicScheduleDayDto(date, forDay));
        }

        return new PublicScheduleDto(
            booking.StartDate,
            booking.EndDate,
            booking.Status.ToString(),
            days
        );
    }
}
