using CampCenter.Application.DTOs.Closures;
using CampCenter.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Admin;

/// Admin CRUD for closures (blokady) — spans when the whole center, or a single
/// room, is unavailable for booking.
[ApiController]
[Authorize]
[Route("api/admin/closures")]
public class ClosuresController : ControllerBase
{
    private readonly IClosureService _closures;
    private readonly IValidator<CreateClosureRequestDto> _createValidator;
    private readonly IValidator<UpdateClosureRequestDto> _updateValidator;

    public ClosuresController(
        IClosureService closures,
        IValidator<CreateClosureRequestDto> createValidator,
        IValidator<UpdateClosureRequestDto> updateValidator
    )
    {
        _closures = closures;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ClosureDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken) =>
        Ok(await _closures.GetAllAsync(cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(ClosureDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateClosureRequestDto request,
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

        var closure = await _closures.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { id = closure.Id }, closure);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ClosureDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateClosureRequestDto request,
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

        return Ok(await _closures.UpdateAsync(id, request, cancellationToken));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _closures.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
