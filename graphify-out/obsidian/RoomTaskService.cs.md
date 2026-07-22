---
source_file: "src/CampCenter.Application/Services/RoomTaskService.cs"
type: "code"
community: "Application Namespaces & DTOs"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_Namespaces__DTOs
---

# RoomTaskService.cs

## Context

_Source: `src/CampCenter.Application/Services/RoomTaskService.cs` (defined near L1; showing L1–L46 of 93)._

```csharp
using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;

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
```

## Connections
- [[CampCenter.Application.DTOs.AdminPanel]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Services]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Exceptions]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `imports` [EXTRACTED]
- [[RoomTaskService]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_Namespaces__DTOs