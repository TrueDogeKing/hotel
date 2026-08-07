using CampCenter.Application.Interfaces;

namespace CampCenter.IntegrationTests;

/// Keeps the emails the API would have sent, instead of opening a socket.
///
/// CI runs no SMTP server, so every booking used to log a "Connection refused"
/// stack trace on its way past. Nothing was broken — sending is best-effort by
/// design (SendSafelyAsync and friends swallow it) — but a suite that prints
/// failures it does not have is a suite whose output stops being read.
///
/// Recording rather than discarding: what the centre emails a group is worth
/// asserting on, and until now there was no way to.
public class RecordingEmailSender : IEmailSender
{
    private readonly List<EmailMessage> _sent = [];

    public Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default)
    {
        // Registered as a singleton shared by the whole collection, so guard the
        // list even though xUnit runs one collection serially.
        lock (_sent)
        {
            _sent.Add(message);
        }

        return Task.CompletedTask;
    }

    /// Everything sent to one address. Tests share this sender, so filtering by
    /// recipient is what keeps one test's assertions clear of another's mail.
    public IReadOnlyList<EmailMessage> To(string address)
    {
        lock (_sent)
        {
            return
            [
                .. _sent.Where(m =>
                    string.Equals(m.To, address, StringComparison.OrdinalIgnoreCase)
                ),
            ];
        }
    }
}
