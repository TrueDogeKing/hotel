---
type: community
cohesion: 0.17
members: 16
---

# Admin Booking & Notifications (3)

**Cohesion:** 0.17 - loosely connected
**Members:** 16 nodes

## Members
- [[.HandleNotificationAsync()_1]] - code - src/CampCenter.Application/Services/PaymentService.cs
- [[.InitiateAsync()_1]] - code - src/CampCenter.Application/Services/PaymentService.cs
- [[.SendSafelyAsync()_1]] - code - src/CampCenter.Application/Services/PaymentService.cs
- [[BookingSettings_3]] - code
- [[CancellationToken_27]] - code
- [[EmailMessage_2]] - code
- [[GatewayNotification_1]] - code
- [[IEmailSender_3]] - code
- [[ILogger_4]] - code
- [[IPaymentGateway_1]] - code
- [[IPaymentService_2]] - code
- [[ITokenService_3]] - code
- [[InitiatePaymentRequestDto_2]] - code
- [[InitiatePaymentResponseDto_1]] - code
- [[PaymentService]] - code - src/CampCenter.Application/Services/PaymentService.cs
- [[Task_26]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Admin_Booking__Notifications_3
SORT file.name ASC
```

## Connections to other communities
- 5 edges to [[_COMMUNITY_Admin Booking & Notifications (2)]]
- 2 edges to [[_COMMUNITY_DTOs  Schedule (1)]]
- 2 edges to [[_COMMUNITY_Admin Booking & Notifications (1)]]
- 1 edge to [[_COMMUNITY_Domain & Infra Namespaces]]

## Top bridge nodes
- [[PaymentService]] - degree 12, connects to 3 communities
- [[.HandleNotificationAsync()_1]] - degree 8, connects to 3 communities
- [[.InitiateAsync()_1]] - degree 9, connects to 2 communities