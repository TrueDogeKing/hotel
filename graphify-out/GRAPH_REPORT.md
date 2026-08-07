# Graph Report - hotel  (2026-08-07)

## Corpus Check
- 298 files · ~115,686 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2587 nodes · 6339 edges · 140 communities (100 shown, 40 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 375 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `fd5d1223`
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
- MealTimeDefault
- ClosureValidatorsTests
- Persistence / Migrations (9)
- Persistence / Migrations (10)
- RoomCleaningRepository
- CampCenter.Domain.Exceptions
- Persistence / Migrations (13)
- frontend (1)
- IClosureRepository
- Root TS Config
- IntegrationTestBase
- BookingStatus
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
- AppDbContext
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
- ScheduleSettings
- eslint

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
- `MealTimeValidatorsTests` --references--> `CreateMealTimeDefaultRequestValidator`  [EXTRACTED]
  tests/CampCenter.UnitTests/Validators/ScheduleValidatorsTests.cs → src/CampCenter.Application/Validators/MealTimeValidators.cs
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

## Communities (140 total, 40 thin omitted)

### Community 0 - "DTOs / Schedule (1)"
Cohesion: 0.09
Nodes (36): ScheduleController, CancellationToken, DateOnly, Guid, HttpDelete, HttpGet, HttpPost, HttpPut (+28 more)

### Community 1 - "Room Management"
Cohesion: 0.06
Nodes (46): RoomsController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+38 more)

### Community 2 - "Admin Bookings Controller & DTOs"
Cohesion: 0.22
Nodes (10): AdminBookingService, CancellationToken, DateOnly, DateTime, Guid, ILogger, int, List (+2 more)

### Community 3 - "src / api (1)"
Cohesion: 0.36
Nodes (10): BookingsController, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IActionResult, IValidator (+2 more)

### Community 4 - "CampCenter.UnitTests / Validators"
Cohesion: 0.25
Nodes (4): UpdateDietaryNotesRequestDto, ScheduleRules, UpdateDietaryNotesRequestValidator, string

### Community 5 - "Room Task Management (1)"
Cohesion: 0.08
Nodes (38): TasksController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+30 more)

### Community 6 - "CampCenter.Application / Services (1)"
Cohesion: 0.20
Nodes (6): AccessToken, JwtTokenService, int, string, RefreshTokenSettings, string

### Community 7 - "Room Mix Calculator Tests"
Cohesion: 0.12
Nodes (13): PeopleCount, SplitMix, RoomMixCalculator, SplitMix, Capacity, Dictionary, IReadOnlyDictionary, List (+5 more)

### Community 8 - "Payment Gateway Integration Tests (1)"
Cohesion: 0.06
Nodes (39): Amount, OrderId, Registered, SessionId, GatewayNotification, GatewayRegisterRequest, GatewayRegisterResult, IPaymentGateway (+31 more)

### Community 9 - "Project & NuGet Config"
Cohesion: 0.05
Nodes (38): BCrypt.Net-Next (4.2.0), FluentValidation (12.1.1), FluentValidation.DependencyInjectionExtensions (12.1.1), MailKit (4.14.1), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9), Microsoft.AspNetCore.Mvc.Testing (10.0.9), Microsoft.AspNetCore.OpenApi (10.0.9), Microsoft.Extensions.Http (10.0.0) (+30 more)

### Community 10 - "Frontend Icon Components"
Cohesion: 0.06
Nodes (29): Tile, TILES, IconArrowRight(), IconBed(), IconCalendar(), IconCheckSquare(), IconClipboard(), IconGrid() (+21 more)

### Community 11 - "CampCenter.Infrastructure / Repositories (1)"
Cohesion: 0.12
Nodes (21): DbContext, DbSet, IDesignTimeDbContextFactory, AppDbContext, ModelBuilder, DesignTimeDbContextFactory, ScheduleEntryRepository, BookingId (+13 more)

