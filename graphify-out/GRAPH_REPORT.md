# Graph Report - hotel  (2026-08-06)

## Corpus Check
- 291 files · ~111,579 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2545 nodes · 6231 edges · 149 communities (103 shown, 46 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 367 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `a5d22c76`
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
- ClosureService
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
- GroupRooms.tsx
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
- EF Core Migrations (4)
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
- AvailabilityService
- ClosureValidatorsTests
- Persistence / Migrations (9)
- Persistence / Migrations (10)
- RoomCleaningRepository
- IntegrationTestBase
- Persistence / Migrations (13)
- frontend (1)
- IClosureRepository
- Root TS Config
- .Calendar
- BookingStatus
- Prettier Dependency
- Node Type Definitions
- React DOM Type Definitions
- TypeScript Dependency
- Prettier Config
- App Brand Identity
- BookingConfiguration
- frontend (3)
- src / assets (1)
- src / assets (2)
- .WithRoomsAsync
- eslint
- .CreateClient
- AdminPanelApiTests
- AdminPricingApiTests
- UserValidators.cs
- eslint
- SupervisorCountsAndRates
- .AddInfrastructure
- 20260719143540_CoreDomain.Designer.cs
- 20260721111400_ReplaceSessionsWithClosures.Designer.cs
- 20260806102552_SupervisorCountsAndRates.Designer.cs
- ScheduleSettings
- GroupRooms.tsx
- .NextFreeSitting
- DependencyInjection.cs
- AdminUserConfiguration
- BookingMealTimeConfiguration
- BookingRoomAssignmentConfiguration
- PaymentConfiguration
- RoomConfiguration
- RoomTaskConfiguration
- ScheduleEntryConfiguration
- eslint

## God Nodes (most connected - your core abstractions)
1. `CampCenter.Domain.Entities` - 89 edges
2. `Booking` - 81 edges
3. `CampCenter.Application.Interfaces` - 56 edges
4. `CampCenter.Domain.Repositories` - 40 edges
5. `AdminBookingService` - 38 edges
6. `ScheduleService` - 36 edges
7. `useAuth()` - 35 edges
8. `IBookingRepository` - 32 edges
9. `CampCenter.Infrastructure.Persistence` - 30 edges
10. `AppDbContext` - 30 edges

## Surprising Connections (you probably didn't know these)
- `CampCenterApiFactory` --references--> `Program`  [EXTRACTED]
  tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs → src/CampCenter.Api/Program.cs
- `HousekeepingServiceTests` --references--> `HousekeepingService`  [EXTRACTED]
  tests/CampCenter.UnitTests/Services/HousekeepingServiceTests.cs → src/CampCenter.Application/Services/HousekeepingService.cs
- `ScheduleConflictTests` --references--> `ScheduleService`  [EXTRACTED]
  tests/CampCenter.UnitTests/Services/ScheduleConflictTests.cs → src/CampCenter.Application/Services/ScheduleService.cs
- `LoginRequestValidatorTests` --references--> `LoginRequestValidator`  [EXTRACTED]
  tests/CampCenter.UnitTests/Validators/LoginRequestValidatorTests.cs → src/CampCenter.Application/Validators/LoginRequestValidator.cs
- `HousekeepingServiceTests` --references--> `IBookingRepository`  [EXTRACTED]
  tests/CampCenter.UnitTests/Services/HousekeepingServiceTests.cs → src/CampCenter.Domain/Repositories/IBookingRepository.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Booking Lifecycle (availability → deposit → confirmation)** — readme_booking_flow, readme_p24_payments, claude_gist_double_booking_guard, claude_domain_model [INFERRED 0.85]
- **Project Conventions (task runners, build env, knowledge graph)** — claude_task_runner_rules, claude_build_environment, claude_knowledge_graph_workflow [EXTRACTED 1.00]
- **Production Stack (Caddy -> frontend/api -> PostgreSQL)** — docker_docker_compose_prod_caddy, docker_docker_compose_prod_api, docker_docker_compose_prod_postgres [EXTRACTED 1.00]
- **CI Validation Pipeline (backend + frontend)** — github_workflows_ci_workflow, github_workflows_ci_backend_job, github_workflows_ci_frontend_job [EXTRACTED 1.00]

## Communities (149 total, 46 thin omitted)

### Community 0 - "DTOs / Schedule (1)"
Cohesion: 0.08
Nodes (40): ScheduleController, CancellationToken, DateOnly, Guid, HttpDelete, HttpGet, HttpPost, HttpPut (+32 more)

### Community 1 - "Room Management"
Cohesion: 0.06
Nodes (44): RoomsController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+36 more)

