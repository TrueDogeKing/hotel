---
type: community
cohesion: 0.18
members: 17
---

# Refresh Token Repository

**Cohesion:** 0.18 - loosely connected
**Members:** 17 nodes

## Members
- [[.AddAsync()_5]] - code - src/CampCenter.Infrastructure/Repositories/RefreshTokenRepository.cs
- [[.Configure()_4]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/RefreshTokenConfiguration.cs
- [[.GetByTokenHashAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/RefreshTokenRepository.cs
- [[.RevokeAllActiveForUserAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/RefreshTokenRepository.cs
- [[.SaveChangesAsync()_7]] - code - src/CampCenter.Infrastructure/Repositories/RefreshTokenRepository.cs
- [[CancellationToken_42]] - code
- [[DateTime_7]] - code
- [[DateTime_12]] - code
- [[EntityTypeBuilder_5]] - code
- [[Guid_22]] - code
- [[Guid_34]] - code
- [[RefreshToken]] - code - src/CampCenter.Domain/Entities/RefreshToken.cs
- [[RefreshToken.cs]] - code - src/CampCenter.Domain/Entities/RefreshToken.cs
- [[RefreshTokenConfiguration]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/RefreshTokenConfiguration.cs
- [[RefreshTokenConfiguration.cs]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/RefreshTokenConfiguration.cs
- [[RefreshTokenRepository]] - code - src/CampCenter.Infrastructure/Repositories/RefreshTokenRepository.cs
- [[Task_41]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Refresh_Token_Repository
SORT file.name ASC
```

## Connections to other communities
- 4 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 3 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 2 edges to [[_COMMUNITY_Persistence  Configurations]]
- 1 edge to [[_COMMUNITY_Admin User & Token Config]]
- 1 edge to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (1)]]

## Top bridge nodes
- [[RefreshTokenRepository]] - degree 7, connects to 3 communities
- [[RefreshToken]] - degree 11, connects to 2 communities
- [[RefreshTokenConfiguration.cs]] - degree 3, connects to 2 communities
- [[RefreshTokenConfiguration]] - degree 4, connects to 1 community
- [[RefreshToken.cs]] - degree 2, connects to 1 community