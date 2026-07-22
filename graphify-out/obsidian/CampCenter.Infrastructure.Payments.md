---
source_file: "src/CampCenter.Infrastructure/Payments/P24Settings.cs"
type: "code"
community: "Przelewy24 Payment Client"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Przelewy24_Payment_Client
---

# CampCenter.Infrastructure.Payments

## Context

_Source: `src/CampCenter.Infrastructure/Payments/P24Settings.cs` (defined near L1; showing L1–L23 of 23)._

```csharp
namespace CampCenter.Infrastructure.Payments;

/// Przelewy24 configuration bound from the "P24" section. Defaults point at the
/// sandbox; production swaps BaseUrl and credentials.
public class P24Settings
{
    public const string SectionName = "P24";

    public long MerchantId { get; set; }

    public long PosId { get; set; }

    /// CRC key used in SHA-384 signatures.
    public string CrcKey { get; set; } = "";

    /// REST API key ("klucz do raportów") for Basic auth (posId:apiKey).
    public string ApiKey { get; set; } = "";

    public string BaseUrl { get; set; } = "https://sandbox.przelewy24.pl";

    /// Public base URL of this API, used to build the urlStatus webhook address.
    public string ApiBaseUrl { get; set; } = "http://localhost:5298";
}
```

## Connections
- [[DependencyInjection.cs_1]] - `imports` [EXTRACTED]
- [[P24Settings.cs]] - `contains` [EXTRACTED]
- [[P24SignCalculator.cs]] - `contains` [EXTRACTED]
- [[P24SignCalculatorTests.cs]] - `imports` [EXTRACTED]
- [[Przelewy24Client.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Przelewy24_Payment_Client