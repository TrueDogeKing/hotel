---
source_file: "src/CampCenter.Infrastructure/DependencyInjection.cs"
type: "code"
community: "Rate Limiting & Startup"
location: "L12"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Rate_Limiting__Startup
---

# CampCenter.Infrastructure

## Context

_Source: `src/CampCenter.Infrastructure/DependencyInjection.cs` (defined near L12; showing L10–L42 of 42)._

```csharp
using Microsoft.Extensions.DependencyInjection;

namespace CampCenter.Infrastructure;

/// Registration of infrastructure services in the DI container.
public static class DependencyInjection
{
    /// Registers <see cref="Persistence.AppDbContext"/> with a PostgreSQL (Npgsql) provider
    /// and other infrastructure services.
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
        services.AddSingleton<ITokenService, JwtTokenService>();

        services.AddScoped<IAdminUserRepository, AdminUserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddSingleton<IEmailSender, SmtpEmailSender>();
        services.AddHttpClient<IPaymentGateway, Przelewy24Client>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IRoomTaskRepository, RoomTaskRepository>();
        services.AddScoped<IClosureRepository, ClosureRepository>();

        return services;
    }
}
```

## Connections
- [[DependencyInjection.cs_1]] - `contains` [EXTRACTED]
- [[Program.cs]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Rate_Limiting__Startup