---
source_file: "tests/CampCenter.IntegrationTests/AuthApiTests.cs"
type: "code"
community: "Integration Test Harness"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Integration_Test_Harness
---

# AuthApiTests

## Context

_Source: `tests/CampCenter.IntegrationTests/AuthApiTests.cs` (defined near L8; showing L6–L53 of 90)._

```csharp
namespace CampCenter.IntegrationTests;

public class AuthApiTests : IntegrationTestBase
{
    public AuthApiTests(CampCenterApiFactory factory)
        : base(factory) { }

    [Fact]
    public async Task Login_WithValidAdminCredentials_ReturnsTokenAndSetsRefreshCookie()
    {
        var client = CreateClient();

        var response = await client.PostAsJsonAsync(
            "/api/auth/login",
            new LoginRequestDto(AdminLogin, AdminPassword)
        );

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        Assert.NotNull(body);
        Assert.False(string.IsNullOrWhiteSpace(body!.Token));
        Assert.Equal(3, body.Token.Split('.').Length); // header.payload.signature
        Assert.Equal(AdminLogin, body.Login);
        Assert.Contains(response.Headers, h => h.Key == "Set-Cookie");
    }

    [Fact]
    public async Task Login_WithWrongPassword_ReturnsUnauthorized()
    {
        var client = CreateClient();

        var response = await client.PostAsJsonAsync(
            "/api/auth/login",
            new LoginRequestDto(AdminLogin, "WrongPassword1!")
        );

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Login_WithUnknownLogin_ReturnsUnauthorized()
    {
        var client = CreateClient();

        var response = await client.PostAsJsonAsync(
            "/api/auth/login",
            new LoginRequestDto("nobody", "Whatever1!")
```

## Connections
- [[.Login_ExceedingRateLimit_ReturnsTooManyRequests()]] - `method` [EXTRACTED]
- [[.Login_WithInvalidPayload_ReturnsBadRequest()]] - `method` [EXTRACTED]
- [[.Login_WithUnknownLogin_ReturnsUnauthorized()]] - `method` [EXTRACTED]
- [[.Login_WithValidAdminCredentials_ReturnsTokenAndSetsRefreshCookie()]] - `method` [EXTRACTED]
- [[.Login_WithWrongPassword_ReturnsUnauthorized()]] - `method` [EXTRACTED]
- [[AuthApiTests.cs]] - `contains` [EXTRACTED]
- [[IntegrationTestBase]] - `inherits` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Integration_Test_Harness