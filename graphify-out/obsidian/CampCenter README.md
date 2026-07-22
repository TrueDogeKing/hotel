---
source_file: "README.md"
type: "document"
community: "Docker & Project Docs"
tags:
  - graphify/document
  - graphify/EXTRACTED
  - community/Docker__Project_Docs
---

# CampCenter README

## Context

_Source document: `README.md` — full text embedded below (74 lines)._

````markdown
# CampCenter

Aplikacja webowa ośrodka kolonijnego: strona informacyjna, rezerwacja turnusów
dla grup zorganizowanych (bez zakładania konta), panel administratora (pokoje,
turnusy, obłożenie, zadania dla obsługi) i płatności online Przelewy24
(zaliczka + dopłata). UI dwujęzyczne PL/EN (przełącznik flag).

## Jak to działa

1. Admin definiuje pokoje (liczba miejsc) i turnusy (daty, cena/os., zaliczka/os.),
   po czym publikuje turnus.
2. Rezerwujący podaje liczbę osób, wybiera turnus z wolnymi miejscami, koryguje
   proponowany podział pokoi i zostawia dane kontaktowe — na e-mail przychodzi
   link do zarządzania rezerwacją.
3. Zaliczka opłacona przez Przelewy24 potwierdza rezerwację (nieopłacone
   rezerwacje są zwalniane po 7 dniach); dopłata jest płatna do 30 dni przed
   rozpoczęciem turnusu.
4. Admin widzi obłożenie pokoi w każdym turnusie i dodaje zadania dla obsługi
   (np. dostawka), a pulpit pokazuje zaległe dopłaty i oczekujące zaliczki.

## Szybki start (dev)

```bash
cp .env.example .env
mise run db:up        # PostgreSQL + Mailpit (docker); bez mise: docker compose -f docker/docker-compose.infra.yml up -d
bun run setup         # dotnet restore + tool restore + bun install
bun run backend       # API: http://localhost:5298 (dotnet watch)
bun run frontend      # SPA: http://localhost:5173 (vite)
```

* Panel admina: http://localhost:5173/admin/logowanie (dev: `admin` / `Admin123!`)
* Scalar (OpenAPI): http://localhost:5298/scalar/v1 (tylko Development)
* Mailpit (podgląd e-maili): http://localhost:8025

Testy: `bun run test` (integracyjne wymagają uruchomionego Dockera — Testcontainers).

## Płatności (Przelewy24)

* Dev/sandbox: załóż konto na https://sandbox.przelewy24.pl, wpisz
  `P24__MerchantId`, `P24__PosId`, `P24__CrcKey`, `P24__ApiKey` w
  `appsettings.Development.json` (albo zmienne środowiskowe).
* Webhook `POST /api/public/payments/p24/status` musi być osiągalny z internetu.
  Lokalnie użyj tunelu (np. `cloudflared tunnel --url http://localhost:5298`)
  i ustaw `P24__ApiBaseUrl` na adres tunelu; alternatywnie w panelu sandboxa
  możesz ręcznie ponowić notyfikację.
* Kwoty zawsze liczy serwer; webhook weryfikuje podpis SHA-384, kwotę i wykonuje
  `transaction/verify` — dopiero wtedy rezerwacja jest potwierdzana.

### Checklist go-live P24

1. Konto produkcyjne na https://przelewy24.pl zweryfikowane i aktywne.
2. W `.env`: `P24_MERCHANT_ID`, `P24_POS_ID`, `P24_CRC_KEY`, `P24_API_KEY`
   z panelu produkcyjnego oraz `P24_BASE_URL=https://secure.przelewy24.pl`.
3. `DOMAIN` wskazuje publicznie na serwer (webhook: `https://DOMAIN/api/public/payments/p24/status`).
4. Wykonaj przelew testowy 1 zł i sprawdź: status w panelu P24, e-mail
   potwierdzenia, status rezerwacji w panelu admina.

## Produkcja

```bash
cp .env.example .env   # uzupełnij DOMAIN, JWT__Key, POSTGRES_PASSWORD, ADMIN_PASSWORD, EMAIL_*, P24_*
bun run prod:up        # Caddy (auto-HTTPS) + frontend + api + postgres
```

Caddy sam wystawia certyfikat Let's Encrypt dla `DOMAIN` (porty 80/443 muszą
być przekierowane na serwer). Baza produkcyjna używa osobnego wolumenu
(`campcenter-db-prod`); kopia zapasowa: `mise run db:backup`.

