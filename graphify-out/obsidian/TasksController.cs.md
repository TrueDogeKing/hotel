---
source_file: "src/CampCenter.Api/Controllers/Admin/TasksController.cs"
type: "code"
community: "Application Namespaces & DTOs"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_Namespaces__DTOs
---

# TasksController.cs

## Context

_Source: `src/CampCenter.Api/Controllers/Admin/TasksController.cs` (defined near L1; showing L1–L46 of 61)._

```csharp
using CampCenter.Api.Extensions;
using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Admin;

/// Housekeeping tasks attached to rooms (e.g. "add one extra bed").
[ApiController]
[Authorize]
[Route("api/admin/tasks")]
public class TasksController : ControllerBase
{
    private readonly IRoomTaskService _tasks;

    public TasksController(IRoomTaskService tasks) => _tasks = tasks;

    [HttpGet]
    [ProducesResponseType(typeof(List<RoomTaskDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        [FromQuery] RoomTaskStatus? status,
        [FromQuery] Guid? bookingId,
        CancellationToken cancellationToken
    ) => Ok(await _tasks.ListAsync(status, bookingId, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(RoomTaskDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(
        [FromBody] CreateRoomTaskRequestDto request,
        CancellationToken cancellationToken
    )
    {
        var task = await _tasks.CreateAsync(request, User.GetUserId(), cancellationToken);
        return CreatedAtAction(nameof(List), new { id = task.Id }, task);
    }

    [HttpPost("{id:guid}/done")]
    [ProducesResponseType(typeof(RoomTaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Done(Guid id, CancellationToken cancellationToken) =>
        Ok(await _tasks.SetStatusAsync(id, RoomTaskStatus.Done, cancellationToken));

```

## Connections
- [[CampCenter.Api.Controllers.Admin]] - `contains` [EXTRACTED]
- [[CampCenter.Api.Extensions]] - `imports` [EXTRACTED]
- [[CampCenter.Application.DTOs.AdminPanel]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[TasksController]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_Namespaces__DTOs