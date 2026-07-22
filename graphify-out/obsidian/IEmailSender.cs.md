---
source_file: "src/CampCenter.Application/Interfaces/IEmailSender.cs"
type: "code"
community: "Admin Booking & Notifications"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Booking__Notifications
---

# IEmailSender.cs

## Context

_Source: `src/CampCenter.Application/Interfaces/IEmailSender.cs` (defined near L1; showing L1–L11 of 11)._

```csharp
namespace CampCenter.Application.Interfaces;

/// <param name="To">Recipient address.</param>
/// <param name="Subject">Subject line.</param>
/// <param name="TextBody">Plain-text body (emails are deliberately plain text).</param>
public record EmailMessage(string To, string Subject, string TextBody);

public interface IEmailSender
{
    Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default);
}
```

## Connections
- [[CampCenter.Application.Interfaces]] - `contains` [EXTRACTED]
- [[EmailMessage]] - `contains` [EXTRACTED]
- [[IEmailSender]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Booking__Notifications