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
