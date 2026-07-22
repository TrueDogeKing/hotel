---
source_file: "src/CampCenter.Infrastructure/DependencyInjection.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# DependencyInjection.cs

## Context

_Source: `src/CampCenter.Infrastructure/DependencyInjection.cs` (defined near L1; showing L1–L42 of 42)._

```csharp
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Auth;
using CampCenter.Infrastructure.Email;
using CampCenter.Infrastructure.Payments;
using CampCenter.Infrastructure.Persistence;
using CampCenter.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure]] - `contains` [EXTRACTED]
- [[CampCenter.Infrastructure.Auth]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Email]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Payments]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Persistence]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Repositories]] - `imports` [EXTRACTED]
- [[DependencyInjection_1]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces