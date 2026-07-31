using CampCenter.Api.Extensions;
using CampCenter.Application.DTOs.Users;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Admin;

/// Panel accounts: who may sign in, and as what.
///
/// Readable by any signed-in account, like every other section — adding, deleting
/// and re-roling are writes, which the default policy already limits to
/// administrators. That does mean a worker can see the list of logins and their
/// roles; restricting this one controller to administrators is a one-line change
/// if that is not wanted.
[ApiController]
[Authorize]
[Route("api/admin/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _users;
    private readonly IValidator<CreateUserRequestDto> _createValidator;
    private readonly IValidator<SetUserRoleRequestDto> _roleValidator;

    public UsersController(
        IUserService users,
        IValidator<CreateUserRequestDto> createValidator,
        IValidator<SetUserRoleRequestDto> roleValidator
    )
    {
        _users = users;
        _createValidator = createValidator;
        _roleValidator = roleValidator;
    }

    /// The signed-in account's id, from the token itself. The two self-lockout
    /// guards are enforced against this rather than anything the caller sends, so
    /// a request cannot claim to be someone else.
    private Guid CallerId => User.GetUserId();

    [HttpGet]
    [ProducesResponseType(typeof(List<AdminUserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(CancellationToken cancellationToken) =>
        Ok(await _users.ListAsync(CallerId, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(AdminUserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserRequestDto request,
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

        var created = await _users.CreateAsync(request, CallerId, cancellationToken);
        return CreatedAtAction(nameof(List), new { id = created.Id }, created);
    }

    /// Promote or demote. Ends the affected account's sessions, because the role
    /// travels in its access token.
    [HttpPut("{id:guid}/role")]
    [ProducesResponseType(typeof(AdminUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetRole(
        Guid id,
        [FromBody] SetUserRoleRequestDto request,
        CancellationToken cancellationToken
    )
    {
        var validation = await _roleValidator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }

        return Ok(await _users.SetRoleAsync(id, request, CallerId, cancellationToken));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _users.DeleteAsync(id, CallerId, cancellationToken);
        return NoContent();
    }
}
