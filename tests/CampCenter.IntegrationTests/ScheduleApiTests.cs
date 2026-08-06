using System.Net;
using System.Net.Http.Json;
using CampCenter.Application.DTOs.Public;
using CampCenter.Application.DTOs.Rooms;
using CampCenter.Application.DTOs.Schedule;

namespace CampCenter.IntegrationTests;

public class ScheduleApiTests : IntegrationTestBase
{
    // Distinct date windows per booking keep the shared room inventory clean.
    private static int _windowOffset;

    public ScheduleApiTests(CampCenterApiFactory factory)
        : base(factory) { }

    [Fact]
    public async Task ScheduleEndpoints_WithoutToken_ReturnUnauthorized()
    {
        var client = CreateClient();

        var calendar = await client.GetAsync(
            "/api/admin/schedule/calendar?start=2034-01-01&end=2034-01-31"
        );
        var day = await client.GetAsync("/api/admin/schedule/day/2034-01-01");
        var mealTimes = await client.GetAsync("/api/admin/meal-times");

        Assert.Equal(HttpStatusCode.Unauthorized, calendar.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, day.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, mealTimes.StatusCode);
    }

    [Fact]
    public async Task MealTimeDefaults_CrudRoundtrip_Works()
    {
        var admin = await CreateAuthenticatedClientAsync();

        var create = await admin.PostAsJsonAsync(
            "/api/admin/meal-times",
            new CreateMealTimeDefaultRequestDto(
                "Snack",
                "Podwieczorek testowy",
                new TimeOnly(16, 0),
                new TimeOnly(16, 30),
                DurationMinutes: 30,
                SortOrder: 9
            )
        );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var slot = (await create.Content.ReadFromJsonAsync<MealTimeDefaultDto>())!;

        var update = await admin.PutAsJsonAsync(
            $"/api/admin/meal-times/{slot.Id}",
            new UpdateMealTimeDefaultRequestDto(
                "Snack",
                "Podwieczorek",
                new TimeOnly(16, 0),
                new TimeOnly(16, 45),
                DurationMinutes: 45,
                SortOrder: 9,
                IsActive: true,
                RowVersion: slot.RowVersion
            )
        );
        Assert.Equal(HttpStatusCode.OK, update.StatusCode);

        // The same (now stale) RowVersion must be rejected.
        var stale = await admin.PutAsJsonAsync(
            $"/api/admin/meal-times/{slot.Id}",
            new UpdateMealTimeDefaultRequestDto(
                "Snack",
                "Podwieczorek 2",
                new TimeOnly(16, 0),
                new TimeOnly(16, 45),
                DurationMinutes: 45,
                SortOrder: 9,
                IsActive: true,
                RowVersion: slot.RowVersion
            )
        );
        Assert.Equal(HttpStatusCode.Conflict, stale.StatusCode);

        // Never referenced by an entry, so it is hard-deleted.
        var delete = await admin.DeleteAsync($"/api/admin/meal-times/{slot.Id}");
        Assert.Equal(HttpStatusCode.OK, delete.StatusCode);
        var result = (await delete.Content.ReadFromJsonAsync<DeleteMealTimeDefaultResultDto>())!;
        Assert.True(result.Deleted);
    }

    [Fact]
    public async Task ScheduleEntries_CrudRoundtrip_Works()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (bookingId, _, start, _) = await CreateBookingAsync(admin);

        var create = await admin.PostAsJsonAsync(
            "/api/admin/schedule/entries",
            new CreateScheduleEntryRequestDto(
                bookingId,
                "Activity",
                null,
                start.AddDays(1),
                new TimeOnly(10, 0),
                new TimeOnly(12, 0),
                "Spływ kajakowy",
                null,
                "Kamizelki dla 12 osób",
                "Przystań",
                12
            )
        );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var entry = (await create.Content.ReadFromJsonAsync<ScheduleEntryDto>())!;
        Assert.Equal("Activity", entry.Kind);
        Assert.Null(entry.MealKind);

        var update = await admin.PutAsJsonAsync(
            $"/api/admin/schedule/entries/{entry.Id}",
            new UpdateScheduleEntryRequestDto(
                "Activity",
                null,
                start.AddDays(1),
                new TimeOnly(10, 30),
                new TimeOnly(12, 30),
                "Spływ kajakowy",
                null,
                "Kamizelki dla 12 osób",
                "Przystań",
                15,
                entry.RowVersion
            )
        );
        Assert.Equal(HttpStatusCode.OK, update.StatusCode);

        var stale = await admin.PutAsJsonAsync(
            $"/api/admin/schedule/entries/{entry.Id}",
            new UpdateScheduleEntryRequestDto(
                "Activity",
                null,
                start.AddDays(1),
                new TimeOnly(11, 0),
                new TimeOnly(13, 0),
                "Spływ kajakowy",
                null,
                null,
                null,
                null,
                entry.RowVersion
            )
        );
        Assert.Equal(HttpStatusCode.Conflict, stale.StatusCode);

        var delete = await admin.DeleteAsync($"/api/admin/schedule/entries/{entry.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delete.StatusCode);
    }

    /// The stay window is inclusive at both ends. Room occupancy is half-open, so
    /// reusing that predicate here would silently reject the departure day — the
    /// day the group still eats breakfast.
    [Fact]
    public async Task ScheduleEntry_OnDepartureDay_IsAccepted_ButNotOutsideTheStay()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (bookingId, _, start, end) = await CreateBookingAsync(admin);

        var onDeparture = await admin.PostAsJsonAsync(
            "/api/admin/schedule/entries",
            EntryOn(bookingId, end, "Pakowanie")
        );
        Assert.Equal(HttpStatusCode.Created, onDeparture.StatusCode);

        var beforeArrival = await admin.PostAsJsonAsync(
            "/api/admin/schedule/entries",
            EntryOn(bookingId, start.AddDays(-1), "Za wcześnie")
        );
        Assert.Equal(HttpStatusCode.BadRequest, beforeArrival.StatusCode);

        var afterDeparture = await admin.PostAsJsonAsync(
            "/api/admin/schedule/entries",
            EntryOn(bookingId, end.AddDays(1), "Za późno")
        );
        Assert.Equal(HttpStatusCode.BadRequest, afterDeparture.StatusCode);
    }

    [Fact]
    public async Task ScheduleEntry_EndBeforeStart_IsRejected()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (bookingId, _, start, _) = await CreateBookingAsync(admin);

        var response = await admin.PostAsJsonAsync(
            "/api/admin/schedule/entries",
            new CreateScheduleEntryRequestDto(
                bookingId,
                "Activity",
                null,
                start,
                new TimeOnly(14, 0),
                new TimeOnly(13, 0),
                "Wehikuł czasu",
                null,
                null,
                null,
                null
            )
        );
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Meal_WithoutMealKind_IsRejected()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (bookingId, _, start, _) = await CreateBookingAsync(admin);

        var response = await admin.PostAsJsonAsync(
            "/api/admin/schedule/entries",
            new CreateScheduleEntryRequestDto(
                bookingId,
                "Meal",
                null,
                start,
                new TimeOnly(18, 0),
                new TimeOnly(19, 0),
                "Kolacja",
                "Kanapki",
                null,
                null,
                null
            )
        );
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    /// The exact bug ListLiveInRangeAsync's half-open predicate would introduce.
    [Fact]
    public async Task DayView_IncludesDepartingGroup()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (bookingId, _, _, end) = await CreateBookingAsync(admin);

        var day = (
            await admin.GetFromJsonAsync<ScheduleDayDto>(
                $"/api/admin/schedule/day/{end:yyyy-MM-dd}"
            )
        )!;

        var group = Assert.Single(day.Groups, g => g.BookingId == bookingId);
        Assert.True(group.IsDepartureDay);
    }

    [Fact]
    public async Task Calendar_BarSpansFullStayInclusive()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (bookingId, _, start, end) = await CreateBookingAsync(admin);

        // The window starts on the departure day: a half-open predicate would miss
        // the booking entirely.
        var calendar = (
            await admin.GetFromJsonAsync<ScheduleCalendarDto>(
                $"/api/admin/schedule/calendar?start={end:yyyy-MM-dd}&end={end.AddDays(5):yyyy-MM-dd}"
            )
        )!;
        var bar = Assert.Single(calendar.Bookings, b => b.BookingId == bookingId);

        Assert.Equal(start, bar.StartDate);
        Assert.Equal(end, bar.EndDate);
        Assert.Equal(bar.Nights + 1, bar.EndDate.DayNumber - bar.StartDate.DayNumber + 1);

        var departureDay = Assert.Single(calendar.Days, d => d.Date == end);
        Assert.True(departureDay.GroupCount >= 1);
    }

    [Fact]
    public async Task GenerateMeals_CoversTheStay_AndIsIdempotent()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (bookingId, _, start, end) = await CreateBookingAsync(admin);

        var first = await admin.PostAsync(
            $"/api/admin/schedule/bookings/{bookingId}/generate-meals",
            null
        );
        Assert.Equal(HttpStatusCode.OK, first.StatusCode);
        var created = (await first.Content.ReadFromJsonAsync<GenerateMealsResultDto>())!;
        Assert.True(created.Created > 0);

        var schedule = (
            await admin.GetFromJsonAsync<BookingScheduleDto>(
                $"/api/admin/schedule/bookings/{bookingId}"
            )
        )!;

        // Every day of the stay is materialized, departure day included.
        Assert.Equal(schedule.Nights + 1, schedule.Days.Count);
        Assert.Equal(start, schedule.Days[0].Date);
        Assert.Equal(end, schedule.Days[^1].Date);

        // Arrival day: only dinner fits after the 15:00 cutoff.
        var arrival = schedule.Days[0].Entries;
        Assert.Single(arrival);
        Assert.Equal("Dinner", arrival[0].MealKind);

        // Departure day: only breakfast ends before the 11:00 cutoff.
        var departure = schedule.Days[^1].Entries;
        Assert.Single(departure);
        Assert.Equal("Breakfast", departure[0].MealKind);

        // Middle days get all three seeded slots.
        Assert.All(schedule.Days[1..^1], day => Assert.Equal(3, day.Entries.Count));

        var totalBefore = schedule.Days.Sum(d => d.Entries.Count);

        // Re-running creates nothing and changes nothing.
        var second = await admin.PostAsync(
            $"/api/admin/schedule/bookings/{bookingId}/generate-meals",
            null
        );
        var again = (await second.Content.ReadFromJsonAsync<GenerateMealsResultDto>())!;
        Assert.Equal(0, again.Created);

        var after = (
            await admin.GetFromJsonAsync<BookingScheduleDto>(
                $"/api/admin/schedule/bookings/{bookingId}"
            )
        )!;
        Assert.Equal(totalBefore, after.Days.Sum(d => d.Entries.Count));
    }

    [Fact]
    public async Task GenerateMeals_DoesNotResurrectADeletedMeal()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (bookingId, _, _, _) = await CreateBookingAsync(admin);

        await admin.PostAsync($"/api/admin/schedule/bookings/{bookingId}/generate-meals", null);
        var schedule = (
            await admin.GetFromJsonAsync<BookingScheduleDto>(
                $"/api/admin/schedule/bookings/{bookingId}"
            )
        )!;
        var victim = schedule.Days.SelectMany(d => d.Entries).First();
        var totalBefore = schedule.Days.Sum(d => d.Entries.Count);

        await admin.DeleteAsync($"/api/admin/schedule/entries/{victim.Id}");
        await admin.PostAsync($"/api/admin/schedule/bookings/{bookingId}/generate-meals", null);

        var after = (
            await admin.GetFromJsonAsync<BookingScheduleDto>(
                $"/api/admin/schedule/bookings/{bookingId}"
            )
        )!;
        Assert.Equal(totalBefore - 1, after.Days.Sum(d => d.Entries.Count));
        Assert.DoesNotContain(after.Days.SelectMany(d => d.Entries), e => e.Id == victim.Id);
    }

    [Fact]
    public async Task DietaryNotes_AreStoredAndReturnedOnTheGroupSchedule()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (bookingId, _, _, _) = await CreateBookingAsync(admin);

        // AdminBookingDto carries no RowVersion, so the group schedule is where the
        // UI gets it from too (BookingScheduleDto.BookingRowVersion).
        var before = (
            await admin.GetFromJsonAsync<BookingScheduleDto>(
                $"/api/admin/schedule/bookings/{bookingId}"
            )
        )!;

        var update = await admin.PutAsJsonAsync(
            $"/api/admin/bookings/{bookingId}/dietary-notes",
            new UpdateDietaryNotesRequestDto(
                "2× bezglutenowa, 1× wegetariańska",
                before.BookingRowVersion
            )
        );
        Assert.Equal(HttpStatusCode.OK, update.StatusCode);

        var schedule = (
            await admin.GetFromJsonAsync<BookingScheduleDto>(
                $"/api/admin/schedule/bookings/{bookingId}"
            )
        )!;
        Assert.Equal("2× bezglutenowa, 1× wegetariańska", schedule.DietaryNotes);
    }

    /// Prep notes are internal kitchen information and must never reach the booker.
    [Fact]
    public async Task PublicSchedule_ByToken_ShowsMenu_ButHidesPrepNotes()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (bookingId, token, start, _) = await CreateBookingAsync(admin);

        await admin.PostAsJsonAsync(
            "/api/admin/schedule/entries",
            new CreateScheduleEntryRequestDto(
                bookingId,
                "Meal",
                "Dinner",
                start,
                new TimeOnly(18, 0),
                new TimeOnly(19, 0),
                "Kolacja",
                "Naleśniki z serem",
                "SEKRETNA NOTATKA KUCHNI",
                "Stołówka",
                null
            )
        );

        var response = await CreateClient().GetAsync($"/api/public/bookings/{token}/schedule");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var raw = await response.Content.ReadAsStringAsync();
        Assert.Contains("Naleśniki z serem", raw);
        Assert.DoesNotContain("SEKRETNA NOTATKA KUCHNI", raw);
        Assert.DoesNotContain("prepNotes", raw, StringComparison.OrdinalIgnoreCase);

        var unknown = await CreateClient()
            .GetAsync("/api/public/bookings/not-a-real-token/schedule");
        Assert.Equal(HttpStatusCode.NotFound, unknown.StatusCode);
    }

    // --- Per-group meal times --------------------------------------------

    [Fact]
    public async Task BookingMealTimes_DefaultToTheCenterTimes()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (bookingId, _, _, _) = await CreateBookingAsync(admin);

        var mealTimes = await GetMealTimesAsync(admin, bookingId);

        Assert.NotEmpty(mealTimes);
        Assert.All(
            mealTimes,
            m =>
            {
                Assert.False(m.IsOverridden);
                Assert.Equal(m.DefaultStartTime, m.StartTime);
                Assert.Equal(m.DefaultEndTime, m.EndTime);
            }
        );
    }

    /// The headline requirement: one group can be shifted off the shared sitting
    /// without touching any other group.
    [Fact]
    public async Task SettingAGroupsMealTime_RetimesTheWholeStay_ButNotOtherGroups()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (shifted, _, _, _) = await CreateBookingAsync(admin);
        var (untouched, _, _, _) = await CreateBookingAsync(admin);

        await GenerateMealsAsync(admin, shifted);
        await GenerateMealsAsync(admin, untouched);

        var breakfast = (await GetMealTimesAsync(admin, shifted)).Single(m =>
            m.MealKind == "Breakfast"
        );

        var response = await admin.PutAsJsonAsync(
            $"/api/admin/schedule/bookings/{shifted}/meal-times/{breakfast.MealTimeDefaultId}",
            new SetBookingMealTimeRequestDto(
                new TimeOnly(9, 30),
                new TimeOnly(10, 15),
                ApplyToExisting: true,
                breakfast.RowVersion
            )
        );
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = (await response.Content.ReadFromJsonAsync<ApplyBookingMealTimeResultDto>())!;

        Assert.True(result.MealTime.IsOverridden);
        Assert.True(result.Updated > 0);
        Assert.Equal(0, result.SkippedCustomized);

        // Every breakfast this group has now sits at the new time.
        var shiftedBreakfasts = await BreakfastsAsync(admin, shifted);
        Assert.NotEmpty(shiftedBreakfasts);
        Assert.All(shiftedBreakfasts, e => Assert.Equal(new TimeOnly(9, 30), e.StartTime));

        // The other group still eats at the center time.
        var otherBreakfasts = await BreakfastsAsync(admin, untouched);
        Assert.NotEmpty(otherBreakfasts);
        Assert.All(otherBreakfasts, e => Assert.Equal(breakfast.DefaultStartTime, e.StartTime));
    }

    /// The other half of the requirement: a day changed on its own for a special
    /// reason must survive a later bulk re-time.
    [Fact]
    public async Task BulkRetime_PreservesADayChangedIndividually()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (bookingId, _, _, _) = await CreateBookingAsync(admin);
        await GenerateMealsAsync(admin, bookingId);

        // Move one day's breakfast to 06:30 — an early start for a trip.
        var exception = (await BreakfastsAsync(admin, bookingId)).First();
        var moved = await admin.PutAsJsonAsync(
            $"/api/admin/schedule/entries/{exception.Id}",
            new UpdateScheduleEntryRequestDto(
                "Meal",
                "Breakfast",
                exception.Date,
                new TimeOnly(6, 30),
                new TimeOnly(7, 0),
                exception.Title,
                exception.Menu,
                exception.PrepNotes,
                exception.Location,
                exception.ParticipantCount,
                exception.RowVersion
            )
        );
        Assert.Equal(HttpStatusCode.OK, moved.StatusCode);
        var movedEntry = (await moved.Content.ReadFromJsonAsync<ScheduleEntryDto>())!;
        Assert.True(movedEntry.TimesCustomized);

        // Now shift the group's breakfast for the whole stay.
        var breakfast = (await GetMealTimesAsync(admin, bookingId)).Single(m =>
            m.MealKind == "Breakfast"
        );
        var response = await admin.PutAsJsonAsync(
            $"/api/admin/schedule/bookings/{bookingId}/meal-times/{breakfast.MealTimeDefaultId}",
            new SetBookingMealTimeRequestDto(
                new TimeOnly(9, 0),
                new TimeOnly(9, 45),
                ApplyToExisting: true,
                breakfast.RowVersion
            )
        );
        var result = (await response.Content.ReadFromJsonAsync<ApplyBookingMealTimeResultDto>())!;

        Assert.Equal(1, result.SkippedCustomized);

        var breakfasts = await BreakfastsAsync(admin, bookingId);
        var kept = Assert.Single(breakfasts, e => e.Id == exception.Id);
        Assert.Equal(new TimeOnly(6, 30), kept.StartTime);
        Assert.All(
            breakfasts.Where(e => e.Id != exception.Id),
            e => Assert.Equal(new TimeOnly(9, 0), e.StartTime)
        );
    }

    [Fact]
    public async Task GenerationUsesTheGroupsOwnTimes_ForMealsAddedLater()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (bookingId, _, _, _) = await CreateBookingAsync(admin);

        // Set the group's breakfast before any meals exist, then generate.
        var breakfast = (await GetMealTimesAsync(admin, bookingId)).Single(m =>
            m.MealKind == "Breakfast"
        );
        await admin.PutAsJsonAsync(
            $"/api/admin/schedule/bookings/{bookingId}/meal-times/{breakfast.MealTimeDefaultId}",
            new SetBookingMealTimeRequestDto(
                new TimeOnly(9, 15),
                new TimeOnly(10, 0),
                ApplyToExisting: false,
                breakfast.RowVersion
            )
        );

        await GenerateMealsAsync(admin, bookingId);

        var breakfasts = await BreakfastsAsync(admin, bookingId);
        Assert.NotEmpty(breakfasts);
        Assert.All(breakfasts, e => Assert.Equal(new TimeOnly(9, 15), e.StartTime));
    }

    [Fact]
    public async Task ResettingAGroupsMealTime_RestoresTheCenterTime()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (bookingId, _, _, _) = await CreateBookingAsync(admin);
        await GenerateMealsAsync(admin, bookingId);

        var breakfast = (await GetMealTimesAsync(admin, bookingId)).Single(m =>
            m.MealKind == "Breakfast"
        );
        await admin.PutAsJsonAsync(
            $"/api/admin/schedule/bookings/{bookingId}/meal-times/{breakfast.MealTimeDefaultId}",
            new SetBookingMealTimeRequestDto(
                new TimeOnly(9, 30),
                new TimeOnly(10, 15),
                ApplyToExisting: true,
                breakfast.RowVersion
            )
        );

        var reset = await admin.DeleteAsync(
            $"/api/admin/schedule/bookings/{bookingId}/meal-times/{breakfast.MealTimeDefaultId}?applyToExisting=true"
        );
        Assert.Equal(HttpStatusCode.OK, reset.StatusCode);
        var result = (await reset.Content.ReadFromJsonAsync<ApplyBookingMealTimeResultDto>())!;
        Assert.False(result.MealTime.IsOverridden);

        var breakfasts = await BreakfastsAsync(admin, bookingId);
        Assert.All(breakfasts, e => Assert.Equal(breakfast.DefaultStartTime, e.StartTime));

        var after = (await GetMealTimesAsync(admin, bookingId)).Single(m =>
            m.MealKind == "Breakfast"
        );
        Assert.False(after.IsOverridden);
    }

    [Fact]
    public async Task SettingAGroupsMealTime_RejectsEndBeforeStart_AndStaleRowVersion()
    {
        var admin = await CreateAuthenticatedClientAsync();
        var (bookingId, _, _, _) = await CreateBookingAsync(admin);
        var breakfast = (await GetMealTimesAsync(admin, bookingId)).Single(m =>
            m.MealKind == "Breakfast"
        );
        var url =
            $"/api/admin/schedule/bookings/{bookingId}/meal-times/{breakfast.MealTimeDefaultId}";

        var backwards = await admin.PutAsJsonAsync(
            url,
            new SetBookingMealTimeRequestDto(
                new TimeOnly(10, 0),
                new TimeOnly(9, 0),
                ApplyToExisting: false,
                breakfast.RowVersion
            )
        );
        Assert.Equal(HttpStatusCode.BadRequest, backwards.StatusCode);

        // First write creates the override row (RowVersion 0 means "none yet").
        var created = await admin.PutAsJsonAsync(
            url,
            new SetBookingMealTimeRequestDto(
                new TimeOnly(9, 0),
                new TimeOnly(9, 30),
                ApplyToExisting: false,
                breakfast.RowVersion
            )
        );
        Assert.Equal(HttpStatusCode.OK, created.StatusCode);
        var saved = (await created.Content.ReadFromJsonAsync<ApplyBookingMealTimeResultDto>())!;

        // Re-using that same RowVersion after another change must now conflict.
        await admin.PutAsJsonAsync(
            url,
            new SetBookingMealTimeRequestDto(
                new TimeOnly(9, 5),
                new TimeOnly(9, 35),
                ApplyToExisting: false,
                saved.MealTime.RowVersion
            )
        );
        var stale = await admin.PutAsJsonAsync(
            url,
            new SetBookingMealTimeRequestDto(
                new TimeOnly(9, 10),
                new TimeOnly(9, 40),
                ApplyToExisting: false,
                saved.MealTime.RowVersion
            )
        );
        Assert.Equal(HttpStatusCode.Conflict, stale.StatusCode);
    }

    // The webhook-triggered generation path is covered by
    // PaymentsApiTests.DepositWebhook_GeneratesTheGroupsMeals — it lives there so
    // this class needs no overridden host of its own.

    // --- helpers ----------------------------------------------------------

    private static async Task<List<BookingMealTimeDto>> GetMealTimesAsync(
        HttpClient admin,
        Guid bookingId
    ) =>
        (
            await admin.GetFromJsonAsync<List<BookingMealTimeDto>>(
                $"/api/admin/schedule/bookings/{bookingId}/meal-times"
            )
        )!;

    private static async Task GenerateMealsAsync(HttpClient admin, Guid bookingId)
    {
        var response = await admin.PostAsync(
            $"/api/admin/schedule/bookings/{bookingId}/generate-meals",
            null
        );
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    private static async Task<List<ScheduleEntryDto>> BreakfastsAsync(
        HttpClient admin,
        Guid bookingId
    )
    {
        var schedule = (
            await admin.GetFromJsonAsync<BookingScheduleDto>(
                $"/api/admin/schedule/bookings/{bookingId}"
            )
        )!;
        return [.. schedule.Days.SelectMany(d => d.Entries).Where(e => e.MealKind == "Breakfast")];
    }

    private static CreateScheduleEntryRequestDto EntryOn(
        Guid bookingId,
        DateOnly date,
        string title
    ) =>
        new(
            bookingId,
            "Activity",
            null,
            date,
            new TimeOnly(9, 0),
            new TimeOnly(10, 0),
            title,
            null,
            null,
            null,
            null
        );

    /// A confirmed-shape booking in its own date window with its own room, so the
    /// shared inventory never causes cross-test interference.
    private async Task<(
        Guid BookingId,
        string Token,
        DateOnly Start,
        DateOnly End
    )> CreateBookingAsync(HttpClient admin)
    {
        var offset = Interlocked.Increment(ref _windowOffset) * 30;
        var suffix = Guid.NewGuid().ToString("N")[..6];

        await admin.PostAsJsonAsync(
            "/api/admin/rooms",
            new CreateRoomRequestDto($"SCH-{suffix}", 12, null)
        );

        var start = new DateOnly(2034, 1, 1).AddDays(offset);
        var end = start.AddDays(5); // 5 nights → 6 schedule days

        var create = await CreateClient()
            .PostAsJsonAsync(
                "/api/public/bookings",
                new CreateBookingRequestDto(
                    start,
                    end,
                    12,
                    0,
                    new Dictionary<int, int> { [12] = 1 },
                    [],
                    $"Grupa {suffix}",
                    "Anna Opiekun",
                    $"sch-{suffix}@example.com",
                    "+48 600 100 200",
                    null,
                    "pl"
                )
            );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var booking = (await create.Content.ReadFromJsonAsync<CreateBookingResponseDto>())!;

        return (booking.BookingId, booking.ManageToken, start, end);
    }
}
