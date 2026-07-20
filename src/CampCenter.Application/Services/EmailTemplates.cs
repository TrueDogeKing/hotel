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
        CampSession session,
        string manageUrl,
        DateOnly finalDueDate
    )
    {
        var total = FormatZl(booking.TotalGrosze);
        var deposit = FormatZl(booking.DepositGrosze);
        var rest = FormatZl(booking.TotalGrosze - booking.DepositGrosze);

        if (booking.Language == "en")
        {
            return new EmailMessage(
                booking.Email,
                $"Booking received — {session.Name}",
                $"""
                Hello {booking.ContactName},

                we received your booking for {booking.Headcount} participants,
                session "{session.Name}" ({Format(session.StartDate, "en")} – {Format(
                    session.EndDate,
                    "en"
                )}).

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
            $"Rezerwacja przyjęta — {session.Name}",
            $"""
            Dzień dobry {booking.ContactName},

            przyjęliśmy rezerwację dla {booking.Headcount} uczestników,
            turnus „{session.Name}" ({Format(session.StartDate, "pl")} – {Format(
                session.EndDate,
                "pl"
            )}).

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

    public static EmailMessage BookingCancelled(Booking booking, CampSession session)
    {
        if (booking.Language == "en")
        {
            return new EmailMessage(
                booking.Email,
                $"Booking cancelled — {session.Name}",
                $"""
                Hello {booking.ContactName},

                your booking for session "{session.Name}" has been cancelled.

                CampCenter
                """
            );
        }

        return new EmailMessage(
            booking.Email,
            $"Rezerwacja anulowana — {session.Name}",
            $"""
            Dzień dobry {booking.ContactName},

            Twoja rezerwacja na turnus „{session.Name}" została anulowana.

            Ośrodek CampCenter
            """
        );
    }

    public static EmailMessage BookingConfirmed(Booking booking, CampSession session)
    {
        if (booking.Language == "en")
        {
            return new EmailMessage(
                booking.Email,
                $"Booking confirmed — {session.Name}",
                $"""
                Hello {booking.ContactName},

                we received your deposit — the booking for session "{session.Name}"
                ({Format(session.StartDate, "en")} – {Format(session.EndDate, "en")}) is confirmed.

                See you soon!
                CampCenter
                """
            );
        }

        return new EmailMessage(
            booking.Email,
            $"Rezerwacja potwierdzona — {session.Name}",
            $"""
            Dzień dobry {booking.ContactName},

            otrzymaliśmy zaliczkę — rezerwacja na turnus „{session.Name}"
            ({Format(session.StartDate, "pl")} – {Format(session.EndDate, "pl")}) jest potwierdzona.

            Do zobaczenia!
            Ośrodek CampCenter
            """
        );
    }

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
