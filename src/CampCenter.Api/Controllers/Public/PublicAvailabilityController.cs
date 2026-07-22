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

    public PublicAvailabilityController(
        IAvailabilityService availability,
        IClosureRepository closures
    )
    {
        _availability = availability;
        _closures = closures;
    }

    [HttpGet]
    [ProducesResponseType(typeof(AvailabilityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        [FromQuery] int? headcount,
        CancellationToken cancellationToken
    )
    {
        if (end <= start)
        {
            ModelState.AddModelError(nameof(end), "The departure date must be after arrival.");
            return ValidationProblem(ModelState);
        }

        return Ok(
            await _availability.GetAvailabilityAsync(start, end, headcount, cancellationToken)
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
