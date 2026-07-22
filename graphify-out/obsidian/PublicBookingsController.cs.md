---
source_file: "src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs"
type: "code"
community: "Application Namespaces & DTOs"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_Namespaces__DTOs
---

# PublicBookingsController.cs

## Context

_Source: `src/CampCenter.Api/Controllers/Public/PublicBookingsController.cs` (defined near L1; showing L1–L46 of 88)._

```csharp
using CampCenter.Api.RateLimiting;
using CampCenter.Application.DTOs.Public;
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

```

## Connections
- [[CampCenter.Api.Controllers.Public]] - `contains` [EXTRACTED]
- [[CampCenter.Api.RateLimiting]] - `imports` [EXTRACTED]
- [[CampCenter.Application.DTOs.Public]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[PublicBookingsController]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_Namespaces__DTOs