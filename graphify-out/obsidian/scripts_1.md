---
source_file: "package.json"
type: "code"
community: "Root Task-Runner Scripts"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Root_Task-Runner_Scripts
---

# scripts

## Context

_Source: `package.json` (defined near L5; showing L3–L22 of 22)._

```json
  "private": true,
  "description": "Warstwa zadań aplikacji uruchamiana przez Bun (bun run <task>). Infrastruktura/baza są w mise.toml (patrz CLAUDE.md Task Runner Rules).",
  "scripts": {
    "setup": "dotnet restore && dotnet tool restore && cd frontend && bun install",
    "dev": "docker compose --env-file .env -f docker/docker-compose.dev.yml up --build",
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
- [[backend]] - `contains` [EXTRACTED]
- [[build_1]] - `contains` [EXTRACTED]
- [[dev_1]] - `contains` [EXTRACTED]
- [[devdown]] - `contains` [EXTRACTED]
- [[format_1]] - `contains` [EXTRACTED]
- [[formatbackend]] - `contains` [EXTRACTED]
- [[formatfrontend]] - `contains` [EXTRACTED]
- [[frontend]] - `contains` [EXTRACTED]
- [[package.json_1]] - `contains` [EXTRACTED]
- [[proddown]] - `contains` [EXTRACTED]
- [[prodlogs]] - `contains` [EXTRACTED]
- [[produp]] - `contains` [EXTRACTED]
- [[setup]] - `contains` [EXTRACTED]
- [[test]] - `contains` [EXTRACTED]
- [[testintegration]] - `contains` [EXTRACTED]
- [[testunit]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Root_Task-Runner_Scripts