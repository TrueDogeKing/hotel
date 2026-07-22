---
source_file: "src/CampCenter.Api/Controllers/Admin/DashboardController.cs"
type: "code"
community: "Admin Bookings Controller & DTOs"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Bookings_Controller__DTOs
---

# DashboardController

## Context

_Source: `src/CampCenter.Api/Controllers/Admin/DashboardController.cs` (defined near L8; showing L6–L21 of 21)._

```csharp
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
```

## Connections
- [[.Get()_1]] - `method` [EXTRACTED]
- [[ControllerBase]] - `inherits` [EXTRACTED]
- [[DashboardController.cs]] - `contains` [EXTRACTED]
- [[IAdminBookingService]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Bookings_Controller__DTOs