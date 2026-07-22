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

    [HttpPost("{id:guid}/reopen")]
    [ProducesResponseType(typeof(RoomTaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Reopen(Guid id, CancellationToken cancellationToken) =>
        Ok(await _tasks.SetStatusAsync(id, RoomTaskStatus.Open, cancellationToken));

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _tasks.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
