using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CampCenter.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private static readonly BookingStatus[] LiveStatuses = BookingStatuses.Live;

    private readonly AppDbContext _db;

    public BookingRepository(AppDbContext db) => _db = db;

    public Task<List<Guid>> GetBookedRoomIdsInRangeAsync(
        DateOnly start,
        DateOnly end,
        Guid? excludeBookingId = null,
        CancellationToken cancellationToken = default
    ) =>
        // Half-open overlap: two stays clash when a.Start < b.End && b.Start < a.End.
        _db
            .BookingRoomAssignments.Where(a =>
                a.StartDate < end
                && start < a.EndDate
                && (excludeBookingId == null || a.BookingId != excludeBookingId)
                && a.Booking != null
                && LiveStatuses.Contains(a.Booking.Status)
            )
            .Select(a => a.RoomId)
            .Distinct()
            .ToListAsync(cancellationToken);

    public async Task AddAsync(Booking booking, CancellationToken cancellationToken = default) =>
        await _db.Bookings.AddAsync(booking, cancellationToken);

    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db
            .Bookings.Include(b => b.RoomAssignments)
                .ThenInclude(a => a.Room)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public Task<List<Booking>> ListAsync(
        BookingStatus? status,
        CancellationToken cancellationToken = default
    )
    {
        var query = _db
            .Bookings.Include(b => b.RoomAssignments)
                .ThenInclude(a => a.Room)
            .AsQueryable();
        if (status is not null)
        {
            query = query.Where(b => b.Status == status);
        }

        return query.OrderByDescending(b => b.CreatedAt).ToListAsync(cancellationToken);
    }

    public Task<List<Booking>> ListLiveInRangeAsync(
        DateOnly start,
        DateOnly end,
        CancellationToken cancellationToken = default
    ) =>
        _db
            .Bookings.Include(b => b.RoomAssignments)
                .ThenInclude(a => a.Room)
            .Where(b =>
                LiveStatuses.Contains(b.Status) && b.StartDate < end && start < b.EndDate
            )
            .OrderBy(b => b.StartDate)
            .ToListAsync(cancellationToken);

    public Task<List<Booking>> ListLivePresentInAsync(
        DateOnly firstDay,
        DateOnly lastDay,
        CancellationToken cancellationToken = default
    ) =>
        // Inclusive on both ends, unlike ListLiveInRangeAsync: a group is still
        // here on its departure day, and the schedule must show it.
        _db
            .Bookings.Where(b =>
                LiveStatuses.Contains(b.Status) && b.StartDate <= lastDay && firstDay <= b.EndDate
            )
            .OrderBy(b => b.StartDate)
            .ThenBy(b => b.OrganizationName)
            .ToListAsync(cancellationToken);

    public Task<List<Booking>> ListUpcomingAsync(
        DateOnly from,
        int take,
        CancellationToken cancellationToken = default
    ) =>
        _db
            .Bookings.Include(b => b.RoomAssignments)
            .Where(b =>
                (b.Status == BookingStatus.PendingDeposit || b.Status == BookingStatus.Confirmed)
                && b.EndDate >= from
            )
            .OrderBy(b => b.StartDate)
            .Take(take)
            .ToListAsync(cancellationToken);

    public async Task<Dictionary<Guid, List<PaymentKind>>> GetCompletedPaymentKindsAsync(
        IReadOnlyCollection<Guid> bookingIds,
        CancellationToken cancellationToken = default
    ) =>
        (
            await _db
                .Payments.Where(p =>
                    bookingIds.Contains(p.BookingId) && p.Status == PaymentStatus.Completed
                )
                .Select(p => new { p.BookingId, p.Kind })
                .ToListAsync(cancellationToken)
        )
            .GroupBy(p => p.BookingId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Kind).ToList());

    public Task<Booking?> GetByTokenHashAsync(
        string tokenHash,
        CancellationToken cancellationToken = default
    ) =>
        _db
            .Bookings.Include(b => b.RoomAssignments)
                .ThenInclude(a => a.Room)
            .FirstOrDefaultAsync(b => b.ManageTokenHash == tokenHash, cancellationToken);

    public async Task AddPaymentAsync(
        Payment payment,
        CancellationToken cancellationToken = default
    ) => await _db.Payments.AddAsync(payment, cancellationToken);

    public Task<Payment?> GetPaymentByP24SessionIdAsync(
        string p24SessionId,
        CancellationToken cancellationToken = default
    ) =>
        _db
            .Payments.Include(p => p.Booking)
                .ThenInclude(b => b!.RoomAssignments)
            .FirstOrDefaultAsync(p => p.P24SessionId == p24SessionId, cancellationToken);

    public Task<List<Payment>> GetPaymentsAsync(
        Guid bookingId,
        CancellationToken cancellationToken = default
    ) =>
        _db
            .Payments.Where(p => p.BookingId == bookingId)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync(cancellationToken);

    public Task<List<Booking>> GetExpiredPendingAsync(
        DateTime nowUtc,
        DateTime paymentGraceCutoffUtc,
        CancellationToken cancellationToken = default
    ) =>
        _db
            .Bookings.Include(b => b.RoomAssignments)
            .Where(b =>
                b.Status == BookingStatus.PendingDeposit
                && b.HoldExpiresAt != null
                && b.HoldExpiresAt < nowUtc
                && !_db.Payments.Any(p =>
                    p.BookingId == b.Id
                    && p.Status == PaymentStatus.Pending
                    && p.CreatedAt > paymentGraceCutoffUtc
                )
            )
            .ToListAsync(cancellationToken);

    public Task<List<Booking>> GetConfirmedEndedAsync(
        DateOnly today,
        CancellationToken cancellationToken = default
    ) =>
        _db
            .Bookings.Where(b => b.Status == BookingStatus.Confirmed && b.EndDate <= today)
            .ToListAsync(cancellationToken);

    public void Detach(Booking booking)
    {
        // Detaching triggers EF navigation fixup which mutates the collection — copy first.
        foreach (var assignment in booking.RoomAssignments.ToList())
        {
            _db.Entry(assignment).State = EntityState.Detached;
        }

        _db.Entry(booking).State = EntityState.Detached;
    }

    public void RemoveAssignment(BookingRoomAssignment assignment) =>
        _db.BookingRoomAssignments.Remove(assignment);

    public async Task AddAssignmentAsync(
        BookingRoomAssignment assignment,
        CancellationToken cancellationToken = default
    ) => await _db.BookingRoomAssignments.AddAsync(assignment, cancellationToken);

    public void RemoveAssignments(Booking booking)
    {
        _db.BookingRoomAssignments.RemoveRange(booking.RoomAssignments);
        booking.RoomAssignments.Clear();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
            when (ex.InnerException
                is PostgresException
                {
                    SqlState: PostgresErrorCodes.UniqueViolation
                        or PostgresErrorCodes.ExclusionViolation
                        or PostgresErrorCodes.DeadlockDetected
                        or PostgresErrorCodes.SerializationFailure
                }
            )
        {
            // A concurrent booking grabbed one of the selected rooms for an
            // overlapping range first. The GiST exclusion constraint can surface
            // this as a plain exclusion violation or, when two overlapping inserts
            // race for the same lock, as a deadlock/serialization failure — all
            // mean the same thing to the caller, which retries once.
            throw new ConflictException(
                "One of the selected rooms was just booked by someone else."
            );
        }
    }
}
