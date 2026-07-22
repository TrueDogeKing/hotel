# Graph Report - .  (2026-07-21)

## Corpus Check
- Corpus is ~35,004 words - fits in a single context window. You may not need a graph.

## Summary
- 1331 nodes · 2696 edges · 71 communities (56 shown, 15 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 138 edges (avg confidence: 0.8)
- Token cost: 204,669 input · 0 output

## Community Hubs (Navigation)
- Room Management
- Booking Persistence & Entities
- Admin Booking & Notifications
- Room Task Management
- Payment Gateway Integration Tests
- Integration Test Harness
- Public Booking Service
- Camp Session Management
- Room Mix Calculator Tests
- Project & NuGet Config
- Admin Bookings Controller & DTOs
- Validator Unit Tests
- Room Closure Management
- Admin Frontend Pages
- Auth Controller
- Frontend Auth & API Client
- Docker & Project Docs
- EF Core Migrations
- Public Booking Frontend
- TypeScript App Config
- Frontend App Shell & i18n
- TypeScript Node Config
- Domain & Infra Namespaces
- Root Task-Runner Scripts
- Application Namespaces & DTOs
- Auth Service & Tokens
- API Launch Settings
- Rate Limiting & Startup
- ESLint Dev Dependencies
- Domain Exceptions
- Frontend Runtime Deps
- Global Exception Handler
- Auth DTOs & Models
- Admin Tasks & Occupancy Pages
- Admin User & Token Config
- Application DTO Namespaces
- OpenAPI Security Scheme
- Booking Maintenance Background Service
- Przelewy24 Payment Client
- JWT Token Service
- Refresh Token Repository
- Frontend Theme Toggle
- Refresh Token EF Config
- Refresh Token Contract
- Frontend Build Scripts
- Social Icon Sprite
- Password Hashing (bcrypt)
- Admin User Repository Contract
- Admin User Repository
- Claims Principal Extensions
- Application DI Registration
- Frontend Package Manifest
- Login Normalizer
- Frontend API Error Handling
- Select Component
- Infrastructure DI Registration
- Frontend Theme Bootstrap
- Root TS Config
- ESLint Package
- React Hooks ESLint Plugin
- Prettier Dependency
- Node Type Definitions
- React DOM Type Definitions
- TypeScript Dependency
- Prettier Config
- App Brand Identity
- React Logo Asset
- Vite Logo Asset

## God Nodes (most connected - your core abstractions)
1. `CampCenter.Domain.Entities` - 45 edges
2. `CampCenter.Application.Interfaces` - 41 edges
3. `Booking` - 40 edges
4. `Room` - 24 edges
5. `IBookingRepository` - 24 edges
6. `CampCenter.Domain.Repositories` - 22 edges
7. `BookingRepository` - 22 edges
8. `BookingService` - 20 edges
9. `Closure` - 20 edges
10. `AppDbContext` - 20 edges

## Surprising Connections (you probably didn't know these)
- `CampCenterApiFactory` --references--> `Program`  [EXTRACTED]
  tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs → src/CampCenter.Api/Program.cs
- `CI Backend Job (build + tests)` --references--> `Clean Architecture (Domain/Application/Infrastructure/Api)`  [INFERRED]
  .github/workflows/ci.yml → CLAUDE.md
- `CI Backend Job (build + tests)` --conceptually_related_to--> `campcenter-db (PostgreSQL 16-alpine)`  [INFERRED]
  .github/workflows/ci.yml → docker/docker-compose.infra.yml
- `CI Frontend Job (lint + typecheck + build)` --references--> `Bun (application task runner / frontend package manager)`  [INFERRED]
  .github/workflows/ci.yml → CLAUDE.md
- `campcenter-caddy (reverse proxy / TLS)` --conceptually_related_to--> `CampCenter Project Instructions`  [INFERRED]
  docker/docker-compose.prod.yml → CLAUDE.md

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Production Stack (Caddy -> frontend/api -> PostgreSQL)** — docker_docker_compose_prod_caddy, docker_docker_compose_prod_api, docker_docker_compose_prod_postgres [EXTRACTED 1.00]
- **CI Validation Pipeline (backend + frontend)** — github_workflows_ci_workflow, github_workflows_ci_backend_job, github_workflows_ci_frontend_job [EXTRACTED 1.00]
- **Dev Environment Bootstrap (Mise infra + Bun app + Docker)** — readme_dev_quickstart, claudemd_mise, claudemd_bun, docker_docker_compose_infra [INFERRED 0.85]

## Communities (71 total, 15 thin omitted)

### Community 0 - "Room Management"
Cohesion: 0.06
Nodes (40): RoomsController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+32 more)

