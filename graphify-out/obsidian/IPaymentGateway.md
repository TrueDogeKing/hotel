---
source_file: "src/CampCenter.Application/Interfaces/IPaymentGateway.cs"
type: "code"
community: "Payment Gateway Integration Tests"
location: "L33"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Payment_Gateway_Integration_Tests
---

# IPaymentGateway

## Context

_Source: `src/CampCenter.Application/Interfaces/IPaymentGateway.cs` (defined near L33; showing L31–L51 of 51)._

```csharp
/// Przelewy24 behind an interface so tests can fake it and the application layer
/// stays provider-agnostic.
public interface IPaymentGateway
{
    /// transaction/register → token + redirect URL for the hosted payment page.
    Task<GatewayRegisterResult> RegisterTransactionAsync(
        GatewayRegisterRequest request,
        CancellationToken cancellationToken = default
    );

    /// Validates the SHA-384 signature of a status notification.
    bool VerifyNotificationSignature(GatewayNotification notification);

    /// transaction/verify — final server-to-server confirmation. Throws on rejection.
    Task VerifyTransactionAsync(
        string sessionId,
        long amountGrosze,
        long orderId,
        CancellationToken cancellationToken = default
    );
}
```

## Connections
- [[.RegisterTransactionAsync()]] - `method` [EXTRACTED]
- [[.VerifyNotificationSignature()]] - `method` [EXTRACTED]
- [[.VerifyTransactionAsync()]] - `method` [EXTRACTED]
- [[FakePaymentGateway]] - `implements` [EXTRACTED]
- [[IPaymentGateway.cs]] - `contains` [EXTRACTED]
- [[PaymentService]] - `references` [EXTRACTED]
- [[Przelewy24Client]] - `implements` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Payment_Gateway_Integration_Tests