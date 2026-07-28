using CampCenter.Application.DTOs.Schedule;
using CampCenter.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Admin;

/// The camp schedule: the month calendar of group stays, the hour-by-hour day
/// timetable, and each group's programme of meals and activities.
[ApiController]
[Authorize]
[Route("api/admin/schedule")]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _schedule;
    private readonly IValidator<CreateScheduleEntryRequestDto> _createValidator;
    private readonly IValidator<UpdateScheduleEntryRequestDto> _updateValidator;
    private readonly IValidator<SetBookingMealTimeRequestDto> _bookingMealTimeValidator;

    public ScheduleController(
        IScheduleService schedule,
        IValidator<CreateScheduleEntryRequestDto> createValidator,
        IValidator<UpdateScheduleEntryRequestDto> updateValidator,
        IValidator<SetBookingMealTimeRequestDto> bookingMealTimeValidator
    )
    {
        _schedule = schedule;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _bookingMealTimeValidator = bookingMealTimeValidator;
    }

    /// Group stay bars plus per-day counts, for the month grid.
    [HttpGet("calendar")]
    [ProducesResponseType(typeof(ScheduleCalendarDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCalendar(
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        CancellationToken cancellationToken
    ) => Ok(await _schedule.GetCalendarAsync(start, end, cancellationToken));

    /// Everything happening on one day, including groups that have no entries yet.
    [HttpGet("day/{date}")]
    [ProducesResponseType(typeof(ScheduleDayDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDay(DateOnly date, CancellationToken cancellationToken) =>
        Ok(await _schedule.GetDayAsync(date, cancellationToken));

    /// One group's whole programme, day by day.
    [HttpGet("bookings/{bookingId:guid}")]
    [ProducesResponseType(typeof(BookingScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetForBooking(
        Guid bookingId,
        CancellationToken cancellationToken
    ) => Ok(await _schedule.GetForBookingAsync(bookingId, cancellationToken));

    [HttpPost("entries")]
    [ProducesResponseType(typeof(ScheduleEntryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateEntry(
        [FromBody] CreateScheduleEntryRequestDto request,
        CancellationToken cancellationToken
    )
    {
        var validation = await _createValidator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }

        var entry = await _schedule.CreateEntryAsync(request, cancellationToken);
        return CreatedAtAction(
            nameof(GetForBooking),
            new { bookingId = entry.BookingId },
            entry
        );
    }

    [HttpPut("entries/{id:guid}")]
    [ProducesResponseType(typeof(ScheduleEntryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateEntry(
        Guid id,
        [FromBody] UpdateScheduleEntryRequestDto request,
        CancellationToken cancellationToken
    )
    {
        var validation = await _updateValidator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }

        return Ok(await _schedule.UpdateEntryAsync(id, request, cancellationToken));
    }

    [HttpDelete("entries/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEntry(Guid id, CancellationToken cancellationToken)
    {
        await _schedule.DeleteEntryAsync(id, cancellationToken);
        return NoContent();
    }

    /// Each center meal slot as it applies to this group — center times, this
    /// group's times, and whether they differ.
    [HttpGet("bookings/{bookingId:guid}/meal-times")]
    [ProducesResponseType(typeof(List<BookingMealTimeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookingMealTimes(
        Guid bookingId,
        CancellationToken cancellationToken
    ) => Ok(await _schedule.GetBookingMealTimesAsync(bookingId, cancellationToken));

    /// Gives this group its own time for a meal slot, optionally re-timing the
    /// meals already generated across the whole stay. Days an admin moved
    /// individually are always preserved.
    [HttpPut("bookings/{bookingId:guid}/meal-times/{mealTimeDefaultId:guid}")]
    [ProducesResponseType(typeof(ApplyBookingMealTimeResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> SetBookingMealTime(
        Guid bookingId,
        Guid mealTimeDefaultId,
        [FromBody] SetBookingMealTimeRequestDto request,
        CancellationToken cancellationToken
    )
    {
        var validation = await _bookingMealTimeValidator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }

        return Ok(
            await _schedule.SetBookingMealTimeAsync(
                bookingId,
                mealTimeDefaultId,
                request,
                cancellationToken
            )
        );
    }

    /// Puts the slot back on the center default for this group.
    [HttpDelete("bookings/{bookingId:guid}/meal-times/{mealTimeDefaultId:guid}")]
    [ProducesResponseType(typeof(ApplyBookingMealTimeResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ResetBookingMealTime(
        Guid bookingId,
        Guid mealTimeDefaultId,
        [FromQuery] bool applyToExisting,
        CancellationToken cancellationToken
    ) =>
        Ok(
            await _schedule.ResetBookingMealTimeAsync(
                bookingId,
                mealTimeDefaultId,
                applyToExisting,
                cancellationToken
            )
        );

    /// Creates any meals this group is missing from the active meal slots.
    /// Idempotent — deliberately deleted meals are not resurrected.
    [HttpPost("bookings/{bookingId:guid}/generate-meals")]
    [ProducesResponseType(typeof(GenerateMealsResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GenerateMeals(
        Guid bookingId,
        CancellationToken cancellationToken
    ) =>
        Ok(
            new GenerateMealsResultDto(
                await _schedule.GenerateMealsForBookingAsync(bookingId, cancellationToken)
            )
        );

    /// Same, for every live group present in the range. Backfill, recovery after a
    /// failed confirmation hook, and "apply a newly added meal slot".
    [HttpPost("generate-meals")]
    [ProducesResponseType(typeof(GenerateMissingMealsResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GenerateMissingMeals(
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to,
        CancellationToken cancellationToken
    ) => Ok(await _schedule.GenerateMissingMealsAsync(from, to, cancellationToken));
}
