using CampCenter.Application.Models;
using CampCenter.Domain.Entities;

namespace CampCenter.Application.Interfaces;

public interface ITokenService
{
    AccessToken CreateAccessToken(AdminUser user);

    RefreshTokenInfo GenerateRefreshToken();

    /// Returns the hash (SHA-256) of the raw refresh token value.
    /// <param name="rawToken">The raw refresh token value.</param>
    string HashRefreshToken(string rawToken);
}
