---
source_file: "package.json"
type: "code"
community: "Root Task-Runner Scripts"
location: "L16"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Root_Task-Runner_Scripts
---

# format:backend

## Context

_Source: `package.json` (defined near L16; showing L14–L22 of 22)._

```json
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