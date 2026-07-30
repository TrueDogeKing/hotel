using CampCenter.Application.DTOs.Schedule;
using CampCenter.Application.Models;
using CampCenter.Application.Services;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Repositories;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace CampCenter.UnitTests.Services;

/// The advisory check behind "that place is already taken" and "another group eats
/// then". It never blocks a save, so what matters is that it flags exactly the cases
/// an admin would want to see — and stays quiet otherwise.
public class ScheduleConflictTests
{
    private static readonly DateOnly Day = new(2026, 7, 20);
    private static readonly Guid OurGroup = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid OtherGroup = Guid.Parse("22222222-2222-2222-2222-222222222222");

    private readonly IScheduleEntryRepository _entries = Substitute.For<IScheduleEntryRepository>();
    private readonly ScheduleService _service;

    public ScheduleConflictTests() =>
        _service = new ScheduleService(
            _entries,
            Substitute.For<IMealTimeDefaultRepository>(),
            Substitute.For<IBookingMealTimeRepository>(),
            Substitute.For<IBookingRepository>(),
            Options.Create(new ScheduleSettings())
        );

    private void DayHas(params ScheduleEntry[] entries) =>
        _entries.ListForDateAsync(Day, Arg.Any<CancellationToken>()).Returns([.. entries]);

    private static ScheduleEntry Entry(
        Guid bookingId,
        ScheduleEntryKind kind,
        string start,
        string end,
        string? location = null
    ) =>
        new()
        {
            Id = Guid.NewGuid(),
            BookingId = bookingId,
            Booking = new Booking
            {
                Id = bookingId,
                OrganizationName = "Inna grupa",
                ContactName = "X",
                Email = "x@example.com",
                Phone = "123",
                Language = "pl",
                ManageTokenHash = "hash",
                RequestedRoomCounts = "{}",
            },
            Kind = kind,
            MealKind = kind == ScheduleEntryKind.Meal ? MealKind.Lunch : null,
            Date = Day,
            StartTime = TimeOnly.Parse(start),
            EndTime = TimeOnly.Parse(end),
            Title = kind == ScheduleEntryKind.Meal ? "Obiad" : "Zajęcia",
            Location = location,
        };

    private Task<ScheduleConflictsDto> CheckAsync(
        ScheduleEntryKind kind,
        string start,
        string end,
        string? location = null,
        Guid? entryId = null
    ) =>
        _service.CheckConflictsAsync(
            new CheckScheduleConflictsRequestDto(
                OurGroup,
                entryId,
                kind.ToString(),
                Day,
                TimeOnly.Parse(start),
                TimeOnly.Parse(end),
                location
            )
        );

    [Fact]
    public async Task Flags_another_group_in_the_same_place()
    {
        DayHas(Entry(OtherGroup, ScheduleEntryKind.Activity, "10:00", "11:00", "Sala A"));

        var result = await CheckAsync(ScheduleEntryKind.Activity, "10:30", "12:00", "Sala A");

        var conflict = Assert.Single(result.Conflicts);
        Assert.Equal("Location", conflict.Reason);
        Assert.Equal(OtherGroup, conflict.BookingId);
    }

    [Fact]
    public async Task Matches_places_case_insensitively()
    {
        DayHas(Entry(OtherGroup, ScheduleEntryKind.Activity, "10:00", "11:00", "Sala A"));

        var result = await CheckAsync(ScheduleEntryKind.Activity, "10:30", "12:00", " sala a ");

        Assert.Single(result.Conflicts);
    }

    [Fact]
    public async Task Ignores_a_different_place_at_the_same_time()
    {
        DayHas(Entry(OtherGroup, ScheduleEntryKind.Activity, "10:00", "11:00", "Sala A"));

        var result = await CheckAsync(ScheduleEntryKind.Activity, "10:00", "11:00", "Boisko");

        Assert.Empty(result.Conflicts);
    }

