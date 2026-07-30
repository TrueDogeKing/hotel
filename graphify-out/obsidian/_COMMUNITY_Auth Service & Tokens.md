---
type: community
cohesion: 0.21
members: 21
---

# Auth Service & Tokens

**Cohesion:** 0.21 - loosely connected
**Members:** 21 nodes

## Members
- [[.AddAsync()_1]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[.CreateTokenEntity()]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.GetByTokenHashAsync()]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[.IssueTokensAsync()]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.LoginAsync()_1]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.LogoutAsync()_1]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.RefreshAsync()_1]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.RevokeAllActiveForUserAsync()]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[.SaveChangesAsync()_2]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[AuthResult]] - code - src/CampCenter.Application/Models/AuthResult.cs
- [[AuthResult.cs]] - code - src/CampCenter.Application/Models/AuthResult.cs
- [[AuthService]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[CancellationToken_23]] - code
- [[CancellationToken_33]] - code
- [[DateTime_1]] - code
- [[DateTime_10]] - code
- [[Guid_11]] - code
- [[Guid_28]] - code
- [[IRefreshTokenRepository]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[Task_22]] - code
- [[Task_32]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Auth_Service__Tokens
SORT file.name ASC
```

## Connections to other communities
- 8 edges to [[_COMMUNITY_JWT Token Service]]
- 4 edges to [[_COMMUNITY_Auth Controller (2)]]
- 4 edges to [[_COMMUNITY_Refresh Token Repository]]
- 3 edges to [[_COMMUNITY_Admin User & Token Config]]
- 2 edges to [[_COMMUNITY_Password Hashing (bcrypt)]]
- 2 edges to [[_COMMUNITY_Rate Limiting & Startup]]
- 1 edge to [[_COMMUNITY_Auth Controller (1)]]
- 1 edge to [[_COMMUNITY_Login Normalizer]]
- 1 edge to [[_COMMUNITY_Domain & Infra Namespaces]]

## Top bridge nodes
- [[AuthService]] - degree 11, connects to 5 communities
- [[.LoginAsync()_1]] - degree 9, connects to 4 communities
- [[.IssueTokensAsync()]] - degree 11, connects to 2 communities
- [[AuthResult]] - degree 7, connects to 2 communities
- [[.CreateTokenEntity()]] - degree 7, connects to 2 communities