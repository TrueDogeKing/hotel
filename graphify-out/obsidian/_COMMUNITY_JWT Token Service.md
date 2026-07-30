---
type: community
members: 15
---

# JWT Token Service

**Members:** 15 nodes

## Members
- [[.CreateAccessToken()_1]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[.GenerateRefreshToken()_1]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[.HashRefreshToken()_1]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[AccessToken]] - code - src/CampCenter.Application/Models/AccessToken.cs
- [[AccessToken.cs]] - code - src/CampCenter.Application/Models/AccessToken.cs
- [[JwtSettings]] - code - src/CampCenter.Infrastructure/Auth/JwtSettings.cs
- [[JwtSettings.cs]] - code - src/CampCenter.Infrastructure/Auth/JwtSettings.cs
- [[JwtTokenService]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[RefreshTokenInfo]] - code - src/CampCenter.Application/Models/RefreshTokenInfo.cs
- [[RefreshTokenInfo.cs]] - code - src/CampCenter.Application/Models/RefreshTokenInfo.cs
- [[RefreshTokenSettings]] - code - src/CampCenter.Infrastructure/Auth/RefreshTokenSettings.cs
- [[RefreshTokenSettings.cs]] - code - src/CampCenter.Infrastructure/Auth/RefreshTokenSettings.cs
- [[int_1]] - code
- [[string_6]] - code
- [[string_7]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/JWT_Token_Service
SORT file.name ASC
```

## Connections to other communities
- 5 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (5)]]
- 4 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 1 edge to [[_COMMUNITY_Auth Controller (1)]]
- 1 edge to [[_COMMUNITY_Admin User & Token Config]]

## Top bridge nodes
- [[JwtTokenService]] - degree 8, connects to 2 communities
- [[RefreshTokenInfo]] - degree 4, connects to 1 community
- [[RefreshTokenSettings]] - degree 4, connects to 1 community
- [[AccessToken]] - degree 3, connects to 1 community
- [[.CreateAccessToken()_1]] - degree 3, connects to 1 community