---
source_file: "src/CampCenter.Infrastructure/Auth/JwtTokenService.cs"
type: "code"
community: "JWT Token Service"
location: "L15"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/JWT_Token_Service
---

# JwtTokenService

## Context

_Source: `src/CampCenter.Infrastructure/Auth/JwtTokenService.cs` (defined near L15; showing L13–L60 of 76)._

```csharp
/// Implementation of <see cref="ITokenService"/>: generates signed JWT tokens (HMAC-SHA256)
/// and cryptographically random refresh tokens.
public class JwtTokenService : ITokenService
{
    private const int RefreshTokenBytes = 32;

    private readonly JwtSettings _settings;
    private readonly RefreshTokenSettings _refreshSettings;

    ///Creates service with JWT and refresh token settings.
    public JwtTokenService(
        IOptions<JwtSettings> settings,
        IOptions<RefreshTokenSettings> refreshSettings
    )
    {
        _settings = settings.Value;
        _refreshSettings = refreshSettings.Value;
    }

    public AccessToken CreateAccessToken(AdminUser user)
    {
        var expiresAtUtc = DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new("preferred_username", user.Login),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: expiresAtUtc,
            signingCredentials: credentials
        );

        var encoded = new JwtSecurityTokenHandler().WriteToken(token);
        return new AccessToken(encoded, expiresAtUtc);
    }

    public RefreshTokenInfo GenerateRefreshToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(RefreshTokenBytes);
```

## Connections
- [[.CreateAccessToken()_1]] - `method` [EXTRACTED]
- [[.GenerateRefreshToken()_1]] - `method` [EXTRACTED]
- [[.HashRefreshToken()_1]] - `method` [EXTRACTED]
- [[ITokenService]] - `implements` [EXTRACTED]
- [[JwtSettings]] - `references` [EXTRACTED]
- [[JwtTokenService.cs]] - `contains` [EXTRACTED]
- [[RefreshTokenSettings]] - `references` [EXTRACTED]
- [[int_1]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/JWT_Token_Service