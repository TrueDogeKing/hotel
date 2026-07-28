using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IBookingMealTimeRepository
{
    /// A group's meal-time overrides. Slots with no row here use the center default.
    Task<List<BookingMealTime>> ListForBookingAsync(
        Guid bookingId,
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
