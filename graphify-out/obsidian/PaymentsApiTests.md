---
source_file: "tests/CampCenter.IntegrationTests/PaymentsApiTests.cs"
type: "code"
community: "Payment Gateway Integration Tests"
location: "L51"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Payment_Gateway_Integration_Tests
---

# PaymentsApiTests

## Context

_Source: `tests/CampCenter.IntegrationTests/PaymentsApiTests.cs` (defined near L51; showing L49–L96 of 242)._

```csharp

public class PaymentsApiTests : IntegrationTestBase
{
    // Default per-night pricing from appsettings.json (grosze per person per night).
    private const long PricePerNight = 12_000;
    private const long DepositPerNight = 3_000;
    private const int Headcount = 9;
    private const int Nights = 10;

    private static int _windowOffset;

    private readonly FakePaymentGateway _gateway = new();
    private readonly HttpClient _client;
    private readonly HttpClient _admin;

    public PaymentsApiTests(CampCenterApiFactory factory)
        : base(factory)
    {
        var host = factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<IPaymentGateway>();
                services.AddSingleton<IPaymentGateway>(_gateway);
            })
        );
        _client = host.CreateClient();
        _admin = host.CreateClient();
    }

    private static GatewayNotification Notification(
        GatewayRegisterRequest registered,
        long orderId = 555,
        long? amountOverride = null
    ) =>
        new(
            0,
            0,
            registered.SessionId,
            amountOverride ?? registered.AmountGrosze,
            registered.AmountGrosze,
            "PLN",
            orderId,
            154,
            "statement",
            "sign-checked-by-fake"
        );

    private async Task<(
```

## Connections
- [[.CreateBookingWithDepositAsync()]] - `method` [EXTRACTED]
- [[.DepositWebhook_ConfirmsBooking_AndIsIdempotent()]] - `method` [EXTRACTED]
- [[.Notification()]] - `method` [EXTRACTED]
- [[.Webhook_AmountMismatch_IsRejected()]] - `method` [EXTRACTED]
- [[.Webhook_BadSignature_IsRejected()]] - `method` [EXTRACTED]
- [[FakePaymentGateway]] - `references` [EXTRACTED]
- [[HttpClient_2]] - `references` [EXTRACTED]
- [[IntegrationTestBase]] - `inherits` [EXTRACTED]
- [[PaymentsApiTests.cs]] - `contains` [EXTRACTED]
- [[int_2]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Payment_Gateway_Integration_Tests