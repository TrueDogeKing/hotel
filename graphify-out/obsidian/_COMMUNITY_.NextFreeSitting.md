---
type: community
members: 2
---

# .NextFreeSitting

**Members:** 2 nodes

## Members
- [[UpdateDietaryNotesRequestDto]] - code - src/CampCenter.Application/DTOs/Schedule/ScheduleDtos.cs
- [[UpdateDietaryNotesRequestValidator]] - code - src/CampCenter.Application/Validators/ScheduleValidators.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/NextFreeSitting
SORT file.name ASC
```

## Connections to other communities
- 1 edge to [[_COMMUNITY_src  api (1)]]
- 1 edge to [[_COMMUNITY_DTOs  Schedule (1)]]
- 1 edge to [[_COMMUNITY_ControllerBase]]
- 1 edge to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 1 edge to [[_COMMUNITY_Camp Session Management]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Validators]]

## Top bridge nodes
- [[UpdateDietaryNotesRequestDto]] - degree 5, connects to 4 communities
- [[UpdateDietaryNotesRequestValidator]] - degree 3, connects to 2 communities