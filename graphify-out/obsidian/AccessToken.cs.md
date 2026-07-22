---
source_file: "src/CampCenter.Application/Models/AccessToken.cs"
type: "code"
community: "Admin User & Token Config"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_User__Token_Config
---

# AccessToken.cs

## Context

_Source: `src/CampCenter.Application/Models/AccessToken.cs` (defined near L1; showing L1–L3 of 3)._

```csharp
namespace CampCenter.Application.Models;

public record AccessToken(string Token, DateTime ExpiresAtUtc);
```

## Connections
- [[AccessToken]] - `contains` [EXTRACTED]
- [[CampCenter.Application.Models]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_User__Token_Config