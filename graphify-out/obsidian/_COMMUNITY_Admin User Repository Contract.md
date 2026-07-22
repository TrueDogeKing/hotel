---
type: community
cohesion: 0.48
members: 7
---

# Admin User Repository Contract

**Cohesion:** 0.48 - moderately connected
**Members:** 7 nodes

## Members
- [[.GetByIdAsync()]] - code - src/CampCenter.Domain/Repositories/IAdminUserRepository.cs
- [[.GetByLoginAsync()]] - code - src/CampCenter.Domain/Repositories/IAdminUserRepository.cs
- [[.SaveChangesAsync()]] - code - src/CampCenter.Domain/Repositories/IAdminUserRepository.cs
- [[CancellationToken_30]] - code
- [[Guid_25]] - code
- [[IAdminUserRepository]] - code - src/CampCenter.Domain/Repositories/IAdminUserRepository.cs
- [[Task_29]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Admin_User_Repository_Contract
SORT file.name ASC
```

## Connections to other communities
- 2 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 2 edges to [[_COMMUNITY_Admin User & Token Config]]
- 1 edge to [[_COMMUNITY_Domain & Infra Namespaces]]
- 1 edge to [[_COMMUNITY_Admin User Repository]]

## Top bridge nodes
- [[IAdminUserRepository]] - degree 6, connects to 3 communities
- [[.GetByLoginAsync()]] - degree 5, connects to 2 communities
- [[.GetByIdAsync()]] - degree 5, connects to 1 community