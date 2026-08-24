# Graph Report - hotel  (2026-08-17)

## Corpus Check
- 299 files · ~117,048 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2594 nodes · 6350 edges · 130 communities (89 shown, 41 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 375 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `fe9acf9d`
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
- AvailabilityService
- Frontend App Shell & i18n
- Auth Controller (1)
- .Update
- Refresh Token Repository
- Exception
- useAuth
- API Launch Settings
- Global Exception Handler
- ESLint Dev Dependencies
- Admin Booking & Notifications (4)
- ClosureValidatorsTests
- RoomNumberComparer
- Frontend Runtime Deps
- .GetBlockedRoomIdsAsync
- RoomCleaningRepository
- 20260730211855_AdminUserRole.Designer.cs
- src / api (2)
- UsersPage.tsx
- 20260728105506_PerGroupMealTimes.Designer.cs
- WriteRequiresAdministratorHandler
- OpenAPI Security Scheme
- EF Core Migrations (1)
- 20260802130913_MakeRoomTaskRoomOptional.Designer.cs
- Booking Maintenance Background Service
- PasswordRules
- SmtpEmailSender
- CampCenter.Application.DTOs.Public
- BookingSettings
- eslint
- Frontend Build Scripts
- Social Icon Sprite
- eslint-plugin-react-hooks
- Claims Principal Extensions
- Frontend Package Manifest
- EF Core Migrations (2)
- EF Core Migrations (3)
- AdminUserRole
- Persistence / Migrations (1)
- Persistence / Migrations (2)
- Persistence / Migrations (3)
- Persistence / Migrations (4)
- Persistence / Migrations (5)
- Persistence / Migrations (6)
- Persistence / Migrations (7)
- .CreateWorkerAsync
- Frontend API Error Handling
- Select Component
- 20260729224623_RoomCleanings.Designer.cs
- MealTimeDefault
- Persistence / Migrations (9)
- Persistence / Migrations (10)
- RoomCleaningRepository
- Persistence / Migrations (13)
- frontend (1)
- IClosureRepository
- Root TS Config
- IntegrationTestBase
- Prettier Dependency
- .WithRoomsAsync
- React DOM Type Definitions
- TypeScript Dependency
- Prettier Config
- App Brand Identity
- BookingConfiguration
- frontend (3)
- src / assets (1)
- src / assets (2)
- AdminPanelApiTests
- AuthApiTests
- GroupRooms.tsx
- BookingGroupSection.tsx
- .NextFreeSitting
- RoomsAndClosuresApiTests
- SupervisorCountsAndRates
- @types/node
- 20260719143540_CoreDomain.Designer.cs
- 20260721111400_ReplaceSessionsWithClosures.Designer.cs
- @types/react
- MakeRoomTaskRoomOptional

## God Nodes (most connected - your core abstractions)
1. `CampCenter.Domain.Entities` - 90 edges
2. `Booking` - 81 edges
3. `CampCenter.Application.Interfaces` - 58 edges
4. `CampCenter.Domain.Repositories` - 40 edges
5. `AdminBookingService` - 39 edges
6. `ScheduleService` - 37 edges
7. `useAuth()` - 35 edges
8. `IBookingRepository` - 32 edges
9. `CampCenter.Infrastructure.Persistence` - 31 edges
10. `AppDbContext` - 30 edges

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

## Communities (130 total, 41 thin omitted)

### Community 0 - "DTOs / Schedule (1)"
Cohesion: 0.07
Nodes (44): AbstractValidator, ScheduleController, CancellationToken, DateOnly, Guid, HttpDelete, HttpGet, HttpPost (+36 more)

### Community 1 - "Room Management"
Cohesion: 0.05
Nodes (48): RoomsController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+40 more)

### Community 2 - "Admin Bookings Controller & DTOs"
Cohesion: 0.05
Nodes (65): ControllerBase, BookingsController, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IActionResult (+57 more)

