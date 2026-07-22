---
source_file: "src/CampCenter.Application/Interfaces/ICampSessionService.cs"
type: "code"
community: "Camp Session Management"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Camp_Session_Management
---

# ICampSessionService

## Context

_Source: `src/CampCenter.Application/Interfaces/ICampSessionService.cs` — full file embedded (25 lines)._ ⚠️ **This file is deleted in the current working tree** (uncommitted change); context below is the committed version from git HEAD.

```csharp
using CampCenter.Application.DTOs.Sessions;

namespace CampCenter.Application.Interfaces;

public interface ICampSessionService
{
    Task<List<CampSessionDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<CampSessionDto> CreateAsync(
        CreateCampSessionRequestDto request,
        CancellationToken cancellationToken = default
    );

    Task<CampSessionDto> UpdateAsync(
        Guid id,
        UpdateCampSessionRequestDto request,
        CancellationToken cancellationToken = default
    );

    Task<CampSessionDto> PublishAsync(Guid id, CancellationToken cancellationToken = default);

    Task<CampSessionDto> ArchiveAsync(Guid id, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
```

## Connections
- [[.ArchiveAsync()]] - `method` [EXTRACTED]
- [[.CreateAsync()_1]] - `method` [EXTRACTED]
- [[.DeleteAsync()]] - `method` [EXTRACTED]
- [[.GetAllAsync()]] - `method` [EXTRACTED]
- [[.PublishAsync()]] - `method` [EXTRACTED]
- [[.UpdateAsync()]] - `method` [EXTRACTED]
- [[CampSessionService]] - `implements` [EXTRACTED]
- [[ICampSessionService.cs]] - `contains` [EXTRACTED]
- [[SessionsController]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Camp_Session_Management