---
type: community
members: 13
---

# JWT Token Service

**Members:** 13 nodes

## Members
- [[.CreateAccessToken()_1]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[.GenerateRefreshToken()_1]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[.HashRefreshToken()_1]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[AccessToken]] - code - src/CampCenter.Application/Models/AccessToken.cs
- [[AccessToken.cs]] - code - src/CampCenter.Application/Models/AccessToken.cs
- [[JwtSettings]] - code - src/CampCenter.Infrastructure/Auth/JwtSettings.cs
- [[JwtSettings.cs]] - code - src/CampCenter.Infrastructure/Auth/JwtSettings.cs
- [[JwtTokenService]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[RefreshTokenSettings]] - code - src/CampCenter.Infrastructure/Auth/RefreshTokenSettings.cs
- [[RefreshTokenSettings.cs]] - code - src/CampCenter.Infrastructure/Auth/RefreshTokenSettings.cs
- [[int_2]] - code
- [[string_6]] - code
- [[string_7]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/JWT_Token_Service
SORT file.name ASC
```

## Connections to other communities
- 3 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 3 edges to [[_COMMUNITY_CampCenter.Application  Services (1)]]
- 1 edge to [[_COMMUNITY_Auth Controller (1)]]
- 1 edge to [[_COMMUNITY_Domain & Infra Namespaces]]
- 1 edge to [[_COMMUNITY_Admin User & Token Config]]

## Top bridge nodes
- [[JwtTokenService]] - degree 8, connects to 2 communities
- [[RefreshTokenSettings]] - degree 4, connects to 1 community
- [[AccessToken]] - degree 3, connects to 1 community
- [[.CreateAccessToken()_1]] - degree 3, connects to 1 community
- [[.GenerateRefreshToken()_1]] - degree 3, connects to 1 community