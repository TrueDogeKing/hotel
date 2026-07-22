---
source_file: "src/CampCenter.Domain/Entities/Payment.cs"
type: "code"
community: "Booking Persistence & Entities"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Booking_Persistence__Entities
---

# Payment.cs

## Context

_Source: `src/CampCenter.Domain/Entities/Payment.cs` (defined near L1; showing L1–L46 of 48)._

```csharp
namespace CampCenter.Domain.Entities;

public enum PaymentKind
{
    Deposit,
    Final,
}

public enum PaymentStatus
{
    Pending,
    Completed,
    Failed,
}

/// One Przelewy24 payment attempt for a booking. Multiple Pending rows per
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

```

## Connections
- [[CampCenter.Domain.Entities]] - `contains` [EXTRACTED]
- [[Payment]] - `contains` [EXTRACTED]
- [[PaymentKind]] - `contains` [EXTRACTED]
- [[PaymentStatus]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Booking_Persistence__Entities