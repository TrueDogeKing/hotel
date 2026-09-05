---
type: community
members: 6
---

# CampCenter.Application / Services (3)

**Members:** 6 nodes

## Members
- [[AbstractValidator]] - code
- [[CreateRoomRequestValidator]] - code - src/CampCenter.Application/Validators/RoomValidators.cs
- [[CreateUserRequestValidator]] - code - src/CampCenter.Application/Validators/UserValidators.cs
- [[RoomValidators.cs]] - code - src/CampCenter.Application/Validators/RoomValidators.cs
- [[SetUserRoleRequestValidator]] - code - src/CampCenter.Application/Validators/UserValidators.cs
- [[UpdateRoomRequestValidator]] - code - src/CampCenter.Application/Validators/RoomValidators.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/CampCenterApplication_/_Services_3
SORT file.name ASC
```

## Connections to other communities
- 3 edges to [[_COMMUNITY_UsersController]]
- 3 edges to [[_COMMUNITY_ScheduleValidators.cs]]
- 3 edges to [[_COMMUNITY_Camp Session Management]]
- 2 edges to [[_COMMUNITY_Room Management]]
- 2 edges to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 2 edges to [[_COMMUNITY_Validator Unit Tests]]
- 2 edges to [[_COMMUNITY_DTOs  Schedule (1)]]
- 1 edge to [[_COMMUNITY_CampCenter.Application.DTOs.Public]]
- 1 edge to [[_COMMUNITY_Room]]
- 1 edge to [[_COMMUNITY_.GetBlockedRoomIdsAsync]]

## Top bridge nodes
- [[AbstractValidator]] - degree 16, connects to 7 communities
- [[RoomValidators.cs]] - degree 4, connects to 2 communities
- [[CreateUserRequestValidator]] - degree 3, connects to 2 communities
- [[SetUserRoleRequestValidator]] - degree 3, connects to 2 communities
- [[CreateRoomRequestValidator]] - degree 3, connects to 1 community