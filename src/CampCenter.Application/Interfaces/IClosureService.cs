using CampCenter.Application.DTOs.Closures;

namespace CampCenter.Application.Interfaces;

public interface IClosureService
{
    Task<List<ClosureDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<ClosureDto> CreateAsync(
        CreateClosureRequestDto request,
        CancellationToken cancellationToken = default
    );

    Task<ClosureDto> UpdateAsync(
        Guid id,
        UpdateClosureRequestDto request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
