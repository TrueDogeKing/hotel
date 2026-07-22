---
source_file: "src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs"
type: "code"
community: "Przelewy24 Payment Client"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Przelewy24_Payment_Client
---

# P24SignCalculator.cs

## Context

_Source: `src/CampCenter.Infrastructure/Payments/P24SignCalculator.cs` (defined near L1; showing L1–L41 of 41)._

```csharp
using System.Security.Cryptography;
using System.Text;
using CampCenter.Application.Interfaces;

namespace CampCenter.Infrastructure.Payments;

/// SHA-384 signatures per the Przelewy24 REST spec: the hash of a compact JSON
/// document with fields in the documented order. Field order and lack of spaces
/// are part of the contract — the strings below must not be reformatted.
public static class P24SignCalculator
{
    public static string RegisterSign(
        string sessionId,
        long merchantId,
        long amount,
        string currency,
        string crc
    ) =>
        Sha384(
            $"{{\"sessionId\":\"{sessionId}\",\"merchantId\":{merchantId},\"amount\":{amount},\"currency\":\"{currency}\",\"crc\":\"{crc}\"}}"
        );

    public static string NotificationSign(GatewayNotification n, string crc) =>
        Sha384(
            $"{{\"merchantId\":{n.MerchantId},\"posId\":{n.PosId},\"sessionId\":\"{n.SessionId}\",\"amount\":{n.Amount},\"originAmount\":{n.OriginAmount},\"currency\":\"{n.Currency}\",\"orderId\":{n.OrderId},\"methodId\":{n.MethodId},\"statement\":\"{n.Statement}\",\"crc\":\"{crc}\"}}"
        );

    public static string VerifySign(
        string sessionId,
        long orderId,
        long amount,
        string currency,
        string crc
    ) =>
        Sha384(
            $"{{\"sessionId\":\"{sessionId}\",\"orderId\":{orderId},\"amount\":{amount},\"currency\":\"{currency}\",\"crc\":\"{crc}\"}}"
        );

    private static string Sha384(string input) =>
        Convert.ToHexStringLower(SHA384.HashData(Encoding.UTF8.GetBytes(input)));
}
```

## Connections
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Payments]] - `contains` [EXTRACTED]
- [[P24SignCalculator]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Przelewy24_Payment_Client