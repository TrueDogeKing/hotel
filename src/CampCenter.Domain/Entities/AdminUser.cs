namespace CampCenter.Domain.Entities;

/// Administrator account. Admins are created by the data seeder — there is no
/// public registration; bookers never have accounts.
public class AdminUser
{
    public Guid Id { get; set; }

    /// Unique sign-in identifier, stored lowercase.
    public required string Login { get; set; }

    public required string PasswordHash { get; set; }

    /// Create date (UTC).
    public DateTime CreatedAt { get; set; }

    public uint RowVersion { get; set; }
}
