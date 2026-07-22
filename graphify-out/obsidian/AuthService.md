---
source_file: "src/CampCenter.Application/Services/AuthService.cs"
type: "code"
community: "Auth Service & Tokens"
location: "L12"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_Service__Tokens
---

# AuthService

## Context

_Source: `src/CampCenter.Application/Services/AuthService.cs` (defined near L12; showing L10–L57 of 165)._

```csharp
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
```

## Connections
- [[.CreateTokenEntity()]] - `method` [EXTRACTED]
- [[.IssueTokensAsync()]] - `method` [EXTRACTED]
- [[.LoginAsync()_1]] - `method` [EXTRACTED]
- [[.LogoutAsync()_1]] - `method` [EXTRACTED]
- [[.RefreshAsync()_1]] - `method` [EXTRACTED]
- [[AuthService.cs]] - `contains` [EXTRACTED]
- [[IAdminUserRepository]] - `references` [EXTRACTED]
- [[IAuthService]] - `implements` [EXTRACTED]
- [[IPasswordHasher]] - `references` [EXTRACTED]
- [[IRefreshTokenRepository]] - `references` [EXTRACTED]
- [[ITokenService]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_Service__Tokens