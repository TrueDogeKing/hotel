---
type: community
cohesion: 0.06
members: 59
---

# Payment Gateway Integration Tests

**Cohesion:** 0.06 - loosely connected
**Members:** 59 nodes

## Members
- [[.CreateBookingWithDepositAsync()]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.DepositWebhook_ConfirmsBooking_AndIsIdempotent()]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.Notification()]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.NotificationSign()]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[.NotificationSign_RoundTrips()]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[.P24Status()]] - code - src/CampCenter.Api/Controllers/Public/PublicPaymentsController.cs
- [[.RegisterSign()]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[.RegisterSign_MatchesDocumentedJsonShape()]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[.RegisterTransactionAsync()]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[.RegisterTransactionAsync()_1]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs
- [[.RegisterTransactionAsync()_2]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.Sha384()]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[.Sha384()_1]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[.VerifyNotificationSignature()]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[.VerifyNotificationSignature()_1]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs
- [[.VerifyNotificationSignature()_2]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.VerifySign()]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[.VerifySign_MatchesDocumentedJsonShape()]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[.VerifyTransactionAsync()]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[.VerifyTransactionAsync()_1]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs
- [[.VerifyTransactionAsync()_2]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.Webhook_AmountMismatch_IsRejected()]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.Webhook_BadSignature_IsRejected()]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[Amount]] - code
- [[CancellationToken_8]] - code
- [[CancellationToken_18]] - code
- [[CancellationToken_37]] - code
- [[CancellationToken_45]] - code
- [[Fact_2]] - code
- [[Fact_5]] - code
- [[FakePaymentGateway]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[GatewayNotification]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[GatewayRegisterRequest]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[GatewayRegisterResult]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[HttpClient]] - code
- [[HttpClient_2]] - code
- [[HttpPost_6]] - code
- [[IActionResult_7]] - code
- [[IPaymentGateway]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[IPaymentGateway.cs]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[List_21]] - code
- [[OrderId]] - code
- [[P24Settings]] - code - src/CampCenter.Infrastructure/Payments/P24Settings.cs
- [[P24Settings.cs]] - code - src/CampCenter.Infrastructure/Payments/P24Settings.cs
- [[P24SignCalculator]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[P24SignCalculatorTests]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[PaymentsApiTests]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[ProducesResponseType_7]] - code
- [[Przelewy24Client]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs
- [[Registered]] - code
- [[SessionId]] - code
- [[Task_8]] - code
- [[Task_17]] - code
- [[Task_36]] - code
- [[Task_48]] - code
- [[Token]] - code
- [[int_2]] - code
- [[string_6]] - code
- [[string_7]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Payment_Gateway_Integration_Tests
SORT file.name ASC
```

## Connections to other communities
- 5 edges to [[_COMMUNITY_Admin Booking & Notifications]]
- 4 edges to [[_COMMUNITY_Przelewy24 Payment Client]]
- 2 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 2 edges to [[_COMMUNITY_Public Booking Service]]
- 2 edges to [[_COMMUNITY_Application DTO Namespaces]]
- 1 edge to [[_COMMUNITY_Integration Test Harness]]

## Top bridge nodes
- [[PaymentsApiTests]] - degree 10, connects to 2 communities
- [[GatewayNotification]] - degree 9, connects to 2 communities
- [[.P24Status()]] - degree 8, connects to 2 communities
- [[FakePaymentGateway]] - degree 11, connects to 1 community
- [[Przelewy24Client]] - degree 8, connects to 1 community