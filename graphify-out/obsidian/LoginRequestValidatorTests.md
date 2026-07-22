---
source_file: "tests/CampCenter.UnitTests/Validators/LoginRequestValidatorTests.cs"
type: "code"
community: "Validator Unit Tests"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Validator_Unit_Tests
---

# LoginRequestValidatorTests

## Context

_Source: `tests/CampCenter.UnitTests/Validators/LoginRequestValidatorTests.cs` (defined near L6; showing L4–L26 of 26)._

```csharp
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
- [[.MissingFields_Fail()]] - `method` [EXTRACTED]
- [[.ValidCredentials_Pass()]] - `method` [EXTRACTED]
- [[LoginRequestValidator]] - `references` [EXTRACTED]
- [[LoginRequestValidatorTests.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Validator_Unit_Tests