---
source_file: "src/CampCenter.Domain/Entities/Booking.cs"
type: "code"
community: "Booking Persistence & Entities"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Booking_Persistence__Entities
---

# Booking.cs

## Context

_Source: `src/CampCenter.Domain/Entities/Booking.cs` (defined near L1; showing L1–L46 of 80)._

```csharp
namespace CampCenter.Domain.Entities;

public enum BookingStatus
{
    /// Created; rooms are held until HoldExpiresAt while the deposit is unpaid.
    PendingDeposit,

    /// Deposit paid — the booking is binding.
    Confirmed,

    Cancelled,

    /// Stay finished (set by the maintenance sweeper).
    Completed,
}

public enum BookingCancelReason
{
    ByBooker,
    ByAdmin,
    DepositNotPaid,
}

/// A group booking for a chosen date range, made without an account. The booker
/// manages it via an emailed link carrying a secret token (stored hashed).
public class Booking
{
    public Guid Id { get; set; }

    /// Arrival day (inclusive).
    public DateOnly StartDate { get; set; }

    /// Departure day (exclusive — the last night stayed is EndDate - 1).
    public DateOnly EndDate { get; set; }

    public required string OrganizationName { get; set; }

    public required string ContactName { get; set; }

    public required string Email { get; set; }

    public required string Phone { get; set; }

    /// Number of participants the group brings.
    public int Headcount { get; set; }

```

## Connections
- [[Booking]] - `contains` [EXTRACTED]
- [[BookingCancelReason]] - `contains` [EXTRACTED]
- [[BookingStatus]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Booking_Persistence__Entities