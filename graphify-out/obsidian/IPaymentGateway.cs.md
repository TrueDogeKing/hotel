---
source_file: "src/CampCenter.Application/Interfaces/IPaymentGateway.cs"
type: "code"
community: "Payment Gateway Integration Tests"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Payment_Gateway_Integration_Tests
---

# IPaymentGateway.cs

## Context

_Source: `src/CampCenter.Application/Interfaces/IPaymentGateway.cs` (defined near L1; showing L1–L46 of 51)._

```csharp
namespace CampCenter.Application.Interfaces;

/// <param name="SessionId">Our transaction id (the Payment Id as a string).</param>
/// <param name="AmountGrosze">Amount in grosze, computed server-side.</param>
/// <param name="Language">"pl" or "en" — the payment page language.</param>
public record GatewayRegisterRequest(
    string SessionId,
    long AmountGrosze,
    string Description,
    string Email,
    string Language,
    string UrlReturn
);

public record GatewayRegisterResult(string Token, string RedirectUrl);

/// The P24 status notification payload (webhook body).
public record GatewayNotification(
    long MerchantId,
    long PosId,
    string SessionId,
    long Amount,
    long OriginAmount,
    string Currency,
    long OrderId,
    long MethodId,
    string Statement,
    string Sign
);

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
```

## Connections
- [[CampCenter.Application.Interfaces]] - `contains` [EXTRACTED]
- [[GatewayNotification]] - `contains` [EXTRACTED]
- [[GatewayRegisterRequest]] - `contains` [EXTRACTED]
- [[GatewayRegisterResult]] - `contains` [EXTRACTED]
- [[IPaymentGateway]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Payment_Gateway_Integration_Tests