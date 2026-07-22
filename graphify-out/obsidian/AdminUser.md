---
source_file: "src/CampCenter.Domain/Entities/AdminUser.cs"
type: "code"
community: "Admin User & Token Config"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_User__Token_Config
---

# AdminUser

## Context

_Source: `src/CampCenter.Domain/Entities/AdminUser.cs` (defined near L5; showing L3–L18 of 18)._

```csharp
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
- [[.Configure()]] - `references` [EXTRACTED]
- [[.CreateAccessToken()]] - `references` [EXTRACTED]
- [[.CreateAccessToken()_1]] - `references` [EXTRACTED]
- [[.GetByIdAsync()]] - `references` [EXTRACTED]
- [[.GetByIdAsync()_5]] - `references` [EXTRACTED]
- [[.GetByLoginAsync()]] - `references` [EXTRACTED]
- [[.GetByLoginAsync()_1]] - `references` [EXTRACTED]
- [[.IssueTokensAsync()]] - `references` [EXTRACTED]
- [[AdminUser.cs]] - `contains` [EXTRACTED]
- [[AdminUserConfiguration]] - `references` [EXTRACTED]
- [[AppDbContext]] - `references` [EXTRACTED]
- [[DateTime_3]] - `references` [EXTRACTED]
- [[Guid_17]] - `references` [EXTRACTED]
- [[RefreshToken]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_User__Token_Config