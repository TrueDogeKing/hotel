---
source_file: "src/CampCenter.Application/DTOs/Rooms/RoomDtos.cs"
type: "code"
community: "Application DTO Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_DTO_Namespaces
---

# CampCenter.Application.DTOs.Rooms

## Context

_Source: `src/CampCenter.Application/DTOs/Rooms/RoomDtos.cs` (defined near L1; showing L1–L22 of 22)._

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
- [[AdminPanelApiTests.cs]] - `imports` [EXTRACTED]
- [[IRoomService.cs]] - `imports` [EXTRACTED]
- [[PaymentsApiTests.cs]] - `imports` [EXTRACTED]
- [[PublicBookingApiTests.cs]] - `imports` [EXTRACTED]
- [[RoomDtos.cs]] - `contains` [EXTRACTED]
- [[RoomService.cs]] - `imports` [EXTRACTED]
- [[RoomValidators.cs]] - `imports` [EXTRACTED]
- [[RoomsAndSessionsApiTests.cs]] - `imports` [EXTRACTED]
- [[RoomsController.cs]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_DTO_Namespaces