## Stack

.NET 10 (Clean Architecture: Domain/Application/Infrastructure/Api) · EF Core +
PostgreSQL (optimistic concurrency przez xmin) · React 19 + Vite + Bun ·
react-i18next · MailKit · Przelewy24 REST · Caddy · Docker Compose ·
xUnit + Testcontainers. Zasady task runnerów i szczegóły domeny: `CLAUDE.md`.
````

## Jak to działa

1. Admin definiuje pokoje (liczba miejsc) i turnusy (daty, cena/os., zaliczka/os.),
   po czym publikuje turnus.
2. Rezerwujący podaje liczbę osób, wybiera turnus z wolnymi miejscami, koryguje
   proponowany podział pokoi i zostawia dane kontaktowe — na e-mail przychodzi
   link do zarządzania rezerwacją.
3. Zaliczka opłacona przez Przelewy24 potwierdza rezerwację (nieopłacone
   rezerwacje są zwalniane po 7 dniach); dopłata jest płatna do 30 dni przed
   rozpoczęciem turnusu.
4. Admin widzi obłożenie pokoi w każdym turnusie i dodaje zadania dla obsługi
   (np. dostawka), a pulpit pokazuje zaległe dopłaty i oczekujące zaliczki.

## Szybki start (dev)

```bash
cp .env.example .env
mise run db:up        # PostgreSQL + Mailpit (docker); bez mise: docker compose -f docker/docker-compose.infra.yml up -d
bun run setup         # dotnet restore + tool restore + bun install
bun run backend       # API: http://localhost:5298 (dotnet watch)
bun run frontend      # SPA: http://localhost:5173 (vite)
```

* Panel admina: http://localhost:5173/admin/logowanie (dev: `admin` / `Admin123!`)
* Scalar (OpenAPI): http://localhost:5298/scalar/v1 (tylko Development)
* Mailpit (podgląd e-maili): http://localhost:8025

Testy: `bun run test` (integracyjne wymagają uruchomionego Dockera — Testcontainers).

## Płatności (Przelewy24)

* Dev/sandbox: załóż konto na https://sandbox.przelewy24.pl, wpisz
  `P24__MerchantId`, `P24__PosId`, `P24__CrcKey`, `P24__ApiKey` w
  `appsettings.Development.json` (albo zmienne środowiskowe).
* Webhook `POST /api/public/payments/p24/status` musi być osiągalny z internetu.
  Lokalnie użyj tunelu (np. `cloudflared tunnel --url http://localhost:5298`)
  i ustaw `P24__ApiBaseUrl` na adres tunelu; alternatywnie w panelu sandboxa
  możesz ręcznie ponowić notyfikację.
* Kwoty zawsze liczy serwer; webhook weryfikuje podpis SHA-384, kwotę i wykonuje
  `transaction/verify` — dopiero wtedy rezerwacja jest potwierdzana.

### Checklist go-live P24

1. Konto produkcyjne na https://przelewy24.pl zweryfikowane i aktywne.
2. W `.env`: `P24_MERCHANT_ID`, `P24_POS_ID`, `P24_CRC_KEY`, `P24_API_KEY`
   z panelu produkcyjnego oraz `P24_BASE_URL=https://secure.przelewy24.pl`.
3. `DOMAIN` wskazuje publicznie na serwer (webhook: `https://DOMAIN/api/public/payments/p24/status`).
4. Wykonaj przelew testowy 1 zł i sprawdź: status w panelu P24, e-mail
   potwierdzenia, status rezerwacji w panelu admina.

## Produkcja

```bash
cp .env.example .env   # uzupełnij DOMAIN, JWT__Key, POSTGRES_PASSWORD, ADMIN_PASSWORD, EMAIL_*, P24_*
bun run prod:up        # Caddy (auto-HTTPS) + frontend + api + postgres
```

Caddy sam wystawia certyfikat Let's Encrypt dla `DOMAIN` (porty 80/443 muszą
być przekierowane na serwer). Baza produkcyjna używa osobnego wolumenu
(`campcenter-db-prod`); kopia zapasowa: `mise run db:backup`.

## Stack

.NET 10 (Clean Architecture: Domain/Application/Infrastructure/Api) · EF Core +
PostgreSQL (optimistic concurrency przez xmin) · React 19 + Vite + Bun ·
react-i18next · MailKit · Przelewy24 REST · Caddy · Docker Compose ·
xUnit + Testcontainers. Zasady task runnerów i szczegóły domeny: `CLAUDE.md`.
````

