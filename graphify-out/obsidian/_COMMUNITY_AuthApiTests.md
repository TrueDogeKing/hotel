---
type: community
members: 10
---

# AuthApiTests

**Members:** 10 nodes

## Members
- [[.CreateClient()]] - code - tests/CampCenter.IntegrationTests/ApiCollection.cs
- [[.Login_ExceedingRateLimit_ReturnsTooManyRequests()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithInvalidPayload_ReturnsBadRequest()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithUnknownLogin_ReturnsUnauthorized()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithValidAdminCredentials_ReturnsTokenAndSetsRefreshCookie()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[.Login_WithWrongPassword_ReturnsUnauthorized()]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[AuthApiTests]] - code - tests/CampCenter.IntegrationTests/AuthApiTests.cs
- [[Fact_3]] - code
- [[HttpClient_2]] - code
- [[Task_74]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/AuthApiTests
SORT file.name ASC
```

## Connections to other communities
- 7 edges to [[_COMMUNITY_src  api (2)]]
- 4 edges to [[_COMMUNITY_tests  CampCenter.IntegrationTests (1)]]
- 3 edges to [[_COMMUNITY_RoomsAndClosuresApiTests]]
- 2 edges to [[_COMMUNITY_AdminPanelApiTests]]
- 2 edges to [[_COMMUNITY_.WithRoomsAsync]]
- 2 edges to [[_COMMUNITY_PasswordRules]]
- 1 edge to [[_COMMUNITY_ScheduleValidators.cs]]

## Top bridge nodes
- [[.CreateClient()]] - degree 22, connects to 6 communities
- [[AuthApiTests]] - degree 7, connects to 2 communities
- [[HttpClient_2]] - degree 3, connects to 2 communities