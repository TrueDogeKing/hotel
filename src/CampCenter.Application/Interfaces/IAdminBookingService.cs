using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Domain.Entities;

namespace CampCenter.Application.Interfaces;

public interface IAdminBookingService
{
    Task<List<AdminBookingDto>> ListAsync(
        Guid? campSessionId,
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

    Task<SessionOccupancyDto> GetOccupancyAsync(
        Guid campSessionId,
        CancellationToken cancellationToken = default
    );

    Task<DashboardDto> GetDashboardAsync(CancellationToken cancellationToken = default);
}
