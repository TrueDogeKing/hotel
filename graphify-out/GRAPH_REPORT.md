# Graph Report - hotel  (2026-07-30)

## Corpus Check
- 246 files · ~79,769 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2174 nodes · 5140 edges · 113 communities (75 shown, 38 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 307 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `2fcdd9f6`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DTOs / Schedule (1)
- Room Management
- Admin Bookings Controller & DTOs
- src / api (1)
- CampCenter.UnitTests / Validators
- Room Task Management (1)
- CampCenter.Application / Services (1)
- Room Mix Calculator Tests
- Payment Gateway Integration Tests (1)
- Project & NuGet Config
- Frontend Icon Components
- CampCenter.Infrastructure / Repositories (1)
- CampCenter.Application / Services (2)
- Public Booking Frontend (1)
- CampCenter.UnitTests / Services (1)
- Admin Booking & Notifications (1)
- Room Closure Management
- tests / CampCenter.IntegrationTests (1)
- Domain & Infra Namespaces
- src / utils
- Integration Test Harness (1)
- CampCenter.Application / Services (3)
- CampCenter.Domain / Repositories (1)
- Camp Session Management
- Integration Test Harness (2)
- Frontend Auth & API Client
- CampCenter.Domain / Repositories (2)
- Booking Persistence & Entities (1)
- Application Namespaces & DTOs
- Admin Booking & Notifications (2)
- Public Booking Service (1)
- Public Booking Service (2)
- Payment Gateway Integration Tests (2)
- Docker & Project Docs
- Rate Limiting & Startup
- components / admin
- Validator Unit Tests
- CampCenter.UnitTests / Services (2)
- TypeScript App Config
- Admin User & Token Config
- TypeScript Node Config
- Auth Service & Tokens
- Root Task-Runner Scripts
- CampCenter.UnitTests / Services (3)
- CampCenter.UnitTests / Services (4)
- Booking Persistence & Entities (2)
- JWT Token Service
- tests / CampCenter.IntegrationTests (2)
- Frontend App Shell & i18n
- Auth Controller (1)
- Admin Frontend Pages
- Refresh Token Repository
- Admin Booking & Notifications (3)
- Room Task Management (2)
- API Launch Settings
- Global Exception Handler
- ESLint Dev Dependencies
- Admin Booking & Notifications (4)
- Persistence / Configurations
- DTOs / AdminPanel
- Frontend Runtime Deps
- CampCenter.Application / Services (4)
- CampCenter.Domain / Entities
- Domain Exceptions
- src / api (2)
- Admin Tasks & Occupancy Pages
- Payment
- 20260728105506_PerGroupMealTimes.Designer.cs
- 20260721111400_ReplaceSessionsWithClosures.Designer.cs
- OpenAPI Security Scheme
- EF Core Migrations (1)
- Booking Maintenance Background Service
- CampCenter.UnitTests / Services (5)
- Auth Controller (2)
- Przelewy24 Payment Client
- Frontend Build Scripts
- Social Icon Sprite
- Password Hashing (bcrypt)
- Claims Principal Extensions
- Frontend Package Manifest
- EF Core Migrations (2)
- EF Core Migrations (3)
- DTOs / Schedule (2)
- EF Core Migrations (4)
- Persistence / Migrations (1)
- Persistence / Migrations (2)
- Persistence / Migrations (3)
- Persistence / Migrations (4)
- Persistence / Migrations (5)
- Persistence / Migrations (6)
- Persistence / Migrations (7)
- Login Normalizer
- Frontend API Error Handling
- Select Component
- EF Core Migrations (5)
- Persistence / Migrations (9)
- Persistence / Migrations (10)
- Persistence / Migrations (12)
- Persistence / Migrations (13)
- frontend (1)
- Root TS Config
- React Hooks ESLint Plugin
- Prettier Dependency
- Node Type Definitions
- React DOM Type Definitions
- TypeScript Dependency
- Prettier Config
- App Brand Identity
- frontend (3)
- src / assets (1)
- src / assets (2)

## God Nodes (most connected - your core abstractions)
1. `Booking` - 78 edges
2. `CampCenter.Domain.Entities` - 74 edges
3. `CampCenter.Application.Interfaces` - 51 edges
4. `ScheduleService` - 36 edges
5. `CampCenter.Domain.Repositories` - 36 edges
6. `IBookingRepository` - 30 edges
7. `AdminBookingService` - 29 edges
8. `MealTimeDefault` - 29 edges
9. `ScheduleEntry` - 28 edges
10. `AppDbContext` - 28 edges

## Surprising Connections (you probably didn't know these)
- `CampCenterApiFactory` --references--> `Program`  [EXTRACTED]
  tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs → src/CampCenter.Api/Program.cs
- `ScheduleConflictTests` --references--> `ScheduleService`  [EXTRACTED]
  tests/CampCenter.UnitTests/Services/ScheduleConflictTests.cs → src/CampCenter.Application/Services/ScheduleService.cs
- `HousekeepingServiceTests` --references--> `IBookingRepository`  [EXTRACTED]
  tests/CampCenter.UnitTests/Services/HousekeepingServiceTests.cs → src/CampCenter.Domain/Repositories/IBookingRepository.cs
- `HousekeepingServiceTests` --references--> `IClosureRepository`  [EXTRACTED]
  tests/CampCenter.UnitTests/Services/HousekeepingServiceTests.cs → src/CampCenter.Domain/Repositories/IClosureRepository.cs
- `HousekeepingServiceTests` --references--> `IRoomRepository`  [EXTRACTED]
  tests/CampCenter.UnitTests/Services/HousekeepingServiceTests.cs → src/CampCenter.Domain/Repositories/IRoomRepository.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Booking Lifecycle (availability → deposit → confirmation)** — readme_booking_flow, readme_p24_payments, claude_gist_double_booking_guard, claude_domain_model [INFERRED 0.85]
- **Project Conventions (task runners, build env, knowledge graph)** — claude_task_runner_rules, claude_build_environment, claude_knowledge_graph_workflow [EXTRACTED 1.00]
- **Production Stack (Caddy -> frontend/api -> PostgreSQL)** — docker_docker_compose_prod_caddy, docker_docker_compose_prod_api, docker_docker_compose_prod_postgres [EXTRACTED 1.00]
- **CI Validation Pipeline (backend + frontend)** — github_workflows_ci_workflow, github_workflows_ci_backend_job, github_workflows_ci_frontend_job [EXTRACTED 1.00]

## Communities (113 total, 38 thin omitted)

### Community 0 - "DTOs / Schedule (1)"
Cohesion: 0.09
Nodes (36): ScheduleController, CancellationToken, DateOnly, Guid, HttpDelete, HttpGet, HttpPost, HttpPut (+28 more)

### Community 1 - "Room Management"
Cohesion: 0.06
Nodes (45): RoomsController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+37 more)

