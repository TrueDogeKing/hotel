---
source_file: "src/CampCenter.Api/Controllers/Admin/RoomsController.cs"
type: "code"
community: "Application Namespaces & DTOs"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_Namespaces__DTOs
---

# RoomsController.cs

## Context

_Source: `src/CampCenter.Api/Controllers/Admin/RoomsController.cs` (defined near L1; showing L1–L46 of 97)._

```csharp
using CampCenter.Application.DTOs.Rooms;
using CampCenter.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Admin;

/// Admin CRUD for the room inventory.
[ApiController]
[Authorize]
[Route("api/admin/rooms")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _rooms;
    private readonly IValidator<CreateRoomRequestDto> _createValidator;
    private readonly IValidator<UpdateRoomRequestDto> _updateValidator;

    public RoomsController(
        IRoomService rooms,
        IValidator<CreateRoomRequestDto> createValidator,
        IValidator<UpdateRoomRequestDto> updateValidator
    )
    {
        _rooms = rooms;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<RoomDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken) =>
        Ok(await _rooms.GetAllAsync(cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(RoomDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        [FromBody] CreateRoomRequestDto request,
        CancellationToken cancellationToken
    )
    {
        var validation = await _createValidator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
```

## Connections
- [[CampCenter.Api.Controllers.Admin]] - `contains` [EXTRACTED]
- [[CampCenter.Application.DTOs.Rooms]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[RoomDeleteResultDto]] - `contains` [EXTRACTED]
- [[RoomsController]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_Namespaces__DTOs