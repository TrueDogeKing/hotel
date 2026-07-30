---
type: community
cohesion: 0.40
members: 5
---

# DTOs / Schedule (2)

**Cohesion:** 0.40 - moderately connected
**Members:** 5 nodes

## Members
- [[DeleteBookingMealsResultDto]] - code - src/CampCenter.Application/DTOs/Schedule/MealTimeDtos.cs
- [[MealTimeDtos.cs]] - code - src/CampCenter.Application/DTOs/Schedule/MealTimeDtos.cs
- [[NeighbourSittingDto]] - code - src/CampCenter.Application/DTOs/Schedule/MealTimeDtos.cs
- [[UpdateMealTimeDefaultRequestDto]] - code - src/CampCenter.Application/DTOs/Schedule/MealTimeDtos.cs
- [[UpdateMealTimeDefaultRequestValidator]] - code - src/CampCenter.Application/Validators/MealTimeValidators.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/DTOs_/_Schedule_2
SORT file.name ASC
```

## Connections to other communities
- 4 edges to [[_COMMUNITY_CampCenter.UnitTests  Validators]]
- 3 edges to [[_COMMUNITY_DTOs  Schedule (1)]]
- 2 edges to [[_COMMUNITY_CampCenter.Application  Services (2)]]
- 2 edges to [[_COMMUNITY_CampCenter.Application  Services (3)]]
- 1 edge to [[_COMMUNITY_Application Namespaces & DTOs]]
- 1 edge to [[_COMMUNITY_Validator Unit Tests]]

## Top bridge nodes
- [[MealTimeDtos.cs]] - degree 10, connects to 5 communities
- [[UpdateMealTimeDefaultRequestDto]] - degree 5, connects to 2 communities
- [[UpdateMealTimeDefaultRequestValidator]] - degree 3, connects to 2 communities
- [[NeighbourSittingDto]] - degree 2, connects to 1 community