---
type: community
cohesion: 0.25
members: 9
---

# Admin Booking & Notifications (5)

**Cohesion:** 0.25 - loosely connected
**Members:** 9 nodes

## Members
- [[.SendAsync()_1]] - code - src/CampCenter.Infrastructure/Email/SmtpEmailSender.cs
- [[CampCenter.Infrastructure.Email]] - code - src/CampCenter.Infrastructure/Email/EmailSettings.cs
- [[CancellationToken_36]] - code
- [[EmailSettings]] - code - src/CampCenter.Infrastructure/Email/EmailSettings.cs
- [[EmailSettings.cs]] - code - src/CampCenter.Infrastructure/Email/EmailSettings.cs
- [[SmtpEmailSender]] - code - src/CampCenter.Infrastructure/Email/SmtpEmailSender.cs
- [[SmtpEmailSender.cs]] - code - src/CampCenter.Infrastructure/Email/SmtpEmailSender.cs
- [[Task_35]] - code
- [[string_5]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Admin_Booking__Notifications_5
SORT file.name ASC
```

## Connections to other communities
- 2 edges to [[_COMMUNITY_Admin Booking & Notifications (4)]]
- 1 edge to [[_COMMUNITY_Application Namespaces & DTOs]]

## Top bridge nodes
- [[SmtpEmailSender]] - degree 4, connects to 1 community
- [[.SendAsync()_1]] - degree 4, connects to 1 community
- [[SmtpEmailSender.cs]] - degree 3, connects to 1 community