---
source_file: "src/CampCenter.Application/Services/PaymentService.cs"
type: "code"
community: "Auth DTOs & Models"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_DTOs__Models
---

# PaymentService.cs

## Context

_Source: `src/CampCenter.Application/Services/PaymentService.cs` (defined near L1; showing L1–L46 of 233)._

```csharp
using CampCenter.Application.Interfaces;
using CampCenter.Application.Models;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CampCenter.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IBookingRepository _bookings;
    private readonly IPaymentGateway _gateway;
    private readonly ITokenService _tokenService;
    private readonly IEmailSender _email;
    private readonly BookingSettings _settings;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(
        IBookingRepository bookings,
        IPaymentGateway gateway,
        ITokenService tokenService,
        IEmailSender email,
        IOptions<BookingSettings> settings,
        ILogger<PaymentService> logger
    )
    {
        _bookings = bookings;
        _gateway = gateway;
        _tokenService = tokenService;
        _email = email;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<InitiatePaymentResponseDto> InitiateAsync(
        string manageToken,
        InitiatePaymentRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var booking =
            await _bookings.GetByTokenHashAsync(
                _tokenService.HashRefreshToken(manageToken),
                cancellationToken
```

## Connections
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Models]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Services]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Exceptions]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Repositories]] - `imports` [EXTRACTED]
- [[PaymentService]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_DTOs__Models