### Community 12 - "CampCenter.Application / Services (2)"
Cohesion: 0.19
Nodes (11): Skipped, BookingMealTimeDto, ScheduleEntryDto, ScheduleService, CancellationToken, DateOnly, Guid, List (+3 more)

### Community 13 - "Public Booking Frontend (1)"
Cohesion: 0.06
Nodes (39): Availability, AvailabilityCalendar, AvailabilityDay, BookingDetails, BookingPayment, cancelBooking(), createBooking(), CreateBookingInput (+31 more)

### Community 14 - "CampCenter.UnitTests / Services (1)"
Cohesion: 0.13
Nodes (16): Slot, MealGenerationPlanner, MealSlot, Date, DateOnly, End, IEnumerable, IReadOnlyCollection (+8 more)

### Community 15 - "Admin Booking & Notifications (1)"
Cohesion: 0.15
Nodes (21): ScheduleEntry, ScheduleEntryKind, DateOnly, DateTime, Guid, TimeOnly, IScheduleEntryRepository, BookingId (+13 more)

### Community 16 - "Room Closure Management"
Cohesion: 0.06
Nodes (53): AdminAssignment, ApplyBookingMealTimeResult, BookingGroupPage, BookingPaymentState, BookingScheduleDay, Closure, ClosureInput, CreateAdminBookingInput (+45 more)

### Community 17 - "tests / CampCenter.IntegrationTests (1)"
Cohesion: 0.20
Nodes (12): ScheduleApiTests, BookingId, DateOnly, End, Fact, Guid, HttpClient, int (+4 more)

### Community 18 - "Domain & Infra Namespaces"
Cohesion: 0.12
Nodes (25): ScheduleDay, ScheduleEntry, ScheduleEntryInput, ScheduleEntryKind, scheduleEntryKinds, buildChips(), Chip, ClashReason (+17 more)

### Community 19 - "src / utils"
Cohesion: 0.17
Nodes (27): getAvailabilityCalendar(), CalendarTile(), Props, DayCalendar(), Props, groupHue(), LaneEvent, packLanes() (+19 more)

### Community 20 - "Integration Test Harness (1)"
Cohesion: 0.10
Nodes (20): HousekeepingJob, HousekeepingPlanner, DateOnly, IEnumerable, List, Booking, BookingCancelReason, BookingPaymentState (+12 more)

### Community 21 - "CampCenter.Application / Services (3)"
Cohesion: 0.29
Nodes (11): UsersController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+3 more)

### Community 22 - "CampCenter.Domain / Repositories (1)"
Cohesion: 0.18
Nodes (12): BookingRepository, CancellationToken, DateOnly, DateTime, Dictionary, Exception, Guid, IReadOnlyCollection (+4 more)

### Community 23 - "Camp Session Management"
Cohesion: 0.06
Nodes (46): AbstractValidator, MealTimesController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut (+38 more)

### Community 24 - "Integration Test Harness (2)"
Cohesion: 0.20
Nodes (11): IBookingRepository, CancellationToken, DateOnly, DateTime, Dictionary, Guid, IReadOnlyCollection, Items (+3 more)

### Community 25 - "Frontend Auth & API Client"
Cohesion: 0.14
Nodes (24): login(), logout(), api, refreshAccessToken(), RetriableConfig, decodeJWT(), getUserIdFromToken(), getUserLoginFromToken() (+16 more)

### Community 26 - "ControllerBase"
Cohesion: 0.29
Nodes (8): AdminBookingDto, OccupancyDto, IAdminBookingService, CancellationToken, DateOnly, Guid, List, Task

### Community 27 - "Booking Persistence & Entities (1)"
Cohesion: 0.14
Nodes (14): AdminUser, DateTime, Guid, AdminUserRole, IAdminUserRepository, CancellationToken, Guid, List (+6 more)

