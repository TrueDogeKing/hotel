---
source_file: "src/CampCenter.Api/Controllers/Admin/RoomsController.cs"
type: "code"
community: "Application Namespaces & DTOs"
location: "L97"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_Namespaces__DTOs
---

# RoomDeleteResultDto

## Context

_Source: `src/CampCenter.Api/Controllers/Admin/RoomsController.cs` (defined near L97; showing L95–L97 of 97)._

```csharp

/// <param name="Deleted">True when hard-deleted; false when deactivated (had history).</param>
public record RoomDeleteResultDto(bool Deleted);
```

## Connections
- [[RoomsController.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_Namespaces__DTOs