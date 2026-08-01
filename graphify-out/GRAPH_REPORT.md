# Graph Report - hotel  (2026-07-31)

## Corpus Check
- 262 files · ~94,049 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2351 nodes · 5694 edges · 120 communities (79 shown, 41 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 344 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `30bee631`
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
- ScheduleEntry
- JWT Token Service
- tests / CampCenter.IntegrationTests (2)
- Frontend App Shell & i18n
- Auth Controller (1)
- .CreateWorkerAsync
- Refresh Token Repository
- useAuth
- API Launch Settings
- Global Exception Handler
- ESLint Dev Dependencies
- Admin Booking & Notifications (4)
- Persistence / Configurations
- Frontend Runtime Deps
- .GetBlockedRoomIdsAsync
- RoomCleaningRepository
- .CreateClient
- src / api (2)
- Admin Tasks & Occupancy Pages
- Payment
- 20260728105506_PerGroupMealTimes.Designer.cs
- WriteRequiresAdministratorHandler
- IEntityTypeConfiguration
- OpenAPI Security Scheme
- EF Core Migrations (1)
- Booking Maintenance Background Service
- PasswordRules
- SmtpEmailSender
- CampCenter.Application.DTOs.Public
- BookingConfiguration
- BookingRoomAssignmentConfiguration
- Frontend Build Scripts
- Social Icon Sprite
- Password Hashing (bcrypt)
- Claims Principal Extensions
- Frontend Package Manifest
- EF Core Migrations (2)
- EF Core Migrations (3)
- AdminUserRole
- EF Core Migrations (4)
- Persistence / Migrations (1)
- Persistence / Migrations (2)
- Persistence / Migrations (3)
- Persistence / Migrations (4)
- Persistence / Migrations (5)
- Persistence / Migrations (6)
- Persistence / Migrations (7)
- Frontend API Error Handling
- Select Component
- 20260729224623_RoomCleanings.Designer.cs
- EF Core Migrations (5)
- 20260730211855_AdminUserRole.Designer.cs
- Persistence / Migrations (9)
- Persistence / Migrations (10)
- @types/react
- PaymentConfiguration
- Persistence / Migrations (13)
- frontend (1)
- RoomConfiguration
- Root TS Config
- ScheduleEntryConfiguration
- Prettier Dependency
- Node Type Definitions
- React DOM Type Definitions
- TypeScript Dependency
- Prettier Config
- App Brand Identity
- Payment
- frontend (3)
- src / assets (1)
- src / assets (2)
- eslint
- eslint-plugin-react-hooks

## God Nodes (most connected - your core abstractions)
1. `CampCenter.Domain.Entities` - 81 edges
2. `Booking` - 80 edges
3. `CampCenter.Application.Interfaces` - 54 edges
4. `CampCenter.Domain.Repositories` - 37 edges
5. `ScheduleService` - 36 edges
6. `useAuth()` - 33 edges
7. `IBookingRepository` - 32 edges
8. `AdminBookingService` - 31 edges
9. `MealTimeDefault` - 29 edges
10. `ScheduleEntry` - 28 edges

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

## Communities (120 total, 41 thin omitted)

### Community 0 - "DTOs / Schedule (1)"
Cohesion: 0.09
Nodes (36): ScheduleController, CancellationToken, DateOnly, Guid, HttpDelete, HttpGet, HttpPost, HttpPut (+28 more)

### Community 1 - "Room Management"
Cohesion: 0.06
Nodes (46): RoomsController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+38 more)

### Community 2 - "Admin Bookings Controller & DTOs"
Cohesion: 0.07
Nodes (43): ControllerBase, BookingsController, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IActionResult (+35 more)

### Community 3 - "src / api (1)"
Cohesion: 0.05
Nodes (53): AdminAssignment, AdminUser, ApplyBookingMealTimeResult, BookingGroupPage, BookingScheduleDay, Closure, ClosureInput, CreateAdminBookingInput (+45 more)

### Community 4 - "CampCenter.UnitTests / Validators"
Cohesion: 0.06
Nodes (23): AbstractValidator, CampCenter.Application.Validators, CampCenter.Application.DTOs.Auth, CampCenter.UnitTests.Validators, InlineData, LoginResponseDto, UpdateMealTimeDefaultRequestDto, UpdateDietaryNotesRequestDto (+15 more)

