---
source_file: "src/CampCenter.Infrastructure/Email/EmailSettings.cs"
type: "code"
community: "Admin Booking & Notifications"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Booking__Notifications
---

# EmailSettings

## Context

_Source: `src/CampCenter.Infrastructure/Email/EmailSettings.cs` (defined near L5; showing L3–L22 of 22)._

```csharp
/// SMTP configuration bound from the "Email" section. Dev points at Mailpit
/// (localhost:1025, no auth); prod at a real provider.
public class EmailSettings
{
    public const string SectionName = "Email";

    public string Host { get; set; } = "localhost";

    public int Port { get; set; } = 1025;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string From { get; set; } = "rezerwacje@campcenter.local";

    public string FromName { get; set; } = "Ośrodek CampCenter";

    public bool UseSsl { get; set; }
}
```

## Connections
- [[EmailSettings.cs]] - `contains` [EXTRACTED]
- [[SmtpEmailSender]] - `references` [EXTRACTED]
- [[string_5]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Booking__Notifications