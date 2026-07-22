---
source_file: "src/CampCenter.Domain/Entities/Room.cs"
type: "code"
community: "Room Management"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Management
---

# Room.cs

## Context

_Source: `src/CampCenter.Domain/Entities/Room.cs` (defined near L1; showing L1–L23 of 23)._

```csharp
namespace CampCenter.Domain.Entities;

/// A physical room in the center. Rooms carry no dates: the occupancy period is
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
- [[CampCenter.Domain.Entities]] - `contains` [EXTRACTED]
- [[Room_1]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Management