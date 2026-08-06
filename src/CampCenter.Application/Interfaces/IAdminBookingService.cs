using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.DTOs.Schedule;
using CampCenter.Domain.Entities;

namespace CampCenter.Application.Interfaces;

public interface IAdminBookingService
{
    Task<List<AdminBookingDto>> ListAsync(
        BookingStatus? status,
        CancellationToken cancellationToken = default
    );

    Task<AdminBookingDto> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// Creates a group entered by staff. Rooms are picked automatically to fit the
    /// headcount, pricing is snapshotted the same way as a public booking, and no
    /// confirmation email is sent. Throws ConflictException when the free rooms
    /// cannot house the group over the requested range.
    Task<AdminBookingDto> CreateAsync(
        CreateAdminBookingRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Admin cancel of any live booking (refunds are handled manually outside the system).
    Task CancelAsync(Guid id, CancellationToken cancellationToken = default);

    /// Manual status override, allowed between any two statuses.
    ///
    /// Cancelling releases the rooms and emails the group, exactly as CancelAsync
    /// does. Moving *out* of Cancelled has to take the rooms back, so it re-runs
    /// assignment from the booking's requested room mix and throws
    /// ConflictException when those rooms have since gone to someone else.
    /// PendingDeposit re-arms the deposit hold; every other status clears it.
    Task<AdminBookingDto> SetStatusAsync(
        Guid id,
        SetBookingStatusRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// The rooms this booking may occupy over its stay: its own, plus every other
    /// active room free of bookings and closures for the whole range. Drawn from the
    /// same availability check ReassignAsync enforces, so the panel offering a move
    /// cannot list a room the save would then reject.
    Task<List<AssignableRoomDto>> GetAssignableRoomsAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    /// Replaces the booking's room assignments. Admin override: people counts may
    /// exceed room capacity (extra beds are a housekeeping task).
    Task<AdminBookingDto> ReassignAsync(
        Guid id,
        ReassignBookingRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Changes what this one group is charged. The rate and deposit are stored as
    /// given; the total is recomputed as rate × headcount × nights unless the
    /// request carries a flat total of its own. Other bookings are untouched — the
    /// centre-wide rates live in IPricingService.
    Task<AdminBookingDto> UpdatePricingAsync(
        Guid id,
        UpdateBookingPricingRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// The panel's single state control: sets status and payment together.
    ///
    /// AwaitingPayment / DepositPaid / Paid record what has been paid on a live
    /// booking (and confirm one waiting on its deposit); Cancelled and Completed
    /// are status moves, and go down exactly the same paths as SetStatusAsync —
    /// cancelling frees the rooms and emails the group.
    Task<AdminBookingDto> SetStateAsync(
        Guid id,
        SetBookingStateRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Records what the owner has been paid. Marking a deposit (or the full amount)
    /// received confirms a booking still waiting on one, exactly as an online
    /// deposit payment used to: the room hold stops expiring.
    Task<AdminBookingDto> SetPaymentStateAsync(
        Guid id,
        SetBookingPaymentStateRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Sets the kitchen-facing dietary/preparation note for a group.
    Task<AdminBookingDto> UpdateDietaryNotesAsync(
        Guid id,
        UpdateDietaryNotesRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Per-room occupancy over an arbitrary date range: each room free, booked, or
    /// blocked by a closure.
    Task<OccupancyDto> GetOccupancyAsync(
        DateOnly start,
        DateOnly end,
        CancellationToken cancellationToken = default
    );

    Task<DashboardDto> GetDashboardAsync(CancellationToken cancellationToken = default);

    /// One page of the groups in a category (current / upcoming / inactive), for the
    /// dashboard's foldable lists. Each fold asks for its own first page when it is
    /// opened and for further pages as they are scrolled to, so the page never loads
    /// a history it is not showing. `take` is clamped to a sane maximum.
    Task<BookingGroupPageDto> GetGroupPageAsync(
        BookingGroupCategory category,
        int skip,
        int take,
        CancellationToken cancellationToken = default
    );
}
