# Graph Report - Trivo  (2026-10-02)

## Corpus Check
- 500 files · ~96,187 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 3511 nodes · 8055 edges · 203 communities (198 shown, 5 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 485 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `cbcf7f03`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- UserRepository
- IUserRepository
- Message
- InterestCategory
- Trivo.Application.Abstractions.Messages
- Trivo.Domain.Models
- Trivo.Application.Caching
- Trivo.Infrastructure.Persistence.Configurations
- AdminController.cs
- Trivo.API.Controllers.V1.Requests
- Trivo.Domain.Enums
- https
- UserController
- Migration
- Notification
- Expert
- Match
- .AddRepositories
- ChatDto
- Sanction
- BHD.ResultPattern — Guía de arquitectura e implementación
- IMatchNotifier
- .Handle
- Dependency Injection Patterns
- Error
- Recruiter
- .Handle
- PagedResult
- User
- CodeRepository
- .Handle
- .Handle
- AdminController
- .GetOrSetAsync
- Chat
- UserInterest
- UserSkill
- InterestRepository
- Entity Framework Core Patterns
- ExceptionHandlingMiddleware
- NotificationHub
- Requerimientos Funcionales
- .AddServices
- Trivo.Application.Utils
- Interest
- Trivo.API.csproj
- .SaveChangesAsync
- .Handle
- MatchHub
- Public API Design and Compatibility
- Skill
- IMatchRepository
- .GetByCategoriesAsync
- .Handle
- UpdateRecruiterCommandHandler.cs
- .ToDto
- .CreateSkillAsync
- AdministratorRepository
- .GetRolesAsync
- Trivo.Infrastructure.Shared.csproj
- IInterestRepository
- ResolveReportCommandHandler
- Database Performance Patterns
- Documento de Requerimientos de Software
- TrivoContext
- SkillRepository
- .ValidateEmailAsync
- Code
- Slopwatch: LLM Anti-Cheat for .NET
- IChatRepository
- Report and Sanction Management (RF9 - RF1 & RF2)
- .Handle
- .UpdateExpertAsync
- CacheEntryOptions
- GoogleGeminiEmbeddingService
- Módulo de Matchmaking con IA — Implementación
- Plan de implementación — Embeddings vía API externa para emparejamiento por afinidad
- ISkillRepository
- ResultT
- Nullable Attributes Reference
- MatchRepository
- Modern C# Coding Standards
- .UpdateRecruiterAsync
- IAdministratorRepository
- ControllerBase
- Plantillas copy-paste — feature CQRS de Trivo
- ICloudinaryService
- .Handle
- Polyfilling the nullable attributes for older target frameworks
- MatchDto
- IReportRepository
- SanctionDto
- .Handle
- Trivo.Application.csproj
- C# Nullable Reference Types
- IEmbeddingService
- NRT Migration Playbook Reference
- .Handle
- .Handle
- Anti-Patterns to Avoid
- IMatchHub
- Report
- AuthenticationService
- GetExpertsPagedQuery
- ResultFilter
- Anti-Patterns and Reflection Avoidance
- .GetUserId
- Avoid Reflection-Based Metaprogramming
- Trivo
- SkillWithIdDto
- Performance and API Design Patterns
- Value Objects and Pattern Matching
- RF9. Administrar aplicación — Documentación de implementación
- .Handle
- Sanction
- ReportDto
- User
- MessageDto
- Trivo.Infrastructure.Persistence.Migrations
- .GetExpertIdAsync
- .GetRecruiterIdAsync
- AddUnaccentExtension
- EmailSetting
- Trivo.Infrastructure.Persistence.csproj
- The `field` Keyword (C# 14 / .NET 10) and Nullability
- MessageStatus
- Trivo.Domain.csproj
- .Handle
- .ToEntity
- UserMappingExtensions.cs
- .CreateMatchAsync
- ErrorType
- NotificationType
- CustomUserIdProvider
- ExpertStatus
- Level
- MatchStatus
- MessageType
- RecruiterStatus
- UserStatus
- PaginationExtensions.cs
- ChatType
- MatchFault
- .RefreshTokenAsync
- ReportListItemDto
- .GetDetailsByUserIdsAsync
- Core Nullability Model
- Arquitectura CQRS de Trivo — mandato para nuevas features
- Composition and Error Handling
- Language Patterns
- AiSetting
- .Handle
- Known Static-Analysis Limitations and Safe Patterns
- Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`
- Fix #43 — `ReceiveChats` no devolvía `lastMessage`
- Preconditions: `AllowNull` and `DisallowNull`
- Performance Patterns
- CLAUDE.md
- AddPendingEmailToUser
- ReportMapper
- FilterUsersByInterestsAndSkillsRequest
- ExpertRepository
- CloudinarySetting
- UpdateNameCommand
- AddUniqueExpertRecruiterUserId
- .Validation
- .BuildModel
- SkillDto
- Administrator
- IQueryHandler
- .Handle
- AbstractValidator
- InterestWithIdDto
- .NotFound
- .ValidateAsync
- .Handle
- TokenResponseDto
- MatchDetailsDto
- .BuildTargetModel
- .BuildTargetModel
- GetRecruitersPagedQuery
- IChatHub
- GetActiveUsersCountQueryHandler
- ConfirmAccountCommand
- Buenas prácticas .NET para Trivo — checklist central
- .Handle
- IQuery
- GetLatestUsersPagedQueryHandler.cs
- Roles
- ResendConfirmationCodeCommandHandler.cs
- AddReportSanctions
- JwtSetting
- ReportDecision
- .BuildTargetModel
- ReportType
- IExpertRepository
- AccountAccessService
- dotnet-guard.sh
- UpdateExpertCommandHandler.cs
- .Handle

## God Nodes (most connected - your core abstractions)
1. `ResultT` - 166 edges
2. `Trivo.Application.Abstractions.Messages` - 136 edges
3. `PagedResult` - 102 edges
4. `Trivo.Application.Utils` - 102 edges
5. `Trivo.Domain.Models` - 100 edges
6. `Trivo.Application.Pagination` - 79 edges
7. `Trivo.Application.Interfaces.Services` - 75 edges
8. `IUserRepository` - 66 edges
9. `TrivoContext` - 64 edges
10. `Trivo.Application.Interfaces.Repository.Account` - 63 edges

## Surprising Connections (you probably didn't know these)
- `AuthController` --references--> `IAuthenticationService`  [EXTRACTED]
  src/API/Trivo.API/Controllers/V1/AuthController.cs → src/Application/Trivo.Application/Interfaces/Services/IAuthenticationService.cs
- `UserController` --references--> `ICodeService`  [EXTRACTED]
  src/API/Trivo.API/Controllers/V1/UserController.cs → src/Application/Trivo.Application/Interfaces/Services/ICodeService.cs
- `UserController` --references--> `IEmailValidationService`  [EXTRACTED]
  src/API/Trivo.API/Controllers/V1/UserController.cs → src/Application/Trivo.Application/Interfaces/Services/IEmailValidationService.cs
- `ICommand` --references--> `Result`  [EXTRACTED]
  src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs → src/Application/Trivo.Application/Utils/Result.cs
- `ICommand` --references--> `ResultT`  [EXTRACTED]
  src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs → src/Application/Trivo.Application/Utils/Result.cs

## Import Cycles
- None detected.

## Communities (203 total, 5 thin omitted)

### Community 0 - "UserRepository"
Cohesion: 0.18
Nodes (14): CancellationToken, Distance, Expert, Guid, IEnumerable, IReadOnlyList, Items, List (+6 more)

### Community 1 - "IUserRepository"
Cohesion: 0.19
Nodes (12): CancellationToken, Distance, Guid, IEnumerable, IReadOnlyList, Items, List, Task (+4 more)

### Community 2 - "Message"
Cohesion: 0.11
Nodes (27): CancellationToken, Guid, List, Task, IMessageRepository, DateTime, Guid, ICollection (+19 more)

### Community 3 - "InterestCategory"
Cohesion: 0.06
Nodes (39): Authorize, CancellationToken, HttpGet, HttpPost, ISender, ProducesResponseType, Task, InterestCategoryController (+31 more)

### Community 4 - "Trivo.Application.Abstractions.Messages"
Cohesion: 0.05
Nodes (31): Trivo.Application.Features.Skills.Query.GetSkillsPagination, Trivo.Application.Abstractions.Messages, Trivo.Application.Features.Users.Commands.UpdatePassword, Trivo.Application.Features.Users.Commands.CreateUser, Trivo.Application.Features.Users.Query.SearchUsers, Trivo.Application.Features.Interests.Commands.CreateInterest, Trivo.Application.Features.Users.Query.GetUserDetails, Trivo.Application.Features.Skills (+23 more)

### Community 5 - "Trivo.Domain.Models"
Cohesion: 0.10
Nodes (12): Trivo.Domain.Common, Trivo.Infrastructure.Persistence.Context, Trivo.Infrastructure.Persistence.Repository.Account, Trivo.Application.Interfaces.Repository.Base, Trivo.Infrastructure.Persistence.Repository, Trivo.Application.Features.Administrator.Query.GetRecruitersPaged, Trivo.Domain.Models, Trivo.Application.Interfaces.Repository (+4 more)

### Community 6 - "Trivo.Application.Caching"
Cohesion: 0.10
Nodes (11): Trivo.Application.DTOs.InterestCategories, Trivo.Application.Features.Users.Commands.UpdateProfilePicture, Trivo.Application.Caching, Trivo.Application.Features.Administrator.Commands.UnbanUser, Trivo.Application.Features.InterestCategories.Commands.CreateInterestCategory, Trivo.Application.Features.InterestCategories.Query.GetPaginatedInterestCategories, Trivo.Application.Features.InterestCategories, UnbanUserCommandValidator (+3 more)

### Community 7 - "Trivo.Infrastructure.Persistence.Configurations"
Cohesion: 0.05
Nodes (24): Trivo.Infrastructure.Persistence.Configurations, IEntityTypeConfiguration, EntityTypeBuilder, AdministratorConfig, EntityTypeBuilder, ChatConfig, EntityTypeBuilder, ChatUserConfig (+16 more)

### Community 8 - "AdminController.cs"
Cohesion: 0.06
Nodes (20): Trivo.Application.Features.Administrator.Query.GetCompletedMatchesCount, Trivo.Application.Features.Reports.Query.GetLatestReports, Trivo.Application.Features.Administrator.Query.GetExpertsPaged, Trivo.Application.Features.Reports.Query.GetReportsPaged, Trivo.Application.Features.Administrator.Query.GetLatestMatches, Trivo.Application.Features.Reports.Commands.CreateReport, Trivo.Application.Features.Administrator.Commands.CreateAdministrator, Trivo.Application.Features.Administrator.Query.GetReportedUsersCount (+12 more)

### Community 9 - "Trivo.API.Controllers.V1.Requests"
Cohesion: 0.07
Nodes (17): Trivo.API.Controllers.V1.Requests, ChangePasswordRequest, ConfirmEmailChangeRequest, RequestEmailChangeRequest, ResolveReportRequest, UpdateBiographyRequest, UpdateNameRequest, UpdatePasswordRequest (+9 more)

### Community 10 - "Trivo.Domain.Enums"
Cohesion: 0.06
Nodes (19): Trivo.Application.Features.Messages.Commands.SendImage, Trivo.Application.Features.Messages.Query.GetMessagePagination, Trivo.Application.Features.Chat.Query.GetChatPagination, Trivo.Application.Features.Chat, Trivo.Application.Features.Matching.Commands.UpdateMatch, Trivo.Application.Features.Matching.Commands.CreateMatch, Trivo.Application.Interfaces.SignalR, Trivo.Application.Features.Chat.Commands.CreateChat (+11 more)

### Community 11 - "https"
Cohesion: 0.10
Nodes (21): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, launchUrl, commandName (+13 more)

### Community 12 - "UserController"
Cohesion: 0.26
Nodes (10): Authorize, CancellationToken, Guid, HttpGet, HttpPost, IEnumerable, ISender, ProducesResponseType (+2 more)

### Community 13 - "Migration"
Cohesion: 0.33
Nodes (3): Migration, MigrationBuilder, AddProfileTextHash

### Community 14 - "Notification"
Cohesion: 0.08
Nodes (27): CancellationToken, Expression, Func, Guid, Task, IGenericRepository, CancellationToken, Guid (+19 more)

### Community 15 - "Expert"
Cohesion: 0.14
Nodes (12): Guid, ExpertMapper, Guid, ICollection, Expert, AvailableForProjects, IsHired, Matches (+4 more)

### Community 16 - "Match"
Cohesion: 0.08
Nodes (23): DateTime, Guid, BaseEntity, CreatedAt, Id, UpdatedAt, Guid, Match (+15 more)

### Community 17 - ".AddRepositories"
Cohesion: 0.24
Nodes (10): IGetExpertIdService, IGetRecruiterIdService, IUserRoleService, IConfiguration, IConnectionMultiplexer, IServiceCollection, DependencyInjection, GetExpertIdService (+2 more)

### Community 18 - "ChatDto"
Cohesion: 0.14
Nodes (19): DateTime, Guid, List, ChatDto, Guid, GetChatPaginationQuery, CancellationToken, ILogger (+11 more)

### Community 19 - "Sanction"
Cohesion: 0.29
Nodes (11): CancellationToken, DateTime, Guid, Task, ISanctionRepository, Sanction, CancellationToken, DateTime (+3 more)

### Community 20 - "BHD.ResultPattern — Guía de arquitectura e implementación"
Cohesion: 0.06
Nodes (31): 1. Arquitectura general, 2.1. Estructura de carpetas, 2.2. `BHD.ResultPattern.csproj`, 2.3. `Result.cs`, 2.4. `Error.cs`, 2. Núcleo: `BHD.ResultPattern` (sin dependencias), 3.1. Estructura de carpetas, 3.2. `BHD.ResultPattern.AspNetCore.csproj` (+23 more)

### Community 21 - "IMatchNotifier"
Cohesion: 0.25
Nodes (9): Guid, IEnumerable, Task, IMatchNotifier, Guid, IEnumerable, IHubContext, Task (+1 more)

### Community 22 - ".Handle"
Cohesion: 0.15
Nodes (12): Guid, GetMessagePaginationQuery, CancellationToken, ILogger, Task, GetMessagePaginationQueryHandler, Exception, Guid (+4 more)

### Community 23 - "Dependency Injection Patterns"
Cohesion: 0.05
Nodes (38): Advanced DI Patterns, Akka.DependencyInjection Reference, Akka.Hosting.TestKit, Akka.NET Actor Scope Management, Common Patterns, Conditional Registration, Contents, Factory-Based Registration (+30 more)

### Community 24 - "Error"
Cohesion: 0.12
Nodes (9): IReadOnlyDictionary, PaginationError, ErrorType, Error, Code, Description, ErrorType, Extensions (+1 more)

### Community 25 - "Recruiter"
Cohesion: 0.14
Nodes (18): Guid, RecruiterMapper, CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task (+10 more)

### Community 26 - ".Handle"
Cohesion: 0.11
Nodes (15): Guid, IEnumerable, CacheKeys, List, ExpertDetailsDto, List, RecruiterDetailsDto, List (+7 more)

### Community 27 - "PagedResult"
Cohesion: 0.09
Nodes (26): DateTime, Guid, UserDto, GetBannedUsersPagedQuery, CancellationToken, ILogger, Task, GetBannedUsersPagedQueryHandler (+18 more)

### Community 28 - "User"
Cohesion: 0.07
Nodes (30): ICollection, Vector, User, Biography, ChatUsers, Codes, Email, Experts (+22 more)

### Community 29 - "CodeRepository"
Cohesion: 0.22
Nodes (8): CancellationToken, Guid, Task, ICodeRepository, CancellationToken, Guid, Task, CodeRepository

### Community 30 - ".Handle"
Cohesion: 0.07
Nodes (27): EmailResponseDto, Guid, ConfirmEmailChangeCommand, CancellationToken, ILogger, Task, ConfirmEmailChangeCommandHandler, ForgotPasswordCommand (+19 more)

### Community 31 - ".Handle"
Cohesion: 0.15
Nodes (11): DateTime, Guid, AdminMatchDto, ExpertMatchDto, RecruiterMatchDto, AdminMatchMapper, GetLatestMatchesQuery, CancellationToken (+3 more)

### Community 32 - "AdminController"
Cohesion: 0.27
Nodes (12): Authorize, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IEnumerable, ISender (+4 more)

### Community 33 - ".GetOrSetAsync"
Cohesion: 0.11
Nodes (16): Guid, IEnumerable, GetInterestsByCategoryIdQuery, CancellationToken, ILogger, Task, GetInterestsByCategoryIdQueryHandler, GetInterestsByCategoryIdValidator (+8 more)

### Community 34 - "Chat"
Cohesion: 0.11
Nodes (23): ICollection, Chat, ChatType, ChatUsers, IsActive, Messages, DateTime, Guid (+15 more)

### Community 35 - "UserInterest"
Cohesion: 0.13
Nodes (18): CancellationToken, Guid, List, Task, IUserInterestRepository, Guid, UserInterest, Interest (+10 more)

### Community 36 - "UserSkill"
Cohesion: 0.11
Nodes (18): CancellationToken, Guid, List, Task, IUserSkillRepository, Guid, UserSkill, Skill (+10 more)

### Community 37 - "InterestRepository"
Cohesion: 0.28
Nodes (7): CancellationToken, Guid, IEnumerable, Interest, List, Task, InterestRepository

### Community 38 - "Entity Framework Core Patterns"
Cohesion: 0.05
Nodes (38): 1. Forgetting to Update When NoTracking, 2. N+1 Query Problem, 3. Tracking Conflicts with Multiple DbContext Instances, 4. Not Using Async Consistently, 5. Querying Inside Loops, Actors / Long-Lived Objects (Factory Pattern), AppHost Configuration, Applying Migrations (+30 more)

### Community 39 - "ExceptionHandlingMiddleware"
Cohesion: 0.08
Nodes (18): Trivo.API.Middlewares, Trivo.API.Extensions, IApplicationBuilder, IEndpointRouteBuilder, IHostEnvironment, ProblemDetails, RequestDelegate, IConfiguration (+10 more)

### Community 40 - "NotificationHub"
Cohesion: 0.16
Nodes (9): Guid, IEnumerable, Task, INotificationHub, Exception, Guid, ILogger, Task (+1 more)

### Community 41 - "Requerimientos Funcionales"
Cohesion: 0.12
Nodes (15): 4.3.1 Análisis Preliminar y Determinación de Requerimientos, 4.3.2 Análisis y Modelado de los Requerimientos del Proyecto, 4.3 Descripción del Modelo de Desarrollo, Requerimientos Funcionales, Requerimientos Funcionales (Sección Adicional), Requerimientos No Funcionales, RF1. Registro, RF2. Inicio de sesión (+7 more)

### Community 42 - ".AddServices"
Cohesion: 0.31
Nodes (5): JwtResponse, IConfiguration, IServiceCollection, IUserIdProvider, DependencyInjection

### Community 43 - "Trivo.Application.Utils"
Cohesion: 0.06
Nodes (22): Trivo.API.Controllers.V1, Trivo.Application.DTOs.Notifications, Trivo.Application.Interfaces.UnitOfWork, Trivo.Application.DTOs.Email, Trivo.Application.Features.Users.Commands.LoginUser, Trivo.Infrastructure.Shared.Services, Trivo.Infrastructure.Persistence.Services, Trivo.Domain.Configurations (+14 more)

### Community 44 - "Interest"
Cohesion: 0.11
Nodes (17): Guid, InterestByCategoryIdDto, Guid, InterestDto, Guid, IEnumerable, Interest, InterestMapper (+9 more)

### Community 45 - "Trivo.API.csproj"
Cohesion: 0.11
Nodes (17): Asp.Versioning.Mvc (8.1.0), AspNetCore.HealthChecks.Redis (9.0.0), Microsoft.AspNetCore.OpenApi (8.0.10), Microsoft.AspNetCore.SignalR.Core (1.2.0), Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore (8.0.10), Scalar.AspNetCore (2.17.1), Serilog.Enrichers.Environment (3.0.1), Serilog.Enrichers.Thread (4.0.0) (+9 more)

### Community 46 - ".SaveChangesAsync"
Cohesion: 0.03
Nodes (85): IBaseCommand, ICommand, ICommandHandler, Guid, IUserOwnedRequest, UserId, UpdateUserDto, Guid (+77 more)

### Community 47 - ".Handle"
Cohesion: 0.22
Nodes (11): Candidates, HasOverlap, CancellationToken, Distance, Guid, ILogger, IReadOnlyList, List (+3 more)

### Community 48 - "MatchHub"
Cohesion: 0.29
Nodes (6): Hub, Exception, ILogger, IMediator, Task, MatchHub

### Community 49 - "Public API Design and Compatibility"
Cohesion: 0.06
Nodes (31): Anti-Patterns, API Approval Testing, API Change Guidelines, Benefits, Breaking Changes Disguised as Fixes, Chesterton's Fence, Deprecation Pattern, Encapsulation Patterns (+23 more)

### Community 50 - "Skill"
Cohesion: 0.22
Nodes (8): DateTime, Guid, ICollection, Skill, Name, RegisteredAt, SkillId, UserSkills

### Community 51 - "IMatchRepository"
Cohesion: 0.32
Nodes (9): AcceptedIds, CancellationToken, Guid, IEnumerable, IReadOnlyList, Match, RejectedIds, Task (+1 more)

### Community 52 - ".GetByCategoriesAsync"
Cohesion: 0.23
Nodes (11): Authorize, CancellationToken, Guid, HttpGet, HttpPost, IEnumerable, ISender, List (+3 more)

### Community 53 - ".Handle"
Cohesion: 0.16
Nodes (12): Guid, CreateMatchRejectionCommand, CreatedBy, ExpertId, RecruiterId, CancellationToken, Dictionary, expertStatus (+4 more)

### Community 54 - "UpdateRecruiterCommandHandler.cs"
Cohesion: 0.18
Nodes (6): Trivo.Application.Features.Recruiters.Commands.UpdateRecruiter, Trivo.Application.Features.Recruiters, Trivo.Application.DTOs.Recruiter, Trivo.Application.Features.Recruiters.Commands.CreateRecruiter, CreateRecruiterValidator, UpdateRecruiterValidator

### Community 55 - ".ToDto"
Cohesion: 0.22
Nodes (7): Guid, UserChatDto, Chat, Guid, Message, User, ChatMapper

### Community 56 - ".CreateSkillAsync"
Cohesion: 0.24
Nodes (9): Authorize, CancellationToken, HttpGet, HttpPost, IEnumerable, ISender, ProducesResponseType, Task (+1 more)

### Community 57 - "AdministratorRepository"
Cohesion: 0.19
Nodes (10): Administrator, CancellationToken, Expert, Guid, IEnumerable, Match, Recruiter, Task (+2 more)

### Community 58 - ".GetRolesAsync"
Cohesion: 0.14
Nodes (12): CancellationToken, Guid, IList, Task, Administrator, CancellationToken, Expert, Guid (+4 more)

### Community 59 - "Trivo.Infrastructure.Shared.csproj"
Cohesion: 0.15
Nodes (12): CloudinaryDotNet (1.27.0), MailKit (4.17.0), Microsoft.AspNetCore.Authentication.JwtBearer (8.0.10), Microsoft.AspNetCore.SignalR (1.2.0), Microsoft.Extensions.Options (10.0.3), Microsoft.Extensions.Options.ConfigurationExtensions (8.0.0), Microsoft.IdentityModel.Tokens (8.10.0), MimeKit (4.17.0) (+4 more)

### Community 60 - "IInterestRepository"
Cohesion: 0.29
Nodes (6): CancellationToken, Guid, IEnumerable, List, Task, IInterestRepository

### Community 61 - "ResolveReportCommandHandler"
Cohesion: 0.16
Nodes (15): DateTime, Guid, ReportResolutionDto, Guid, ResolveReportCommand, CancellationToken, DateTime, Guid (+7 more)

### Community 62 - "Database Performance Patterns"
Cohesion: 0.07
Nodes (26): Always Apply Row Limits, Architecture, AsNoTracking for Read Queries, Avoid Cartesian Explosions, Avoid N+1 Queries, Configure Default Behavior, Constrain Column Sizes, Core Principles (+18 more)

### Community 63 - "Documento de Requerimientos de Software"
Cohesion: 0.17
Nodes (11): 1. Búsqueda y Filtros, 2. Gestión de Usuarios, 3. Sistema de Reportes, 4. Reportes y Listados PDF, Cambios básicos en la información del usuario, Documento de Requerimientos de Software, Endpoints de Listados Requeridos, Filtros personalizados para buscar recomendaciones (+3 more)

### Community 64 - "TrivoContext"
Cohesion: 0.08
Nodes (24): DbContext, DbContextOptions, DbSet, CancellationToken, ModelBuilder, Task, TrivoContext, Administrators (+16 more)

### Community 65 - "SkillRepository"
Cohesion: 0.29
Nodes (6): CancellationToken, Guid, IEnumerable, List, Task, SkillRepository

### Community 66 - ".ValidateEmailAsync"
Cohesion: 0.25
Nodes (7): CancellationToken, Task, IEmailValidationService, CancellationToken, ILogger, Task, EmailValidationService

### Community 67 - "Code"
Cohesion: 0.14
Nodes (13): DateTime, Guid, Code, CodeId, CreatedAt, ExpiresAt, IsRevoked, IsUsed (+5 more)

### Community 68 - "Slopwatch: LLM Anti-Cheat for .NET"
Cohesion: 0.09
Nodes (22): After Every Code Change, As a Global Tool, As a Local Tool (Recommended), Azure Pipelines, CI/CD Integration, Claude Code Hook Integration, Common Slop Patterns, Configuration (+14 more)

### Community 69 - "IChatRepository"
Cohesion: 0.15
Nodes (15): ILogger, CreateChatCommandHandler, CancellationToken, Task, CancellationToken, Task, CancellationToken, Task (+7 more)

### Community 70 - "Report and Sanction Management (RF9 - RF1 & RF2)"
Cohesion: 0.10
Nodes (20): Blocking account access (`IAccountAccessService`), Business rules, Commits included, Creating a report (`CreateReportCommandHandler`), Database migration, Design decisions, Domain model changes, Endpoints (+12 more)

### Community 71 - ".Handle"
Cohesion: 0.19
Nodes (13): Guid, IEnumerable, GetMatchByUserQuery, CancellationToken, Dictionary, Func, Guid, IEnumerable (+5 more)

### Community 72 - ".UpdateExpertAsync"
Cohesion: 0.18
Nodes (10): Authorize, CancellationToken, Guid, HttpPost, HttpPut, ISender, ProducesResponseType, Task (+2 more)

### Community 73 - "CacheEntryOptions"
Cohesion: 0.11
Nodes (17): IDatabase, IReadOnlyList, TimeSpan, CacheEntryOptions, AbsoluteExpiration, Tags, CacheProfiles, Cold (+9 more)

### Community 74 - "GoogleGeminiEmbeddingService"
Cohesion: 0.16
Nodes (14): ContentPart, EmbedContentResponse, EmbeddingValues, HttpClient, CancellationToken, ILogger, Task, ContentPart (+6 more)

### Community 75 - "Módulo de Matchmaking con IA — Implementación"
Cohesion: 0.14
Nodes (13): 1. Problema de negocio, 2.1 Abstracción del proveedor — `IEmbeddingService`, 2.2 Construcción del texto — `UserProfileTextBuilder`, 2.3 Cuándo se regenera el embedding — `UserProfileChangedEvent`, 2. Arquitectura, 3. Cambios de esquema (tablas), 4. El algoritmo de recomendación, 5. Qué NO se cachea, y por qué (+5 more)

### Community 76 - "Plan de implementación — Embeddings vía API externa para emparejamiento por afinidad"
Cohesion: 0.14
Nodes (13): 0. Decisión de proveedor y por qué la interfaz debe ser agnóstica, 10. Pruebas, 1. Infraestructura de base de datos (bloqueante, va primero), 2. Dominio (`Trivo.Domain`), 3. Application (`Trivo.Application`), 4. Infrastructure.Shared — implementación del proveedor, 5. Infrastructure.Persistence — columna, mapping e índice, 6. Cuándo se genera/regenera el embedding (+5 more)

### Community 77 - "ISkillRepository"
Cohesion: 0.26
Nodes (8): ILogger, SearchSkillsByNameQueryHandler, CancellationToken, Guid, IEnumerable, List, Task, ISkillRepository

### Community 78 - "ResultT"
Cohesion: 0.15
Nodes (6): IFormFile, UpdateProfilePictureRequest, ILogger, PaginationValidator, ResultT, Value

### Community 79 - "Nullable Attributes Reference"
Cohesion: 0.17
Nodes (12): Attribute Catalog, `[DoesNotReturn]`, `[DoesNotReturnIf(bool)]`, Helper methods: `MemberNotNull` and `MemberNotNullWhen`, `[MaybeNull]`, `[MemberNotNull]`, `[MemberNotNullWhen(bool)]`, `[NotNull]` (+4 more)

### Community 80 - "MatchRepository"
Cohesion: 0.34
Nodes (9): AcceptedIds, CancellationToken, Guid, IEnumerable, IReadOnlyList, Match, RejectedIds, Task (+1 more)

### Community 81 - "Modern C# Coding Standards"
Cohesion: 0.17
Nodes (12): Additional Resources, Avoid Reflection-Based Metaprogramming, Best Practices Summary, Code Organization, Composition Over Inheritance, Core Principles, DO's, DON'Ts (+4 more)

### Community 82 - ".UpdateRecruiterAsync"
Cohesion: 0.19
Nodes (10): Authorize, CancellationToken, Guid, HttpPost, HttpPut, ISender, ProducesResponseType, Task (+2 more)

### Community 83 - "IAdministratorRepository"
Cohesion: 0.24
Nodes (5): CancellationToken, Guid, IEnumerable, Task, IAdministratorRepository

### Community 84 - "ControllerBase"
Cohesion: 0.18
Nodes (10): ControllerBase, Authorize, CancellationToken, HttpPost, ISender, Task, ChatController, Guid (+2 more)

### Community 85 - "Plantillas copy-paste — feature CQRS de Trivo"
Cohesion: 0.18
Nodes (10): 1. DTO, 2. Command (con respuesta) — ejemplo real: `CreateInterestCommand`, 3. Validator (co-ubicado con el Command), 4. Handler — orquesta repos + UnitOfWork, nunca lanza excepciones de negocio, 5. Query con paginación + cache — ejemplo real: `GetInterestsPaginationQuery`, 6. Mapper — extensiones estáticas, una clase por feature, 7. Repositorio — patrón MANDATORIO para entidades nuevas (extiende `IGenericRepository<T>`), 8. DI — registrar el repo nuevo (+2 more)

### Community 86 - "ICloudinaryService"
Cohesion: 0.21
Nodes (10): CancellationToken, Stream, Task, ICloudinaryService, CancellationToken, IOptions, Stream, Task (+2 more)

### Community 87 - ".Handle"
Cohesion: 0.24
Nodes (8): Guid, InterestDetailsDto, Guid, CreateInterestCommand, CancellationToken, ILogger, Task, CreateInterestCommandHandler

### Community 88 - "Polyfilling the nullable attributes for older target frameworks"
Cohesion: 0.20
Nodes (10): Candidate packages (evaluate, do not default to one), Decision rules, File-level `#nullable` directives, Incremental Adoption Strategy, Is a polyfill needed at all?, Options and tradeoffs, Polyfilling the nullable attributes for older target frameworks, Project-level (+2 more)

### Community 89 - "MatchDto"
Cohesion: 0.22
Nodes (10): Guid, List, ExpertAiRecommendationDto, DateTime, Guid, MatchDto, Guid, List (+2 more)

### Community 90 - "IReportRepository"
Cohesion: 0.12
Nodes (19): DateTime, Guid, ReportDetailDto, Guid, GetReportByIdQuery, CancellationToken, ILogger, Task (+11 more)

### Community 91 - "SanctionDto"
Cohesion: 0.24
Nodes (9): DateTime, Guid, SanctionDto, Guid, GetSanctionsHistoryQuery, CancellationToken, ILogger, Task (+1 more)

### Community 92 - ".Handle"
Cohesion: 0.12
Nodes (13): Trivo.Application.Behaviors, IHttpContextAccessor, IPipelineBehavior, IValidator, CancellationToken, RequestHandlerDelegate, Task, AuthorizationBehavior (+5 more)

### Community 93 - "Trivo.Application.csproj"
Cohesion: 0.20
Nodes (9): BCrypt.Net-Next (4.0.3), FluentValidation (11.10.0), FluentValidation.DependencyInjectionExtensions (11.10.0), Microsoft.Extensions.DependencyInjection.Abstractions (8.0.2), net8.0, MediatR (12.2.0), Microsoft.Extensions.Caching.StackExchangeRedis (8.0.10), Serilog.AspNetCore (8.0.0) (+1 more)

### Community 94 - "C# Nullable Reference Types"
Cohesion: 0.20
Nodes (10): API Design Rules (Signatures), C# Nullable Reference Types, Core Goals, Generation Checklist (Summary), Project Configuration, Public API compatibility for libraries, Reference Files, References (+2 more)

### Community 95 - "IEmbeddingService"
Cohesion: 0.25
Nodes (6): EmbeddingClient, IEmbeddingService, CancellationToken, ILogger, Task, OpenAiEmbeddingService

### Community 96 - "NRT Migration Playbook Reference"
Cohesion: 0.22
Nodes (6): Full Generation Checklist, Gradual annotation of a library, Legacy and Unannotated API Interop, NRT Migration Playbook Reference, Trust annotated libraries, Wrapping unannotated or legacy APIs

### Community 97 - ".Handle"
Cohesion: 0.22
Nodes (6): IEnumerable, SearchSkillsByNameQuery, CancellationToken, IEnumerable, Task, SearchSkillsByNameValidator

### Community 98 - ".Handle"
Cohesion: 0.19
Nodes (11): DateTime, Guid, AdminDto, Administrator, AdminMapper, IFormFile, CreateAdminCommand, CancellationToken (+3 more)

### Community 99 - "Anti-Patterns to Avoid"
Cohesion: 0.25
Nodes (8): Anti-Patterns to Avoid, Don't: Block on async code, Don't: Create deep inheritance hierarchies, Don't: Forget CancellationToken in async methods, Don't: Return List<T> when you mean IReadOnlyList<T>, Don't: Use byte[] when ReadOnlySpan<byte> works, Don't: Use classes for value objects, Don't: Use mutable DTOs

### Community 100 - "IMatchHub"
Cohesion: 0.43
Nodes (4): Guid, IEnumerable, Task, IMatchHub

### Community 101 - "Report"
Cohesion: 0.10
Nodes (20): DateTime, Guid, Report, CreatedAt, FinalReason, Message, MessageId, Note (+12 more)

### Community 102 - "AuthenticationService"
Cohesion: 0.20
Nodes (9): CancellationToken, Task, IAuthenticationService, Administrator, CancellationToken, IOptions, Task, User (+1 more)

### Community 103 - "GetExpertsPagedQuery"
Cohesion: 0.27
Nodes (8): DateTime, Guid, AdminExpertDto, GetExpertsPagedQuery, CancellationToken, ILogger, Task, GetExpertsPagedQueryHandler

### Community 104 - "ResultFilter"
Cohesion: 0.22
Nodes (7): ActionExecutingContext, ActionExecutionDelegate, Trivo.API.Filters, IAsyncActionFilter, ILogger, Task, ResultFilter

### Community 105 - "Anti-Patterns and Reflection Avoidance"
Cohesion: 0.29
Nodes (3): Anti-Patterns and Reflection Avoidance, Contents, UnsafeAccessorAttribute (.NET 8+)

### Community 106 - ".GetUserId"
Cohesion: 0.40
Nodes (3): Guid, HttpContext, AuthenticatedUserHelper

### Community 107 - "Avoid Reflection-Based Metaprogramming"
Cohesion: 0.29
Nodes (7): Avoid Reflection-Based Metaprogramming, Banned Libraries, Benefits of Explicit Mappings, Complex Mappings, Use Explicit Mapping Methods Instead, When Reflection is Acceptable, Why Reflection Mapping Fails

### Community 108 - "Trivo"
Cohesion: 0.14
Nodes (13): API responses — Result Pattern, Building the Docker image, Errors, Health checks, Logging, Prerequisites, Project structure, Running locally (+5 more)

### Community 109 - "SkillWithIdDto"
Cohesion: 0.23
Nodes (10): Guid, SkillWithIdDto, Guid, IEnumerable, GetUserSkillsQuery, CancellationToken, IEnumerable, ILogger (+2 more)

### Community 110 - "Performance and API Design Patterns"
Cohesion: 0.29
Nodes (6): Accept Abstractions, Return Appropriately Specific, API Design Principles, Contents, Method Signatures Best Practices, Performance and API Design Patterns, Span<T> and Memory<T> for Zero-Allocation Code

### Community 111 - "Value Objects and Pattern Matching"
Cohesion: 0.29
Nodes (7): Constraint-Enforcing Value Objects, Contents, No Implicit Conversions, Pattern Matching (C# 8-12), TypeConverter Support for Configuration Binding, Value Objects and Pattern Matching, Value Objects as readonly record struct

### Community 112 - "RF9. Administrar aplicación — Documentación de implementación"
Cohesion: 0.10
Nodes (19): RF9. Administrar aplicación, 1. Resumen, 2.1 Enums, 2.2 Entidad `Report` (rediseñada), 2.3 Entidad `Sanction` (nueva), 2.4 Migración de base de datos, 2. Modelo de dominio nuevo, 3.1 Crear un reporte (`CreateReportCommandHandler`) (+11 more)

### Community 113 - ".Handle"
Cohesion: 0.29
Nodes (6): LoginUserCommand, CancellationToken, ILogger, Task, LoginUserCommandHandler, LoginUserCommandValidator

### Community 114 - "Sanction"
Cohesion: 0.15
Nodes (13): DateTime, Guid, Sanction, Admin, AdminId, ExpiresAt, Reason, Report (+5 more)

### Community 115 - "ReportDto"
Cohesion: 0.11
Nodes (15): Authorize, CancellationToken, HttpPost, ISender, ProducesResponseType, Task, ReportController, DateTime (+7 more)

### Community 116 - "User"
Cohesion: 0.05
Nodes (44): Guid, List, UserAiRecommendationDto, Guid, GetUserRecommendationsQuery, GetUserRecommendationsValidator, Guid, List (+36 more)

### Community 117 - "MessageDto"
Cohesion: 0.10
Nodes (26): Authorize, CancellationToken, HttpPost, ISender, Task, MessageController, DateTime, Guid (+18 more)

### Community 118 - "Trivo.Infrastructure.Persistence.Migrations"
Cohesion: 0.24
Nodes (6): Trivo.Infrastructure.Persistence.Migrations, DateTime, Guid, MigrationBuilder, Vector, InitialCreate

### Community 119 - ".GetExpertIdAsync"
Cohesion: 0.25
Nodes (6): CancellationToken, Guid, Task, CancellationToken, Guid, Task

### Community 120 - ".GetRecruiterIdAsync"
Cohesion: 0.25
Nodes (6): CancellationToken, Guid, Task, CancellationToken, Guid, Task

### Community 121 - "AddUnaccentExtension"
Cohesion: 0.20
Nodes (6): MigrationBuilder, DateTime, Guid, ModelBuilder, Vector, AddUnaccentExtension

### Community 122 - "EmailSetting"
Cohesion: 0.18
Nodes (10): EmailSetting, DisplayName, EmailFrom, SmtpHost, SmtpPassword, SmtpPort, SmtpUser, IOptions (+2 more)

### Community 123 - "Trivo.Infrastructure.Persistence.csproj"
Cohesion: 0.22
Nodes (8): Microsoft.EntityFrameworkCore (8.0.10), Npgsql.EntityFrameworkCore.PostgreSQL (8.0.10), Pgvector.EntityFrameworkCore (0.2.2), StackExchange.Redis (2.7.27), net8.0, Microsoft.EntityFrameworkCore.Design (8.0.10), Microsoft.Extensions.Caching.StackExchangeRedis (8.0.10), Microsoft.NET.Sdk

### Community 124 - "The `field` Keyword (C# 14 / .NET 10) and Nullability"
Cohesion: 0.33
Nodes (6): Lazy-initialized property (null-resilient getter), Non-resilient getter escape hatch, Null-resilience, Other notes, Setter and constructor analysis, The `field` Keyword (C# 14 / .NET 10) and Nullability

### Community 125 - "MessageStatus"
Cohesion: 0.29
Nodes (6): MessageStatus, Deleted, Delivered, Seen, Sent, Updated

### Community 126 - "Trivo.Domain.csproj"
Cohesion: 0.29
Nodes (3): Pgvector (0.3.0), net8.0, Microsoft.NET.Sdk

### Community 127 - ".Handle"
Cohesion: 0.14
Nodes (12): INotification, INotificationHandler, Guid, UserProfileChangedEvent, CancellationToken, ILogger, Task, UserProfileChangedEventHandler (+4 more)

### Community 128 - ".ToEntity"
Cohesion: 0.33
Nodes (4): Trivo.Application.Features.Administrator.Commands.CreateAdministrator.Mappings, Administrator, CreateAdminCommand, AdminMappingExtensions

### Community 129 - "UserMappingExtensions.cs"
Cohesion: 0.33
Nodes (4): Trivo.Application.Features.Users.Commands.CreateUser.Mappings, CreateUserCommand, User, UserMappingExtensions

### Community 130 - ".CreateMatchAsync"
Cohesion: 0.36
Nodes (7): Authorize, CancellationToken, HttpPost, HttpPut, ISender, Task, MatchController

### Community 131 - "ErrorType"
Cohesion: 0.15
Nodes (12): ErrorType, Conflict, Custom, ExternalService, Forbidden, NotFound, Timeout, TooManyRequests (+4 more)

### Community 132 - "NotificationType"
Cohesion: 0.33
Nodes (5): NotificationType, Alert, Match, Message, Reminder

### Community 133 - "CustomUserIdProvider"
Cohesion: 0.40
Nodes (3): HubConnectionContext, IUserIdProvider, CustomUserIdProvider

### Community 134 - "ExpertStatus"
Cohesion: 0.40
Nodes (4): ExpertStatus, Completed, Pending, Rejected

### Community 135 - "Level"
Cohesion: 0.40
Nodes (4): Level, Advanced, Basic, Intermediate

### Community 136 - "MatchStatus"
Cohesion: 0.40
Nodes (4): MatchStatus, Completed, Pending, Rejected

### Community 137 - "MessageType"
Cohesion: 0.40
Nodes (4): MessageType, File, Image, Text

### Community 138 - "RecruiterStatus"
Cohesion: 0.40
Nodes (4): RecruiterStatus, Completed, Pending, Rejected

### Community 139 - "UserStatus"
Cohesion: 0.33
Nodes (5): UserStatus, Active, Banned, Inactive, Suspended

### Community 141 - "ChatType"
Cohesion: 0.50
Nodes (3): ChatType, Group, Private

### Community 142 - "MatchFault"
Cohesion: 0.50
Nodes (3): MatchFault, Expert, Recruiter

### Community 143 - ".RefreshTokenAsync"
Cohesion: 0.20
Nodes (8): CancellationToken, HttpPost, ProducesResponseType, SwaggerOperation, Task, AuthController, RefreshTokenRequest, RefreshToken

### Community 144 - "ReportListItemDto"
Cohesion: 0.11
Nodes (20): DateTime, Guid, ReportListItemDto, IEnumerable, GetLatestReportsQuery, CancellationToken, IEnumerable, ILogger (+12 more)

### Community 145 - ".GetDetailsByUserIdsAsync"
Cohesion: 0.36
Nodes (6): CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task

### Community 146 - "Core Nullability Model"
Cohesion: 0.40
Nodes (5): Core Nullability Model, Non-nullable vs nullable, Null-forgiving operator (`!`), Null-state analysis (flow), Reorganize code before suppressing warnings

### Community 147 - "Arquitectura CQRS de Trivo — mandato para nuevas features"
Cohesion: 0.40
Nodes (4): Arquitectura CQRS de Trivo — mandato para nuevas features, Checklist para agregar una feature nueva ("{Feature}" / "{Action}" / "{Entity}"), Flujo de referencia rápido, Reglas duras (no negociables)

### Community 148 - "Composition and Error Handling"
Cohesion: 0.40
Nodes (5): Composition and Error Handling, Composition Over Inheritance, Contents, Result Type Pattern, Testing Patterns

### Community 149 - "Language Patterns"
Cohesion: 0.40
Nodes (5): Language Patterns, Nullable Reference Types (C# 8+), Pattern Matching (C# 8-12), Records for Immutable Data (C# 9+), Value Objects as readonly record struct

### Community 150 - "AiSetting"
Cohesion: 0.40
Nodes (4): AiSetting, ApiKey, EmbeddingModel, Provider

### Community 151 - ".Handle"
Cohesion: 0.27
Nodes (8): ReportType, Guid, CreateReportCommand, UserId, CancellationToken, ILogger, Task, CreateReportCommandHandler

### Community 152 - "Known Static-Analysis Limitations and Safe Patterns"
Cohesion: 0.50
Nodes (4): Arrays and default values, Known Static-Analysis Limitations and Safe Patterns, Other limitations to keep in mind, Structs with non-nullable fields

### Community 153 - "Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`"
Cohesion: 0.50
Nodes (4): Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`, `[MaybeNullWhen(bool)]`, `[NotNullIfNotNull(string)]`, `[NotNullWhen(bool)]`

### Community 154 - "Fix #43 — `ReceiveChats` no devolvía `lastMessage`"
Cohesion: 0.29
Nodes (6): Causa, Fix #43 — `ReceiveChats` no devolvía `lastMessage`, Pendiente (fuera de este fix), Resultado, Solución, Síntoma

### Community 155 - "Preconditions: `AllowNull` and `DisallowNull`"
Cohesion: 0.67
Nodes (3): `[AllowNull]`, `[DisallowNull]`, Preconditions: `AllowNull` and `DisallowNull`

### Community 156 - "Performance Patterns"
Cohesion: 0.67
Nodes (3): Async/Await Best Practices, Performance Patterns, Span<T> and Memory<T>

### Community 158 - "AddPendingEmailToUser"
Cohesion: 0.20
Nodes (6): MigrationBuilder, DateTime, Guid, ModelBuilder, Vector, AddPendingEmailToUser

### Community 159 - "ReportMapper"
Cohesion: 0.21
Nodes (7): DateTime, Guid, ReportUserDetailDto, DateTime, Message, User, ReportMapper

### Community 160 - "FilterUsersByInterestsAndSkillsRequest"
Cohesion: 0.50
Nodes (3): Guid, List, FilterUsersByInterestsAndSkillsRequest

### Community 161 - "ExpertRepository"
Cohesion: 0.36
Nodes (7): CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task, ExpertRepository

### Community 163 - "UpdateNameCommand"
Cohesion: 0.31
Nodes (7): UpdateNameDto, Guid, UpdateNameCommand, CancellationToken, ILogger, Task, UpdateNameCommandHandler

### Community 164 - "AddUniqueExpertRecruiterUserId"
Cohesion: 0.20
Nodes (6): MigrationBuilder, DateTime, Guid, ModelBuilder, Vector, AddUniqueExpertRecruiterUserId

### Community 165 - ".Validation"
Cohesion: 0.08
Nodes (34): HttpDelete, Authorize, CancellationToken, Guid, HttpPost, HttpPut, Task, NotificationController (+26 more)

### Community 166 - ".BuildModel"
Cohesion: 0.25
Nodes (6): ModelSnapshot, DateTime, Guid, ModelBuilder, Vector, TrivoContextModelSnapshot

### Community 167 - "SkillDto"
Cohesion: 0.22
Nodes (6): DateTime, Guid, SkillDto, Guid, IEnumerable, SkillMapper

### Community 168 - "Administrator"
Cohesion: 0.18
Nodes (10): Administrator, Biography, Email, FirstName, IsActive, LastName, LinkedIn, PasswordHash (+2 more)

### Community 171 - "IQueryHandler"
Cohesion: 0.15
Nodes (13): IRequestHandler, IQueryHandler, CompletedMatchesCountDto, GetCompletedMatchesCountQuery, CancellationToken, ILogger, Task, GetCompletedMatchesCountQueryHandler (+5 more)

### Community 172 - ".Handle"
Cohesion: 0.31
Nodes (7): UserBiographyDto, Guid, GetUserBiographyQuery, CancellationToken, ILogger, Task, GetUserBiographyQueryHandler

### Community 173 - "AbstractValidator"
Cohesion: 0.04
Nodes (28): AbstractValidator, Trivo.Application.Features.Users.Commands.UpdateBiography, Trivo.Application.Features.Interests.Commands.UpdateInterest, Trivo.Application.Features.Users.Commands.ForgotPassword, GetSanctionsHistoryValidator, GetChatPaginationValidator, UpdateInterestValidator, GetInterestsPaginationValidator (+20 more)

### Community 174 - "InterestWithIdDto"
Cohesion: 0.13
Nodes (17): Guid, InterestWithIdDto, IEnumerable, SearchInterestsByNameQuery, CancellationToken, IEnumerable, ILogger, Task (+9 more)

### Community 175 - ".NotFound"
Cohesion: 0.10
Nodes (23): DateTime, Guid, CodeDto, CancellationToken, Task, CancellationToken, Guid, Task (+15 more)

### Community 176 - ".ValidateAsync"
Cohesion: 0.32
Nodes (6): CancellationToken, Expression, Func, Guid, Task, GenericRepository

### Community 177 - ".Handle"
Cohesion: 0.31
Nodes (7): UserProfilePictureDto, Guid, GetUserProfilePictureQuery, CancellationToken, ILogger, Task, GetUserProfilePictureQueryHandler

### Community 178 - "TokenResponseDto"
Cohesion: 0.19
Nodes (9): TokenResponseDto, AccessToken, RefreshToken, AdminLoginCommand, CancellationToken, ILogger, Task, AdminLoginCommandHandler (+1 more)

### Community 179 - "MatchDetailsDto"
Cohesion: 0.11
Nodes (17): DateTime, Guid, MatchDetailsDto, Guid, UpdateMatchingCommand, CancellationToken, Guid, ILogger (+9 more)

### Community 181 - ".BuildTargetModel"
Cohesion: 0.40
Nodes (4): DateTime, Guid, ModelBuilder, Vector

### Community 182 - ".BuildTargetModel"
Cohesion: 0.40
Nodes (4): DateTime, Guid, ModelBuilder, Vector

### Community 185 - "GetRecruitersPagedQuery"
Cohesion: 0.24
Nodes (9): DateTime, Guid, AdminRecruiterDto, GetRecruitersPagedQuery, CancellationToken, ILogger, Task, GetRecruitersPagedQueryHandler (+1 more)

### Community 186 - "IChatHub"
Cohesion: 0.33
Nodes (4): Guid, IEnumerable, Task, IChatHub

### Community 187 - "GetActiveUsersCountQueryHandler"
Cohesion: 0.36
Nodes (6): ActiveUsersCountDto, GetActiveUsersCountQuery, CancellationToken, ILogger, Task, GetActiveUsersCountQueryHandler

### Community 189 - "ConfirmAccountCommand"
Cohesion: 0.25
Nodes (7): Guid, ConfirmAccountCommand, CancellationToken, ILogger, Task, ConfirmAccountCommandHandler, ConfirmAccountValidator

### Community 190 - "Buenas prácticas .NET para Trivo — checklist central"
Cohesion: 0.33
Nodes (5): 1. ¿Qué skill detallado cargar?, 2. EF Core — reglas que más bugs causan en Trivo, 3. C# / arquitectura, 4. Antes de dar por terminado un cambio, Buenas prácticas .NET para Trivo — checklist central

### Community 194 - ".Handle"
Cohesion: 0.31
Nodes (7): UpdateUsernameDto, Guid, UpdateUsernameCommand, CancellationToken, ILogger, Task, UpdateUsernameCommandHandler

### Community 196 - "IQuery"
Cohesion: 0.15
Nodes (13): IRequest, IQuery, ReportedUsersCountDto, GetReportedUsersCountQuery, CancellationToken, ILogger, Task, GetReportedUsersCountQueryHandler (+5 more)

### Community 198 - "GetLatestUsersPagedQueryHandler.cs"
Cohesion: 0.17
Nodes (5): Trivo.Application.Features.Administrator.Query.GetLatestUsersPaged, Trivo.Application.Features.Users, Trivo.Application.Features.Administrator.Query.GetBannedUsersPaged, GetBannedUsersPagedValidator, GetLatestUsersPagedValidator

### Community 199 - "Roles"
Cohesion: 0.33
Nodes (5): Roles, Administrator, Expert, Recruiter, User

### Community 204 - "AddReportSanctions"
Cohesion: 0.32
Nodes (4): DateTime, Guid, MigrationBuilder, AddReportSanctions

### Community 206 - "JwtSetting"
Cohesion: 0.33
Nodes (5): JwtSetting, Audience, DurationInMinutes, Issuer, Key

### Community 207 - "ReportDecision"
Cohesion: 0.50
Nodes (3): ReportDecision, Approved, Rejected

### Community 208 - ".BuildTargetModel"
Cohesion: 0.40
Nodes (4): DateTime, Guid, ModelBuilder, Vector

### Community 209 - "ReportType"
Cohesion: 0.50
Nodes (3): ReportType, Message, Profile

### Community 210 - "IExpertRepository"
Cohesion: 0.08
Nodes (30): Guid, ExpertDto, Guid, RecruiterDto, Guid, CreateExpertCommand, CancellationToken, ILogger (+22 more)

### Community 212 - "AccountAccessService"
Cohesion: 0.12
Nodes (15): IServiceCollection, CancellationToken, Task, IAccountAccessService, CancellationToken, DateTime, ILogger, Task (+7 more)

### Community 215 - "UpdateExpertCommandHandler.cs"
Cohesion: 0.15
Nodes (7): Trivo.Application.DTOs.Expert, Trivo.Application.Features.Experts.Commands.CreateExpert, Trivo.Application.Features.Experts, Trivo.Application.Helpers, Trivo.Application.Features.Experts.Commands.UpdateExpert, CreateExpertValidator, UpdateExpertValidator

### Community 221 - ".Handle"
Cohesion: 0.22
Nodes (9): Guid, IFormFile, List, CreateUserCommand, CancellationToken, ILogger, IPublisher, Task (+1 more)

## Knowledge Gaps
- **677 isolated node(s):** `dotnet-guard.sh script`, `$schema`, `windowsAuthentication`, `anonymousAuthentication`, `applicationUrl` (+672 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **5 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `ResultT` connect `ResultT` to `.CreateMatchAsync`, `InterestCategory`, `Trivo.API.Controllers.V1.Requests`, `UserController`, `.RefreshTokenAsync`, `ReportListItemDto`, `ChatDto`, `.Handle`, `.Handle`, `Error`, `.Handle`, `PagedResult`, `.Handle`, `.Handle`, `AdminController`, `.GetOrSetAsync`, `UpdateNameCommand`, `.Validation`, `IQueryHandler`, `.Handle`, `.SaveChangesAsync`, `.NotFound`, `InterestWithIdDto`, `.Handle`, `TokenResponseDto`, `MatchDetailsDto`, `.GetByCategoriesAsync`, `.Handle`, `.Handle`, `.CreateSkillAsync`, `GetRecruitersPagedQuery`, `GetActiveUsersCountQueryHandler`, `ResolveReportCommandHandler`, `ConfirmAccountCommand`, `.Handle`, `.ValidateEmailAsync`, `IQuery`, `IChatRepository`, `.Handle`, `.UpdateExpertAsync`, `.UpdateRecruiterAsync`, `IExpertRepository`, `ControllerBase`, `.Handle`, `IReportRepository`, `SanctionDto`, `.Handle`, `.Handle`, `.Handle`, `AuthenticationService`, `GetExpertsPagedQuery`, `SkillWithIdDto`, `.Handle`, `ReportDto`, `User`, `MessageDto`?**
  _High betweenness centrality (0.140) - this node is a cross-community bridge._
- **Why does `Trivo.Domain.Models` connect `Trivo.Domain.Models` to `Message`, `InterestCategory`, `Trivo.Application.Abstractions.Messages`, `Trivo.Application.Caching`, `Trivo.Infrastructure.Persistence.Configurations`, `AdminController.cs`, `Trivo.Domain.Enums`, `Notification`, `Expert`, `Match`, `Chat`, `UserInterest`, `UserSkill`, `Administrator`, `Trivo.Application.Utils`, `Skill`, `UpdateRecruiterCommandHandler.cs`, `Code`, `GetLatestUsersPagedQueryHandler.cs`, `UpdateExpertCommandHandler.cs`, `Report`?**
  _High betweenness centrality (0.067) - this node is a cross-community bridge._
- **Why does `TrivoContext` connect `TrivoContext` to `UserRepository`, `Message`, `InterestCategory`, `Trivo.Domain.Models`, `Notification`, `Expert`, `Match`, `.AddRepositories`, `Sanction`, `Recruiter`, `CodeRepository`, `ExpertRepository`, `Chat`, `UserInterest`, `UserSkill`, `InterestRepository`, `ExceptionHandlingMiddleware`, `Administrator`, `Interest`, `.SaveChangesAsync`, `.ValidateAsync`, `Skill`, `AdministratorRepository`, `SkillRepository`, `Code`, `MatchRepository`, `IReportRepository`, `Report`, `User`?**
  _High betweenness centrality (0.061) - this node is a cross-community bridge._
- **What connects `dotnet-guard.sh script`, `$schema`, `windowsAuthentication` to the rest of the system?**
  _677 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Message` be split into smaller, more focused modules?**
  _Cohesion score 0.10810810810810811 - nodes in this community are weakly interconnected._
- **Should `InterestCategory` be split into smaller, more focused modules?**
  _Cohesion score 0.056107539450613676 - nodes in this community are weakly interconnected._
- **Should `Trivo.Application.Abstractions.Messages` be split into smaller, more focused modules?**
  _Cohesion score 0.045709908069458634 - nodes in this community are weakly interconnected._