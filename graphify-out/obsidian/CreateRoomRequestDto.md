---
source_file: "src/CampCenter.Application/DTOs/Rooms/RoomDtos.cs"
type: "code"
community: "Room Management"
location: "L12"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Management
---

# CreateRoomRequestDto

## Context

_Source: `src/CampCenter.Application/DTOs/Rooms/RoomDtos.cs` (defined near L12; showing L10–L22 of 22)._

```csharp
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
- [[.Create()]] - `references` [EXTRACTED]
- [[.CreateAsync()_2]] - `references` [EXTRACTED]
- [[.CreateAsync()_6]] - `references` [EXTRACTED]
- [[CreateRoomRequestValidator]] - `references` [EXTRACTED]
- [[RoomDtos.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Management