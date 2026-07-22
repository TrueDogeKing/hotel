---
source_file: "src/CampCenter.Application/Interfaces/IAdminBookingService.cs"
type: "code"
community: "Admin Bookings Controller & DTOs"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Bookings_Controller__DTOs
---

# IAdminBookingService

## Context

_Source: `src/CampCenter.Application/Interfaces/IAdminBookingService.cs` (defined near L6; showing L4–L35 of 35)._

```csharp
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
- [[.CancelAsync()]] - `method` [EXTRACTED]
- [[.GetAsync()]] - `method` [EXTRACTED]
- [[.GetDashboardAsync()]] - `method` [EXTRACTED]
- [[.GetOccupancy()]] - `references` [EXTRACTED]
- [[.GetOccupancyAsync()]] - `method` [EXTRACTED]
- [[.ListAsync()]] - `method` [EXTRACTED]
- [[.ReassignAsync()]] - `method` [EXTRACTED]
- [[AdminBookingService]] - `implements` [EXTRACTED]
- [[BookingsController]] - `references` [EXTRACTED]
- [[DashboardController]] - `references` [EXTRACTED]
- [[IAdminBookingService.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Bookings_Controller__DTOs