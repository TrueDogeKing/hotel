namespace CampCenter.Application.DTOs.Schedule;

public record MealTimeDefaultDto(
    Guid Id,
    string MealKind,
    string Label,
    TimeOnly StartTime,
    TimeOnly EndTime,
    int SortOrder,
    bool IsActive,
    uint RowVersion
);

public record CreateMealTimeDefaultRequestDto(
    string MealKind,
    string Label,
    TimeOnly StartTime,
    TimeOnly EndTime,
    int SortOrder
);

public record UpdateMealTimeDefaultRequestDto(
    string MealKind,
    string Label,
    TimeOnly StartTime,
    TimeOnly EndTime,
    int SortOrder,
    bool IsActive,
    uint RowVersion
);

/// Delete outcome: a default that already produced entries is deactivated rather
/// than removed, so generated meals keep their provenance.
public record DeleteMealTimeDefaultResultDto(bool Deleted);

// --- Per-group meal times -------------------------------------------------

/// One center slot as it applies to a single group: the center times, this
/// group's times, and whether they differ.
public record BookingMealTimeDto(
    Guid MealTimeDefaultId,
    string MealKind,
    string Label,
    int SortOrder,
    TimeOnly DefaultStartTime,
    TimeOnly DefaultEndTime,
    TimeOnly StartTime,
    TimeOnly EndTime,
    bool IsOverridden,
    uint RowVersion
);

public record SetBookingMealTimeRequestDto(
    TimeOnly StartTime,
    TimeOnly EndTime,
    /// Re-time this group's already-generated meals for this slot across the whole
    /// stay. Entries an admin moved for one specific day are always left alone.
    bool ApplyToExisting,
    uint RowVersion
);

/// How a re-time landed: how many meals moved, how many one-off exceptions were
/// deliberately preserved, and how many the stay was missing and got seeded.
public record ApplyBookingMealTimeResultDto(
    BookingMealTimeDto MealTime,
    int Updated,
    int SkippedCustomized,
    int Created
);
