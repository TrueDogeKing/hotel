using CampCenter.Application.DTOs.Schedule;

namespace CampCenter.Application.Interfaces;

public interface IMealTimeService
{
    Task<List<MealTimeDefaultDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<MealTimeDefaultDto> CreateAsync(
        CreateMealTimeDefaultRequestDto request,
        CancellationToken cancellationToken = default
    );

    Task<MealTimeDefaultDto> UpdateAsync(
        Guid id,
        UpdateMealTimeDefaultRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Hard-deletes an unreferenced default; deactivates one that already produced
    /// schedule entries. The result says which happened.
    Task<DeleteMealTimeDefaultResultDto> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );
}
