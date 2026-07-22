---
source_file: "CLAUDE.md"
type: "document"
community: "Docker & Project Docs"
tags:
  - graphify/document
  - graphify/EXTRACTED
  - community/Docker__Project_Docs
---

# CampCenter Project Instructions

## Context

_Source document: `CLAUDE.md` — full text embedded below (76 lines)._

```markdown
# CampCenter

## Cel projektu

Aplikacja webowa ośrodka kolonijnego: strona informacyjna, rezerwacja turnusów
dla grup zorganizowanych (bez konta, potwierdzenie e-mailem) oraz panel
administratora (pokoje, turnusy, obłożenie, zadania dla obsługi). Płatności
online przez Przelewy24 (zaliczka + dopłata).

## Architektura

* Backend: ASP.NET Core Web API (.NET 10), Clean Architecture
  (CampCenter.Domain / Application / Infrastructure / Api)
* Frontend: React + Vite (SPA), i18n PL/EN (react-i18next, przełącznik flag)
* Baza danych: PostgreSQL, Entity Framework Core, optimistic concurrency (xmin)
* Autoryzacja: JWT (access token) + refresh token w cookie HttpOnly — tylko admini;
  rezerwujący nie mają kont (link zarządzania z tokenem w e-mailu)
* E-mail: MailKit/SMTP (dev: Mailpit, http://localhost:8025)
* Płatności: Przelewy24 (sandbox w dev), zaliczka potwierdza rezerwację
* Reverse proxy / TLS: Caddy; środowisko: Docker Compose

## Model domeny

* AdminUser + RefreshToken — logowanie panelu admina (seeder tworzy konto "admin")
* Room — pokój (numer, pojemność 2/3/4…, aktywny); bez dat — okres zajętości
  wynika z turnusu (przydział → rezerwacja → turnus)
* CampSession (turnus) — nazwa, daty, cena/os., zaliczka/os., status
  Draft/Published/Archived; opublikowane turnusy nie mogą się nakładać
* Booking — rezerwacja grupowa (organizacja, kontakt, liczba osób, status
  PendingDeposit/Confirmed/Cancelled/Completed, token zarządzania hashowany,
  język pl/en, kwoty w groszach — snapshot)
* BookingRoomAssignment — konkretne pokoje przydzielone przy utworzeniu;
  unikalny indeks (CampSessionId, RoomId) chroni przed podwójną rezerwacją
* RoomTask — zadania dla obsługi (np. dostawka), Open/Done
* Payment — Deposit/Final, Pending/Completed/Failed, pola P24; częściowy
  unikalny indeks (BookingId, Kind) WHERE Completed

## Wymagania bezpieczeństwa

* Hasła bcrypt; JWT + rotacja refresh tokenów z detekcją ponownego użycia
* Rate limiting globalny per-IP + zaostrzony na auth i publiczne endpointy rezerwacji
* Walidacja FluentValidation; kwoty płatności zawsze liczone po stronie serwera
* Webhook P24: weryfikacja podpisu SHA-384 + kwoty + transaction/verify, idempotentny

# Task Runner Rules

This project uses two different tools with separate responsibilities.

## Mise responsibilities

Mise is used ONLY for: infrastructure lifecycle, database lifecycle, environment
bootstrap, developer tooling. Examples: `db:up`, `db:down`, `db:reset`, `ef:add`,
`ef:update`.

Mise MUST NOT be used for: frontend/backend dev servers, application workflows,
build pipelines, testing workflows.

## Bun responsibilities

Bun is the primary application task runner and the frontend package manager
(tasks live in root `package.json`, run as `bun run <task>`; frontend deps are
locked in `frontend/bun.lock`). All application workflows must be exposed
through these tasks: `bun run dev`, `bun run build`, `bun run test`,
`bun run backend`, `bun run frontend`. Always invoke tasks as `bun run <task>`
— bare `bun <task>` collides with Bun built-ins (`bun build`, `bun test`,
`bun install`).

## Decision rule

Infrastructure → Mise. Application → Bun. If uncertain, prefer Bun.

## Build environment

Prefer container builds (devcontainer / docker compose). The host has .NET SDK 9
globally and SDK 10 only in the user profile (`~/.dotnet` — set
`DOTNET_ROOT=$HOME/.dotnet` and prepend it to PATH when building on the host).
```

