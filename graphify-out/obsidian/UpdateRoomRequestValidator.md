---
source_file: "src/CampCenter.Application/Validators/RoomValidators.cs"
type: "code"
community: "Room Management"
location: "L16"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Management
---

# UpdateRoomRequestValidator

## Context

_Source: `src/CampCenter.Application/Validators/RoomValidators.cs` (defined near L16; showing L14–L24 of 24)._

```csharp
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
- [[RoomValidators.cs]] - `contains` [EXTRACTED]
- [[UpdateRoomRequestDto]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Management