namespace CampCenter.Api.RateLimiting;

/// Names of the rate limiting policies configured in Program.cs.
public static class RateLimitPolicies
{
    /// Brute-force protection for the authentication endpoints (login).
    public const string Auth = "auth";

    /// Stricter per-IP limit on public booking create/lookup endpoints.
    public const string PublicBooking = "public-booking";
}
