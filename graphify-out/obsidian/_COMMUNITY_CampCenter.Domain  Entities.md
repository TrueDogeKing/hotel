---
type: community
cohesion: 0.19
members: 13
---

# CampCenter.Domain / Entities

**Cohesion:** 0.19 - loosely connected
**Members:** 13 nodes

## Members
- [[.Configure()_10]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/RoomCleaningConfiguration.cs
- [[.KindOrder()]] - code - src/CampCenter.Application/Services/HousekeepingService.cs
- [[.ParseStatus()_1]] - code - src/CampCenter.Application/Services/HousekeepingService.cs
- [[DateOnly_24]] - code
- [[DateTime_16]] - code
- [[EntityTypeBuilder_10]] - code
- [[Guid_52]] - code
- [[RoomCleaning]] - code - src/CampCenter.Domain/Entities/RoomCleaning.cs
- [[RoomCleaning.cs]] - code - src/CampCenter.Domain/Entities/RoomCleaning.cs
- [[RoomCleaningConfiguration]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/RoomCleaningConfiguration.cs
- [[RoomCleaningConfiguration.cs]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/RoomCleaningConfiguration.cs
- [[RoomCleaningKind_1]] - code - src/CampCenter.Domain/Entities/RoomCleaning.cs
- [[RoomCleaningStatus_1]] - code - src/CampCenter.Domain/Entities/RoomCleaning.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/CampCenterDomain_/_Entities
SORT file.name ASC
```

## Connections to other communities
- 4 edges to [[_COMMUNITY_CampCenter.Application  Services (4)]]
- 3 edges to [[_COMMUNITY_CampCenter.Domain  Repositories (3)]]
- 3 edges to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (3)]]
- 2 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 2 edges to [[_COMMUNITY_Persistence  Configurations]]
- 1 edge to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (1)]]

## Top bridge nodes
- [[RoomCleaning]] - degree 15, connects to 3 communities
- [[RoomCleaningConfiguration.cs]] - degree 3, connects to 2 communities
- [[RoomCleaning.cs]] - degree 4, connects to 1 community
- [[RoomCleaningConfiguration]] - degree 4, connects to 1 community
- [[.KindOrder()]] - degree 3, connects to 1 community