### Community 5 - "Room Task Management (1)"
Cohesion: 0.08
Nodes (37): TasksController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, IActionResult, ProducesResponseType (+29 more)

### Community 6 - "CampCenter.Application / Services (1)"
Cohesion: 0.15
Nodes (8): AccessToken, JwtSettings, string, JwtTokenService, int, string, RefreshTokenSettings, string

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
Nodes (31): Tile, TILES, IconArrowRight(), IconBed(), IconCalendar(), IconCheckSquare(), IconClipboard(), IconGrid() (+23 more)

### Community 11 - "CampCenter.Infrastructure / Repositories (1)"
Cohesion: 0.13
Nodes (21): DbContext, DbSet, IDesignTimeDbContextFactory, AppDbContext, ModelBuilder, DesignTimeDbContextFactory, ScheduleEntryRepository, BookingId (+13 more)

### Community 12 - "CampCenter.Application / Services (2)"
Cohesion: 0.06
Nodes (52): Skipped, BookingMealTimeDto, ScheduleEntryDto, ScheduleSettings, string, TimeOnly, ScheduleService, CancellationToken (+44 more)

### Community 13 - "Public Booking Frontend (1)"
Cohesion: 0.08
Nodes (33): formatZl(), Availability, AvailabilityCalendar, AvailabilityDay, BookingDetails, BookingPayment, cancelBooking(), createBooking() (+25 more)

### Community 14 - "CampCenter.UnitTests / Services (1)"
Cohesion: 0.06
Nodes (38): Slot, MealTimeDefaultDto, MealGenerationPlanner, MealSlot, Date, DateOnly, End, IEnumerable (+30 more)

### Community 15 - "Admin Booking & Notifications (1)"
Cohesion: 0.23
Nodes (11): AdminBookingDto, AdminBookingService, CancellationToken, DateOnly, DateTime, Guid, ILogger, int (+3 more)

### Community 16 - "Room Closure Management"
Cohesion: 0.09
Nodes (34): createMealTime(), deleteMealTime(), getMealTimes(), MealKind, mealKinds, MealTimeDefault, ScheduleDay, ScheduleEntry (+26 more)

### Community 17 - "tests / CampCenter.IntegrationTests (1)"
Cohesion: 0.06
Nodes (44): Admin, ICollectionFixture, AdminPanelApiTests, DateOnly, Fact, Task, ApiCollection, IntegrationTestBase (+36 more)

### Community 18 - "Domain & Infra Namespaces"
Cohesion: 0.11
Nodes (11): CampCenter.Application.Models, CampCenter.Domain.Exceptions, CampCenter.Application.Common, CampCenter.Application.Services, CampCenter.UnitTests.Services, Exception, BusinessRuleViolationException, ConcurrencyConflictException (+3 more)

### Community 19 - "src / utils"
Cohesion: 0.15
Nodes (30): getScheduleCalendar(), getScheduleDay(), getAvailabilityCalendar(), CalendarTile(), Props, groupHue(), LaneEvent, packLanes() (+22 more)

### Community 20 - "Integration Test Harness (1)"
Cohesion: 0.11
Nodes (18): ScheduleCalendarBookingDto, HousekeepingJob, HousekeepingPlanner, DateOnly, IEnumerable, List, Booking, BookingCancelReason (+10 more)

### Community 21 - "CampCenter.Application / Services (3)"
Cohesion: 0.18
Nodes (13): BookingSchedule, checkScheduleConflicts(), createScheduleEntry(), deleteScheduleEntry(), getBookingSchedule(), getScheduleLocations(), ScheduleConflict, ScheduleLocations (+5 more)

### Community 22 - "CampCenter.Domain / Repositories (1)"
Cohesion: 0.18
Nodes (12): BookingRepository, CancellationToken, DateOnly, DateTime, Dictionary, Exception, Guid, IReadOnlyCollection (+4 more)

### Community 23 - "Camp Session Management"
Cohesion: 0.08
Nodes (23): MealTimesController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+15 more)

### Community 24 - "Integration Test Harness (2)"
Cohesion: 0.20
Nodes (11): IBookingRepository, CancellationToken, DateOnly, DateTime, Dictionary, Guid, IReadOnlyCollection, Items (+3 more)

### Community 25 - "Frontend Auth & API Client"
Cohesion: 0.17
Nodes (22): login(), logout(), api, refreshAccessToken(), RetriableConfig, decodeJWT(), getUserIdFromToken(), getUserLoginFromToken() (+14 more)

### Community 26 - "CampCenter.Domain / Repositories (2)"
Cohesion: 0.22
Nodes (10): BookingMealTime, deleteBookingMeals(), getBookingMealTimes(), NeighbourSitting, resetBookingMealTime(), setBookingMealTime(), clashingNeighbours(), GroupMealTimes() (+2 more)

### Community 27 - "Booking Persistence & Entities (1)"
Cohesion: 0.14
Nodes (22): UsersController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+14 more)

### Community 28 - "Application Namespaces & DTOs"
Cohesion: 0.14
Nodes (7): CampCenter.Api.Controllers.Admin, CampCenter.Api.Controllers.Public, CampCenter.Application.DTOs.AdminPanel, CampCenter.Application.Interfaces, CampCenter.Application.DTOs.Schedule, CampCenter.Api.Extensions, RoomDeleteResultDto

### Community 29 - "Admin Booking & Notifications (2)"
Cohesion: 0.11
Nodes (24): PublicBookingsController, CancellationToken, EnableRateLimiting, HttpGet, HttpPost, IActionResult, IValidator, ProducesResponseType (+16 more)

### Community 30 - "Public Booking Service (1)"
Cohesion: 0.18
Nodes (12): AvailabilityDayDto, BookingDetailsDto, BookingPaymentDto, CreateBookingRequestDto, CreateBookingResponseDto, PublicClosureDto, BookingService, CancellationToken (+4 more)

### Community 31 - "Public Booking Service (2)"
Cohesion: 0.18
Nodes (5): CampCenter.Api.Background, CampCenter.Infrastructure.Repositories, CampCenter.Domain.Entities, CampCenter.Domain.Repositories, CampCenter.Infrastructure.Persistence

### Community 32 - "Payment Gateway Integration Tests (2)"
Cohesion: 0.17
Nodes (11): IAsyncLifetime, IServiceProvider, IWebHostBuilder, PostgreSqlContainer, Program, DataSeeder, CancellationToken, Task (+3 more)

### Community 33 - "Docker & Project Docs"
Cohesion: 0.05
Nodes (43): Aktualizacja po każdej zmianie (obowiązkowe), Architektura, Build environment, Bun responsibilities, CampCenter, Cel projektu, Closure Model Replaces Camp Sessions, Decision rule (+35 more)

### Community 34 - "Rate Limiting & Startup"
Cohesion: 0.50
Nodes (3): IConfiguration, DependencyInjection, IServiceCollection

### Community 35 - "components / admin"
Cohesion: 0.33
Nodes (3): BookingRoomAssignment, DateOnly, Guid

### Community 36 - "Validator Unit Tests"
Cohesion: 0.05
Nodes (52): ClosuresController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+44 more)

### Community 37 - "CampCenter.UnitTests / Services (2)"
Cohesion: 0.08
Nodes (39): HousekeepingController, CancellationToken, DateOnly, Guid, HttpGet, HttpPut, IActionResult, ProducesResponseType (+31 more)

### Community 38 - "TypeScript App Config"
Cohesion: 0.09
Nodes (22): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection, moduleResolution (+14 more)

### Community 39 - "Admin User & Token Config"
Cohesion: 0.14
Nodes (14): AdminUser, DateTime, Guid, AdminUserRole, IAdminUserRepository, CancellationToken, Guid, List (+6 more)

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

### Community 45 - "ScheduleEntry"
Cohesion: 0.33
Nodes (4): PasswordRules, int, IRuleBuilder, IRuleBuilderOptions

### Community 47 - "tests / CampCenter.IntegrationTests (2)"
Cohesion: 0.39
Nodes (6): AdminUserDto, UserService, CancellationToken, Guid, List, Task

### Community 48 - "Frontend App Shell & i18n"
Cohesion: 0.13
Nodes (18): AdminBooking, BookingGroupCategory, BookingStatus, bookingStatuses, cancelAdminBooking(), createAdminBooking(), DashboardBooking, getAdminBookings() (+10 more)

### Community 49 - "Auth Controller (1)"
Cohesion: 0.17
Nodes (14): CookieOptions, AuthController, CancellationToken, DateTime, EnableRateLimiting, HttpPost, IActionResult, IValidator (+6 more)

### Community 51 - "Refresh Token Repository"
Cohesion: 0.24
Nodes (8): RefreshToken, DateTime, Guid, RefreshTokenRepository, CancellationToken, DateTime, Guid, Task

### Community 53 - "useAuth"
Cohesion: 0.13
Nodes (19): bookingGroupCategories, createTask(), Dashboard, deleteTask(), getDashboard(), getOccupancy(), getTasks(), Occupancy (+11 more)

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
Cohesion: 0.14
Nodes (13): EmailMessage, IEmailSender, CancellationToken, Task, BookingSettings, string, EmailTemplates, DateOnly (+5 more)

### Community 60 - "Frontend Runtime Deps"
Cohesion: 0.15
Nodes (13): axios, dependencies, axios, i18next, react, react-dom, react-i18next, react-router-dom (+5 more)

### Community 61 - ".GetBlockedRoomIdsAsync"
Cohesion: 0.29
Nodes (9): AvailabilityCalendarDto, AvailabilityDto, IAvailabilityService, CancellationToken, DateOnly, Dictionary, Guid, HashSet (+1 more)

### Community 62 - "RoomCleaningRepository"
Cohesion: 0.32
Nodes (7): RoomCleaningRepository, CancellationToken, DateOnly, Dictionary, Guid, List, Task

### Community 63 - ".CreateClient"
Cohesion: 0.10
Nodes (11): CampCenter.Infrastructure.Auth, CampCenter.Api.RateLimiting, CampCenter.Application, CampCenter.Api.Controllers, CampCenter.Api.Errors, CampCenter.Infrastructure, CampCenter.Infrastructure.Persistence.Seed, RateLimitPolicies (+3 more)

### Community 64 - "src / api (2)"
Cohesion: 0.24
Nodes (8): RoomCleaning, RoomCleaningKind, RoomCleaningStatus, DateOnly, DateTime, Guid, RoomCleaningConfiguration, EntityTypeBuilder

### Community 65 - "Admin Tasks & Occupancy Pages"
Cohesion: 0.32
Nodes (7): AssignableRoom, getAdminBooking(), getAssignableRooms(), reassignBooking(), Draft, GroupRooms(), Props

### Community 66 - "Payment"
Cohesion: 0.29
Nodes (4): CampCenter.Infrastructure.Payments, RegisterData, RegisterData, RegisterResponse

### Community 68 - "WriteRequiresAdministratorHandler"
Cohesion: 0.22
Nodes (9): AuthorizationHandler, AuthorizationHandlerContext, CampCenter.Api.Auth, IAuthorizationRequirement, IHttpContextAccessor, WriteRequiresAdministratorHandler, WriteRequiresAdministratorRequirement, string (+1 more)

### Community 69 - "IEntityTypeConfiguration"
Cohesion: 0.20
Nodes (5): CampCenter.Infrastructure.Persistence.Configurations, ClosureConfiguration, EntityTypeBuilder, MealTimeDefaultConfiguration, EntityTypeBuilder

### Community 70 - "OpenAPI Security Scheme"
Cohesion: 0.20
Nodes (8): CampCenter.Api.OpenApi, IOpenApiDocumentTransformer, OpenApiDocument, OpenApiDocumentTransformerContext, BearerSecuritySchemeTransformer, CancellationToken, string, Task

### Community 73 - "Booking Maintenance Background Service"
Cohesion: 0.31
Nodes (7): BackgroundService, IServiceScopeFactory, BookingMaintenanceService, CancellationToken, ILogger, Task, TimeSpan

### Community 74 - "PasswordRules"
Cohesion: 0.25
Nodes (4): CampCenter.Application.DTOs.Users, RoleRules, IRuleBuilder, IRuleBuilderOptions

### Community 75 - "SmtpEmailSender"
Cohesion: 0.25
Nodes (6): CampCenter.Infrastructure.Email, EmailSettings, string, SmtpEmailSender, CancellationToken, Task

### Community 76 - "CampCenter.Application.DTOs.Public"
Cohesion: 0.33
Nodes (4): CampCenter.IntegrationTests, CampCenter.Application.DTOs.Rooms, CampCenter.Application.DTOs.Public, CampCenter.Application.DTOs.Closures

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
Cohesion: 0.40
Nodes (3): Migration, CoreDomain, MigrationBuilder

### Community 85 - "EF Core Migrations (3)"
Cohesion: 0.20
Nodes (6): CampCenter.Infrastructure.Persistence.Migrations, ModelSnapshot, MealSittingDuration, ModelBuilder, AppDbContextModelSnapshot, ModelBuilder

### Community 106 - "frontend (1)"
Cohesion: 0.67
Nodes (3): Lakeside Typography (Bricolage Grotesque + Inter), SPA HTML Shell, Pre-Paint Theme Restore

### Community 109 - "ScheduleEntryConfiguration"
Cohesion: 0.25
Nodes (5): IEntityTypeConfiguration, RefreshTokenConfiguration, EntityTypeBuilder, ScheduleEntryConfiguration, EntityTypeBuilder

### Community 117 - "Payment"
Cohesion: 0.47
Nodes (5): Payment, PaymentKind, PaymentStatus, DateTime, Guid

## Knowledge Gaps
- **240 isolated node(s):** `printWidth`, `name`, `version`, `private`, `type` (+235 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **41 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `CampCenter.Application.Interfaces` connect `Application Namespaces & DTOs` to `Room Management`, `Payment`, `CampCenter.UnitTests / Validators`, `Validator Unit Tests`, `Payment Gateway Integration Tests (1)`, `PasswordRules`, `SmtpEmailSender`, `CampCenter.Application.DTOs.Public`, `Password Hashing (bcrypt)`, `Domain & Infra Namespaces`, `Admin Booking & Notifications (4)`, `Public Booking Service (2)`, `Admin Booking & Notifications (2)`, `.CreateClient`?**
  _High betweenness centrality (0.093) - this node is a cross-community bridge._
- **Why does `CampCenter.Domain.Entities` connect `Public Booking Service (2)` to `Room Management`, `Admin Bookings Controller & DTOs`, `CampCenter.UnitTests / Validators`, `Room Task Management (1)`, `CampCenter.Application / Services (2)`, `CampCenter.UnitTests / Services (1)`, `Domain & Infra Namespaces`, `Integration Test Harness (1)`, `Application Namespaces & DTOs`, `components / admin`, `Validator Unit Tests`, `Admin User & Token Config`, `JWT Token Service`, `.CreateWorkerAsync`, `Refresh Token Repository`, `Persistence / Configurations`, `.CreateClient`, `src / api (2)`, `WriteRequiresAdministratorHandler`, `IEntityTypeConfiguration`, `PasswordRules`, `CampCenter.Application.DTOs.Public`, `BookingConfiguration`, `BookingRoomAssignmentConfiguration`, `PaymentConfiguration`, `RoomConfiguration`, `ScheduleEntryConfiguration`, `Payment`?**
  _High betweenness centrality (0.088) - this node is a cross-community bridge._
- **Why does `AppDbContext` connect `CampCenter.Infrastructure / Repositories (1)` to `src / api (2)`, `Room Management`, `components / admin`, `Validator Unit Tests`, `Room Task Management (1)`, `Admin User & Token Config`, `CampCenter.Application / Services (2)`, `CampCenter.UnitTests / Services (1)`, `Refresh Token Repository`, `Integration Test Harness (1)`, `Payment`, `CampCenter.Domain / Repositories (1)`, `RoomCleaningRepository`, `Public Booking Service (2)`?**
  _High betweenness centrality (0.069) - this node is a cross-community bridge._
- **What connects `printWidth`, `name`, `version` to the rest of the system?**
  _240 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DTOs / Schedule (1)` be split into smaller, more focused modules?**
  _Cohesion score 0.09090909090909091 - nodes in this community are weakly interconnected._
- **Should `Room Management` be split into smaller, more focused modules?**
  _Cohesion score 0.05636114911080711 - nodes in this community are weakly interconnected._
- **Should `Admin Bookings Controller & DTOs` be split into smaller, more focused modules?**
  _Cohesion score 0.07146087743102668 - nodes in this community are weakly interconnected._