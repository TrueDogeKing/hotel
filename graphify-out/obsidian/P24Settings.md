---
source_file: "src/CampCenter.Infrastructure/Payments/P24Settings.cs"
type: "code"
community: "Payment Gateway Integration Tests"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Payment_Gateway_Integration_Tests
---

# P24Settings

## Context

_Source: `src/CampCenter.Infrastructure/Payments/P24Settings.cs` (defined near L5; showing L3–L23 of 23)._

```csharp
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
- [[P24Settings.cs]] - `contains` [EXTRACTED]
- [[Przelewy24Client]] - `references` [EXTRACTED]
- [[string_6]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Payment_Gateway_Integration_Tests