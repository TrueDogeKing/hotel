---
source_file: "src/CampCenter.Api/Program.cs"
type: "code"
community: "Rate Limiting & Startup"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Rate_Limiting__Startup
---

# Program.cs

## Context

_Source: `src/CampCenter.Api/Program.cs` (defined near L1; showing L1–L46 of 216)._

```csharp
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
builder.Services.AddHealthChecks();

// CORS for the frontend. Credentials are allowed (refresh token cookie), so origins must be explicit.
```

## Connections
- [[CampCenter.Api.Errors]] - `imports` [EXTRACTED]
- [[CampCenter.Api.OpenApi]] - `imports` [EXTRACTED]
- [[CampCenter.Api.RateLimiting]] - `imports` [EXTRACTED]
- [[CampCenter.Application]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Auth]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Persistence]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Persistence.Seed]] - `imports` [EXTRACTED]
- [[Program]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Rate_Limiting__Startup