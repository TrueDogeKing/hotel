---
source_file: "src/CampCenter.Application/Services/PaymentService.cs"
type: "code"
community: "Admin Booking & Notifications"
location: "L11"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Booking__Notifications
---

# PaymentService

## Context

_Source: `src/CampCenter.Application/Services/PaymentService.cs` (defined near L11; showing L9–L56 of 233)._

```csharp
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
            ) ?? throw new NotFoundException("Booking not found.");

        var kind = request.Kind switch
        {
            "Deposit" => PaymentKind.Deposit,
            "Final" => PaymentKind.Final,
            _ => throw new BusinessRuleViolationException("Unknown payment kind."),
        };

        var completedKinds = (await _bookings.GetPaymentsAsync(booking.Id, cancellationToken))
```

## Connections
- [[.HandleNotificationAsync()_1]] - `method` [EXTRACTED]
- [[.InitiateAsync()_1]] - `method` [EXTRACTED]
- [[.SendSafelyAsync()_1]] - `method` [EXTRACTED]
- [[BookingSettings]] - `references` [EXTRACTED]
- [[IBookingRepository]] - `references` [EXTRACTED]
- [[IEmailSender]] - `references` [EXTRACTED]
- [[ILogger_4]] - `references` [EXTRACTED]
- [[IPaymentGateway]] - `references` [EXTRACTED]
- [[IPaymentService]] - `implements` [EXTRACTED]
- [[ITokenService]] - `references` [EXTRACTED]
- [[PaymentService.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Booking__Notifications