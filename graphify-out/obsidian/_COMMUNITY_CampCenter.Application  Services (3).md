---
type: community
cohesion: 0.15
members: 33
---

# CampCenter.Application / Services (3)

**Cohesion:** 0.15 - loosely connected
**Members:** 33 nodes

## Members
- [[.AddAsync()_10]] - code - src/CampCenter.Domain/Repositories/IMealTimeDefaultRepository.cs
- [[.CreateAsync()_13]] - code - src/CampCenter.Application/Services/MealTimeService.cs
- [[.DeleteAsync()_9]] - code - src/CampCenter.Application/Services/MealTimeService.cs
- [[.GetActiveAsync()_2]] - code - src/CampCenter.Domain/Repositories/IMealTimeDefaultRepository.cs
- [[.GetAllAsync()_11]] - code - src/CampCenter.Application/Services/MealTimeService.cs
- [[.GetAllAsync()_12]] - code - src/CampCenter.Domain/Repositories/IMealTimeDefaultRepository.cs
- [[.GetByIdAsync()_9]] - code - src/CampCenter.Domain/Repositories/IMealTimeDefaultRepository.cs
- [[.GetOrThrowAsync()_3]] - code - src/CampCenter.Application/Services/MealTimeService.cs
- [[.GuardTimes()]] - code - src/CampCenter.Application/Services/MealTimeService.cs
- [[.IsReferencedAsync()]] - code - src/CampCenter.Domain/Repositories/IMealTimeDefaultRepository.cs
- [[.ParseMealKind()]] - code - src/CampCenter.Application/Services/MealTimeService.cs
- [[.Remove()_7]] - code - src/CampCenter.Domain/Repositories/IMealTimeDefaultRepository.cs
- [[.SaveChangesAsync()_12]] - code - src/CampCenter.Domain/Repositories/IMealTimeDefaultRepository.cs
- [[.ToDto()_5]] - code - src/CampCenter.Application/Services/MealTimeService.cs
- [[.UpdateAsync()_7]] - code - src/CampCenter.Application/Services/MealTimeService.cs
- [[CancellationToken_58]] - code
- [[CancellationToken_61]] - code
- [[DateTime_15]] - code
- [[Guid_48]] - code
- [[Guid_51]] - code
- [[Guid_55]] - code
- [[IMealTimeDefaultRepository]] - code - src/CampCenter.Domain/Repositories/IMealTimeDefaultRepository.cs
- [[List_29]] - code
- [[List_32]] - code
- [[MealKind_1]] - code - src/CampCenter.Domain/Entities/MealTimeDefault.cs
- [[MealTimeDefault_1]] - code - src/CampCenter.Domain/Entities/MealTimeDefault.cs
- [[MealTimeDefault.cs]] - code - src/CampCenter.Domain/Entities/MealTimeDefault.cs
- [[MealTimeDefaultDto]] - code - src/CampCenter.Application/DTOs/Schedule/MealTimeDtos.cs
- [[MealTimeService]] - code - src/CampCenter.Application/Services/MealTimeService.cs
- [[Task_63]] - code
- [[Task_66]] - code
- [[TimeOnly_2]] - code
- [[TimeOnly_5]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/CampCenterApplication_/_Services_3
SORT file.name ASC
```

## Connections to other communities
- 7 edges to [[_COMMUNITY_CampCenter.Application  Services (2)]]
- 6 edges to [[_COMMUNITY_CampCenter.UnitTests  Validators]]
- 6 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (1)]]
- 6 edges to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (2)]]
- 3 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 2 edges to [[_COMMUNITY_DTOs  Schedule (2)]]
- 2 edges to [[_COMMUNITY_CampCenter.Domain  Repositories (2)]]
- 2 edges to [[_COMMUNITY_CampCenter.Domain  Repositories (1)]]
- 2 edges to [[_COMMUNITY_Persistence  Configurations]]
- 1 edge to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (1)]]

## Top bridge nodes
- [[MealTimeDefault_1]] - degree 29, connects to 7 communities
- [[IMealTimeDefaultRepository]] - degree 11, connects to 3 communities
- [[MealKind_1]] - degree 6, connects to 3 communities
- [[MealTimeService]] - degree 11, connects to 2 communities
- [[MealTimeDefaultDto]] - degree 8, connects to 2 communities