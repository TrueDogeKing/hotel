---
type: community
members: 14
---

# Camp Session Management

**Members:** 14 nodes

## Members
- [[.NotificationSign()]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[.NotificationSign_RoundTrips()]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[.RegisterSign()]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[.RegisterSign_MatchesDocumentedJsonShape()]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[.Sha384()]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[.Sha384()_1]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[.VerifyNotificationSignature()]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[.VerifyNotificationSignature()_1]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs
- [[.VerifySign()]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[.VerifySign_MatchesDocumentedJsonShape()]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[Fact_9]] - code
- [[GatewayNotification]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[P24SignCalculator]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[P24SignCalculatorTests]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Camp_Session_Management
SORT file.name ASC
```

## Connections to other communities
- 3 edges to [[_COMMUNITY_Payment Gateway Integration Tests (1)]]
- 3 edges to [[_COMMUNITY_Admin Booking & Notifications (2)]]
- 2 edges to [[_COMMUNITY_Admin Booking & Notifications (4)]]
- 2 edges to [[_COMMUNITY_Przelewy24 Payment Client]]
- 1 edge to [[_COMMUNITY_Booking Persistence & Entities (2)]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Services (3)]]
- 1 edge to [[_COMMUNITY_CampCenter.Domain  Repositories (1)]]

## Top bridge nodes
- [[GatewayNotification]] - degree 9, connects to 5 communities
- [[.VerifyNotificationSignature()]] - degree 3, connects to 2 communities
- [[P24SignCalculator]] - degree 5, connects to 1 community
- [[P24SignCalculatorTests]] - degree 5, connects to 1 community
- [[.RegisterSign()]] - degree 4, connects to 1 community