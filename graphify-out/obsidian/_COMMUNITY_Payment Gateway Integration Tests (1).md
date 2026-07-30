---
type: community
cohesion: 0.07
members: 47
---

# Payment Gateway Integration Tests (1)

**Cohesion:** 0.07 - loosely connected
**Members:** 47 nodes

## Members
- [[.HandleNotificationAsync()]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[.InitiateAsync()]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[.NotificationSign()]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[.NotificationSign_RoundTrips()]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[.P24Status()]] - code - src/CampCenter.Api/Controllers/Public/PublicPaymentsController.cs
- [[.RegisterSign()]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[.RegisterSign_MatchesDocumentedJsonShape()]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[.RegisterTransactionAsync()]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[.RegisterTransactionAsync()_1]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs
- [[.Sha384()]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[.Sha384()_1]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[.VerifyNotificationSignature()]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[.VerifyNotificationSignature()_1]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs
- [[.VerifySign()]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[.VerifySign_MatchesDocumentedJsonShape()]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[.VerifyTransactionAsync()]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[.VerifyTransactionAsync()_1]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs
- [[CancellationToken_8]] - code
- [[CancellationToken_18]] - code
- [[CancellationToken_19]] - code
- [[CancellationToken_37]] - code
- [[Fact_5]] - code
- [[GatewayNotification]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[GatewayRegisterRequest]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[GatewayRegisterResult]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[HttpClient]] - code
- [[HttpPost_6]] - code
- [[IActionResult_7]] - code
- [[IPaymentGateway]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[IPaymentGateway.cs]] - code - src/CampCenter.Application/Interfaces/IPaymentGateway.cs
- [[IPaymentService]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[IPaymentService.cs]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[InitiatePaymentRequestDto]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[InitiatePaymentResponseDto]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[P24Settings]] - code - src/CampCenter.Infrastructure/Payments/P24Settings.cs
- [[P24Settings.cs]] - code - src/CampCenter.Infrastructure/Payments/P24Settings.cs
- [[P24SignCalculator]] - code - src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs
- [[P24SignCalculatorTests]] - code - tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs
- [[ProducesResponseType_7]] - code
- [[Przelewy24Client]] - code - src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs
- [[PublicPaymentsController]] - code - src/CampCenter.Api/Controllers/Public/PublicPaymentsController.cs
- [[Task_8]] - code
- [[Task_17]] - code
- [[Task_18]] - code
- [[Task_36]] - code
- [[string_6]] - code
- [[string_7]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Payment_Gateway_Integration_Tests_1
SORT file.name ASC
```

## Connections to other communities
- 4 edges to [[_COMMUNITY_Przelewy24 Payment Client]]
- 3 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 1 edge to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 1 edge to [[_COMMUNITY_Room Mix Calculator Tests]]

## Top bridge nodes
- [[IPaymentService.cs]] - degree 5, connects to 2 communities
- [[PublicPaymentsController]] - degree 4, connects to 2 communities
- [[Przelewy24Client]] - degree 8, connects to 1 community
- [[IPaymentGateway.cs]] - degree 5, connects to 1 community
- [[P24SignCalculator]] - degree 5, connects to 1 community