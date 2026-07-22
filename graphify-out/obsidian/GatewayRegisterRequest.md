---
source_file: "src/CampCenter.Application/Interfaces/IPaymentGateway.cs"
type: "code"
community: "Payment Gateway Integration Tests"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Payment_Gateway_Integration_Tests
---

# GatewayRegisterRequest

## Context

_Source: `src/CampCenter.Application/Interfaces/IPaymentGateway.cs` (defined near L6; showing L4–L51 of 51)._

```csharp
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
        long amountGrosze,
        long orderId,
        CancellationToken cancellationToken = default
    );
}
```

## Connections
- [[.CreateBookingWithDepositAsync()]] - `references` [EXTRACTED]
- [[.Notification()]] - `references` [EXTRACTED]
- [[.RegisterTransactionAsync()]] - `references` [EXTRACTED]
- [[.RegisterTransactionAsync()_1]] - `references` [EXTRACTED]
- [[.RegisterTransactionAsync()_2]] - `references` [EXTRACTED]
- [[FakePaymentGateway]] - `references` [EXTRACTED]
- [[IPaymentGateway.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Payment_Gateway_Integration_Tests