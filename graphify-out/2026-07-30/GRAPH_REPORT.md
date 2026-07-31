# Graph Report - hotel  (2026-07-30)

## Corpus Check
- 249 files · ~82,399 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2205 nodes · 5221 edges · 107 communities (75 shown, 32 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 310 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `4716a9e3`
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
- Domain Exceptions
- src / api (2)
- Admin Tasks & Occupancy Pages
- Payment
- 20260728105506_PerGroupMealTimes.Designer.cs
- OpenAPI Security Scheme
- EF Core Migrations (1)
- Booking Maintenance Background Service
- PasswordRules
- Frontend Build Scripts
- Social Icon Sprite
- Password Hashing (bcrypt)
- Claims Principal Extensions
- Frontend Package Manifest
- EF Core Migrations (2)
- EF Core Migrations (3)
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
1. `Booking` - 80 edges
2. `CampCenter.Domain.Entities` - 76 edges
3. `CampCenter.Application.Interfaces` - 51 edges
4. `ScheduleService` - 36 edges
5. `CampCenter.Domain.Repositories` - 36 edges
6. `AdminBookingService` - 31 edges
7. `IBookingRepository` - 31 edges
8. `MealTimeDefault` - 29 edges
9. `ScheduleEntry` - 28 edges
10. `AppDbContext` - 28 edges

## Surprising Connections (you probably didn't know these)
- `CampCenterApiFactory` --references--> `Program`  [EXTRACTED]
  tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs → src/CampCenter.Api/Program.cs
- `ScheduleConflictTests` --references--> `ScheduleService`  [EXTRACTED]
  tests/CampCenter.UnitTests/Services/ScheduleConflictTests.cs → src/CampCenter.Application/Services/ScheduleService.cs
- `ScheduleEntryValidatorsTests` --references--> `CreateScheduleEntryRequestValidator`  [EXTRACTED]
  tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs → src/CampCenter.Application/Validators/ScheduleValidators.cs
- `HousekeepingServiceTests` --references--> `IBookingRepository`  [EXTRACTED]
  tests/CampCenter.UnitTests/Services/HousekeepingServiceTests.cs → src/CampCenter.Domain/Repositories/IBookingRepository.cs
- `HousekeepingServiceTests` --references--> `IClosureRepository`  [EXTRACTED]
  tests/CampCenter.UnitTests/Services/HousekeepingServiceTests.cs → src/CampCenter.Domain/Repositories/IClosureRepository.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Booking Lifecycle (availability → deposit → confirmation)** — readme_booking_flow, readme_p24_payments, claude_gist_double_booking_guard, claude_domain_model [INFERRED 0.85]
- **Project Conventions (task runners, build env, knowledge graph)** — claude_task_runner_rules, claude_build_environment, claude_knowledge_graph_workflow [EXTRACTED 1.00]
- **Production Stack (Caddy -> frontend/api -> PostgreSQL)** — docker_docker_compose_prod_caddy, docker_docker_compose_prod_api, docker_docker_compose_prod_postgres [EXTRACTED 1.00]
- **CI Validation Pipeline (backend + frontend)** — github_workflows_ci_workflow, github_workflows_ci_backend_job, github_workflows_ci_frontend_job [EXTRACTED 1.00]

## Communities (107 total, 32 thin omitted)

### Community 0 - "DTOs / Schedule (1)"
Cohesion: 0.06
Nodes (48): AbstractValidator, ScheduleController, CancellationToken, DateOnly, Guid, HttpDelete, HttpGet, HttpPost (+40 more)

### Community 1 - "Room Management"
Cohesion: 0.07
Nodes (38): RoomsController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+30 more)

### Community 2 - "Admin Bookings Controller & DTOs"
Cohesion: 0.07
Nodes (42): ControllerBase, BookingsController, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IActionResult (+34 more)

### Community 3 - "src / api (1)"
Cohesion: 0.14
Nodes (16): Closure, createClosure(), createRoom(), deleteClosure(), deleteRoom(), getClosures(), getRooms(), Room (+8 more)

### Community 4 - "CampCenter.UnitTests / Validators"
Cohesion: 0.15
Nodes (14): DateOnly, IEnumerable, List, Booking, BookingCancelReason, BookingStatus, DateOnly, DateTime (+6 more)

### Community 5 - "Room Task Management (1)"
Cohesion: 0.08
Nodes (37): TasksController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, IActionResult, ProducesResponseType (+29 more)