### Community 1 - "Booking Persistence & Entities"
Cohesion: 0.05
Nodes (40): CampCenter.Infrastructure.Persistence.Configurations, DbContext, DbSet, IDesignTimeDbContextFactory, IEntityTypeConfiguration, Booking, BookingCancelReason, BookingStatus (+32 more)

### Community 2 - "Admin Booking & Notifications"
Cohesion: 0.07
Nodes (36): CampCenter.Infrastructure.Email, AdminBookingDto, EmailMessage, IEmailSender, CancellationToken, Task, BookingSettings, string (+28 more)

### Community 3 - "Room Task Management"
Cohesion: 0.08
Nodes (36): TasksController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, IActionResult, ProducesResponseType (+28 more)

### Community 4 - "Payment Gateway Integration Tests"
Cohesion: 0.06
Nodes (34): Amount, OrderId, Registered, SessionId, CancellationToken, HttpPost, IActionResult, ProducesResponseType (+26 more)

### Community 5 - "Integration Test Harness"
Cohesion: 0.06
Nodes (34): Count, IAsyncLifetime, ICollectionFixture, IServiceProvider, IWebHostBuilder, PostgreSqlContainer, DataSeeder, CancellationToken (+26 more)

### Community 6 - "Public Booking Service"
Cohesion: 0.09
Nodes (29): PublicBookingsController, CancellationToken, EnableRateLimiting, HttpGet, HttpPost, IActionResult, IValidator, ProducesResponseType (+21 more)

### Community 7 - "Camp Session Management"
Cohesion: 0.12
Nodes (25): SessionsController, CancellationToken, Guid, HttpDelete, HttpGet, HttpPost, HttpPut, IActionResult (+17 more)

### Community 8 - "Room Mix Calculator Tests"
Cohesion: 0.07
Nodes (27): IReadOnlyDictionary, PeopleCount, CancellationToken, HttpGet, IActionResult, ProducesResponseType, Task, PublicSessionDto (+19 more)

### Community 9 - "Project & NuGet Config"
Cohesion: 0.05
Nodes (38): BCrypt.Net-Next (4.2.0), FluentValidation (12.1.1), FluentValidation.DependencyInjectionExtensions (12.1.1), MailKit (4.14.1), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.9), Microsoft.AspNetCore.Mvc.Testing (10.0.9), Microsoft.AspNetCore.OpenApi (10.0.9), Microsoft.Extensions.Http (10.0.0) (+30 more)

### Community 10 - "Admin Bookings Controller & DTOs"
Cohesion: 0.10
Nodes (29): ControllerBase, BookingsController, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IActionResult (+21 more)

### Community 11 - "Validator Unit Tests"
Cohesion: 0.07
Nodes (24): AbstractValidator, CampCenter.Application.Validators, CampCenter.UnitTests.Validators, Func, CampSessionRules, CreateCampSessionRequestValidator, UpdateCampSessionRequestValidator, DateOnly (+16 more)

### Community 12 - "Room Closure Management"
Cohesion: 0.12
Nodes (18): Closure, DateOnly, DateTime, Guid, IClosureRepository, CancellationToken, DateOnly, Guid (+10 more)

### Community 13 - "Admin Frontend Pages"
Cohesion: 0.11
Nodes (29): AdminAssignment, AdminBooking, archiveSession(), CampSession, CampSessionInput, CampSessionStatus, cancelAdminBooking(), createRoom() (+21 more)

### Community 14 - "Auth Controller"
Cohesion: 0.14
Nodes (16): CookieOptions, AuthController, CancellationToken, DateTime, EnableRateLimiting, HttpPost, IActionResult, IValidator (+8 more)

### Community 15 - "Frontend Auth & API Client"
Cohesion: 0.18
Nodes (21): login(), logout(), api, refreshAccessToken(), RetriableConfig, decodeJWT(), getUserIdFromToken(), getUserLoginFromToken() (+13 more)

### Community 16 - "Docker & Project Docs"
Cohesion: 0.12
Nodes (25): Bun (application task runner / frontend package manager), CampCenter Project Instructions, Clean Architecture (Domain/Application/Infrastructure/Api), CampCenter Domain Model, JWT + Refresh Token Auth (admin only), Mise (infrastructure task runner), Przelewy24 Webhook Security (SHA-384 verify), Task Runner Rules (Mise vs Bun) (+17 more)

### Community 17 - "EF Core Migrations"
Cohesion: 0.09
Nodes (13): CampCenter.Infrastructure.Persistence.Migrations, Migration, ModelSnapshot, InitialAuth, MigrationBuilder, InitialAuth, ModelBuilder, CoreDomain (+5 more)

