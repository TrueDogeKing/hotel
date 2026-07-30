---
type: community
cohesion: 0.14
members: 27
---

# Payment Gateway Integration Tests (2)

**Cohesion:** 0.14 - loosely connected
**Members:** 27 nodes

## Members
- [[.CreateBookingWithDepositAsync()]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.DepositWebhook_ConfirmsBooking_AndIsIdempotent()]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.DepositWebhook_GeneratesTheGroupsMeals()]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.Notification()]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.RegisterTransactionAsync()_2]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.VerifyNotificationSignature()_2]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.VerifyTransactionAsync()_2]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.Webhook_AmountMismatch_IsRejected()]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.Webhook_BadSignature_IsRejected()]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[Amount]] - code
- [[CancellationToken_45]] - code
- [[Fact_2]] - code
- [[FakePaymentGateway]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[GatewayNotification_2]] - code
- [[GatewayRegisterRequest_1]] - code
- [[GatewayRegisterResult_1]] - code
- [[HttpClient_2]] - code
- [[IPaymentGateway_2]] - code
- [[List_21]] - code
- [[OrderId]] - code
- [[PaymentsApiTests]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[Registered]] - code
- [[SessionId]] - code
- [[Task_48]] - code
- [[Token_1]] - code
- [[int_2]] - code
- [[long]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Payment_Gateway_Integration_Tests_2
SORT file.name ASC
```

## Connections to other communities
- 2 edges to [[_COMMUNITY_tests  CampCenter.IntegrationTests (2)]]
- 1 edge to [[_COMMUNITY_Integration Test Harness (1)]]

## Top bridge nodes
- [[PaymentsApiTests]] - degree 12, connects to 2 communities
- [[FakePaymentGateway]] - degree 11, connects to 1 community