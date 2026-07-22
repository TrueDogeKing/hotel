---
source_file: "src/CampCenter.Domain/Entities/Room.cs"
type: "code"
community: "Room Management"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Management
---

# Room

## Context

_Source: `src/CampCenter.Domain/Entities/Room.cs` (defined near L6; showing L4–L23 of 23)._

```csharp
/// derived from assignment → booking → session dates (published sessions never
/// overlap, so (session, room) fully determines when a room is occupied).
public class Room
{
    public Guid Id { get; set; }

    /// Human-readable room number, unique (e.g. "12", "A-3").
    public required string Number { get; set; }

    /// Number of beds (2/3/4…). Extra beds are handled as RoomTasks, not capacity changes.
    public int Capacity { get; set; }

    /// Soft-deactivation: inactive rooms are excluded from availability but keep
    /// their history (rooms referenced by assignments are never hard-deleted).
    public bool IsActive { get; set; } = true;

    public string? Description { get; set; }

    public uint RowVersion { get; set; }
}
```

## Connections
- [[.AddAsync()_3]] - `references` [EXTRACTED]
- [[.AddAsync()_8]] - `references` [EXTRACTED]
- [[.AssignRooms()]] - `references` [EXTRACTED]
- [[.Configure()_6]] - `references` [EXTRACTED]
- [[.GetActiveAsync()]] - `references` [EXTRACTED]
- [[.GetActiveAsync()_1]] - `references` [EXTRACTED]
- [[.GetAllAsync()_5]] - `references` [EXTRACTED]
- [[.GetAllAsync()_7]] - `references` [EXTRACTED]
- [[.GetByIdAsync()_3]] - `references` [EXTRACTED]
- [[.GetByIdAsync()_8]] - `references` [EXTRACTED]
- [[.GetByNumberAsync()]] - `references` [EXTRACTED]
- [[.GetByNumberAsync()_1]] - `references` [EXTRACTED]
- [[.PickRoomsAsync()]] - `references` [EXTRACTED]
- [[.Remove()_1]] - `references` [EXTRACTED]
- [[.Remove()_4]] - `references` [EXTRACTED]
- [[.SaveWithConcurrencyCheckAsync()_1]] - `references` [EXTRACTED]
- [[.ToDto()_2]] - `references` [EXTRACTED]
- [[AppDbContext]] - `references` [EXTRACTED]
- [[BookingRoomAssignment]] - `references` [EXTRACTED]
- [[Closure]] - `references` [EXTRACTED]
- [[Guid_23]] - `references` [EXTRACTED]
- [[Room.cs]] - `contains` [EXTRACTED]
- [[RoomConfiguration]] - `references` [EXTRACTED]
- [[RoomTask_1]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Management