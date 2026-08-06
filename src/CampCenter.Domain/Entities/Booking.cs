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

/// How much of the price the owner has actually been paid. Set by hand in the
/// panel: money changes hands by transfer or in cash, not through the site.
public enum BookingPaymentState
{
    /// Nothing received yet ("oczekuje na płatność").
    Unpaid,

    /// The deposit has arrived; the rest is still owed ("zaliczka zapłacona").
    DepositPaid,

    /// Paid in full ("opłacone").
    Paid,
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
    ///
    /// "Exclusive" applies to nights and room occupancy only. The group is still
    /// physically here on this day, so it IS a valid schedule day — see
    /// ScheduleEntry.Date.
    public DateOnly EndDate { get; set; }

    public required string OrganizationName { get; set; }

    public required string ContactName { get; set; }

    public required string Email { get; set; }

    public required string Phone { get; set; }

    /// Everyone the group brings, campers and supervisors together. Stays the
    /// total because capacity, pricing, catering and the emails all reason about
    /// bodies in beds rather than about who they are.
    public int Headcount { get; set; }

    /// How many of Headcount are adult supervisors (kadra). They sleep in their
    /// own rooms, are shown in brackets beside the group in schedules, and are
    /// charged at their own rate.
    public int SupervisorCount { get; set; }

    /// The children. Derived, never stored — one number can only be edited in one
    /// place without the two drifting apart.
    public int CamperCount => Headcount - SupervisorCount;

    /// Free-text note left by the booker at booking time.
    public string? Notes { get; set; }

    /// Admin-managed dietary requirements and preparation note for the kitchen
    /// ("2× bezglutenowa, 1× wegetariańska"). Distinct from Notes, which the
    /// booker wrote.
    public string? DietaryNotes { get; set; }

    public BookingStatus Status { get; set; } = BookingStatus.PendingDeposit;

    public BookingCancelReason? CancelReason { get; set; }

    /// SHA-256 hash of the secret manage-link token; the plaintext is emailed once.
    public required string ManageTokenHash { get; set; }

    /// While PendingDeposit: rooms are released after this instant if the deposit
    /// hasn't been paid. Null once confirmed.
    public DateTime? HoldExpiresAt { get; set; }

    /// Amounts snapshotted at creation so later price edits don't change existing bookings.
    /// The owner may edit all three afterwards on this booking alone.
    public long TotalGrosze { get; set; }

    public long DepositGrosze { get; set; }

    /// The camper rate this group's total was worked out from. Kept on the booking
    /// so the panel can show what the group is charged per participant per night
    /// and recompute the total when either side changes.
    public long PricePerPersonPerNightGrosze { get; set; }

    /// The kadra's own rate. Total = camper rate × campers × nights +
    /// supervisor rate × supervisors × nights, unless a flat total was typed over it.
    public long SupervisorPricePerPersonPerNightGrosze { get; set; }

    /// Whether the money has arrived — recorded by the owner, who is paid by
    /// transfer or in cash rather than through the site.
    public BookingPaymentState PaymentState { get; set; } = BookingPaymentState.Unpaid;

    /// Requested room mix as JSON {"4": 40, "3": 2} (capacity → count). Historical
    /// record that survives assignment deletion on cancel.
    public required string RequestedRoomCounts { get; set; }

    /// UI language at booking time ("pl"/"en"); drives email language and the P24 page.
    public required string Language { get; set; }

    public DateTime CreatedAt { get; set; }

    public uint RowVersion { get; set; }

    public List<BookingRoomAssignment> RoomAssignments { get; set; } = [];

    /// Number of nights the group stays.
    public int Nights => EndDate.DayNumber - StartDate.DayNumber;
}
