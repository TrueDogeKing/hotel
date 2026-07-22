---
source_file: "src/CampCenter.Domain/Repositories/IBookingRepository.cs"
type: "code"
community: "Admin Booking & Notifications"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Booking__Notifications
---

# IBookingRepository

## Context

_Source: `src/CampCenter.Domain/Repositories/IBookingRepository.cs` (defined near L5; showing L3–L50 of 104)._

```csharp
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
    );

    /// Booking by manage-token hash, with assignments (incl. rooms) and payments loaded.
    Task<Booking?> GetByTokenHashAsync(
```

## Connections
- [[.AddAssignmentAsync()]] - `method` [EXTRACTED]
- [[.AddAsync()]] - `method` [EXTRACTED]
- [[.AddPaymentAsync()]] - `method` [EXTRACTED]
- [[.Detach()]] - `method` [EXTRACTED]
- [[.GetBookedRoomIdsInRangeAsync()]] - `method` [EXTRACTED]
- [[.GetByIdAsync()_1]] - `method` [EXTRACTED]
- [[.GetByTokenHashAsync()]] - `method` [EXTRACTED]
- [[.GetCompletedPaymentKindsAsync()]] - `method` [EXTRACTED]
- [[.GetConfirmedEndedAsync()]] - `method` [EXTRACTED]
- [[.GetExpiredPendingAsync()]] - `method` [EXTRACTED]
- [[.GetPaymentByP24SessionIdAsync()]] - `method` [EXTRACTED]
- [[.GetPaymentsAsync()]] - `method` [EXTRACTED]
- [[.ListAsync()_4]] - `method` [EXTRACTED]
- [[.ListLiveInRangeAsync()]] - `method` [EXTRACTED]
- [[.ListUpcomingAsync()]] - `method` [EXTRACTED]
- [[.RemoveAssignment()]] - `method` [EXTRACTED]
- [[.RemoveAssignments()]] - `method` [EXTRACTED]
- [[.SaveChangesAsync()_1]] - `method` [EXTRACTED]
- [[AdminBookingService]] - `references` [EXTRACTED]
- [[AvailabilityService]] - `references` [EXTRACTED]
- [[BookingRepository]] - `implements` [EXTRACTED]
- [[BookingService]] - `references` [EXTRACTED]
- [[IBookingRepository.cs]] - `contains` [EXTRACTED]
- [[PaymentService]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Booking__Notifications