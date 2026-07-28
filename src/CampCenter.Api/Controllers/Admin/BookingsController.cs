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

    public BookingsController(
        IAdminBookingService bookings,
        IValidator<UpdateDietaryNotesRequestDto> dietaryNotesValidator
    )
    {
        _bookings = bookings;
        _dietaryNotesValidator = dietaryNotesValidator;
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
