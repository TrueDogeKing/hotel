---
source_file: "docker/docker-compose.infra.yml"
type: "concept"
community: "Docker & Project Docs"
location: "services.mailpit"
tags:
  - graphify/concept
  - graphify/EXTRACTED
  - community/Docker__Project_Docs
---

# campcenter-mail (Mailpit)

## Context

_Source: `docker/docker-compose.infra.yml` — full file embedded (32 lines)._

```yaml
name: campcenter

services:
  postgres:
    image: postgres:16-alpine
    container_name: campcenter-db
    restart: unless-stopped
    environment:
      POSTGRES_USER: ${POSTGRES_USER:-campcenter}
      POSTGRES_PASSWORD: ${POSTGRES_PASSWORD:-campcenter}
      POSTGRES_DB: ${POSTGRES_DB:-campcenter}
    ports:
      - "${POSTGRES_PORT:-5432}:5432"
    volumes:
      - campcenter-db:/var/lib/postgresql/data
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U ${POSTGRES_USER:-campcenter} -d ${POSTGRES_DB:-campcenter}"]
      interval: 5s
      timeout: 5s
      retries: 10
      start_period: 10s

  mailpit:
    image: axllent/mailpit
    container_name: campcenter-mail
    restart: unless-stopped
    ports:
      - "1025:1025" # SMTP
      - "8025:8025" # web UI (podgląd wysłanych maili)

volumes:
  campcenter-db:
```

## Connections
- [[Infra Docker Compose Stack]] - `references` [EXTRACTED]
- [[campcenter-api (dev service)]] - `shares_data_with` [EXTRACTED]

#graphify/concept #graphify/EXTRACTED #community/Docker__Project_Docs