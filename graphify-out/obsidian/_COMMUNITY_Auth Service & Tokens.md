---
type: community
members: 27
---

# Auth Service & Tokens

**Members:** 27 nodes

## Members
- [[.AddAsync()_4]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[.CreateAccessToken()]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[.CreateTokenEntity()]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.GenerateRefreshToken()]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[.GetByTokenHashAsync()_1]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[.HashRefreshToken()]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[.IssueTokensAsync()]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.LoginAsync()_1]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.LogoutAsync()_1]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.RefreshAsync()_1]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.RevokeAllActiveForUserAsync()]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[.SaveChangesAsync()_5]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[AuthResult]] - code - src/CampCenter.Application/Models/AuthResult.cs
- [[AuthResult.cs]] - code - src/CampCenter.Application/Models/AuthResult.cs
- [[AuthService]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[CancellationToken_30]] - code
- [[CancellationToken_45]] - code
- [[DateTime_2]] - code
- [[DateTime_15]] - code
- [[Guid_17]] - code
- [[Guid_42]] - code
- [[IRefreshTokenRepository]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[ITokenService]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[RefreshTokenInfo]] - code - src/CampCenter.Application/Models/RefreshTokenInfo.cs
- [[RefreshTokenInfo.cs]] - code - src/CampCenter.Application/Models/RefreshTokenInfo.cs
- [[Task_29]] - code
- [[Task_44]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Auth_Service__Tokens
SORT file.name ASC
```

## Connections to other communities
- 5 edges to [[_COMMUNITY_Auth Controller (1)]]
- 4 edges to [[_COMMUNITY_Admin User & Token Config]]
- 4 edges to [[_COMMUNITY_Refresh Token Repository]]
- 3 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 3 edges to [[_COMMUNITY_Public Booking Service (1)]]
- 3 edges to [[_COMMUNITY_JWT Token Service]]
- 2 edges to [[_COMMUNITY_Password Hashing (bcrypt)]]
- 2 edges to [[_COMMUNITY_Admin Booking & Notifications (1)]]
- 2 edges to [[_COMMUNITY_Admin Booking & Notifications (4)]]
- 1 edge to [[_COMMUNITY_Login Normalizer]]
- 1 edge to [[_COMMUNITY_Rate Limiting & Startup]]
- 1 edge to [[_COMMUNITY_Public Booking Service (2)]]

## Top bridge nodes
- [[ITokenService]] - degree 9, connects to 5 communities
- [[AuthService]] - degree 11, connects to 4 communities
- [[.LoginAsync()_1]] - degree 9, connects to 4 communities
- [[IRefreshTokenRepository]] - degree 7, connects to 2 communities
- [[.GenerateRefreshToken()]] - degree 6, connects to 2 communities