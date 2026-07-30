---
type: community
cohesion: 0.09
members: 27
---

# Docker & Project Docs

**Cohesion:** 0.09 - loosely connected
**Members:** 27 nodes

## Members
- [[Admin Panel (Rooms, Closures, Occupancy, Housekeeping)]] - concept - README.md
- [[Build Environment (.NET SDK 10)]] - rationale - CLAUDE.md
- [[CI Backend Job (build + tests)]] - concept - .github/workflows/ci.yml
- [[CI Frontend Job (lint + typecheck + build)]] - concept - .github/workflows/ci.yml
- [[CI Workflow]] - concept - .github/workflows/ci.yml
- [[CampCenter Domain Model]] - concept - CLAUDE.md
- [[Closure Model Replaces Camp Sessions]] - rationale - CLAUDE.md
- [[Dev Docker Compose Stack]] - concept - docker/docker-compose.dev.yml
- [[Dev Quick Start]] - concept - README.md
- [[GiST Exclusion Constraint Against Double Booking]] - rationale - CLAUDE.md
- [[Guest Booking Flow]] - concept - README.md
- [[Infra Docker Compose Stack]] - concept - docker/docker-compose.infra.yml
- [[Knowledge Graph  Obsidian Vault Workflow]] - rationale - CLAUDE.md
- [[P24 Go-Live Checklist]] - concept - README.md
- [[Production Deployment (Caddy + Docker Compose)]] - concept - README.md
- [[Production Docker Compose Stack]] - concept - docker/docker-compose.prod.yml
- [[Przelewy24 Payments and Webhook]] - concept - README.md
- [[Security Requirements]] - concept - CLAUDE.md
- [[Task Runner Rules (Mise vs Bun)]] - rationale - CLAUDE.md
- [[Technology Stack]] - concept - README.md
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