### Community 6 - "CampCenter.Application / Services (1)"
Cohesion: 0.12
Nodes (8): CampCenter.Infrastructure.Auth, CampCenter.Application, CampCenter.Api.Controllers, CampCenter.Infrastructure, CampCenter.Infrastructure.Persistence.Seed, Program, DependencyInjection, IServiceCollection

### Community 7 - "Room Mix Calculator Tests"
Cohesion: 0.18
Nodes (8): PeopleCount, RoomMixCalculator, Capacity, Dictionary, IReadOnlyDictionary, List, RoomMixCalculatorTests, Fact

### Community 8 - "Payment Gateway Integration Tests (1)"
Cohesion: 0.07
Nodes (30): Amount, OrderId, Registered, SessionId, GatewayNotification, GatewayRegisterRequest, GatewayRegisterResult, IPaymentGateway (+22 more)

### Community 9 - "Project & NuGet Config"
Cohesion: 0.05
Nodes (38): BCrypt.Net-Next (4.2.0), FluentValidation (12.1.1), FluentValidation.DependencyInjectionExtensions (12.1.1), MailKit (4.14.1), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9), Microsoft.AspNetCore.Mvc.Testing (10.0.9), Microsoft.AspNetCore.OpenApi (10.0.9), Microsoft.Extensions.Http (10.0.0) (+30 more)

### Community 10 - "Frontend Icon Components"
Cohesion: 0.06
Nodes (28): Tile, TILES, IconArrowRight(), IconBed(), IconCalendar(), IconCheckSquare(), IconClipboard(), IconGrid() (+20 more)

### Community 11 - "CampCenter.Infrastructure / Repositories (1)"
Cohesion: 0.19
Nodes (15): ScheduleEntryRepository, BookingId, CancellationToken, Count, Date, DateOnly, End, Guid (+7 more)

### Community 12 - "CampCenter.Application / Services (2)"
Cohesion: 0.07
Nodes (42): Skipped, BookingMealTimeDto, ScheduleEntryDto, ScheduleSettings, string, TimeOnly, ScheduleService, CancellationToken (+34 more)

### Community 13 - "Public Booking Frontend (1)"
Cohesion: 0.10
Nodes (29): formatZl(), Availability, BookingDetails, BookingPayment, cancelBooking(), createBooking(), CreateBookingInput, CreateBookingResult (+21 more)

### Community 14 - "CampCenter.UnitTests / Services (1)"
Cohesion: 0.17
Nodes (11): Slot, MealSlot, Date, DateOnly, IEnumerable, IReadOnlyList, List, MealGenerationPlannerTests (+3 more)

### Community 15 - "Admin Booking & Notifications (1)"
Cohesion: 0.23
Nodes (11): AdminBookingDto, AdminBookingService, CancellationToken, DateOnly, DateTime, Guid, ILogger, int (+3 more)

### Community 16 - "Room Closure Management"
Cohesion: 0.05
Nodes (52): ClosuresController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+44 more)

### Community 17 - "tests / CampCenter.IntegrationTests (1)"
Cohesion: 0.07
Nodes (37): ICollectionFixture, AdminPanelApiTests, DateOnly, Fact, Task, ApiCollection, IntegrationTestBase, HttpClient (+29 more)

### Community 18 - "Domain & Infra Namespaces"
Cohesion: 0.12
Nodes (10): CampCenter.Application.Models, CampCenter.Domain.Exceptions, CampCenter.Application.Services, CampCenter.UnitTests.Services, Exception, BusinessRuleViolationException, ConcurrencyConflictException, ConflictException (+2 more)

### Community 19 - "src / utils"
Cohesion: 0.16
Nodes (27): getScheduleCalendar(), getScheduleDay(), ScheduleCalendar, CalendarTile(), Props, groupHue(), LaneEvent, packLanes() (+19 more)

### Community 20 - "Integration Test Harness (1)"
Cohesion: 0.16
Nodes (8): CampCenter.IntegrationTests, CampCenter.Api.RateLimiting, CampCenter.Application.DTOs.Rooms, CampCenter.Api.Controllers.Public, CampCenter.Application.DTOs.Public, CampCenter.Application.DTOs.Closures, RateLimitPolicies, string

### Community 21 - "CampCenter.Application / Services (3)"
Cohesion: 0.05
Nodes (63): AdminAssignment, ApplyBookingMealTimeResult, AssignableRoom, BookingGroupPage, BookingMealTime, BookingSchedule, BookingScheduleDay, checkScheduleConflicts() (+55 more)

