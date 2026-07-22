---
source_file: "src/CampCenter.Infrastructure/Repositories/BookingRepository.cs"
type: "code"
community: "Booking Persistence & Entities"
location: "L10"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Booking_Persistence__Entities
---

# BookingRepository

## Context

_Source: `src/CampCenter.Infrastructure/Repositories/BookingRepository.cs` (defined near L10; showing L8–L55 of 218)._

```csharp
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
```

## Connections
- [[.AddAssignmentAsync()_1]] - `method` [EXTRACTED]
- [[.AddAsync()_5]] - `method` [EXTRACTED]
- [[.AddPaymentAsync()_1]] - `method` [EXTRACTED]
- [[.Detach()_1]] - `method` [EXTRACTED]
- [[.GetBookedRoomIdsInRangeAsync()_1]] - `method` [EXTRACTED]
- [[.GetByIdAsync()_6]] - `method` [EXTRACTED]
- [[.GetByTokenHashAsync()_2]] - `method` [EXTRACTED]
- [[.GetCompletedPaymentKindsAsync()_1]] - `method` [EXTRACTED]
- [[.GetConfirmedEndedAsync()_1]] - `method` [EXTRACTED]
- [[.GetExpiredPendingAsync()_1]] - `method` [EXTRACTED]
- [[.GetPaymentByP24SessionIdAsync()_1]] - `method` [EXTRACTED]
- [[.GetPaymentsAsync()_1]] - `method` [EXTRACTED]
- [[.ListAsync()_6]] - `method` [EXTRACTED]
- [[.ListLiveInRangeAsync()_1]] - `method` [EXTRACTED]
- [[.ListUpcomingAsync()_1]] - `method` [EXTRACTED]
- [[.RemoveAssignment()_1]] - `method` [EXTRACTED]
- [[.RemoveAssignments()_1]] - `method` [EXTRACTED]
- [[.SaveChangesAsync()_7]] - `method` [EXTRACTED]
- [[AppDbContext]] - `references` [EXTRACTED]
- [[BookingRepository.cs]] - `contains` [EXTRACTED]
- [[BookingStatus]] - `references` [EXTRACTED]
- [[IBookingRepository]] - `implements` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Booking_Persistence__Entities