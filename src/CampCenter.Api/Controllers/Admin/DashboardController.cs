using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Admin;

[ApiController]
[Authorize]
[Route("api/admin/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IAdminBookingService _bookings;

    public DashboardController(IAdminBookingService bookings) => _bookings = bookings;

    [HttpGet]
    [ProducesResponseType(typeof(DashboardDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken) =>
        Ok(await _bookings.GetDashboardAsync(cancellationToken));

    /// One page of a dashboard group list. Fetched per fold as it is opened and
    /// scrolled, so the dashboard never loads the whole booking history up front.
    [HttpGet("groups")]
    [ProducesResponseType(typeof(BookingGroupPageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Groups(
        [FromQuery] BookingGroupCategory category,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 20,
        CancellationToken cancellationToken = default
    ) => Ok(await _bookings.GetGroupPageAsync(category, skip, take, cancellationToken));
}
