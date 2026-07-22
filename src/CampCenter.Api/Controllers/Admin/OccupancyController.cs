using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Admin;

/// Per-room occupancy grid over an arbitrary date range: each room shown as
/// free, booked, or blocked by a closure.
[ApiController]
[Authorize]
[Route("api/admin/occupancy")]
public class OccupancyController : ControllerBase
{
    private readonly IAdminBookingService _bookings;

    public OccupancyController(IAdminBookingService bookings) => _bookings = bookings;

    [HttpGet]
    [ProducesResponseType(typeof(OccupancyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        CancellationToken cancellationToken
    ) => Ok(await _bookings.GetOccupancyAsync(start, end, cancellationToken));
}