### Community 2 - "Admin Bookings Controller & DTOs"
Cohesion: 0.07
Nodes (45): ControllerBase, BookingsController, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IActionResult (+37 more)

### Community 3 - "src / api (1)"
Cohesion: 0.07
Nodes (41): AdminAssignment, ApplyBookingMealTimeResult, BookingScheduleDay, Closure, ClosureInput, CreateAdminBookingInput, createClosure(), createRoom() (+33 more)

### Community 4 - "CampCenter.UnitTests / Validators"
Cohesion: 0.15
Nodes (19): HousekeepingController, CancellationToken, DateOnly, Guid, HttpGet, HttpPut, IActionResult, ProducesResponseType (+11 more)

### Community 5 - "Room Task Management (1)"
Cohesion: 0.08
Nodes (36): TasksController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, IActionResult, ProducesResponseType (+28 more)

### Community 6 - "CampCenter.Application / Services (1)"
Cohesion: 0.19
Nodes (5): CampCenter.Application, CampCenter.Application.Services, CampCenter.UnitTests.Services, DependencyInjection, IServiceCollection

### Community 7 - "Room Mix Calculator Tests"
Cohesion: 0.18
Nodes (8): PeopleCount, RoomMixCalculator, Capacity, Dictionary, IReadOnlyDictionary, List, RoomMixCalculatorTests, Fact

### Community 8 - "Payment Gateway Integration Tests (1)"
Cohesion: 0.18
Nodes (11): Amount, OrderId, SessionId, GatewayRegisterRequest, GatewayRegisterResult, IPaymentGateway, CancellationToken, Task (+3 more)

### Community 9 - "Project & NuGet Config"
Cohesion: 0.05
Nodes (38): BCrypt.Net-Next (4.2.0), FluentValidation (12.1.1), FluentValidation.DependencyInjectionExtensions (12.1.1), MailKit (4.14.1), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9), Microsoft.AspNetCore.Mvc.Testing (10.0.9), Microsoft.AspNetCore.OpenApi (10.0.9), Microsoft.Extensions.Http (10.0.0) (+30 more)

