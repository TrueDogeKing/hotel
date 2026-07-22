---
source_file: "src/CampCenter.Application/Interfaces/IRoomTaskService.cs"
type: "code"
community: "Room Task Management"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Task_Management
---

# IRoomTaskService

## Context

_Source: `src/CampCenter.Application/Interfaces/IRoomTaskService.cs` (defined near L6; showing L4–L27 of 27)._

```csharp
namespace CampCenter.Application.Interfaces;

public interface IRoomTaskService
{
    Task<List<RoomTaskDto>> ListAsync(
        RoomTaskStatus? status,
        Guid? bookingId,
        CancellationToken cancellationToken = default
    );

    Task<RoomTaskDto> CreateAsync(
        CreateRoomTaskRequestDto request,
        Guid createdByAdminUserId,
        CancellationToken cancellationToken = default
    );

    Task<RoomTaskDto> SetStatusAsync(
        Guid id,
        RoomTaskStatus status,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
```

## Connections
- [[.CreateAsync()_3]] - `method` [EXTRACTED]
- [[.DeleteAsync()_2]] - `method` [EXTRACTED]
- [[.ListAsync()_1]] - `method` [EXTRACTED]
- [[.SetStatusAsync()]] - `method` [EXTRACTED]
- [[IRoomTaskService.cs]] - `contains` [EXTRACTED]
- [[RoomTaskService]] - `implements` [EXTRACTED]
- [[TasksController]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Task_Management