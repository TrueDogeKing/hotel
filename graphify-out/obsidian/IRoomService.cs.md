---
source_file: "src/CampCenter.Application/Interfaces/IRoomService.cs"
type: "code"
community: "Room Management"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Management
---

# IRoomService.cs

## Context

_Source: `src/CampCenter.Application/Interfaces/IRoomService.cs` (defined near L1; showing L1–L23 of 23)._

```csharp
using CampCenter.Application.DTOs.Rooms;

namespace CampCenter.Application.Interfaces;

public interface IRoomService
{
    Task<List<RoomDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<RoomDto> CreateAsync(
        CreateRoomRequestDto request,
        CancellationToken cancellationToken = default
    );

    Task<RoomDto> UpdateAsync(
        Guid id,
        UpdateRoomRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Hard-deletes an unreferenced room; a room with assignment history is
    /// deactivated instead (returns false to signal deactivation, true for delete).
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
```

## Connections
- [[CampCenter.Application.DTOs.Rooms]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `contains` [EXTRACTED]
- [[IRoomService]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Management