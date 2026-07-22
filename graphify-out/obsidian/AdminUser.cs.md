---
source_file: "src/CampCenter.Domain/Entities/AdminUser.cs"
type: "code"
community: "Admin User & Token Config"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_User__Token_Config
---

# AdminUser.cs

## Context

_Source: `src/CampCenter.Domain/Entities/AdminUser.cs` (defined near L1; showing L1–L18 of 18)._

```csharp
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
```

## Connections
- [[AdminUser]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_User__Token_Config