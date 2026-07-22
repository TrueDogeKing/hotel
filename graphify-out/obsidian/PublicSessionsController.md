---
source_file: "src/CampCenter.Api/Controllers/Public/PublicSessionsController.cs"
type: "code"
community: "Application Namespaces & DTOs"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_Namespaces__DTOs
---

# PublicSessionsController

## Context

_Source: `src/CampCenter.Api/Controllers/Public/PublicSessionsController.cs` — full file embedded (23 lines)._ ⚠️ **This file is deleted in the current working tree** (uncommitted change); context below is the committed version from git HEAD.

```csharp
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
```

## Connections
- [[.Get()_2]] - `method` [EXTRACTED]
- [[ControllerBase]] - `inherits` [EXTRACTED]
- [[IAvailabilityService]] - `references` [EXTRACTED]
- [[PublicSessionsController.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_Namespaces__DTOs