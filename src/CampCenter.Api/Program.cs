using System.Globalization;
using System.Text;
using System.Threading.RateLimiting;
using CampCenter.Api.Errors;
using CampCenter.Api.OpenApi;
using CampCenter.Api.RateLimiting;
using CampCenter.Application;
using CampCenter.Infrastructure;
using CampCenter.Infrastructure.Auth;
using CampCenter.Infrastructure.Persistence;
using CampCenter.Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

const string FrontendCorsPolicy = "Frontend";

var builder = WebApplication.CreateBuilder(args);

// The largest legitimate payload is a booking request (a few KB of JSON); Kestrel's
// 30 MB default would let a single POST buffer megabytes for nothing. 1 MB is
// generous headroom and caps memory per request.
builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = 1024 * 1024);

// In production requests arrive via Caddy; without this the app would see every
// client as Caddy's container IP and per-IP rate limiting would be global instead
// of per-client. Only proxies on private networks are trusted, so a direct client
// cannot spoof X-Forwarded-For.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownIPNetworks.Add(System.Net.IPNetwork.Parse("10.0.0.0/8"));
    options.KnownIPNetworks.Add(System.Net.IPNetwork.Parse("172.16.0.0/12"));
    options.KnownIPNetworks.Add(System.Net.IPNetwork.Parse("192.168.0.0/16"));
});

builder.Services.AddControllers();

builder.Services.AddOpenApi(options =>
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>()
);

// Health checks. The database check is tagged "ready" so the readiness endpoint
// can select it and the liveness endpoint can leave it out: liveness answers
// "is this process wedged?" (failing it restarts the container), readiness
// answers "can this instance serve a request?" (failing it only removes the
// instance from load balancing). A liveness probe that touched the database
// would turn a database blip into a restart of every API instance.
builder
    .Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>("database", tags: ["ready"]);

// CORS for the frontend. Credentials are allowed (refresh token cookie), so origins must be explicit.
var allowedOrigins =
    builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? Array.Empty<string>();
builder.Services.AddCors(options =>
    options.AddPolicy(
        FrontendCorsPolicy,
        policy =>
            policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    )
);

// Global exception handling with ProblemDetails responses.
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Uwierzytelnianie JWT.
var jwtSettings =
    builder.Configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()
    ?? throw new InvalidOperationException("Brak sekcji konfiguracji 'Jwt'.");
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionName));
builder.Services.Configure<RefreshTokenSettings>(
    builder.Configuration.GetSection(RefreshTokenSettings.SectionName)
);
builder.Services.Configure<CampCenter.Application.Models.BookingSettings>(
    builder.Configuration.GetSection(CampCenter.Application.Models.BookingSettings.SectionName)
);
builder.Services.Configure<CampCenter.Application.Models.ScheduleSettings>(
    builder.Configuration.GetSection(CampCenter.Application.Models.ScheduleSettings.SectionName)
);
builder.Services.Configure<CampCenter.Infrastructure.Email.EmailSettings>(
    builder.Configuration.GetSection(CampCenter.Infrastructure.Email.EmailSettings.SectionName)
);

// P24 is switched off (see Application/DependencyInjection.cs); nothing reads
// these settings while that is the case.
// builder.Services.Configure<CampCenter.Infrastructure.Payments.P24Settings>(
//     builder.Configuration.GetSection(CampCenter.Infrastructure.Payments.P24Settings.SectionName)
// );
builder.Services.AddHostedService<CampCenter.Api.Background.BookingMaintenanceService>();

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Off, or the handler rewrites the short claim names it knows — "role"
        // among them — into their WS-Federation URIs on the way in, and the
        // RoleClaimType below would then match nothing.
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
            ClockSkew = TimeSpan.Zero,
            // Matches the short claim JwtTokenService writes, so [Authorize(Roles = …)]
            // and the panel read the same one.
            RoleClaimType = JwtTokenService.RoleClaim,
        };
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<
    IAuthorizationHandler,
    CampCenter.Api.Auth.WriteRequiresAdministratorHandler
