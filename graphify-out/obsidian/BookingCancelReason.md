---
source_file: "src/CampCenter.Domain/Entities/Booking.cs"
type: "code"
community: "Booking Persistence & Entities"
location: "L17"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Booking_Persistence__Entities
---

# BookingCancelReason

## Context

_Source: `src/CampCenter.Domain/Entities/Booking.cs` (defined near L17; showing L15–L62 of 80)._

```csharp
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

    public string? Notes { get; set; }

    public BookingStatus Status { get; set; } = BookingStatus.PendingDeposit;

    public BookingCancelReason? CancelReason { get; set; }

    /// SHA-256 hash of the secret manage-link token; the plaintext is emailed once.
    public required string ManageTokenHash { get; set; }

    /// While PendingDeposit: rooms are released after this instant if the deposit
    /// hasn't been paid. Null once confirmed.
    public DateTime? HoldExpiresAt { get; set; }

    /// Amounts snapshotted at creation so later price edits don't change existing bookings.
    public long TotalGrosze { get; set; }

```

## Connections
- [[Booking]] - `references` [EXTRACTED]
- [[Booking.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Booking_Persistence__Entities