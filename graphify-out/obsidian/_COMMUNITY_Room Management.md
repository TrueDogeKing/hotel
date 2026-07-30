---
type: community
cohesion: 0.06
members: 75
---

# Room Management

**Cohesion:** 0.06 - loosely connected
**Members:** 75 nodes

## Members
- [[.AddAsync()_2]] - code - src/CampCenter.Domain/Repositories/IRoomRepository.cs
- [[.AddAsync()_6]] - code - src/CampCenter.Infrastructure/Repositories/RoomRepository.cs
- [[.Configure()_5]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/RoomConfiguration.cs
- [[.Create()]] - code - src/CampCenter.Api/Controllers/Admin/RoomsController.cs
- [[.CreateAsync()_1]] - code - src/CampCenter.Application/Interfaces/IRoomService.cs
- [[.CreateAsync()_4]] - code - src/CampCenter.Application/Services/RoomService.cs
- [[.Delete()]] - code - src/CampCenter.Api/Controllers/Admin/RoomsController.cs
- [[.DeleteAsync()_1]] - code - src/CampCenter.Application/Interfaces/IRoomService.cs
- [[.DeleteAsync()_4]] - code - src/CampCenter.Application/Services/RoomService.cs
- [[.GetActiveAsync()]] - code - src/CampCenter.Domain/Repositories/IRoomRepository.cs
- [[.GetActiveAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/RoomRepository.cs
- [[.GetAll()]] - code - src/CampCenter.Api/Controllers/Admin/RoomsController.cs
- [[.GetAllAsync()_1]] - code - src/CampCenter.Application/Interfaces/IRoomService.cs
- [[.GetAllAsync()_3]] - code - src/CampCenter.Application/Services/RoomService.cs
- [[.GetAllAsync()_5]] - code - src/CampCenter.Domain/Repositories/IRoomRepository.cs
- [[.GetAllAsync()_7]] - code - src/CampCenter.Infrastructure/Repositories/RoomRepository.cs
- [[.GetByIdAsync()_2]] - code - src/CampCenter.Domain/Repositories/IRoomRepository.cs
- [[.GetByIdAsync()_6]] - code - src/CampCenter.Infrastructure/Repositories/RoomRepository.cs
- [[.GetByNumberAsync()]] - code - src/CampCenter.Domain/Repositories/IRoomRepository.cs
- [[.GetByNumberAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/RoomRepository.cs
- [[.HasAssignmentsAsync()]] - code - src/CampCenter.Domain/Repositories/IRoomRepository.cs
- [[.HasAssignmentsAsync()_1]] - code - src/CampCenter.Infrastructure/Repositories/RoomRepository.cs
- [[.Remove()_1]] - code - src/CampCenter.Domain/Repositories/IRoomRepository.cs
- [[.Remove()_4]] - code - src/CampCenter.Infrastructure/Repositories/RoomRepository.cs
- [[.SaveChangesAsync()_3]] - code - src/CampCenter.Domain/Repositories/IRoomRepository.cs
- [[.SaveChangesAsync()_8]] - code - src/CampCenter.Infrastructure/Repositories/RoomRepository.cs
- [[.SaveWithConcurrencyCheckAsync()_1]] - code - src/CampCenter.Application/Services/RoomService.cs
- [[.ToDto()_1]] - code - src/CampCenter.Application/Services/RoomService.cs
- [[.Update()]] - code - src/CampCenter.Api/Controllers/Admin/RoomsController.cs
- [[.UpdateAsync()_1]] - code - src/CampCenter.Application/Interfaces/IRoomService.cs
- [[.UpdateAsync()_3]] - code - src/CampCenter.Application/Services/RoomService.cs
- [[CancellationToken_3]] - code
- [[CancellationToken_20]] - code
- [[CancellationToken_28]] - code
- [[CancellationToken_34]] - code
- [[CancellationToken_43]] - code
- [[CreateRoomRequestDto]] - code - src/CampCenter.Application/DTOs/Rooms/RoomDtos.cs
- [[CreateRoomRequestValidator]] - code - src/CampCenter.Application/Validators/RoomValidators.cs
- [[EntityTypeBuilder_6]] - code
- [[Guid_1]] - code
- [[Guid_8]] - code
- [[Guid_15]] - code
- [[Guid_23]] - code
- [[Guid_29]] - code
- [[Guid_35]] - code
- [[HttpDelete]] - code
- [[HttpGet_2]] - code
- [[HttpPost_1]] - code
- [[HttpPut_1]] - code
- [[IActionResult_2]] - code
- [[IRoomRepository]] - code - src/CampCenter.Domain/Repositories/IRoomRepository.cs
- [[IRoomService]] - code - src/CampCenter.Application/Interfaces/IRoomService.cs
- [[IValidator]] - code
- [[List_3]] - code
- [[List_10]] - code
- [[List_15]] - code
- [[List_19]] - code
- [[ProducesResponseType_2]] - code
- [[Room]] - code - src/CampCenter.Domain/Entities/Room.cs
- [[Room.cs]] - code - src/CampCenter.Domain/Entities/Room.cs
- [[RoomConfiguration]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/RoomConfiguration.cs
- [[RoomConfiguration.cs]] - code - src/CampCenter.Infrastructure/Persistence/Configurations/RoomConfiguration.cs
- [[RoomDto]] - code - src/CampCenter.Application/DTOs/Rooms/RoomDtos.cs
- [[RoomDtos.cs]] - code - src/CampCenter.Application/DTOs/Rooms/RoomDtos.cs
- [[RoomRepository]] - code - src/CampCenter.Infrastructure/Repositories/RoomRepository.cs
- [[RoomService]] - code - src/CampCenter.Application/Services/RoomService.cs
- [[RoomValidators.cs]] - code - src/CampCenter.Application/Validators/RoomValidators.cs
- [[RoomsController]] - code - src/CampCenter.Api/Controllers/Admin/RoomsController.cs
- [[Task_3]] - code
- [[Task_19]] - code
- [[Task_27]] - code
- [[Task_33]] - code
- [[Task_42]] - code
- [[UpdateRoomRequestDto]] - code - src/CampCenter.Application/DTOs/Rooms/RoomDtos.cs
- [[UpdateRoomRequestValidator]] - code - src/CampCenter.Application/Validators/RoomValidators.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Room_Management
SORT file.name ASC
```

## Connections to other communities
- 5 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 4 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 3 edges to [[_COMMUNITY_Room Task Management (1)]]
- 2 edges to [[_COMMUNITY_Room Mix Calculator Tests]]
- 2 edges to [[_COMMUNITY_DTOs  Schedule (1)]]
- 2 edges to [[_COMMUNITY_Persistence  Configurations]]
- 1 edge to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 1 edge to [[_COMMUNITY_Validator Unit Tests]]
- 1 edge to [[_COMMUNITY_Booking Persistence & Entities (4)]]
- 1 edge to [[_COMMUNITY_Room Closure Management]]
- 1 edge to [[_COMMUNITY_CampCenter.Infrastructure  Repositories (1)]]

## Top bridge nodes
- [[Room]] - degree 21, connects to 3 communities
- [[IRoomRepository]] - degree 13, connects to 3 communities
- [[RoomRepository]] - degree 11, connects to 2 communities
- [[RoomsController]] - degree 8, connects to 2 communities
- [[RoomValidators.cs]] - degree 4, connects to 2 communities