### Community 28 - "Application Namespaces & DTOs"
Cohesion: 0.15
Nodes (7): CampCenter.Application.DTOs.Users, CampCenter.Api.Controllers.Admin, CampCenter.Application.DTOs.AdminPanel, CampCenter.Application.Interfaces, CampCenter.Application.DTOs.Schedule, CampCenter.Api.Extensions, RoomDeleteResultDto

### Community 29 - "Admin Booking & Notifications (2)"
Cohesion: 0.24
Nodes (8): DashboardController, CancellationToken, HttpGet, IActionResult, ProducesResponseType, Task, BookingGroupPageDto, BookingGroupCategory

### Community 30 - "Public Booking Service (1)"
Cohesion: 0.20
Nodes (10): CreateBookingRequestDto, CreateBookingResponseDto, BookingService, CancellationToken, DateOnly, Dictionary, ILogger, IReadOnlyDictionary (+2 more)

### Community 31 - "Public Booking Service (2)"
Cohesion: 0.13
Nodes (8): CampCenter.Infrastructure.Auth, CampCenter.Api.RateLimiting, CampCenter.Api.Controllers, RateLimitPolicies, string, BcryptPasswordHasher, JwtSettings, string

### Community 32 - "Payment Gateway Integration Tests (2)"
Cohesion: 0.18
Nodes (10): IAsyncLifetime, IServiceProvider, IWebHostBuilder, PostgreSqlContainer, DataSeeder, CancellationToken, Task, CampCenterApiFactory (+2 more)

### Community 33 - "Docker & Project Docs"
Cohesion: 0.09
Nodes (26): Closure Model Replaces Camp Sessions, CampCenter Domain Model, GiST Exclusion Constraint Against Double Booking, Security Requirements, Dev Docker Compose Stack, campcenter-api (dev service), campcenter-frontend (dev service), Infra Docker Compose Stack (+18 more)

### Community 34 - "ClosureService"
Cohesion: 0.14
Nodes (8): CampCenter.Application.Models, CampCenter.Api.Background, CampCenter.Domain.Exceptions, CampCenter.Application.Common, CampCenter.Domain.Entities, CampCenter.Domain.Repositories, CampCenter.Application.Services, CampCenter.UnitTests.Services

### Community 35 - "components / admin"
Cohesion: 0.22
Nodes (10): MealKind, MealTimeDefault, DateTime, Guid, TimeOnly, MealTimeDefaultRepository, CancellationToken, Guid (+2 more)

### Community 36 - "Validator Unit Tests"
Cohesion: 0.06
Nodes (45): ClosuresController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+37 more)

### Community 37 - "CampCenter.UnitTests / Services (2)"
Cohesion: 0.05
Nodes (55): AllowWorkerWrite, HousekeepingController, CancellationToken, DateOnly, Guid, HttpGet, HttpPut, IActionResult (+47 more)

### Community 38 - "TypeScript App Config"
Cohesion: 0.09
Nodes (22): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection, moduleResolution (+14 more)

### Community 39 - "Admin User & Token Config"
Cohesion: 0.30
Nodes (6): IPasswordHasher, UserService, CancellationToken, Guid, List, Task

### Community 40 - "TypeScript Node Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, moduleResolution, noEmit (+12 more)

### Community 41 - "Auth Service & Tokens"
Cohesion: 0.17
Nodes (13): ITokenService, AuthResult, RefreshTokenInfo, AuthService, CancellationToken, DateTime, Guid, Task (+5 more)

### Community 42 - "Root Task-Runner Scripts"
Cohesion: 0.10
Nodes (19): description, name, private, scripts, backend, build, dev, dev:down (+11 more)

### Community 43 - "CampCenter.UnitTests / Services (3)"
Cohesion: 0.07
Nodes (36): PricingController, CancellationToken, HttpGet, HttpPut, IActionResult, ProducesResponseType, Task, PublicAvailabilityController (+28 more)

### Community 44 - "CampCenter.UnitTests / Services (4)"
Cohesion: 0.42
Nodes (5): ScheduleConflictTests, DateOnly, Fact, Guid, Task