### Community 2 - "Admin Bookings Controller & DTOs"
Cohesion: 0.07
Nodes (48): ControllerBase, BookingsController, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IActionResult (+40 more)

### Community 3 - "src / api (1)"
Cohesion: 0.09
Nodes (25): AdminUser, BookingMealTime, createUser(), deleteBookingMeals(), deleteUser(), getBookingMealTimes(), getUsers(), NeighbourSitting (+17 more)

### Community 4 - "CampCenter.UnitTests / Validators"
Cohesion: 0.13
Nodes (8): CampCenter.Application.Models, CampCenter.Infrastructure.Auth, CampCenter.Api.Controllers, CampCenter.Application.Common, CampCenter.Application.DTOs.Auth, LoginRequestDto, LoginResponseDto, LoginRequestValidator

### Community 5 - "Room Task Management (1)"
Cohesion: 0.08
Nodes (38): TasksController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+30 more)

### Community 6 - "CampCenter.Application / Services (1)"
Cohesion: 0.15
Nodes (8): AccessToken, JwtSettings, string, JwtTokenService, int, string, RefreshTokenSettings, string

### Community 7 - "Room Mix Calculator Tests"
Cohesion: 0.12
Nodes (13): PeopleCount, SplitMix, RoomMixCalculator, SplitMix, Capacity, Dictionary, IReadOnlyDictionary, List (+5 more)

### Community 8 - "Payment Gateway Integration Tests (1)"
Cohesion: 0.06
Nodes (35): Amount, OrderId, Registered, SessionId, GatewayNotification, GatewayRegisterRequest, GatewayRegisterResult, IPaymentGateway (+27 more)

### Community 9 - "Project & NuGet Config"
Cohesion: 0.05
Nodes (38): BCrypt.Net-Next (4.2.0), FluentValidation (12.1.1), FluentValidation.DependencyInjectionExtensions (12.1.1), MailKit (4.14.1), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9), Microsoft.AspNetCore.Mvc.Testing (10.0.9), Microsoft.AspNetCore.OpenApi (10.0.9), Microsoft.Extensions.Http (10.0.0) (+30 more)

### Community 10 - "Frontend Icon Components"
Cohesion: 0.06
Nodes (31): Tile, TILES, IconArrowRight(), IconBed(), IconCalendar(), IconCheckSquare(), IconClipboard(), IconGrid() (+23 more)

### Community 11 - "CampCenter.Infrastructure / Repositories (1)"
Cohesion: 0.14
Nodes (21): ScheduleEntry, ScheduleEntryKind, DateOnly, DateTime, Guid, TimeOnly, ScheduleEntryRepository, BookingId (+13 more)

### Community 12 - "CampCenter.Application / Services (2)"
Cohesion: 0.19
Nodes (11): Skipped, BookingMealTimeDto, ScheduleEntryDto, ScheduleService, CancellationToken, DateOnly, Guid, List (+3 more)

### Community 13 - "Public Booking Frontend (1)"
Cohesion: 0.07
Nodes (33): Availability, AvailabilityCalendar, AvailabilityDay, BookingDetails, BookingPayment, cancelBooking(), createBooking(), CreateBookingInput (+25 more)

### Community 14 - "CampCenter.UnitTests / Services (1)"
Cohesion: 0.17
Nodes (11): Slot, MealSlot, Date, DateOnly, IEnumerable, IReadOnlyList, List, MealGenerationPlannerTests (+3 more)

### Community 15 - "Admin Booking & Notifications (1)"
Cohesion: 0.23
Nodes (11): AdminBookingDto, AdminBookingService, CancellationToken, DateOnly, DateTime, Guid, ILogger, int (+3 more)

### Community 16 - "Room Closure Management"
Cohesion: 0.06
Nodes (54): AdminAssignment, ApplyBookingMealTimeResult, BookingGroupPage, BookingPaymentState, BookingScheduleDay, Closure, ClosureInput, CreateAdminBookingInput (+46 more)

### Community 17 - "tests / CampCenter.IntegrationTests (1)"
Cohesion: 0.20
Nodes (12): ScheduleApiTests, BookingId, DateOnly, End, Fact, Guid, HttpClient, int (+4 more)

### Community 18 - "Domain & Infra Namespaces"
Cohesion: 0.14
Nodes (22): ScheduleDay, ScheduleEntry, ScheduleEntryInput, ScheduleEntryKind, buildChips(), Chip, ClashReason, DayTimetable() (+14 more)

### Community 19 - "src / utils"
Cohesion: 0.17
Nodes (27): getAvailabilityCalendar(), CalendarTile(), Props, DayCalendar(), Props, groupHue(), LaneEvent, packLanes() (+19 more)

### Community 20 - "Integration Test Harness (1)"
Cohesion: 0.15
Nodes (14): HousekeepingJob, HousekeepingPlanner, DateOnly, IEnumerable, List, Booking, DateOnly, DateTime (+6 more)

### Community 21 - "CampCenter.Application / Services (3)"
Cohesion: 0.17
Nodes (15): AdminUserDto, CreateUserRequestDto, SetUserPasswordRequestDto, SetUserRoleRequestDto, IUserService, CancellationToken, Guid, List (+7 more)

### Community 22 - "CampCenter.Domain / Repositories (1)"
Cohesion: 0.18
Nodes (12): BookingRepository, CancellationToken, DateOnly, DateTime, Dictionary, Exception, Guid, IReadOnlyCollection (+4 more)

### Community 23 - "Camp Session Management"
Cohesion: 0.06
Nodes (25): AbstractValidator, CampCenter.Application.Validators, CampCenter.UnitTests.Validators, InlineData, CreateScheduleEntryRequestDto, CreateBookingRequestValidator, Dictionary, CreateMealTimeDefaultRequestValidator (+17 more)

### Community 24 - "Integration Test Harness (2)"
Cohesion: 0.20
Nodes (11): IBookingRepository, CancellationToken, DateOnly, DateTime, Dictionary, Guid, IReadOnlyCollection, Items (+3 more)

### Community 25 - "Frontend Auth & API Client"
Cohesion: 0.17
Nodes (22): login(), logout(), api, refreshAccessToken(), RetriableConfig, decodeJWT(), getUserIdFromToken(), getUserLoginFromToken() (+14 more)

### Community 26 - "ControllerBase"
Cohesion: 0.29
Nodes (11): UsersController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+3 more)

### Community 27 - "Booking Persistence & Entities (1)"
Cohesion: 0.14
Nodes (14): AdminUser, DateTime, Guid, AdminUserRole, IAdminUserRepository, CancellationToken, Guid, List (+6 more)

### Community 28 - "Application Namespaces & DTOs"
Cohesion: 0.16
Nodes (7): CampCenter.Application.DTOs.Users, CampCenter.Api.Controllers.Admin, CampCenter.Application.DTOs.AdminPanel, CampCenter.Application.Interfaces, CampCenter.Application.DTOs.Schedule, CampCenter.Api.Extensions, RoomDeleteResultDto

### Community 29 - "Admin Booking & Notifications (2)"
Cohesion: 0.22
Nodes (13): PublicBookingsController, CancellationToken, EnableRateLimiting, HttpGet, HttpPost, IActionResult, IValidator, ProducesResponseType (+5 more)

### Community 30 - "Public Booking Service (1)"
Cohesion: 0.20
Nodes (10): CreateBookingRequestDto, CreateBookingResponseDto, BookingService, CancellationToken, DateOnly, Dictionary, ILogger, IReadOnlyDictionary (+2 more)

### Community 31 - "Public Booking Service (2)"
Cohesion: 0.13
Nodes (8): CampCenter.Infrastructure.Repositories, CampCenter.Infrastructure, CampCenter.Infrastructure.Persistence, CampCenter.Infrastructure.Persistence.Seed, IConfiguration, Program, DependencyInjection, IServiceCollection

