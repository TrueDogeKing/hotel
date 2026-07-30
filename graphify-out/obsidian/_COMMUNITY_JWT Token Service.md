---
type: community
cohesion: 0.12
members: 19
---

# JWT Token Service

**Cohesion:** 0.12 - loosely connected
**Members:** 19 nodes

## Members
- [[.CreateAccessToken()]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[.CreateAccessToken()_1]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[.GenerateRefreshToken()]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[.GenerateRefreshToken()_1]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[.HashRefreshToken()]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[.HashRefreshToken()_1]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[AccessToken]] - code - src/CampCenter.Application/Models/AccessToken.cs
- [[AccessToken.cs]] - code - src/CampCenter.Application/Models/AccessToken.cs
- [[ITokenService]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[JwtSettings]] - code - src/CampCenter.Infrastructure/Auth/JwtSettings.cs
- [[JwtSettings.cs]] - code - src/CampCenter.Infrastructure/Auth/JwtSettings.cs
- [[JwtTokenService]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[RefreshTokenInfo]] - code - src/CampCenter.Application/Models/RefreshTokenInfo.cs
- [[RefreshTokenInfo.cs]] - code - src/CampCenter.Application/Models/RefreshTokenInfo.cs
- [[RefreshTokenSettings]] - code - src/CampCenter.Infrastructure/Auth/RefreshTokenSettings.cs
- [[RefreshTokenSettings.cs]] - code - src/CampCenter.Infrastructure/Auth/RefreshTokenSettings.cs
- [[int_1]] - code
- [[string_3]] - code
- [[string_4]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/JWT_Token_Service
SORT file.name ASC
```

## Connections to other communities
- 8 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 6 edges to [[_COMMUNITY_Rate Limiting & Startup]]
- 2 edges to [[_COMMUNITY_Admin User & Token Config]]
- 1 edge to [[_COMMUNITY_Auth Controller (1)]]

## Top bridge nodes
- [[ITokenService]] - degree 6, connects to 2 communities
- [[.CreateAccessToken()]] - degree 5, connects to 2 communities
- [[JwtTokenService]] - degree 8, connects to 1 community
- [[.GenerateRefreshToken()]] - degree 4, connects to 1 community
- [[RefreshTokenInfo]] - degree 4, connects to 1 community