### Community 3 - "src / api (1)"
Cohesion: 0.13
Nodes (18): IconMoon(), IconSun(), IconSunSea(), LanguageSwitcher(), Props, SECTIONS, ThemeToggle(), getStoredLanguage() (+10 more)

### Community 4 - "CampCenter.UnitTests / Validators"
Cohesion: 0.18
Nodes (11): InitiatePaymentRequestDto, InitiatePaymentResponseDto, IPaymentService, CancellationToken, Task, BookingSettings, string, PaymentService (+3 more)

### Community 5 - "Room Task Management (1)"
Cohesion: 0.07
Nodes (41): TasksController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+33 more)

### Community 6 - "CampCenter.Application / Services (1)"
Cohesion: 0.15
Nodes (8): AccessToken, JwtSettings, string, JwtTokenService, int, string, RefreshTokenSettings, string

### Community 7 - "Room Mix Calculator Tests"
Cohesion: 0.12
Nodes (13): PeopleCount, SplitMix, RoomMixCalculator, SplitMix, Capacity, Dictionary, IReadOnlyDictionary, List (+5 more)

### Community 8 - "Payment Gateway Integration Tests (1)"
Cohesion: 0.07
Nodes (30): Amount, OrderId, Registered, SessionId, GatewayNotification, GatewayRegisterRequest, GatewayRegisterResult, IPaymentGateway (+22 more)

### Community 9 - "Project & NuGet Config"
Cohesion: 0.05
Nodes (38): BCrypt.Net-Next (4.2.0), FluentValidation (12.1.1), FluentValidation.DependencyInjectionExtensions (12.1.1), MailKit (4.14.1), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9), Microsoft.AspNetCore.Mvc.Testing (10.0.9), Microsoft.AspNetCore.OpenApi (10.0.9), Microsoft.Extensions.Http (10.0.0) (+30 more)

### Community 10 - "Frontend Icon Components"
Cohesion: 0.07
Nodes (19): Tile, TILES, IconArrowRight(), IconBed(), IconCalendar(), IconCheckSquare(), IconClipboard(), IconGrid() (+11 more)

### Community 11 - "CampCenter.Infrastructure / Repositories (1)"
Cohesion: 0.12
Nodes (26): ScheduleEntry, ScheduleEntryKind, DateOnly, DateTime, Guid, TimeOnly, ScheduleEntryRepository, BookingId (+18 more)

### Community 12 - "CampCenter.Application / Services (2)"
Cohesion: 0.06
Nodes (48): Skipped, BookingMealTimeDto, ScheduleEntryDto, ScheduleSettings, string, TimeOnly, ScheduleService, CancellationToken (+40 more)

### Community 13 - "Public Booking Frontend (1)"
Cohesion: 0.07
Nodes (35): Availability, AvailabilityCalendar, AvailabilityDay, BookingDetails, BookingPayment, cancelBooking(), createBooking(), CreateBookingInput (+27 more)

### Community 14 - "CampCenter.UnitTests / Services (1)"
Cohesion: 0.06
Nodes (40): Slot, MealTimeDefaultDto, MealGenerationPlanner, MealSlot, Date, DateOnly, End, IEnumerable (+32 more)

### Community 15 - "Admin Booking & Notifications (1)"
Cohesion: 0.27
Nodes (6): BookingCancelReason, BookingPaymentState, BookingStatus, BookingState, BookingStates, BookingStatuses

### Community 16 - "Room Closure Management"
Cohesion: 0.05
Nodes (64): AdminAssignment, ApplyBookingMealTimeResult, BookingGroupPage, BookingMealTime, BookingPaymentState, BookingScheduleDay, Closure, ClosureInput (+56 more)

### Community 17 - "tests / CampCenter.IntegrationTests (1)"
Cohesion: 0.20
Nodes (12): ScheduleApiTests, BookingId, DateOnly, End, Fact, Guid, HttpClient, int (+4 more)

