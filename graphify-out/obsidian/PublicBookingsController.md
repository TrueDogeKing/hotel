---
source_file: "src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs"
type: "code"
community: "Public Booking Service"
location: "L12"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Service
---

# PublicBookingsController

## Context

_Source: `src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs` (defined near L12; showing L10–L57 of 88)._

```csharp
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
```

## Connections
- [[.Cancel()_1]] - `method` [EXTRACTED]
- [[.Create()_3]] - `method` [EXTRACTED]
- [[.GetByToken()]] - `method` [EXTRACTED]
- [[.InitiatePayment()]] - `method` [EXTRACTED]
- [[ControllerBase]] - `inherits` [EXTRACTED]
- [[IBookingService]] - `references` [EXTRACTED]
- [[IValidator_3]] - `references` [EXTRACTED]
- [[PublicBookingsController.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Service