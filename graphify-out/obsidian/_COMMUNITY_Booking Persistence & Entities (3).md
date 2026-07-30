---
type: community
cohesion: 0.27
members: 10
---

# Booking Persistence & Entities (3)

**Cohesion:** 0.27 - loosely connected
**Members:** 10 nodes

## Members
- [[.Configure()_3]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/PaymentConfiguration.cs
- [[DateTime_6]] - code
- [[EntityTypeBuilder_4]] - code
- [[Guid_21]] - code
- [[Payment]] - code - src/CampCenter.Domain/Entities/Payment.cs
- [[Payment.cs]] - code - src/CampCenter.Domain/Entities/Payment.cs
- [[PaymentConfiguration]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/PaymentConfiguration.cs
- [[PaymentConfiguration.cs]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/PaymentConfiguration.cs
- [[PaymentKind]] - code - src/CampCenter.Domain/Entities/Payment.cs
- [[PaymentStatus]] - code - src/CampCenter.Domain/Entities/Payment.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Booking_Persistence__Entities_3
SORT file.name ASC
```

## Connections to other communities
- 2 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 2 edges to [[_COMMUNITY_Persistence  Configurations]]
- 1 edge to [[_COMMUNITY_Booking Persistence & Entities (2)]]

## Top bridge nodes
- [[PaymentConfiguration.cs]] - degree 3, connects to 2 communities
- [[Payment]] - degree 8, connects to 1 community
- [[Payment.cs]] - degree 4, connects to 1 community
- [[PaymentConfiguration]] - degree 4, connects to 1 community