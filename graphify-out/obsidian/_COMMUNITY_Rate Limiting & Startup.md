---
type: community
cohesion: 0.14
members: 15
---

# Rate Limiting & Startup

**Cohesion:** 0.14 - loosely connected
**Members:** 15 nodes

## Members
- [[AuthController.cs]] - code - src/CampCenter.Api/Controllers/AuthController.cs
- [[BcryptPasswordHasher.cs]] - code - src/CampCenter.Infrastructure/Auth/BcryptPasswordHasher.cs
- [[CampCenter.Api.Controllers]] - code - src/CampCenter.Api/Controllers/AuthController.cs
- [[CampCenter.Api.RateLimiting]] - code - src/CampCenter.Api/RateLimiting/RateLimitPolicies.cs
- [[CampCenter.Infrastructure]] - code - src/CampCenter.Infrastructure/DependencyInjection.cs
- [[CampCenter.Infrastructure.Auth]] - code - src/CampCenter.Infrastructure/Auth/BcryptPasswordHasher.cs
- [[CampCenter.Infrastructure.Persistence.Seed]] - code - src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs
- [[CampCenterApiFactory.cs]] - code - tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs
- [[DataSeeder.cs]] - code - src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs
- [[JwtTokenService.cs]] - code - src/CampCenter.Infrastructure/Auth/JwtTokenService.cs
- [[Program]] - code - src/CampCenter.Api/Program.cs
- [[Program.cs]] - code - src/CampCenter.Api/Program.cs
- [[RateLimitPolicies]] - code - src/CampCenter.Api/RateLimiting/RateLimitPolicies.cs
- [[RateLimitPolicies.cs]] - code - src/CampCenter.Api/RateLimiting/RateLimitPolicies.cs
- [[string_1]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Rate_Limiting__Startup
SORT file.name ASC
```

## Connections to other communities
- 6 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 5 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 3 edges to [[_COMMUNITY_Auth DTOs & Models]]
- 3 edges to [[_COMMUNITY_Integration Test Harness]]
- 2 edges to [[_COMMUNITY_Auth Controller]]
- 2 edges to [[_COMMUNITY_JWT Token Service]]
- 1 edge to [[_COMMUNITY_Domain Exceptions]]
- 1 edge to [[_COMMUNITY_OpenAPI Security Scheme]]
- 1 edge to [[_COMMUNITY_Application DI Registration]]
- 1 edge to [[_COMMUNITY_Password Hashing (bcrypt)]]
- 1 edge to [[_COMMUNITY_Application DTO Namespaces]]

## Top bridge nodes
- [[Program.cs]] - degree 9, connects to 4 communities
- [[JwtTokenService.cs]] - degree 5, connects to 4 communities
- [[CampCenter.Infrastructure.Auth]] - degree 7, connects to 3 communities
- [[AuthController.cs]] - degree 7, connects to 3 communities
- [[DataSeeder.cs]] - degree 4, connects to 3 communities