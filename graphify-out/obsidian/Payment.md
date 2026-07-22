---
source_file: "src/CampCenter.Domain/Entities/Payment.cs"
type: "code"
community: "Booking Persistence & Entities"
location: "L19"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Booking_Persistence__Entities
---

# Payment

## Context

_Source: `src/CampCenter.Domain/Entities/Payment.cs` (defined near L19; showing L17–L48 of 48)._

```csharp
/// (booking, kind) are allowed (abandoned attempts); a partial unique index on
/// (BookingId, Kind) WHERE Status = Completed prevents double-paying.
public class Payment
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }

    public Booking? Booking { get; set; }

    public PaymentKind Kind { get; set; }

    /// Amount in grosze, computed server-side from the booking — never from client input.
    public long AmountGrosze { get; set; }

    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    /// Our transaction id sent to P24 as sessionId (the Payment Id as a string).
    public required string P24SessionId { get; set; }

    /// Token returned by transaction/register; used to build the redirect URL.
    public string? P24Token { get; set; }

    /// P24 order id from the status notification; required by transaction/verify.
    public long? P24OrderId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public uint RowVersion { get; set; }
}
```

## Connections
- [[.AddPaymentAsync()]] - `references` [EXTRACTED]
- [[.AddPaymentAsync()_1]] - `references` [EXTRACTED]
- [[.Configure()_4]] - `references` [EXTRACTED]
- [[.GetPaymentByP24SessionIdAsync()]] - `references` [EXTRACTED]
- [[.GetPaymentByP24SessionIdAsync()_1]] - `references` [EXTRACTED]
- [[.GetPaymentsAsync()]] - `references` [EXTRACTED]
- [[.GetPaymentsAsync()_1]] - `references` [EXTRACTED]
- [[AppDbContext]] - `references` [EXTRACTED]
- [[Booking]] - `references` [EXTRACTED]
- [[DateTime_6]] - `references` [EXTRACTED]
- [[Guid_21]] - `references` [EXTRACTED]
- [[Payment.cs]] - `contains` [EXTRACTED]
- [[PaymentConfiguration]] - `references` [EXTRACTED]
- [[PaymentKind]] - `references` [EXTRACTED]
- [[PaymentStatus]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Booking_Persistence__Entities