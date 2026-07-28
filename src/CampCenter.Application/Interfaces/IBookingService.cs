using CampCenter.Application.DTOs.Public;
using CampCenter.Application.DTOs.Schedule;

namespace CampCenter.Application.Interfaces;

public interface IBookingService
{
    /// Creates a booking with auto-assigned concrete rooms and emails the manage
    /// link. Retries once when a concurrent booking grabs a selected room.
    Task<CreateBookingResponseDto> CreateAsync(
        CreateBookingRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Booking details for the manage page; token is the plaintext manage token.
    Task<BookingDetailsDto> GetByTokenAsync(
        string token,
        CancellationToken cancellationToken = default
    );

    /// The group's own read-only camp programme. Kitchen prep notes are never
    /// included — see PublicScheduleEntryDto.
    Task<PublicScheduleDto> GetScheduleByTokenAsync(
        string token,
        CancellationToken cancellationToken = default
    );

    /// Booker-initiated cancel; allowed only while the deposit is unpaid.
    Task CancelByTokenAsync(string token, CancellationToken cancellationToken = default);
}
