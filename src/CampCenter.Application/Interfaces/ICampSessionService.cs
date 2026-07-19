using CampCenter.Application.DTOs.Sessions;

namespace CampCenter.Application.Interfaces;

public interface ICampSessionService
{
    Task<List<CampSessionDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<CampSessionDto> CreateAsync(
        CreateCampSessionRequestDto request,
        CancellationToken cancellationToken = default
    );

    Task<CampSessionDto> UpdateAsync(
        Guid id,
        UpdateCampSessionRequestDto request,
        CancellationToken cancellationToken = default
    );

    Task<CampSessionDto> PublishAsync(Guid id, CancellationToken cancellationToken = default);

    Task<CampSessionDto> ArchiveAsync(Guid id, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
