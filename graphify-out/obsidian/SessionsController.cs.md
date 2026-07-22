---
source_file: "src/CampCenter.Api/Controllers/Admin/SessionsController.cs"
type: "code"
community: "Application Namespaces & DTOs"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_Namespaces__DTOs
---

# SessionsController.cs

## Context

_Source: `src/CampCenter.Api/Controllers/Admin/SessionsController.cs` — full file embedded (116 lines)._ ⚠️ **This file is deleted in the current working tree** (uncommitted change); context below is the committed version from git HEAD.

```csharp
using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.DTOs.Sessions;
using CampCenter.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Admin;

/// Admin CRUD + lifecycle (publish/archive) for camp sessions (turnusy).
[ApiController]
[Authorize]
[Route("api/admin/sessions")]
public class SessionsController : ControllerBase
{
    private readonly ICampSessionService _sessions;
    private readonly IValidator<CreateCampSessionRequestDto> _createValidator;
    private readonly IValidator<UpdateCampSessionRequestDto> _updateValidator;

    public SessionsController(
        ICampSessionService sessions,
        IValidator<CreateCampSessionRequestDto> createValidator,
        IValidator<UpdateCampSessionRequestDto> updateValidator
    )
    {
        _sessions = sessions;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<CampSessionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken) =>
        Ok(await _sessions.GetAllAsync(cancellationToken));

    /// Per-room occupancy grid of the session (booking, people count, open tasks).
    [HttpGet("{id:guid}/occupancy")]
    [ProducesResponseType(typeof(SessionOccupancyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOccupancy(
        Guid id,
        [FromServices] IAdminBookingService adminBookings,
        CancellationToken cancellationToken
    ) => Ok(await adminBookings.GetOccupancyAsync(id, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(CampSessionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateCampSessionRequestDto request,
        CancellationToken cancellationToken
    )
    {
        var validation = await _createValidator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }

        var session = await _sessions.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { id = session.Id }, session);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CampSessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateCampSessionRequestDto request,
        CancellationToken cancellationToken
    )
    {
        var validation = await _updateValidator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }

        return Ok(await _sessions.UpdateAsync(id, request, cancellationToken));
    }

    [HttpPost("{id:guid}/publish")]
    [ProducesResponseType(typeof(CampSessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Publish(Guid id, CancellationToken cancellationToken) =>
        Ok(await _sessions.PublishAsync(id, cancellationToken));

    [HttpPost("{id:guid}/archive")]
    [ProducesResponseType(typeof(CampSessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Archive(Guid id, CancellationToken cancellationToken) =>
        Ok(await _sessions.ArchiveAsync(id, cancellationToken));

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _sessions.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
```

## Connections
- [[CampCenter.Api.Controllers.Admin]] - `contains` [EXTRACTED]
- [[CampCenter.Application.DTOs.AdminPanel]] - `imports` [EXTRACTED]
- [[CampCenter.Application.DTOs.Sessions]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[SessionsController]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_Namespaces__DTOs