### Community 45 - "ScheduleEntry"
Cohesion: 0.14
Nodes (17): getHousekeepingDay(), getHousekeepingRange(), HousekeepingDay, HousekeepingRange, HousekeepingRoom, RoomCleaningStatus, roomCleaningStatuses, setRoomCleaning() (+9 more)

### Community 46 - "AvailabilityService"
Cohesion: 0.12
Nodes (16): Aktualizacja po każdej zmianie (obowiązkowe), Architektura, Build environment, Bun responsibilities, CampCenter, Cel projektu, Decision rule, Eksploracja przez vault (oszczędność tokenów) (+8 more)

### Community 47 - "ClosureService"
Cohesion: 0.17
Nodes (5): CreateScheduleEntryRequestDto, CreateScheduleEntryRequestValidator, MealTimeValidatorsTests, ScheduleEntryValidatorsTests, Fact

### Community 48 - "Frontend App Shell & i18n"
Cohesion: 0.09
Nodes (36): bookingGroupCategories, BookingGroupCategory, BookingSchedule, BookingStatus, bookingStatuses, checkScheduleConflicts(), createScheduleEntry(), Dashboard (+28 more)

### Community 49 - "Auth Controller (1)"
Cohesion: 0.06
Nodes (29): CookieOptions, CampCenter.Application.Validators, CampCenter.Application.DTOs.Auth, CampCenter.UnitTests.Validators, InlineData, AuthController, CancellationToken, DateTime (+21 more)

### Community 50 - ".Update"
Cohesion: 0.47
Nodes (5): Payment, PaymentKind, PaymentStatus, DateTime, Guid

### Community 51 - "Refresh Token Repository"
Cohesion: 0.18
Nodes (10): RefreshToken, DateTime, Guid, RefreshTokenConfiguration, EntityTypeBuilder, RefreshTokenRepository, CancellationToken, DateTime (+2 more)

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
Nodes (15): @eslint/js, eslint-plugin-react-refresh, devDependencies, @eslint/js, eslint-plugin-react-refresh, globals, prettier, typescript-eslint (+7 more)

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
Cohesion: 0.12
Nodes (15): AdminAssignmentDto, AssignableRoomDto, CreateAdminBookingRequestDto, DashboardBookingDto, DashboardDto, ReassignBookingRequestDto, ReassignmentEntryDto, RoomOccupancyDto (+7 more)

### Community 62 - "RoomCleaningRepository"
Cohesion: 0.22
Nodes (5): Migration, InitialAuth, MigrationBuilder, ScheduleAndMealTimes, MigrationBuilder

### Community 64 - "src / api (2)"
Cohesion: 0.26
Nodes (9): PublicBookingApiTests, Capacity, Count, DateOnly, Dictionary, Fact, HttpClient, long (+1 more)

### Community 68 - "WriteRequiresAdministratorHandler"
Cohesion: 0.16
Nodes (11): Attribute, AuthorizationHandler, AuthorizationHandlerContext, CampCenter.Api.Auth, IAuthorizationRequirement, IHttpContextAccessor, AllowWorkerWriteAttribute, WriteRequiresAdministratorHandler (+3 more)

### Community 69 - "GroupRooms.tsx"
Cohesion: 0.21
Nodes (10): BookingMealTime, DateTime, Guid, TimeOnly, BookingMealTimeRepository, CancellationToken, Guid, IReadOnlyCollection (+2 more)

### Community 70 - "OpenAPI Security Scheme"
Cohesion: 0.20
Nodes (8): CampCenter.Api.OpenApi, IOpenApiDocumentTransformer, OpenApiDocument, OpenApiDocumentTransformerContext, BearerSecuritySchemeTransformer, CancellationToken, string, Task

### Community 73 - "Booking Maintenance Background Service"
Cohesion: 0.31
Nodes (7): BackgroundService, IServiceScopeFactory, BookingMaintenanceService, CancellationToken, ILogger, Task, TimeSpan

