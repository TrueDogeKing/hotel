using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IBookingMealTimeRepository
{
    /// A group's meal-time overrides. Slots with no row here use the center default.
    Task<List<BookingMealTime>> ListForBookingAsync(
        Guid bookingId,
        CancellationToken cancellationToken = default
    );

    /// Overrides for several groups at once — used when seating a new group against
    /// the sittings the groups it overlaps have already taken.
    Task<List<BookingMealTime>> ListForBookingsAsync(
        IReadOnlyCollection<Guid> bookingIds,
        CancellationToken cancellationToken = default
    );

    Task<BookingMealTime?> GetAsync(
        Guid bookingId,
        Guid mealTimeDefaultId,
        CancellationToken cancellationToken = default
    );

    Task AddAsync(BookingMealTime mealTime, CancellationToken cancellationToken = default);

    void Remove(BookingMealTime mealTime);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
