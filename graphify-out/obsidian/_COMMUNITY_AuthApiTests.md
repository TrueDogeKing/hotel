---
type: community
members: 8
---

# AuthApiTests

**Members:** 8 nodes

## Members
- [[.Login_ExceedingRateLimit_ReturnsTooManyRequests()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithInvalidPayload_ReturnsBadRequest()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithUnknownLogin_ReturnsUnauthorized()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithValidAdminCredentials_ReturnsTokenAndSetsRefreshCookie()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithWrongPassword_ReturnsUnauthorized()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[AuthApiTests]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[Fact_3]] - code
- [[Task_74]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/AuthApiTests
SORT file.name ASC
```

## Connections to other communities
- 4 edges to [[_COMMUNITY_src  api (2)]]
- 1 edge to [[_COMMUNITY_Payment Gateway Integration Tests (2)]]
- 1 edge to [[_COMMUNITY_ScheduleValidators.cs]]

## Top bridge nodes
- [[AuthApiTests]] - degree 7, connects to 2 communities
- [[.Login_WithValidAdminCredentials_ReturnsTokenAndSetsRefreshCookie()]] - degree 4, connects to 1 community
- [[.Login_WithWrongPassword_ReturnsUnauthorized()]] - degree 4, connects to 1 community
- [[.Login_WithUnknownLogin_ReturnsUnauthorized()]] - degree 4, connects to 1 community
- [[.Login_WithInvalidPayload_ReturnsBadRequest()]] - degree 4, connects to 1 community