---
source_file: "src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs"
type: "code"
community: "Przelewy24 Payment Client"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Przelewy24_Payment_Client
---

# Przelewy24Client.cs

## Context

_Source: `src/CampCenter.Infrastructure/Payments/Przelewy24Client.cs` (defined near L1; showing L1–L46 of 138)._

```csharp
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CampCenter.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace CampCenter.Infrastructure.Payments;

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
```

## Connections
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Payments]] - `contains` [EXTRACTED]
- [[Przelewy24Client]] - `contains` [EXTRACTED]
- [[RegisterData_1]] - `contains` [EXTRACTED]
- [[RegisterResponse]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Przelewy24_Payment_Client