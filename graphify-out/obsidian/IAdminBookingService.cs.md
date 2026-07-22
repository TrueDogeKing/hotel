---
source_file: "src/CampCenter.Application/Interfaces/IAdminBookingService.cs"
type: "code"
community: "Application Namespaces & DTOs"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_Namespaces__DTOs
---

# IAdminBookingService.cs

## Context

_Source: `src/CampCenter.Application/Interfaces/IAdminBookingService.cs` (defined near L1; showing L1–L35 of 35)._

```csharp
using CampCenter.Application.DTOs.AdminPanel;
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
- [[CampCenter.Application.DTOs.AdminPanel]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[IAdminBookingService]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_Namespaces__DTOs