### Community 32 - "Payment Gateway Integration Tests (2)"
Cohesion: 0.18
Nodes (10): IAsyncLifetime, IServiceProvider, IWebHostBuilder, PostgreSqlContainer, DataSeeder, CancellationToken, Task, CampCenterApiFactory (+2 more)

### Community 33 - "Docker & Project Docs"
Cohesion: 0.09
Nodes (26): Closure Model Replaces Camp Sessions, CampCenter Domain Model, GiST Exclusion Constraint Against Double Booking, Security Requirements, Dev Docker Compose Stack, campcenter-api (dev service), campcenter-frontend (dev service), Infra Docker Compose Stack (+18 more)

### Community 34 - "ClosureService"
Cohesion: 0.18
Nodes (6): Exception, BusinessRuleViolationException, ConcurrencyConflictException, ConflictException, ForbiddenActionException, NotFoundException

### Community 35 - "components / admin"
Cohesion: 0.21
Nodes (15): IScheduleEntryRepository, BookingId, CancellationToken, Count, Date, DateOnly, End, Guid (+7 more)

### Community 36 - "Validator Unit Tests"
Cohesion: 0.05
Nodes (53): CampCenter.Application.DTOs.Closures, ClosuresController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut (+45 more)

### Community 37 - "CampCenter.UnitTests / Services (2)"
Cohesion: 0.14
Nodes (20): AllowWorkerWrite, HousekeepingController, CancellationToken, DateOnly, Guid, HttpGet, HttpPut, IActionResult (+12 more)

### Community 38 - "TypeScript App Config"
Cohesion: 0.09
Nodes (22): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection, moduleResolution (+14 more)

### Community 39 - "Admin User & Token Config"
Cohesion: 0.31
Nodes (7): UserService, CancellationToken, Guid, List, Task, DateTime, Guid

### Community 40 - "TypeScript Node Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, moduleResolution, noEmit (+12 more)

### Community 41 - "Auth Service & Tokens"
Cohesion: 0.19
Nodes (11): ITokenService, AuthResult, RefreshTokenInfo, AuthService, CancellationToken, DateTime, Guid, Task (+3 more)

### Community 42 - "Root Task-Runner Scripts"
Cohesion: 0.10
Nodes (19): description, name, private, scripts, backend, build, dev, dev:down (+11 more)

### Community 43 - "CampCenter.UnitTests / Services (3)"
Cohesion: 0.08
Nodes (27): PricingController, CancellationToken, HttpGet, HttpPut, IActionResult, ProducesResponseType, Task, PricingDefaultsDto (+19 more)

### Community 44 - "CampCenter.UnitTests / Services (4)"
Cohesion: 0.42
Nodes (5): ScheduleConflictTests, DateOnly, Fact, Guid, Task

### Community 45 - "ScheduleEntry"
Cohesion: 0.14
Nodes (17): getHousekeepingDay(), getHousekeepingRange(), HousekeepingDay, HousekeepingRange, HousekeepingRoom, RoomCleaningStatus, roomCleaningStatuses, setRoomCleaning() (+9 more)

### Community 46 - "AvailabilityService"
Cohesion: 0.12
Nodes (16): Aktualizacja po każdej zmianie (obowiązkowe), Architektura, Build environment, Bun responsibilities, CampCenter, Cel projektu, Decision rule, Eksploracja przez vault (oszczędność tokenów) (+8 more)

### Community 48 - "Frontend App Shell & i18n"
Cohesion: 0.08
Nodes (38): bookingGroupCategories, BookingGroupCategory, BookingSchedule, BookingStatus, bookingStatuses, checkScheduleConflicts(), createScheduleEntry(), Dashboard (+30 more)

### Community 49 - "Auth Controller (1)"
Cohesion: 0.19
Nodes (13): CookieOptions, AuthController, CancellationToken, DateTime, EnableRateLimiting, HttpPost, IActionResult, IValidator (+5 more)

### Community 50 - ".Update"
Cohesion: 0.47
Nodes (5): Payment, PaymentKind, PaymentStatus, DateTime, Guid

### Community 51 - "Refresh Token Repository"
Cohesion: 0.24
Nodes (8): RefreshToken, DateTime, Guid, RefreshTokenRepository, CancellationToken, DateTime, Guid, Task

