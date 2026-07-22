---
source_file: "src/CampCenter.Infrastructure/Auth/JwtTokenService.cs"
type: "code"
community: "Rate Limiting & Startup"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Rate_Limiting__Startup
---

# JwtTokenService.cs

## Context

_Source: `src/CampCenter.Infrastructure/Auth/JwtTokenService.cs` (defined near L1; showing L1–L46 of 76)._

```csharp
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CampCenter.Application.Interfaces;
using CampCenter.Application.Models;
using CampCenter.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CampCenter.Infrastructure.Auth;

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
```

## Connections
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Models]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Auth]] - `contains` [EXTRACTED]
- [[JwtTokenService]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Rate_Limiting__Startup