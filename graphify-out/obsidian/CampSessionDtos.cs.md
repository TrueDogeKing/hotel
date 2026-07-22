---
source_file: "src/CampCenter.Application/DTOs/Sessions/CampSessionDtos.cs"
type: "code"
community: "Camp Session Management"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Camp_Session_Management
---

# CampSessionDtos.cs

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
- [[CampCenter.Application.DTOs.Sessions]] - `contains` [EXTRACTED]
- [[CampSessionDto]] - `contains` [EXTRACTED]
- [[CreateCampSessionRequestDto]] - `contains` [EXTRACTED]
- [[UpdateCampSessionRequestDto]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Camp_Session_Management