### Community 52 - "Exception"
Cohesion: 0.12
Nodes (6): CampCenter.Api.Background, CampCenter.Domain.Exceptions, CampCenter.Domain.Entities, CampCenter.Domain.Repositories, CampCenter.Application.Services, CampCenter.UnitTests.Services

### Community 53 - "useAuth"
Cohesion: 0.17
Nodes (19): AdminBooking, BookingState, bookingStates, createAdminBooking(), formatZl(), getAdminBookings(), getPricingDefaults(), groszeToZl() (+11 more)

### Community 54 - "API Launch Settings"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 55 - "Global Exception Handler"
Cohesion: 0.14
Nodes (12): CampCenter.Api.Errors, Detail, HttpContext, IExceptionHandler, IProblemDetailsService, GlobalExceptionHandler, CancellationToken, Exception (+4 more)

### Community 56 - "ESLint Dev Dependencies"
Cohesion: 0.13
Nodes (15): @eslint/js, eslint-plugin-react-hooks, eslint-plugin-react-refresh, devDependencies, @eslint/js, eslint-plugin-react-hooks, eslint-plugin-react-refresh, globals (+7 more)

### Community 57 - "Admin Booking & Notifications (4)"
Cohesion: 0.14
Nodes (13): EmailMessage, IEmailSender, CancellationToken, Task, BookingSettings, string, EmailTemplates, DateOnly (+5 more)

### Community 59 - "RoomNumberComparer"
Cohesion: 0.33
Nodes (4): GeneratedRegex, IComparer, Regex, RoomNumberComparer

### Community 60 - "Frontend Runtime Deps"
Cohesion: 0.15
Nodes (13): axios, dependencies, axios, i18next, react, react-dom, react-i18next, react-router-dom (+5 more)

### Community 61 - ".GetBlockedRoomIdsAsync"
Cohesion: 0.19
Nodes (13): AvailabilityCalendarDto, AvailabilityDayDto, AvailabilityDto, BookingDetailsDto, BookingPaymentDto, PublicClosureDto, IAvailabilityService, CancellationToken (+5 more)

### Community 64 - "src / api (2)"
Cohesion: 0.12
Nodes (22): MealTimesController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+14 more)

### Community 68 - "WriteRequiresAdministratorHandler"
Cohesion: 0.16
Nodes (11): Attribute, AuthorizationHandler, AuthorizationHandlerContext, CampCenter.Api.Auth, IAuthorizationRequirement, IHttpContextAccessor, AllowWorkerWriteAttribute, WriteRequiresAdministratorHandler (+3 more)

### Community 69 - "GroupRooms.tsx"
Cohesion: 0.13
Nodes (16): DbContext, DbSet, IDesignTimeDbContextFactory, BookingMealTime, DateTime, Guid, TimeOnly, AppDbContext (+8 more)

### Community 70 - "OpenAPI Security Scheme"
Cohesion: 0.20
Nodes (8): CampCenter.Api.OpenApi, IOpenApiDocumentTransformer, OpenApiDocument, OpenApiDocumentTransformerContext, BearerSecuritySchemeTransformer, CancellationToken, string, Task

### Community 73 - "Booking Maintenance Background Service"
Cohesion: 0.31
Nodes (7): BackgroundService, IServiceScopeFactory, BookingMaintenanceService, CancellationToken, ILogger, Task, TimeSpan

### Community 74 - "PasswordRules"
Cohesion: 0.25
Nodes (9): PublicBookingApiTests, Capacity, Count, DateOnly, Dictionary, Fact, HttpClient, long (+1 more)

### Community 75 - "SmtpEmailSender"
Cohesion: 0.29
Nodes (4): CampCenter.Infrastructure.Payments, RegisterData, RegisterData, RegisterResponse

### Community 76 - "CampCenter.Application.DTOs.Public"
Cohesion: 0.17
Nodes (7): CampCenter.IntegrationTests, CampCenter.Api.RateLimiting, CampCenter.Application.DTOs.Rooms, CampCenter.Api.Controllers.Public, CampCenter.Application.DTOs.Public, RateLimitPolicies, string