## Jak to działa

1. Admin definiuje pokoje (liczba miejsc) i turnusy (daty, cena/os., zaliczka/os.),
   po czym publikuje turnus.
2. Rezerwujący podaje liczbę osób, wybiera turnus z wolnymi miejscami, koryguje
   proponowany podział pokoi i zostawia dane kontaktowe — na e-mail przychodzi
   link do zarządzania rezerwacją.
3. Zaliczka opłacona przez Przelewy24 potwierdza rezerwację (nieopłacone
   rezerwacje są zwalniane po 7 dniach); dopłata jest płatna do 30 dni przed
   rozpoczęciem turnusu.
4. Admin widzi obłożenie pokoi w każdym turnusie i dodaje zadania dla obsługi
   (np. dostawka), a pulpit pokazuje zaległe dopłaty i oczekujące zaliczki.

## Szybki start (dev)

```bash
cp .env.example .env
mise run db:up        # PostgreSQL + Mailpit (docker); bez mise: docker compose -f docker/docker-compose.infra.yml up -d
bun run setup         # dotnet restore + tool restore + bun install
bun run backend       # API: http://localhost:5298 (dotnet watch)
bun run frontend      # SPA: http://localhost:5173 (vite)
```

* Panel admina: http://localhost:5173/admin/logowanie (dev: `admin` / `Admin123!`)
* Scalar (OpenAPI): http://localhost:5298/scalar/v1 (tylko Development)
* Mailpit (podgląd e-maili): http://localhost:8025

Testy: `bun run test` (integracyjne wymagają uruchomionego Dockera — Testcontainers).

## Płatności (Przelewy24)

* Dev/sandbox: załóż konto na https://sandbox.przelewy24.pl, wpisz
  `P24__MerchantId`, `P24__PosId`, `P24__CrcKey`, `P24__ApiKey` w
  `appsettings.Development.json` (albo zmienne środowiskowe).
* Webhook `POST /api/public/payments/p24/status` musi być osiągalny z internetu.
  Lokalnie użyj tunelu (np. `cloudflared tunnel --url http://localhost:5298`)
  i ustaw `P24__ApiBaseUrl` na adres tunelu; alternatywnie w panelu sandboxa
  możesz ręcznie ponowić notyfikację.
* Kwoty zawsze liczy serwer; webhook weryfikuje podpis SHA-384, kwotę i wykonuje
  `transaction/verify` — dopiero wtedy rezerwacja jest potwierdzana.

### Checklist go-live P24

1. Konto produkcyjne na https://przelewy24.pl zweryfikowane i aktywne.
2. W `.env`: `P24_MERCHANT_ID`, `P24_POS_ID`, `P24_CRC_KEY`, `P24_API_KEY`
   z panelu produkcyjnego oraz `P24_BASE_URL=https://secure.przelewy24.pl`.
3. `DOMAIN` wskazuje publicznie na serwer (webhook: `https://DOMAIN/api/public/payments/p24/status`).
4. Wykonaj przelew testowy 1 zł i sprawdź: status w panelu P24, e-mail
   potwierdzenia, status rezerwacji w panelu admina.

## Produkcja

```bash
cp .env.example .env   # uzupełnij DOMAIN, JWT__Key, POSTGRES_PASSWORD, ADMIN_PASSWORD, EMAIL_*, P24_*
bun run prod:up        # Caddy (auto-HTTPS) + frontend + api + postgres
```

Caddy sam wystawia certyfikat Let's Encrypt dla `DOMAIN` (porty 80/443 muszą
być przekierowane na serwer). Baza produkcyjna używa osobnego wolumenu
(`campcenter-db-prod`); kopia zapasowa: `mise run db:backup`.

## Stack

.NET 10 (Clean Architecture: Domain/Application/Infrastructure/Api) · EF Core +
PostgreSQL (optimistic concurrency przez xmin) · React 19 + Vite + Bun ·
react-i18next · MailKit · Przelewy24 REST · Caddy · Docker Compose ·
xUnit + Testcontainers. Zasady task runnerów i szczegóły domeny: `CLAUDE.md`.
````

## Connections
- [[Booking Flow (reserve turnus, deposit confirms)]] - `references` [EXTRACTED]
- [[Dev Quickstart]] - `references` [EXTRACTED]
- [[Przelewy24 Payments Integration]] - `references` [EXTRACTED]

#graphify/document #graphify/EXTRACTED #community/Docker__Project_Docs