namespace CampCenter.Domain.Entities;

/// A concrete room assigned to a booking. Rooms are auto-assigned at booking
/// creation (the booker only chooses counts per capacity); admins can reassign.
/// A Postgres exclusion constraint over (RoomId, [StartDate, EndDate)) is the
/// double-booking guard: no two assignments of the same room may overlap in time.
/// Rows are deleted when a booking is cancelled — that is what frees the rooms.
public class BookingRoomAssignment
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }

    public Booking? Booking { get; set; }

    public Guid RoomId { get; set; }

    public Room? Room { get; set; }

    /// Denormalized from the booking so the overlap exclusion constraint and the
    /// occupancy queries need no join. Half-open: EndDate is the checkout day.
    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    /// Suggested occupancy for this room (capacity for all but the last room of a
    /// mix, the remainder in the last one). Admins may adjust.
    public int PeopleCount { get; set; }

    /// This room holds the group's supervisors (kadra) rather than its campers.
    /// Stored rather than inferred: once an admin has reassigned by hand, a
    /// three-person kadra room and a three-person camper room are identical in
    /// every other respect.
    public bool IsSupervisorRoom { get; set; }
}
