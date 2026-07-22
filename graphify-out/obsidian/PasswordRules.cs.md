---
source_file: "src/CampCenter.Application/Validators/PasswordRules.cs"
type: "code"
community: "Validator Unit Tests"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Validator_Unit_Tests
---

# PasswordRules.cs

## Context

_Source: `src/CampCenter.Application/Validators/PasswordRules.cs` (defined near L1; showing L1–L25 of 25)._

```csharp
using FluentValidation;

namespace CampCenter.Application.Validators;

/// Shared password complexity policy, applied wherever a new password is set
/// (contact creation and password change). Kept in one place so the rules stay consistent.
public static class PasswordRules
{
    /// Minimum required password length.
    public const int MinLength = 8;

    /// Applies the password complexity policy: non-empty, at least <see cref="MinLength"/>
    /// characters, at least one uppercase letter and at least one special (non-alphanumeric) character.
    public static IRuleBuilderOptions<T, string> ValidPassword<T>(
        this IRuleBuilder<T, string> rule
    ) =>
        rule.NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(MinLength)
            .WithMessage($"Password must be at least {MinLength} characters long.")
            .Matches("[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("Password must contain at least one special character.");
}
```

## Connections
- [[CampCenter.Application.Validators]] - `contains` [EXTRACTED]
- [[PasswordRules]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Validator_Unit_Tests