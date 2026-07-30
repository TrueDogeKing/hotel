namespace CampCenter.Domain.Entities;

/// Why a room is on housekeeping's list for a given day.
public enum RoomCleaningKind
{
    /// A group left this room that morning: check it over and clean it.
    Departure,

    /// A group moves in that day: have the room ready before they arrive.
    Arrival,

    /// Both, in one day — one group out and the next one in. The tight one, and the
    /// reason this list is ordered by kind rather than by room number.
    Turnaround,
}

public enum RoomCleaningStatus
{
    Pending,
    InProgress,
    Done,
}

/// How far housekeeping has got with one room on one day.
///
/// Deliberately only the *progress*: which rooms need doing, and why, is derived from
/// the room assignments every time the list is read (see HousekeepingPlanner). Rooms
/// get reassigned and bookings get cancelled, and a table of pre-generated jobs would
/// quietly keep sending someone to clean a room nobody is leaving.
///
/// A row therefore exists only once somebody has touched that room's job — the same
/// shape as BookingMealTime, which exists only where a group's meal time differs from
/// the centre's. No row means Pending.
public class RoomCleaning
{
    public Guid Id { get; set; }

    public Guid RoomId { get; set; }

    public Room? Room { get; set; }

    /// The day the work is due. Unique together with RoomId: a room is cleaned once a
    /// day, however many groups pass through it.
    public DateOnly Date { get; set; }

    /// What the job was when it was last touched, kept for the record. The list still
    /// shows the kind derived from today's assignments — if a booking moved, that
    /// derived kind is the truth and this is only history.
    public RoomCleaningKind Kind { get; set; }

    public RoomCleaningStatus Status { get; set; } = RoomCleaningStatus.Pending;

    /// Free-text note from whoever cleaned it ("kaloryfer cieknie"). Room faults that
    /// outlive the day belong in a RoomTask instead, which is why this is not a to-do.
    public string? Note { get; set; }

    /// When the room was marked Done. Cleared if it is reopened, so "cleaned today"
    /// never reports a room that was un-ticked afterwards.
    public DateTime? DoneAt { get; set; }

    public Guid? DoneByAdminUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public uint RowVersion { get; set; }
}
