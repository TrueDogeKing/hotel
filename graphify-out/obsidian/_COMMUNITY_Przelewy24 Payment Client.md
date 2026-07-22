---
type: community
cohesion: 0.22
members: 9
---

# Przelewy24 Payment Client

**Cohesion:** 0.22 - loosely connected
**Members:** 9 nodes

## Members
- [[CampCenter.Infrastructure.Payments]] - code - src/CampCenter.Infrastructure/Payments/P24Settings.cs
- [[CampCenter.UnitTests.Services]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[P24SignCalculator.cs]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[P24SignCalculatorTests.cs]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[Przelewy24Client.cs]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs
- [[RegisterData]] - code
- [[RegisterData_1]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs
- [[RegisterResponse]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs
- [[RoomMixCalculatorTests.cs]] - code - tests/CampCenter.UnitTests/Services/RoomMixCalculatorTests.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Przelewy24_Payment_Client
SORT file.name ASC
```

## Connections to other communities
- 4 edges to [[_COMMUNITY_Payment Gateway Integration Tests]]
- 3 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 1 edge to [[_COMMUNITY_Auth DTOs & Models]]
- 1 edge to [[_COMMUNITY_Domain & Infra Namespaces]]
- 1 edge to [[_COMMUNITY_Room Mix Calculator Tests]]

## Top bridge nodes
- [[CampCenter.Infrastructure.Payments]] - degree 5, connects to 2 communities
- [[Przelewy24Client.cs]] - degree 5, connects to 2 communities
- [[P24SignCalculatorTests.cs]] - degree 4, connects to 2 communities
- [[P24SignCalculator.cs]] - degree 3, connects to 2 communities
- [[RoomMixCalculatorTests.cs]] - degree 3, connects to 2 communities