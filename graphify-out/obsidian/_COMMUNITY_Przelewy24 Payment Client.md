---
type: community
cohesion: 0.29
members: 7
---

# Przelewy24 Payment Client

**Cohesion:** 0.29 - loosely connected
**Members:** 7 nodes

## Members
- [[CampCenter.Infrastructure.Payments]] - code - src/CampCenter.Infrastructure/Payments/P24Settings.cs
- [[P24SignCalculator.cs]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[P24SignCalculatorTests.cs]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[Przelewy24Client.cs]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs
- [[RegisterData]] - code
- [[RegisterData_1]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs
- [[RegisterResponse]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Przelewy24_Payment_Client
SORT file.name ASC
```

## Connections to other communities
- 4 edges to [[_COMMUNITY_Payment Gateway Integration Tests (1)]]
- 3 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Services (5)]]

## Top bridge nodes
- [[P24SignCalculatorTests.cs]] - degree 4, connects to 3 communities
- [[Przelewy24Client.cs]] - degree 5, connects to 2 communities
- [[P24SignCalculator.cs]] - degree 3, connects to 2 communities
- [[CampCenter.Infrastructure.Payments]] - degree 4, connects to 1 community