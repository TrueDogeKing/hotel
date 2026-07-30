---
type: community
cohesion: 0.21
members: 15
---

# Admin Booking & Notifications (4)

**Cohesion:** 0.21 - loosely connected
**Members:** 15 nodes

## Members
- [[.BookingCancelled()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.BookingConfirmed()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.BookingCreated()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.Format()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.FormatDateTime()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.FormatZl()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.SendAsync()]] - code - src/CampCenter.Application/Interfaces/IEmailSender.cs
- [[CancellationToken_17]] - code
- [[DateOnly_1]] - code
- [[DateTime_2]] - code
- [[EmailMessage]] - code - src/CampCenter.Application/Interfaces/IEmailSender.cs
- [[EmailTemplates]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[IEmailSender]] - code - src/CampCenter.Application/Interfaces/IEmailSender.cs
- [[IEmailSender.cs]] - code - src/CampCenter.Application/Interfaces/IEmailSender.cs
- [[Task_16]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Admin_Booking__Notifications_4
SORT file.name ASC
```

## Connections to other communities
- 3 edges to [[_COMMUNITY_Booking Persistence & Entities (2)]]
- 2 edges to [[_COMMUNITY_Admin Booking & Notifications (5)]]
- 1 edge to [[_COMMUNITY_Application Namespaces & DTOs]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Services (5)]]

## Top bridge nodes
- [[EmailTemplates]] - degree 7, connects to 1 community
- [[.BookingCreated()]] - degree 7, connects to 1 community
- [[EmailMessage]] - degree 6, connects to 1 community
- [[.BookingConfirmed()]] - degree 4, connects to 1 community
- [[IEmailSender.cs]] - degree 3, connects to 1 community