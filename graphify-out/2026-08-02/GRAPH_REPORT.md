# Graph Report - hotel  (2026-08-02)

## Corpus Check
- 268 files · ~96,091 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2376 nodes · 5747 edges · 130 communities (84 shown, 46 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 344 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `8ee6900c`
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
- ControllerBase
- Booking Persistence & Entities (1)
- Application Namespaces & DTOs
- Admin Booking & Notifications (2)
- Public Booking Service (1)
- Public Booking Service (2)
- Payment Gateway Integration Tests (2)
- Docker & Project Docs
- ClosureService
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
- AvailabilityService
- tests / CampCenter.IntegrationTests (2)
- Frontend App Shell & i18n
- Auth Controller (1)
- IClosureRepository
- Refresh Token Repository
- Exception
- useAuth
- API Launch Settings
- Global Exception Handler
- ESLint Dev Dependencies
- Admin Booking & Notifications (4)
- .Calendar
- RoomNumberComparer
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
- 20260802130913_MakeRoomTaskRoomOptional.Designer.cs
- Booking Maintenance Background Service
- .RevokeAllActiveForUserAsync
- SmtpEmailSender
- CampCenter.Application.DTOs.Public
- ClosureValidatorsTests
- IEntityTypeConfiguration
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
- UserValidators.cs
- Frontend API Error Handling
- Select Component
- 20260729224623_RoomCleanings.Designer.cs
- EF Core Migrations (5)
- .AddInfrastructure
- Persistence / Migrations (9)
- Persistence / Migrations (10)
- @types/react
- BookingConfiguration
- Persistence / Migrations (13)
- frontend (1)
- BookingRoomAssignmentConfiguration
- Root TS Config
- ClosureConfiguration
- PaymentConfiguration
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
- RefreshTokenConfiguration
- eslint
- RoomCleaningConfiguration
- RoomConfiguration
- RoomTaskConfiguration
- ScheduleEntryConfiguration
- eslint

## God Nodes (most connected - your core abstractions)
1. `CampCenter.Domain.Entities` - 82 edges
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

## Communities (130 total, 46 thin omitted)

### Community 0 - "DTOs / Schedule (1)"
Cohesion: 0.07
Nodes (43): AbstractValidator, ScheduleController, CancellationToken, DateOnly, Guid, HttpDelete, HttpGet, HttpPost (+35 more)

### Community 1 - "Room Management"
Cohesion: 0.07
Nodes (38): RoomsController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+30 more)

### Community 2 - "Admin Bookings Controller & DTOs"
Cohesion: 0.07
Nodes (43): BookingsController, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IActionResult, IValidator (+35 more)

### Community 3 - "src / api (1)"
Cohesion: 0.05
Nodes (62): AdminAssignment, AdminUser, ApplyBookingMealTimeResult, BookingGroupPage, BookingMealTime, BookingScheduleDay, Closure, ClosureInput (+54 more)

### Community 4 - "CampCenter.UnitTests / Validators"
Cohesion: 0.07
Nodes (16): CampCenter.Application.Validators, CampCenter.Application.DTOs.Auth, CampCenter.UnitTests.Validators, InlineData, LoginResponseDto, CreateBookingRequestValidator, LoginRequestValidator, MealTimeRules (+8 more)

### Community 5 - "Room Task Management (1)"
Cohesion: 0.08
Nodes (36): TasksController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, IActionResult, ProducesResponseType (+28 more)

### Community 6 - "CampCenter.Application / Services (1)"
Cohesion: 0.13
Nodes (9): AccessToken, RefreshTokenInfo, JwtSettings, string, JwtTokenService, int, string, RefreshTokenSettings (+1 more)

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
Cohesion: 0.07
Nodes (20): Tile, TILES, IconArrowRight(), IconBed(), IconCalendar(), IconCheckSquare(), IconClipboard(), IconGrid() (+12 more)

### Community 11 - "CampCenter.Infrastructure / Repositories (1)"
Cohesion: 0.12
Nodes (21): DbContext, DbSet, IDesignTimeDbContextFactory, AppDbContext, ModelBuilder, DesignTimeDbContextFactory, ScheduleEntryRepository, BookingId (+13 more)

### Community 12 - "CampCenter.Application / Services (2)"
Cohesion: 0.06
Nodes (52): Skipped, BookingMealTimeDto, ScheduleEntryDto, ScheduleSettings, string, TimeOnly, ScheduleService, CancellationToken (+44 more)

### Community 13 - "Public Booking Frontend (1)"
Cohesion: 0.07
Nodes (34): formatZl(), Availability, AvailabilityCalendar, AvailabilityDay, BookingDetails, BookingPayment, cancelBooking(), createBooking() (+26 more)

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
Cohesion: 0.39
Nodes (4): LoginRequestDto, IAuthService, CancellationToken, Task

### Community 19 - "src / utils"
Cohesion: 0.15
Nodes (29): getAvailabilityCalendar(), CalendarTile(), Props, DateRangeField(), Props, DayCalendar(), Props, groupHue() (+21 more)

### Community 20 - "Integration Test Harness (1)"
Cohesion: 0.11
Nodes (18): ScheduleCalendarBookingDto, HousekeepingJob, HousekeepingPlanner, DateOnly, IEnumerable, List, Booking, BookingCancelReason (+10 more)

### Community 22 - "CampCenter.Domain / Repositories (1)"
Cohesion: 0.18
Nodes (12): BookingRepository, CancellationToken, DateOnly, DateTime, Dictionary, Exception, Guid, IReadOnlyCollection (+4 more)

### Community 23 - "Camp Session Management"
Cohesion: 0.07
Nodes (27): MealTimesController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+19 more)

### Community 24 - "Integration Test Harness (2)"
Cohesion: 0.20
Nodes (11): IBookingRepository, CancellationToken, DateOnly, DateTime, Dictionary, Guid, IReadOnlyCollection, Items (+3 more)

### Community 25 - "Frontend Auth & API Client"
Cohesion: 0.17
Nodes (22): login(), logout(), api, refreshAccessToken(), RetriableConfig, decodeJWT(), getUserIdFromToken(), getUserLoginFromToken() (+14 more)

### Community 26 - "ControllerBase"
Cohesion: 0.23
Nodes (12): ControllerBase, ClosuresController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut (+4 more)

### Community 27 - "Booking Persistence & Entities (1)"
Cohesion: 0.07
Nodes (42): UsersController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+34 more)

### Community 28 - "Application Namespaces & DTOs"
Cohesion: 0.15
Nodes (7): CampCenter.Application.DTOs.Users, CampCenter.Api.Controllers.Admin, CampCenter.Application.DTOs.AdminPanel, CampCenter.Application.Interfaces, CampCenter.Application.DTOs.Schedule, CampCenter.Api.Extensions, RoomDeleteResultDto

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

### Community 34 - "ClosureService"
Cohesion: 0.38
Nodes (7): ClosureDto, ClosureService, CancellationToken, DateOnly, Guid, List, Task

### Community 35 - "components / admin"
Cohesion: 0.33
Nodes (3): BookingRoomAssignment, DateOnly, Guid

### Community 36 - "Validator Unit Tests"
Cohesion: 0.23
Nodes (10): Closure, DateOnly, DateTime, Guid, ClosureRepository, CancellationToken, DateOnly, Guid (+2 more)

### Community 37 - "CampCenter.UnitTests / Services (2)"
Cohesion: 0.06
Nodes (52): HousekeepingController, CancellationToken, DateOnly, Guid, HttpGet, HttpPut, IActionResult, ProducesResponseType (+44 more)

### Community 38 - "TypeScript App Config"
Cohesion: 0.09
Nodes (22): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection, moduleResolution (+14 more)

### Community 39 - "Admin User & Token Config"
Cohesion: 0.42
Nodes (5): IClosureService, CancellationToken, Guid, List, Task

### Community 40 - "TypeScript Node Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, moduleResolution, noEmit (+12 more)

### Community 41 - "Auth Service & Tokens"
Cohesion: 0.21
Nodes (8): IPasswordHasher, ITokenService, AuthResult, AuthService, CancellationToken, DateTime, Guid, Task

### Community 42 - "Root Task-Runner Scripts"
Cohesion: 0.10
Nodes (19): description, name, private, scripts, backend, build, dev, dev:down (+11 more)

### Community 44 - "CampCenter.UnitTests / Services (4)"
Cohesion: 0.42
Nodes (5): ScheduleConflictTests, DateOnly, Fact, Guid, Task

### Community 45 - "ScheduleEntry"
Cohesion: 0.21
Nodes (12): AdminBooking, BookingGroupCategory, BookingStatus, bookingStatuses, createAdminBooking(), DashboardBooking, getBookingGroupPage(), AddGroupForm() (+4 more)

### Community 46 - "AvailabilityService"
Cohesion: 0.34
Nodes (8): AvailabilityService, CancellationToken, DateOnly, Dictionary, Guid, HashSet, int, Task

### Community 47 - "tests / CampCenter.IntegrationTests (2)"
Cohesion: 0.22
Nodes (10): getHousekeepingDay(), getHousekeepingRange(), HousekeepingDay, HousekeepingRange, HousekeepingRoom, RoomCleaningStatus, roomCleaningStatuses, setRoomCleaning() (+2 more)

### Community 48 - "Frontend App Shell & i18n"
Cohesion: 0.11
Nodes (27): bookingGroupCategories, BookingSchedule, checkScheduleConflicts(), createScheduleEntry(), Dashboard, deleteScheduleEntry(), getBookingSchedule(), getDashboard() (+19 more)

### Community 49 - "Auth Controller (1)"
Cohesion: 0.25
Nodes (10): CookieOptions, AuthController, CancellationToken, DateTime, EnableRateLimiting, HttpPost, IActionResult, IValidator (+2 more)

### Community 50 - "IClosureRepository"
Cohesion: 0.31
Nodes (6): IClosureRepository, CancellationToken, DateOnly, Guid, List, Task

### Community 51 - "Refresh Token Repository"
Cohesion: 0.24
Nodes (8): RefreshToken, DateTime, Guid, RefreshTokenRepository, CancellationToken, DateTime, Guid, Task

### Community 52 - "Exception"
Cohesion: 0.11
Nodes (11): CampCenter.Application.Models, CampCenter.Domain.Exceptions, CampCenter.Application.Common, CampCenter.Application.Services, CampCenter.UnitTests.Services, Exception, BusinessRuleViolationException, ConcurrencyConflictException (+3 more)

### Community 53 - "useAuth"
Cohesion: 0.14
Nodes (18): cancelAdminBooking(), getAdminBookings(), App(), useAuth(), ProtectedRoute(), AdminLayout(), IconSun(), IconSunSea() (+10 more)

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

### Community 58 - ".Calendar"
Cohesion: 0.44
Nodes (7): PublicAvailabilityController, CancellationToken, DateOnly, HttpGet, IActionResult, ProducesResponseType, Task

### Community 59 - "RoomNumberComparer"
Cohesion: 0.33
Nodes (4): GeneratedRegex, IComparer, Regex, RoomNumberComparer

### Community 60 - "Frontend Runtime Deps"
Cohesion: 0.15
Nodes (13): axios, dependencies, axios, i18next, react, react-dom, react-i18next, react-router-dom (+5 more)

### Community 61 - ".GetBlockedRoomIdsAsync"
Cohesion: 0.29
Nodes (9): AvailabilityCalendarDto, AvailabilityDto, IAvailabilityService, CancellationToken, DateOnly, Dictionary, Guid, HashSet (+1 more)

### Community 63 - ".CreateClient"
Cohesion: 0.08
Nodes (12): CampCenter.Infrastructure.Auth, CampCenter.Api.RateLimiting, CampCenter.Application, CampCenter.Api.Controllers, CampCenter.Api.Errors, CampCenter.Api.Controllers.Public, CampCenter.Infrastructure, CampCenter.Infrastructure.Persistence.Seed (+4 more)

### Community 65 - "Admin Tasks & Occupancy Pages"
Cohesion: 0.32
Nodes (7): AssignableRoom, getAdminBooking(), getAssignableRooms(), reassignBooking(), Draft, GroupRooms(), Props

### Community 66 - "Payment"
Cohesion: 0.29
Nodes (4): CampCenter.Infrastructure.Payments, RegisterData, RegisterData, RegisterResponse

### Community 68 - "WriteRequiresAdministratorHandler"
Cohesion: 0.22
Nodes (9): AuthorizationHandler, AuthorizationHandlerContext, CampCenter.Api.Auth, IAuthorizationRequirement, IHttpContextAccessor, WriteRequiresAdministratorHandler, WriteRequiresAdministratorRequirement, string (+1 more)

### Community 70 - "OpenAPI Security Scheme"
Cohesion: 0.20
Nodes (8): CampCenter.Api.OpenApi, IOpenApiDocumentTransformer, OpenApiDocument, OpenApiDocumentTransformerContext, BearerSecuritySchemeTransformer, CancellationToken, string, Task

### Community 73 - "Booking Maintenance Background Service"
Cohesion: 0.31
Nodes (7): BackgroundService, IServiceScopeFactory, BookingMaintenanceService, CancellationToken, ILogger, Task, TimeSpan

### Community 74 - ".RevokeAllActiveForUserAsync"
Cohesion: 0.39
Nodes (5): IRefreshTokenRepository, CancellationToken, DateTime, Guid, Task

### Community 75 - "SmtpEmailSender"
Cohesion: 0.25
Nodes (6): CampCenter.Infrastructure.Email, EmailSettings, string, SmtpEmailSender, CancellationToken, Task

### Community 76 - "CampCenter.Application.DTOs.Public"
Cohesion: 0.23
Nodes (4): CampCenter.IntegrationTests, CampCenter.Application.DTOs.Rooms, CampCenter.Application.DTOs.Public, CampCenter.Application.DTOs.Closures

### Community 77 - "ClosureValidatorsTests"
Cohesion: 0.27
Nodes (6): CreateClosureRequestDto, UpdateClosureRequestDto, CreateClosureRequestValidator, UpdateClosureRequestValidator, ClosureValidatorsTests, Fact

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

### Community 85 - "EF Core Migrations (3)"
Cohesion: 0.20
Nodes (6): CampCenter.Infrastructure.Persistence.Migrations, ModelSnapshot, AdminUserRole, ModelBuilder, AppDbContextModelSnapshot, ModelBuilder

### Community 86 - "AdminUserRole"
Cohesion: 0.40
Nodes (3): Migration, AdminUserRole, MigrationBuilder

### Community 95 - "UserValidators.cs"
Cohesion: 0.40
Nodes (3): RoleRules, IRuleBuilder, IRuleBuilderOptions

### Community 100 - ".AddInfrastructure"
Cohesion: 0.50
Nodes (3): IConfiguration, DependencyInjection, IServiceCollection

### Community 103 - "@types/react"
Cohesion: 0.40
Nodes (3): CampCenter.Infrastructure.Persistence.Configurations, AdminUserConfiguration, EntityTypeBuilder

### Community 106 - "frontend (1)"
Cohesion: 0.67
Nodes (3): Lakeside Typography (Bricolage Grotesque + Inter), SPA HTML Shell, Pre-Paint Theme Restore

### Community 117 - "Payment"
Cohesion: 0.47
Nodes (5): Payment, PaymentKind, PaymentStatus, DateTime, Guid

### Community 128 - "ScheduleEntryConfiguration"
Cohesion: 0.40
Nodes (3): IEntityTypeConfiguration, ScheduleEntryConfiguration, EntityTypeBuilder

## Knowledge Gaps
- **243 isolated node(s):** `printWidth`, `name`, `version`, `private`, `type` (+238 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **46 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Booking` connect `Integration Test Harness (1)` to `components / admin`, `Room Task Management (1)`, `CampCenter.UnitTests / Services (2)`, `BookingConfiguration`, `CampCenter.Infrastructure / Repositories (1)`, `CampCenter.Application / Services (2)`, `Admin Booking & Notifications (1)`, `Payment`, `CampCenter.Domain / Repositories (1)`, `Integration Test Harness (2)`, `Admin Booking & Notifications (4)`, `Public Booking Service (1)`?**
  _High betweenness centrality (0.088) - this node is a cross-community bridge._
- **Why does `CampCenter.Domain.Entities` connect `Public Booking Service (2)` to `DTOs / Schedule (1)`, `Room Management`, `Admin Bookings Controller & DTOs`, `ScheduleEntryConfiguration`, `CampCenter.UnitTests / Validators`, `Room Task Management (1)`, `CampCenter.Application / Services (2)`, `CampCenter.UnitTests / Services (1)`, `Integration Test Harness (1)`, `Booking Persistence & Entities (1)`, `Application Namespaces & DTOs`, `components / admin`, `Validator Unit Tests`, `CampCenter.UnitTests / Services (2)`, `Refresh Token Repository`, `Exception`, `RoomNumberComparer`, `.CreateClient`, `WriteRequiresAdministratorHandler`, `IEntityTypeConfiguration`, `CampCenter.Application.DTOs.Public`, `IEntityTypeConfiguration`, `UserValidators.cs`, `@types/react`, `BookingConfiguration`, `BookingRoomAssignmentConfiguration`, `ClosureConfiguration`, `PaymentConfiguration`, `Payment`, `RefreshTokenConfiguration`, `RoomCleaningConfiguration`, `RoomConfiguration`, `RoomTaskConfiguration`?**
  _High betweenness centrality (0.083) - this node is a cross-community bridge._
- **Why does `CampCenter.Application.Interfaces` connect `Application Namespaces & DTOs` to `Payment`, `CampCenter.UnitTests / Validators`, `Payment Gateway Integration Tests (1)`, `Auth Service & Tokens`, `SmtpEmailSender`, `CampCenter.Application.DTOs.Public`, `Exception`, `Admin Booking & Notifications (4)`, `Public Booking Service (2)`, `Admin Booking & Notifications (2)`, `.CreateClient`?**
  _High betweenness centrality (0.070) - this node is a cross-community bridge._
- **What connects `printWidth`, `name`, `version` to the rest of the system?**
  _243 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DTOs / Schedule (1)` be split into smaller, more focused modules?**
  _Cohesion score 0.07368421052631578 - nodes in this community are weakly interconnected._
- **Should `Room Management` be split into smaller, more focused modules?**
  _Cohesion score 0.06921529175050302 - nodes in this community are weakly interconnected._
- **Should `Admin Bookings Controller & DTOs` be split into smaller, more focused modules?**
  _Cohesion score 0.07055630936227951 - nodes in this community are weakly interconnected._