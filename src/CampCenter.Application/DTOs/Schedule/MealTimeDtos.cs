namespace CampCenter.Application.DTOs.Schedule;

public record MealTimeDefaultDto(
    Guid Id,
    string MealKind,
    string Label,
    TimeOnly StartTime,
    TimeOnly EndTime,
    /// How long one group's sitting lasts within the window.
    int DurationMinutes,
    int SortOrder,
    bool IsActive,
    uint RowVersion
);

public record CreateMealTimeDefaultRequestDto(
    string MealKind,
    string Label,
    TimeOnly StartTime,
    TimeOnly EndTime,
    int DurationMinutes,
    int SortOrder
);

public record UpdateMealTimeDefaultRequestDto(
    string MealKind,
    string Label,
    TimeOnly StartTime,
    TimeOnly EndTime,
    int DurationMinutes,
    int SortOrder,
    bool IsActive,
    uint RowVersion
);

/// Delete outcome: a default that already produced entries is deactivated rather
/// than removed, so generated meals keep their provenance.
public record DeleteMealTimeDefaultResultDto(bool Deleted);

// --- Per-group meal times -------------------------------------------------

/// Another group's sitting in the same window, so the panel can show who this group
/// is seated around and warn before a change crowds them.
public record NeighbourSittingDto(string OrganizationName, TimeOnly StartTime, TimeOnly EndTime);

/// One center slot as it applies to a single group: the center times, this
/// group's times, and whether they differ.
public record BookingMealTimeDto(
    Guid MealTimeDefaultId,
    string MealKind,
    string Label,
    int SortOrder,
    TimeOnly DefaultStartTime,
    TimeOnly DefaultEndTime,
    /// The window's sitting length; the gap between sittings is MealGapMinutes.
    int DurationMinutes,
    TimeOnly StartTime,
    TimeOnly EndTime,
    bool IsOverridden,
    /// This group's sitting runs past the end of the serving window — the centre is
    /// hosting more groups than the window comfortably seats.
    bool ExceedsWindow,
    /// Sittings of the groups sharing the centre with this one, over this slot.
    List<NeighbourSittingDto> Neighbours,
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

/// How many meals came off the group's programme.
public record DeleteBookingMealsResultDto(int Deleted);

/// How a re-time landed: how many meals moved, how many one-off exceptions were
/// deliberately preserved, and how many the stay was missing and got seeded.
public record ApplyBookingMealTimeResultDto(
    BookingMealTimeDto MealTime,
    int Updated,
    int SkippedCustomized,
    int Created
);
