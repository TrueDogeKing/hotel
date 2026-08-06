using CampCenter.Application.DTOs.Users;

namespace CampCenter.Application.Interfaces;

/// Panel accounts: who may sign in and what they may do. Administrator-only —
/// the controller enforces that; this service assumes the caller is one.
///
/// Every method takes the caller's id so the two rules that protect the panel from
/// locking itself out can be applied in one place: an account may not delete
/// itself, and the last administrator may be neither deleted nor demoted.
public interface IUserService
{
    Task<List<AdminUserDto>> ListAsync(
        Guid callerId,
        CancellationToken cancellationToken = default
    );

    /// Throws BusinessRuleViolationException when the login is taken.
    Task<AdminUserDto> CreateAsync(
        CreateUserRequestDto request,
        Guid callerId,
        CancellationToken cancellationToken = default
    );

    Task<AdminUserDto> SetRoleAsync(
        Guid id,
        SetUserRoleRequestDto request,
        Guid callerId,
        CancellationToken cancellationToken = default
    );

    /// Resets an account's password. No current-password check: only an
    /// administrator can reach this, and they may already reset anyone's role or
    /// delete the account outright, so requiring the old password would guard
    /// nothing a determined admin could not already do another way. Ends the
    /// account's sessions the same as a role change, its own included.
    Task<AdminUserDto> SetPasswordAsync(
        Guid id,
        SetUserPasswordRequestDto request,
        Guid callerId,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(Guid id, Guid callerId, CancellationToken cancellationToken = default);
}
