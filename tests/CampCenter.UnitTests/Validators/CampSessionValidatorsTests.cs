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
