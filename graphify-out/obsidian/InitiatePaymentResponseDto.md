---
source_file: "src/CampCenter.Application/Interfaces/IPaymentService.cs"
type: "code"
community: "Public Booking Service"
location: "L7"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Service
---

# InitiatePaymentResponseDto

## Context

_Source: `src/CampCenter.Application/Interfaces/IPaymentService.cs` (defined near L7; showing L5–L26 of 26)._

```csharp
public record InitiatePaymentRequestDto(string Kind);

public record InitiatePaymentResponseDto(string RedirectUrl);

public interface IPaymentService
{
    /// Creates a Pending payment for the booking (amount computed server-side)
    /// and registers a P24 transaction. Returns the hosted-payment redirect URL.
    Task<InitiatePaymentResponseDto> InitiateAsync(
        string manageToken,
        InitiatePaymentRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Handles the P24 status webhook: verifies the signature and the amount,
    /// calls transaction/verify, marks the payment Completed and advances the
    /// booking. Idempotent — an already-completed payment is acknowledged.
    Task HandleNotificationAsync(
        GatewayNotification notification,
        CancellationToken cancellationToken = default
    );
}
```

## Connections
- [[.InitiateAsync()]] - `references` [EXTRACTED]
- [[.InitiateAsync()_1]] - `references` [EXTRACTED]
- [[IPaymentService.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Service