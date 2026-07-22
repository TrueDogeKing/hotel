---
source_file: "docker/docker-compose.dev.yml"
type: "concept"
community: "Docker & Project Docs"
location: "services.frontend"
tags:
  - graphify/concept
  - graphify/EXTRACTED
  - community/Docker__Project_Docs
---

# campcenter-frontend (dev service)

## Context

_Source: `docker/docker-compose.dev.yml` — full file embedded (38 lines)._

```yaml
name: campcenter

include:
  - docker-compose.infra.yml

services:
  api:
    container_name: campcenter-api
    restart: unless-stopped
    build:
      context: ..
      dockerfile: src/CampCenter.Api/Dockerfile
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_HTTP_PORTS=8080
      - ConnectionStrings__DefaultConnection=Host=postgres;Port=5432;Database=${POSTGRES_DB:-campcenter};Username=${POSTGRES_USER:-campcenter};Password=${POSTGRES_PASSWORD:-campcenter}
      - Database__MigrateAutomatically=true
      - Database__SeedAutomatically=true
      - Email__Host=mailpit
      - Email__Port=1025
      - Email__From=${EMAIL_FROM:-rezerwacje@campcenter.local}
      - Email__UseSsl=false
    ports:
      - "5080:8080"
    depends_on:
      postgres:
        condition: service_healthy

  frontend:
    container_name: campcenter-frontend
    restart: unless-stopped
    build:
      context: ../frontend
    ports:
      - "8080:8080"
    depends_on:
      api:
        condition: service_healthy
```

## Connections
- [[Dev Docker Compose Stack]] - `references` [EXTRACTED]
- [[campcenter-api (dev service)]] - `references` [EXTRACTED]

#graphify/concept #graphify/EXTRACTED #community/Docker__Project_Docs