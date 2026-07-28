using CampCenter.Application.DTOs.Schedule;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;

namespace CampCenter.Application.Services;

public class MealTimeService : IMealTimeService
{
    private readonly IMealTimeDefaultRepository _mealTimes;

    public MealTimeService(IMealTimeDefaultRepository mealTimes) => _mealTimes = mealTimes;

    public async Task<List<MealTimeDefaultDto>> GetAllAsync(
        CancellationToken cancellationToken = default
    ) => (await _mealTimes.GetAllAsync(cancellationToken)).Select(ToDto).ToList();

    public async Task<MealTimeDefaultDto> CreateAsync(
        CreateMealTimeDefaultRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        GuardTimes(request.StartTime, request.EndTime);

        var mealTime = new MealTimeDefault
        {
            Id = Guid.NewGuid(),
            MealKind = ParseMealKind(request.MealKind),
            Label = request.Label.Trim(),
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            SortOrder = request.SortOrder,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        await _mealTimes.AddAsync(mealTime, cancellationToken);
        await _mealTimes.SaveChangesAsync(cancellationToken);
        return ToDto(mealTime);
    }

    public async Task<MealTimeDefaultDto> UpdateAsync(
        Guid id,
        UpdateMealTimeDefaultRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var mealTime = await GetOrThrowAsync(id, cancellationToken);
        if (mealTime.RowVersion != request.RowVersion)
        {
            throw new ConcurrencyConflictException(
                "The meal slot was modified by someone else. Reload and try again."
            );
        }

        GuardTimes(request.StartTime, request.EndTime);

        // Editing a default affects future generation only — entries already
        // generated from it keep the times they were created with.
        mealTime.MealKind = ParseMealKind(request.MealKind);
        mealTime.Label = request.Label.Trim();
        mealTime.StartTime = request.StartTime;
        mealTime.EndTime = request.EndTime;
        mealTime.SortOrder = request.SortOrder;
        mealTime.IsActive = request.IsActive;

        await _mealTimes.SaveChangesAsync(cancellationToken);
        return ToDto(mealTime);
    }

    public async Task<DeleteMealTimeDefaultResultDto> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var mealTime = await GetOrThrowAsync(id, cancellationToken);

        if (await _mealTimes.IsReferencedAsync(id, cancellationToken))
        {
            // Meals were already generated from this slot; deactivate so history
            // survives and generation stops using it.
            mealTime.IsActive = false;
            await _mealTimes.SaveChangesAsync(cancellationToken);
            return new DeleteMealTimeDefaultResultDto(false);
        }

        _mealTimes.Remove(mealTime);
        await _mealTimes.SaveChangesAsync(cancellationToken);
        return new DeleteMealTimeDefaultResultDto(true);
    }

    private static void GuardTimes(TimeOnly start, TimeOnly end)
    {
        if (end <= start)
        {
            throw new BusinessRuleViolationException("End time must be after the start time.");
        }
    }

    private static MealKind ParseMealKind(string mealKind) =>
        Enum.TryParse<MealKind>(mealKind, ignoreCase: true, out var parsed)
            ? parsed
            : throw new BusinessRuleViolationException($"Unknown meal kind '{mealKind}'.");

    private async Task<MealTimeDefault> GetOrThrowAsync(
        Guid id,
        CancellationToken cancellationToken
    ) =>
        await _mealTimes.GetByIdAsync(id, cancellationToken)
        ?? throw new NotFoundException("Meal slot not found.");

    private static MealTimeDefaultDto ToDto(MealTimeDefault m) =>
        new(
            m.Id,
            m.MealKind.ToString(),
            m.Label,
            m.StartTime,
            m.EndTime,
            m.SortOrder,
            m.IsActive,
            m.RowVersion
        );
}
