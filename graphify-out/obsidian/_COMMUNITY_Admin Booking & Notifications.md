---
type: community
cohesion: 0.07
members: 74
---

# Admin Booking & Notifications

**Cohesion:** 0.07 - loosely connected
**Members:** 74 nodes

## Members
- [[.AddAssignmentAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.AddAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.AddPaymentAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.BookingCancelled()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.BookingConfirmed()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.BookingCreated()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.CancelAsync()_1]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.Format()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.FormatDateTime()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.FormatZl()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.GetAsync()_1]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.GetBookedRoomIdsInRangeAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.GetByIdAsync()_1]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.GetByTokenHashAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.GetCompletedPaymentKindsAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.GetConfirmedEndedAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.GetDashboardAsync()_1]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.GetExpiredPendingAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.GetOccupancyAsync()_1]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.GetOrThrowAsync()]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.GetPaymentByP24SessionIdAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.GetPaymentsAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.HandleNotificationAsync()_1]] - code - src/CampCenter.Application/Services/PaymentService.cs
- [[.InitiateAsync()_1]] - code - src/CampCenter.Application/Services/PaymentService.cs
- [[.ListAsync()_2]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.ListAsync()_4]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.ListLiveInRangeAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.ListUpcomingAsync()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.ReassignAsync()_1]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[.SaveChangesAsync()_1]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.SendAsync()]] - code - src/CampCenter.Application/Interfaces/IEmailSender.cs
- [[.SendAsync()_1]] - code - src/CampCenter.Infrastructure/Email/SmtpEmailSender.cs
- [[.SendSafelyAsync()_1]] - code - src/CampCenter.Application/Services/PaymentService.cs
- [[.ToDto()]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[AdminBookingDto]] - code - src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs
- [[AdminBookingService]] - code - src/CampCenter.Application/Services/AdminBookingService.cs
- [[BookingSettings]] - code - src/CampCenter.Application/Models/BookingSettings.cs
- [[BookingSettings.cs]] - code - src/CampCenter.Application/Models/BookingSettings.cs
- [[CampCenter.Infrastructure.Email]] - code - src/CampCenter.Infrastructure/Email/EmailSettings.cs
- [[CancellationToken_17]] - code
- [[CancellationToken_22]] - code
- [[CancellationToken_27]] - code
- [[CancellationToken_31]] - code
- [[CancellationToken_36]] - code
- [[DateOnly_1]] - code
- [[DateOnly_6]] - code
- [[DateTime_2]] - code
- [[DateTime_9]] - code
- [[Dictionary_3]] - code
- [[EmailMessage]] - code - src/CampCenter.Application/Interfaces/IEmailSender.cs
- [[EmailSettings]] - code - src/CampCenter.Infrastructure/Email/EmailSettings.cs
- [[EmailSettings.cs]] - code - src/CampCenter.Infrastructure/Email/EmailSettings.cs
- [[EmailTemplates]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[Guid_10]] - code
- [[Guid_26]] - code
- [[IBookingRepository]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[ICampSessionRepository]] - code
- [[IEmailSender]] - code - src/CampCenter.Application/Interfaces/IEmailSender.cs
- [[IEmailSender.cs]] - code - src/CampCenter.Application/Interfaces/IEmailSender.cs
- [[ILogger_2]] - code
- [[ILogger_4]] - code
- [[IReadOnlyCollection]] - code
- [[List_5]] - code
- [[List_13]] - code
- [[PaymentService]] - code - src/CampCenter.Application/Services/PaymentService.cs
- [[SmtpEmailSender]] - code - src/CampCenter.Infrastructure/Email/SmtpEmailSender.cs
- [[SmtpEmailSender.cs]] - code - src/CampCenter.Infrastructure/Email/SmtpEmailSender.cs
- [[Task_16]] - code
- [[Task_21]] - code
- [[Task_26]] - code
- [[Task_30]] - code
- [[Task_35]] - code
- [[string_2]] - code
- [[string_5]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Admin_Booking__Notifications
SORT file.name ASC
```

## Connections to other communities
- 27 edges to [[_COMMUNITY_Booking Persistence & Entities]]
- 15 edges to [[_COMMUNITY_Public Booking Service]]
- 8 edges to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 5 edges to [[_COMMUNITY_Payment Gateway Integration Tests]]
- 4 edges to [[_COMMUNITY_Room Management]]
- 3 edges to [[_COMMUNITY_Application Namespaces & DTOs]]
- 3 edges to [[_COMMUNITY_Auth DTOs & Models]]
- 3 edges to [[_COMMUNITY_Room Task Management]]
- 2 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 2 edges to [[_COMMUNITY_Domain & Infra Namespaces]]
- 1 edge to [[_COMMUNITY_Booking Maintenance Background Service]]
- 1 edge to [[_COMMUNITY_Room Mix Calculator Tests]]

## Top bridge nodes
- [[IBookingRepository]] - degree 24, connects to 4 communities
- [[AdminBookingService]] - degree 17, connects to 4 communities
- [[PaymentService]] - degree 11, connects to 4 communities
- [[.ReassignAsync()_1]] - degree 12, connects to 3 communities
- [[.InitiateAsync()_1]] - degree 11, connects to 3 communities