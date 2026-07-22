---
type: community
cohesion: 0.14
members: 28
---

# Auth Controller

**Cohesion:** 0.14 - loosely connected
**Members:** 28 nodes

## Members
- [[.BuildCookieOptions()]] - code - src/CampCenter.Api/Controllers/AuthController.cs
- [[.DeleteRefreshTokenCookie()]] - code - src/CampCenter.Api/Controllers/AuthController.cs
- [[.IssueTokens()]] - code - src/CampCenter.Api/Controllers/AuthController.cs
- [[.Login()]] - code - src/CampCenter.Api/Controllers/AuthController.cs
- [[.LoginAsync()]] - code - src/CampCenter.Application/Interfaces/IAuthService.cs
- [[.Logout()]] - code - src/CampCenter.Api/Controllers/AuthController.cs
- [[.LogoutAsync()]] - code - src/CampCenter.Application/Interfaces/IAuthService.cs
- [[.Refresh()]] - code - src/CampCenter.Api/Controllers/AuthController.cs
- [[.RefreshAsync()]] - code - src/CampCenter.Application/Interfaces/IAuthService.cs
- [[.SetRefreshTokenCookie()]] - code - src/CampCenter.Api/Controllers/AuthController.cs
- [[AuthController]] - code - src/CampCenter.Api/Controllers/AuthController.cs
- [[CancellationToken_6]] - code
- [[CancellationToken_13]] - code
- [[CookieOptions]] - code
- [[DateTime]] - code
- [[EnableRateLimiting]] - code
- [[HttpPost_4]] - code
- [[IActionResult_5]] - code
- [[IAuthService]] - code - src/CampCenter.Application/Interfaces/IAuthService.cs
- [[IValidator_2]] - code
- [[LoginRequestDto]] - code - src/CampCenter.Application/DTOs/Auth/LoginRequestDto.cs
- [[LoginRequestDto.cs]] - code - src/CampCenter.Application/DTOs/Auth/LoginRequestDto.cs
- [[ProducesResponseType_5]] - code
- [[RefreshTokenSettings]] - code - src/CampCenter.Infrastructure/Auth/RefreshTokenSettings.cs
- [[RefreshTokenSettings.cs]] - code - src/CampCenter.Infrastructure/Auth/RefreshTokenSettings.cs
- [[Task_6]] - code
- [[Task_12]] - code
- [[string_4]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Auth_Controller
SORT file.name ASC
```

## Connections to other communities
- 5 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 2 edges to [[_COMMUNITY_Rate Limiting & Startup]]
- 2 edges to [[_COMMUNITY_Auth DTOs & Models]]
- 1 edge to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 1 edge to [[_COMMUNITY_Validator Unit Tests]]
- 1 edge to [[_COMMUNITY_JWT Token Service]]

## Top bridge nodes
- [[AuthController]] - degree 12, connects to 2 communities
- [[IAuthService]] - degree 6, connects to 2 communities
- [[LoginRequestDto]] - degree 5, connects to 2 communities
- [[.IssueTokens()]] - degree 6, connects to 1 community
- [[.LoginAsync()]] - degree 6, connects to 1 community