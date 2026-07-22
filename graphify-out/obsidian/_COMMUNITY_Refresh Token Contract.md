---
type: community
cohesion: 0.39
members: 8
---

# Refresh Token Contract

**Cohesion:** 0.39 - loosely connected
**Members:** 8 nodes

## Members
- [[.AddAsync()_2]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[.GetByTokenHashAsync()_1]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[.RevokeAllActiveForUserAsync()]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[CancellationToken_33]] - code
- [[DateTime_10]] - code
- [[Guid_28]] - code
- [[IRefreshTokenRepository]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[Task_32]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Refresh_Token_Contract
SORT file.name ASC
```

## Connections to other communities
- 9 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 2 edges to [[_COMMUNITY_Refresh Token EF Config]]
- 1 edge to [[_COMMUNITY_Domain & Infra Namespaces]]
- 1 edge to [[_COMMUNITY_Refresh Token Repository]]

## Top bridge nodes
- [[IRefreshTokenRepository]] - degree 7, connects to 3 communities
- [[.AddAsync()_2]] - degree 6, connects to 2 communities
- [[.GetByTokenHashAsync()_1]] - degree 6, connects to 2 communities
- [[.RevokeAllActiveForUserAsync()]] - degree 6, connects to 1 community
- [[CancellationToken_33]] - degree 4, connects to 1 community