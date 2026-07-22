---
source_file: "src/CampCenter.Api/Controllers/Admin/BookingsController.cs"
type: "code"
community: "Application Namespaces & DTOs"
location: "L7"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_Namespaces__DTOs
---

# CampCenter.Api.Controllers.Admin

## Context

_Source: `src/CampCenter.Api/Controllers/Admin/BookingsController.cs` (defined near L7; showing L5–L52 of 52)._

```csharp
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Admin;

/// Admin overview and management of bookings.
[ApiController]
[Authorize]
[Route("api/admin/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IAdminBookingService _bookings;

    public BookingsController(IAdminBookingService bookings) => _bookings = bookings;

    [HttpGet]
    [ProducesResponseType(typeof(List<AdminBookingDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        [FromQuery] BookingStatus? status,
        CancellationToken cancellationToken
    ) => Ok(await _bookings.ListAsync(status, cancellationToken));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AdminBookingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken) =>
        Ok(await _bookings.GetAsync(id, cancellationToken));

    [HttpPost("{id:guid}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
    {
        await _bookings.CancelAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpPut("{id:guid}/assignments")]
    [ProducesResponseType(typeof(AdminBookingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Reassign(
        Guid id,
        [FromBody] ReassignBookingRequestDto request,
        CancellationToken cancellationToken
    ) => Ok(await _bookings.ReassignAsync(id, request, cancellationToken));
}
```

## Connections
- [[BookingsController.cs]] - `contains` [EXTRACTED]
- [[DashboardController.cs]] - `contains` [EXTRACTED]
- [[RoomsController.cs]] - `contains` [EXTRACTED]
- [[SessionsController.cs]] - `contains` [EXTRACTED]
- [[TasksController.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_Namespaces__DTOs