### Community 77 - "BookingSettings"
Cohesion: 0.19
Nodes (13): HousekeepingService, CancellationToken, DateOnly, Guid, List, Task, IRoomCleaningRepository, CancellationToken (+5 more)

### Community 78 - "eslint"
Cohesion: 0.33
Nodes (7): IReadOnlyList, IBookingMealTimeRepository, CancellationToken, Guid, IReadOnlyCollection, List, Task

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
Cohesion: 0.14
Nodes (8): CampCenter.Infrastructure.Persistence.Migrations, ModelSnapshot, MealSittingDuration, ModelBuilder, RoomCleanings, ModelBuilder, AppDbContextModelSnapshot, ModelBuilder

### Community 90 - "Persistence / Migrations (3)"
Cohesion: 0.40
Nodes (3): Migration, SuppressDeletedGeneratedMeals, MigrationBuilder

### Community 95 - ".CreateWorkerAsync"
Cohesion: 0.27
Nodes (8): Admin, Task, UsersAndRolesApiTests, Fact, HttpClient, string, Task, Worker

### Community 98 - "20260729224623_RoomCleanings.Designer.cs"
Cohesion: 0.32
Nodes (7): HousekeepingServiceTests, DateOnly, Fact, Guid, IEnumerable, Room, Task

### Community 99 - "AvailabilityService"
Cohesion: 0.22
Nodes (10): MealKind, MealTimeDefault, DateTime, Guid, TimeOnly, IMealTimeDefaultRepository, CancellationToken, Guid (+2 more)

### Community 100 - "ClosureValidatorsTests"
Cohesion: 0.25
Nodes (6): CampCenter.Infrastructure.Email, EmailSettings, string, SmtpEmailSender, CancellationToken, Task

### Community 103 - "RoomCleaningRepository"
Cohesion: 0.33
Nodes (7): MealTimeDefaultDto, MealTimeService, CancellationToken, Guid, List, Task, TimeOnly

### Community 104 - "IntegrationTestBase"
Cohesion: 0.60
Nodes (3): RoomsAndClosuresApiTests, Fact, Task

### Community 106 - "frontend (1)"
Cohesion: 0.67
Nodes (3): Lakeside Typography (Bricolage Grotesque + Inter), SPA HTML Shell, Pre-Paint Theme Restore

### Community 107 - "IClosureRepository"
Cohesion: 0.35
Nodes (5): MealTimeDefaultRepository, CancellationToken, Guid, List, Task

### Community 109 - ".Calendar"
Cohesion: 0.32
Nodes (7): RoomCleaningRepository, CancellationToken, DateOnly, Dictionary, Guid, List, Task

### Community 110 - "BookingStatus"
Cohesion: 0.33
Nodes (3): BookingRoomAssignment, DateOnly, Guid

### Community 117 - "BookingConfiguration"
Cohesion: 0.22
Nodes (5): IEntityTypeConfiguration, BookingConfiguration, EntityTypeBuilder, RoomConfiguration, EntityTypeBuilder

### Community 123 - ".WithRoomsAsync"
Cohesion: 0.49
Nodes (4): AdminSupervisorRoomsApiTests, Fact, HttpClient, Task

### Community 125 - ".CreateClient"
Cohesion: 0.44
Nodes (4): HttpClient, AuthApiTests, Fact, Task

### Community 126 - "AdminPanelApiTests"
Cohesion: 0.44
Nodes (4): AdminPanelApiTests, DateOnly, Fact, Task

### Community 127 - "AdminPricingApiTests"
Cohesion: 0.27
Nodes (7): ICollectionFixture, AdminPricingApiTests, Fact, Task, ApiCollection, IntegrationTestBase, string

### Community 128 - "UserValidators.cs"
Cohesion: 0.24
Nodes (8): RoomCleaning, RoomCleaningKind, RoomCleaningStatus, DateOnly, DateTime, Guid, RoomCleaningConfiguration, EntityTypeBuilder

### Community 129 - "eslint"
Cohesion: 0.22
Nodes (5): CampCenter.Infrastructure.Persistence.Configurations, AdminUserConfiguration, EntityTypeBuilder, MealTimeDefaultConfiguration, EntityTypeBuilder

