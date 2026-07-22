---
source_file: "tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs"
type: "code"
community: "Payment Gateway Integration Tests"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Payment_Gateway_Integration_Tests
---

# P24SignCalculatorTests

## Context

_Source: `tests/CampCenter.UnitTests/Services/P24SignCalculatorTests.cs` (defined near L8; showing L6–L53 of 65)._

```csharp
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
            300000,
            300000,
            "PLN",
            777,
            154,
            "statement",
            Sign: ""
```

## Connections
- [[.NotificationSign_RoundTrips()]] - `method` [EXTRACTED]
- [[.RegisterSign_MatchesDocumentedJsonShape()]] - `method` [EXTRACTED]
- [[.Sha384()_1]] - `method` [EXTRACTED]
- [[.VerifySign_MatchesDocumentedJsonShape()]] - `method` [EXTRACTED]
- [[P24SignCalculatorTests.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Payment_Gateway_Integration_Tests