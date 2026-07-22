---
type: community
cohesion: 0.29
members: 8
---

# Refresh Token EF Config

**Cohesion:** 0.29 - loosely connected
**Members:** 8 nodes

## Members
- [[.Configure()_5]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/RefreshTokenConfiguration.cs
- [[DateTime_7]] - code
- [[EntityTypeBuilder_5]] - code
- [[Guid_22]] - code
- [[RefreshToken]] - code - src/CampCenter.Domain/Entities/RefreshToken.cs
- [[RefreshToken.cs]] - code - src/CampCenter.Domain/Entities/RefreshToken.cs
- [[RefreshTokenConfiguration]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/RefreshTokenConfiguration.cs
- [[RefreshTokenConfiguration.cs]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/RefreshTokenConfiguration.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Refresh_Token_EF_Config
SORT file.name ASC
```

## Connections to other communities
- 3 edges to [[_COMMUNITY_Booking Persistence & Entities]]
- 2 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 2 edges to [[_COMMUNITY_Refresh Token Contract]]
- 2 edges to [[_COMMUNITY_Refresh Token Repository]]
- 1 edge to [[_COMMUNITY_Auth Service & Tokens]]
- 1 edge to [[_COMMUNITY_Admin User & Token Config]]

## Top bridge nodes
- [[RefreshToken]] - degree 12, connects to 5 communities
- [[RefreshTokenConfiguration.cs]] - degree 3, connects to 2 communities
- [[RefreshTokenConfiguration]] - degree 4, connects to 1 community
- [[RefreshToken.cs]] - degree 2, connects to 1 community