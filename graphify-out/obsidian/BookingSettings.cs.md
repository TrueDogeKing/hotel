---
source_file: "src/CampCenter.Application/Models/BookingSettings.cs"
type: "code"
community: "Admin Booking & Notifications"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Booking__Notifications
---

# BookingSettings.cs

## Context

_Source: `src/CampCenter.Application/Models/BookingSettings.cs` (defined near L1; showing L1–L28 of 28)._

```csharp
namespace CampCenter.Application.Models;

/// Booking lifecycle policy, bound from the "Booking" configuration section.
public class BookingSettings
{
    public const string SectionName = "Booking";

    /// Days a PendingDeposit booking holds its rooms before the sweeper releases them.
    public int DepositHoldDays { get; set; } = 7;

    /// The final payment is due this many days before the stay starts.
    public int FinalPaymentDueDays { get; set; } = 30;

    /// Price per participant per night, in grosze. Booking total = rate × people × nights.
    public long PricePerPersonPerNightGrosze { get; set; } = 12000;

    /// Deposit per participant per night, in grosze (must not exceed the price).
    public long DepositPerPersonPerNightGrosze { get; set; } = 3000;

    /// Longest stay (nights) a single public booking may request.
    public int MaxNights { get; set; } = 60;

    /// Public site base URL used to build manage links in emails.
    public string PublicBaseUrl { get; set; } = "http://localhost:5173";

    /// Address alerted when a payment arrives for an already-cancelled booking.
    public string? AdminAlertEmail { get; set; }
}
```

## Connections
- [[BookingSettings]] - `contains` [EXTRACTED]
- [[CampCenter.Application.Models]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Booking__Notifications