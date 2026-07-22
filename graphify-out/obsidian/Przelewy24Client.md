---
source_file: "src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs"
type: "code"
community: "Payment Gateway Integration Tests"
location: "L12"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Payment_Gateway_Integration_Tests
---

# Przelewy24Client

## Context

_Source: `src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs` (defined near L12; showing L10–L57 of 138)._

```csharp

/// Przelewy24 REST client (transaction/register + transaction/verify).
public class Przelewy24Client : IPaymentGateway
{
    private const string Currency = "PLN";

    private readonly HttpClient _http;
    private readonly P24Settings _settings;

    public Przelewy24Client(HttpClient http, IOptions<P24Settings> settings)
    {
        _settings = settings.Value;
        _http = http;
        _http.BaseAddress = new Uri(_settings.BaseUrl.TrimEnd('/') + "/");
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic",
            Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_settings.PosId}:{_settings.ApiKey}"))
        );
    }

    public async Task<GatewayRegisterResult> RegisterTransactionAsync(
        GatewayRegisterRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var payload = new
        {
            merchantId = _settings.MerchantId,
            posId = _settings.PosId,
            sessionId = request.SessionId,
            amount = request.AmountGrosze,
            currency = Currency,
            description = request.Description,
            email = request.Email,
            country = "PL",
            language = request.Language,
            urlReturn = request.UrlReturn,
            urlStatus = $"{_settings.ApiBaseUrl.TrimEnd('/')}/api/public/payments/p24/status",
            sign = P24SignCalculator.RegisterSign(
                request.SessionId,
                _settings.MerchantId,
                request.AmountGrosze,
                Currency,
                _settings.CrcKey
            ),
        };

        var response = await _http.PostAsJsonAsync(
```

## Connections
- [[.RegisterTransactionAsync()_1]] - `method` [EXTRACTED]
- [[.VerifyNotificationSignature()_1]] - `method` [EXTRACTED]
- [[.VerifyTransactionAsync()_1]] - `method` [EXTRACTED]
- [[HttpClient]] - `references` [EXTRACTED]
- [[IPaymentGateway]] - `implements` [EXTRACTED]
- [[P24Settings]] - `references` [EXTRACTED]
- [[Przelewy24Client.cs]] - `contains` [EXTRACTED]
- [[string_7]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Payment_Gateway_Integration_Tests