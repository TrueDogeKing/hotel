---
source_file: "src/CampCenter.Domain/Repositories/IBookingRepository.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# IBookingRepository.cs

## Context

_Source: `src/CampCenter.Domain/Repositories/IBookingRepository.cs` (defined near L1; showing L1–L46 of 104)._

```csharp
using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IBookingRepository
{
    /// Room ids held by "live" bookings (PendingDeposit, Confirmed, Completed)
    /// whose stay overlaps [start, end) — i.e. rooms unavailable over that range.
    /// <paramref name="excludeBookingId"/> ignores one booking's own rooms (reassign).
    Task<List<Guid>> GetBookedRoomIdsInRangeAsync(
        DateOnly start,
        DateOnly end,
        Guid? excludeBookingId = null,
        CancellationToken cancellationToken = default
    );

    Task AddAsync(Booking booking, CancellationToken cancellationToken = default);

    /// Booking by id with assignments (incl. rooms) loaded.
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// Admin listing, newest first, with assignments loaded.
    Task<List<Booking>> ListAsync(
        BookingStatus? status,
        CancellationToken cancellationToken = default
    );

    /// Live bookings whose stay overlaps [start, end), with assignments (incl.
    /// rooms) loaded — the source for the occupancy grid over a date range.
    Task<List<Booking>> ListLiveInRangeAsync(
        DateOnly start,
        DateOnly end,
        CancellationToken cancellationToken = default
    );

    /// Live bookings starting on/after <paramref name="from"/>, earliest first.
    Task<List<Booking>> ListUpcomingAsync(
        DateOnly from,
        int take,
        CancellationToken cancellationToken = default
    );

    /// Kinds of Completed payments per booking (for payment-status badges).
    Task<Dictionary<Guid, List<PaymentKind>>> GetCompletedPaymentKindsAsync(
        IReadOnlyCollection<Guid> bookingIds,
        CancellationToken cancellationToken = default
```

## Connections
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `contains` [EXTRACTED]
- [[IBookingRepository]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces