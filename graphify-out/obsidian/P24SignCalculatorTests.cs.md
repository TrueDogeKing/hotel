---
source_file: "tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs"
type: "code"
community: "Przelewy24 Payment Client"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Przelewy24_Payment_Client
---

# P24SignCalculatorTests.cs

## Context

_Source: `tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs` (defined near L1; showing L1–L46 of 65)._

```csharp
using System.Security.Cryptography;
using System.Text;
using CampCenter.Application.Interfaces;
using CampCenter.Infrastructure.Payments;

namespace CampCenter.UnitTests.Services;

public class P24SignCalculatorTests
{
    private static string Sha384(string s) =>
        Convert.ToHexStringLower(SHA384.HashData(Encoding.UTF8.GetBytes(s)));

    [Fact]
    public void RegisterSign_MatchesDocumentedJsonShape()
    {
        // The P24 contract hashes the exact compact JSON with this field order.
        var expected = Sha384(
            "{\"sessionId\":\"s-1\",\"merchantId\":12345,\"amount\":300000,\"currency\":\"PLN\",\"crc\":\"secretcrc\"}"
        );

        Assert.Equal(
            expected,
            P24SignCalculator.RegisterSign("s-1", 12345, 300000, "PLN", "secretcrc")
        );
    }

    [Fact]
    public void VerifySign_MatchesDocumentedJsonShape()
    {
        var expected = Sha384(
            "{\"sessionId\":\"s-1\",\"orderId\":777,\"amount\":300000,\"currency\":\"PLN\",\"crc\":\"secretcrc\"}"
        );

        Assert.Equal(
            expected,
            P24SignCalculator.VerifySign("s-1", 777, 300000, "PLN", "secretcrc")
        );
    }

    [Fact]
    public void NotificationSign_RoundTrips()
    {
        var notification = new GatewayNotification(
            12345,
            67890,
            "s-1",
```

## Connections
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Payments]] - `imports` [EXTRACTED]
- [[CampCenter.UnitTests.Services]] - `contains` [EXTRACTED]
- [[P24SignCalculatorTests]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Przelewy24_Payment_Client