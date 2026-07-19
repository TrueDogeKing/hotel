namespace CampCenter.Application.Models;

/// <param name="AccessToken">Access token (JWT).</param>
/// <param name="AccessTokenExpiresAtUtc">Access token expiration time (UTC).</param>
/// <param name="Login">Login of the authenticated user.</param>
/// <param name="RefreshToken">Plaintext refresh token value.</param>
/// <param name="RefreshTokenExpiresAtUtc">Refresh token expiration time (UTC).</param>
public record AuthResult(
    string AccessToken,
    DateTime AccessTokenExpiresAtUtc,
    string Login,
    string RefreshToken,
    DateTime RefreshTokenExpiresAtUtc
);
