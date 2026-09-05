---
type: community
members: 7
---

# IntegrationTestBase

**Members:** 7 nodes

## Members
- [[.MergedState_DrivesPaymentAndStatusTogether()]] - code - tests/CampCenter.IntegrationTests/AdminPricingApiTests.cs
- [[.OnlyARecordedPayment_EmailsTheGroup()]] - code - tests/CampCenter.IntegrationTests/AdminPricingApiTests.cs
- [[.Rates_PrefillANewBooking_AndCanBeRepricedPerGroup()]] - code - tests/CampCenter.IntegrationTests/AdminPricingApiTests.cs
- [[.RecordingTheDeposit_ConfirmsABookingWaitingOnIt()]] - code - tests/CampCenter.IntegrationTests/AdminPricingApiTests.cs
- [[AdminPricingApiTests]] - code - tests/CampCenter.IntegrationTests/AdminPricingApiTests.cs
- [[Fact_1]] - code
- [[Task_71]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/IntegrationTestBase
SORT file.name ASC
```

## Connections to other communities
- 4 edges to [[_COMMUNITY_tests  CampCenter.IntegrationTests (1)]]
- 1 edge to [[_COMMUNITY_CampCenter.Application.DTOs.Public]]
- 1 edge to [[_COMMUNITY_Payment Gateway Integration Tests (2)]]

## Top bridge nodes
- [[AdminPricingApiTests]] - degree 6, connects to 2 communities
- [[.Rates_PrefillANewBooking_AndCanBeRepricedPerGroup()]] - degree 4, connects to 1 community
- [[.RecordingTheDeposit_ConfirmsABookingWaitingOnIt()]] - degree 4, connects to 1 community
- [[.OnlyARecordedPayment_EmailsTheGroup()]] - degree 4, connects to 1 community
- [[.MergedState_DrivesPaymentAndStatusTogether()]] - degree 4, connects to 1 community