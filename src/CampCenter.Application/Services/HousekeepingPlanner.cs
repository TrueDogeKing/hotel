using CampCenter.Domain.Entities;

namespace CampCenter.Application.Services;

/// One room's housekeeping job on one day, worked out from the room's bookings.
///
/// Outgoing and Incoming are the groups on either side of the job: for a departure only
/// the outgoing one is set, for an arrival only the incoming one, and a turnaround has
/// both — which is exactly the information the person cleaning the room needs (how many
/// beds to strip, how many to make up).
public record HousekeepingJob(
    Guid RoomId,
    RoomCleaningKind Kind,
    Booking? Outgoing,
    Booking? Incoming,
    BookingRoomAssignment? OutgoingAssignment,
    BookingRoomAssignment? IncomingAssignment
);

/// Works out which rooms housekeeping has to deal with on a given day. Pure and static
/// so it is unit-testable without a database — same shape as MealGenerationPlanner.
///
/// Nothing here is stored: the day's list is derived from the room assignments every
/// time it is read, so a reassignment or a cancellation is reflected immediately rather
/// than leaving somebody cleaning a room nobody is leaving. Only the progress against a
/// job is persisted (RoomCleaning).
public static class HousekeepingPlanner
{
    /// Every room needing attention on <paramref name="day"/>, one job per room.
    ///
    /// Assignment dates are half-open — EndDate is the checkout day, StartDate the
    /// arrival day — so a room whose assignment ends today is being vacated today, and
    /// one whose assignment starts today needs to be ready this morning. A room that is
    /// both is a turnaround, and the tightest job of the day.
    ///
    /// Rooms merely occupied through the day are deliberately absent: mid-stay rooms are
    /// the group's own business, and putting them on the list would bury the handful that
    /// actually need doing.
    public static List<HousekeepingJob> ForDay(IEnumerable<Booking> bookings, DateOnly day)
    {
        var leaving = new Dictionary<Guid, (Booking Booking, BookingRoomAssignment Assignment)>();
        var arriving = new Dictionary<Guid, (Booking Booking, BookingRoomAssignment Assignment)>();

        foreach (var booking in bookings)
        {
            foreach (var assignment in booking.RoomAssignments)
            {
                if (assignment.EndDate == day)
                {
                    leaving[assignment.RoomId] = (booking, assignment);
                }

                if (assignment.StartDate == day)
                {
                    arriving[assignment.RoomId] = (booking, assignment);
                }
            }
        }

        var jobs = new List<HousekeepingJob>();

        foreach (var (roomId, outgoing) in leaving)
        {
            var hasIncoming = arriving.TryGetValue(roomId, out var incoming);
            jobs.Add(
                new HousekeepingJob(
                    roomId,
                    hasIncoming ? RoomCleaningKind.Turnaround : RoomCleaningKind.Departure,
                    outgoing.Booking,
                    hasIncoming ? incoming.Booking : null,
                    outgoing.Assignment,
                    hasIncoming ? incoming.Assignment : null
                )
            );
        }

        // Whatever is left in `arriving` is a room nobody is vacating — free beforehand,
        // so it only needs making up rather than clearing out first.
        foreach (var (roomId, incoming) in arriving)
        {
            if (leaving.ContainsKey(roomId))
            {
                continue;
            }

            jobs.Add(
                new HousekeepingJob(
                    roomId,
                    RoomCleaningKind.Arrival,
                    null,
                    incoming.Booking,
                    null,
                    incoming.Assignment
                )
            );
        }

        return jobs;
    }
}
