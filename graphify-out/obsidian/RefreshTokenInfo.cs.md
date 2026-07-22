---
source_file: "src/CampCenter.Application/Models/RefreshTokenInfo.cs"
type: "code"
community: "JWT Token Service"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/JWT_Token_Service
---

# RefreshTokenInfo.cs

## Context

_Source: `src/CampCenter.Application/Models/RefreshTokenInfo.cs` (defined near L1; showing L1–L8 of 8)._

```csharp
namespace CampCenter.Application.Models;

/// Generated refresh token: plaintext value (for client), its hash (for database),
/// and expiration time.
/// <param name="RawToken">Plaintext token value – delivered to the client once.</param>
/// <param name="TokenHash">SHA-256 hash of the plaintext – stored in the database.</param>
/// <param name="ExpiresAtUtc">Token expiration time (UTC).</param>
public record RefreshTokenInfo(string RawToken, string TokenHash, DateTime ExpiresAtUtc);
```

## Connections
- [[CampCenter.Application.Models]] - `contains` [EXTRACTED]
- [[RefreshTokenInfo]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/JWT_Token_Service