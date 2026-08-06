# Graph Report - hotel  (2026-08-06)

## Corpus Check
- 283 files · ~103,259 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2477 nodes · 6023 edges · 119 communities (81 shown, 38 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 361 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `72b7c9bb`
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
- Frontend API Error Handling
- Select Component
- 20260729224623_RoomCleanings.Designer.cs
- Persistence / Migrations (9)
- Persistence / Migrations (10)
- Persistence / Migrations (13)
- frontend (1)
- Root TS Config
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
- eslint
- eslint

## God Nodes (most connected - your core abstractions)
1. `CampCenter.Domain.Entities` - 89 edges
2. `Booking` - 81 edges
3. `CampCenter.Application.Interfaces` - 56 edges
4. `CampCenter.Domain.Repositories` - 40 edges
5. `AdminBookingService` - 36 edges
6. `ScheduleService` - 36 edges
7. `useAuth()` - 33 edges
8. `IBookingRepository` - 32 edges
9. `AppDbContext` - 30 edges
10. `MealTimeDefault` - 29 edges

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

## Communities (119 total, 38 thin omitted)

### Community 0 - "DTOs / Schedule (1)"
Cohesion: 0.08
Nodes (38): ScheduleController, CancellationToken, DateOnly, Guid, HttpDelete, HttpGet, HttpPost, HttpPut (+30 more)

### Community 1 - "Room Management"
Cohesion: 0.06
Nodes (44): RoomsController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+36 more)

### Community 2 - "Admin Bookings Controller & DTOs"
Cohesion: 0.07
Nodes (47): ControllerBase, BookingsController, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IActionResult (+39 more)

### Community 3 - "src / api (1)"
Cohesion: 0.05
Nodes (59): AdminAssignment, ApplyBookingMealTimeResult, BookingGroupPage, BookingPaymentState, BookingScheduleDay, Closure, ClosureInput, CreateAdminBookingInput (+51 more)

### Community 4 - "CampCenter.UnitTests / Validators"
Cohesion: 0.06
Nodes (28): AbstractValidator, CampCenter.Application.Validators, CampCenter.UnitTests.Validators, InlineData, UpdateMealTimeDefaultRequestDto, UpdateDietaryNotesRequestDto, UpdateScheduleEntryRequestDto, UpdateClosureRequestValidator (+20 more)

### Community 5 - "Room Task Management (1)"
Cohesion: 0.08
Nodes (38): TasksController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+30 more)

### Community 6 - "CampCenter.Application / Services (1)"
Cohesion: 0.20
Nodes (6): AccessToken, JwtSettings, string, JwtTokenService, int, string

### Community 7 - "Room Mix Calculator Tests"
Cohesion: 0.18
Nodes (8): PeopleCount, RoomMixCalculator, Capacity, Dictionary, IReadOnlyDictionary, List, RoomMixCalculatorTests, Fact

### Community 8 - "Payment Gateway Integration Tests (1)"
Cohesion: 0.06
Nodes (39): Amount, OrderId, Registered, SessionId, GatewayNotification, GatewayRegisterRequest, GatewayRegisterResult, IPaymentGateway (+31 more)

### Community 9 - "Project & NuGet Config"
Cohesion: 0.05
Nodes (38): BCrypt.Net-Next (4.2.0), FluentValidation (12.1.1), FluentValidation.DependencyInjectionExtensions (12.1.1), MailKit (4.14.1), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9), Microsoft.AspNetCore.Mvc.Testing (10.0.9), Microsoft.AspNetCore.OpenApi (10.0.9), Microsoft.Extensions.Http (10.0.0) (+30 more)

### Community 10 - "Frontend Icon Components"
Cohesion: 0.06
Nodes (31): Tile, TILES, IconArrowRight(), IconBed(), IconCalendar(), IconCheckSquare(), IconClipboard(), IconGrid() (+23 more)

### Community 11 - "CampCenter.Infrastructure / Repositories (1)"
Cohesion: 0.12
Nodes (21): DbContext, DbSet, IDesignTimeDbContextFactory, AppDbContext, ModelBuilder, DesignTimeDbContextFactory, ScheduleEntryRepository, BookingId (+13 more)

### Community 12 - "CampCenter.Application / Services (2)"
Cohesion: 0.06
Nodes (52): Skipped, BookingMealTimeDto, ScheduleEntryDto, ScheduleSettings, string, TimeOnly, ScheduleService, CancellationToken (+44 more)

### Community 13 - "Public Booking Frontend (1)"
Cohesion: 0.07
Nodes (38): BookingState, bookingStates, formatZl(), getAdminBookings(), setBookingState(), updateBookingPricing(), Availability, AvailabilityCalendar (+30 more)

