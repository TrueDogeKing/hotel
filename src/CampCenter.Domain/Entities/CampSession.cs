namespace CampCenter.Domain.Entities;

public enum CampSessionStatus
{
    Draft,
    Published,
    Archived,
}

/// A camp session (turnus) with fixed dates. Bookers choose a session, never
/// free date ranges. Published sessions must not overlap — the physical room
/// inventory is shared.
public class CampSession
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    /// Price per participant for the whole session, in grosze.
    public long PricePerPersonGrosze { get; set; }

    /// Deposit per participant (paid online to confirm a booking), in grosze.
    public long DepositPerPersonGrosze { get; set; }

    /// Only Published sessions are visible to the public booking flow.
    public CampSessionStatus Status { get; set; } = CampSessionStatus.Draft;

    public DateTime CreatedAt { get; set; }

    public uint RowVersion { get; set; }
}
