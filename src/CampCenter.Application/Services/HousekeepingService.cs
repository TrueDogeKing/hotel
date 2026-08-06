using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;

namespace CampCenter.Application.Services;

/// The housekeeping list: which rooms need cleaning on a day, and how far the work has
/// got. The "which" is derived from the room assignments on every read (see
/// HousekeepingPlanner); only the progress is stored.
public class HousekeepingService : IHousekeepingService
{
    private readonly IBookingRepository _bookings;
    private readonly IRoomRepository _rooms;
    private readonly IRoomCleaningRepository _cleanings;
    private readonly IRoomTaskRepository _tasks;
    private readonly IClosureRepository _closures;

    public HousekeepingService(
        IBookingRepository bookings,
        IRoomRepository rooms,
        IRoomCleaningRepository cleanings,
        IRoomTaskRepository tasks,
        IClosureRepository closures
    )
    {
        _bookings = bookings;
        _rooms = rooms;
        _cleanings = cleanings;
        _tasks = tasks;
        _closures = closures;
    }

    /// Turnarounds first: a room that has to be emptied, cleaned and remade before the
    /// next group walks in is the one that will go wrong if it is left till last.
    private static int KindOrder(RoomCleaningKind kind) =>
        kind switch
        {
            RoomCleaningKind.Turnaround => 0,
            RoomCleaningKind.Departure => 1,
            _ => 2,
        };

    public async Task<HousekeepingDayDto> GetDayAsync(
        DateOnly date,
        CancellationToken cancellationToken = default
    )
    {
        var jobs = await GetJobsAsync(date, cancellationToken);

        var rooms = (await _rooms.GetAllAsync(cancellationToken)).ToDictionary(r => r.Id);
        var progress = (await _cleanings.ListForDateAsync(date, cancellationToken)).ToDictionary(
            c => c.RoomId
        );
        var openTasks = await _tasks.CountOpenByRoomAsync(cancellationToken);

        // A closure covering the day is worth showing: the room may be out of use for a
        // burst pipe, which changes what the person walking over there should expect.
        var closures = await _closures.GetOverlappingAsync(
            date,
            date.AddDays(1),
            cancellationToken
        );
        var centerClosure = closures.FirstOrDefault(c => c.RoomId is null);
        var roomClosures = closures
            .Where(c => c.RoomId is not null)
            .GroupBy(c => c.RoomId!.Value)
            .ToDictionary(g => g.Key, g => g.First().Reason);

        var dtos = jobs.Where(job => rooms.ContainsKey(job.RoomId))
            .Select(job =>
            {
                var room = rooms[job.RoomId];
                progress.TryGetValue(job.RoomId, out var cleaning);
                var closureReason =
                    centerClosure?.Reason ?? roomClosures.GetValueOrDefault(room.Id);

                return new HousekeepingRoomDto(
                    room.Id,
                    room.Number,
                    room.Capacity,
                    job.Kind.ToString(),
                    (cleaning?.Status ?? RoomCleaningStatus.Pending).ToString(),
                    job.Outgoing?.Id,
                    job.Outgoing?.OrganizationName,
                    job.OutgoingAssignment?.PeopleCount,
                    job.Incoming?.Id,
                    job.Incoming?.OrganizationName,
                    job.IncomingAssignment?.PeopleCount,
                    cleaning?.Note,
                    cleaning?.DoneAt,
                    openTasks.GetValueOrDefault(room.Id),
                    closureReason is not null,
                    closureReason,
                    cleaning?.RowVersion ?? 0
                );
            })
            .OrderBy(dto => KindOrder(Enum.Parse<RoomCleaningKind>(dto.Kind)))
            .ThenBy(dto => dto.RoomNumber, StringComparer.CurrentCulture)
            .ToList();

        return new HousekeepingDayDto(
            date,
            dtos,
            dtos.Count(d => d.Kind == nameof(RoomCleaningKind.Turnaround)),
            dtos.Count(d => d.Kind == nameof(RoomCleaningKind.Departure)),
            dtos.Count(d => d.Kind == nameof(RoomCleaningKind.Arrival)),
            dtos.Count(d => d.Status == nameof(RoomCleaningStatus.Done))
        );
    }

    public async Task<HousekeepingRangeDto> GetRangeAsync(
        DateOnly from,
        DateOnly to,
        CancellationToken cancellationToken = default
    )
    {
        if (to < from)
        {
            throw new BusinessRuleViolationException("End date cannot be before the start date.");
        }

        // One query for the whole window rather than one per day: the strip spans a week
        // or two, and the changeovers in it all come from the same set of bookings.
        var bookings = await _bookings.ListLiveChangingOverAsync(from, to, cancellationToken);
        var doneByDate = await _cleanings.CountDoneByDateAsync(from, to, cancellationToken);

        var days = new List<HousekeepingDaySummaryDto>();
        for (var date = from; date <= to; date = date.AddDays(1))
        {
            days.Add(
                new HousekeepingDaySummaryDto(
                    date,
                    HousekeepingPlanner.ForDay(bookings, date).Count,
                    doneByDate.GetValueOrDefault(date)
                )
            );
        }

        return new HousekeepingRangeDto(from, to, days);
    }

    public async Task<HousekeepingRoomDto> SetStatusAsync(
        Guid roomId,
        DateOnly date,
        SetRoomCleaningRequestDto request,
        Guid adminUserId,
        CancellationToken cancellationToken = default
    )
    {
        var status = ParseStatus(request.Status);
        var note = string.IsNullOrWhiteSpace(request.Note) ? null : request.Note.Trim();
        if (note?.Length > 1000)
        {
            throw new BusinessRuleViolationException("The note must be at most 1000 characters.");
        }

        // The job has to still exist. Without this check a page left open overnight could
        // mark a room clean for a booking that has since been moved or cancelled — and
        // because the list is derived, that row would then never be seen again.
        var job =
            (await GetJobsAsync(date, cancellationToken)).FirstOrDefault(j => j.RoomId == roomId)
            ?? throw new BusinessRuleViolationException(
                "That room has no arrival or departure on this day."
            );

        var cleaning = await _cleanings.GetAsync(roomId, date, cancellationToken);
        if (cleaning is null)
        {
            cleaning = new RoomCleaning
            {
                Id = Guid.NewGuid(),
                RoomId = roomId,
                Date = date,
                CreatedAt = DateTime.UtcNow,
            };
            await _cleanings.AddAsync(cleaning, cancellationToken);
        }
        else
        {
            cleaning.UpdatedAt = DateTime.UtcNow;
        }

        cleaning.Kind = job.Kind;
        cleaning.Status = status;
        cleaning.Note = note;
        // Cleared when a room is un-ticked, so "cleaned today" never counts a room that
        // was reopened afterwards.
        cleaning.DoneAt = status == RoomCleaningStatus.Done ? DateTime.UtcNow : null;
        cleaning.DoneByAdminUserId = status == RoomCleaningStatus.Done ? adminUserId : null;

        await _cleanings.SaveChangesAsync(cancellationToken);

        // Read the day back rather than mapping the row by hand: the response carries the
        // room's groups and task count too, and there is exactly one place that assembles
        // those.
        var day = await GetDayAsync(date, cancellationToken);
        return day.Rooms.First(r => r.RoomId == roomId);
    }

    private async Task<List<HousekeepingJob>> GetJobsAsync(
        DateOnly date,
        CancellationToken cancellationToken
    ) =>
        HousekeepingPlanner.ForDay(
            await _bookings.ListLiveChangingOverAsync(date, date, cancellationToken),
            date
        );

    private static RoomCleaningStatus ParseStatus(string status) =>
        Enum.TryParse<RoomCleaningStatus>(status, ignoreCase: true, out var parsed)
            ? parsed
            : throw new BusinessRuleViolationException(
                $"Unknown cleaning status '{status}'. Use Pending, InProgress or Done."
            );
}
