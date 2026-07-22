---
source_file: "src/CampCenter.Application/DTOs/Auth/LoginResponseDto.cs"
type: "code"
community: "Auth DTOs & Models"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_DTOs__Models
---

# LoginResponseDto.cs

## Context

_Source: `src/CampCenter.Application/DTOs/Auth/LoginResponseDto.cs` (defined near L1; showing L1–L4 of 4)._

```csharp
namespace CampCenter.Application.DTOs.Auth;

/// <param name="Token">JWT token.</param>
public record LoginResponseDto(string Token, DateTime ExpiresAtUtc, string Login);
```

## Connections
- [[CampCenter.Application.DTOs.Auth]] - `contains` [EXTRACTED]
- [[LoginResponseDto]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_DTOs__Models