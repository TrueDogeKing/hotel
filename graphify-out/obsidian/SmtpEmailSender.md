---
source_file: "src/CampCenter.Infrastructure/Email/SmtpEmailSender.cs"
type: "code"
community: "Admin Booking & Notifications"
location: "L10"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Booking__Notifications
---

# SmtpEmailSender

## Context

_Source: `src/CampCenter.Infrastructure/Email/SmtpEmailSender.cs` (defined near L10; showing L8–L43 of 43)._

```csharp

/// MailKit-based SMTP sender. A connection per message is plenty at booking volumes.
public class SmtpEmailSender : IEmailSender
{
    private readonly EmailSettings _settings;

    public SmtpEmailSender(IOptions<EmailSettings> settings) => _settings = settings.Value;

    public async Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default)
    {
        var mime = new MimeMessage();
        mime.From.Add(new MailboxAddress(_settings.FromName, _settings.From));
        mime.To.Add(MailboxAddress.Parse(message.To));
        mime.Subject = message.Subject;
        mime.Body = new TextPart("plain") { Text = message.TextBody };

        using var client = new SmtpClient();
        await client.ConnectAsync(
            _settings.Host,
            _settings.Port,
            _settings.UseSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto,
            cancellationToken
        );
        if (!string.IsNullOrEmpty(_settings.Username))
        {
            await client.AuthenticateAsync(
                _settings.Username,
                _settings.Password,
                cancellationToken
            );
        }

        await client.SendAsync(mime, cancellationToken);
        await client.DisconnectAsync(quit: true, cancellationToken);
    }
}
```

## Connections
- [[.SendAsync()_1]] - `method` [EXTRACTED]
- [[EmailSettings]] - `references` [EXTRACTED]
- [[IEmailSender]] - `implements` [EXTRACTED]
- [[SmtpEmailSender.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Booking__Notifications