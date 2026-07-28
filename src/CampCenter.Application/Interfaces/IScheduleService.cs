using CampCenter.Application.DTOs.Schedule;

namespace CampCenter.Application.Interfaces;

public interface IScheduleService
{
    /// Group stay bars plus per-day counts for the month calendar.
    Task<ScheduleCalendarDto> GetCalendarAsync(
        DateOnly start,
        DateOnly end,
        CancellationToken cancellationToken = default
    );

    /// Everything happening on one day: the groups present (even those with no
    /// entries yet) and every meal/activity, in time order.
    Task<ScheduleDayDto> GetDayAsync(DateOnly date, CancellationToken cancellationToken = default);

    /// One group's whole camp programme, day by day.
    Task<BookingScheduleDto> GetForBookingAsync(
        Guid bookingId,
        CancellationToken cancellationToken = default
    );

    Task<ScheduleEntryDto> CreateEntryAsync(
        CreateScheduleEntryRequestDto request,
        CancellationToken cancellationToken = default
    );

    Task<ScheduleEntryDto> UpdateEntryAsync(
        Guid id,
        UpdateScheduleEntryRequestDto request,
        CancellationToken cancellationToken = default
    );

    Task DeleteEntryAsync(Guid id, CancellationToken cancellationToken = default);

    /// Each active center meal slot as it applies to this group, showing both the
    /// center times and the group's own.
    Task<List<BookingMealTimeDto>> GetBookingMealTimesAsync(
        Guid bookingId,
        CancellationToken cancellationToken = default
    );

    /// Sets this group's own time for a meal slot. Optionally re-times the meals
    /// already generated across the stay — always skipping entries an admin moved
    /// for one specific day.
    Task<ApplyBookingMealTimeResultDto> SetBookingMealTimeAsync(
        Guid bookingId,
        Guid mealTimeDefaultId,
        SetBookingMealTimeRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Drops the group's override so the slot follows the center default again,
    /// re-timing existing meals back (except one-day exceptions).
    Task<ApplyBookingMealTimeResultDto> ResetBookingMealTimeAsync(
        Guid bookingId,
        Guid mealTimeDefaultId,
        bool applyToExisting,
        CancellationToken cancellationToken = default
    );

    /// Creates the meals a stay should have from the active meal-time defaults.
    /// Idempotent: existing generated meals are left alone and deleted ones are
    /// not resurrected within the same (day, default) pair.
    Task<int> GenerateMealsForBookingAsync(
        Guid bookingId,
        CancellationToken cancellationToken = default
    );

    /// Runs generation for every live booking present in [from, to]. Serves as
    /// backfill, as recovery when the confirmation hook failed, and as "apply a
    /// newly added meal slot to upcoming groups".
    Task<GenerateMissingMealsResultDto> GenerateMissingMealsAsync(
        DateOnly from,
        DateOnly to,
        CancellationToken cancellationToken = default
    );
}
