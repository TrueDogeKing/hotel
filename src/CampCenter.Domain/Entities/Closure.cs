namespace CampCenter.Domain.Entities;

/// A blockade of availability: a span of days the center (or a single room) is
/// closed and cannot be booked. Replaces the old fixed-turnus model — bookings
/// now choose free date ranges and are only rejected when they hit a closure or
/// an already-booked room.
public class Closure
{
    public Guid Id { get; set; }

    /// Why the span is blocked (e.g. "Przerwa zimowa", "Remont pokoju 12").
    public required string Reason { get; set; }

    /// First closed day (inclusive).
    public DateOnly StartDate { get; set; }

    /// Last closed day (inclusive).
    public DateOnly EndDate { get; set; }

    /// Null = whole center is closed; set = only this room is blocked (e.g. maintenance).
    public Guid? RoomId { get; set; }

    public Room? Room { get; set; }

    public DateTime CreatedAt { get; set; }

    public uint RowVersion { get; set; }
}
