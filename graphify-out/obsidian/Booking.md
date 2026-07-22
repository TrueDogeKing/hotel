---
source_file: "src/CampCenter.Domain/Entities/Booking.cs"
type: "code"
community: "Booking Persistence & Entities"
location: "L26"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Booking_Persistence__Entities
---

# Booking

## Context

_Source: `src/CampCenter.Domain/Entities/Booking.cs` (defined near L26; showing L24–L71 of 80)._

```csharp
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

    public long DepositGrosze { get; set; }

    /// Requested room mix as JSON {"4": 40, "3": 2} (capacity → count). Historical
    /// record that survives assignment deletion on cancel.
    public required string RequestedRoomCounts { get; set; }

    /// UI language at booking time ("pl"/"en"); drives email language and the P24 page.
    public required string Language { get; set; }

```

## Connections
- [[.AddAsync()]] - `references` [EXTRACTED]
- [[.AddAsync()_5]] - `references` [EXTRACTED]
- [[.AssignRooms()]] - `references` [EXTRACTED]
- [[.BookingCancelled()]] - `references` [EXTRACTED]
- [[.BookingConfirmed()]] - `references` [EXTRACTED]
- [[.BookingCreated()]] - `references` [EXTRACTED]
- [[.Configure()_1]] - `references` [EXTRACTED]
- [[.Detach()]] - `references` [EXTRACTED]
- [[.Detach()_1]] - `references` [EXTRACTED]
- [[.FindByTokenAsync()]] - `references` [EXTRACTED]
- [[.GetByIdAsync()_1]] - `references` [EXTRACTED]
- [[.GetByIdAsync()_6]] - `references` [EXTRACTED]
- [[.GetByTokenHashAsync()]] - `references` [EXTRACTED]
- [[.GetByTokenHashAsync()_2]] - `references` [EXTRACTED]
- [[.GetConfirmedEndedAsync()]] - `references` [EXTRACTED]
- [[.GetConfirmedEndedAsync()_1]] - `references` [EXTRACTED]
- [[.GetExpiredPendingAsync()]] - `references` [EXTRACTED]
- [[.GetExpiredPendingAsync()_1]] - `references` [EXTRACTED]
- [[.GetOrThrowAsync()]] - `references` [EXTRACTED]
- [[.ListAsync()_4]] - `references` [EXTRACTED]
- [[.ListAsync()_6]] - `references` [EXTRACTED]
- [[.ListLiveInRangeAsync()]] - `references` [EXTRACTED]
- [[.ListLiveInRangeAsync()_1]] - `references` [EXTRACTED]
- [[.ListUpcomingAsync()]] - `references` [EXTRACTED]
- [[.ListUpcomingAsync()_1]] - `references` [EXTRACTED]
- [[.RemoveAssignments()]] - `references` [EXTRACTED]
- [[.RemoveAssignments()_1]] - `references` [EXTRACTED]
- [[.ToDto()]] - `references` [EXTRACTED]
- [[AppDbContext]] - `references` [EXTRACTED]
- [[Booking.cs]] - `contains` [EXTRACTED]
- [[BookingCancelReason]] - `references` [EXTRACTED]
- [[BookingConfiguration]] - `references` [EXTRACTED]
- [[BookingRoomAssignment]] - `references` [EXTRACTED]
- [[BookingStatus]] - `references` [EXTRACTED]
- [[DateOnly_3]] - `references` [EXTRACTED]
- [[DateTime_4]] - `references` [EXTRACTED]
- [[Guid_18]] - `references` [EXTRACTED]
- [[List_12]] - `references` [EXTRACTED]
- [[Payment]] - `references` [EXTRACTED]
- [[RoomTask_1]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Booking_Persistence__Entities