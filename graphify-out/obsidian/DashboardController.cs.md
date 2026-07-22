---
source_file: "src/CampCenter.Api/Controllers/Admin/DashboardController.cs"
type: "code"
community: "Application Namespaces & DTOs"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_Namespaces__DTOs
---

# DashboardController.cs

## Context

_Source: `src/CampCenter.Api/Controllers/Admin/DashboardController.cs` (defined near L1; showing L1–L21 of 21)._

```csharp
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
```

## Connections
- [[CampCenter.Api.Controllers.Admin]] - `contains` [EXTRACTED]
- [[CampCenter.Application.DTOs.AdminPanel]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[DashboardController]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_Namespaces__DTOs