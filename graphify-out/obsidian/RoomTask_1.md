---
source_file: "src/CampCenter.Domain/Entities/RoomTask.cs"
type: "code"
community: "Room Task Management"
location: "L11"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Task_Management
---

# RoomTask

## Context

_Source: `src/CampCenter.Domain/Entities/RoomTask.cs` (defined near L11; showing L9–L34 of 34)._

```csharp
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
- [[.AddAsync()_4]] - `references` [EXTRACTED]
- [[.AddAsync()_9]] - `references` [EXTRACTED]
- [[.Configure()_7]] - `references` [EXTRACTED]
- [[.GetByIdAsync()_4]] - `references` [EXTRACTED]
- [[.GetByIdAsync()_9]] - `references` [EXTRACTED]
- [[.ListAsync()_5]] - `references` [EXTRACTED]
- [[.ListAsync()_7]] - `references` [EXTRACTED]
- [[.Remove()_2]] - `references` [EXTRACTED]
- [[.Remove()_5]] - `references` [EXTRACTED]
- [[.ToDto()_3]] - `references` [EXTRACTED]
- [[AppDbContext]] - `references` [EXTRACTED]
- [[Booking]] - `references` [EXTRACTED]
- [[DateTime_8]] - `references` [EXTRACTED]
- [[Guid_24]] - `references` [EXTRACTED]
- [[Room_1]] - `references` [EXTRACTED]
- [[RoomTask.cs]] - `contains` [EXTRACTED]
- [[RoomTaskConfiguration]] - `references` [EXTRACTED]
- [[RoomTaskStatus]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Task_Management