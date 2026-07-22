---
source_file: "src/CampCenter.Application/Interfaces/IRoomService.cs"
type: "code"
community: "Room Management"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Management
---

# IRoomService

## Context

_Source: `src/CampCenter.Application/Interfaces/IRoomService.cs` (defined near L5; showing L3–L23 of 23)._

```csharp
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
- [[.CreateAsync()_2]] - `method` [EXTRACTED]
- [[.DeleteAsync()_1]] - `method` [EXTRACTED]
- [[.GetAllAsync()_1]] - `method` [EXTRACTED]
- [[.UpdateAsync()_1]] - `method` [EXTRACTED]
- [[IRoomService.cs]] - `contains` [EXTRACTED]
- [[RoomService]] - `implements` [EXTRACTED]
- [[RoomsController]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Management