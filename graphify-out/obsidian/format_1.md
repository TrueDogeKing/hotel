---
source_file: "package.json"
type: "code"
community: "Root Task-Runner Scripts"
location: "L15"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Root_Task-Runner_Scripts
---

# format

## Context

_Source: `package.json` (defined near L15; showing L13–L22 of 22)._

```json
    "frontend": "cd frontend && bun run dev",
    "build": "dotnet build CampCenter.slnx -c Release && cd frontend && bun run build",
    "format": "dotnet csharpier format . && cd frontend && bun run format",
    "format:backend": "dotnet csharpier format .",
    "format:frontend": "cd frontend && bun run format",
    "test": "dotnet test CampCenter.slnx",
    "test:unit": "dotnet test tests/CampCenter.UnitTests/CampCenter.UnitTests.csproj",
    "test:integration": "dotnet test tests/CampCenter.IntegrationTests/CampCenter.IntegrationTests.csproj"
  }
}
```

## Connections
- [[scripts_1]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Root_Task-Runner_Scripts