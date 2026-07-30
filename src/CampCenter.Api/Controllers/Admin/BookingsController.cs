using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.DTOs.Schedule;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Admin;

/// Admin overview and management of bookings.
[ApiController]
[Authorize]
[Route("api/admin/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IAdminBookingService _bookings;
    private readonly IValidator<UpdateDietaryNotesRequestDto> _dietaryNotesValidator;
    private readonly IValidator<CreateAdminBookingRequestDto> _createValidator;

    public BookingsController(
        IAdminBookingService bookings,
        IValidator<UpdateDietaryNotesRequestDto> dietaryNotesValidator,
        IValidator<CreateAdminBookingRequestDto> createValidator
    )
    {
        _bookings = bookings;
        _dietaryNotesValidator = dietaryNotesValidator;
        _createValidator = createValidator;
    }

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

    /// Records a group taken by phone or at the door. No confirmation email is
    /// sent and the starting status is the admin's to choose.
    [HttpPost]
    [ProducesResponseType(typeof(AdminBookingDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        [FromBody] CreateAdminBookingRequestDto request,
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

        var created = await _bookings.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    /// Manual status override between any two statuses.
    [HttpPut("{id:guid}/status")]
    [ProducesResponseType(typeof(AdminBookingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> SetStatus(
        Guid id,
        [FromBody] SetBookingStatusRequestDto request,
        CancellationToken cancellationToken
    ) => Ok(await _bookings.SetStatusAsync(id, request, cancellationToken));

    [HttpPost("{id:guid}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
    {
        await _bookings.CancelAsync(id, cancellationToken);
        return NoContent();
    }

    /// Rooms this booking may occupy — the choices behind a room move.
    [HttpGet("{id:guid}/assignable-rooms")]
    [ProducesResponseType(typeof(List<AssignableRoomDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignableRooms(Guid id, CancellationToken cancellationToken) =>
        Ok(await _bookings.GetAssignableRoomsAsync(id, cancellationToken));

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

    /// Kitchen-facing dietary/preparation note for the group. Separate from the
    /// booker's own Notes.
    [HttpPut("{id:guid}/dietary-notes")]
    [ProducesResponseType(typeof(AdminBookingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateDietaryNotes(
        Guid id,
        [FromBody] UpdateDietaryNotesRequestDto request,
        CancellationToken cancellationToken
    )
    {
        var validation = await _dietaryNotesValidator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }

        return Ok(await _bookings.UpdateDietaryNotesAsync(id, request, cancellationToken));
    }
}
