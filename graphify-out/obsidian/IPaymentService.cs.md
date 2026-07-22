---
source_file: "src/CampCenter.Application/Interfaces/IPaymentService.cs"
type: "code"
community: "Public Booking Service"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Service
---

# IPaymentService.cs

## Context

_Source: `src/CampCenter.Application/Interfaces/IPaymentService.cs` (defined near L1; showing L1–L26 of 26)._

```csharp
using CampCenter.Application.DTOs.Public;

namespace CampCenter.Application.Interfaces;

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
- [[CampCenter.Application.DTOs.Public]] - `imports` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `contains` [EXTRACTED]
- [[IPaymentService]] - `contains` [EXTRACTED]
- [[InitiatePaymentRequestDto]] - `contains` [EXTRACTED]
- [[InitiatePaymentResponseDto]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Service