### Community 18 - "Domain & Infra Namespaces"
Cohesion: 0.07
Nodes (50): bookingGroupCategories, BookingSchedule, checkScheduleConflicts(), createScheduleEntry(), Dashboard, deleteScheduleEntry(), getBookingSchedule(), getDashboard() (+42 more)

### Community 19 - "src / utils"
Cohesion: 0.15
Nodes (29): getAvailabilityCalendar(), CalendarTile(), Props, DateRangeField(), Props, DayCalendar(), Props, groupHue() (+21 more)

### Community 20 - "Integration Test Harness (1)"
Cohesion: 0.15
Nodes (14): HousekeepingJob, HousekeepingPlanner, DateOnly, IEnumerable, List, Booking, DateOnly, DateTime (+6 more)

### Community 21 - "CampCenter.Application / Services (3)"
Cohesion: 0.14
Nodes (22): UsersController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+14 more)

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
Cohesion: 0.67
Nodes (4): log(), prune_old(), run_backup(), backup-db.sh script

### Community 27 - "Booking Persistence & Entities (1)"
Cohesion: 0.12
Nodes (16): AdminUser, DateTime, Guid, AdminUserRole, IAdminUserRepository, CancellationToken, Guid, List (+8 more)

### Community 28 - "Application Namespaces & DTOs"
Cohesion: 0.16
Nodes (7): CampCenter.Application.DTOs.Users, CampCenter.Api.Controllers.Admin, CampCenter.Application.DTOs.AdminPanel, CampCenter.Application.Interfaces, CampCenter.Application.DTOs.Schedule, CampCenter.Api.Extensions, RoomDeleteResultDto

### Community 30 - "Public Booking Service (1)"
Cohesion: 0.20
Nodes (10): CreateBookingRequestDto, CreateBookingResponseDto, BookingService, CancellationToken, DateOnly, Dictionary, ILogger, IReadOnlyDictionary (+2 more)

### Community 31 - "Public Booking Service (2)"
Cohesion: 0.17
Nodes (6): CampCenter.Infrastructure.Auth, CampCenter.Api.RateLimiting, CampCenter.Api.Controllers, CampCenter.Api.Controllers.Public, RateLimitPolicies, string

### Community 32 - "Payment Gateway Integration Tests (2)"
Cohesion: 0.18
Nodes (10): IAsyncLifetime, IServiceProvider, IWebHostBuilder, PostgreSqlContainer, DataSeeder, CancellationToken, Task, CampCenterApiFactory (+2 more)

### Community 33 - "Docker & Project Docs"
Cohesion: 0.08
Nodes (27): Closure Model Replaces Camp Sessions, CampCenter Domain Model, GiST Exclusion Constraint Against Double Booking, Security Requirements, Dev Docker Compose Stack, campcenter-api (dev service), campcenter-frontend (dev service), Infra Docker Compose Stack (+19 more)

### Community 34 - "ClosureService"
Cohesion: 0.13
Nodes (8): CampCenter.Application.Models, CampCenter.Api.Background, CampCenter.Domain.Exceptions, CampCenter.Application.Common, CampCenter.Domain.Entities, CampCenter.Domain.Repositories, CampCenter.Application.Services, CampCenter.UnitTests.Services

### Community 35 - "components / admin"
Cohesion: 0.33
Nodes (4): PasswordRules, int, IRuleBuilder, IRuleBuilderOptions

### Community 36 - "Validator Unit Tests"
Cohesion: 0.05
Nodes (47): ClosuresController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+39 more)

### Community 37 - "CampCenter.UnitTests / Services (2)"
Cohesion: 0.05
Nodes (55): AllowWorkerWrite, HousekeepingController, CancellationToken, DateOnly, Guid, HttpGet, HttpPut, IActionResult (+47 more)

### Community 38 - "TypeScript App Config"
Cohesion: 0.09
Nodes (22): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection, moduleResolution (+14 more)

### Community 39 - "Admin User & Token Config"
Cohesion: 0.39
Nodes (6): AdminUserDto, UserService, CancellationToken, Guid, List, Task

### Community 40 - "TypeScript Node Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, moduleResolution, noEmit (+12 more)

### Community 41 - "Auth Service & Tokens"
Cohesion: 0.18
Nodes (12): ITokenService, RefreshTokenInfo, AuthService, CancellationToken, DateTime, Guid, Task, IRefreshTokenRepository (+4 more)

### Community 42 - "Root Task-Runner Scripts"
Cohesion: 0.10
Nodes (19): description, name, private, scripts, backend, build, dev, dev:down (+11 more)

### Community 43 - "CampCenter.UnitTests / Services (3)"
Cohesion: 0.07
Nodes (34): PricingController, CancellationToken, HttpGet, HttpPut, IActionResult, ProducesResponseType, Task, PublicAvailabilityController (+26 more)

### Community 46 - "AvailabilityService"
Cohesion: 0.12
Nodes (16): Aktualizacja po każdej zmianie (obowiązkowe), Architektura, Build environment, Bun responsibilities, CampCenter, Cel projektu, Decision rule, Eksploracja przez vault (oszczędność tokenów) (+8 more)

### Community 48 - "Frontend App Shell & i18n"
Cohesion: 0.36
Nodes (7): BookingGroupCategory, BookingStatus, bookingStatuses, DashboardBooking, getBookingGroupPage(), BookingGroupSection(), Props

### Community 49 - "Auth Controller (1)"
Cohesion: 0.15
Nodes (15): CookieOptions, AuthController, CancellationToken, DateTime, EnableRateLimiting, HttpPost, IActionResult, IValidator (+7 more)

### Community 50 - ".Update"
Cohesion: 0.32
Nodes (6): Payment, PaymentKind, PaymentStatus, DateTime, Guid, EntityTypeBuilder

### Community 51 - "Refresh Token Repository"
Cohesion: 0.10
Nodes (16): DbContext, DbSet, IDesignTimeDbContextFactory, RefreshToken, DateTime, Guid, AppDbContext, ModelBuilder (+8 more)

### Community 52 - "Exception"
Cohesion: 0.12
Nodes (9): CampCenter.Infrastructure.Repositories, CampCenter.Api.Errors, CampCenter.Infrastructure, CampCenter.Infrastructure.Persistence, CampCenter.Infrastructure.Persistence.Seed, IConfiguration, Program, DependencyInjection (+1 more)

### Community 53 - "useAuth"
Cohesion: 0.17
Nodes (19): AdminBooking, BookingState, bookingStates, createAdminBooking(), formatZl(), getAdminBookings(), getPricingDefaults(), groszeToZl() (+11 more)

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
Nodes (12): EmailMessage, IEmailSender, CancellationToken, Task, EmailTemplates, DateOnly, DateTime, RecordingEmailSender (+4 more)

### Community 59 - "RoomNumberComparer"
Cohesion: 0.33
Nodes (4): GeneratedRegex, IComparer, Regex, RoomNumberComparer

### Community 60 - "Frontend Runtime Deps"
Cohesion: 0.15
Nodes (13): axios, dependencies, axios, i18next, react, react-dom, react-i18next, react-router-dom (+5 more)

### Community 61 - ".GetBlockedRoomIdsAsync"
Cohesion: 0.07
Nodes (16): CampCenter.Application.Validators, CampCenter.Application.DTOs.Auth, CampCenter.UnitTests.Validators, InlineData, LoginResponseDto, CreateBookingRequestValidator, Dictionary, LoginRequestValidator (+8 more)

### Community 62 - "RoomCleaningRepository"
Cohesion: 0.22
Nodes (5): Migration, InitialAuth, MigrationBuilder, ScheduleAndMealTimes, MigrationBuilder

### Community 64 - "src / api (2)"
Cohesion: 0.25
Nodes (9): PublicBookingApiTests, Capacity, Count, DateOnly, Dictionary, Fact, HttpClient, long (+1 more)

### Community 68 - "WriteRequiresAdministratorHandler"
Cohesion: 0.16
Nodes (11): Attribute, AuthorizationHandler, AuthorizationHandlerContext, CampCenter.Api.Auth, IAuthorizationRequirement, IHttpContextAccessor, AllowWorkerWriteAttribute, WriteRequiresAdministratorHandler (+3 more)

### Community 70 - "OpenAPI Security Scheme"
Cohesion: 0.20
Nodes (8): CampCenter.Api.OpenApi, IOpenApiDocumentTransformer, OpenApiDocument, OpenApiDocumentTransformerContext, BearerSecuritySchemeTransformer, CancellationToken, string, Task

### Community 73 - "Booking Maintenance Background Service"
Cohesion: 0.31
Nodes (7): BackgroundService, IServiceScopeFactory, BookingMaintenanceService, CancellationToken, ILogger, Task, TimeSpan

### Community 74 - "PasswordRules"
Cohesion: 0.27
Nodes (8): Admin, Task, UsersAndRolesApiTests, Fact, HttpClient, string, Task, Worker

### Community 75 - "SmtpEmailSender"
Cohesion: 0.29
Nodes (4): CampCenter.Infrastructure.Payments, RegisterData, RegisterData, RegisterResponse

### Community 76 - "CampCenter.Application.DTOs.Public"
Cohesion: 0.22
Nodes (4): CampCenter.IntegrationTests, CampCenter.Application.DTOs.Rooms, CampCenter.Application.DTOs.Public, CampCenter.Application.DTOs.Closures

### Community 77 - "BookingSettings"
Cohesion: 0.22
Nodes (13): PublicBookingsController, CancellationToken, EnableRateLimiting, HttpGet, HttpPost, IActionResult, IValidator, ProducesResponseType (+5 more)

### Community 78 - "eslint"
Cohesion: 0.25
Nodes (6): CampCenter.Infrastructure.Email, EmailSettings, string, SmtpEmailSender, CancellationToken, Task

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
Cohesion: 0.11
Nodes (10): CampCenter.Infrastructure.Persistence.Migrations, ModelSnapshot, SuppressDeletedGeneratedMeals, ModelBuilder, RoomCleanings, ModelBuilder, SupervisorCountsAndRates, ModelBuilder (+2 more)

### Community 89 - "Persistence / Migrations (2)"
Cohesion: 0.25
Nodes (7): AvailabilityCalendarDto, AvailabilityDayDto, AvailabilityDto, BookingDetailsDto, BookingPaymentDto, PublicClosureDto, PublicPricingDto

### Community 99 - "MealTimeDefault"
Cohesion: 0.09
Nodes (24): AssignableRoom, createMealTime(), deleteMealTime(), getAdminBooking(), getAssignableRooms(), getMealTimes(), MealKind, mealKinds (+16 more)

### Community 102 - "Persistence / Migrations (10)"
Cohesion: 0.16
Nodes (15): AdminUser, createUser(), deleteUser(), getUsers(), setUserPassword(), setUserRole(), userRoles, UserRole (+7 more)

### Community 106 - "frontend (1)"
Cohesion: 0.67
Nodes (3): Lakeside Typography (Bricolage Grotesque + Inter), SPA HTML Shell, Pre-Paint Theme Restore

### Community 109 - "IntegrationTestBase"
Cohesion: 0.57
Nodes (3): AdminPricingApiTests, Fact, Task

### Community 111 - "Prettier Dependency"
Cohesion: 0.18
Nodes (6): Exception, BusinessRuleViolationException, ConcurrencyConflictException, ConflictException, ForbiddenActionException, NotFoundException

### Community 112 - ".WithRoomsAsync"
Cohesion: 0.49
Nodes (4): AdminSupervisorRoomsApiTests, Fact, HttpClient, Task

### Community 117 - "BookingConfiguration"
Cohesion: 0.11
Nodes (12): CampCenter.Infrastructure.Persistence.Configurations, IEntityTypeConfiguration, BookingRoomAssignment, DateOnly, Guid, BookingConfiguration, EntityTypeBuilder, BookingRoomAssignmentConfiguration (+4 more)

### Community 124 - "AdminPanelApiTests"
Cohesion: 0.44
Nodes (4): AdminPanelApiTests, DateOnly, Fact, Task

### Community 125 - "AuthApiTests"
Cohesion: 0.44
Nodes (4): HttpClient, AuthApiTests, Fact, Task

### Community 128 - ".NextFreeSitting"
Cohesion: 0.40
Nodes (3): CampCenter.Application, DependencyInjection, IServiceCollection

### Community 129 - "RoomsAndClosuresApiTests"
Cohesion: 0.27
Nodes (7): ICollectionFixture, ApiCollection, IntegrationTestBase, string, RoomsAndClosuresApiTests, Fact, Task

## Knowledge Gaps
- **255 isolated node(s):** `printWidth`, `name`, `version`, `private`, `type` (+250 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **41 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `CampCenter.Domain.Entities` connect `ClosureService` to `DTOs / Schedule (1)`, `Room Management`, `Admin Bookings Controller & DTOs`, `Room Task Management (1)`, `CampCenter.Infrastructure / Repositories (1)`, `CampCenter.Application / Services (2)`, `CampCenter.UnitTests / Services (1)`, `Admin Booking & Notifications (1)`, `Integration Test Harness (1)`, `Booking Persistence & Entities (1)`, `Application Namespaces & DTOs`, `Public Booking Service (2)`, `Validator Unit Tests`, `CampCenter.UnitTests / Services (2)`, `CampCenter.UnitTests / Services (3)`, `.Update`, `Refresh Token Repository`, `Exception`, `RoomNumberComparer`, `.GetBlockedRoomIdsAsync`, `WriteRequiresAdministratorHandler`, `CampCenter.Application.DTOs.Public`, `BookingConfiguration`?**
  _High betweenness centrality (0.094) - this node is a cross-community bridge._
- **Why does `CampCenter.Application.Interfaces` connect `Application Namespaces & DTOs` to `.NextFreeSitting`, `ClosureService`, `CampCenter.UnitTests / Validators`, `Payment Gateway Integration Tests (1)`, `SmtpEmailSender`, `CampCenter.Application.DTOs.Public`, `eslint`, `Admin Booking & Notifications (2)`, `Exception`, `Admin Booking & Notifications (4)`, `.GetBlockedRoomIdsAsync`, `Public Booking Service (2)`?**
  _High betweenness centrality (0.068) - this node is a cross-community bridge._
- **Why does `Booking` connect `Integration Test Harness (1)` to `Admin Bookings Controller & DTOs`, `Room Task Management (1)`, `CampCenter.UnitTests / Services (2)`, `CampCenter.Infrastructure / Repositories (1)`, `CampCenter.Application / Services (2)`, `Admin Booking & Notifications (1)`, `.Update`, `Refresh Token Repository`, `BookingConfiguration`, `CampCenter.Domain / Repositories (1)`, `Integration Test Harness (2)`, `Admin Booking & Notifications (4)`, `Public Booking Service (1)`?**
  _High betweenness centrality (0.065) - this node is a cross-community bridge._
- **What connects `printWidth`, `name`, `version` to the rest of the system?**
  _255 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DTOs / Schedule (1)` be split into smaller, more focused modules?**
  _Cohesion score 0.0721120984278879 - nodes in this community are weakly interconnected._
- **Should `Room Management` be split into smaller, more focused modules?**
  _Cohesion score 0.05362614913176711 - nodes in this community are weakly interconnected._
- **Should `Admin Bookings Controller & DTOs` be split into smaller, more focused modules?**
  _Cohesion score 0.051368578927634044 - nodes in this community are weakly interconnected._