namespace CampCenter.Application.DTOs.Schedule;

public record ScheduleEntryDto(
    Guid Id,
    Guid BookingId,
    string OrganizationName,
    int Headcount,
    /// How many of them are supervisors — shown in brackets beside the group.
    int SupervisorCount,
    string Kind,
    string? MealKind,
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    string Title,
    string? Menu,
    string? PrepNotes,
    string? Location,
    /// Null means "the whole group" (Headcount).
    int? ParticipantCount,
    /// True when this entry's time was set for this one day, so a bulk re-time of
    /// the group's meal slot will leave it alone.
    bool TimesCustomized,
    uint RowVersion
);

// --- Month calendar -------------------------------------------------------

/// One group's stay, rendered as a bar. EndDate is INCLUSIVE here — the bar spans
/// Nights + 1 days, because the group is present on its departure day.
public record ScheduleCalendarBookingDto(
    Guid BookingId,
    string OrganizationName,
    DateOnly StartDate,
    DateOnly EndDate,
    int Nights,
    int Headcount,
    /// How many of them are supervisors — shown in brackets beside the group.
    int SupervisorCount,
    string Status
);

public record ScheduleCalendarDayDto(
    DateOnly Date,
    int GroupCount,
    int PeopleCount,
    int MealCount,
    int ActivityCount
);

public record ScheduleCalendarDto(
    DateOnly Start,
    DateOnly End,
    List<ScheduleCalendarBookingDto> Bookings,
    List<ScheduleCalendarDayDto> Days
);

// --- Day timetable --------------------------------------------------------

public record ScheduleDayGroupDto(
    Guid BookingId,
    string OrganizationName,
    int Headcount,
    /// How many of them are supervisors — shown in brackets beside the group.
    int SupervisorCount,
    string Status,
    bool IsArrivalDay,
    bool IsDepartureDay,
    string? DietaryNotes
);

public record ScheduleDayDto(
    DateOnly Date,
    List<ScheduleDayGroupDto> Groups,
    List<ScheduleEntryDto> Entries
);

// --- One group's whole programme -----------------------------------------

public record BookingScheduleDayDto(
    DateOnly Date,
    bool IsArrivalDay,
    bool IsDepartureDay,
    List<ScheduleEntryDto> Entries
);

public record BookingScheduleDto(
    Guid BookingId,
    string OrganizationName,
    string ContactName,
    string Email,
    string Phone,
    DateOnly StartDate,
    DateOnly EndDate,
    int Nights,
    int Headcount,
    /// How many of them are supervisors — shown in brackets beside the group.
    int SupervisorCount,
    string Status,
    string? Notes,
    string? DietaryNotes,
    uint BookingRowVersion,
    /// Every day of the stay, including days with no entries — so the UI needs no
    /// date arithmetic of its own.
    List<BookingScheduleDayDto> Days
);

// --- Writes ---------------------------------------------------------------

public record CreateScheduleEntryRequestDto(
    Guid BookingId,
    string Kind,
    string? MealKind,
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    string Title,
    string? Menu,
    string? PrepNotes,
    string? Location,
    int? ParticipantCount
);

/// BookingId is intentionally absent: moving an entry to another group is a
/// delete plus a create, not an update.
public record UpdateScheduleEntryRequestDto(
    string Kind,
    string? MealKind,
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    string Title,
    string? Menu,
    string? PrepNotes,
    string? Location,
    int? ParticipantCount,
    uint RowVersion
);

// --- Clash detection ------------------------------------------------------

/// A proposed entry, checked against the rest of the day before it is saved.
/// EntryId is set when editing, so an entry never clashes with itself.
public record CheckScheduleConflictsRequestDto(
    Guid BookingId,
    Guid? EntryId,
    string Kind,
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    string? Location
);

public record ScheduleConflictDto(
    Guid EntryId,
    Guid BookingId,
    string OrganizationName,
    string Kind,
    string Title,
    TimeOnly StartTime,
    TimeOnly EndTime,
    string? Location,
    /// "Location" — another group already has that place at that time.
    /// "Meal" — another group is at the tables then, wherever it eats.
    string Reason
);

/// Advisory only: the admin is warned and may save anyway, so this never blocks a
/// write. MealGapMinutes travels along to explain why near-misses count as clashes.
public record ScheduleConflictsDto(List<ScheduleConflictDto> Conflicts, int MealGapMinutes);

/// Places to suggest in the entry form. MealLocation is called out separately so the
/// form can pre-fill it for a meal — that is what keeps every sitting in one place and
/// makes the same-place check flag two groups eating at once.
public record ScheduleLocationsDto(List<string> Locations, string? MealLocation);

public record GenerateMealsResultDto(int Created);

public record GenerateMissingMealsResultDto(int Bookings, int Created);

public record UpdateDietaryNotesRequestDto(string? DietaryNotes, uint RowVersion);

// --- Public (token-authenticated) ----------------------------------------

/// Deliberately a separate type rather than a projection of ScheduleEntryDto:
/// PrepNotes is internal kitchen information, and a separate type means a field
/// added to the admin DTO later cannot silently become public.
public record PublicScheduleEntryDto(
    string Kind,
    string? MealKind,
    TimeOnly StartTime,
    TimeOnly EndTime,
    string Title,
    string? Menu,
    string? Location
);

public record PublicScheduleDayDto(DateOnly Date, List<PublicScheduleEntryDto> Entries);

public record PublicScheduleDto(
    DateOnly StartDate,
    DateOnly EndDate,
    string Status,
    List<PublicScheduleDayDto> Days
);
