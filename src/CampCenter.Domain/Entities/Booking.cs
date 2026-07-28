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
    ///
    /// "Exclusive" applies to nights and room occupancy only. The group is still
    /// physically here on this day, so it IS a valid schedule day — see
    /// ScheduleEntry.Date.
    public DateOnly EndDate { get; set; }

    public required string OrganizationName { get; set; }

    public required string ContactName { get; set; }

    public required string Email { get; set; }

    public required string Phone { get; set; }

    /// Number of participants the group brings.
    public int Headcount { get; set; }

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
    public long TotalGrosze { get; set; }

    public long DepositGrosze { get; set; }

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
