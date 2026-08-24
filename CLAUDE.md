# CampCenter

## Cel projektu

Aplikacja webowa ośrodka kolonijnego: strona informacyjna, rezerwacja pobytów
w dowolnym zakresie dat dla grup zorganizowanych (bez konta, potwierdzenie
e-mailem) oraz panel administratora (pokoje, blokady, obłożenie, zadania dla
obsługi). Płatności rozliczane poza systemem: właściciel odnotowuje je w panelu.

## Architektura

* Backend: ASP.NET Core Web API (.NET 10), Clean Architecture
  (CampCenter.Domain / Application / Infrastructure / Api)
* Frontend: React + Vite (SPA), i18n PL/EN (react-i18next, przełącznik flag)
* Baza danych: PostgreSQL, Entity Framework Core, optimistic concurrency (xmin)
* Autoryzacja: JWT (access token) + refresh token w cookie HttpOnly — tylko admini;
  rezerwujący nie mają kont (link zarządzania z tokenem w e-mailu)
* E-mail: MailKit/SMTP (dev: Mailpit, http://localhost:8025)
* Płatności: rozliczane poza aplikacją (przelew/gotówka). Właściciel ustawia
  stan rezerwacji jedną listą (BookingState: AwaitingPayment / DepositPaid /
  Paid / Cancelled / Completed — endpoint PUT /api/admin/bookings/{id}/state),
  która składa razem Status i PaymentState; odnotowanie wpłaty potwierdza
  rezerwację, wybór "anulowana" zwalnia pokoje i wysyła e-mail. Kod Przelewy24 jest zakomentowany, nie usunięty —
  PublicPaymentsController, akcja InitiatePayment, rejestracje IPaymentService /
  IPaymentGateway i binding P24Settings
* Reverse proxy / TLS: Caddy; środowisko: Docker Compose
* Kopie zapasowe (produkcja): usługa `db-backup` w `docker-compose.prod.yml`
  (ten sam obraz postgres:16-alpine, logika w `scripts/backup-db.sh`) — codzienny
  `pg_dump` o `BACKUP_TIME` do katalogu `BACKUP_DIR` na hoście, retencja
  `BACKUP_RETENTION_DAYS`, zrzut nadrabiający (sprawdzany co godzinę, także przy
  starcie), gdy najnowsza kopia jest starsza niż doba,
  healthcheck zgłasza brak świeżej kopii; `scripts/deploy.sh` robi dodatkowy
  zrzut przed przebudową (API migruje bazę przy starcie)

## Model domeny

* AdminUser + RefreshToken — logowanie panelu admina (seeder tworzy konto "admin")
* Room — pokój (numer, pojemność 2/3/4…, aktywny); zajętość wynika z zakresu
  dat przydziału pokoju do rezerwacji
* Closure (blokada) — zakres dat, w którym cały ośrodek (RoomId null) albo
  jeden pokój jest niedostępny; zastępuje dawny model turnusów
* Booking — rezerwacja grupowa (daty StartDate/EndDate, organizacja, kontakt,
  Headcount = uczestnicy + opiekunowie (SupervisorCount; CamperCount jest
  wyliczany), liczba osób, status PendingDeposit/Confirmed/Cancelled/Completed, token
  zarządzania hashowany, język pl/en, kwoty w groszach — snapshot). Cennik
  dwie stawki snapshotowane na rezerwacji i edytowalne per grupa — za uczestnika
  i za opiekuna (total = stawka uczestnika × uczestnicy × noce + stawka opiekuna
  × opiekunowie × noce, można nadpisać kwotą negocjowaną; zaliczka to jedna
  stawka od każdej osoby); PaymentState
  Unpaid/DepositPaid/Paid ustawiany ręcznie przez właściciela
* PricingDefaults — jednowierszowa tabela ze stawkami ośrodka (cena i zaliczka
  na osobę za noc), edytowana w panelu nad listą rezerwacji; wypełnia nową
  rezerwację, nie przelicza istniejących. Do czasu pierwszego zapisu obowiązują
  wartości z konfiguracji (sekcja "Booking")
* BookingRoomAssignment — konkretne pokoje przydzielone przy utworzeniu
  (IsSupervisorRoom oznacza pokoje kadry — kadra nigdy nie mieszka z
  uczestnikami; RoomMixCalculator.SuggestSplitMix sadza ją w najmniejszych
  wolnych pokojach, a gdy się nie da rozdzielić — odmawia), z
  zakresem dat; ograniczenie wykluczające GiST (btree_gist) na
  (RoomId, daterange[StartDate,EndDate)) chroni przed podwójną rezerwacją
  (zakresy półotwarte — wyjazd = przyjazd tego samego dnia nie kolidują)
* RoomTask — zadania dla obsługi (np. dostawka), Open/Done
* Payment — historyczne wpłaty P24 (tabela została, flow wyłączony); częściowy
  unikalny indeks (BookingId, Kind) WHERE Completed

## Knowledge graph / Obsidian vault (obowiązkowe)

Projekt ma zbudowany graf wiedzy w `graphify-out/` (graf: `graph.json`,
raport: `GRAPH_REPORT.md`, vault Obsidian: `graphify-out/obsidian/` —
~1000 notatek, jedna na plik/symbol, z frontmatterem `source_file`,
`community` i linkami `[[…]]`).

### Eksploracja przez vault (oszczędność tokenów)

Zanim zaczniesz czytać kod, orientuj się w projekcie przez graf — nie przez
rekurencyjne `Glob`/`Grep`/czytanie całych plików:

1. Pytanie o architekturę, przepływ danych, „gdzie jest X", „co wywołuje Y" →
   najpierw `graphify query "<pytanie>"` (uruchamiane z `D:\hotel`).
   Dla śledzenia konkretnej ścieżki: `graphify query "<pytanie>" --dfs`,
   a przy ograniczaniu odpowiedzi `--budget <tokeny>`.
2. Relacja między dwoma pojęciami: `graphify path "BookingService" "Payment"`.
   Wyjaśnienie jednego węzła: `graphify explain "BookingRoomAssignment"`.
   Analiza wpływu przed zmianą: `graphify affected "IBookingRepository"`.
3. Przegląd całości: `graphify-out/GRAPH_REPORT.md` (społeczności, god nodes).
4. Notatki w `graphify-out/obsidian/` czytaj punktowo (`Read`/`Grep` po tym
   katalogu) — zawierają fragment kodu + kontekst, więc są tańsze niż pełne
   pliki źródłowe.
5. Dopiero gdy graf wskaże konkretne `source_file`, otwieraj prawdziwy plik —
   i tylko te potrzebne fragmenty. Graf jest punktem wyjścia, nie źródłem
   prawdy: przed twierdzeniem o zachowaniu kodu zweryfikuj w pliku źródłowym.

### Aktualizacja po każdej zmianie (obowiązkowe)

Po każdej zmianie w kodzie lub dokumentacji — zanim uznasz zadanie za
zakończone — zaktualizuj graf i vault:

```bash
graphify update . && graphify export obsidian
```

`update` przelicza wyłącznie pliki nowe/zmienione (wg `manifest.json`) i nie
wymaga LLM, więc jest tanie; `export obsidian` odświeża notatki vaulta w
`graphify-out/obsidian/`. Dotyczy to również zmian w `CLAUDE.md` i `README.md`. Jeśli
aktualizacja się nie powiedzie, zgłoś to użytkownikowi zamiast przemilczeć —
nieaktualny vault jest gorszy niż jego brak.

## Wymagania bezpieczeństwa

* Hasła bcrypt; JWT + rotacja refresh tokenów z detekcją ponownego użycia
* Rate limiting globalny per-IP + zaostrzony na auth i publiczne endpointy rezerwacji
* Walidacja FluentValidation; kwoty płatności zawsze liczone po stronie serwera
* Webhook P24 (wyłączony, kod zakomentowany): weryfikacja podpisu SHA-384 +
  kwoty + transaction/verify, idempotentny

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