### Community 22 - "CampCenter.Domain / Repositories (1)"
Cohesion: 0.17
Nodes (13): BookingGroupCategory, BookingRepository, CancellationToken, DateOnly, DateTime, Dictionary, Exception, Guid (+5 more)

### Community 23 - "Camp Session Management"
Cohesion: 0.08
Nodes (23): MealTimesController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+15 more)

### Community 24 - "Integration Test Harness (2)"
Cohesion: 0.21
Nodes (11): IBookingRepository, CancellationToken, DateOnly, DateTime, Dictionary, Guid, IReadOnlyCollection, Items (+3 more)

### Community 25 - "Frontend Auth & API Client"
Cohesion: 0.18
Nodes (21): login(), logout(), api, refreshAccessToken(), RetriableConfig, decodeJWT(), getUserIdFromToken(), getUserLoginFromToken() (+13 more)

### Community 26 - "CampCenter.Domain / Repositories (2)"
Cohesion: 0.23
Nodes (10): BookingMealTime, DateTime, Guid, TimeOnly, BookingMealTimeRepository, CancellationToken, Guid, IReadOnlyCollection (+2 more)

### Community 27 - "Booking Persistence & Entities (1)"
Cohesion: 0.25
Nodes (6): DbContext, DbSet, IDesignTimeDbContextFactory, AppDbContext, ModelBuilder, DesignTimeDbContextFactory

### Community 28 - "Application Namespaces & DTOs"
Cohesion: 0.17
Nodes (6): CampCenter.Api.Controllers.Admin, CampCenter.Application.DTOs.AdminPanel, CampCenter.Application.Interfaces, CampCenter.Application.DTOs.Schedule, CampCenter.Api.Extensions, RoomDeleteResultDto

### Community 29 - "Admin Booking & Notifications (2)"
Cohesion: 0.40
Nodes (5): IMealTimeDefaultRepository, CancellationToken, Guid, List, Task

### Community 30 - "Public Booking Service (1)"
Cohesion: 0.05
Nodes (50): PublicAvailabilityController, CancellationToken, DateOnly, HttpGet, IActionResult, ProducesResponseType, Task, PublicBookingsController (+42 more)

### Community 31 - "Public Booking Service (2)"
Cohesion: 0.16
Nodes (6): CampCenter.Api.Background, CampCenter.Infrastructure.Repositories, CampCenter.Domain.Entities, CampCenter.Domain.Repositories, CampCenter.Infrastructure.Persistence, BookingStatuses

### Community 32 - "Payment Gateway Integration Tests (2)"
Cohesion: 0.18
Nodes (10): IAsyncLifetime, IServiceProvider, IWebHostBuilder, PostgreSqlContainer, DataSeeder, CancellationToken, Task, CampCenterApiFactory (+2 more)

### Community 33 - "Docker & Project Docs"
Cohesion: 0.05
Nodes (43): Aktualizacja po każdej zmianie (obowiązkowe), Architektura, Build environment, Bun responsibilities, CampCenter, Cel projektu, Closure Model Replaces Camp Sessions, Decision rule (+35 more)

### Community 34 - "Rate Limiting & Startup"
Cohesion: 0.09
Nodes (12): CampCenter.Application.Validators, CampCenter.Application.DTOs.Auth, CampCenter.UnitTests.Validators, InlineData, LoginResponseDto, CreateBookingRequestValidator, LoginRequestValidator, MealTimeRules (+4 more)

### Community 35 - "components / admin"
Cohesion: 0.26
Nodes (10): createTask(), deleteTask(), getOccupancy(), getTasks(), Occupancy, RoomOccupancy, RoomTask, setTaskDone() (+2 more)

### Community 36 - "Validator Unit Tests"
Cohesion: 0.33
Nodes (7): MealTimeDefaultDto, MealTimeService, CancellationToken, Guid, List, Task, TimeOnly

### Community 37 - "CampCenter.UnitTests / Services (2)"
Cohesion: 0.05
Nodes (54): HousekeepingController, CancellationToken, DateOnly, Guid, HttpGet, HttpPut, IActionResult, ProducesResponseType (+46 more)

### Community 38 - "TypeScript App Config"
Cohesion: 0.09
Nodes (22): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection, moduleResolution (+14 more)

### Community 39 - "Admin User & Token Config"
Cohesion: 0.15
Nodes (13): AdminUser, DateTime, Guid, IAdminUserRepository, CancellationToken, Guid, Task, AdminUserConfiguration (+5 more)

### Community 40 - "TypeScript Node Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, moduleResolution, noEmit (+12 more)