### Community 74 - "PasswordRules"
Cohesion: 0.25
Nodes (9): Admin, HttpClient, Task, UsersAndRolesApiTests, Fact, HttpClient, string, Task (+1 more)

### Community 75 - "SmtpEmailSender"
Cohesion: 0.29
Nodes (4): CampCenter.Infrastructure.Payments, RegisterData, RegisterData, RegisterResponse

### Community 76 - "CampCenter.Application.DTOs.Public"
Cohesion: 0.16
Nodes (5): CampCenter.IntegrationTests, CampCenter.Application.DTOs.Rooms, CampCenter.Api.Controllers.Public, CampCenter.Application.DTOs.Public, CampCenter.Application.DTOs.Closures

### Community 77 - "BookingSettings"
Cohesion: 0.37
Nodes (9): PublicBookingsController, CancellationToken, EnableRateLimiting, HttpGet, HttpPost, IActionResult, IValidator, ProducesResponseType (+1 more)

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

### Community 87 - "EF Core Migrations (4)"
Cohesion: 0.30
Nodes (9): AdminUserDto, CreateUserRequestDto, SetUserPasswordRequestDto, SetUserRoleRequestDto, IUserService, CancellationToken, Guid, List (+1 more)

### Community 89 - "Persistence / Migrations (2)"
Cohesion: 0.17
Nodes (11): AvailabilityCalendarDto, AvailabilityDayDto, AvailabilityDto, BookingDetailsDto, BookingPaymentDto, PublicClosureDto, PublicPricingDto, PublicScheduleDto (+3 more)

### Community 99 - "MealTimeDefault"
Cohesion: 0.32
Nodes (7): AssignableRoom, getAdminBooking(), getAssignableRooms(), reassignBooking(), Draft, GroupRooms(), Props

### Community 100 - "ClosureValidatorsTests"
Cohesion: 0.36
Nodes (7): IAvailabilityService, CancellationToken, DateOnly, Dictionary, Guid, HashSet, Task

### Community 102 - "Persistence / Migrations (10)"
Cohesion: 0.09
Nodes (26): AdminUser, BookingMealTime, createUser(), deleteBookingMeals(), deleteUser(), getBookingMealTimes(), getUsers(), NeighbourSitting (+18 more)

### Community 104 - "CampCenter.Domain.Exceptions"
Cohesion: 0.33
Nodes (7): IReadOnlyList, IBookingMealTimeRepository, CancellationToken, Guid, IReadOnlyCollection, List, Task

### Community 106 - "frontend (1)"
Cohesion: 0.67
Nodes (3): Lakeside Typography (Bricolage Grotesque + Inter), SPA HTML Shell, Pre-Paint Theme Restore

### Community 109 - "IntegrationTestBase"
Cohesion: 0.57
Nodes (3): AdminPricingApiTests, Fact, Task

### Community 110 - "BookingStatus"
Cohesion: 0.33
Nodes (3): BookingRoomAssignment, DateOnly, Guid

### Community 111 - "Prettier Dependency"
Cohesion: 0.18
Nodes (6): Exception, BusinessRuleViolationException, ConcurrencyConflictException, ConflictException, ForbiddenActionException, NotFoundException

### Community 112 - ".WithRoomsAsync"
Cohesion: 0.49
Nodes (4): AdminSupervisorRoomsApiTests, Fact, HttpClient, Task

### Community 117 - "BookingConfiguration"
Cohesion: 0.06
Nodes (22): CampCenter.Infrastructure.Persistence.Configurations, IEntityTypeConfiguration, AdminUserConfiguration, EntityTypeBuilder, BookingConfiguration, EntityTypeBuilder, BookingMealTimeConfiguration, EntityTypeBuilder (+14 more)

### Community 123 - "AppDbContext"
Cohesion: 0.22
Nodes (8): ControllerBase, OccupancyController, CancellationToken, DateOnly, HttpGet, IActionResult, ProducesResponseType, Task

