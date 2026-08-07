using CampCenter.Application.DTOs.Public;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Public;

/// Public availability: free rooms and pricing for a chosen date range, plus the
/// list of upcoming center-wide closures so the site can flag closed periods.
[ApiController]
[Route("api/public/availability")]
public class PublicAvailabilityController : ControllerBase
{
    private readonly IAvailabilityService _availability;
    private readonly IClosureRepository _closures;
    private readonly IPricingService _pricing;

    public PublicAvailabilityController(
        IAvailabilityService availability,
        IClosureRepository closures,
        IPricingService pricing
    )
    {
        _availability = availability;
        _closures = closures;
        _pricing = pricing;
    }

    [HttpGet]
    [ProducesResponseType(typeof(AvailabilityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        [FromQuery] int? headcount,
        [FromQuery] int? supervisors,
        CancellationToken cancellationToken
    )
    {
        if (end <= start)
        {
            ModelState.AddModelError(nameof(end), "The departure date must be after arrival.");
            return ValidationProblem(ModelState);
        }

        return Ok(
            await _availability.GetAvailabilityAsync(
                start,
                end,
                headcount,
                supervisors,
                cancellationToken
            )
        );
    }

    /// Night-by-night availability for the booking calendar: which days can be
    /// slept in, and how many beds are left. Both ends inclusive — these are the
    /// days a calendar draws, not a stay.
    [HttpGet("calendar")]
    [ProducesResponseType(typeof(AvailabilityCalendarDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Calendar(
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        [FromQuery] int? headcount,
        CancellationToken cancellationToken
    ) => Ok(await _availability.GetCalendarAsync(start, end, headcount, cancellationToken));

    /// The centre's rates, with no stay attached — what the booking wizard quotes
    /// before any dates have been chosen. Anonymous: these are the prices the
    /// public site advertises.
    [HttpGet("/api/public/pricing")]
    [ProducesResponseType(typeof(PublicPricingDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Pricing(CancellationToken cancellationToken)
    {
        var rates = await _pricing.GetAsync(cancellationToken);
        return Ok(
            new PublicPricingDto(
                rates.PricePerPersonPerNightGrosze,
                rates.SupervisorPricePerPersonPerNightGrosze,
                rates.DepositPerPersonPerNightGrosze
            )
        );
    }

    /// Upcoming center-wide closures, for advertising closed periods on the site.
    [HttpGet("closures")]
    [ProducesResponseType(typeof(List<PublicClosureDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Closures(CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var closures = await _closures.GetUpcomingCenterWideAsync(today, cancellationToken);
        return Ok(
            closures.Select(c => new PublicClosureDto(c.Reason, c.StartDate, c.EndDate)).ToList()
        );
    }
}
