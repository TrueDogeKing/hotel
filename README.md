# CampCenter

Web application for a summer camp center: an informational site, stay bookings
for any dates for organized groups (no account required), an admin panel (rooms,
closures, occupancy, housekeeping tasks) and Przelewy24 online payments (deposit
+ balance). Bilingual PL/EN UI (flag switcher).

## How it works

1. The admin defines rooms (number of beds) and closures — date ranges when the
   whole center or a single room is unavailable (e.g. winter break, renovation).
   Pricing (price/person/night, deposit/person/night) is global in the config.
2. The guest picks arrival and departure dates plus the number of people, gets
   availability (free rooms and the computed price for the number of nights),
   adjusts the suggested room split and leaves contact details — a link to
   manage the booking is sent by e-mail.
3. A deposit paid via Przelewy24 confirms the booking (unpaid bookings are
   released after 7 days); the balance is due up to 30 days before the stay
   begins.
4. The admin sees room occupancy over a chosen date range (free / occupied /
   closed) and adds housekeeping tasks (e.g. an extra bed), while the dashboard
   shows overdue balances, pending deposits and active closures.

## Quick start (dev)

```bash
cp .env.example .env
mise run db:up        # PostgreSQL + Mailpit (docker); without mise: docker compose -f docker/docker-compose.infra.yml up -d
bun run setup         # dotnet restore + tool restore + bun install
bun run backend       # API: http://localhost:5298 (dotnet watch)
bun run frontend      # SPA: http://localhost:5173 (vite)
```

* Admin panel: http://localhost:5173/admin/logowanie (dev: `admin` / `Admin123!`)
* Scalar (OpenAPI): http://localhost:5298/scalar/v1 (Development only)
* Mailpit (e-mail preview): http://localhost:8025

Tests: `bun run test` (integration tests require a running Docker — Testcontainers).

## Payments (Przelewy24)

* Dev/sandbox: create an account at https://sandbox.przelewy24.pl, set
  `P24__MerchantId`, `P24__PosId`, `P24__CrcKey`, `P24__ApiKey` in
  `appsettings.Development.json` (or as environment variables).
* The webhook `POST /api/public/payments/p24/status` must be reachable from the
  internet. Locally, use a tunnel (e.g. `cloudflared tunnel --url http://localhost:5298`)
  and set `P24__ApiBaseUrl` to the tunnel address; alternatively, you can
  manually re-send the notification from the sandbox panel.
* Amounts are always computed by the server; the webhook verifies the SHA-384
  signature and the amount and calls `transaction/verify` — only then is the
  booking confirmed.

### P24 go-live checklist

1. Production account at https://przelewy24.pl verified and active.
2. In `.env`: `P24_MERCHANT_ID`, `P24_POS_ID`, `P24_CRC_KEY`, `P24_API_KEY`
   from the production panel, plus `P24_BASE_URL=https://secure.przelewy24.pl`.
3. `DOMAIN` points publicly to the server (webhook: `https://DOMAIN/api/public/payments/p24/status`).
4. Make a 1 PLN test transfer and check: the status in the P24 panel, the
   confirmation e-mail, and the booking status in the admin panel.

## Production

```bash
cp .env.example .env   # fill in DOMAIN, JWT__Key, POSTGRES_PASSWORD, ADMIN_PASSWORD, EMAIL_*, P24_*
bun run prod:up        # Caddy (auto-HTTPS) + frontend + api + postgres
```

Caddy issues a Let's Encrypt certificate for `DOMAIN` on its own (ports 80/443
must be forwarded to the server). The production database uses a separate volume
(`campcenter-db-prod`); backup: `mise run db:backup`.

## Stack

.NET 10 (Clean Architecture: Domain/Application/Infrastructure/Api) · EF Core +
PostgreSQL (optimistic concurrency via xmin) · React 19 + Vite + Bun ·
react-i18next · MailKit · Przelewy24 REST · Caddy · Docker Compose ·
xUnit + Testcontainers. Task-runner rules and domain details: `CLAUDE.md`.
