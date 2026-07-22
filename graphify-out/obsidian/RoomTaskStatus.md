---
source_file: "src/CampCenter.Domain/Entities/RoomTask.cs"
type: "code"
community: "Room Task Management"
location: "L3"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Task_Management
---

# RoomTaskStatus

## Context

_Source: `src/CampCenter.Domain/Entities/RoomTask.cs` (defined near L3; showing L1–L34 of 34)._

```csharp
namespace CampCenter.Domain.Entities;

public enum RoomTaskStatus
{
    Open,
    Done,
}

/// A housekeeping note attached to a room (e.g. "add one extra bed"), optionally
/// scoped to a booking for context. Housekeeping works off the Open list.
public class RoomTask
{
    public Guid Id { get; set; }

    public Guid RoomId { get; set; }

    public Room? Room { get; set; }

    public Guid? BookingId { get; set; }

    public Booking? Booking { get; set; }

    public required string Text { get; set; }

    public RoomTaskStatus Status { get; set; } = RoomTaskStatus.Open;

    public Guid CreatedByAdminUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DoneAt { get; set; }

    public uint RowVersion { get; set; }
}
```

## Connections
- [[.List()_1]] - `references` [EXTRACTED]
- [[.ListAsync()_1]] - `references` [EXTRACTED]
- [[.ListAsync()_3]] - `references` [EXTRACTED]
- [[.ListAsync()_5]] - `references` [EXTRACTED]
- [[.ListAsync()_7]] - `references` [EXTRACTED]
- [[.SetStatusAsync()]] - `references` [EXTRACTED]
- [[.SetStatusAsync()_1]] - `references` [EXTRACTED]
- [[RoomTask_1]] - `references` [EXTRACTED]
- [[RoomTask.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Task_Management