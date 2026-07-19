namespace CampCenter.Domain.Entities;

/// A concrete room assigned to a booking. Rooms are auto-assigned at booking
/// creation (the booker only chooses counts per capacity); admins can reassign.
/// The unique index on (CampSessionId, RoomId) is the double-booking guard.
/// Rows are deleted when a booking is cancelled — that is what frees the rooms.
public class BookingRoomAssignment
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }

    public Booking? Booking { get; set; }

    public Guid RoomId { get; set; }

    public Room? Room { get; set; }

    /// Denormalized from the booking so the unique (session, room) index and the
    /// occupancy queries need no join.
    public Guid CampSessionId { get; set; }

    /// Suggested occupancy for this room (capacity for all but the last room of a
    /// mix, the remainder in the last one). Admins may adjust.
    public int PeopleCount { get; set; }
}
