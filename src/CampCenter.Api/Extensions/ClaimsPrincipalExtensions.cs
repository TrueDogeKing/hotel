using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace CampCenter.Api.Extensions;

/// Helpers for reading the authenticated admin's identity from JWT claims.
public static class ClaimsPrincipalExtensions
{
    /// The admin user id from the "sub"/NameIdentifier claim. Throws for unauthenticated principals.
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var value =
            principal.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? principal.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? throw new InvalidOperationException("The current principal has no user id claim.");
        return Guid.Parse(value);
    }
}
