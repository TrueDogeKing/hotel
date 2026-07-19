using CampCenter.Application.DTOs.Auth;
using CampCenter.Application.Models;

namespace CampCenter.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResult?> LoginAsync(
        LoginRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Exchanges a refresh token for a new token pair (rotation). Returns null if the token
    /// is unknown, expired, or revoked. Reuse of a rotated token is treated as theft and
    /// revokes all sessions of that admin.
    /// <param name="rawRefreshToken">Plaintext refresh token value.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<AuthResult?> RefreshAsync(
        string? rawRefreshToken,
        CancellationToken cancellationToken = default
    );

    Task LogoutAsync(string? rawRefreshToken, CancellationToken cancellationToken = default);
}