### Community 10 - "Frontend Icon Components"
Cohesion: 0.08
Nodes (20): IconArrowRight(), IconBed(), IconMail(), IconMap(), IconMapPin(), IconMoon(), IconPhone(), IconSun() (+12 more)

### Community 11 - "CampCenter.Infrastructure / Repositories (1)"
Cohesion: 0.19
Nodes (15): ScheduleEntryRepository, BookingId, CancellationToken, Count, Date, DateOnly, End, Guid (+7 more)

### Community 12 - "CampCenter.Application / Services (2)"
Cohesion: 0.06
Nodes (52): Skipped, BookingMealTimeDto, ScheduleEntryDto, ScheduleSettings, string, TimeOnly, ScheduleService, CancellationToken (+44 more)

### Community 13 - "Public Booking Frontend (1)"
Cohesion: 0.09
Nodes (27): Availability, BookingDetails, BookingPayment, cancelBooking(), createBooking(), CreateBookingInput, CreateBookingResult, getAvailability() (+19 more)

### Community 14 - "CampCenter.UnitTests / Services (1)"
Cohesion: 0.06
Nodes (38): Slot, MealTimeDefaultDto, MealGenerationPlanner, MealSlot, Date, DateOnly, End, IEnumerable (+30 more)

### Community 15 - "Admin Booking & Notifications (1)"
Cohesion: 0.11
Nodes (22): AdminBookingDto, AdminBookingService, CancellationToken, DateOnly, DateTime, Guid, ILogger, IReadOnlyDictionary (+14 more)

### Community 16 - "Room Closure Management"
Cohesion: 0.05
Nodes (52): ClosuresController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+44 more)

### Community 17 - "tests / CampCenter.IntegrationTests (1)"
Cohesion: 0.05
Nodes (47): IAsyncLifetime, ICollectionFixture, IServiceProvider, IWebHostBuilder, PostgreSqlContainer, DataSeeder, CancellationToken, Task (+39 more)

### Community 18 - "Domain & Infra Namespaces"
Cohesion: 0.18
Nodes (5): CampCenter.Api.Background, CampCenter.Infrastructure.Repositories, CampCenter.Domain.Entities, CampCenter.Domain.Repositories, CampCenter.Infrastructure.Persistence

### Community 19 - "src / utils"
Cohesion: 0.14
Nodes (28): getScheduleCalendar(), getScheduleDay(), ScheduleCalendar, CalendarTile(), Props, groupHue(), LaneEvent, packLanes() (+20 more)

### Community 20 - "Integration Test Harness (1)"
Cohesion: 0.26
Nodes (4): CampCenter.IntegrationTests, CampCenter.Application.DTOs.Rooms, CampCenter.Application.DTOs.Public, CampCenter.Application.DTOs.Closures

### Community 21 - "CampCenter.Application / Services (3)"
Cohesion: 0.18
Nodes (12): BookingMealTime, deleteBookingMeals(), getBookingMealTimes(), NeighbourSitting, resetBookingMealTime(), setBookingMealTime(), clashingNeighbours(), GroupMealTimes() (+4 more)

### Community 22 - "CampCenter.Domain / Repositories (1)"
Cohesion: 0.31
Nodes (8): Registered, PaymentsApiTests, Fact, HttpClient, int, long, Task, Token

### Community 23 - "Camp Session Management"
Cohesion: 0.26
Nodes (4): GatewayNotification, P24SignCalculator, P24SignCalculatorTests, Fact

### Community 24 - "Integration Test Harness (2)"
Cohesion: 0.32
Nodes (7): RoomCleaningRepository, CancellationToken, DateOnly, Dictionary, Guid, List, Task

### Community 25 - "Frontend Auth & API Client"
Cohesion: 0.18
Nodes (21): login(), logout(), api, refreshAccessToken(), RetriableConfig, decodeJWT(), getUserIdFromToken(), getUserLoginFromToken() (+13 more)

### Community 26 - "CampCenter.Domain / Repositories (2)"
Cohesion: 0.24
Nodes (8): RoomCleaning, RoomCleaningKind, RoomCleaningStatus, DateOnly, DateTime, Guid, RoomCleaningConfiguration, EntityTypeBuilder

### Community 27 - "Booking Persistence & Entities (1)"
Cohesion: 0.22
Nodes (6): DbContext, DbSet, IDesignTimeDbContextFactory, AppDbContext, ModelBuilder, DesignTimeDbContextFactory

