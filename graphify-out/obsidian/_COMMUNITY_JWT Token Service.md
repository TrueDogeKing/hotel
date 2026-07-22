---
type: community
cohesion: 0.25
members: 9
---

# JWT Token Service

**Cohesion:** 0.25 - loosely connected
**Members:** 9 nodes

## Members
- [[.GenerateRefreshToken()_1]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[.HashRefreshToken()_1]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[JwtSettings]] - code - src/CampCenter.Infrastructure/Auth/JwtSettings.cs
- [[JwtSettings.cs]] - code - src/CampCenter.Infrastructure/Auth/JwtSettings.cs
- [[JwtTokenService]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[RefreshTokenInfo]] - code - src/CampCenter.Application/Models/RefreshTokenInfo.cs
- [[RefreshTokenInfo.cs]] - code - src/CampCenter.Application/Models/RefreshTokenInfo.cs
- [[int_1]] - code
- [[string_3]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/JWT_Token_Service
SORT file.name ASC
```

## Connections to other communities
- 3 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 2 edges to [[_COMMUNITY_Rate Limiting & Startup]]
- 1 edge to [[_COMMUNITY_Auth DTOs & Models]]
- 1 edge to [[_COMMUNITY_Admin User & Token Config]]
- 1 edge to [[_COMMUNITY_Auth Controller]]

## Top bridge nodes
- [[JwtTokenService]] - degree 8, connects to 4 communities
- [[RefreshTokenInfo]] - degree 4, connects to 1 community
- [[RefreshTokenInfo.cs]] - degree 2, connects to 1 community
- [[JwtSettings.cs]] - degree 2, connects to 1 community