### Community 131 - ".AddInfrastructure"
Cohesion: 0.27
Nodes (6): BookingCancelReason, BookingPaymentState, BookingStatus, BookingState, BookingStates, BookingStatuses

### Community 137 - "ScheduleSettings"
Cohesion: 0.50
Nodes (3): ScheduleSettings, string, TimeOnly

### Community 138 - "GroupRooms.tsx"
Cohesion: 0.32
Nodes (7): AssignableRoom, getAdminBooking(), getAssignableRooms(), reassignBooking(), Draft, GroupRooms(), Props

### Community 139 - ".NextFreeSitting"
Cohesion: 0.38
Nodes (5): MealGenerationPlanner, End, IReadOnlyCollection, Start, TimeOnly

### Community 140 - "DependencyInjection.cs"
Cohesion: 0.40
Nodes (3): CampCenter.Application, DependencyInjection, IServiceCollection

## Knowledge Gaps
- **249 isolated node(s):** `printWidth`, `name`, `version`, `private`, `type` (+244 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **46 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `CampCenter.Domain.Entities` connect `Exception` to `DTOs / Schedule (1)`, `Room Management`, `Admin Bookings Controller & DTOs`, `.AddInfrastructure`, `CampCenter.UnitTests / Validators`, `UserValidators.cs`, `Room Task Management (1)`, `eslint`, `CampCenter.Infrastructure / Repositories (1)`, `AdminUserConfiguration`, `BookingMealTimeConfiguration`, `BookingRoomAssignmentConfiguration`, `PaymentConfiguration`, `RoomTaskConfiguration`, `ScheduleEntryConfiguration`, `Integration Test Harness (1)`, `CampCenter.Application / Services (3)`, `Camp Session Management`, `Booking Persistence & Entities (1)`, `Application Namespaces & DTOs`, `Public Booking Service (2)`, `Validator Unit Tests`, `CampCenter.UnitTests / Services (3)`, `.Update`, `Refresh Token Repository`, `RoomNumberComparer`, `WriteRequiresAdministratorHandler`, `CampCenter.Application.DTOs.Public`, `eslint-plugin-react-hooks`, `AvailabilityService`, `BookingStatus`, `BookingConfiguration`?**
  _High betweenness centrality (0.092) - this node is a cross-community bridge._
- **Why does `CampCenter.Application.Interfaces` connect `Application Namespaces & DTOs` to `CampCenter.UnitTests / Validators`, `Validator Unit Tests`, `ClosureValidatorsTests`, `Payment Gateway Integration Tests (1)`, `SmtpEmailSender`, `DependencyInjection.cs`, `CampCenter.Application.DTOs.Public`, `ClosureService`, `Exception`, `Admin Booking & Notifications (4)`, `Public Booking Service (2)`?**
  _High betweenness centrality (0.076) - this node is a cross-community bridge._
- **Why does `CampCenter.Infrastructure.Persistence` connect `Public Booking Service (2)` to `UsersPage.tsx`, `20260728105506_PerGroupMealTimes.Designer.cs`, `20260719143540_CoreDomain.Designer.cs`, `Persistence / Migrations (9)`, `GroupRooms.tsx`, `20260721111400_ReplaceSessionsWithClosures.Designer.cs`, `EF Core Migrations (1)`, `Persistence / Migrations (10)`, `Persistence / Migrations (13)`, `20260802130913_MakeRoomTaskRoomOptional.Designer.cs`, `20260806102552_SupervisorCountsAndRates.Designer.cs`, `Exception`, `EF Core Migrations (3)`, `20260730211855_AdminUserRole.Designer.cs`?**
  _High betweenness centrality (0.069) - this node is a cross-community bridge._
- **What connects `printWidth`, `name`, `version` to the rest of the system?**
  _249 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DTOs / Schedule (1)` be split into smaller, more focused modules?**
  _Cohesion score 0.07552758237689744 - nodes in this community are weakly interconnected._
- **Should `Room Management` be split into smaller, more focused modules?**
  _Cohesion score 0.06052393857271906 - nodes in this community are weakly interconnected._
- **Should `Admin Bookings Controller & DTOs` be split into smaller, more focused modules?**
  _Cohesion score 0.06790123456790123 - nodes in this community are weakly interconnected._