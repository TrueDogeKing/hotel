namespace CampCenter.Application.Common;

/// Canonical form for logins so lookups and the unique index are case-insensitive
/// in practice (applied at login).
public static class LoginNormalizer
{
    public static string Normalize(string login) => login.Trim().ToLowerInvariant();
}
