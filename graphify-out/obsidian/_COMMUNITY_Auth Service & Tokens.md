---
type: community
cohesion: 0.26
members: 17
---

# Auth Service & Tokens

**Cohesion:** 0.26 - loosely connected
**Members:** 17 nodes

## Members
- [[.CreateAccessToken()]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[.CreateTokenEntity()]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.GenerateRefreshToken()]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[.HashRefreshToken()]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[.IssueTokensAsync()]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.LoginAsync()_1]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.LogoutAsync()_1]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.RefreshAsync()_1]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.SaveChangesAsync()_3]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[AuthResult]] - code - src/CampCenter.Application/Models/AuthResult.cs
- [[AuthResult.cs]] - code - src/CampCenter.Application/Models/AuthResult.cs
- [[AuthService]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[CancellationToken_23]] - code
- [[DateTime_1]] - code
- [[Guid_11]] - code
- [[ITokenService]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[Task_22]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Auth_Service__Tokens
SORT file.name ASC
```

## Connections to other communities
- 9 edges to [[_COMMUNITY_Refresh Token Contract]]
- 5 edges to [[_COMMUNITY_Auth Controller]]
- 3 edges to [[_COMMUNITY_Auth DTOs & Models]]
- 3 edges to [[_COMMUNITY_Public Booking Service]]
- 3 edges to [[_COMMUNITY_JWT Token Service]]
- 3 edges to [[_COMMUNITY_Admin User & Token Config]]
- 2 edges to [[_COMMUNITY_Password Hashing (bcrypt)]]
- 2 edges to [[_COMMUNITY_Admin Booking & Notifications]]
- 2 edges to [[_COMMUNITY_Admin User Repository Contract]]
- 1 edge to [[_COMMUNITY_Login Normalizer]]
- 1 edge to [[_COMMUNITY_Refresh Token EF Config]]

## Top bridge nodes
- [[AuthService]] - degree 11, connects to 5 communities
- [[.LoginAsync()_1]] - degree 9, connects to 4 communities
- [[ITokenService]] - degree 8, connects to 4 communities
- [[.IssueTokensAsync()]] - degree 11, connects to 2 communities
- [[.CreateTokenEntity()]] - degree 7, connects to 2 communities