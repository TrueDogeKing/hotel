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
    Task<List<AdminUserDto>> ListAsync(Guid callerId, CancellationToken cancellationToken = default);

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

    Task DeleteAsync(Guid id, Guid callerId, CancellationToken cancellationToken = default);
}
