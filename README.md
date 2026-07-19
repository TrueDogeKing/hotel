# CampCenter

Aplikacja webowa ośrodka kolonijnego: strona informacyjna, rezerwacja turnusów
dla grup (bez zakładania konta), panel administratora (pokoje, turnusy,
obłożenie, zadania dla obsługi) i płatności online (Przelewy24, zaliczka + dopłata).
UI dwujęzyczne (PL/EN, przełącznik flag).

## Szybki start (dev)

```bash
cp .env.example .env
mise run db:up        # PostgreSQL + Mailpit (docker)
bun run setup         # dotnet restore + tool restore + bun install
bun run backend       # API: http://localhost:5298 (dotnet watch)
bun run frontend      # SPA: http://localhost:5173 (vite)
```

* Panel admina: http://localhost:5173/admin/logowanie (dev: `admin` / `Admin123!`)
* Scalar (OpenAPI): http://localhost:5298/scalar/v1 (tylko Development)
* Mailpit (podgląd e-maili): http://localhost:8025

Testy: `bun run test` (integracyjne wymagają uruchomionego Dockera — Testcontainers).

## Stack

.NET 10 (Clean Architecture) · EF Core + PostgreSQL · React 19 + Vite + Bun ·
react-i18next · MailKit · Caddy · Docker Compose. Szczegóły: `CLAUDE.md`.
