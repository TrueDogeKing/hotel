---
source_file: "src/CampCenter.Application/DependencyInjection.cs"
type: "code"
community: "Application DI Registration"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_DI_Registration
---

# DependencyInjection.cs

## Context

_Source: `src/CampCenter.Application/DependencyInjection.cs` (defined near L1; showing L1–L26 of 26)._

```csharp
using CampCenter.Application.Interfaces;
using CampCenter.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CampCenter.Application;

public static class DependencyInjection
{
    /// Register services and validators application layer.
    /// <param name="services">Service collection.</param>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IClosureService, ClosureService>();
        services.AddScoped<IAvailabilityService, AvailabilityService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IAdminBookingService, AdminBookingService>();
        services.AddScoped<IRoomTaskService, RoomTaskService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
```

## Connections
- [[CampCenter.Application]] - `contains` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Services]] - `imports` [EXTRACTED]
- [[DependencyInjection]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_DI_Registration