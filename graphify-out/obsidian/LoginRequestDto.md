---
source_file: "src/CampCenter.Application/DTOs/Auth/LoginRequestDto.cs"
type: "code"
community: "Auth Controller"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_Controller
---

# LoginRequestDto

## Context

_Source: `src/CampCenter.Application/DTOs/Auth/LoginRequestDto.cs` (defined near L5; showing L3–L5 of 5)._

```csharp
/// <param name="Login">Unique sign-in identifier.</param>
/// <param name="Password">Password in plain text.</param>
public record LoginRequestDto(string Login, string Password);
```

## Connections
- [[.Login()]] - `references` [EXTRACTED]
- [[.LoginAsync()]] - `references` [EXTRACTED]
- [[.LoginAsync()_1]] - `references` [EXTRACTED]
- [[LoginRequestDto.cs]] - `contains` [EXTRACTED]
- [[LoginRequestValidator]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_Controller