### Community 28 - "Application Namespaces & DTOs"
Cohesion: 0.17
Nodes (6): CampCenter.Api.Controllers.Admin, CampCenter.Application.DTOs.AdminPanel, CampCenter.Application.Interfaces, CampCenter.Application.DTOs.Schedule, CampCenter.Api.Extensions, RoomDeleteResultDto

### Community 29 - "Admin Booking & Notifications (2)"
Cohesion: 0.24
Nodes (7): P24Settings, string, Przelewy24Client, CancellationToken, HttpClient, string, Task

### Community 30 - "Public Booking Service (1)"
Cohesion: 0.05
Nodes (45): CampCenter.Infrastructure.Email, PublicBookingsController, CancellationToken, EnableRateLimiting, HttpGet, HttpPost, IActionResult, IValidator (+37 more)

### Community 31 - "Public Booking Service (2)"
Cohesion: 0.13
Nodes (7): CampCenter.Api.RateLimiting, CampCenter.Api.Errors, CampCenter.Api.Controllers.Public, CampCenter.Infrastructure.Persistence.Seed, Program, RateLimitPolicies, string

### Community 32 - "Payment Gateway Integration Tests (2)"
Cohesion: 0.32
Nodes (7): AssignableRoom, getAdminBooking(), getAssignableRooms(), reassignBooking(), Draft, GroupRooms(), Props

### Community 33 - "Docker & Project Docs"
Cohesion: 0.05
Nodes (43): Aktualizacja po każdej zmianie (obowiązkowe), Architektura, Build environment, Bun responsibilities, CampCenter, Cel projektu, Closure Model Replaces Camp Sessions, Decision rule (+35 more)

### Community 34 - "Rate Limiting & Startup"
Cohesion: 0.08
Nodes (14): CampCenter.Application.Validators, CampCenter.UnitTests.Validators, InlineData, IRuleBuilder, IRuleBuilderOptions, CreateBookingRequestValidator, LoginRequestValidator, PasswordRules (+6 more)

### Community 35 - "components / admin"
Cohesion: 0.27
Nodes (12): ScheduleDay, buildChips(), Chip, ClashReason, DayTimetable(), findClashes(), groupsOf(), minutesOf() (+4 more)

### Community 36 - "Validator Unit Tests"
Cohesion: 0.25
Nodes (5): IEntityTypeConfiguration, AdminUserConfiguration, EntityTypeBuilder, ClosureConfiguration, EntityTypeBuilder

### Community 37 - "CampCenter.UnitTests / Services (2)"
Cohesion: 0.05
Nodes (52): HousekeepingJob, HousekeepingPlanner, DateOnly, IEnumerable, List, HousekeepingService, CancellationToken, DateOnly (+44 more)

### Community 38 - "TypeScript App Config"
Cohesion: 0.09
Nodes (22): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection, moduleResolution (+14 more)

### Community 39 - "Admin User & Token Config"
Cohesion: 0.18
Nodes (11): AdminUser, DateTime, Guid, IAdminUserRepository, CancellationToken, Guid, Task, AdminUserRepository (+3 more)

### Community 40 - "TypeScript Node Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, moduleResolution, noEmit (+12 more)

### Community 41 - "Auth Service & Tokens"
Cohesion: 0.18
Nodes (12): ITokenService, AuthResult, AuthService, CancellationToken, DateTime, Guid, Task, IRefreshTokenRepository (+4 more)

### Community 42 - "Root Task-Runner Scripts"
Cohesion: 0.10
Nodes (19): description, name, private, scripts, backend, build, dev, dev:down (+11 more)

### Community 43 - "CampCenter.UnitTests / Services (3)"
Cohesion: 0.39
Nodes (5): InitiatePaymentRequestDto, InitiatePaymentResponseDto, IPaymentService, CancellationToken, Task

### Community 44 - "CampCenter.UnitTests / Services (4)"
Cohesion: 0.42
Nodes (5): ScheduleConflictTests, DateOnly, Fact, Guid, Task

### Community 45 - "Booking Persistence & Entities (2)"
Cohesion: 0.33
Nodes (5): CancellationToken, HttpPost, IActionResult, ProducesResponseType, Task

### Community 46 - "JWT Token Service"
Cohesion: 0.14
Nodes (8): AccessToken, RefreshTokenInfo, JwtSettings, string, JwtTokenService, int, RefreshTokenSettings, string