### Community 14 - "CampCenter.UnitTests / Services (1)"
Cohesion: 0.06
Nodes (38): Slot, MealTimeDefaultDto, MealGenerationPlanner, MealSlot, Date, DateOnly, End, IEnumerable (+30 more)

### Community 15 - "Admin Booking & Notifications (1)"
Cohesion: 0.22
Nodes (12): AdminBookingDto, AdminBookingService, CancellationToken, DateOnly, DateTime, Guid, ILogger, int (+4 more)

### Community 16 - "Room Closure Management"
Cohesion: 0.25
Nodes (14): ScheduleDay, ScheduleEntry, buildChips(), Chip, ClashReason, DayTimetable(), findClashes(), formatTime() (+6 more)

### Community 17 - "tests / CampCenter.IntegrationTests (1)"
Cohesion: 0.05
Nodes (47): Admin, ICollectionFixture, AdminPanelApiTests, DateOnly, Fact, Task, AdminPricingApiTests, Fact (+39 more)

### Community 18 - "Domain & Infra Namespaces"
Cohesion: 0.09
Nodes (29): BookingMealTime, createMealTime(), deleteBookingMeals(), deleteMealTime(), getBookingMealTimes(), getMealTimes(), MealKind, mealKinds (+21 more)

### Community 19 - "src / utils"
Cohesion: 0.11
Nodes (33): getAvailabilityCalendar(), CalendarTile(), Props, DateField(), Props, DateRangeField(), Props, DayCalendar() (+25 more)

### Community 20 - "Integration Test Harness (1)"
Cohesion: 0.10
Nodes (20): HousekeepingJob, HousekeepingPlanner, DateOnly, IEnumerable, List, Booking, BookingCancelReason, BookingPaymentState (+12 more)

### Community 21 - "CampCenter.Application / Services (3)"
Cohesion: 0.28
Nodes (10): AdminUserDto, CreateUserRequestDto, SetUserPasswordRequestDto, SetUserRoleRequestDto, IUserService, CancellationToken, Guid, List (+2 more)

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

### Community 26 - "ControllerBase"
Cohesion: 0.29
Nodes (11): UsersController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+3 more)

### Community 27 - "Booking Persistence & Entities (1)"
Cohesion: 0.14
Nodes (14): AdminUser, DateTime, Guid, AdminUserRole, IAdminUserRepository, CancellationToken, Guid, List (+6 more)

### Community 28 - "Application Namespaces & DTOs"
Cohesion: 0.14
Nodes (7): CampCenter.Application.DTOs.Users, CampCenter.Api.Controllers.Admin, CampCenter.Application.DTOs.AdminPanel, CampCenter.Application.Interfaces, CampCenter.Application.DTOs.Schedule, CampCenter.Api.Extensions, RoomDeleteResultDto

### Community 29 - "Admin Booking & Notifications (2)"
Cohesion: 0.22
Nodes (13): PublicBookingsController, CancellationToken, EnableRateLimiting, HttpGet, HttpPost, IActionResult, IValidator, ProducesResponseType (+5 more)

### Community 30 - "Public Booking Service (1)"
Cohesion: 0.24
Nodes (8): CreateBookingRequestDto, CreateBookingResponseDto, BookingService, CancellationToken, DateOnly, ILogger, List, Task

### Community 31 - "Public Booking Service (2)"
Cohesion: 0.11
Nodes (10): CampCenter.Infrastructure.Auth, CampCenter.Application, CampCenter.Api.Errors, CampCenter.Infrastructure, CampCenter.Infrastructure.Persistence.Seed, IConfiguration, DependencyInjection, IServiceCollection (+2 more)

### Community 32 - "Payment Gateway Integration Tests (2)"
Cohesion: 0.17
Nodes (11): IAsyncLifetime, IServiceProvider, IWebHostBuilder, PostgreSqlContainer, Program, DataSeeder, CancellationToken, Task (+3 more)

### Community 33 - "Docker & Project Docs"
Cohesion: 0.09
Nodes (26): Closure Model Replaces Camp Sessions, CampCenter Domain Model, GiST Exclusion Constraint Against Double Booking, Security Requirements, Dev Docker Compose Stack, campcenter-api (dev service), campcenter-frontend (dev service), Infra Docker Compose Stack (+18 more)

### Community 34 - "ClosureService"
Cohesion: 0.11
Nodes (11): CampCenter.Application.Models, CampCenter.Domain.Exceptions, CampCenter.Application.Common, CampCenter.Application.Services, CampCenter.UnitTests.Services, Exception, BusinessRuleViolationException, ConcurrencyConflictException (+3 more)

### Community 35 - "components / admin"
Cohesion: 0.33
Nodes (3): BookingRoomAssignment, DateOnly, Guid

### Community 36 - "Validator Unit Tests"
Cohesion: 0.05
Nodes (51): ClosuresController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+43 more)

### Community 37 - "CampCenter.UnitTests / Services (2)"
Cohesion: 0.05
Nodes (55): AllowWorkerWrite, HousekeepingController, CancellationToken, DateOnly, Guid, HttpGet, HttpPut, IActionResult (+47 more)

### Community 38 - "TypeScript App Config"
Cohesion: 0.09
Nodes (22): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection, moduleResolution (+14 more)

### Community 39 - "Admin User & Token Config"
Cohesion: 0.40
Nodes (5): UserService, CancellationToken, Guid, List, Task

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
Cohesion: 0.09
Nodes (25): PricingController, CancellationToken, HttpGet, HttpPut, IActionResult, ProducesResponseType, Task, PricingDefaultsDto (+17 more)

### Community 44 - "CampCenter.UnitTests / Services (4)"
Cohesion: 0.42
Nodes (5): ScheduleConflictTests, DateOnly, Fact, Guid, Task

### Community 45 - "ScheduleEntry"
Cohesion: 0.21
Nodes (12): AdminBooking, BookingGroupCategory, BookingStatus, bookingStatuses, createAdminBooking(), DashboardBooking, getBookingGroupPage(), AddGroupForm() (+4 more)

### Community 46 - "AvailabilityService"
Cohesion: 0.12
Nodes (16): Aktualizacja po każdej zmianie (obowiązkowe), Architektura, Build environment, Bun responsibilities, CampCenter, Cel projektu, Decision rule, Eksploracja przez vault (oszczędność tokenów) (+8 more)

### Community 48 - "Frontend App Shell & i18n"
Cohesion: 0.11
Nodes (29): bookingGroupCategories, BookingSchedule, checkScheduleConflicts(), createScheduleEntry(), Dashboard, deleteScheduleEntry(), getBookingSchedule(), getDashboard() (+21 more)

### Community 49 - "Auth Controller (1)"
Cohesion: 0.14
Nodes (16): CookieOptions, AuthController, CancellationToken, DateTime, EnableRateLimiting, HttpPost, IActionResult, IValidator (+8 more)

### Community 50 - ".Update"
Cohesion: 0.47
Nodes (5): Payment, PaymentKind, PaymentStatus, DateTime, Guid

### Community 51 - "Refresh Token Repository"
Cohesion: 0.18
Nodes (10): RefreshToken, DateTime, Guid, RefreshTokenConfiguration, EntityTypeBuilder, RefreshTokenRepository, CancellationToken, DateTime (+2 more)

### Community 52 - "Exception"
Cohesion: 0.17
Nodes (5): CampCenter.Api.Background, CampCenter.Infrastructure.Repositories, CampCenter.Domain.Entities, CampCenter.Domain.Repositories, CampCenter.Infrastructure.Persistence

### Community 53 - "useAuth"
Cohesion: 0.21
Nodes (11): AdminUser, createUser(), deleteUser(), getUsers(), setUserPassword(), setUserRole(), userRoles, UserRole (+3 more)

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
Cohesion: 0.12
Nodes (13): CampCenter.Infrastructure.Email, EmailMessage, IEmailSender, CancellationToken, Task, EmailTemplates, DateOnly, DateTime (+5 more)

### Community 59 - "RoomNumberComparer"
Cohesion: 0.33
Nodes (4): GeneratedRegex, IComparer, Regex, RoomNumberComparer

### Community 60 - "Frontend Runtime Deps"
Cohesion: 0.15
Nodes (13): axios, dependencies, axios, i18next, react, react-dom, react-i18next, react-router-dom (+5 more)

### Community 61 - ".GetBlockedRoomIdsAsync"
Cohesion: 0.19
Nodes (13): AvailabilityCalendarDto, AvailabilityDayDto, AvailabilityDto, BookingDetailsDto, BookingPaymentDto, PublicClosureDto, IAvailabilityService, CancellationToken (+5 more)

### Community 68 - "WriteRequiresAdministratorHandler"
Cohesion: 0.16
Nodes (11): Attribute, AuthorizationHandler, AuthorizationHandlerContext, CampCenter.Api.Auth, IAuthorizationRequirement, IHttpContextAccessor, AllowWorkerWriteAttribute, WriteRequiresAdministratorHandler (+3 more)

### Community 69 - "GroupRooms.tsx"
Cohesion: 0.32
Nodes (7): AssignableRoom, getAdminBooking(), getAssignableRooms(), reassignBooking(), Draft, GroupRooms(), Props

### Community 70 - "OpenAPI Security Scheme"
Cohesion: 0.20
Nodes (8): CampCenter.Api.OpenApi, IOpenApiDocumentTransformer, OpenApiDocument, OpenApiDocumentTransformerContext, BearerSecuritySchemeTransformer, CancellationToken, string, Task

