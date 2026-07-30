---
type: community
cohesion: 0.16
members: 28
---

# Public Booking Service (1)

**Cohesion:** 0.16 - loosely connected
**Members:** 28 nodes

## Members
- [[.Cancel()_1]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[.CancelByTokenAsync()]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[.Create()_5]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[.CreateAsync()_7]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[.GetByToken()]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[.GetByTokenAsync()]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[.GetScheduleByToken()]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[.GetScheduleByTokenAsync()]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[.InitiatePayment()]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[BookingDetailsDto_1]] - code
- [[CancellationToken_7]] - code
- [[CancellationToken_15]] - code
- [[CreateBookingRequestDto_1]] - code
- [[CreateBookingRequestDto_2]] - code
- [[CreateBookingResponseDto_1]] - code
- [[EnableRateLimiting_2]] - code
- [[HttpGet_5]] - code
- [[HttpPost_5]] - code
- [[IActionResult_6]] - code
- [[IBookingService]] - code - src/CampCenter.Application/Interfaces/IBookingService.cs
- [[IPaymentService_1]] - code
- [[IValidator_3]] - code
- [[InitiatePaymentRequestDto_1]] - code
- [[ProducesResponseType_6]] - code
- [[PublicBookingsController]] - code - src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs
- [[PublicScheduleDto]] - code - src/CampCenter.Application/DTOs/Schedule/ScheduleDtos.cs
- [[Task_7]] - code
- [[Task_14]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Public_Booking_Service_1
SORT file.name ASC
```

## Connections to other communities
- 2 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 2 edges to [[_COMMUNITY_Public Booking Service (2)]]
- 1 edge to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 1 edge to [[_COMMUNITY_DTOs  Schedule (1)]]
- 1 edge to [[_COMMUNITY_CampCenter.Application  Services (2)]]

## Top bridge nodes
- [[PublicScheduleDto]] - degree 4, connects to 3 communities
- [[PublicBookingsController]] - degree 9, connects to 2 communities
- [[IBookingService]] - degree 7, connects to 2 communities