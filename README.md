# CampCenter

Web application for a summer camp center: an informational site, stay bookings
for any dates for organized groups (no account required), an admin panel (rooms,
closures, occupancy, housekeeping tasks). Payment is settled directly with the
centre and recorded by the owner. Bilingual PL/EN UI (flag switcher).

## How it works

1. The admin defines rooms (number of beds) and closures — date ranges when the
   whole center or a single room is unavailable (e.g. winter break, renovation).
   Pricing (price/person/night, deposit/person/night) is set in the panel, above
   the bookings list; each booking keeps its own copy and can be re-priced.
2. The guest picks arrival and departure dates plus the number of people, gets
   availability (free rooms and the computed price for the number of nights),
   adjusts the suggested room split and leaves contact details — a link to
   manage the booking is sent by e-mail.
3. The group pays the centre directly; the owner records it on the booking
   (awaiting payment / deposit paid / paid in full). Recording a payment confirms
   the booking (bookings with nothing recorded are released after 7 days); the
   balance is due up to 30 days before the stay begins.
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

## Payments

Money changes hands outside the application — by transfer or in cash — and the
owner records what arrived on each booking (`Unpaid` / `DepositPaid` / `Paid`).
Recording a payment confirms a booking that was waiting on its deposit.

The Przelewy24 integration is switched off, not deleted: `PublicPaymentsController`,
the `InitiatePayment` action, the `IPaymentService` / `IPaymentGateway`
registrations and the `P24Settings` binding are all commented out, and
`PaymentsApiTests` is skipped. `PaymentService`, `Przelewy24Client` and the
`Payments` table are untouched, so bringing card payment back is a matter of
uncommenting those five places.

## Production

```bash
cp .env.example .env   # fill in DOMAIN, JWT__Key, POSTGRES_PASSWORD, ADMIN_PASSWORD, EMAIL_*
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
