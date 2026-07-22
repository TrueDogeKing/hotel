---
source_file: "src/CampCenter.Application/DependencyInjection.cs"
type: "code"
community: "Application DI Registration"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_DI_Registration
---

# CampCenter.Application

## Context

_Source: `src/CampCenter.Application/DependencyInjection.cs` (defined near L6; showing L4–L26 of 26)._

```csharp
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
- [[DependencyInjection.cs]] - `contains` [EXTRACTED]
- [[Program.cs]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_DI_Registration