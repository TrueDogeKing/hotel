using CampCenter.Application.DTOs.Public;
using CampCenter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Public;

/// Public availability: published upcoming sessions, optionally scored for a headcount.
[ApiController]
[Route("api/public/sessions")]
public class PublicSessionsController : ControllerBase
{
    private readonly IAvailabilityService _availability;

    public PublicSessionsController(IAvailabilityService availability) =>
        _availability = availability;

    [HttpGet]
    [ProducesResponseType(typeof(List<PublicSessionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromQuery] int? headcount,
        CancellationToken cancellationToken
    ) => Ok(await _availability.GetPublicSessionsAsync(headcount, cancellationToken));
}