### Community 41 - "Auth Service & Tokens"
Cohesion: 0.17
Nodes (13): ITokenService, AuthResult, RefreshTokenInfo, AuthService, CancellationToken, DateTime, Guid, Task (+5 more)

### Community 42 - "Root Task-Runner Scripts"
Cohesion: 0.10
Nodes (19): description, name, private, scripts, backend, build, dev, dev:down (+11 more)

### Community 44 - "CampCenter.UnitTests / Services (4)"
Cohesion: 0.42
Nodes (5): ScheduleConflictTests, DateOnly, Fact, Guid, Task

### Community 45 - "Booking Persistence & Entities (2)"
Cohesion: 0.38
Nodes (5): MealGenerationPlanner, End, IReadOnlyCollection, Start, TimeOnly

### Community 46 - "JWT Token Service"
Cohesion: 0.17
Nodes (7): AccessToken, JwtSettings, string, JwtTokenService, int, RefreshTokenSettings, string

### Community 47 - "tests / CampCenter.IntegrationTests (2)"
Cohesion: 0.33
Nodes (3): BookingRoomAssignment, DateOnly, Guid

### Community 48 - "Frontend App Shell & i18n"
Cohesion: 0.13
Nodes (18): AdminBooking, BookingGroupCategory, BookingStatus, bookingStatuses, cancelAdminBooking(), createAdminBooking(), DashboardBooking, getAdminBookings() (+10 more)

### Community 49 - "Auth Controller (1)"
Cohesion: 0.17
Nodes (14): CookieOptions, AuthController, CancellationToken, DateTime, EnableRateLimiting, HttpPost, IActionResult, IValidator (+6 more)

### Community 50 - "Admin Frontend Pages"
Cohesion: 0.47
Nodes (5): Payment, PaymentKind, PaymentStatus, DateTime, Guid

### Community 51 - "Refresh Token Repository"
Cohesion: 0.18
Nodes (10): RefreshToken, DateTime, Guid, RefreshTokenConfiguration, EntityTypeBuilder, RefreshTokenRepository, CancellationToken, DateTime (+2 more)

### Community 52 - "Admin Booking & Notifications (3)"
Cohesion: 0.20
Nodes (10): MealKind, MealTimeDefault, DateTime, Guid, TimeOnly, MealTimeDefaultRepository, CancellationToken, Guid (+2 more)

### Community 53 - "Room Task Management (2)"
Cohesion: 0.22
Nodes (10): getHousekeepingDay(), getHousekeepingRange(), HousekeepingDay, HousekeepingRange, HousekeepingRoom, RoomCleaningStatus, roomCleaningStatuses, setRoomCleaning() (+2 more)

### Community 54 - "API Launch Settings"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 55 - "Global Exception Handler"
Cohesion: 0.14
Nodes (12): CampCenter.Api.Errors, Detail, HttpContext, IExceptionHandler, IProblemDetailsService, GlobalExceptionHandler, CancellationToken, Exception (+4 more)

### Community 56 - "ESLint Dev Dependencies"
Cohesion: 0.13
Nodes (15): @eslint/js, eslint-plugin-react-refresh, devDependencies, @eslint/js, eslint-plugin-react-refresh, globals, @types/react, typescript-eslint (+7 more)

### Community 57 - "Admin Booking & Notifications (4)"
Cohesion: 0.14
Nodes (13): EmailMessage, IEmailSender, CancellationToken, Task, BookingSettings, string, EmailTemplates, DateOnly (+5 more)

### Community 58 - "Persistence / Configurations"
Cohesion: 0.06
Nodes (20): CampCenter.Infrastructure.Persistence.Configurations, IEntityTypeConfiguration, BookingConfiguration, EntityTypeBuilder, BookingMealTimeConfiguration, EntityTypeBuilder, BookingRoomAssignmentConfiguration, EntityTypeBuilder (+12 more)

### Community 60 - "Frontend Runtime Deps"
Cohesion: 0.15
Nodes (13): axios, dependencies, axios, i18next, react, react-dom, react-i18next, react-router-dom (+5 more)

### Community 64 - "src / api (2)"
Cohesion: 0.27
Nodes (12): ScheduleDay, buildChips(), Chip, ClashReason, DayTimetable(), findClashes(), groupsOf(), minutesOf() (+4 more)

### Community 65 - "Admin Tasks & Occupancy Pages"
Cohesion: 0.20
Nodes (11): bookingGroupCategories, Dashboard, getDashboard(), App(), useAuth(), ProtectedRoute(), AdminLayout(), IconSunSea() (+3 more)

### Community 66 - "Payment"
Cohesion: 0.10
Nodes (13): CampCenter.Infrastructure.Email, CampCenter.Infrastructure.Payments, IConfiguration, RegisterData, DependencyInjection, IServiceCollection, EmailSettings, string (+5 more)

### Community 70 - "OpenAPI Security Scheme"
Cohesion: 0.20
Nodes (8): CampCenter.Api.OpenApi, IOpenApiDocumentTransformer, OpenApiDocument, OpenApiDocumentTransformerContext, BearerSecuritySchemeTransformer, CancellationToken, string, Task

### Community 71 - "EF Core Migrations (1)"
Cohesion: 0.20
Nodes (5): CampCenter.Infrastructure.Persistence.Migrations, InitialAuth, ModelBuilder, RoomCleanings, ModelBuilder

### Community 73 - "Booking Maintenance Background Service"
Cohesion: 0.31
Nodes (7): BackgroundService, IServiceScopeFactory, BookingMaintenanceService, CancellationToken, ILogger, Task, TimeSpan

### Community 74 - "PasswordRules"
Cohesion: 0.33
Nodes (4): IRuleBuilder, IRuleBuilderOptions, PasswordRules, int

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

### Community 106 - "frontend (1)"
Cohesion: 0.67
Nodes (3): Lakeside Typography (Bricolage Grotesque + Inter), SPA HTML Shell, Pre-Paint Theme Restore

## Knowledge Gaps
- **233 isolated node(s):** `printWidth`, `name`, `version`, `private`, `type` (+228 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **32 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Booking` connect `CampCenter.UnitTests / Validators` to `DTOs / Schedule (1)`, `Persistence / Configurations`, `Room Task Management (1)`, `CampCenter.UnitTests / Services (2)`, `CampCenter.Application / Services (2)`, `Admin Booking & Notifications (1)`, `tests / CampCenter.IntegrationTests (2)`, `Admin Frontend Pages`, `CampCenter.Domain / Repositories (1)`, `Integration Test Harness (2)`, `Admin Booking & Notifications (4)`, `CampCenter.Domain / Repositories (2)`, `Booking Persistence & Entities (1)`, `Public Booking Service (1)`?**
  _High betweenness centrality (0.086) - this node is a cross-community bridge._
- **Why does `CampCenter.Application.Interfaces` connect `Application Namespaces & DTOs` to `Room Management`, `Rate Limiting & Startup`, `Payment`, `CampCenter.Application / Services (1)`, `Payment Gateway Integration Tests (1)`, `Room Closure Management`, `Password Hashing (bcrypt)`, `Domain & Infra Namespaces`, `Integration Test Harness (1)`, `Admin Booking & Notifications (4)`, `Public Booking Service (1)`, `Public Booking Service (2)`?**
  _High betweenness centrality (0.075) - this node is a cross-community bridge._
- **Why does `CampCenter.Domain.Entities` connect `Public Booking Service (2)` to `DTOs / Schedule (1)`, `Room Management`, `CampCenter.UnitTests / Validators`, `Room Task Management (1)`, `CampCenter.Application / Services (1)`, `CampCenter.Application / Services (2)`, `Room Closure Management`, `Domain & Infra Namespaces`, `Integration Test Harness (1)`, `CampCenter.Domain / Repositories (1)`, `CampCenter.Domain / Repositories (2)`, `Application Namespaces & DTOs`, `Rate Limiting & Startup`, `CampCenter.UnitTests / Services (2)`, `Admin User & Token Config`, `tests / CampCenter.IntegrationTests (2)`, `Admin Frontend Pages`, `Refresh Token Repository`, `Admin Booking & Notifications (3)`, `Persistence / Configurations`, `CampCenter.Application / Services (4)`?**
  _High betweenness centrality (0.072) - this node is a cross-community bridge._
- **What connects `printWidth`, `name`, `version` to the rest of the system?**
  _233 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DTOs / Schedule (1)` be split into smaller, more focused modules?**
  _Cohesion score 0.06435498089920658 - nodes in this community are weakly interconnected._
- **Should `Room Management` be split into smaller, more focused modules?**
  _Cohesion score 0.06621004566210045 - nodes in this community are weakly interconnected._
- **Should `Admin Bookings Controller & DTOs` be split into smaller, more focused modules?**
  _Cohesion score 0.07451923076923077 - nodes in this community are weakly interconnected._