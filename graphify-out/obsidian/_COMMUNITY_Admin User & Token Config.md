---
type: community
cohesion: 0.14
members: 22
---

# Admin User & Token Config

**Cohesion:** 0.14 - loosely connected
**Members:** 22 nodes

## Members
- [[.Configure()]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/AdminUserConfiguration.cs
- [[.GetByIdAsync()]] - code - src/CampCenter.Domain/Repositories/IAdminUserRepository.cs
- [[.GetByIdAsync()_4]] - code - src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs
- [[.GetByLoginAsync()]] - code - src/CampCenter.Domain/Repositories/IAdminUserRepository.cs
- [[.GetByLoginAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs
- [[.SaveChangesAsync()]] - code - src/CampCenter.Domain/Repositories/IAdminUserRepository.cs
- [[.SaveChangesAsync()_5]] - code - src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs
- [[AdminUser]] - code - src/CampCenter.Domain/Entities/AdminUser.cs
- [[AdminUser.cs]] - code - src/CampCenter.Domain/Entities/AdminUser.cs
- [[AdminUserConfiguration]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/AdminUserConfiguration.cs
- [[AdminUserConfiguration.cs]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/AdminUserConfiguration.cs
- [[AdminUserRepository]] - code - src/CampCenter.Infrastructure/Repositories/AdminUserRepository.cs
- [[CancellationToken_30]] - code
- [[CancellationToken_39]] - code
- [[DateTime_3]] - code
- [[EntityTypeBuilder]] - code
- [[Guid_17]] - code
- [[Guid_25]] - code
- [[Guid_31]] - code
- [[IAdminUserRepository]] - code - src/CampCenter.Domain/Repositories/IAdminUserRepository.cs
- [[Task_29]] - code
- [[Task_38]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Admin_User__Token_Config
SORT file.name ASC
```

## Connections to other communities
- 4 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 3 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 2 edges to [[_COMMUNITY_JWT Token Service]]
- 2 edges to [[_COMMUNITY_Persistence  Configurations]]
- 1 edge to [[_COMMUNITY_Refresh Token Repository]]
- 1 edge to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (1)]]

## Top bridge nodes
- [[AdminUser]] - degree 13, connects to 3 communities
- [[IAdminUserRepository]] - degree 6, connects to 2 communities
- [[AdminUserRepository]] - degree 6, connects to 2 communities
- [[AdminUserConfiguration.cs]] - degree 3, connects to 2 communities
- [[.GetByLoginAsync()]] - degree 5, connects to 1 community