### Community 48 - "Frontend App Shell & i18n"
Cohesion: 0.18
Nodes (16): AdminBooking, BookingStatus, bookingStatuses, cancelAdminBooking(), createAdminBooking(), Dashboard, formatZl(), getAdminBookings() (+8 more)

### Community 49 - "Auth Controller (1)"
Cohesion: 0.25
Nodes (10): CookieOptions, AuthController, CancellationToken, DateTime, EnableRateLimiting, HttpPost, IActionResult, IValidator (+2 more)

### Community 51 - "Refresh Token Repository"
Cohesion: 0.24
Nodes (8): RefreshToken, DateTime, Guid, RefreshTokenRepository, CancellationToken, DateTime, Guid, Task

### Community 54 - "API Launch Settings"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 55 - "Global Exception Handler"
Cohesion: 0.17
Nodes (11): Detail, HttpContext, IExceptionHandler, IProblemDetailsService, GlobalExceptionHandler, CancellationToken, Exception, ILogger (+3 more)

### Community 56 - "ESLint Dev Dependencies"
Cohesion: 0.13
Nodes (15): eslint, @eslint/js, eslint-plugin-react-refresh, devDependencies, eslint, @eslint/js, eslint-plugin-react-refresh, globals (+7 more)

### Community 57 - "Admin Booking & Notifications (4)"
Cohesion: 0.31
Nodes (6): BookingSettings, string, PaymentService, CancellationToken, ILogger, Task

### Community 58 - "Persistence / Configurations"
Cohesion: 0.20
Nodes (5): CampCenter.Infrastructure.Persistence.Configurations, MealTimeDefaultConfiguration, EntityTypeBuilder, RefreshTokenConfiguration, EntityTypeBuilder

### Community 60 - "Frontend Runtime Deps"
Cohesion: 0.15
Nodes (13): axios, dependencies, axios, i18next, react, react-dom, react-i18next, react-router-dom (+5 more)

### Community 63 - "Domain Exceptions"
Cohesion: 0.19
Nodes (7): CampCenter.Domain.Exceptions, Exception, BusinessRuleViolationException, ConcurrencyConflictException, ConflictException, ForbiddenActionException, NotFoundException

### Community 64 - "src / api (2)"
Cohesion: 0.09
Nodes (31): BookingSchedule, checkScheduleConflicts(), createMealTime(), createScheduleEntry(), deleteMealTime(), deleteScheduleEntry(), getBookingSchedule(), getMealTimes() (+23 more)

### Community 65 - "Admin Tasks & Occupancy Pages"
Cohesion: 0.15
Nodes (16): createTask(), deleteTask(), getOccupancy(), getTasks(), Occupancy, RoomOccupancy, RoomTask, setTaskDone() (+8 more)

### Community 70 - "OpenAPI Security Scheme"
Cohesion: 0.20
Nodes (8): CampCenter.Api.OpenApi, IOpenApiDocumentTransformer, OpenApiDocument, OpenApiDocumentTransformerContext, BearerSecuritySchemeTransformer, CancellationToken, string, Task

### Community 71 - "EF Core Migrations (1)"
Cohesion: 0.20
Nodes (5): CampCenter.Infrastructure.Persistence.Migrations, InitialAuth, ModelBuilder, RoomCleanings, ModelBuilder

### Community 73 - "Booking Maintenance Background Service"
Cohesion: 0.31
Nodes (7): BackgroundService, IServiceScopeFactory, BookingMaintenanceService, CancellationToken, ILogger, Task, TimeSpan

### Community 75 - "CampCenter.UnitTests / Services (5)"
Cohesion: 0.18
Nodes (5): CampCenter.Application.Models, CampCenter.Infrastructure.Auth, CampCenter.Api.Controllers, CampCenter.Application.DTOs.Auth, LoginResponseDto

### Community 76 - "Auth Controller (2)"
Cohesion: 0.39
Nodes (4): LoginRequestDto, IAuthService, CancellationToken, Task

### Community 78 - "Przelewy24 Payment Client"
Cohesion: 0.15
Nodes (8): CampCenter.Infrastructure.Payments, CampCenter.Infrastructure, IConfiguration, RegisterData, DependencyInjection, IServiceCollection, RegisterData, RegisterResponse

### Community 79 - "Frontend Build Scripts"
Cohesion: 0.29
Nodes (7): scripts, build, dev, format, format:check, lint, preview

