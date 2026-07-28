using CampCenter.Application.DTOs.Schedule;
using CampCenter.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Admin;

/// The center's default meal slots. Meal generation for confirmed bookings works
/// from these; editing one affects future generation only.
[ApiController]
[Authorize]
[Route("api/admin/meal-times")]
public class MealTimesController : ControllerBase
{
    private readonly IMealTimeService _mealTimes;
    private readonly IValidator<CreateMealTimeDefaultRequestDto> _createValidator;
    private readonly IValidator<UpdateMealTimeDefaultRequestDto> _updateValidator;

    public MealTimesController(
        IMealTimeService mealTimes,
        IValidator<CreateMealTimeDefaultRequestDto> createValidator,
        IValidator<UpdateMealTimeDefaultRequestDto> updateValidator
    )
    {
        _mealTimes = mealTimes;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<MealTimeDefaultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken) =>
        Ok(await _mealTimes.GetAllAsync(cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(MealTimeDefaultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateMealTimeDefaultRequestDto request,
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

        var mealTime = await _mealTimes.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { id = mealTime.Id }, mealTime);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(MealTimeDefaultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateMealTimeDefaultRequestDto request,
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

        return Ok(await _mealTimes.UpdateAsync(id, request, cancellationToken));
    }

    /// Hard-deletes an unused slot; deactivates one that already produced meals.
    /// The response says which happened, like the rooms endpoint.
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(DeleteMealTimeDefaultResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) =>
        Ok(await _mealTimes.DeleteAsync(id, cancellationToken));
}
