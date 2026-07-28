using CampCenter.Domain.Entities;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CampCenter.Infrastructure.Repositories;

public class BookingMealTimeRepository : IBookingMealTimeRepository
{
    private readonly AppDbContext _db;

    public BookingMealTimeRepository(AppDbContext db) => _db = db;

    public Task<List<BookingMealTime>> ListForBookingAsync(
        Guid bookingId,
        CancellationToken cancellationToken = default
    ) =>
        _db
            .BookingMealTimes.Where(m => m.BookingId == bookingId)
            .ToListAsync(cancellationToken);

    public Task<BookingMealTime?> GetAsync(
        Guid bookingId,
        Guid mealTimeDefaultId,
        CancellationToken cancellationToken = default
    ) =>
        _db.BookingMealTimes.FirstOrDefaultAsync(
            m => m.BookingId == bookingId && m.MealTimeDefaultId == mealTimeDefaultId,
            cancellationToken
        );

    public async Task AddAsync(
        BookingMealTime mealTime,
        CancellationToken cancellationToken = default
    ) => await _db.BookingMealTimes.AddAsync(mealTime, cancellationToken);

    public void Remove(BookingMealTime mealTime) => _db.BookingMealTimes.Remove(mealTime);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _db.SaveChangesAsync(cancellationToken);
}
