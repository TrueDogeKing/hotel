using CampCenter.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace CampCenter.Api.Auth;

/// <summary>
/// Only an administrator may change anything: a request that is not a read
/// (GET/HEAD/OPTIONS) has to carry the Administrator role.
/// </summary>
/// <remarks>
/// Applied as part of the default policy rather than attribute-by-attribute so it
/// cannot be forgotten: an endpoint added later is covered the moment it is
/// written, and a missing attribute makes a write *more* restricted, never less.
/// Which sections a worker may read at all is a separate decision, made per
/// controller with <c>[Authorize(Roles = …)]</c>.
/// </remarks>
public class WriteRequiresAdministratorRequirement : IAuthorizationRequirement { }

public class WriteRequiresAdministratorHandler
    : AuthorizationHandler<WriteRequiresAdministratorRequirement>
{
    private static readonly string[] ReadMethods = ["GET", "HEAD", "OPTIONS"];

    private readonly IHttpContextAccessor _http;

    public WriteRequiresAdministratorHandler(IHttpContextAccessor http) => _http = http;

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        WriteRequiresAdministratorRequirement requirement
    )
    {
        var method = _http.HttpContext?.Request.Method;

        // No HttpContext means this is not an HTTP request being authorized; fail
        // the requirement rather than guessing it was a read.
        if (method is not null && ReadMethods.Contains(method))
        {
            context.Succeed(requirement);
        }
        else if (context.User.IsInRole(nameof(AdminUserRole.Administrator)))
        {
            context.Succeed(requirement);
        }
        else if (AllowsWorkerWrite())
        {
            // A worker reaching a deliberately opened endpoint. Still an authenticated
            // panel account — the default policy's other requirements have already
            // established that.
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }

    /// Whether the endpoint being called carries <see cref="AllowWorkerWriteAttribute"/>.
    private bool AllowsWorkerWrite() =>
        _http.HttpContext?.GetEndpoint()?.Metadata.GetMetadata<AllowWorkerWriteAttribute>()
        is not null;
}
