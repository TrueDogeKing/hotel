---
source_file: "src/CampCenter.Application/Services/RoomTaskService.cs"
type: "code"
community: "Room Task Management"
location: "L9"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Task_Management
---

# RoomTaskService

## Context

_Source: `src/CampCenter.Application/Services/RoomTaskService.cs` (defined near L9; showing L7–L54 of 93)._

```csharp
namespace CampCenter.Application.Services;

public class RoomTaskService : IRoomTaskService
{
    private readonly IRoomTaskRepository _tasks;
    private readonly IRoomRepository _rooms;

    public RoomTaskService(IRoomTaskRepository tasks, IRoomRepository rooms)
    {
        _tasks = tasks;
        _rooms = rooms;
    }

    public async Task<List<RoomTaskDto>> ListAsync(
        RoomTaskStatus? status,
        Guid? bookingId,
        CancellationToken cancellationToken = default
    ) => (await _tasks.ListAsync(status, bookingId, cancellationToken)).Select(ToDto).ToList();

    public async Task<RoomTaskDto> CreateAsync(
        CreateRoomTaskRequestDto request,
        Guid createdByAdminUserId,
        CancellationToken cancellationToken = default
    )
    {
        var text = request.Text.Trim();
        if (text.Length is 0 or > 1000)
        {
            throw new BusinessRuleViolationException("Task text must be 1–1000 characters.");
        }

        var room =
            await _rooms.GetByIdAsync(request.RoomId, cancellationToken)
            ?? throw new NotFoundException("Room not found.");

        var task = new RoomTask
        {
            Id = Guid.NewGuid(),
            RoomId = room.Id,
            Room = room,
            BookingId = request.BookingId,
            Text = text,
            CreatedByAdminUserId = createdByAdminUserId,
            CreatedAt = DateTime.UtcNow,
        };

        await _tasks.AddAsync(task, cancellationToken);
        await _tasks.SaveChangesAsync(cancellationToken);
```

## Connections
- [[.CreateAsync()_7]] - `method` [EXTRACTED]
- [[.DeleteAsync()_5]] - `method` [EXTRACTED]
- [[.ListAsync()_3]] - `method` [EXTRACTED]
- [[.SetStatusAsync()_1]] - `method` [EXTRACTED]
- [[.ToDto()_3]] - `method` [EXTRACTED]
- [[IRoomRepository]] - `references` [EXTRACTED]
- [[IRoomTaskRepository]] - `references` [EXTRACTED]
- [[IRoomTaskService]] - `implements` [EXTRACTED]
- [[RoomTaskService.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Task_Management