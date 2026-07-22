---
source_file: "src/CampCenter.Application/DTOs/Auth/LoginResponseDto.cs"
type: "code"
community: "Auth DTOs & Models"
location: "L4"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_DTOs__Models
---

# LoginResponseDto

## Context

_Source: `src/CampCenter.Application/DTOs/Auth/LoginResponseDto.cs` (defined near L4; showing L2–L4 of 4)._

```csharp

/// <param name="Token">JWT token.</param>
public record LoginResponseDto(string Token, DateTime ExpiresAtUtc, string Login);
```

## Connections
- [[LoginResponseDto.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_DTOs__Models