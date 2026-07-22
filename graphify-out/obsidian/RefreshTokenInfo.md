---
source_file: "src/CampCenter.Application/Models/RefreshTokenInfo.cs"
type: "code"
community: "JWT Token Service"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/JWT_Token_Service
---

# RefreshTokenInfo

## Context

_Source: `src/CampCenter.Application/Models/RefreshTokenInfo.cs` (defined near L8; showing L6–L8 of 8)._

```csharp
/// <param name="TokenHash">SHA-256 hash of the plaintext – stored in the database.</param>
/// <param name="ExpiresAtUtc">Token expiration time (UTC).</param>
public record RefreshTokenInfo(string RawToken, string TokenHash, DateTime ExpiresAtUtc);
```

## Connections
- [[.CreateTokenEntity()]] - `references` [EXTRACTED]
- [[.GenerateRefreshToken()]] - `references` [EXTRACTED]
- [[.GenerateRefreshToken()_1]] - `references` [EXTRACTED]
- [[RefreshTokenInfo.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/JWT_Token_Service