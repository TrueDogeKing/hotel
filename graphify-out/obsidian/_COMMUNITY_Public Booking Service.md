---
type: community
cohesion: 0.09
members: 51
---

# Public Booking Service

**Cohesion:** 0.09 - loosely connected
**Members:** 51 nodes

## Members
- [[.AssignRooms()]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.Cancel()_1]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[.CancelByTokenAsync()]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[.CancelByTokenAsync()_1]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.Create()_3]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[.CreateAsync()]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[.CreateAsync()_4]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.FinalDueDate()]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.FindByTokenAsync()]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.GetByToken()]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[.GetByTokenAsync()]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[.GetByTokenAsync()_1]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.HandleNotificationAsync()]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[.InitiateAsync()]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[.InitiatePayment()]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[.ManageUrl()]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.PickRoomsAsync()]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.SendSafelyAsync()]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[.TryCreateAsync()]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[BookingDetailsDto]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[BookingPaymentDto]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[BookingService]] - code - src/CampCenter.Application/Services/BookingService.cs
- [[CancellationToken_7]] - code
- [[CancellationToken_15]] - code
- [[CancellationToken_19]] - code
- [[CancellationToken_25]] - code
- [[CreateBookingRequestDto]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[CreateBookingResponseDto]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[DateOnly]] - code
- [[EnableRateLimiting_1]] - code
- [[Guid_13]] - code
- [[HttpGet_5]] - code
- [[HttpPost_5]] - code
- [[IActionResult_6]] - code
- [[IBookingService]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[IBookingService.cs]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[ICampSessionRepository_2]] - code
- [[ILogger_3]] - code
- [[IPaymentService]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[IPaymentService.cs]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[IValidator_3]] - code
- [[InitiatePaymentRequestDto]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[InitiatePaymentResponseDto]] - code - src/CampCenter.Application/Interfaces/IPaymentService.cs
- [[List_7]] - code
- [[ProducesResponseType_6]] - code
- [[PublicBookingsController]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[PublicDtos.cs]] - code - src/CampCenter.Application/DTOs/Public/PublicDtos.cs
- [[Task_7]] - code
- [[Task_14]] - code
- [[Task_18]] - code
- [[Task_24]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Public_Booking_Service
SORT file.name ASC
```

## Connections to other communities
- 15 edges to [[_COMMUNITY_Admin Booking & Notifications]]
- 5 edges to [[_COMMUNITY_Room Mix Calculator Tests]]
- 4 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 4 edges to [[_COMMUNITY_Room Management]]
- 4 edges to [[_COMMUNITY_Booking Persistence & Entities]]
- 3 edges to [[_COMMUNITY_Application DTO Namespaces]]
- 3 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 2 edges to [[_COMMUNITY_Payment Gateway Integration Tests]]
- 1 edge to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 1 edge to [[_COMMUNITY_Validator Unit Tests]]
- 1 edge to [[_COMMUNITY_Integration Test Harness]]
- 1 edge to [[_COMMUNITY_Auth DTOs & Models]]

## Top bridge nodes
- [[BookingService]] - degree 20, connects to 5 communities
- [[.TryCreateAsync()]] - degree 18, connects to 4 communities
- [[.FindByTokenAsync()]] - degree 8, connects to 3 communities
- [[.AssignRooms()]] - degree 7, connects to 3 communities
- [[CreateBookingRequestDto]] - degree 9, connects to 2 communities