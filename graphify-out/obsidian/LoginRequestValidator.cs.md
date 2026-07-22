---
source_file: "src/CampCenter.Application/Validators/LoginRequestValidator.cs"
type: "code"
community: "Validator Unit Tests"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Validator_Unit_Tests
---

# LoginRequestValidator.cs

## Context

_Source: `src/CampCenter.Application/Validators/LoginRequestValidator.cs` (defined near L1; showing L1–L14 of 14)._

```csharp
using CampCenter.Application.DTOs.Auth;
using FluentValidation;

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
- [[CampCenter.Application.DTOs.Auth]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Validators]] - `contains` [EXTRACTED]
- [[LoginRequestValidator]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Validator_Unit_Tests