namespace CampCenter.Domain.Entities;

/// A booking as the panel shows it: one list, folding <see cref="BookingStatus"/>
/// together with <see cref="BookingPaymentState"/>.
///
/// The two were always read together — a booking is confirmed exactly when a
/// payment has been recorded against it — so the panel offers one control rather
/// than two that can disagree. The stored fields stay separate: the sweeper,
/// availability and the dashboard all reason about status alone.
public enum BookingState
{
    /// Live, nothing received yet ("oczekuje na płatność").
    AwaitingPayment,

    /// Live and confirmed by a deposit ("zaliczka zapłacona").
    DepositPaid,

    /// Live, confirmed and settled in full ("opłacone").
    Paid,

    Cancelled,

    /// The stay has finished.
    Completed,
}

public static class BookingStates
{
    /// The panel's list, in the order it is offered.
    public static readonly BookingState[] All =
    [
        BookingState.AwaitingPayment,
        BookingState.DepositPaid,
        BookingState.Paid,
        BookingState.Cancelled,
        BookingState.Completed,
    ];

    /// How a stored booking reads as one value. Cancelled and Completed are facts
    /// about the stay and outrank what was paid; anything still live is described
    /// by its payment.
    public static BookingState Of(BookingStatus status, BookingPaymentState payment) =>
        status switch
        {
            BookingStatus.Cancelled => BookingState.Cancelled,
            BookingStatus.Completed => BookingState.Completed,
            _ => payment switch
            {
                BookingPaymentState.Paid => BookingState.Paid,
                BookingPaymentState.DepositPaid => BookingState.DepositPaid,
                _ => BookingState.AwaitingPayment,
            },
        };
}
