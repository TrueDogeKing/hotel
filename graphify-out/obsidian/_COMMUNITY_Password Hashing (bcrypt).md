---
type: community
members: 7
---

# Password Hashing (bcrypt)

**Members:** 7 nodes

## Members
- [[.Hash()]] - code - src/CampCenter.Application/Interfaces/IPasswordHasher.cs
- [[.Hash()_1]] - code - src/CampCenter.Infrastructure/Auth/BcryptPasswordHasher.cs
- [[.Verify()]] - code - src/CampCenter.Application/Interfaces/IPasswordHasher.cs
- [[.Verify()_1]] - code - src/CampCenter.Infrastructure/Auth/BcryptPasswordHasher.cs
- [[BcryptPasswordHasher]] - code - src/CampCenter.Infrastructure/Auth/BcryptPasswordHasher.cs
- [[IPasswordHasher]] - code - src/CampCenter.Application/Interfaces/IPasswordHasher.cs
- [[IPasswordHasher.cs]] - code - src/CampCenter.Application/Interfaces/IPasswordHasher.cs

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Password_Hashing_bcrypt
SORT file.name ASC
```

## Connections to other communities
- 2 edges to [[_COMMUNITY_Auth Service & Tokens]]
- 1 edge to [[_COMMUNITY_Application Namespaces & DTOs]]
- 1 edge to [[_COMMUNITY_CampCenter.UnitTests  Services (5)]]

## Top bridge nodes
- [[IPasswordHasher]] - degree 5, connects to 1 community
- [[BcryptPasswordHasher]] - degree 4, connects to 1 community
- [[IPasswordHasher.cs]] - degree 2, connects to 1 community
- [[.Verify()]] - degree 2, connects to 1 community