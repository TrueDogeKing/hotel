---
source_file: "tests/CampCenter.IntegrationTests/ApiCollection.cs"
type: "code"
community: "Integration Test Harness"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Integration_Test_Harness
---

# ApiCollection

## Context

_Source: `tests/CampCenter.IntegrationTests/ApiCollection.cs` (defined near L8; showing L6–L46 of 46)._

```csharp

/// All integration tests share a single API host + PostgreSQL container via this collection.
[CollectionDefinition(Name)]
public class ApiCollection : ICollectionFixture<CampCenterApiFactory>
{
    public const string Name = "api";
}

/// Base class with the shared factory and HTTP helpers.
[Collection(ApiCollection.Name)]
public abstract class IntegrationTestBase
{
    // Seeded by DataSeeder (admin from appsettings "Admin" section).
    protected const string AdminLogin = "admin";
    protected const string AdminPassword = "Admin123!";

    protected CampCenterApiFactory Factory { get; }

    protected IntegrationTestBase(CampCenterApiFactory factory) => Factory = factory;

    /// An unauthenticated client.
    protected HttpClient CreateClient() => Factory.CreateClient();

    /// A client whose Authorization header carries a fresh admin access token.
    protected async Task<HttpClient> CreateAuthenticatedClientAsync()
    {
        var client = Factory.CreateClient();
        var response = await client.PostAsJsonAsync(
            "/api/auth/login",
            new LoginRequestDto(AdminLogin, AdminPassword)
        );
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            body!.Token
        );
        return client;
    }
}
```

## Connections
- [[ApiCollection.cs]] - `contains` [EXTRACTED]
- [[CampCenterApiFactory]] - `references` [EXTRACTED]
- [[ICollectionFixture]] - `implements` [EXTRACTED]
- [[string_8]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Integration_Test_Harness