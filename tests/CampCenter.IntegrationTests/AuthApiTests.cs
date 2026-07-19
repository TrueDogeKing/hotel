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
    public async Task Login_WithUnknownLogin_ReturnsUnauthorized()
    {
        var client = CreateClient();

        var response = await client.PostAsJsonAsync(
            "/api/auth/login",
            new LoginRequestDto("nobody", "Whatever1!")
        );

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Login_WithInvalidPayload_ReturnsBadRequest()
    {
        var client = CreateClient();

        var response = await client.PostAsJsonAsync("/api/auth/login", new LoginRequestDto("", ""));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_ExceedingRateLimit_ReturnsTooManyRequests()
    {
        // Isolated host with a strict limit so this does not affect the shared suite.
        var strictClient = Factory
            .WithWebHostBuilder(builder => builder.UseSetting("RateLimiting:Auth:PermitLimit", "3"))
            .CreateClient();

        var credentials = new LoginRequestDto("nobody", "Whatever1!");

        HttpResponseMessage response = null!;
        for (var attempt = 0; attempt < 5; attempt++)
        {
            response = await strictClient.PostAsJsonAsync("/api/auth/login", credentials);
        }

        // First 3 attempts pass the limiter (returning 401); the rest are rejected with 429.
        Assert.Equal(HttpStatusCode.TooManyRequests, response.StatusCode);
        // The client is told when it may retry.
        Assert.True(response.Headers.Contains("Retry-After"));
    }
}
