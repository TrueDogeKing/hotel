---
source_file: "src/CampCenter.Application/Services/AuthService.cs"
type: "code"
community: "Auth DTOs & Models"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_DTOs__Models
---

# AuthService.cs

## Context

_Source: `src/CampCenter.Application/Services/AuthService.cs` (defined near L1; showing L1–L46 of 165)._

```csharp
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

```

## Connections
- [[AuthService]] - `contains` [EXTRACTED]
- [[CampCenter.Application.Common]] - `imports` [EXTRACTED]
- [[CampCenter.Application.DTOs.Auth]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Models]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Services]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_DTOs__Models