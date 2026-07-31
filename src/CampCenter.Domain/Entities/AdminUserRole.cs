namespace CampCenter.Domain.Entities;

/// What a panel account may do. Stored as a string, so a role added later reads
/// the same in the database as it does here.
public enum AdminUserRole
{
    /// Full access: every section, and the accounts themselves.
    Administrator,

    /// Read-only. Sees every section an administrator does and can change nothing
    /// in any of them. The API enforces that — any request that is not a read
    /// needs the Administrator role — and the panel only hides the controls whose
    /// request would be refused.
    Worker,
}
