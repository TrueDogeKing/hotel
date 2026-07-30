---
type: community
cohesion: 0.10
members: 32
---

# Integration Test Harness (2)

**Cohesion:** 0.10 - loosely connected
**Members:** 32 nodes

## Members
- [[.ConfigureWebHost()]] - code - tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs
- [[.CreateAuthenticatedClientAsync()]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[.CreateClient()]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[.DisposeAsync()]] - code - tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs
- [[.InitializeAsync()]] - code - tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs
- [[.Login_ExceedingRateLimit_ReturnsTooManyRequests()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithInvalidPayload_ReturnsBadRequest()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithUnknownLogin_ReturnsUnauthorized()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithValidAdminCredentials_ReturnsTokenAndSetsRefreshCookie()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithWrongPassword_ReturnsUnauthorized()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.SeedAdminUserAsync()]] - code - src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs
- [[.SeedMealTimeDefaultsAsync()]] - code - src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs
- [[ApiCollection]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[ApiCollection.cs]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[AuthApiTests]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[CampCenterApiFactory]] - code - tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs
- [[CancellationToken_38]] - code
- [[DataSeeder]] - code - src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs
- [[Fact_1]] - code
- [[HttpClient_1]] - code
- [[IAsyncLifetime]] - code
- [[ICollectionFixture]] - code
- [[IServiceProvider]] - code
- [[IWebHostBuilder]] - code
- [[IntegrationTestBase]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[PostgreSqlContainer]] - code
- [[Task_37]] - code
- [[Task_45]] - code
- [[Task_46]] - code
- [[Task_47]] - code
- [[WebApplicationFactory]] - code
- [[string_8]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Integration_Test_Harness_2
SORT file.name ASC
```

## Connections to other communities
- 4 edges to [[_COMMUNITY_tests  CampCenter.IntegrationTests (2)]]
- 2 edges to [[_COMMUNITY_Rate Limiting & Startup]]

## Top bridge nodes
- [[ApiCollection.cs]] - degree 4, connects to 2 communities
- [[CampCenterApiFactory]] - degree 10, connects to 1 community
- [[AuthApiTests]] - degree 7, connects to 1 community
- [[DataSeeder]] - degree 3, connects to 1 community