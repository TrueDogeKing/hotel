---
source_file: "src/CampCenter.Api/Controllers/AuthController.cs"
type: "code"
community: "Rate Limiting & Startup"
location: "L11"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Rate_Limiting__Startup
---

# CampCenter.Api.Controllers

## Context

_Source: `src/CampCenter.Api/Controllers/AuthController.cs` (defined near L11; showing L9–L56 of 127)._

```csharp
using Microsoft.Extensions.Options;

namespace CampCenter.Api.Controllers;

/// Admin authentication endpoints. Access token (JWT) is returned in the response body,
/// and refresh token is set in an HttpOnly cookie. There is no registration endpoint —
/// admin accounts are created by the data seeder.
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IValidator<LoginRequestDto> _loginValidator;
    private readonly RefreshTokenSettings _refreshSettings;

    /// Creates controller with dependencies.
    public AuthController(
        IAuthService authService,
        IValidator<LoginRequestDto> loginValidator,
        IOptions<RefreshTokenSettings> refreshSettings
    )
    {
        _authService = authService;
        _loginValidator = loginValidator;
        _refreshSettings = refreshSettings.Value;
    }

    /// Logs in an admin, returns an access token and sets a refresh token in an HttpOnly cookie.
    /// <param name="request">Login data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPost("login")]
    [EnableRateLimiting(RateLimitPolicies.Auth)]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto request,
        CancellationToken cancellationToken
    )
    {
        var validation = await _loginValidator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
```

## Connections
- [[AuthController.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Rate_Limiting__Startup