---
source_file: "src/CampCenter.Application/Interfaces/IAdminBookingService.cs"
type: "code"
community: "Application Namespaces & DTOs"
location: "L4"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_Namespaces__DTOs
---

# CampCenter.Application.Interfaces

## Context

_Source: `src/CampCenter.Application/Interfaces/IAdminBookingService.cs` (defined near L4; showing L2–L35 of 35)._

```csharp
using CampCenter.Domain.Entities;

namespace CampCenter.Application.Interfaces;

public interface IAdminBookingService
{
    Task<List<AdminBookingDto>> ListAsync(
        BookingStatus? status,
        CancellationToken cancellationToken = default
    );

    Task<AdminBookingDto> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// Admin cancel of any live booking (refunds are handled manually outside the system).
    Task CancelAsync(Guid id, CancellationToken cancellationToken = default);

    /// Replaces the booking's room assignments. Admin override: people counts may
    /// exceed room capacity (extra beds are a housekeeping task).
    Task<AdminBookingDto> ReassignAsync(
        Guid id,
        ReassignBookingRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Per-room occupancy over an arbitrary date range: each room free, booked, or
    /// blocked by a closure.
    Task<OccupancyDto> GetOccupancyAsync(
        DateOnly start,
        DateOnly end,
        CancellationToken cancellationToken = default
    );

    Task<DashboardDto> GetDashboardAsync(CancellationToken cancellationToken = default);
}
```

## Connections
- [[AdminBookingService.cs]] - `imports` [EXTRACTED]
- [[AuthController.cs]] - `imports` [EXTRACTED]
- [[AuthService.cs]] - `imports` [EXTRACTED]
- [[AvailabilityService.cs]] - `imports` [EXTRACTED]
- [[BcryptPasswordHasher.cs]] - `imports` [EXTRACTED]
- [[BookingMaintenanceService.cs]] - `imports` [EXTRACTED]
- [[BookingService.cs]] - `imports` [EXTRACTED]
- [[BookingsController.cs]] - `imports` [EXTRACTED]
- [[CampSessionService.cs]] - `imports` [EXTRACTED]
- [[DashboardController.cs]] - `imports` [EXTRACTED]
- [[DataSeeder.cs]] - `imports` [EXTRACTED]
- [[DependencyInjection.cs]] - `imports` [EXTRACTED]
- [[DependencyInjection.cs_1]] - `imports` [EXTRACTED]
- [[EmailTemplates.cs]] - `imports` [EXTRACTED]
- [[IAdminBookingService.cs]] - `contains` [EXTRACTED]
- [[IAuthService.cs]] - `contains` [EXTRACTED]
- [[IAvailabilityService.cs]] - `contains` [EXTRACTED]
- [[IBookingService.cs]] - `contains` [EXTRACTED]
- [[ICampSessionService.cs]] - `contains` [EXTRACTED]
- [[IEmailSender.cs]] - `contains` [EXTRACTED]
- [[IPasswordHasher.cs]] - `contains` [EXTRACTED]
- [[IPaymentGateway.cs]] - `contains` [EXTRACTED]
- [[IPaymentService.cs]] - `contains` [EXTRACTED]
- [[IRoomService.cs]] - `contains` [EXTRACTED]
- [[IRoomTaskService.cs]] - `contains` [EXTRACTED]
- [[ITokenService.cs]] - `contains` [EXTRACTED]
- [[JwtTokenService.cs]] - `imports` [EXTRACTED]
- [[P24SignCalculator.cs]] - `imports` [EXTRACTED]
- [[P24SignCalculatorTests.cs]] - `imports` [EXTRACTED]
- [[PaymentService.cs]] - `imports` [EXTRACTED]
- [[PaymentsApiTests.cs]] - `imports` [EXTRACTED]
- [[Przelewy24Client.cs]] - `imports` [EXTRACTED]
- [[PublicBookingsController.cs]] - `imports` [EXTRACTED]
- [[PublicPaymentsController.cs]] - `imports` [EXTRACTED]
- [[PublicSessionsController.cs]] - `imports` [EXTRACTED]
- [[RoomService.cs]] - `imports` [EXTRACTED]
- [[RoomTaskService.cs]] - `imports` [EXTRACTED]
- [[RoomsController.cs]] - `imports` [EXTRACTED]
- [[SessionsController.cs]] - `imports` [EXTRACTED]
- [[SmtpEmailSender.cs]] - `imports` [EXTRACTED]
- [[TasksController.cs]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_Namespaces__DTOs