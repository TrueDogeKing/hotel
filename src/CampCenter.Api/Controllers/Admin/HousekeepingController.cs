using CampCenter.Api.Extensions;
using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Admin;

/// The housekeeping round: which rooms have to be cleaned on a given day because a group
/// is leaving them, a group is moving in, or both — and how far the work has got.
///
/// The list itself is derived from the room assignments on every read, so there is no
/// endpoint that creates jobs; only progress against a room is written.
[ApiController]
[Authorize]
[Route("api/admin/housekeeping")]
public class HousekeepingController : ControllerBase
{
    private readonly IHousekeepingService _housekeeping;

    public HousekeepingController(IHousekeepingService housekeeping) =>
        _housekeeping = housekeeping;

    /// One day's rooms, turnarounds first.
    [HttpGet("day/{date}")]
    [ProducesResponseType(typeof(HousekeepingDayDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDay(DateOnly date, CancellationToken cancellationToken) =>
        Ok(await _housekeeping.GetDayAsync(date, cancellationToken));

    /// Rooms-to-do and rooms-done per day, for the day strip above the list.
    [HttpGet("range")]
    [ProducesResponseType(typeof(HousekeepingRangeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetRange(
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to,
        CancellationToken cancellationToken
    ) => Ok(await _housekeeping.GetRangeAsync(from, to, cancellationToken));

    /// Marks a room pending, in progress or done for that day, with an optional note.
    /// 400 when the room has no arrival or departure on the day — a page left open while
    /// the booking moved.
    [HttpPut("day/{date}/rooms/{roomId:guid}")]
    [ProducesResponseType(typeof(HousekeepingRoomDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> SetStatus(
        DateOnly date,
        Guid roomId,
        [FromBody] SetRoomCleaningRequestDto request,
        CancellationToken cancellationToken
    ) =>
        Ok(
            await _housekeeping.SetStatusAsync(
                roomId,
                date,
                request,
                User.GetUserId(),
                cancellationToken
            )
        );
}