### Community 73 - "Booking Maintenance Background Service"
Cohesion: 0.31
Nodes (7): BackgroundService, IServiceScopeFactory, BookingMaintenanceService, CancellationToken, ILogger, Task, TimeSpan

### Community 74 - "PasswordRules"
Cohesion: 0.33
Nodes (4): PasswordRules, int, IRuleBuilder, IRuleBuilderOptions

### Community 75 - "SmtpEmailSender"
Cohesion: 0.29
Nodes (4): CampCenter.Infrastructure.Payments, RegisterData, RegisterData, RegisterResponse

### Community 76 - "CampCenter.Application.DTOs.Public"
Cohesion: 0.19
Nodes (5): CampCenter.IntegrationTests, CampCenter.Application.DTOs.Rooms, CampCenter.Api.Controllers.Public, CampCenter.Application.DTOs.Public, CampCenter.Application.DTOs.Closures

### Community 78 - "eslint"
Cohesion: 0.18
Nodes (6): CampCenter.Api.RateLimiting, CampCenter.Api.Controllers, CampCenter.Application.DTOs.Auth, RateLimitPolicies, string, LoginResponseDto

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
Nodes (8): CampCenter.Infrastructure.Persistence.Migrations, ModelSnapshot, CoreDomain, ModelBuilder, ReplaceSessionsWithClosures, ModelBuilder, AppDbContextModelSnapshot, ModelBuilder

### Community 88 - "Persistence / Migrations (1)"
Cohesion: 0.40
Nodes (3): Migration, ReplaceSessionsWithClosures, MigrationBuilder

### Community 106 - "frontend (1)"
Cohesion: 0.67
Nodes (3): Lakeside Typography (Bricolage Grotesque + Inter), SPA HTML Shell, Pre-Paint Theme Restore

### Community 117 - "BookingConfiguration"
Cohesion: 0.05
Nodes (24): CampCenter.Infrastructure.Persistence.Configurations, IEntityTypeConfiguration, AdminUserConfiguration, EntityTypeBuilder, BookingConfiguration, EntityTypeBuilder, BookingMealTimeConfiguration, EntityTypeBuilder (+16 more)

## Knowledge Gaps
- **245 isolated node(s):** `printWidth`, `name`, `version`, `private`, `type` (+240 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **38 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `CampCenter.Domain.Entities` connect `Exception` to `Room Management`, `Admin Bookings Controller & DTOs`, `CampCenter.UnitTests / Validators`, `Room Task Management (1)`, `CampCenter.Application / Services (2)`, `CampCenter.UnitTests / Services (1)`, `Integration Test Harness (1)`, `Booking Persistence & Entities (1)`, `Application Namespaces & DTOs`, `Public Booking Service (2)`, `ClosureService`, `components / admin`, `Validator Unit Tests`, `CampCenter.UnitTests / Services (2)`, `CampCenter.UnitTests / Services (3)`, `.Update`, `Refresh Token Repository`, `RoomNumberComparer`, `WriteRequiresAdministratorHandler`, `CampCenter.Application.DTOs.Public`, `BookingConfiguration`?**
  _High betweenness centrality (0.105) - this node is a cross-community bridge._
- **Why does `CampCenter.Application.Interfaces` connect `Application Namespaces & DTOs` to `ClosureService`, `Payment Gateway Integration Tests (1)`, `SmtpEmailSender`, `CampCenter.Application.DTOs.Public`, `eslint`, `ClosureService`, `Exception`, `Admin Booking & Notifications (4)`, `Public Booking Service (2)`?**
  _High betweenness centrality (0.091) - this node is a cross-community bridge._
- **Why does `CampCenter.Infrastructure.Persistence` connect `Exception` to `src / api (2)`, `UsersPage.tsx`, `20260729224623_RoomCleanings.Designer.cs`, `20260728105506_PerGroupMealTimes.Designer.cs`, `Persistence / Migrations (9)`, `Persistence / Migrations (10)`, `EF Core Migrations (1)`, `20260802130913_MakeRoomTaskRoomOptional.Designer.cs`, `Persistence / Migrations (13)`, `CampCenter.Infrastructure / Repositories (1)`, `EF Core Migrations (3)`, `20260730211855_AdminUserRole.Designer.cs`, `Public Booking Service (2)`?**
  _High betweenness centrality (0.075) - this node is a cross-community bridge._
- **What connects `printWidth`, `name`, `version` to the rest of the system?**
  _245 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DTOs / Schedule (1)` be split into smaller, more focused modules?**
  _Cohesion score 0.08439897698209718 - nodes in this community are weakly interconnected._
- **Should `Room Management` be split into smaller, more focused modules?**
  _Cohesion score 0.06052393857271906 - nodes in this community are weakly interconnected._
- **Should `Admin Bookings Controller & DTOs` be split into smaller, more focused modules?**
  _Cohesion score 0.06971975393028025 - nodes in this community are weakly interconnected._