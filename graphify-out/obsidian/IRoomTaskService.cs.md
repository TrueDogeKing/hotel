---
source_file: "src/CampCenter.Application/Interfaces/IRoomTaskService.cs"
type: "code"
community: "Application Namespaces & DTOs"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_Namespaces__DTOs
---

# IRoomTaskService.cs

## Context

_Source: `src/CampCenter.Application/Interfaces/IRoomTaskService.cs` (defined near L1; showing L1–L27 of 27)._

```csharp
using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Domain.Entities;

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
- [[CampCenter.Application.DTOs.AdminPanel]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[IRoomTaskService]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_Namespaces__DTOs