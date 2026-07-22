---
source_file: "tests/CampCenter.UnitTests/Validators/LoginRequestValidatorTests.cs"
type: "code"
community: "Validator Unit Tests"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Validator_Unit_Tests
---

# LoginRequestValidatorTests.cs

## Context

_Source: `tests/CampCenter.UnitTests/Validators/LoginRequestValidatorTests.cs` (defined near L1; showing L1–L26 of 26)._

```csharp
using CampCenter.Application.DTOs.Auth;
using CampCenter.Application.Validators;

namespace CampCenter.UnitTests.Validators;

public class LoginRequestValidatorTests
{
    private readonly LoginRequestValidator _validator = new();

    [Fact]
    public void ValidCredentials_Pass()
    {
        var result = _validator.Validate(new LoginRequestDto("admin", "Admin123!"));
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("", "Admin123!")]
    [InlineData("admin", "")]
    [InlineData("", "")]
    public void MissingFields_Fail(string login, string password)
    {
        var result = _validator.Validate(new LoginRequestDto(login, password));
        Assert.False(result.IsValid);
    }
}
```

## Connections
- [[CampCenter.Application.DTOs.Auth]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Validators]] - `imports` [EXTRACTED]
- [[CampCenter.UnitTests.Validators]] - `contains` [EXTRACTED]
- [[LoginRequestValidatorTests]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Validator_Unit_Tests