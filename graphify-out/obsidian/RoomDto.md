---
source_file: "src/CampCenter.Application/DTOs/Rooms/RoomDtos.cs"
type: "code"
community: "Room Management"
location: "L3"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Management
---

# RoomDto

## Context

_Source: `src/CampCenter.Application/DTOs/Rooms/RoomDtos.cs` (defined near L3; showing L1–L22 of 22)._

```csharp
namespace CampCenter.Application.DTOs.Rooms;

public record RoomDto(
    Guid Id,
    string Number,
    int Capacity,
    bool IsActive,
    string? Description,
    uint RowVersion
);

public record CreateRoomRequestDto(string Number, int Capacity, string? Description);

/// RowVersion carries the xmin the client last saw; a mismatch means someone
/// else edited the room in the meantime (409).
public record UpdateRoomRequestDto(
    string Number,
    int Capacity,
    bool IsActive,
    string? Description,
    uint RowVersion
);
```

## Connections
- [[.CreateAsync()_2]] - `references` [EXTRACTED]
- [[.CreateAsync()_6]] - `references` [EXTRACTED]
- [[.GetAllAsync()_1]] - `references` [EXTRACTED]
- [[.GetAllAsync()_3]] - `references` [EXTRACTED]
- [[.ToDto()_2]] - `references` [EXTRACTED]
- [[.UpdateAsync()_1]] - `references` [EXTRACTED]
- [[.UpdateAsync()_3]] - `references` [EXTRACTED]
- [[RoomDtos.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Management