## Cel projektu

Aplikacja webowa ośrodka kolonijnego: strona informacyjna, rezerwacja turnusów
dla grup zorganizowanych (bez konta, potwierdzenie e-mailem) oraz panel
administratora (pokoje, turnusy, obłożenie, zadania dla obsługi). Płatności
online przez Przelewy24 (zaliczka + dopłata).

## Architektura

* Backend: ASP.NET Core Web API (.NET 10), Clean Architecture
  (CampCenter.Domain / Application / Infrastructure / Api)
* Frontend: React + Vite (SPA), i18n PL/EN (react-i18next, przełącznik flag)
* Baza danych: PostgreSQL, Entity Framework Core, optimistic concurrency (xmin)
* Autoryzacja: JWT (access token) + refresh token w cookie HttpOnly — tylko admini;
  rezerwujący nie mają kont (link zarządzania z tokenem w e-mailu)
* E-mail: MailKit/SMTP (dev: Mailpit, http://localhost:8025)
* Płatności: Przelewy24 (sandbox w dev), zaliczka potwierdza rezerwację
* Reverse proxy / TLS: Caddy; środowisko: Docker Compose

## Model domeny

* AdminUser + RefreshToken — logowanie panelu admina (seeder tworzy konto "admin")
* Room — pokój (numer, pojemność 2/3/4…, aktywny); bez dat — okres zajętości
  wynika z turnusu (przydział → rezerwacja → turnus)
* CampSession (turnus) — nazwa, daty, cena/os., zaliczka/os., status
  Draft/Published/Archived; opublikowane turnusy nie mogą się nakładać
* Booking — rezerwacja grupowa (organizacja, kontakt, liczba osób, status
  PendingDeposit/Confirmed/Cancelled/Completed, token zarządzania hashowany,
  język pl/en, kwoty w groszach — snapshot)
* BookingRoomAssignment — konkretne pokoje przydzielone przy utworzeniu;
  unikalny indeks (CampSessionId, RoomId) chroni przed podwójną rezerwacją
* RoomTask — zadania dla obsługi (np. dostawka), Open/Done
* Payment — Deposit/Final, Pending/Completed/Failed, pola P24; częściowy
  unikalny indeks (BookingId, Kind) WHERE Completed

## Wymagania bezpieczeństwa

* Hasła bcrypt; JWT + rotacja refresh tokenów z detekcją ponownego użycia
* Rate limiting globalny per-IP + zaostrzony na auth i publiczne endpointy rezerwacji
* Walidacja FluentValidation; kwoty płatności zawsze liczone po stronie serwera
* Webhook P24: weryfikacja podpisu SHA-384 + kwoty + transaction/verify, idempotentny

# Task Runner Rules

This project uses two different tools with separate responsibilities.

## Mise responsibilities

Mise is used ONLY for: infrastructure lifecycle, database lifecycle, environment
bootstrap, developer tooling. Examples: `db:up`, `db:down`, `db:reset`, `ef:add`,
`ef:update`.

Mise MUST NOT be used for: frontend/backend dev servers, application workflows,
build pipelines, testing workflows.

## Bun responsibilities

Bun is the primary application task runner and the frontend package manager
(tasks live in root `package.json`, run as `bun run <task>`; frontend deps are
locked in `frontend/bun.lock`). All application workflows must be exposed
through these tasks: `bun run dev`, `bun run build`, `bun run test`,
`bun run backend`, `bun run frontend`. Always invoke tasks as `bun run <task>`
— bare `bun <task>` collides with Bun built-ins (`bun build`, `bun test`,
`bun install`).

## Decision rule

Infrastructure → Mise. Application → Bun. If uncertain, prefer Bun.

## Build environment

Prefer container builds (devcontainer / docker compose). The host has .NET SDK 9
globally and SDK 10 only in the user profile (`~/.dotnet` — set
`DOTNET_ROOT=$HOME/.dotnet` and prepend it to PATH when building on the host).
```

## Cel projektu

Aplikacja webowa ośrodka kolonijnego: strona informacyjna, rezerwacja turnusów
dla grup zorganizowanych (bez konta, potwierdzenie e-mailem) oraz panel
administratora (pokoje, turnusy, obłożenie, zadania dla obsługi). Płatności
online przez Przelewy24 (zaliczka + dopłata).

## Architektura

* Backend: ASP.NET Core Web API (.NET 10), Clean Architecture
  (CampCenter.Domain / Application / Infrastructure / Api)
* Frontend: React + Vite (SPA), i18n PL/EN (react-i18next, przełącznik flag)
* Baza danych: PostgreSQL, Entity Framework Core, optimistic concurrency (xmin)
* Autoryzacja: JWT (access token) + refresh token w cookie HttpOnly — tylko admini;
  rezerwujący nie mają kont (link zarządzania z tokenem w e-mailu)
* E-mail: MailKit/SMTP (dev: Mailpit, http://localhost:8025)
* Płatności: Przelewy24 (sandbox w dev), zaliczka potwierdza rezerwację
* Reverse proxy / TLS: Caddy; środowisko: Docker Compose

## Model domeny

* AdminUser + RefreshToken — logowanie panelu admina (seeder tworzy konto "admin")
* Room — pokój (numer, pojemność 2/3/4…, aktywny); bez dat — okres zajętości
  wynika z turnusu (przydział → rezerwacja → turnus)
* CampSession (turnus) — nazwa, daty, cena/os., zaliczka/os., status
  Draft/Published/Archived; opublikowane turnusy nie mogą się nakładać
* Booking — rezerwacja grupowa (organizacja, kontakt, liczba osób, status
  PendingDeposit/Confirmed/Cancelled/Completed, token zarządzania hashowany,
  język pl/en, kwoty w groszach — snapshot)
* BookingRoomAssignment — konkretne pokoje przydzielone przy utworzeniu;
  unikalny indeks (CampSessionId, RoomId) chroni przed podwójną rezerwacją
* RoomTask — zadania dla obsługi (np. dostawka), Open/Done
* Payment — Deposit/Final, Pending/Completed/Failed, pola P24; częściowy
  unikalny indeks (BookingId, Kind) WHERE Completed

## Wymagania bezpieczeństwa

* Hasła bcrypt; JWT + rotacja refresh tokenów z detekcją ponownego użycia
* Rate limiting globalny per-IP + zaostrzony na auth i publiczne endpointy rezerwacji
* Walidacja FluentValidation; kwoty płatności zawsze liczone po stronie serwera
* Webhook P24: weryfikacja podpisu SHA-384 + kwoty + transaction/verify, idempotentny

# Task Runner Rules

This project uses two different tools with separate responsibilities.

## Mise responsibilities

Mise is used ONLY for: infrastructure lifecycle, database lifecycle, environment
bootstrap, developer tooling. Examples: `db:up`, `db:down`, `db:reset`, `ef:add`,
`ef:update`.

Mise MUST NOT be used for: frontend/backend dev servers, application workflows,
build pipelines, testing workflows.

## Bun responsibilities

Bun is the primary application task runner and the frontend package manager
(tasks live in root `package.json`, run as `bun run <task>`; frontend deps are
locked in `frontend/bun.lock`). All application workflows must be exposed
through these tasks: `bun run dev`, `bun run build`, `bun run test`,
`bun run backend`, `bun run frontend`. Always invoke tasks as `bun run <task>`
— bare `bun <task>` collides with Bun built-ins (`bun build`, `bun test`,
`bun install`).

## Decision rule

Infrastructure → Mise. Application → Bun. If uncertain, prefer Bun.

## Build environment

Prefer container builds (devcontainer / docker compose). The host has .NET SDK 9
globally and SDK 10 only in the user profile (`~/.dotnet` — set
`DOTNET_ROOT=$HOME/.dotnet` and prepend it to PATH when building on the host).
```

## Connections
- [[CampCenter Domain Model]] - `references` [EXTRACTED]
- [[Clean Architecture (DomainApplicationInfrastructureApi)]] - `references` [EXTRACTED]
- [[JWT + Refresh Token Auth (admin only)]] - `references` [EXTRACTED]
- [[Przelewy24 Webhook Security (SHA-384 verify)]] - `references` [EXTRACTED]
- [[campcenter-caddy (reverse proxy  TLS)]] - `conceptually_related_to` [INFERRED]

#graphify/document #graphify/EXTRACTED #community/Docker__Project_Docs