### Community 124 - "AdminPanelApiTests"
Cohesion: 0.44
Nodes (4): AdminPanelApiTests, DateOnly, Fact, Task

### Community 125 - "AuthApiTests"
Cohesion: 0.44
Nodes (3): AuthApiTests, Fact, Task

### Community 128 - ".NextFreeSitting"
Cohesion: 0.40
Nodes (3): CampCenter.Application, DependencyInjection, IServiceCollection

### Community 129 - "RoomsAndClosuresApiTests"
Cohesion: 0.27
Nodes (7): ICollectionFixture, ApiCollection, IntegrationTestBase, string, RoomsAndClosuresApiTests, Fact, Task

### Community 138 - "ScheduleSettings"
Cohesion: 0.50
Nodes (3): ScheduleSettings, string, TimeOnly

## Knowledge Gaps
- **255 isolated node(s):** `printWidth`, `name`, `version`, `private`, `type` (+250 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **40 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `CampCenter.Domain.Entities` connect `ClosureService` to `Room Management`, `CampCenter.UnitTests / Validators`, `Room Task Management (1)`, `CampCenter.Infrastructure / Repositories (1)`, `Admin Booking & Notifications (1)`, `Integration Test Harness (1)`, `Camp Session Management`, `Booking Persistence & Entities (1)`, `Application Namespaces & DTOs`, `Admin Booking & Notifications (2)`, `Public Booking Service (2)`, `components / admin`, `Validator Unit Tests`, `CampCenter.UnitTests / Services (2)`, `CampCenter.UnitTests / Services (3)`, `.Update`, `Refresh Token Repository`, `Exception`, `RoomNumberComparer`, `.GetBlockedRoomIdsAsync`, `WriteRequiresAdministratorHandler`, `GroupRooms.tsx`, `CampCenter.Application.DTOs.Public`, `BookingStatus`, `BookingConfiguration`?**
  _High betweenness centrality (0.109) - this node is a cross-community bridge._
- **Why does `CampCenter.Application.Interfaces` connect `Application Namespaces & DTOs` to `.NextFreeSitting`, `ClosureService`, `Admin User & Token Config`, `Payment Gateway Integration Tests (1)`, `SmtpEmailSender`, `CampCenter.Application.DTOs.Public`, `eslint`, `Auth Controller (1)`, `Exception`, `Admin Booking & Notifications (4)`, `Public Booking Service (2)`?**
  _High betweenness centrality (0.072) - this node is a cross-community bridge._
- **Why does `CampCenter.Infrastructure.Persistence` connect `Exception` to `UsersPage.tsx`, `20260728105506_PerGroupMealTimes.Designer.cs`, `20260719143540_CoreDomain.Designer.cs`, `Persistence / Migrations (9)`, `20260721111400_ReplaceSessionsWithClosures.Designer.cs`, `EF Core Migrations (1)`, `20260802130913_MakeRoomTaskRoomOptional.Designer.cs`, `Persistence / Migrations (13)`, `CampCenter.Infrastructure / Repositories (1)`, `IClosureRepository`, `EF Core Migrations (3)`, `GroupRooms.tsx`, `20260730211855_AdminUserRole.Designer.cs`?**
  _High betweenness centrality (0.064) - this node is a cross-community bridge._
- **What connects `printWidth`, `name`, `version` to the rest of the system?**
  _255 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DTOs / Schedule (1)` be split into smaller, more focused modules?**
  _Cohesion score 0.09326923076923077 - nodes in this community are weakly interconnected._
- **Should `Room Management` be split into smaller, more focused modules?**
  _Cohesion score 0.05742296918767507 - nodes in this community are weakly interconnected._
- **Should `Room Task Management (1)` be split into smaller, more focused modules?**
  _Cohesion score 0.0770735524256651 - nodes in this community are weakly interconnected._