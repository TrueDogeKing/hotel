---
source_file: "tests/CampCenter.IntegrationTests/PaymentsApiTests.cs"
type: "code"
community: "Payment Gateway Integration Tests"
location: "L15"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Payment_Gateway_Integration_Tests
---

# FakePaymentGateway

## Context

_Source: `tests/CampCenter.IntegrationTests/PaymentsApiTests.cs` (defined near L15; showing L13–L60 of 242)._

```csharp
/// Records register/verify calls and lets tests craft valid "notifications".
public class FakePaymentGateway : IPaymentGateway
{
    public List<GatewayRegisterRequest> Registered { get; } = [];

    public List<(string SessionId, long Amount, long OrderId)> Verified { get; } = [];

    public bool RejectSignature { get; set; }

    public Task<GatewayRegisterResult> RegisterTransactionAsync(
        GatewayRegisterRequest request,
        CancellationToken cancellationToken = default
    )
    {
        Registered.Add(request);
        return Task.FromResult(
            new GatewayRegisterResult(
                "fake-token",
                $"https://sandbox.przelewy24.pl/trnRequest/fake-token-{request.SessionId}"
            )
        );
    }

    public bool VerifyNotificationSignature(GatewayNotification notification) => !RejectSignature;

    public Task VerifyTransactionAsync(
        string sessionId,
        long amountGrosze,
        long orderId,
        CancellationToken cancellationToken = default
    )
    {
        Verified.Add((sessionId, amountGrosze, orderId));
        return Task.CompletedTask;
    }
}

public class PaymentsApiTests : IntegrationTestBase
{
    // Default per-night pricing from appsettings.json (grosze per person per night).
    private const long PricePerNight = 12_000;
    private const long DepositPerNight = 3_000;
    private const int Headcount = 9;
    private const int Nights = 10;

    private static int _windowOffset;

    private readonly FakePaymentGateway _gateway = new();
```

## Connections
- [[.RegisterTransactionAsync()_2]] - `method` [EXTRACTED]
- [[.VerifyNotificationSignature()_2]] - `method` [EXTRACTED]
- [[.VerifyTransactionAsync()_2]] - `method` [EXTRACTED]
- [[Amount]] - `references` [EXTRACTED]
- [[GatewayRegisterRequest]] - `references` [EXTRACTED]
- [[IPaymentGateway]] - `implements` [EXTRACTED]
- [[List_21]] - `references` [EXTRACTED]
- [[OrderId]] - `references` [EXTRACTED]
- [[PaymentsApiTests]] - `references` [EXTRACTED]
- [[PaymentsApiTests.cs]] - `contains` [EXTRACTED]
- [[SessionId]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Payment_Gateway_Integration_Tests