>();
builder.Services.AddAuthorization(options =>
{
    // Every [Authorize] endpoint: signed in, and — unless the request is a read —
    // an administrator. Workers are read-only everywhere by construction, so a
    // write endpoint added later is covered without anyone remembering to say so.
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddRequirements(new CampCenter.Api.Auth.WriteRequiresAdministratorRequirement())
        .Build();
});

// Rate limiting: brute-force protection for the authentication endpoints, partitioned by client IP.
// Behind a reverse proxy/ingress, configure forwarded headers so RemoteIpAddress is the real client.
var authPermitLimit = builder.Configuration.GetValue<int?>("RateLimiting:Auth:PermitLimit") ?? 5;
var authWindowSeconds =
    builder.Configuration.GetValue<int?>("RateLimiting:Auth:WindowSeconds") ?? 30;
var globalPermitLimit =
    builder.Configuration.GetValue<int?>("RateLimiting:Global:PermitLimit") ?? 100;
var globalWindowSeconds =
    builder.Configuration.GetValue<int?>("RateLimiting:Global:WindowSeconds") ?? 10;
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    // Backstop for every endpoint (including authenticated ones): no single IP can
    // hammer the API without limit. Generous enough for the SPA's normal traffic.
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = globalPermitLimit,
                Window = TimeSpan.FromSeconds(globalWindowSeconds),
                QueueLimit = 0,
            }
        )
    );

    // On rejection, advertise when the client may retry (seconds) via the Retry-After header,
    // so the frontend can show an accurate countdown.
    options.OnRejected = (context, _) =>
    {
        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            context.HttpContext.Response.Headers.RetryAfter = (
                (int)Math.Ceiling(retryAfter.TotalSeconds)
            ).ToString(CultureInfo.InvariantCulture);
        }
        return ValueTask.CompletedTask;
    };

    var publicPermitLimit =
        builder.Configuration.GetValue<int?>("RateLimiting:PublicBooking:PermitLimit") ?? 20;
    var publicWindowSeconds =
        builder.Configuration.GetValue<int?>("RateLimiting:PublicBooking:WindowSeconds") ?? 60;
    options.AddPolicy(
        RateLimitPolicies.PublicBooking,
        httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                factory: _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = publicPermitLimit,
                    Window = TimeSpan.FromSeconds(publicWindowSeconds),
                    QueueLimit = 0,
                }
            )
    );

    options.AddPolicy(
        RateLimitPolicies.Auth,
        httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                factory: _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = authPermitLimit,
                    Window = TimeSpan.FromSeconds(authWindowSeconds),
                    QueueLimit = 0,
                }
            )
    );
});

var app = builder.Build();

// Automated migration on container start.
if (app.Configuration.GetValue<bool>("Database:MigrateAutomatically"))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Seed initial data (default admin account, default meal slots)
if (app.Configuration.GetValue<bool>("Database:SeedAutomatically"))
{
    await DataSeeder.SeedAdminUserAsync(app.Services);
    await DataSeeder.SeedMealTimeDefaultsAsync(app.Services);
}

// Configure the HTTP request pipeline. Forwarded headers must run first so
// everything downstream (rate limiter partitions, logs) sees the real client IP.
app.UseForwardedHeaders();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors(FrontendCorsPolicy);

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Three endpoints over the same checks. Rate limiting is switched off on all of
// them: a probe that gets a 429 counts as a failed probe, and a failed liveness
// probe restarts the container.
//   /health       every check — the Docker HEALTHCHECK and compose's
//                 `depends_on: condition: service_healthy` point here, so this
//                 now also means "the database answered".
//   /health/live  no checks; the process is up and serving. Kubernetes liveness.
//   /health/ready the "ready"-tagged checks. Kubernetes readiness and startup.
app.MapHealthChecks("/health").DisableRateLimiting();
app.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false })
    .DisableRateLimiting();
app.MapHealthChecks(
        "/health/ready",
        new HealthCheckOptions { Predicate = check => check.Tags.Contains("ready") }
    )
    .DisableRateLimiting();

app.Run();

/// Exposed so the integration test project can target it with WebApplicationFactory<Program>.
public partial class Program;
