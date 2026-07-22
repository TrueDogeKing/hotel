---
type: community
cohesion: 0.12
members: 25
---

# Docker & Project Docs

**Cohesion:** 0.12 - loosely connected
**Members:** 25 nodes

## Members
- [[Booking Flow (reserve turnus, deposit confirms)]] - concept - README.md
- [[Bun (application task runner  frontend package manager)]] - concept - CLAUDE.md
- [[CI Backend Job (build + tests)]] - concept - .github/workflows/ci.yml
- [[CI Frontend Job (lint + typecheck + build)]] - concept - .github/workflows/ci.yml
- [[CI Workflow]] - concept - .github/workflows/ci.yml
- [[CampCenter Domain Model]] - concept - CLAUDE.md
- [[CampCenter Project Instructions]] - document - CLAUDE.md
- [[CampCenter README]] - document - README.md
- [[Clean Architecture (DomainApplicationInfrastructureApi)]] - concept - CLAUDE.md
- [[Dev Docker Compose Stack]] - concept - docker/docker-compose.dev.yml
- [[Dev Quickstart]] - concept - README.md
- [[Infra Docker Compose Stack]] - concept - docker/docker-compose.infra.yml
- [[JWT + Refresh Token Auth (admin only)]] - concept - CLAUDE.md
- [[Mise (infrastructure task runner)]] - concept - CLAUDE.md
- [[Production Docker Compose Stack]] - concept - docker/docker-compose.prod.yml
- [[Przelewy24 Payments Integration]] - concept - README.md
- [[Przelewy24 Webhook Security (SHA-384 verify)]] - rationale - CLAUDE.md
- [[Task Runner Rules (Mise vs Bun)]] - rationale - CLAUDE.md
- [[campcenter-api (dev service)]] - concept - docker/docker-compose.dev.yml
- [[campcenter-api (prod service)]] - concept - docker/docker-compose.prod.yml
- [[campcenter-caddy (reverse proxy  TLS)]] - concept - docker/docker-compose.prod.yml
- [[campcenter-db (PostgreSQL 16-alpine)]] - concept - docker/docker-compose.infra.yml
- [[campcenter-db-prod (PostgreSQL)]] - concept - docker/docker-compose.prod.yml
- [[campcenter-frontend (dev service)]] - concept - docker/docker-compose.dev.yml
- [[campcenter-mail (Mailpit)]] - concept - docker/docker-compose.infra.yml

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Docker__Project_Docs
SORT file.name ASC
```
