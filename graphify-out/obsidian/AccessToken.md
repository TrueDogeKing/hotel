---
source_file: "src/CampCenter.Application/Models/AccessToken.cs"
type: "code"
community: "Admin User & Token Config"
location: "L3"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_User__Token_Config
---

# AccessToken

## Context

_Source: `src/CampCenter.Application/Models/AccessToken.cs` (defined near L3; showing L1–L3 of 3)._

```csharp
namespace CampCenter.Application.Models;

public record AccessToken(string Token, DateTime ExpiresAtUtc);
```

## Connections
- [[.CreateAccessToken()]] - `references` [EXTRACTED]
- [[.CreateAccessToken()_1]] - `references` [EXTRACTED]
- [[AccessToken.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_User__Token_Config