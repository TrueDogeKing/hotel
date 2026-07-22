---
source_file: "src/CampCenter.Infrastructure/Repositories/BookingRepository.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# BookingRepository.cs

## Context

_Source: `src/CampCenter.Infrastructure/Repositories/BookingRepository.cs` (defined near L1; showing L1–L46 of 218)._

```csharp
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
```

## Connections
- [[BookingRepository]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Exceptions]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Persistence]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Repositories]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces