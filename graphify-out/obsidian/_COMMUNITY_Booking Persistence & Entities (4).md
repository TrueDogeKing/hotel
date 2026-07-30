---
type: community
cohesion: 0.29
members: 8
---

# Booking Persistence & Entities (4)

**Cohesion:** 0.29 - loosely connected
**Members:** 8 nodes

## Members
- [[.Configure()_1]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/BookingRoomAssignmentConfiguration.cs
- [[BookingRoomAssignment]] - code - src/CampCenter.Domain/Entities/BookingRoomAssignment.cs
- [[BookingRoomAssignment.cs]] - code - src/CampCenter.Domain/Entities/BookingRoomAssignment.cs
- [[BookingRoomAssignmentConfiguration]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/BookingRoomAssignmentConfiguration.cs
- [[BookingRoomAssignmentConfiguration.cs]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/BookingRoomAssignmentConfiguration.cs
- [[DateOnly_4]] - code
- [[EntityTypeBuilder_2]] - code
- [[Guid_19]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Booking_Persistence__Entities_4
SORT file.name ASC
```

## Connections to other communities
- 2 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 2 edges to [[_COMMUNITY_Persistence  Configurations]]
- 1 edge to [[_COMMUNITY_Room Management]]

## Top bridge nodes
- [[BookingRoomAssignmentConfiguration.cs]] - degree 3, connects to 2 communities
- [[BookingRoomAssignment]] - degree 6, connects to 1 community
- [[BookingRoomAssignmentConfiguration]] - degree 4, connects to 1 community
- [[BookingRoomAssignment.cs]] - degree 2, connects to 1 community