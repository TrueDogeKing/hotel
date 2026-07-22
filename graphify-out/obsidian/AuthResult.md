---
source_file: "src/CampCenter.Application/Models/AuthResult.cs"
type: "code"
community: "Auth Service & Tokens"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_Service__Tokens
---

# AuthResult

## Context

_Source: `src/CampCenter.Application/Models/AuthResult.cs` (defined near L8; showing L6–L14 of 14)._

```csharp
/// <param name="RefreshToken">Plaintext refresh token value.</param>
/// <param name="RefreshTokenExpiresAtUtc">Refresh token expiration time (UTC).</param>
public record AuthResult(
    string AccessToken,
    DateTime AccessTokenExpiresAtUtc,
    string Login,
    string RefreshToken,
    DateTime RefreshTokenExpiresAtUtc
);
```

## Connections
- [[.IssueTokens()]] - `references` [EXTRACTED]
- [[.IssueTokensAsync()]] - `references` [EXTRACTED]
- [[.LoginAsync()]] - `references` [EXTRACTED]
- [[.LoginAsync()_1]] - `references` [EXTRACTED]
- [[.RefreshAsync()]] - `references` [EXTRACTED]
- [[.RefreshAsync()_1]] - `references` [EXTRACTED]
- [[AuthResult.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_Service__Tokens