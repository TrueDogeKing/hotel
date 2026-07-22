---
source_file: "src/CampCenter.Domain/Repositories/IAdminUserRepository.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L3"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# CampCenter.Domain.Repositories

## Context

_Source: `src/CampCenter.Domain/Repositories/IAdminUserRepository.cs` (defined near L3; showing L1–L12 of 12)._

```csharp
using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IAdminUserRepository
{
    Task<AdminUser?> GetByLoginAsync(string login, CancellationToken cancellationToken = default);

    Task<AdminUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
```

## Connections
- [[AdminBookingService.cs]] - `imports` [EXTRACTED]
- [[AdminUserRepository.cs]] - `imports` [EXTRACTED]
- [[AuthService.cs]] - `imports` [EXTRACTED]
- [[AvailabilityService.cs]] - `imports` [EXTRACTED]
- [[BookingMaintenanceService.cs]] - `imports` [EXTRACTED]
- [[BookingRepository.cs]] - `imports` [EXTRACTED]
- [[BookingService.cs]] - `imports` [EXTRACTED]
- [[CampSessionService.cs]] - `imports` [EXTRACTED]
- [[ClosureRepository.cs]] - `imports` [EXTRACTED]
- [[DependencyInjection.cs_1]] - `imports` [EXTRACTED]
- [[IAdminUserRepository.cs]] - `contains` [EXTRACTED]
- [[IBookingRepository.cs]] - `contains` [EXTRACTED]
- [[IClosureRepository.cs]] - `contains` [EXTRACTED]
- [[IRefreshTokenRepository.cs]] - `contains` [EXTRACTED]
- [[IRoomRepository.cs]] - `contains` [EXTRACTED]
- [[IRoomTaskRepository.cs]] - `contains` [EXTRACTED]
- [[PaymentService.cs]] - `imports` [EXTRACTED]
- [[RefreshTokenRepository.cs]] - `imports` [EXTRACTED]
- [[RoomRepository.cs]] - `imports` [EXTRACTED]
- [[RoomService.cs]] - `imports` [EXTRACTED]
- [[RoomTaskRepository.cs]] - `imports` [EXTRACTED]
- [[RoomTaskService.cs]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces