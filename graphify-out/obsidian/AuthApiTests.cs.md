---
source_file: "tests/CampCenter.IntegrationTests/AuthApiTests.cs"
type: "code"
community: "Integration Test Harness"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Integration_Test_Harness
---

# AuthApiTests.cs

## Context

_Source: `tests/CampCenter.IntegrationTests/AuthApiTests.cs` (defined near L1; showing L1–L46 of 90)._

```csharp
using System.Net;
using System.Net.Http.Json;
using CampCenter.Application.DTOs.Auth;
using Microsoft.AspNetCore.Hosting;

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
```

## Connections
- [[AuthApiTests]] - `contains` [EXTRACTED]
- [[CampCenter.Application.DTOs.Auth]] - `imports` [EXTRACTED]
- [[CampCenter.IntegrationTests]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Integration_Test_Harness