namespace CampCenter.Infrastructure.Email;

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
