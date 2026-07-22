---
source_file: "src/CampCenter.Application/Validators/LoginRequestValidator.cs"
type: "code"
community: "Validator Unit Tests"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Validator_Unit_Tests
---

# LoginRequestValidator

## Context

_Source: `src/CampCenter.Application/Validators/LoginRequestValidator.cs` (defined near L6; showing L4–L14 of 14)._

```csharp
namespace CampCenter.Application.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty().WithMessage("Login is required.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}
```

## Connections
- [[AbstractValidator]] - `inherits` [EXTRACTED]
- [[LoginRequestDto]] - `references` [EXTRACTED]
- [[LoginRequestValidator.cs]] - `contains` [EXTRACTED]
- [[LoginRequestValidatorTests]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Validator_Unit_Tests