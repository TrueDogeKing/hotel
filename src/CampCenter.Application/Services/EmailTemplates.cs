using System.Globalization;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Entities;

namespace CampCenter.Application.Services;

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
            Dzień dobry {booking.ContactName},

            przyjęliśmy rezerwację dla {booking.Headcount} uczestników,
            {stay} ({booking.Nights} nocy).

            Cena łączna: {total}
            Zaliczka potwierdzająca rezerwację: {deposit}
              (płatna do {FormatDateTime(
                booking.HoldExpiresAt,
                "pl"
            )} — nieopłacone rezerwacje są zwalniane)
            Pozostała kwota: {rest}, płatna do {Format(finalDueDate, "pl")}.

            Rezerwacją zarządzasz (płatność, anulowanie) tutaj:
            {manageUrl}

            Nie udostępniaj tego linku — każdy, kto go ma, może zarządzać rezerwacją.

            Ośrodek CampCenter
            """
        );
    }

    public static EmailMessage BookingCancelled(Booking booking)
    {
        var stay = Stay(booking, booking.Language);
        if (booking.Language == "en")
        {
            return new EmailMessage(
                booking.Email,
                $"Booking cancelled — {stay}",
                $"""
                Hello {booking.ContactName},

                your booking for {stay} has been cancelled.

                CampCenter
                """
            );
        }

        return new EmailMessage(
            booking.Email,
            $"Rezerwacja anulowana — {stay}",
            $"""
            Dzień dobry {booking.ContactName},

            Twoja rezerwacja ({stay}) została anulowana.

            Ośrodek CampCenter
            """
        );
    }

    public static EmailMessage BookingConfirmed(Booking booking)
    {
        var stay = Stay(booking, booking.Language);
        if (booking.Language == "en")
        {
            return new EmailMessage(
                booking.Email,
                $"Booking confirmed — {stay}",
                $"""
                Hello {booking.ContactName},

                we received your deposit — the booking for {stay} is confirmed.

                See you soon!
                CampCenter
                """
            );
        }

        return new EmailMessage(
            booking.Email,
            $"Rezerwacja potwierdzona — {stay}",
            $"""
            Dzień dobry {booking.ContactName},

            otrzymaliśmy zaliczkę — rezerwacja ({stay}) jest potwierdzona.

            Do zobaczenia!
            Ośrodek CampCenter
            """
        );
    }

    /// A stay rendered as a date range, e.g. "12–19 lipca 2026".
    private static string Stay(Booking booking, string language) =>
        $"{Format(booking.StartDate, language)} – {Format(booking.EndDate, language)}";

    private static string Format(DateOnly date, string language) =>
        date.ToString("d MMMM yyyy", new CultureInfo(language == "en" ? "en-GB" : "pl-PL"));

    private static string FormatDateTime(DateTime? utc, string language) =>
        utc is null
            ? "-"
            : utc.Value.ToString(
                "d MMMM yyyy HH:mm 'UTC'",
                new CultureInfo(language == "en" ? "en-GB" : "pl-PL")
            );

    private static string FormatZl(long grosze) =>
        string.Create(new CultureInfo("pl-PL"), $"{grosze / 100m:N2} zł");
}
