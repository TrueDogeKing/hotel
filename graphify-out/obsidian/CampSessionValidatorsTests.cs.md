---
source_file: "tests/CampCenter.UnitTests/Validators/CampSessionValidatorsTests.cs"
type: "code"
community: "Validator Unit Tests"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Validator_Unit_Tests
---

# CampSessionValidatorsTests.cs

## Context

_Source: `tests/CampCenter.UnitTests/Validators/CampSessionValidatorsTests.cs` — full file embedded (44 lines)._ ⚠️ **This file is deleted in the current working tree** (uncommitted change); context below is the committed version from git HEAD.

```csharp
using CampCenter.Application.DTOs.Sessions;
using CampCenter.Application.Validators;

namespace CampCenter.UnitTests.Validators;

public class CampSessionValidatorsTests
{
    private readonly CreateCampSessionRequestValidator _validator = new();

    private static CreateCampSessionRequestDto Valid() =>
        new(
            "Turnus I",
            new DateOnly(2026, 7, 1),
            new DateOnly(2026, 7, 14),
            PricePerPersonGrosze: 120_000,
            DepositPerPersonGrosze: 30_000
        );

    [Fact]
    public void ValidSession_Passes() => Assert.True(_validator.Validate(Valid()).IsValid);

    [Fact]
    public void EndDateNotAfterStart_Fails()
    {
        var dto = Valid() with { EndDate = new DateOnly(2026, 7, 1) };
        Assert.False(_validator.Validate(dto).IsValid);
    }

    [Fact]
    public void DepositAbovePrice_Fails()
    {
        var dto = Valid() with { DepositPerPersonGrosze = 200_000 };
        Assert.False(_validator.Validate(dto).IsValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void NonPositivePrice_Fails(long price)
    {
        var dto = Valid() with { PricePerPersonGrosze = price };
        Assert.False(_validator.Validate(dto).IsValid);
    }
}
```

## Connections
- [[CampCenter.Application.DTOs.Sessions]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Validators]] - `imports` [EXTRACTED]
- [[CampCenter.UnitTests.Validators]] - `contains` [EXTRACTED]
- [[CampSessionValidatorsTests]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Validator_Unit_Tests