using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CampCenter.Infrastructure.Repositories;

public class ScheduleEntryRepository : IScheduleEntryRepository
{
    private readonly AppDbContext _db;

    public ScheduleEntryRepository(AppDbContext db) => _db = db;

    public Task<ScheduleEntry?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    ) =>
        _db
            .ScheduleEntries.Include(e => e.Booking)
            .FirstOrDefaultAsync(e => e.Id == id && !e.IsSuppressed, cancellationToken);

    public Task<List<ScheduleEntry>> ListForDateAsync(
        DateOnly date,
        CancellationToken cancellationToken = default
    ) =>
        // One LEFT JOIN carries the organization name, so the day view never has to
        // loop back for group names.
        _db
            .ScheduleEntries.Include(e => e.Booking)
            .Where(e =>
                e.Date == date
                && !e.IsSuppressed
                && e.Booking != null
                && BookingStatuses.Live.Contains(e.Booking.Status)
            )
            .OrderBy(e => e.StartTime)
            .ThenBy(e => e.Booking!.OrganizationName)
            .ToListAsync(cancellationToken);

    public Task<List<ScheduleEntry>> ListForBookingAsync(
        Guid bookingId,
        CancellationToken cancellationToken = default
    ) =>
        _db
            .ScheduleEntries.Where(e => e.BookingId == bookingId && !e.IsSuppressed)
            .OrderBy(e => e.Date)
            .ThenBy(e => e.StartTime)
            .ThenBy(e => e.Title)
            .ToListAsync(cancellationToken);

    public async Task<List<(DateOnly Date, Guid MealTimeDefaultId)>> ListGeneratedSlotsAsync(
        Guid bookingId,
        CancellationToken cancellationToken = default
    )
    {
        // Deliberately ignores IsSuppressed: a suppressed row still occupies its
        // slot, which is exactly what stops generation recreating it.
        var rows = await _db
            .ScheduleEntries.Where(e => e.BookingId == bookingId && e.MealTimeDefaultId != null)
            .Select(e => new { e.Date, MealTimeDefaultId = e.MealTimeDefaultId!.Value })
            .ToListAsync(cancellationToken);

        return [.. rows.Select(r => (r.Date, r.MealTimeDefaultId))];
    }

    public async Task<List<(Guid BookingId, Guid MealTimeDefaultId)>> ListFullySuppressedSlotsAsync(
        IReadOnlyCollection<Guid> bookingIds,
        CancellationToken cancellationToken = default
    )
    {
        var rows = await _db
            .ScheduleEntries.Where(e =>
                bookingIds.Contains(e.BookingId) && e.MealTimeDefaultId != null
            )
            .GroupBy(e => new { e.BookingId, MealTimeDefaultId = e.MealTimeDefaultId!.Value })
            .Select(g => new
            {
                g.Key.BookingId,
                g.Key.MealTimeDefaultId,
                Visible = g.Count(e => !e.IsSuppressed),
            })
            // Generated at some point, and nothing left on the programme.
            .Where(x => x.Visible == 0)
            .ToListAsync(cancellationToken);

        return [.. rows.Select(r => (r.BookingId, r.MealTimeDefaultId))];
    }

    public async Task<
        List<(Guid BookingId, Guid MealTimeDefaultId, TimeOnly Start, TimeOnly End)>
    > ListVisibleSlotSpansAsync(
        IReadOnlyCollection<Guid> bookingIds,
        CancellationToken cancellationToken = default
    )
    {
        var rows = await _db
            .ScheduleEntries.Where(e =>
                bookingIds.Contains(e.BookingId) && e.MealTimeDefaultId != null && !e.IsSuppressed
            )
            .GroupBy(e => new { e.BookingId, MealTimeDefaultId = e.MealTimeDefaultId!.Value })
            .Select(g => new
            {
                g.Key.BookingId,
                g.Key.MealTimeDefaultId,
                Start = g.Min(e => e.StartTime),
                End = g.Max(e => e.EndTime),
            })
            .ToListAsync(cancellationToken);

        return [.. rows.Select(r => (r.BookingId, r.MealTimeDefaultId, r.Start, r.End))];
    }

    public Task<List<string>> ListLocationsAsync(CancellationToken cancellationToken = default) =>
        _db
            .ScheduleEntries.Where(e => e.Location != null && e.Location != "" && !e.IsSuppressed)
            .Select(e => e.Location!)
            .Distinct()
            .OrderBy(location => location)
            .ToListAsync(cancellationToken);

    public async Task<
        List<(DateOnly Date, ScheduleEntryKind Kind, int Count)>
    > CountByDateAndKindAsync(
        DateOnly start,
        DateOnly end,
        IReadOnlyCollection<Guid> bookingIds,
        CancellationToken cancellationToken = default
    )
    {
        if (bookingIds.Count == 0)
        {
            return [];
        }

        var rows = await _db
            .ScheduleEntries.Where(e =>
                e.Date >= start
                && e.Date <= end
                && !e.IsSuppressed
                && bookingIds.Contains(e.BookingId)
            )
            .GroupBy(e => new { e.Date, e.Kind })
            .Select(g => new
            {
                g.Key.Date,
                g.Key.Kind,
                Count = g.Count(),
            })
            .ToListAsync(cancellationToken);

        return [.. rows.Select(r => (r.Date, r.Kind, r.Count))];
    }

    public async Task<List<(Guid BookingId, DateOnly Date)>> ListOutingDaysAsync(
        DateOnly start,
        DateOnly end,
        IReadOnlyCollection<Guid> bookingIds,
        CancellationToken cancellationToken = default
    )
    {
        if (bookingIds.Count == 0)
        {
            return [];
        }

        // Distinct: two trips on one day still mark that day once.
        var rows = await _db
            .ScheduleEntries.Where(e =>
                e.Kind == ScheduleEntryKind.Outing
                && e.Date >= start
                && e.Date <= end
                && !e.IsSuppressed
                && bookingIds.Contains(e.BookingId)
            )
            .Select(e => new { e.BookingId, e.Date })
            .Distinct()
            .ToListAsync(cancellationToken);

        return [.. rows.Select(r => (r.BookingId, r.Date))];
    }

    public async Task AddRangeAsync(
        IReadOnlyCollection<ScheduleEntry> entries,
        CancellationToken cancellationToken = default
    ) => await _db.ScheduleEntries.AddRangeAsync(entries, cancellationToken);

    public async Task AddAsync(
        ScheduleEntry entry,
        CancellationToken cancellationToken = default
    ) => await _db.ScheduleEntries.AddAsync(entry, cancellationToken);

    public void Remove(ScheduleEntry entry) => _db.ScheduleEntries.Remove(entry);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
            when (ex.InnerException
                    is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation }
            )
        {
            // The generation idempotency index rejected a duplicate: a concurrent
            // run (a retried P24 notification) already created these meals.
            throw new ConflictException("These schedule entries already exist.");
        }
    }
}