### Community 18 - "Public Booking Frontend"
Cohesion: 0.13
Nodes (19): formatZl(), BookingDetails, BookingPayment, cancelBooking(), createBooking(), CreateBookingInput, CreateBookingResult, getBooking() (+11 more)

### Community 19 - "TypeScript App Config"
Cohesion: 0.09
Nodes (22): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection, moduleResolution (+14 more)

### Community 20 - "Frontend App Shell & i18n"
Cohesion: 0.18
Nodes (12): Dashboard, getDashboard(), App(), useAuth(), ProtectedRoute(), AdminLayout(), LanguageSwitcher(), getStoredLanguage() (+4 more)

### Community 21 - "TypeScript Node Config"
Cohesion: 0.10
Nodes (20): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, moduleResolution, noEmit (+12 more)

### Community 22 - "Domain & Infra Namespaces"
Cohesion: 0.23
Nodes (5): CampCenter.Api.Background, CampCenter.Infrastructure.Repositories, CampCenter.Domain.Entities, CampCenter.Domain.Repositories, CampCenter.Infrastructure.Persistence

### Community 23 - "Root Task-Runner Scripts"
Cohesion: 0.10
Nodes (19): description, name, private, scripts, backend, build, dev, dev:down (+11 more)

### Community 24 - "Application Namespaces & DTOs"
Cohesion: 0.18
Nodes (7): CampCenter.Api.Controllers.Admin, CampCenter.Api.Controllers.Public, CampCenter.Application.DTOs.AdminPanel, CampCenter.Application.Interfaces, RoomDeleteResultDto, PublicPaymentsController, PublicSessionsController

### Community 26 - "Auth Service & Tokens"
Cohesion: 0.26
Nodes (7): ITokenService, AuthResult, AuthService, CancellationToken, DateTime, Guid, Task

### Community 27 - "API Launch Settings"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 28 - "Rate Limiting & Startup"
Cohesion: 0.14
Nodes (8): CampCenter.Infrastructure.Auth, CampCenter.Api.RateLimiting, CampCenter.Api.Controllers, CampCenter.Infrastructure, CampCenter.Infrastructure.Persistence.Seed, Program, RateLimitPolicies, string

### Community 29 - "ESLint Dev Dependencies"
Cohesion: 0.13
Nodes (15): @eslint/js, eslint-plugin-react-refresh, devDependencies, @eslint/js, eslint-plugin-react-refresh, globals, @types/react, typescript-eslint (+7 more)

### Community 30 - "Domain Exceptions"
Cohesion: 0.19
Nodes (8): CampCenter.Domain.Exceptions, CampCenter.Api.Errors, Exception, BusinessRuleViolationException, ConcurrencyConflictException, ConflictException, ForbiddenActionException, NotFoundException

### Community 31 - "Frontend Runtime Deps"
Cohesion: 0.15
Nodes (13): axios, dependencies, axios, i18next, react, react-dom, react-i18next, react-router-dom (+5 more)

### Community 32 - "Global Exception Handler"
Cohesion: 0.17
Nodes (11): Detail, HttpContext, IExceptionHandler, IProblemDetailsService, GlobalExceptionHandler, CancellationToken, Exception, ILogger (+3 more)

### Community 33 - "Auth DTOs & Models"
Cohesion: 0.21
Nodes (4): CampCenter.Application.Models, CampCenter.Application.DTOs.Auth, CampCenter.Application.Services, LoginResponseDto

### Community 34 - "Admin Tasks & Occupancy Pages"
Cohesion: 0.26
Nodes (10): createTask(), deleteTask(), getOccupancy(), getTasks(), RoomOccupancy, RoomTask, SessionOccupancy, setTaskDone() (+2 more)

### Community 35 - "Admin User & Token Config"
Cohesion: 0.20
Nodes (6): AccessToken, AdminUser, DateTime, Guid, AdminUserConfiguration, EntityTypeBuilder

### Community 36 - "Application DTO Namespaces"
Cohesion: 0.38
Nodes (4): CampCenter.IntegrationTests, CampCenter.Application.DTOs.Sessions, CampCenter.Application.DTOs.Rooms, CampCenter.Application.DTOs.Public

### Community 37 - "OpenAPI Security Scheme"
Cohesion: 0.20
Nodes (8): CampCenter.Api.OpenApi, IOpenApiDocumentTransformer, OpenApiDocument, OpenApiDocumentTransformerContext, BearerSecuritySchemeTransformer, CancellationToken, string, Task

### Community 38 - "Booking Maintenance Background Service"
Cohesion: 0.31
Nodes (7): BackgroundService, IServiceScopeFactory, BookingMaintenanceService, CancellationToken, ILogger, Task, TimeSpan

