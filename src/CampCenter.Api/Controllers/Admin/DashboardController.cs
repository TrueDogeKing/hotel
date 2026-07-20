using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.Interfaces;
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
}
