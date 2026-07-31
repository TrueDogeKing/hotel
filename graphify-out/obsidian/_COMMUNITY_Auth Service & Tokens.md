---
type: community
members: 25
---

# Auth Service & Tokens

**Members:** 25 nodes

## Members
- [[.AddAsync()_5]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[.CreateAccessToken()]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[.CreateTokenEntity()]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.GenerateRefreshToken()]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[.GetByTokenHashAsync()_1]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[.IssueTokensAsync()]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.LoginAsync()_1]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.LogoutAsync()_1]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.RefreshAsync()_1]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.RevokeAllActiveForUserAsync()]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[.SaveChangesAsync()_5]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[AuthResult]] - code - src/CampCenter.Application/Models/AuthResult.cs
- [[AuthResult.cs]] - code - src/CampCenter.Application/Models/AuthResult.cs
- [[AuthService]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[CancellationToken_32]] - code
- [[CancellationToken_48]] - code
- [[DateTime_2]] - code
- [[DateTime_15]] - code
- [[Guid_19]] - code
- [[Guid_45]] - code
- [[IRefreshTokenRepository]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[RefreshTokenInfo]] - code - src/CampCenter.Application/Models/RefreshTokenInfo.cs
- [[RefreshTokenInfo.cs]] - code - src/CampCenter.Application/Models/RefreshTokenInfo.cs
- [[Task_32]] - code
- [[Task_48]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Auth_Service__Tokens
SORT file.name ASC
```

## Connections to other communities
- 5 edges to [[_COMMUNITY_Auth Controller (1)]]
- 5 edges to [[_COMMUNITY_Admin Booking & Notifications (4)]]
- 4 edges to [[_COMMUNITY_Refresh Token Repository]]
- 3 edges to [[_COMMUNITY_Admin User & Token Config]]
- 3 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 3 edges to [[_COMMUNITY_tests  CampCenter.IntegrationTests (2)]]
- 2 edges to [[_COMMUNITY_Password Hashing (bcrypt)]]
- 2 edges to [[_COMMUNITY_CampCenter.Application  Services (1)]]
- 1 edge to [[_COMMUNITY_eslint]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Validators]]
- 1 edge to [[_COMMUNITY_Admin Booking & Notifications (1)]]
- 1 edge to [[_COMMUNITY_Public Booking Service (1)]]
- 1 edge to [[_COMMUNITY_Public Booking Service (2)]]

## Top bridge nodes
- [[AuthService]] - degree 11, connects to 5 communities
- [[.LoginAsync()_1]] - degree 9, connects to 4 communities
- [[IRefreshTokenRepository]] - degree 8, connects to 3 communities
- [[.GenerateRefreshToken()]] - degree 6, connects to 3 communities
- [[.CreateAccessToken()]] - degree 5, connects to 2 communities