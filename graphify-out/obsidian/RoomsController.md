---
source_file: "src/CampCenter.Api/Controllers/Admin/RoomsController.cs"
type: "code"
community: "Room Management"
location: "L10"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Management
---

# RoomsController

## Context

_Source: `src/CampCenter.Api/Controllers/Admin/RoomsController.cs` (defined near L10; showing L8–L55 of 97)._

```csharp

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
            foreach (var error in validation.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }

        var room = await _rooms.CreateAsync(request, cancellationToken);
```

## Connections
- [[.Create()]] - `method` [EXTRACTED]
- [[.Delete()]] - `method` [EXTRACTED]
- [[.GetAll()]] - `method` [EXTRACTED]
- [[.Update()]] - `method` [EXTRACTED]
- [[ControllerBase]] - `inherits` [EXTRACTED]
- [[IRoomService]] - `references` [EXTRACTED]
- [[IValidator]] - `references` [EXTRACTED]
- [[RoomsController.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Management