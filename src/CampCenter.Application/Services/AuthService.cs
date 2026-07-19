using CampCenter.Application.Common;
using CampCenter.Application.DTOs.Auth;
using CampCenter.Application.Interfaces;
using CampCenter.Application.Models;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Repositories;

namespace CampCenter.Application.Services;

/// Implementation of <see cref="IAuthService"/>: verifies credentials, issues access and refresh tokens,
/// and handles rotation and revocation. Only admin accounts exist; there is no registration.
public class AuthService : IAuthService
{
    private readonly IAdminUserRepository _admins;
    private readonly IRefreshTokenRepository _refreshTokens;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    /// Creates service with dependencies.
    public AuthService(
        IAdminUserRepository admins,
        IRefreshTokenRepository refreshTokens,
        IPasswordHasher passwordHasher,
        ITokenService tokenService
    )
    {
        _admins = admins;
        _refreshTokens = refreshTokens;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<AuthResult?> LoginAsync(
        LoginRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var admin = await _admins.GetByLoginAsync(
            LoginNormalizer.Normalize(request.Login),
            cancellationToken
        );
        if (admin is null || !_passwordHasher.Verify(request.Password, admin.PasswordHash))
        {
            return null;
        }

        return await IssueTokensAsync(admin, cancellationToken);
    }

    public async Task<AuthResult?> RefreshAsync(
        string? rawRefreshToken,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(rawRefreshToken))
        {
            return null;
        }

        var tokenHash = _tokenService.HashRefreshToken(rawRefreshToken);
        var stored = await _refreshTokens.GetByTokenHashAsync(tokenHash, cancellationToken);
        if (stored is null)
        {
            return null;
        }

        var now = DateTime.UtcNow;

        // Reuse of a revoked (rotated) token = possible theft.
        // Action: revoke all active sessions for this admin.
        if (stored.RevokedAtUtc is not null)
        {
            await _refreshTokens.RevokeAllActiveForUserAsync(
                stored.AdminUserId,
                now,
                cancellationToken
            );
            await _refreshTokens.SaveChangesAsync(cancellationToken);
            return null;
        }

        if (stored.ExpiresAtUtc <= now || stored.AdminUser is null)
        {
            return null;
        }

        // Rotation: current token is revoked and replaced with a new one.
        var refresh = _tokenService.GenerateRefreshToken();
        await _refreshTokens.AddAsync(
            CreateTokenEntity(stored.AdminUserId, refresh, now),
            cancellationToken
        );

        stored.RevokedAtUtc = now;
        stored.ReplacedByTokenHash = refresh.TokenHash;
        await _refreshTokens.SaveChangesAsync(cancellationToken);

        var access = _tokenService.CreateAccessToken(stored.AdminUser);
        return new AuthResult(
            access.Token,
            access.ExpiresAtUtc,
            stored.AdminUser.Login,
            refresh.RawToken,
            refresh.ExpiresAtUtc
        );
    }

    public async Task LogoutAsync(
        string? rawRefreshToken,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(rawRefreshToken))
        {
            return;
        }

        var tokenHash = _tokenService.HashRefreshToken(rawRefreshToken);
        var stored = await _refreshTokens.GetByTokenHashAsync(tokenHash, cancellationToken);
        if (stored is null || stored.RevokedAtUtc is not null)
        {
            return;
        }

        stored.RevokedAtUtc = DateTime.UtcNow;
        await _refreshTokens.SaveChangesAsync(cancellationToken);
    }

    private async Task<AuthResult> IssueTokensAsync(
        AdminUser admin,
        CancellationToken cancellationToken
    )
    {
        var access = _tokenService.CreateAccessToken(admin);
        var refresh = _tokenService.GenerateRefreshToken();

        await _refreshTokens.AddAsync(
            CreateTokenEntity(admin.Id, refresh, DateTime.UtcNow),
            cancellationToken
        );
        await _refreshTokens.SaveChangesAsync(cancellationToken);

        return new AuthResult(
            access.Token,
            access.ExpiresAtUtc,
            admin.Login,
            refresh.RawToken,
            refresh.ExpiresAtUtc
        );
    }

    private static RefreshToken CreateTokenEntity(
        Guid adminUserId,
        RefreshTokenInfo info,
        DateTime createdAtUtc
    ) =>
        new()
        {
            Id = Guid.NewGuid(),
            AdminUserId = adminUserId,
            TokenHash = info.TokenHash,
            ExpiresAtUtc = info.ExpiresAtUtc,
            CreatedAtUtc = createdAtUtc,
        };
}
