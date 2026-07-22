---
source_file: "src/CampCenter.Application/Interfaces/IEmailSender.cs"
type: "code"
community: "Admin Booking & Notifications"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Booking__Notifications
---

# IEmailSender

## Context

_Source: `src/CampCenter.Application/Interfaces/IEmailSender.cs` (defined near L8; showing L6–L11 of 11)._

```csharp
public record EmailMessage(string To, string Subject, string TextBody);

public interface IEmailSender
{
    Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default);
}
```

## Connections
- [[.SendAsync()]] - `method` [EXTRACTED]
- [[AdminBookingService]] - `references` [EXTRACTED]
- [[BookingService]] - `references` [EXTRACTED]
- [[IEmailSender.cs]] - `contains` [EXTRACTED]
- [[PaymentService]] - `references` [EXTRACTED]
- [[SmtpEmailSender]] - `implements` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Booking__Notifications