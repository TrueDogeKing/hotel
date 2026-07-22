---
source_file: "src/CampCenter.Application/Validators/RoomValidators.cs"
type: "code"
community: "Room Management"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Management
---

# CreateRoomRequestValidator

## Context

_Source: `src/CampCenter.Application/Validators/RoomValidators.cs` (defined near L6; showing L4–L24 of 24)._

```csharp
namespace CampCenter.Application.Validators;

public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequestDto>
{
    public CreateRoomRequestValidator()
    {
        RuleFor(x => x.Number).NotEmpty().MaximumLength(32);
        RuleFor(x => x.Capacity).InclusiveBetween(1, 20);
        RuleFor(x => x.Description).MaximumLength(512);
    }
}

public class UpdateRoomRequestValidator : AbstractValidator<UpdateRoomRequestDto>
{
    public UpdateRoomRequestValidator()
    {
        RuleFor(x => x.Number).NotEmpty().MaximumLength(32);
        RuleFor(x => x.Capacity).InclusiveBetween(1, 20);
        RuleFor(x => x.Description).MaximumLength(512);
    }
}
```

## Connections
- [[AbstractValidator]] - `inherits` [EXTRACTED]
- [[CreateRoomRequestDto]] - `references` [EXTRACTED]
- [[RoomValidators.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Management