---
source_file: "src/CampCenter.Application/Validators/CreateBookingRequestValidator.cs"
type: "code"
community: "Validator Unit Tests"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Validator_Unit_Tests
---

# CreateBookingRequestValidator

## Context

_Source: `src/CampCenter.Application/Validators/CreateBookingRequestValidator.cs` (defined near L6; showing L4–L33 of 33)._

```csharp
namespace CampCenter.Application.Validators;

public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequestDto>
{
    public CreateBookingRequestValidator()
    {
        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("The departure date must be after the arrival date.");
        RuleFor(x => x.Headcount).InclusiveBetween(1, 2000);
        RuleFor(x => x.RoomCounts)
            .NotEmpty()
            .Must(counts =>
                counts.All(kv => kv.Key is > 0 and <= 20 && kv.Value is >= 0 and <= 500)
            )
            .WithMessage("Invalid room counts.");
        RuleFor(x => x.OrganizationName).NotEmpty().MaximumLength(256);
        RuleFor(x => x.ContactName).NotEmpty().MaximumLength(128);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(32)
            .Matches(@"^[0-9+\-() ]+$")
            .WithMessage("Invalid phone number.");
        RuleFor(x => x.Notes).MaximumLength(2000);
        RuleFor(x => x.Language)
            .Must(l => l is "pl" or "en")
            .WithMessage("Language must be 'pl' or 'en'.");
    }
}
```

## Connections
- [[AbstractValidator]] - `inherits` [EXTRACTED]
- [[CreateBookingRequestDto]] - `references` [EXTRACTED]
- [[CreateBookingRequestValidator.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Validator_Unit_Tests