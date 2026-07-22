---
source_file: "src/CampCenter.Application/Services/EmailTemplates.cs"
type: "code"
community: "Admin Booking & Notifications"
location: "L9"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Booking__Notifications
---

# EmailTemplates

## Context

_Source: `src/CampCenter.Application/Services/EmailTemplates.cs` (defined near L9; showing L7–L54 of 159)._

```csharp
/// Plain-text booking emails in the booker's language ("pl"/"en", captured at
/// booking time). Anything unknown falls back to Polish.
public static class EmailTemplates
{
    public static EmailMessage BookingCreated(
        Booking booking,
        string manageUrl,
        DateOnly finalDueDate
    )
    {
        var total = FormatZl(booking.TotalGrosze);
        var deposit = FormatZl(booking.DepositGrosze);
        var rest = FormatZl(booking.TotalGrosze - booking.DepositGrosze);
        var stay = Stay(booking, booking.Language);

        if (booking.Language == "en")
        {
            return new EmailMessage(
                booking.Email,
                $"Booking received — {stay}",
                $"""
                Hello {booking.ContactName},

                we received your booking for {booking.Headcount} participants,
                {stay} ({booking.Nights} nights).

                Total price: {total}
                Deposit due to confirm the booking: {deposit}
                  (payable until {FormatDateTime(
                    booking.HoldExpiresAt,
                    "en"
                )} — unpaid bookings are released)
                Remaining amount: {rest}, due by {Format(finalDueDate, "en")}.

                Manage your booking (payment, cancellation) here:
                {manageUrl}

                Keep this link private — anyone who has it can manage the booking.

                CampCenter
                """
            );
        }

        return new EmailMessage(
            booking.Email,
            $"Rezerwacja przyjęta — {stay}",
            $"""
```

## Connections
- [[.BookingCancelled()]] - `method` [EXTRACTED]
- [[.BookingConfirmed()]] - `method` [EXTRACTED]
- [[.BookingCreated()]] - `method` [EXTRACTED]
- [[.Format()]] - `method` [EXTRACTED]
- [[.FormatDateTime()]] - `method` [EXTRACTED]
- [[.FormatZl()]] - `method` [EXTRACTED]
- [[EmailTemplates.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Booking__Notifications