---
source_file: "src/CampCenter.Application/Interfaces/IEmailSender.cs"
type: "code"
community: "Admin Booking & Notifications"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Booking__Notifications
---

# EmailMessage

## Context

_Source: `src/CampCenter.Application/Interfaces/IEmailSender.cs` (defined near L6; showing L4–L11 of 11)._

```csharp
/// <param name="Subject">Subject line.</param>
/// <param name="TextBody">Plain-text body (emails are deliberately plain text).</param>
public record EmailMessage(string To, string Subject, string TextBody);

public interface IEmailSender
{
    Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default);
}
```

## Connections
- [[.BookingCancelled()]] - `references` [EXTRACTED]
- [[.BookingConfirmed()]] - `references` [EXTRACTED]
- [[.BookingCreated()]] - `references` [EXTRACTED]
- [[.SendAsync()]] - `references` [EXTRACTED]
- [[.SendAsync()_1]] - `references` [EXTRACTED]
- [[.SendSafelyAsync()]] - `references` [EXTRACTED]
- [[.SendSafelyAsync()_1]] - `references` [EXTRACTED]
- [[IEmailSender.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Booking__Notifications