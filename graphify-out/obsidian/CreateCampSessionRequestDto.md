---
source_file: "src/CampCenter.Application/DTOs/Sessions/CampSessionDtos.cs"
type: "code"
community: "Camp Session Management"
location: "L14"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Camp_Session_Management
---

# CreateCampSessionRequestDto

## Context

_Source: `src/CampCenter.Application/DTOs/Sessions/CampSessionDtos.cs` — full file embedded (29 lines)._ ⚠️ **This file is deleted in the current working tree** (uncommitted change); context below is the committed version from git HEAD.

```csharp
namespace CampCenter.Application.DTOs.Sessions;

public record CampSessionDto(
    Guid Id,
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    long PricePerPersonGrosze,
    long DepositPerPersonGrosze,
    string Status,
    uint RowVersion
);

public record CreateCampSessionRequestDto(
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    long PricePerPersonGrosze,
    long DepositPerPersonGrosze
);

public record UpdateCampSessionRequestDto(
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    long PricePerPersonGrosze,
    long DepositPerPersonGrosze,
    uint RowVersion
);
```

## Connections
- [[.Create()_1]] - `references` [EXTRACTED]
- [[.CreateAsync()_1]] - `references` [EXTRACTED]
- [[.CreateAsync()_5]] - `references` [EXTRACTED]
- [[.Valid()]] - `references` [EXTRACTED]
- [[CampSessionDtos.cs]] - `contains` [EXTRACTED]
- [[CreateCampSessionRequestValidator]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Camp_Session_Management