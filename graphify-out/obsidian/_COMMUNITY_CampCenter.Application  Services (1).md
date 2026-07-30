---
type: community
cohesion: 0.09
members: 54
---

# CampCenter.Application / Services (1)

**Cohesion:** 0.09 - loosely connected
**Members:** 54 nodes

## Members
- [[.Create()_2]] - code - src/CampCenter.Api/Controllers/Admin/ClosuresController.cs
- [[.CreateAsync()_8]] - code - src/CampCenter.Application/Interfaces/IClosureService.cs
- [[.CreateAsync()_12]] - code - src/CampCenter.Application/Services/ClosureService.cs
- [[.Delete()_1]] - code - src/CampCenter.Api/Controllers/Admin/ClosuresController.cs
- [[.DeleteAsync()_6]] - code - src/CampCenter.Application/Interfaces/IClosureService.cs
- [[.DeleteAsync()_8]] - code - src/CampCenter.Application/Services/ClosureService.cs
- [[.EmptyReason_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ClosureValidatorsTests.cs
- [[.EndBeforeStart_Fails()]] - code - tests/CampCenter.UnitTests/Validators/ClosureValidatorsTests.cs
- [[.GetAll()_1]] - code - src/CampCenter.Api/Controllers/Admin/ClosuresController.cs
- [[.GetAllAsync()_8]] - code - src/CampCenter.Application/Interfaces/IClosureService.cs
- [[.GetAllAsync()_10]] - code - src/CampCenter.Application/Services/ClosureService.cs
- [[.GetOrThrowAsync()_2]] - code - src/CampCenter.Application/Services/ClosureService.cs
- [[.GuardNoLiveBookingsAsync()]] - code - src/CampCenter.Application/Services/ClosureService.cs
- [[.GuardRoomExistsAsync()]] - code - src/CampCenter.Application/Services/ClosureService.cs
- [[.SingleDayClosure_Passes()]] - code - tests/CampCenter.UnitTests/Validators/ClosureValidatorsTests.cs
- [[.ToDto()_4]] - code - src/CampCenter.Application/Services/ClosureService.cs
- [[.Update()_1]] - code - src/CampCenter.Api/Controllers/Admin/ClosuresController.cs
- [[.UpdateAsync()_4]] - code - src/CampCenter.Application/Interfaces/IClosureService.cs
- [[.UpdateAsync()_6]] - code - src/CampCenter.Application/Services/ClosureService.cs
- [[.Valid()]] - code - tests/CampCenter.UnitTests/Validators/ClosureValidatorsTests.cs
- [[.ValidClosure_Passes()]] - code - tests/CampCenter.UnitTests/Validators/ClosureValidatorsTests.cs
- [[CancellationToken_46]] - code
- [[CancellationToken_52]] - code
- [[CancellationToken_56]] - code
- [[ClosureDto]] - code - src/CampCenter.Application/DTOs/Closures/ClosureDtos.cs
- [[ClosureDtos.cs]] - code - src/CampCenter.Application/DTOs/Closures/ClosureDtos.cs
- [[ClosureService]] - code - src/CampCenter.Application/Services/ClosureService.cs
- [[ClosureValidators.cs]] - code - src/CampCenter.Application/Validators/ClosureValidators.cs
- [[ClosureValidatorsTests]] - code - tests/CampCenter.UnitTests/Validators/ClosureValidatorsTests.cs
- [[ClosuresController]] - code - src/CampCenter.Api/Controllers/Admin/ClosuresController.cs
- [[CreateClosureRequestDto]] - code - src/CampCenter.Application/DTOs/Closures/ClosureDtos.cs
- [[CreateClosureRequestValidator]] - code - src/CampCenter.Application/Validators/ClosureValidators.cs
- [[DateOnly_19]] - code
- [[Fact_15]] - code
- [[Guid_38]] - code
- [[Guid_42]] - code
- [[Guid_46]] - code
- [[HttpDelete_3]] - code
- [[HttpGet_7]] - code
- [[HttpPost_7]] - code
- [[HttpPut_3]] - code
- [[IActionResult_9]] - code
- [[IClosureRepository_3]] - code
- [[IClosureService]] - code - src/CampCenter.Application/Interfaces/IClosureService.cs
- [[IRoomRepository_3]] - code
- [[IValidator_5]] - code
- [[List_22]] - code
- [[List_25]] - code
- [[ProducesResponseType_9]] - code
- [[Task_51]] - code
- [[Task_57]] - code
- [[Task_61]] - code
- [[UpdateClosureRequestDto]] - code - src/CampCenter.Application/DTOs/Closures/ClosureDtos.cs
- [[UpdateClosureRequestValidator]] - code - src/CampCenter.Application/Validators/ClosureValidators.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/CampCenterApplication_/_Services_1
SORT file.name ASC
```

## Connections to other communities
- 3 edges to [[_COMMUNITY_tests  CampCenter.IntegrationTests (2)]]
- 2 edges to [[_COMMUNITY_Validator Unit Tests]]
- 2 edges to [[_COMMUNITY_DTOs  Schedule (1)]]
- 2 edges to [[_COMMUNITY_Admin Booking & Notifications (2)]]
- 1 edge to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 1 edge to [[_COMMUNITY_Application Namespaces & DTOs]]
- 1 edge to [[_COMMUNITY_Domain & Infra Namespaces]]

## Top bridge nodes
- [[ClosureService]] - degree 13, connects to 2 communities
- [[ClosuresController]] - degree 8, connects to 2 communities
- [[ClosureValidators.cs]] - degree 4, connects to 2 communities
- [[.GuardNoLiveBookingsAsync()]] - degree 8, connects to 1 community
- [[IClosureService]] - degree 7, connects to 1 community