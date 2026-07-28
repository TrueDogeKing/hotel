namespace CampCenter.Domain.Entities;

public static class BookingStatuses
{
    /// Statuses that still hold rooms and represent a real stay. Cancelled bookings
    /// are excluded — cancelling deletes the assignments, which is what frees rooms.
    /// Shared so availability, occupancy and the schedule can never drift apart.
    public static readonly BookingStatus[] Live =
    [
        BookingStatus.PendingDeposit,
        BookingStatus.Confirmed,
        BookingStatus.Completed,
    ];
}
