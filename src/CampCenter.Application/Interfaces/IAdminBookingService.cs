using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.DTOs.Schedule;
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

    /// Sets the kitchen-facing dietary/preparation note for a group.
    Task<AdminBookingDto> UpdateDietaryNotesAsync(
        Guid id,
        UpdateDietaryNotesRequestDto request,
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
