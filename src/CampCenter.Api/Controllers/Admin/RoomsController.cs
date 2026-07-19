using CampCenter.Application.DTOs.Rooms;
using CampCenter.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Admin;

/// Admin CRUD for the room inventory.
[ApiController]
[Authorize]
[Route("api/admin/rooms")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _rooms;
    private readonly IValidator<CreateRoomRequestDto> _createValidator;
    private readonly IValidator<UpdateRoomRequestDto> _updateValidator;

    public RoomsController(
        IRoomService rooms,
        IValidator<CreateRoomRequestDto> createValidator,
        IValidator<UpdateRoomRequestDto> updateValidator
    )
    {
        _rooms = rooms;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<RoomDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken) =>
        Ok(await _rooms.GetAllAsync(cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(RoomDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        [FromBody] CreateRoomRequestDto request,
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

        var room = await _rooms.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { id = room.Id }, room);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateRoomRequestDto request,
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

        return Ok(await _rooms.UpdateAsync(id, request, cancellationToken));
    }

    /// Deletes a room without booking history; a room with history is deactivated
    /// instead. The response says which happened.
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(RoomDeleteResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _rooms.DeleteAsync(id, cancellationToken);
        return Ok(new RoomDeleteResultDto(deleted));
    }
}

/// <param name="Deleted">True when hard-deleted; false when deactivated (had history).</param>
public record RoomDeleteResultDto(bool Deleted);
