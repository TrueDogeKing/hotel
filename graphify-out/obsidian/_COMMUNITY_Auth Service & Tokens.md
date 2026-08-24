---
type: community
members: 30
---

# Auth Service & Tokens

**Members:** 30 nodes

## Members
- [[.AddAsync()_6]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[.CreateAccessToken()]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[.CreateTokenEntity()]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.GenerateRefreshToken()]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[.GetByTokenHashAsync()_1]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[.HashRefreshToken()]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[.IssueTokensAsync()]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.LoginAsync()_1]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.LogoutAsync()_1]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.RefreshAsync()_1]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[.RevokeAllActiveForUserAsync()]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[.SaveChangesAsync()_6]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[.Verify()]] - code - src/CampCenter.Application/Interfaces/IPasswordHasher.cs
- [[AuthResult]] - code - src/CampCenter.Application/Models/AuthResult.cs
- [[AuthResult.cs]] - code - src/CampCenter.Application/Models/AuthResult.cs
- [[AuthService]] - code - src/CampCenter.Application/Services/AuthService.cs
- [[CancellationToken_33]] - code
- [[CancellationToken_51]] - code
- [[DateTime_2]] - code
- [[DateTime_16]] - code
- [[Guid_19]] - code
- [[Guid_46]] - code
- [[IPasswordHasher]] - code - src/CampCenter.Application/Interfaces/IPasswordHasher.cs
- [[IPasswordHasher.cs]] - code - src/CampCenter.Application/Interfaces/IPasswordHasher.cs
- [[IRefreshTokenRepository]] - code - src/CampCenter.Domain/Repositories/IRefreshTokenRepository.cs
- [[ITokenService]] - code - src/CampCenter.Application/Interfaces/ITokenService.cs
- [[RefreshTokenInfo]] - code - src/CampCenter.Application/Models/RefreshTokenInfo.cs
- [[RefreshTokenInfo.cs]] - code - src/CampCenter.Application/Models/RefreshTokenInfo.cs
- [[Task_33]] - code
- [[Task_51]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Auth_Service__Tokens
SORT file.name ASC
```

## Connections to other communities
- 6 edges to [[_COMMUNITY_Admin User & Token Config]]
- 5 edges to [[_COMMUNITY_.GetBlockedRoomIdsAsync]]
- 4 edges to [[_COMMUNITY_ClosureService]]
- 4 edges to [[_COMMUNITY_Booking Persistence & Entities (1)]]
- 4 edges to [[_COMMUNITY_Refresh Token Repository]]
- 3 edges to [[_COMMUNITY_Public Booking Service (1)]]
- 3 edges to [[_COMMUNITY_CampCenter.Application  Services (1)]]
- 2 edges to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 2 edges to [[_COMMUNITY_Room]]
- 1 edge to [[_COMMUNITY_BookingGroupSection.tsx]]
- 1 edge to [[_COMMUNITY_Application Namespaces & DTOs]]
- 1 edge to [[_COMMUNITY_Public Booking Service (2)]]
- 1 edge to [[_COMMUNITY_Exception]]

## Top bridge nodes
- [[ITokenService]] - degree 9, connects to 5 communities
- [[AuthService]] - degree 11, connects to 3 communities
- [[.LoginAsync()_1]] - degree 9, connects to 3 communities
- [[IRefreshTokenRepository]] - degree 8, connects to 3 communities
- [[IPasswordHasher]] - degree 6, connects to 2 communities