namespace CampCenter.Application.Models;

/// Booking lifecycle policy, bound from the "Booking" configuration section.
public class BookingSettings
{
    public const string SectionName = "Booking";

    /// Days a PendingDeposit booking holds its rooms before the sweeper releases them.
    public int DepositHoldDays { get; set; } = 7;

    /// The final payment is due this many days before the session starts.
    public int FinalPaymentDueDays { get; set; } = 30;

    /// Public site base URL used to build manage links in emails.
    public string PublicBaseUrl { get; set; } = "http://localhost:5173";
}
