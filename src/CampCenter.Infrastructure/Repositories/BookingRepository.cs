using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CampCenter.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private static readonly BookingStatus[] LiveStatuses =
    [
        BookingStatus.PendingDeposit,
        BookingStatus.Confirmed,
        BookingStatus.Completed,
    ];

    private readonly AppDbContext _db;

    public BookingRepository(AppDbContext db) => _db = db;

    public Task<List<Guid>> GetLiveAssignedRoomIdsAsync(
        Guid campSessionId,
        CancellationToken cancellationToken = default
    ) =>
        _db
            .BookingRoomAssignments.Where(a =>
                a.CampSessionId == campSessionId
                && a.Booking != null
                && LiveStatuses.Contains(a.Booking.Status)
            )
            .Select(a => a.RoomId)
            .ToListAsync(cancellationToken);

    public async Task AddAsync(Booking booking, CancellationToken cancellationToken = default) =>
        await _db.Bookings.AddAsync(booking, cancellationToken);

    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db
            .Bookings.Include(b => b.CampSession)
            .Include(b => b.RoomAssignments)
                .ThenInclude(a => a.Room)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public Task<List<Booking>> ListAsync(
        Guid? campSessionId,
        BookingStatus? status,
        CancellationToken cancellationToken = default
    )
    {
        var query = _db
            .Bookings.Include(b => b.CampSession)
            .Include(b => b.RoomAssignments)
                .ThenInclude(a => a.Room)
            .AsQueryable();
        if (campSessionId is not null)
        {
            query = query.Where(b => b.CampSessionId == campSessionId);
        }

        if (status is not null)
        {
            query = query.Where(b => b.Status == status);
        }

        return query.OrderByDescending(b => b.CreatedAt).ToListAsync(cancellationToken);
    }

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
            .Bookings.Include(b => b.CampSession)
            .Include(b => b.RoomAssignments)
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
            .ThenInclude(b => b!.CampSession)
            .Include(p => p.Booking)
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
            .Bookings.Include(b => b.CampSession)
            .Include(b => b.RoomAssignments)
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
            .Bookings.Where(b =>
                b.Status == BookingStatus.Confirmed
                && b.CampSession != null
                && b.CampSession.EndDate < today
            )
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
                    is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation }
            )
        {
            // A concurrent booking grabbed one of the selected rooms first.
            throw new ConflictException(
                "One of the selected rooms was just booked by someone else."
            );
        }
    }
}
