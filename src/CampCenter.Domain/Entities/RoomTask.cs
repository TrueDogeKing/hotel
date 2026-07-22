namespace CampCenter.Domain.Entities;

public enum RoomTaskStatus
{
    Open,
    Done,
}

/// A housekeeping note attached to a room (e.g. "add one extra bed"), optionally
/// scoped to a booking for context. Housekeeping works off the Open list.
public class RoomTask
{
    public Guid Id { get; set; }

    public Guid RoomId { get; set; }

    public Room? Room { get; set; }

    public Guid? BookingId { get; set; }

    public Booking? Booking { get; set; }

    public required string Text { get; set; }

    public RoomTaskStatus Status { get; set; } = RoomTaskStatus.Open;

    public Guid CreatedByAdminUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DoneAt { get; set; }

    public uint RowVersion { get; set; }
}
