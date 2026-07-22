---
type: community
cohesion: 0.20
members: 11
---

# Admin User & Token Config

**Cohesion:** 0.20 - loosely connected
**Members:** 11 nodes

## Members
- [[.Configure()]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/AdminUserConfiguration.cs
- [[.CreateAccessToken()_1]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[AccessToken]] - code - src/CampCenter.Application/Models/AccessToken.cs
- [[AccessToken.cs]] - code - src/CampCenter.Application/Models/AccessToken.cs
- [[AdminUser]] - code - src/CampCenter.Domain/Entities/AdminUser.cs
- [[AdminUser.cs]] - code - src/CampCenter.Domain/Entities/AdminUser.cs
- [[AdminUserConfiguration]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/AdminUserConfiguration.cs
- [[AdminUserConfiguration.cs]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/AdminUserConfiguration.cs
- [[DateTime_3]] - code
- [[EntityTypeBuilder]] - code
- [[Guid_17]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Admin_User__Token_Config
SORT file.name ASC
```

## Connections to other communities
- 3 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 3 edges to [[_COMMUNITY_Booking Persistence & Entities]]
- 2 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 2 edges to [[_COMMUNITY_Admin User Repository Contract]]
- 2 edges to [[_COMMUNITY_Admin User Repository]]
- 1 edge to [[_COMMUNITY_Auth DTOs & Models]]
- 1 edge to [[_COMMUNITY_Refresh Token EF Config]]
- 1 edge to [[_COMMUNITY_JWT Token Service]]

## Top bridge nodes
- [[AdminUser]] - degree 14, connects to 5 communities
- [[AdminUserConfiguration.cs]] - degree 3, connects to 2 communities
- [[AdminUserConfiguration]] - degree 4, connects to 1 community
- [[AccessToken]] - degree 3, connects to 1 community
- [[.CreateAccessToken()_1]] - degree 3, connects to 1 community