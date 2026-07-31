namespace CampCenter.Domain.Entities;

/// Panel account. The first one is created by the data seeder and the rest by an
/// administrator — there is no public registration; bookers never have accounts.
public class AdminUser
{
    public Guid Id { get; set; }

    /// Unique sign-in identifier, stored lowercase.
    public required string Login { get; set; }

    public required string PasswordHash { get; set; }

    /// What this account may do. Administrator by default: the column was added to
    /// a table whose every existing row was an administrator.
    public AdminUserRole Role { get; set; } = AdminUserRole.Administrator;

    /// Create date (UTC).
    public DateTime CreatedAt { get; set; }

    public uint RowVersion { get; set; }
}
