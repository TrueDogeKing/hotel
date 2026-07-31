using CampCenter.Application.Common;
using CampCenter.Application.DTOs.Users;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;

namespace CampCenter.Application.Services;

/// <inheritdoc cref="IUserService"/>
public class UserService : IUserService
{
    private readonly IAdminUserRepository _users;
    private readonly IRefreshTokenRepository _refreshTokens;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(
        IAdminUserRepository users,
        IRefreshTokenRepository refreshTokens,
        IPasswordHasher passwordHasher
    )
    {
        _users = users;
        _refreshTokens = refreshTokens;
        _passwordHasher = passwordHasher;
    }

    public async Task<List<AdminUserDto>> ListAsync(
        Guid callerId,
        CancellationToken cancellationToken = default
    ) => [.. (await _users.ListAsync(cancellationToken)).Select(u => ToDto(u, callerId))];

    public async Task<AdminUserDto> CreateAsync(
        CreateUserRequestDto request,
        Guid callerId,
        CancellationToken cancellationToken = default
    )
    {
        var login = LoginNormalizer.Normalize(request.Login);
        if (await _users.GetByLoginAsync(login, cancellationToken) is not null)
        {
            throw new BusinessRuleViolationException($"Login \"{login}\" is already taken.");
        }

        var user = new AdminUser
        {
            Id = Guid.NewGuid(),
            Login = login,
            PasswordHash = _passwordHasher.Hash(request.Password),
            Role = ParseRole(request.Role),
            CreatedAt = DateTime.UtcNow,
        };

        await _users.AddAsync(user, cancellationToken);
        await _users.SaveChangesAsync(cancellationToken);
        return ToDto(user, callerId);
    }

    public async Task<AdminUserDto> SetRoleAsync(
        Guid id,
        SetUserRoleRequestDto request,
        Guid callerId,
        CancellationToken cancellationToken = default
    )
    {
        var user = await GetOrThrowAsync(id, cancellationToken);
        var role = ParseRole(request.Role);
        if (user.Role == role)
        {
            return ToDto(user, callerId);
        }

        if (user.Role == AdminUserRole.Administrator)
        {
            await GuardLastAdministratorAsync(cancellationToken);
        }

        user.Role = role;

        // The role travels in the access token, so the demoted account would keep
        // its old powers until that token expired. Dropping its refresh tokens ends
        // the session instead: the next silent refresh fails and it signs in again.
        // Revoked before the save, so the role change and the revocation land in one
        // transaction — a demotion that kept the sessions alive would be the unsafe
        // half to commit on its own.
        await _refreshTokens.RevokeAllActiveForUserAsync(
            user.Id,
            DateTime.UtcNow,
            cancellationToken
        );
        await _users.SaveChangesAsync(cancellationToken);
        return ToDto(user, callerId);
    }

    public async Task DeleteAsync(
        Guid id,
        Guid callerId,
        CancellationToken cancellationToken = default
    )
    {
        var user = await GetOrThrowAsync(id, cancellationToken);
        if (user.Id == callerId)
        {
            throw new BusinessRuleViolationException("You cannot delete your own account.");
        }

        if (user.Role == AdminUserRole.Administrator)
        {
            await GuardLastAdministratorAsync(cancellationToken);
        }

        await _refreshTokens.RevokeAllActiveForUserAsync(
            user.Id,
            DateTime.UtcNow,
            cancellationToken
        );
        _users.Remove(user);
        await _users.SaveChangesAsync(cancellationToken);
    }

    /// The panel has to keep at least one account that can administer it — losing
    /// the last administrator would leave nobody able to make another.
    private async Task GuardLastAdministratorAsync(CancellationToken cancellationToken)
    {
        if (await _users.CountByRoleAsync(AdminUserRole.Administrator, cancellationToken) <= 1)
        {
            throw new BusinessRuleViolationException(
                "This is the last administrator — promote another account first."
            );
        }
    }

    private async Task<AdminUser> GetOrThrowAsync(Guid id, CancellationToken cancellationToken) =>
        await _users.GetByIdAsync(id, cancellationToken)
        ?? throw new NotFoundException("Account not found.");

    private static AdminUserRole ParseRole(string role) =>
        Enum.TryParse<AdminUserRole>(role, ignoreCase: true, out var parsed)
            ? parsed
            : throw new BusinessRuleViolationException($"Unknown role \"{role}\".");

    private static AdminUserDto ToDto(AdminUser user, Guid callerId) =>
        new(user.Id, user.Login, user.Role.ToString(), user.CreatedAt, user.Id == callerId);
}
