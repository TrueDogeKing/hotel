---
source_file: "package.json"
type: "code"
community: "Root Task-Runner Scripts"
location: "L18"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Root_Task-Runner_Scripts
---

# test

## Context

_Source: `package.json` (defined near L18; showing L16–L22 of 22)._

```json
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