---
source_file: "src/CampCenter.Domain/Entities/Closure.cs"
type: "code"
community: "Room Closure Management"
location: "L7"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Closure_Management
---

# Closure

## Context

_Source: `src/CampCenter.Domain/Entities/Closure.cs` (defined near L7; showing L5–L28 of 28)._

```csharp
/// now choose free date ranges and are only rejected when they hit a closure or
/// an already-booked room.
public class Closure
{
    public Guid Id { get; set; }

    /// Why the span is blocked (e.g. "Przerwa zimowa", "Remont pokoju 12").
    public required string Reason { get; set; }

    /// First closed day (inclusive).
    public DateOnly StartDate { get; set; }

    /// Last closed day (inclusive).
    public DateOnly EndDate { get; set; }

    /// Null = whole center is closed; set = only this room is blocked (e.g. maintenance).
    public Guid? RoomId { get; set; }

    public Room? Room { get; set; }

    public DateTime CreatedAt { get; set; }

    public uint RowVersion { get; set; }
}
```

## Connections
- [[.AddAsync()_1]] - `references` [EXTRACTED]
- [[.AddAsync()_6]] - `references` [EXTRACTED]
- [[.Configure()_3]] - `references` [EXTRACTED]
- [[.GetAllAsync()_4]] - `references` [EXTRACTED]
- [[.GetAllAsync()_6]] - `references` [EXTRACTED]
- [[.GetByIdAsync()_2]] - `references` [EXTRACTED]
- [[.GetByIdAsync()_7]] - `references` [EXTRACTED]
- [[.GetOverlappingAsync()]] - `references` [EXTRACTED]
- [[.GetOverlappingAsync()_1]] - `references` [EXTRACTED]
- [[.GetUpcomingCenterWideAsync()]] - `references` [EXTRACTED]
- [[.GetUpcomingCenterWideAsync()_1]] - `references` [EXTRACTED]
- [[.Remove()]] - `references` [EXTRACTED]
- [[.Remove()_3]] - `references` [EXTRACTED]
- [[AppDbContext]] - `references` [EXTRACTED]
- [[Closure.cs]] - `contains` [EXTRACTED]
- [[ClosureConfiguration]] - `references` [EXTRACTED]
- [[DateOnly_5]] - `references` [EXTRACTED]
- [[DateTime_5]] - `references` [EXTRACTED]
- [[Guid_20]] - `references` [EXTRACTED]
- [[Room_1]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Closure_Management