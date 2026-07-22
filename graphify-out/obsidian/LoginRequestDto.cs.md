---
source_file: "src/CampCenter.Application/DTOs/Auth/LoginRequestDto.cs"
type: "code"
community: "Auth Controller"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_Controller
---

# LoginRequestDto.cs

## Context

_Source: `src/CampCenter.Application/DTOs/Auth/LoginRequestDto.cs` (defined near L1; showing L1–L5 of 5)._

```csharp
namespace CampCenter.Application.DTOs.Auth;

/// <param name="Login">Unique sign-in identifier.</param>
/// <param name="Password">Password in plain text.</param>
public record LoginRequestDto(string Login, string Password);
```

## Connections
- [[CampCenter.Application.DTOs.Auth]] - `contains` [EXTRACTED]
- [[LoginRequestDto]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_Controller