    [Fact]
    public async Task Ignores_the_place_being_free_before_the_activity_starts()
    {
        DayHas(Entry(OtherGroup, ScheduleEntryKind.Activity, "10:00", "11:00", "Sala A"));

        // Back to back is fine for a room: nothing has to be cleared between groups.
        var result = await CheckAsync(ScheduleEntryKind.Activity, "11:00", "12:00", "Sala A");

        Assert.Empty(result.Conflicts);
    }

    [Fact]
    public async Task Ignores_the_group_own_programme()
    {
        DayHas(Entry(OurGroup, ScheduleEntryKind.Activity, "10:00", "11:00", "Sala A"));

        var result = await CheckAsync(ScheduleEntryKind.Activity, "10:30", "12:00", "Sala A");

        Assert.Empty(result.Conflicts);
    }

    [Fact]
    public async Task Flags_a_meal_while_another_group_is_at_the_tables()
    {
        DayHas(Entry(OtherGroup, ScheduleEntryKind.Meal, "13:00", "14:00", "Stołówka"));

        var result = await CheckAsync(ScheduleEntryKind.Meal, "13:30", "14:30");

        // No place given, so the complaint is the sitting itself.
        Assert.Equal("Meal", Assert.Single(result.Conflicts).Reason);
    }

    [Fact]
    public async Task Flags_a_sitting_that_leaves_no_changeover()
    {
        DayHas(Entry(OtherGroup, ScheduleEntryKind.Meal, "13:00", "14:00", "Stołówka"));

        // 14:00 + 15 min changeover — 14:10 is still too early.
        var result = await CheckAsync(ScheduleEntryKind.Meal, "14:10", "15:00", "Stołówka");

        Assert.Single(result.Conflicts);
        Assert.Equal(15, result.MealGapMinutes);
    }

    [Fact]
    public async Task Accepts_a_sitting_after_the_changeover()
    {
        DayHas(Entry(OtherGroup, ScheduleEntryKind.Meal, "13:00", "14:00", "Stołówka"));

        var result = await CheckAsync(ScheduleEntryKind.Meal, "14:15", "15:15", "Stołówka");

        Assert.Empty(result.Conflicts);
    }

    [Fact]
    public async Task Reports_the_place_rather_than_the_meal_when_both_hold()
    {
        DayHas(Entry(OtherGroup, ScheduleEntryKind.Meal, "13:00", "14:00", "Stołówka"));

        var result = await CheckAsync(ScheduleEntryKind.Meal, "13:30", "14:30", "Stołówka");

        Assert.Equal("Location", Assert.Single(result.Conflicts).Reason);
    }

    [Fact]
    public async Task Does_not_flag_an_activity_against_a_meal_elsewhere()
    {
        DayHas(Entry(OtherGroup, ScheduleEntryKind.Meal, "13:00", "14:00", "Stołówka"));

        var result = await CheckAsync(ScheduleEntryKind.Activity, "13:00", "14:00", "Boisko");

        Assert.Empty(result.Conflicts);
    }

    [Fact]
    public async Task Does_not_flag_the_entry_being_edited_against_itself()
    {
        var existing = Entry(OtherGroup, ScheduleEntryKind.Activity, "10:00", "11:00", "Sala A");
        DayHas(existing);

        var result = await CheckAsync(
            ScheduleEntryKind.Activity,
            "10:00",
            "11:30",
            "Sala A",
            existing.Id
        );

        Assert.Empty(result.Conflicts);
    }

    [Fact]
    public async Task Late_evening_sittings_do_not_wrap_past_midnight()
    {
        DayHas(Entry(OtherGroup, ScheduleEntryKind.Meal, "22:00", "23:00", "Stołówka"));

        // 23:00 + 15 min changeover must stay in the evening, not become 00:15 and
        // silently let a 23:05 sitting through.
        var result = await CheckAsync(ScheduleEntryKind.Meal, "23:05", "23:45", "Stołówka");

        Assert.Single(result.Conflicts);
    }
}
