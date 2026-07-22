---
source_file: "package.json"
type: "code"
community: "Root Task-Runner Scripts"
location: "L10"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Root_Task-Runner_Scripts
---

# prod:down

## Context

_Source: `package.json` (defined near L10; showing L8–L22 of 22)._

```json
    "dev:down": "docker compose --env-file .env -f docker/docker-compose.dev.yml down",
    "prod:up": "docker compose --env-file .env -f docker/docker-compose.prod.yml up -d --build",
    "prod:down": "docker compose -f docker/docker-compose.prod.yml down",
    "prod:logs": "docker compose -f docker/docker-compose.prod.yml logs -f --tail 100",
    "backend": "dotnet watch run --project src/CampCenter.Api",
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