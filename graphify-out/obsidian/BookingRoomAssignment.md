---
source_file: "src/CampCenter.Domain/Entities/BookingRoomAssignment.cs"
type: "code"
community: "Booking Persistence & Entities"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Booking_Persistence__Entities
---

# BookingRoomAssignment

## Context

_Source: `src/CampCenter.Domain/Entities/BookingRoomAssignment.cs` (defined near L8; showing L6–L29 of 29)._

```csharp
/// double-booking guard: no two assignments of the same room may overlap in time.
/// Rows are deleted when a booking is cancelled — that is what frees the rooms.
public class BookingRoomAssignment
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }

    public Booking? Booking { get; set; }

    public Guid RoomId { get; set; }

    public Room? Room { get; set; }

    /// Denormalized from the booking so the overlap exclusion constraint and the
    /// occupancy queries need no join. Half-open: EndDate is the checkout day.
    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    /// Suggested occupancy for this room (capacity for all but the last room of a
    /// mix, the remainder in the last one). Admins may adjust.
    public int PeopleCount { get; set; }
}
```

## Connections
- [[.AddAssignmentAsync()]] - `references` [EXTRACTED]
- [[.AddAssignmentAsync()_1]] - `references` [EXTRACTED]
- [[.Configure()_2]] - `references` [EXTRACTED]
- [[.RemoveAssignment()]] - `references` [EXTRACTED]
- [[.RemoveAssignment()_1]] - `references` [EXTRACTED]
- [[AppDbContext]] - `references` [EXTRACTED]
- [[Booking]] - `references` [EXTRACTED]
- [[BookingRoomAssignment.cs]] - `contains` [EXTRACTED]
- [[BookingRoomAssignmentConfiguration]] - `references` [EXTRACTED]
- [[DateOnly_4]] - `references` [EXTRACTED]
- [[Guid_19]] - `references` [EXTRACTED]
- [[Room_1]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Booking_Persistence__Entities