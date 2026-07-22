using CampCenter.Application.DTOs.Closures;
using CampCenter.Application.Validators;

namespace CampCenter.UnitTests.Validators;

public class ClosureValidatorsTests
{
    private readonly CreateClosureRequestValidator _validator = new();

    private static CreateClosureRequestDto Valid() =>
        new("Przerwa zimowa", new DateOnly(2026, 12, 20), new DateOnly(2027, 1, 5), RoomId: null);

    [Fact]
    public void ValidClosure_Passes() => Assert.True(_validator.Validate(Valid()).IsValid);

    [Fact]
    public void SingleDayClosure_Passes()
    {
        var dto = Valid() with { EndDate = Valid().StartDate };
        Assert.True(_validator.Validate(dto).IsValid);
    }

    [Fact]
    public void EndBeforeStart_Fails()
    {
        var dto = Valid() with { EndDate = new DateOnly(2026, 12, 19) };
        Assert.False(_validator.Validate(dto).IsValid);
    }

    [Fact]
    public void EmptyReason_Fails()
    {
        var dto = Valid() with { Reason = "" };
        Assert.False(_validator.Validate(dto).IsValid);
    }
}
