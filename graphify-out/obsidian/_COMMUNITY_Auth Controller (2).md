---
type: community
cohesion: 0.39
members: 8
---

# Auth Controller (2)

**Cohesion:** 0.39 - loosely connected
**Members:** 8 nodes

## Members
- [[.LoginAsync()]] - code - src/CampCenter.Application/Interfaces/IAuthService.cs
- [[.LogoutAsync()]] - code - src/CampCenter.Application/Interfaces/IAuthService.cs
- [[.RefreshAsync()]] - code - src/CampCenter.Application/Interfaces/IAuthService.cs
- [[CancellationToken_13]] - code
- [[IAuthService]] - code - src/CampCenter.Application/Interfaces/IAuthService.cs
- [[LoginRequestDto]] - code - src/CampCenter.Application/DTOs/Auth/LoginRequestDto.cs
- [[LoginRequestDto.cs]] - code - src/CampCenter.Application/DTOs/Auth/LoginRequestDto.cs
- [[Task_12]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Auth_Controller_2
SORT file.name ASC
```

## Connections to other communities
- 5 edges to [[_COMMUNITY_Auth Controller (1)]]
- 4 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 2 edges to [[_COMMUNITY_Rate Limiting & Startup]]
- 1 edge to [[_COMMUNITY_Validator Unit Tests]]

## Top bridge nodes
- [[IAuthService]] - degree 6, connects to 3 communities
- [[LoginRequestDto]] - degree 5, connects to 3 communities
- [[.LoginAsync()]] - degree 6, connects to 2 communities
- [[.RefreshAsync()]] - degree 5, connects to 2 communities
- [[.LogoutAsync()]] - degree 4, connects to 1 community