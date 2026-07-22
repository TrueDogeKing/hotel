---
source_file: "src/CampCenter.Application/Services/RoomService.cs"
type: "code"
community: "Application DTO Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_DTO_Namespaces
---

# RoomService.cs

## Context

_Source: `src/CampCenter.Application/Services/RoomService.cs` (defined near L1; showing L1–L46 of 117)._

```csharp
using CampCenter.Application.DTOs.Rooms;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;

namespace CampCenter.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _rooms;

    public RoomService(IRoomRepository rooms) => _rooms = rooms;

    public async Task<List<RoomDto>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await _rooms.GetAllAsync(cancellationToken)).Select(ToDto).ToList();

    public async Task<RoomDto> CreateAsync(
        CreateRoomRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var number = request.Number.Trim();
        if (await _rooms.GetByNumberAsync(number, cancellationToken) is not null)
        {
            throw new ConflictException("A room with this number already exists.");
        }

        var room = new Room
        {
            Id = Guid.NewGuid(),
            Number = number,
            Capacity = request.Capacity,
            Description = request.Description,
        };

        await _rooms.AddAsync(room, cancellationToken);
        await _rooms.SaveChangesAsync(cancellationToken);
        return ToDto(room);
    }

    public async Task<RoomDto> UpdateAsync(
        Guid id,
        UpdateRoomRequestDto request,
        CancellationToken cancellationToken = default
    )
```

## Connections
- [[CampCenter.Application.DTOs.Rooms]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Services]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Exceptions]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `imports` [EXTRACTED]
- [[RoomService]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_DTO_Namespaces