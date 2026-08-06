using CampCenter.Api.RateLimiting;
using CampCenter.Application.DTOs.Public;
using CampCenter.Application.DTOs.Schedule;
using CampCenter.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CampCenter.Api.Controllers.Public;

/// Public booking endpoints. No accounts: a booking is created anonymously and
/// then managed via the secret token from the confirmation email.
[ApiController]
[Route("api/public/bookings")]
public class PublicBookingsController : ControllerBase
{
    private readonly IBookingService _bookings;
    private readonly IValidator<CreateBookingRequestDto> _createValidator;

    public PublicBookingsController(
        IBookingService bookings,
        IValidator<CreateBookingRequestDto> createValidator
    )
    {
        _bookings = bookings;
        _createValidator = createValidator;
    }

    [HttpPost]
    [EnableRateLimiting(RateLimitPolicies.PublicBooking)]
    [ProducesResponseType(typeof(CreateBookingResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> Create(
        [FromBody] CreateBookingRequestDto request,
        CancellationToken cancellationToken
    )
    {
        var validation = await _createValidator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }

        var result = await _bookings.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetByToken), new { token = result.ManageToken }, result);
    }

    /// Booking details for the manage page (link from the confirmation email).
    [HttpGet("{token}")]
    [EnableRateLimiting(RateLimitPolicies.PublicBooking)]
    [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByToken(
        string token,
        CancellationToken cancellationToken
    ) => Ok(await _bookings.GetByTokenAsync(token, cancellationToken));

    /// The group's own camp programme — meals (with menus) and activities, day by
    /// day. Internal kitchen prep notes are never included.
    [HttpGet("{token}/schedule")]
    [EnableRateLimiting(RateLimitPolicies.PublicBooking)]
    [ProducesResponseType(typeof(PublicScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetScheduleByToken(
        string token,
        CancellationToken cancellationToken
    ) => Ok(await _bookings.GetScheduleByTokenAsync(token, cancellationToken));

    // Online payment is switched off: groups pay the centre directly and the
    // owner records it in the panel (Booking.PaymentState). The Przelewy24 code
    // is left in the tree, unregistered, for whenever card payment comes back —
    // see PublicPaymentsController and Infrastructure/Payments.
    //
    // /// Starts an online payment (deposit or final) — returns the P24 redirect URL.
    // [HttpPost("{token}/payments")]
    // [EnableRateLimiting(RateLimitPolicies.PublicBooking)]
    // [ProducesResponseType(typeof(InitiatePaymentResponseDto), StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [ProducesResponseType(StatusCodes.Status409Conflict)]
    // public async Task<IActionResult> InitiatePayment(
    //     string token,
    //     [FromBody] InitiatePaymentRequestDto request,
    //     [FromServices] IPaymentService payments,
    //     CancellationToken cancellationToken
    // ) => Ok(await payments.InitiateAsync(token, request, cancellationToken));

    [HttpPost("{token}/cancel")]
    [EnableRateLimiting(RateLimitPolicies.PublicBooking)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel(string token, CancellationToken cancellationToken)
    {
        await _bookings.CancelByTokenAsync(token, cancellationToken);
        return NoContent();
    }
}
