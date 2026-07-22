---
source_file: "src/CampCenter.Application/DTOs/Auth/LoginRequestDto.cs"
type: "code"
community: "Auth DTOs & Models"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Auth_DTOs__Models
---

# CampCenter.Application.DTOs.Auth

## Context

_Source: `src/CampCenter.Application/DTOs/Auth/LoginRequestDto.cs` (defined near L1; showing L1–L5 of 5)._

```csharp
namespace CampCenter.Application.DTOs.Auth;

/// <param name="Login">Unique sign-in identifier.</param>
/// <param name="Password">Password in plain text.</param>
public record LoginRequestDto(string Login, string Password);
```

## Connections
- [[ApiCollection.cs]] - `imports` [EXTRACTED]
- [[AuthApiTests.cs]] - `imports` [EXTRACTED]
- [[AuthController.cs]] - `imports` [EXTRACTED]
- [[AuthService.cs]] - `imports` [EXTRACTED]
- [[IAuthService.cs]] - `imports` [EXTRACTED]
- [[LoginRequestDto.cs]] - `contains` [EXTRACTED]
- [[LoginRequestValidator.cs]] - `imports` [EXTRACTED]
- [[LoginRequestValidatorTests.cs]] - `imports` [EXTRACTED]
- [[LoginResponseDto.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Auth_DTOs__Models