---
source_file: "src/CampCenter.Application/Validators/RoomValidators.cs"
type: "code"
community: "Room Management"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Management
---

# RoomValidators.cs

## Context

_Source: `src/CampCenter.Application/Validators/RoomValidators.cs` (defined near L1; showing L1–L24 of 24)._

```csharp
using CampCenter.Application.DTOs.Rooms;
using FluentValidation;

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
- [[CampCenter.Application.DTOs.Rooms]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Validators]] - `contains` [EXTRACTED]
- [[CreateRoomRequestValidator]] - `contains` [EXTRACTED]
- [[UpdateRoomRequestValidator]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Management