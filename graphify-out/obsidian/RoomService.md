---
source_file: "src/CampCenter.Application/Services/RoomService.cs"
type: "code"
community: "Room Management"
location: "L9"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Management
---

# RoomService

## Context

_Source: `src/CampCenter.Application/Services/RoomService.cs` (defined near L9; showing L7–L54 of 117)._

```csharp
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
    {
        var room =
            await _rooms.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Room not found.");

        var number = request.Number.Trim();
        var duplicate = await _rooms.GetByNumberAsync(number, cancellationToken);
        if (duplicate is not null && duplicate.Id != id)
```

## Connections
- [[.CreateAsync()_6]] - `method` [EXTRACTED]
- [[.DeleteAsync()_4]] - `method` [EXTRACTED]
- [[.GetAllAsync()_3]] - `method` [EXTRACTED]
- [[.SaveWithConcurrencyCheckAsync()_1]] - `method` [EXTRACTED]
- [[.ToDto()_2]] - `method` [EXTRACTED]
- [[.UpdateAsync()_3]] - `method` [EXTRACTED]
- [[IRoomRepository]] - `references` [EXTRACTED]
- [[IRoomService]] - `implements` [EXTRACTED]
- [[RoomService.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Management