### Community 39 - "Przelewy24 Payment Client"
Cohesion: 0.22
Nodes (5): CampCenter.Infrastructure.Payments, CampCenter.UnitTests.Services, RegisterData, RegisterData, RegisterResponse

### Community 40 - "JWT Token Service"
Cohesion: 0.25
Nodes (5): RefreshTokenInfo, JwtSettings, string, JwtTokenService, int

### Community 41 - "Refresh Token Repository"
Cohesion: 0.39
Nodes (5): RefreshTokenRepository, CancellationToken, DateTime, Guid, Task

### Community 42 - "Frontend Theme Toggle"
Cohesion: 0.32
Nodes (6): applyTheme(), getTheme(), Listener, listeners, Theme, toggleTheme()

### Community 43 - "Refresh Token EF Config"
Cohesion: 0.29
Nodes (5): RefreshToken, DateTime, Guid, RefreshTokenConfiguration, EntityTypeBuilder

### Community 44 - "Refresh Token Contract"
Cohesion: 0.39
Nodes (5): IRefreshTokenRepository, CancellationToken, DateTime, Guid, Task

### Community 45 - "Frontend Build Scripts"
Cohesion: 0.29
Nodes (7): scripts, build, dev, format, format:check, lint, preview

### Community 46 - "Social Icon Sprite"
Cohesion: 0.38
Nodes (7): Bluesky Icon, Discord Icon, Documentation Icon, GitHub Icon, Social Icon, Icon Sprite Sheet, X (Twitter) Icon

### Community 48 - "Admin User Repository Contract"
Cohesion: 0.48
Nodes (4): IAdminUserRepository, CancellationToken, Guid, Task

### Community 49 - "Admin User Repository"
Cohesion: 0.48
Nodes (4): AdminUserRepository, CancellationToken, Guid, Task

### Community 50 - "Claims Principal Extensions"
Cohesion: 0.33
Nodes (4): ClaimsPrincipal, CampCenter.Api.Extensions, ClaimsPrincipalExtensions, Guid

### Community 51 - "Application DI Registration"
Cohesion: 0.40
Nodes (3): CampCenter.Application, DependencyInjection, IServiceCollection

### Community 52 - "Frontend Package Manifest"
Cohesion: 0.40
Nodes (4): name, private, type, version

### Community 56 - "Infrastructure DI Registration"
Cohesion: 0.50
Nodes (3): IConfiguration, DependencyInjection, IServiceCollection

### Community 57 - "Frontend Theme Bootstrap"
Cohesion: 0.67
Nodes (3): Frontend index.html (SPA entry), Pre-paint Theme Bootstrap (localStorage), Frontend README (React + TS + Vite template)

## Knowledge Gaps
- **177 isolated node(s):** `printWidth`, `name`, `version`, `private`, `type` (+172 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **15 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `CampCenter.Application.Interfaces` connect `Application Namespaces & DTOs` to `Room Management`, `Auth DTOs & Models`, `Admin Booking & Notifications`, `Payment Gateway Integration Tests`, `Application DTO Namespaces`, `Public Booking Service`, `Camp Session Management`, `Room Mix Calculator Tests`, `Przelewy24 Payment Client`, `Password Hashing (bcrypt)`, `Application DI Registration`, `Domain & Infra Namespaces`, `Rate Limiting & Startup`?**
  _High betweenness centrality (0.108) - this node is a cross-community bridge._
- **Why does `CampCenter.Domain.Entities` connect `Domain & Infra Namespaces` to `Room Management`, `Auth DTOs & Models`, `Booking Persistence & Entities`, `Admin User & Token Config`, `Application DTO Namespaces`, `Room Task Management`, `Refresh Token EF Config`, `Room Closure Management`, `Application Namespaces & DTOs`, `Rate Limiting & Startup`?**
  _High betweenness centrality (0.069) - this node is a cross-community bridge._
- **Why does `AppDbContext` connect `Booking Persistence & Entities` to `Room Management`, `Admin User & Token Config`, `Room Task Management`, `Refresh Token Repository`, `Refresh Token EF Config`, `Room Closure Management`, `Admin User Repository`, `Domain & Infra Namespaces`?**
  _High betweenness centrality (0.042) - this node is a cross-community bridge._
- **What connects `printWidth`, `name`, `version` to the rest of the system?**
  _177 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Room Management` be split into smaller, more focused modules?**
  _Cohesion score 0.062456140350877196 - nodes in this community are weakly interconnected._
- **Should `Booking Persistence & Entities` be split into smaller, more focused modules?**
  _Cohesion score 0.05405405405405406 - nodes in this community are weakly interconnected._
- **Should `Admin Booking & Notifications` be split into smaller, more focused modules?**
  _Cohesion score 0.06627175120325805 - nodes in this community are weakly interconnected._