namespace CampCenter.Domain.Entities;

/// The centre's current rates, as a single row the owner edits in the panel.
///
/// These are defaults only: they prefill a new booking and are then snapshotted
/// onto it, so raising the rate never changes what an existing group owes. The
/// appsettings "Booking" section still carries the values a fresh database is
/// seeded with.
public class PricingDefaults
{
    /// The one row. A fixed id keeps "read the settings" a primary-key lookup and
    /// makes a second row impossible to insert by accident.
    public static readonly Guid SingletonId = new("9a1f7c40-0f5f-4a2e-9a0f-2f1e2d3c4b5a");

    public Guid Id { get; set; } = SingletonId;

    /// Price per participant per night, in grosze.
    public long PricePerPersonPerNightGrosze { get; set; }

    /// Deposit per participant per night, in grosze (never above the price).
    public long DepositPerPersonPerNightGrosze { get; set; }

    public DateTime UpdatedAt { get; set; }

    public uint RowVersion { get; set; }
}
