---
source_file: "src/CampCenter.Domain/Entities/AdminUser.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# CampCenter.Domain.Entities

## Context

_Source: `src/CampCenter.Domain/Entities/AdminUser.cs` (defined near L1; showing L1–L18 of 18)._

```csharp
namespace CampCenter.Domain.Entities;

/// Administrator account. Admins are created by the data seeder — there is no
/// public registration; bookers never have accounts.
public class AdminUser
{
    public Guid Id { get; set; }

    /// Unique sign-in identifier, stored lowercase.
    public required string Login { get; set; }

    public required string PasswordHash { get; set; }

    /// Create date (UTC).
    public DateTime CreatedAt { get; set; }

    public uint RowVersion { get; set; }
}
```

## Connections
- [[AdminBookingService.cs]] - `imports` [EXTRACTED]
- [[AdminUser.cs]] - `contains` [EXTRACTED]
- [[AdminUserConfiguration.cs]] - `imports` [EXTRACTED]
- [[AdminUserRepository.cs]] - `imports` [EXTRACTED]
- [[AppDbContext.cs]] - `imports` [EXTRACTED]
- [[AuthService.cs]] - `imports` [EXTRACTED]
- [[Booking.cs]] - `contains` [EXTRACTED]
- [[BookingConfiguration.cs]] - `imports` [EXTRACTED]
- [[BookingMaintenanceService.cs]] - `imports` [EXTRACTED]
- [[BookingRepository.cs]] - `imports` [EXTRACTED]
- [[BookingRoomAssignment.cs]] - `contains` [EXTRACTED]
- [[BookingRoomAssignmentConfiguration.cs]] - `imports` [EXTRACTED]
- [[BookingService.cs]] - `imports` [EXTRACTED]
- [[BookingsController.cs]] - `imports` [EXTRACTED]
- [[CampSessionService.cs]] - `imports` [EXTRACTED]
- [[Closure.cs]] - `contains` [EXTRACTED]
- [[ClosureConfiguration.cs]] - `imports` [EXTRACTED]
- [[ClosureRepository.cs]] - `imports` [EXTRACTED]
- [[DataSeeder.cs]] - `imports` [EXTRACTED]
- [[EmailTemplates.cs]] - `imports` [EXTRACTED]
- [[IAdminBookingService.cs]] - `imports` [EXTRACTED]
- [[IAdminUserRepository.cs]] - `imports` [EXTRACTED]
- [[IBookingRepository.cs]] - `imports` [EXTRACTED]
- [[IClosureRepository.cs]] - `imports` [EXTRACTED]
- [[IRefreshTokenRepository.cs]] - `imports` [EXTRACTED]
- [[IRoomRepository.cs]] - `imports` [EXTRACTED]
- [[IRoomTaskRepository.cs]] - `imports` [EXTRACTED]
- [[IRoomTaskService.cs]] - `imports` [EXTRACTED]
- [[ITokenService.cs]] - `imports` [EXTRACTED]
- [[JwtTokenService.cs]] - `imports` [EXTRACTED]
- [[Payment.cs]] - `contains` [EXTRACTED]
- [[PaymentConfiguration.cs]] - `imports` [EXTRACTED]
- [[PaymentService.cs]] - `imports` [EXTRACTED]
- [[RefreshToken.cs]] - `contains` [EXTRACTED]
- [[RefreshTokenConfiguration.cs]] - `imports` [EXTRACTED]
- [[RefreshTokenRepository.cs]] - `imports` [EXTRACTED]
- [[Room.cs]] - `contains` [EXTRACTED]
- [[RoomConfiguration.cs]] - `imports` [EXTRACTED]
- [[RoomRepository.cs]] - `imports` [EXTRACTED]
- [[RoomService.cs]] - `imports` [EXTRACTED]
- [[RoomTask.cs]] - `contains` [EXTRACTED]
- [[RoomTaskConfiguration.cs]] - `imports` [EXTRACTED]
- [[RoomTaskRepository.cs]] - `imports` [EXTRACTED]
- [[RoomTaskService.cs]] - `imports` [EXTRACTED]
- [[TasksController.cs]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces