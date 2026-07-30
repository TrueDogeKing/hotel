# Graph Report - .  (2026-07-30)

## Corpus Check
- 128 files · ~77,183 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2259 nodes · 4955 edges · 154 communities (89 shown, 65 thin omitted)
- Extraction: 96% EXTRACTED · 4% INFERRED · 0% AMBIGUOUS · INFERRED: 218 edges (avg confidence: 0.8)
- Token cost: 9,000 input · 3,500 output

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
- Controllers / Admin
- CampCenter.Domain / Repositories (3)
- CampCenter.Infrastructure / Repositories (2)
- CampCenter.Infrastructure / Repositories (3)
- OpenAPI Security Scheme
- EF Core Migrations (1)
- Booking Persistence & Entities (3)
- Booking Maintenance Background Service
- Admin Booking & Notifications (5)
- CampCenter.UnitTests / Services (5)
- Auth Controller (2)
- Booking Persistence & Entities (4)
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
- Infrastructure DI Registration
- EF Core Migrations (5)
- Persistence / Migrations (8)
- Persistence / Migrations (9)
- Persistence / Migrations (10)
- Persistence / Migrations (11)
- Persistence / Migrations (12)
- Persistence / Migrations (13)
- frontend (1)
- Public Booking Frontend (2)
- Root TS Config
- ESLint Package
- React Hooks ESLint Plugin
- Prettier Dependency
- Node Type Definitions
- React DOM Type Definitions
- TypeScript Dependency
- Prettier Config
- App Brand Identity
- Unlabeled (1)
- frontend (3)
- src / assets (1)
- src / assets (2)
- Unlabeled (2)
- Unlabeled (3)
- Unlabeled (4)
- Unlabeled (5)
- Unlabeled (6)
- Unlabeled (7)
- Unlabeled (8)
- Unlabeled (9)
- Unlabeled (10)
- Unlabeled (11)
- Unlabeled (12)
- Unlabeled (13)
- Unlabeled (14)
- Unlabeled (15)
- Unlabeled (16)
- Unlabeled (17)
- Unlabeled (18)
- Unlabeled (19)
- Unlabeled (20)
- Unlabeled (21)
- Unlabeled (22)
- Unlabeled (23)
- Unlabeled (24)
- Unlabeled (25)
- Unlabeled (26)
- Unlabeled (27)
- Unlabeled (28)
- Unlabeled (29)
- Unlabeled (30)
- Unlabeled (31)
- Unlabeled (32)

## God Nodes (most connected - your core abstractions)
1. `Booking` - 77 edges
2. `CampCenter.Domain.Entities` - 75 edges
3. `CampCenter.Application.Interfaces` - 53 edges
4. `CampCenter.Domain.Repositories` - 37 edges
5. `ScheduleService` - 36 edges
6. `IBookingRepository` - 30 edges
7. `MealTimeDefault` - 29 edges
8. `AdminBookingService` - 28 edges
9. `ScheduleEntry` - 28 edges
10. `ScheduleApiTests` - 26 edges

## Surprising Connections (you probably didn't know these)
- `CampCenterApiFactory` --references--> `Program`  [EXTRACTED]
  tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs → src/CampCenter.Api/Program.cs
- `CI Backend Job (build + tests)` --conceptually_related_to--> `campcenter-db (PostgreSQL 16-alpine)`  [INFERRED]
  .github/workflows/ci.yml → docker/docker-compose.infra.yml
- `campcenter-api (prod service)` --references--> `Przelewy24 Payments and Webhook`  [INFERRED]
  docker/docker-compose.prod.yml → README.md
- `HousekeepingServiceTests` --references--> `HousekeepingService`  [EXTRACTED]
  tests/CampCenter.UnitTests/Services/HousekeepingServiceTests.cs → src/CampCenter.Application/Services/HousekeepingService.cs
- `ScheduleConflictTests` --references--> `ScheduleService`  [EXTRACTED]
  tests/CampCenter.UnitTests/Services/ScheduleConflictTests.cs → src/CampCenter.Application/Services/ScheduleService.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Booking Lifecycle (availability → deposit → confirmation)** — readme_booking_flow, readme_p24_payments, claude_gist_double_booking_guard, claude_domain_model [INFERRED 0.85]
- **Project Conventions (task runners, build env, knowledge graph)** — claude_task_runner_rules, claude_build_environment, claude_knowledge_graph_workflow [EXTRACTED 1.00]
- **Production Stack (Caddy -> frontend/api -> PostgreSQL)** — docker_docker_compose_prod_caddy, docker_docker_compose_prod_api, docker_docker_compose_prod_postgres [EXTRACTED 1.00]
- **CI Validation Pipeline (backend + frontend)** — github_workflows_ci_workflow, github_workflows_ci_backend_job, github_workflows_ci_frontend_job [EXTRACTED 1.00]

## Communities (154 total, 65 thin omitted)

### Community 0 - "DTOs / Schedule (1)"
Cohesion: 0.07
Nodes (45): AbstractValidator, ScheduleController, CancellationToken, DateOnly, Guid, HttpDelete, HttpGet, HttpPost (+37 more)

### Community 1 - "Room Management"
Cohesion: 0.06
Nodes (40): RoomsController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+32 more)

### Community 2 - "Admin Bookings Controller & DTOs"
Cohesion: 0.06
Nodes (50): ControllerBase, BookingsController, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IActionResult (+42 more)

### Community 3 - "src / api (1)"
Cohesion: 0.06
Nodes (49): AdminAssignment, AdminBooking, ApplyBookingMealTimeResult, BookingSchedule, BookingScheduleDay, BookingStatus, bookingStatuses, checkScheduleConflicts() (+41 more)

### Community 4 - "CampCenter.UnitTests / Validators"
Cohesion: 0.08
Nodes (23): MealTimesController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+15 more)

### Community 5 - "Room Task Management (1)"
Cohesion: 0.09
Nodes (29): RoomTaskDto, IRoomTaskService, CancellationToken, Guid, List, Task, RoomTaskService, CancellationToken (+21 more)

### Community 6 - "CampCenter.Application / Services (1)"
Cohesion: 0.09
Nodes (31): ClosuresController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+23 more)

### Community 7 - "Room Mix Calculator Tests"
Cohesion: 0.07
Nodes (27): CampCenter.Application.DTOs.Public, IReadOnlyDictionary, PeopleCount, BookingDetailsDto, BookingPaymentDto, CreateBookingRequestDto, CreateBookingResponseDto, PublicSessionDto (+19 more)

### Community 8 - "Payment Gateway Integration Tests (1)"
Cohesion: 0.07
Nodes (27): PublicPaymentsController, CancellationToken, HttpPost, IActionResult, ProducesResponseType, Task, GatewayNotification, GatewayRegisterRequest (+19 more)

### Community 9 - "Project & NuGet Config"
Cohesion: 0.05
Nodes (38): BCrypt.Net-Next (4.2.0), FluentValidation (12.1.1), FluentValidation.DependencyInjectionExtensions (12.1.1), MailKit (4.14.1), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9), Microsoft.AspNetCore.Mvc.Testing (10.0.9), Microsoft.AspNetCore.OpenApi (10.0.9), Microsoft.Extensions.Http (10.0.0) (+30 more)

### Community 10 - "Frontend Icon Components"
Cohesion: 0.07
Nodes (21): IconArrowRight(), IconBed(), IconLandscape(), IconMail(), IconMap(), IconMapPin(), IconMoon(), IconPhone() (+13 more)

### Community 11 - "CampCenter.Infrastructure / Repositories (1)"
Cohesion: 0.10
Nodes (25): AdminUser, DbContext, DbSet, IDesignTimeDbContextFactory, RefreshToken, AppDbContext, BookingRoomAssignment, ModelBuilder (+17 more)

### Community 12 - "CampCenter.Application / Services (2)"
Cohesion: 0.19
Nodes (11): Skipped, BookingMealTimeDto, ScheduleEntryDto, ScheduleService, CancellationToken, DateOnly, Guid, List (+3 more)

### Community 13 - "Public Booking Frontend (1)"
Cohesion: 0.10
Nodes (25): cancelAdminBooking(), formatZl(), getAdminBookings(), Availability, BookingDetails, BookingPayment, cancelBooking(), createBooking() (+17 more)

### Community 14 - "CampCenter.UnitTests / Services (1)"
Cohesion: 0.13
Nodes (16): Slot, MealGenerationPlanner, MealSlot, Date, DateOnly, End, IEnumerable, IReadOnlyCollection (+8 more)

### Community 15 - "Admin Booking & Notifications (1)"
Cohesion: 0.16
Nodes (18): AdminBookingDto, AdminBookingService, BookingRoomAssignment, BookingSettings, CancellationToken, DateOnly, DateTime, Guid (+10 more)

### Community 16 - "Room Closure Management"
Cohesion: 0.12
Nodes (18): Closure, DateOnly, DateTime, Guid, IClosureRepository, CancellationToken, DateOnly, Guid (+10 more)

### Community 17 - "tests / CampCenter.IntegrationTests (1)"
Cohesion: 0.19
Nodes (12): ScheduleApiTests, BookingId, DateOnly, End, Fact, Guid, HttpClient, int (+4 more)

### Community 18 - "Domain & Infra Namespaces"
Cohesion: 0.14
Nodes (5): CampCenter.Api.Background, CampCenter.Infrastructure.Repositories, CampCenter.Domain.Entities, CampCenter.Domain.Repositories, CampCenter.Infrastructure.Persistence

### Community 19 - "src / utils"
Cohesion: 0.14
Nodes (28): getScheduleCalendar(), getScheduleDay(), ScheduleCalendar, CalendarTile(), Props, groupHue(), LaneEvent, packLanes() (+20 more)

### Community 20 - "Integration Test Harness (1)"
Cohesion: 0.11
Nodes (18): Capacity, IntegrationTestBase, AdminPanelApiTests, DateOnly, Fact, Task, PublicBookingApiTests, Count (+10 more)

### Community 21 - "CampCenter.Application / Services (3)"
Cohesion: 0.15
Nodes (17): MealTimeDefaultDto, MealTimeService, CancellationToken, Guid, List, Task, TimeOnly, MealKind (+9 more)

### Community 22 - "CampCenter.Domain / Repositories (1)"
Cohesion: 0.15
Nodes (21): ScheduleEntry, ScheduleEntryKind, DateOnly, DateTime, Guid, TimeOnly, IScheduleEntryRepository, BookingId (+13 more)

### Community 23 - "Camp Session Management"
Cohesion: 0.17
Nodes (15): CampCenter.Application.DTOs.Sessions, CampSessionDto, CreateCampSessionRequestDto, UpdateCampSessionRequestDto, ICampSessionService, CancellationToken, Guid, List (+7 more)

### Community 24 - "Integration Test Harness (2)"
Cohesion: 0.10
Nodes (19): IAsyncLifetime, ICollectionFixture, IServiceProvider, IWebHostBuilder, PostgreSqlContainer, DataSeeder, CancellationToken, Task (+11 more)

### Community 25 - "Frontend Auth & API Client"
Cohesion: 0.15
Nodes (23): login(), logout(), api, refreshAccessToken(), RetriableConfig, decodeJWT(), getUserIdFromToken(), getUserLoginFromToken() (+15 more)

### Community 26 - "CampCenter.Domain / Repositories (2)"
Cohesion: 0.14
Nodes (17): IReadOnlyList, BookingMealTime, DateTime, Guid, TimeOnly, IBookingMealTimeRepository, CancellationToken, Guid (+9 more)

### Community 27 - "Booking Persistence & Entities (1)"
Cohesion: 0.19
Nodes (12): BookingRepository, BookingRoomAssignment, CancellationToken, DateOnly, DateTime, Dictionary, Guid, IReadOnlyCollection (+4 more)

### Community 28 - "Application Namespaces & DTOs"
Cohesion: 0.14
Nodes (7): CampCenter.Application.DTOs.Rooms, CampCenter.Api.Controllers.Admin, CampCenter.Api.Controllers.Public, CampCenter.Application.DTOs.AdminPanel, CampCenter.Application.Interfaces, CampCenter.Application.DTOs.Schedule, RoomDeleteResultDto

### Community 29 - "Admin Booking & Notifications (2)"
Cohesion: 0.19
Nodes (12): IBookingRepository, BookingRoomAssignment, CancellationToken, DateOnly, DateTime, Dictionary, Guid, IReadOnlyCollection (+4 more)

### Community 30 - "Public Booking Service (1)"
Cohesion: 0.16
Nodes (19): EnableRateLimiting, PublicBookingsController, CancellationToken, CreateBookingRequestDto, HttpGet, HttpPost, IActionResult, InitiatePaymentRequestDto (+11 more)

### Community 31 - "Public Booking Service (2)"
Cohesion: 0.15
Nodes (15): BookingService, BookingDetailsDto, BookingSettings, CancellationToken, CreateBookingRequestDto, CreateBookingResponseDto, DateOnly, EmailMessage (+7 more)

### Community 32 - "Payment Gateway Integration Tests (2)"
Cohesion: 0.14
Nodes (18): Amount, GatewayRegisterRequest, GatewayRegisterResult, IPaymentGateway, OrderId, Registered, SessionId, FakePaymentGateway (+10 more)

### Community 33 - "Docker & Project Docs"
Cohesion: 0.09
Nodes (27): Build Environment (.NET SDK 10), Closure Model Replaces Camp Sessions, CampCenter Domain Model, GiST Exclusion Constraint Against Double Booking, Knowledge Graph / Obsidian Vault Workflow, Security Requirements, Task Runner Rules (Mise vs Bun), Dev Docker Compose Stack (+19 more)

### Community 34 - "Rate Limiting & Startup"
Cohesion: 0.09
Nodes (13): CampCenter.Application.Models, CampCenter.Infrastructure.Auth, CampCenter.Api.RateLimiting, CampCenter.Api.Controllers, CampCenter.Application.DTOs.Auth, RateLimitPolicies, string, LoginResponseDto (+5 more)

### Community 35 - "components / admin"
Cohesion: 0.13
Nodes (23): ScheduleDay, ScheduleEntry, ScheduleEntryInput, ScheduleEntryKind, getBookingSchedule(), PublicSchedule, buildChips(), Chip (+15 more)

### Community 36 - "Validator Unit Tests"
Cohesion: 0.09
Nodes (12): CampCenter.Application.Validators, LoginRequestValidator, MealTimeRules, string, PasswordRules, int, IRuleBuilder, IRuleBuilderOptions (+4 more)

### Community 37 - "CampCenter.UnitTests / Services (2)"
Cohesion: 0.23
Nodes (10): Room, HousekeepingServiceTests, DateOnly, Fact, Guid, IClosureRepository, IEnumerable, IRoomRepository (+2 more)

### Community 38 - "TypeScript App Config"
Cohesion: 0.09
Nodes (22): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection, moduleResolution (+14 more)

### Community 39 - "Admin User & Token Config"
Cohesion: 0.14
Nodes (13): AdminUser, DateTime, Guid, IAdminUserRepository, CancellationToken, Guid, Task, AdminUserConfiguration (+5 more)

### Community 40 - "TypeScript Node Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, moduleResolution, noEmit (+12 more)

### Community 41 - "Auth Service & Tokens"
Cohesion: 0.21
Nodes (11): AuthResult, AuthService, CancellationToken, DateTime, Guid, Task, IRefreshTokenRepository, CancellationToken (+3 more)

### Community 42 - "Root Task-Runner Scripts"
Cohesion: 0.10
Nodes (19): description, name, private, scripts, backend, build, dev, dev:down (+11 more)

### Community 43 - "CampCenter.UnitTests / Services (3)"
Cohesion: 0.18
Nodes (9): HousekeepingJob, HousekeepingPlanner, DateOnly, IEnumerable, List, HousekeepingPlannerTests, DateOnly, Fact (+1 more)

### Community 44 - "CampCenter.UnitTests / Services (4)"
Cohesion: 0.42
Nodes (5): ScheduleConflictTests, DateOnly, Fact, Guid, Task

### Community 45 - "Booking Persistence & Entities (2)"
Cohesion: 0.12
Nodes (12): ScheduleCalendarBookingDto, Booking, BookingCancelReason, BookingStatus, BookingRoomAssignment, DateOnly, DateTime, Guid (+4 more)

### Community 46 - "JWT Token Service"
Cohesion: 0.12
Nodes (9): ITokenService, AccessToken, RefreshTokenInfo, JwtSettings, string, JwtTokenService, int, RefreshTokenSettings (+1 more)

### Community 47 - "tests / CampCenter.IntegrationTests (2)"
Cohesion: 0.12
Nodes (8): CampCenter.IntegrationTests, CampCenter.Application, CampCenter.Infrastructure, CampCenter.Application.DTOs.Closures, CampCenter.Infrastructure.Persistence.Seed, Program, DependencyInjection, IServiceCollection

### Community 48 - "Frontend App Shell & i18n"
Cohesion: 0.13
Nodes (12): createMealTime(), deleteMealTime(), getMealTimes(), MealKind, mealKinds, MealTimeDefault, updateMealTime(), App() (+4 more)

### Community 49 - "Auth Controller (1)"
Cohesion: 0.25
Nodes (10): CookieOptions, AuthController, CancellationToken, DateTime, EnableRateLimiting, HttpPost, IActionResult, IValidator (+2 more)

### Community 50 - "Admin Frontend Pages"
Cohesion: 0.14
Nodes (15): Closure, createClosure(), createRoom(), deleteClosure(), deleteRoom(), getClosures(), getRooms(), updateClosure() (+7 more)

### Community 51 - "Refresh Token Repository"
Cohesion: 0.18
Nodes (10): RefreshToken, DateTime, Guid, RefreshTokenConfiguration, EntityTypeBuilder, RefreshTokenRepository, CancellationToken, DateTime (+2 more)

### Community 52 - "Admin Booking & Notifications (3)"
Cohesion: 0.17
Nodes (13): InitiatePaymentResponseDto, IPaymentService, PaymentService, BookingSettings, CancellationToken, EmailMessage, GatewayNotification, IEmailSender (+5 more)

### Community 53 - "Room Task Management (2)"
Cohesion: 0.30
Nodes (11): IRoomTaskService, RoomTaskStatus, TasksController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost (+3 more)

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
Cohesion: 0.21
Nodes (7): EmailMessage, IEmailSender, CancellationToken, Task, EmailTemplates, DateOnly, DateTime

### Community 58 - "Persistence / Configurations"
Cohesion: 0.16
Nodes (8): CampCenter.Infrastructure.Persistence.Configurations, IEntityTypeConfiguration, BookingMealTimeConfiguration, EntityTypeBuilder, MealTimeDefaultConfiguration, EntityTypeBuilder, ScheduleEntryConfiguration, EntityTypeBuilder

### Community 59 - "DTOs / AdminPanel"
Cohesion: 0.24
Nodes (10): HousekeepingDayDto, HousekeepingDaySummaryDto, HousekeepingRangeDto, HousekeepingRoomDto, SetRoomCleaningRequestDto, IHousekeepingService, CancellationToken, DateOnly (+2 more)

### Community 60 - "Frontend Runtime Deps"
Cohesion: 0.15
Nodes (13): axios, dependencies, axios, i18next, react, react-dom, react-i18next, react-router-dom (+5 more)

### Community 61 - "CampCenter.Application / Services (4)"
Cohesion: 0.31
Nodes (9): HousekeepingService, CancellationToken, DateOnly, Guid, IClosureRepository, IRoomRepository, IRoomTaskRepository, List (+1 more)

### Community 62 - "CampCenter.Domain / Entities"
Cohesion: 0.19
Nodes (8): RoomCleaning, RoomCleaningKind, RoomCleaningStatus, DateOnly, DateTime, Guid, RoomCleaningConfiguration, EntityTypeBuilder

### Community 63 - "Domain Exceptions"
Cohesion: 0.23
Nodes (7): CampCenter.Domain.Exceptions, Exception, BusinessRuleViolationException, ConcurrencyConflictException, ConflictException, ForbiddenActionException, NotFoundException

### Community 64 - "src / api (2)"
Cohesion: 0.20
Nodes (11): BookingMealTime, deleteBookingMeals(), getBookingMealTimes(), NeighbourSitting, resetBookingMealTime(), setBookingMealTime(), clashingNeighbours(), GroupMealTimes() (+3 more)

### Community 65 - "Admin Tasks & Occupancy Pages"
Cohesion: 0.26
Nodes (10): createTask(), deleteTask(), getOccupancy(), getTasks(), Occupancy, RoomOccupancy, RoomTask, setTaskDone() (+2 more)

### Community 66 - "Controllers / Admin"
Cohesion: 0.33
Nodes (9): HousekeepingController, CancellationToken, DateOnly, Guid, HttpGet, HttpPut, IActionResult, ProducesResponseType (+1 more)

### Community 67 - "CampCenter.Domain / Repositories (3)"
Cohesion: 0.32
Nodes (7): IRoomCleaningRepository, CancellationToken, DateOnly, Dictionary, Guid, List, Task

### Community 68 - "CampCenter.Infrastructure / Repositories (2)"
Cohesion: 0.35
Nodes (5): MealTimeDefaultRepository, CancellationToken, Guid, List, Task

### Community 69 - "CampCenter.Infrastructure / Repositories (3)"
Cohesion: 0.32
Nodes (7): RoomCleaningRepository, CancellationToken, DateOnly, Dictionary, Guid, List, Task

### Community 70 - "OpenAPI Security Scheme"
Cohesion: 0.20
Nodes (8): CampCenter.Api.OpenApi, IOpenApiDocumentTransformer, OpenApiDocument, OpenApiDocumentTransformerContext, BearerSecuritySchemeTransformer, CancellationToken, string, Task

### Community 71 - "EF Core Migrations (1)"
Cohesion: 0.20
Nodes (5): CampCenter.Infrastructure.Persistence.Migrations, InitialAuth, ModelBuilder, RoomCleanings, ModelBuilder

### Community 72 - "Booking Persistence & Entities (3)"
Cohesion: 0.27
Nodes (7): Payment, PaymentKind, PaymentStatus, DateTime, Guid, PaymentConfiguration, EntityTypeBuilder

### Community 73 - "Booking Maintenance Background Service"
Cohesion: 0.31
Nodes (7): BackgroundService, IServiceScopeFactory, BookingMaintenanceService, CancellationToken, ILogger, Task, TimeSpan

### Community 74 - "Admin Booking & Notifications (5)"
Cohesion: 0.25
Nodes (6): CampCenter.Infrastructure.Email, EmailSettings, string, SmtpEmailSender, CancellationToken, Task

### Community 76 - "Auth Controller (2)"
Cohesion: 0.39
Nodes (4): LoginRequestDto, IAuthService, CancellationToken, Task

### Community 77 - "Booking Persistence & Entities (4)"
Cohesion: 0.29
Nodes (5): BookingRoomAssignment, DateOnly, Guid, BookingRoomAssignmentConfiguration, EntityTypeBuilder

### Community 78 - "Przelewy24 Payment Client"
Cohesion: 0.29
Nodes (4): CampCenter.Infrastructure.Payments, RegisterData, RegisterData, RegisterResponse

### Community 79 - "Frontend Build Scripts"
Cohesion: 0.29
Nodes (7): scripts, build, dev, format, format:check, lint, preview

### Community 80 - "Social Icon Sprite"
Cohesion: 0.38
Nodes (7): Bluesky Icon, Discord Icon, Documentation Icon, GitHub Icon, Social Icon, Icon Sprite Sheet, X (Twitter) Icon

### Community 82 - "Claims Principal Extensions"
Cohesion: 0.33
Nodes (4): ClaimsPrincipal, CampCenter.Api.Extensions, ClaimsPrincipalExtensions, Guid

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
Cohesion: 0.40
Nodes (4): DeleteBookingMealsResultDto, NeighbourSittingDto, UpdateMealTimeDefaultRequestDto, UpdateMealTimeDefaultRequestValidator

### Community 98 - "Infrastructure DI Registration"
Cohesion: 0.50
Nodes (3): IConfiguration, DependencyInjection, IServiceCollection

### Community 106 - "frontend (1)"
Cohesion: 0.67
Nodes (3): Lakeside Typography (Bricolage Grotesque + Inter), SPA HTML Shell, Pre-Paint Theme Restore

## Knowledge Gaps
- **223 isolated node(s):** `printWidth`, `name`, `version`, `private`, `type` (+218 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **65 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Room` connect `CampCenter.UnitTests / Services (2)` to `Admin Frontend Pages`, `src / api (1)`?**
  _High betweenness centrality (0.176) - this node is a cross-community bridge._
- **Why does `Booking` connect `Booking Persistence & Entities (2)` to `Room Task Management (1)`, `CampCenter.UnitTests / Services (2)`, `Booking Persistence & Entities (3)`, `CampCenter.UnitTests / Services (3)`, `CampCenter.Application / Services (2)`, `CampCenter.Infrastructure / Repositories (1)`, `Admin Booking & Notifications (1)`, `CampCenter.Domain / Repositories (1)`, `Admin Booking & Notifications (4)`, `CampCenter.Domain / Repositories (2)`, `Booking Persistence & Entities (1)`, `Admin Booking & Notifications (2)`, `Public Booking Service (2)`?**
  _High betweenness centrality (0.170) - this node is a cross-community bridge._
- **Why does `CampCenter.Domain.Entities` connect `Domain & Infra Namespaces` to `DTOs / Schedule (1)`, `Room Management`, `Room Task Management (1)`, `CampCenter.UnitTests / Services (1)`, `Room Closure Management`, `CampCenter.Application / Services (3)`, `CampCenter.Domain / Repositories (1)`, `Application Namespaces & DTOs`, `Rate Limiting & Startup`, `Validator Unit Tests`, `Admin User & Token Config`, `CampCenter.UnitTests / Services (3)`, `Booking Persistence & Entities (2)`, `tests / CampCenter.IntegrationTests (2)`, `Refresh Token Repository`, `Persistence / Configurations`, `CampCenter.Domain / Entities`, `Booking Persistence & Entities (3)`, `CampCenter.UnitTests / Services (5)`, `Booking Persistence & Entities (4)`?**
  _High betweenness centrality (0.137) - this node is a cross-community bridge._
- **What connects `printWidth`, `name`, `version` to the rest of the system?**
  _223 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DTOs / Schedule (1)` be split into smaller, more focused modules?**
  _Cohesion score 0.0691333982473223 - nodes in this community are weakly interconnected._
- **Should `Room Management` be split into smaller, more focused modules?**
  _Cohesion score 0.06378378378378378 - nodes in this community are weakly interconnected._
- **Should `Admin Bookings Controller & DTOs` be split into smaller, more focused modules?**
  _Cohesion score 0.05835010060362173 - nodes in this community are weakly interconnected._