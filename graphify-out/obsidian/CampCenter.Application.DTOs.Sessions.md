---
source_file: "src/CampCenter.Application/DTOs/Sessions/CampSessionDtos.cs"
type: "code"
community: "Application DTO Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_DTO_Namespaces
---

# CampCenter.Application.DTOs.Sessions

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
- [[AdminPanelApiTests.cs]] - `imports` [EXTRACTED]
- [[CampSessionDtos.cs]] - `contains` [EXTRACTED]
- [[CampSessionService.cs]] - `imports` [EXTRACTED]
- [[CampSessionValidators.cs]] - `imports` [EXTRACTED]
- [[CampSessionValidatorsTests.cs]] - `imports` [EXTRACTED]
- [[ICampSessionService.cs]] - `imports` [EXTRACTED]
- [[PaymentsApiTests.cs]] - `imports` [EXTRACTED]
- [[PublicBookingApiTests.cs]] - `imports` [EXTRACTED]
- [[RoomsAndSessionsApiTests.cs]] - `imports` [EXTRACTED]
- [[SessionsController.cs]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_DTO_Namespaces