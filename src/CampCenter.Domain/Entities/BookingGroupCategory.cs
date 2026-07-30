namespace CampCenter.Domain.Entities;

/// How a booking sits relative to today, for the dashboard's three group lists.
///
/// The three are disjoint and cover every booking, so a group appears in exactly
/// one of them: cancelled stays and stays that have already ended are Inactive
/// whatever their dates say, which leaves Current and Upcoming to mean what the
/// front desk means by them.
public enum BookingGroupCategory
{
    /// Holding rooms and here today — arrival on or before today, departure on or
    /// after it.
    Current,

    /// Holding rooms, arriving after today.
    Upcoming,

    /// Cancelled, or departed before today. Nothing left to prepare for.
    Inactive,
}
