---
source_file: "src/CampCenter.Application/DTOs/Rooms/RoomDtos.cs"
type: "code"
community: "Room Management"
location: "L16"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Management
---

# UpdateRoomRequestDto

## Context

_Source: `src/CampCenter.Application/DTOs/Rooms/RoomDtos.cs` (defined near L16; showing L14–L22 of 22)._

```csharp
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
- [[.Update()]] - `references` [EXTRACTED]
- [[.UpdateAsync()_1]] - `references` [EXTRACTED]
- [[.UpdateAsync()_3]] - `references` [EXTRACTED]
- [[RoomDtos.cs]] - `contains` [EXTRACTED]
- [[UpdateRoomRequestValidator]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Management