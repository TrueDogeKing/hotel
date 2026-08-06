---
type: community
members: 63
---

# Payment Gateway Integration Tests (1)

**Members:** 63 nodes

## Members
- [[.CreateBookingWithDepositAsync()]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.DepositWebhook_ConfirmsBooking_AndIsIdempotent()]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.DepositWebhook_GeneratesTheGroupsMeals()]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.HandleNotificationAsync()]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[.InitiateAsync()]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[.Notification()]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[.NotificationSign()]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[.NotificationSign_RoundTrips()]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
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
- [[CancellationToken_25]] - code
- [[CancellationToken_26]] - code
- [[CancellationToken_57]] - code
- [[CancellationToken_70]] - code
- [[Fact_4]] - code
- [[Fact_12]] - code
- [[FakePaymentGateway]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[GatewayNotification]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[GatewayRegisterRequest]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[GatewayRegisterResult]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[HttpClient]] - code
- [[HttpClient_3]] - code
- [[IPaymentGateway]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[IPaymentGateway.cs]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[IPaymentService]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[IPaymentService.cs]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[InitiatePaymentRequestDto]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[InitiatePaymentResponseDto]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[List_38]] - code
- [[OrderId]] - code
- [[P24Settings]] - code - src/CampCenter.Infrastructure/Payments/P24Settings.cs
- [[P24Settings.cs]] - code - src/CampCenter.Infrastructure/Payments/P24Settings.cs
- [[P24SignCalculator]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[P24SignCalculatorTests]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[PaymentsApiTests]] - code - tests/CampCenter.IntegrationTests/PaymentsApiTests.cs
- [[Przelewy24Client]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs
- [[Registered]] - code
- [[SessionId]] - code
- [[Task_25]] - code
- [[Task_26]] - code
- [[Task_57]] - code
- [[Task_76]] - code
- [[Token]] - code
- [[int_4]] - code
- [[long_2]] - code
- [[string_11]] - code
- [[string_12]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Payment_Gateway_Integration_Tests_1
SORT file.name ASC
```

## Connections to other communities
- 8 edges to [[_COMMUNITY_Admin Booking & Notifications (4)]]
- 4 edges to [[_COMMUNITY_SmtpEmailSender]]
- 3 edges to [[_COMMUNITY_CampCenter.Application.DTOs.Public]]
- 2 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 1 edge to [[_COMMUNITY_AdminPricingApiTests]]

## Top bridge nodes
- [[PaymentsApiTests]] - degree 12, connects to 2 communities
- [[IPaymentService.cs]] - degree 5, connects to 2 communities
- [[FakePaymentGateway]] - degree 11, connects to 1 community
- [[GatewayNotification]] - degree 8, connects to 1 community
- [[Przelewy24Client]] - degree 8, connects to 1 community