### Community 80 - "Social Icon Sprite"
Cohesion: 0.38
Nodes (7): Bluesky Icon, Discord Icon, Documentation Icon, GitHub Icon, Social Icon, Icon Sprite Sheet, X (Twitter) Icon

### Community 82 - "Claims Principal Extensions"
Cohesion: 0.40
Nodes (3): ClaimsPrincipal, ClaimsPrincipalExtensions, Guid

### Community 83 - "Frontend Package Manifest"
Cohesion: 0.40
Nodes (4): name, private, type, version

### Community 84 - "EF Core Migrations (2)"
Cohesion: 0.50
Nodes (3): Migration, CoreDomain, MigrationBuilder

### Community 85 - "EF Core Migrations (3)"
Cohesion: 0.40
Nodes (3): ModelSnapshot, AppDbContextModelSnapshot, ModelBuilder

### Community 86 - "DTOs / Schedule (2)"
Cohesion: 0.06
Nodes (33): AbstractValidator, MealTimesController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut (+25 more)

### Community 106 - "frontend (1)"
Cohesion: 0.67
Nodes (3): Lakeside Typography (Bricolage Grotesque + Inter), SPA HTML Shell, Pre-Paint Theme Restore

## Knowledge Gaps
- **231 isolated node(s):** `printWidth`, `name`, `version`, `private`, `type` (+226 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **38 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `CampCenter.Application.Interfaces` connect `Application Namespaces & DTOs` to `Room Management`, `CampCenter.Application / Services (1)`, `Payment Gateway Integration Tests (1)`, `CampCenter.UnitTests / Services (5)`, `CampCenter.UnitTests / Services (3)`, `Przelewy24 Payment Client`, `Room Closure Management`, `Password Hashing (bcrypt)`, `Domain & Infra Namespaces`, `Integration Test Harness (1)`, `Domain Exceptions`, `Public Booking Service (1)`, `Public Booking Service (2)`?**
  _High betweenness centrality (0.080) - this node is a cross-community bridge._
- **Why does `CampCenter.Domain.Entities` connect `Domain & Infra Namespaces` to `Room Management`, `Room Task Management (1)`, `CampCenter.Application / Services (1)`, `CampCenter.Application / Services (2)`, `CampCenter.UnitTests / Services (1)`, `Admin Booking & Notifications (1)`, `Room Closure Management`, `Integration Test Harness (1)`, `CampCenter.Domain / Repositories (2)`, `Booking Persistence & Entities (1)`, `Application Namespaces & DTOs`, `Public Booking Service (2)`, `Rate Limiting & Startup`, `Validator Unit Tests`, `CampCenter.UnitTests / Services (2)`, `Admin User & Token Config`, `tests / CampCenter.IntegrationTests (2)`, `Admin Frontend Pages`, `Refresh Token Repository`, `Admin Booking & Notifications (3)`, `Room Task Management (2)`, `Persistence / Configurations`, `DTOs / AdminPanel`, `CampCenter.Application / Services (4)`, `Domain Exceptions`, `Payment`, `CampCenter.UnitTests / Services (5)`, `DTOs / Schedule (2)`?**
  _High betweenness centrality (0.079) - this node is a cross-community bridge._
- **Why does `CampCenter.Infrastructure.Persistence` connect `Domain & Infra Namespaces` to `EF Core Migrations (5)`, `20260721111400_ReplaceSessionsWithClosures.Designer.cs`, `Persistence / Migrations (9)`, `Persistence / Migrations (10)`, `EF Core Migrations (1)`, `20260728105506_PerGroupMealTimes.Designer.cs`, `Persistence / Migrations (12)`, `Persistence / Migrations (13)`, `Przelewy24 Payment Client`, `EF Core Migrations (3)`, `Booking Persistence & Entities (1)`, `Public Booking Service (2)`?**
  _High betweenness centrality (0.066) - this node is a cross-community bridge._
- **What connects `printWidth`, `name`, `version` to the rest of the system?**
  _231 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DTOs / Schedule (1)` be split into smaller, more focused modules?**
  _Cohesion score 0.09090909090909091 - nodes in this community are weakly interconnected._
- **Should `Room Management` be split into smaller, more focused modules?**
  _Cohesion score 0.05737234652897304 - nodes in this community are weakly interconnected._
- **Should `Admin Bookings Controller & DTOs` be split into smaller, more focused modules?**
  _Cohesion score 0.068997668997669 - nodes in this community are weakly interconnected._