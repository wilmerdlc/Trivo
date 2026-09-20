# Graph Report - Trivo  (2026-09-20)

## Corpus Check
- 430 files · ~79,069 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 3073 nodes · 6943 edges · 181 communities (176 shown, 5 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 423 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `1bb9249c`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- UserRepository
- Trivo.Application.Abstractions.Messages
- Message
- InterestCategory
- Trivo.Application.DTOs.Users
- Trivo.Domain.Models
- Trivo.Application.Pagination
- Trivo.Infrastructure.Persistence.Configurations
- ReportDto
- IUserRepository
- Trivo.Application.Interfaces.SignalR
- https
- UserController
- Trivo.Infrastructure.Persistence.Migrations
- Notification
- ExpertDto
- Match
- .AddRepositories
- IRealTimeNotifier
- Code
- BHD.ResultPattern — Guía de arquitectura e implementación
- MatchDetailsDto
- User
- Dependency Injection Patterns
- Trivo.Domain.Enums
- Result
- IInterestCategoryRepository
- UserDto
- Recruiter
- IAdministratorRepository
- .Handle
- .Handle
- AdminController
- NotificationDto
- Chat
- .Handle
- UserSkill
- InterestRepository
- Entity Framework Core Patterns
- ExceptionHandlingMiddleware
- NotificationHub
- Requerimientos Funcionales
- .AddServices
- InterestCategoryDto
- Interest
- Trivo.API.csproj
- ResultT
- IInterestRepository
- GetMatchByUserQuery
- Public API Design and Compatibility
- Skill
- IChatRepository
- .GetByCategoriesAsync
- IMatchRepository
- ExpertController.cs
- GetMatchByUserQueryHandler
- .CreateSkillAsync
- Error
- .GetRolesAsync
- Trivo.Infrastructure.Shared.csproj
- ControllerBase
- ChatUser
- Database Performance Patterns
- Documento de Requerimientos de Software
- TrivoContext
- ISkillRepository
- UserInterest
- SkillDto
- Slopwatch: LLM Anti-Cheat for .NET
- UserAiRecommendationDto
- Validation
- Expert
- Trivo.Application.DTOs.Administrator
- ICacheService
- GoogleGeminiEmbeddingService
- Módulo de Matchmaking con IA — Implementación
- Plan de implementación — Embeddings vía API externa para emparejamiento por afinidad
- Trivo.Application.DTOs.Authentication
- SkillRepository
- Nullable Attributes Reference
- MatchRepository
- Modern C# Coding Standards
- .Handle
- .MapToInterests
- .Handle
- Plantillas copy-paste — feature CQRS de Trivo
- ICloudinaryService
- SkillWithIdDto
- Polyfilling the nullable attributes for older target frameworks
- IQuery
- CreateMatchingCommandHandler
- .ValidateEmailAsync
- .Handle
- Trivo.Application.csproj
- C# Nullable Reference Types
- IEmbeddingService
- NRT Migration Playbook Reference
- .Validation
- UpdateInterestCommand
- Anti-Patterns to Avoid
- AuthenticationService
- Report
- UpdateProfilePictureCommand
- UpdateSkillCommand
- ResultFilter
- Anti-Patterns and Reflection Avoidance
- .GetUserId
- Avoid Reflection-Based Metaprogramming
- Trivo
- UpdateMatchingCommand
- Performance and API Design Patterns
- Value Objects and Pattern Matching
- RF9-administrar-aplicacion.md
- PagedResult
- ICommand
- .CreateInterestCategoryAsync
- .Handle
- User
- CreateReportCommandHandler.cs
- .GetExpertIdAsync
- .GetRecruiterIdAsync
- UnbanUserCommand
- EmailSetting
- Trivo.Infrastructure.Persistence.csproj
- The `field` Keyword (C# 14 / .NET 10) and Nullability
- MessageStatus
- Trivo.Domain.csproj
- ChatDto
- .ToEntity
- UserMappingExtensions.cs
- IChatHub
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
- .Handle
- ReportStatus
- IGenericRepository
- Core Nullability Model
- Arquitectura CQRS de Trivo — mandato para nuevas features
- Composition and Error Handling
- Language Patterns
- .Handle
- .Handle
- Known Static-Analysis Limitations and Safe Patterns
- Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`
- InterestCategoryRepository
- Preconditions: `AllowNull` and `DisallowNull`
- Performance Patterns
- CLAUDE.md
- ChatHub
- ExpertRepository
- CreateInterestCommandHandler
- ForgotPasswordCommand
- .Handle
- SendImageCommand
- AiSetting
- IExpertRepository
- IMatchHub
- MessageDto
- .Handle
- UpdateUserCommand
- .Handle
- EmailTemplate
- ConfirmAccountCommand
- ICommandHandler
- InterestWithIdDto
- .Handle
- CloudinarySetting
- AbstractValidator
- IUserOwnedRequest

## God Nodes (most connected - your core abstractions)
1. `ResultT` - 140 edges
2. `Trivo.Application.Abstractions.Messages` - 112 edges
3. `Trivo.Domain.Models` - 92 edges
4. `Trivo.Application.Utils` - 88 edges
5. `Trivo.Application.Interfaces.Services` - 71 edges
6. `PagedResult` - 70 edges
7. `TrivoContext` - 61 edges
8. `IUserRepository` - 59 edges
9. `Trivo.Application.Pagination` - 57 edges
10. `Trivo.Application.Interfaces.Repository.Account` - 55 edges

## Surprising Connections (you probably didn't know these)
- `UserController` --references--> `ICodeService`  [EXTRACTED]
  src/API/Trivo.API/Controllers/V1/UserController.cs → src/Application/Trivo.Application/Interfaces/Services/ICodeService.cs
- `UserController` --references--> `IEmailValidationService`  [EXTRACTED]
  src/API/Trivo.API/Controllers/V1/UserController.cs → src/Application/Trivo.Application/Interfaces/Services/IEmailValidationService.cs
- `ICommand` --references--> `Result`  [EXTRACTED]
  src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs → src/Application/Trivo.Application/Utils/Result.cs
- `ICommand` --references--> `ResultT`  [EXTRACTED]
  src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs → src/Application/Trivo.Application/Utils/Result.cs
- `CreateAdminCommand` --implements--> `ICommand`  [EXTRACTED]
  src/Application/Trivo.Application/Features/Administrator/Commands/CreateAdministrator/CreateAdminCommand.cs → src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs

## Import Cycles
- None detected.

## Communities (181 total, 5 thin omitted)

### Community 0 - "UserRepository"
Cohesion: 0.20
Nodes (12): CancellationToken, Distance, Expert, Guid, IEnumerable, IReadOnlyList, List, Recruiter (+4 more)

### Community 1 - "Trivo.Application.Abstractions.Messages"
Cohesion: 0.13
Nodes (8): Trivo.Application.Abstractions.Messages, Trivo.Application.Interfaces.UnitOfWork, Trivo.Application.DTOs.Email, Trivo.Application.Utils, Trivo.Application.Caching, Trivo.Application.Features.Users.Events, Trivo.Application.Interfaces.Repository.Account, Trivo.Application.Interfaces.Services

### Community 2 - "Message"
Cohesion: 0.11
Nodes (27): CancellationToken, Guid, List, Task, IMessageRepository, DateTime, Guid, ICollection (+19 more)

### Community 3 - "InterestCategory"
Cohesion: 0.17
Nodes (10): DateTime, Guid, ICollection, InterestCategory, CategoryId, CreatedAt, Interests, Name (+2 more)

### Community 4 - "Trivo.Application.DTOs.Users"
Cohesion: 0.07
Nodes (20): Trivo.Application.Features.Users.Commands.CreateUser, Trivo.Application.Features.Interests.Commands.CreateInterest, Trivo.Application.Features.Users.Query.GetUserDetails, Trivo.Application.Features.Skills, Trivo.Application.Features.Users, Trivo.Application.Features.Users.Commands.UpdateUser, Trivo.Application.Features.Users.Query.GetUserInterests, Trivo.Application.DTOs.Users (+12 more)

### Community 5 - "Trivo.Domain.Models"
Cohesion: 0.11
Nodes (9): Trivo.Domain.Common, Trivo.Infrastructure.Persistence.Context, Trivo.Infrastructure.Persistence.Repository.Account, Trivo.Application.Interfaces.Repository.Base, Trivo.Infrastructure.Persistence.Services, Trivo.Infrastructure.Persistence.Repository, Trivo.Domain.Models, Trivo.Application.Interfaces.Repository (+1 more)

### Community 6 - "Trivo.Application.Pagination"
Cohesion: 0.09
Nodes (11): Trivo.Application.Features.Messages.Query.GetMessagePagination, Trivo.Application.Features.Skills.Query.GetSkillsPagination, Trivo.Application.DTOs.InterestCategories, Trivo.Application.Features.Administrator.Query.GetLatestUsersPaged, Trivo.Application.Features.Interests.Query.GetInterestsByCategoryId, Trivo.Application.Features.InterestCategories.Commands.CreateInterestCategory, Trivo.Application.Pagination, Trivo.Application.Features.Interests.Query.GetInterestsPagination (+3 more)

### Community 7 - "Trivo.Infrastructure.Persistence.Configurations"
Cohesion: 0.09
Nodes (15): Trivo.Infrastructure.Persistence.Configurations, IEntityTypeConfiguration, EntityTypeBuilder, AdministratorConfig, EntityTypeBuilder, ChatConfig, EntityTypeBuilder, CodeConfig (+7 more)

### Community 8 - "ReportDto"
Cohesion: 0.19
Nodes (8): DateTime, Guid, MessageReportDto, Guid, ReportDto, Guid, UserReportDto, ReportMapper

### Community 9 - "IUserRepository"
Cohesion: 0.22
Nodes (10): CancellationToken, Distance, Guid, IEnumerable, IReadOnlyList, List, Task, User (+2 more)

### Community 10 - "Trivo.Application.Interfaces.SignalR"
Cohesion: 0.07
Nodes (13): Trivo.Application.Features.Messages.Commands.SendImage, Trivo.Application.Features.Chat.Query.GetChatPagination, Trivo.Application.Features.Chat, Trivo.API.Controllers.V1, Trivo.Application.DTOs.Notifications, Trivo.Application.Features.Notifications, Trivo.Application.Interfaces.SignalR, Trivo.Application.Features.Chat.Commands.CreateChat (+5 more)

### Community 11 - "https"
Cohesion: 0.10
Nodes (21): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, launchUrl, commandName (+13 more)

### Community 12 - "UserController"
Cohesion: 0.10
Nodes (27): Trivo.API.Controllers.V1.Requests, ChangePasswordRequest, Guid, List, FilterUsersByInterestsAndSkillsRequest, UpdateBiographyRequest, UpdatePasswordRequest, IFormFile (+19 more)

### Community 13 - "Trivo.Infrastructure.Persistence.Migrations"
Cohesion: 0.05
Nodes (29): Trivo.Infrastructure.Persistence.Migrations, Migration, ModelSnapshot, DateTime, Guid, MigrationBuilder, Vector, DateTime (+21 more)

### Community 14 - "Notification"
Cohesion: 0.11
Nodes (21): CancellationToken, Guid, Task, INotificationRepository, DateTime, Guid, Notification, Content (+13 more)

### Community 15 - "ExpertDto"
Cohesion: 0.10
Nodes (18): Authorize, CancellationToken, Guid, HttpPost, HttpPut, ISender, ProducesResponseType, Task (+10 more)

### Community 16 - "Match"
Cohesion: 0.08
Nodes (23): DateTime, Guid, BaseEntity, CreatedAt, Id, UpdatedAt, Guid, Match (+15 more)

### Community 17 - ".AddRepositories"
Cohesion: 0.18
Nodes (14): IReportRepository, IUserSkillRepository, IGetExpertIdService, IGetRecruiterIdService, IUserRoleService, IConfiguration, IConnectionMultiplexer, IServiceCollection (+6 more)

### Community 18 - "IRealTimeNotifier"
Cohesion: 0.24
Nodes (9): Guid, IEnumerable, Task, IRealTimeNotifier, Guid, IEnumerable, IHubContext, Task (+1 more)

### Community 19 - "Code"
Cohesion: 0.12
Nodes (21): CancellationToken, Guid, Task, ICodeRepository, DateTime, Guid, Code, CodeId (+13 more)

### Community 20 - "BHD.ResultPattern — Guía de arquitectura e implementación"
Cohesion: 0.06
Nodes (31): 1. Arquitectura general, 2.1. Estructura de carpetas, 2.2. `BHD.ResultPattern.csproj`, 2.3. `Result.cs`, 2.4. `Error.cs`, 2. Núcleo: `BHD.ResultPattern` (sin dependencias), 3.1. Estructura de carpetas, 3.2. `BHD.ResultPattern.AspNetCore.csproj` (+23 more)

### Community 21 - "MatchDetailsDto"
Cohesion: 0.15
Nodes (16): DateTime, Guid, MatchDetailsDto, Guid, ILogger, Task, UpdateMatchingCommandHandler, Guid (+8 more)

### Community 22 - "User"
Cohesion: 0.07
Nodes (27): ICollection, Vector, User, Biography, ChatUsers, Codes, Email, Experts (+19 more)

### Community 23 - "Dependency Injection Patterns"
Cohesion: 0.05
Nodes (38): Advanced DI Patterns, Akka.DependencyInjection Reference, Akka.Hosting.TestKit, Akka.NET Actor Scope Management, Common Patterns, Conditional Registration, Contents, Factory-Based Registration (+30 more)

### Community 24 - "Trivo.Domain.Enums"
Cohesion: 0.15
Nodes (6): Trivo.Application.Features.Matching.Commands.UpdateMatch, Trivo.Application.Features.Matching.Commands.CreateMatch, Trivo.Domain.Enums, Trivo.Application.Features.Matching.Commands.CreateMatchRejection, Trivo.Application.Features.Matching.Query.GetMatchByUser, Trivo.Application.DTOs.Matching

### Community 25 - "Result"
Cohesion: 0.08
Nodes (28): DateTime, Guid, CodeDto, CancellationToken, ILogger, Task, ConfirmAccountCommandHandler, CancellationToken (+20 more)

### Community 26 - "IInterestCategoryRepository"
Cohesion: 0.44
Nodes (4): CancellationToken, Guid, Task, IInterestCategoryRepository

### Community 27 - "UserDto"
Cohesion: 0.15
Nodes (15): Guid, UserDto, IEnumerable, GetLast10BannedUsersQuery, CancellationToken, IEnumerable, ILogger, Task (+7 more)

### Community 28 - "Recruiter"
Cohesion: 0.06
Nodes (39): Authorize, CancellationToken, Guid, HttpPost, HttpPut, ISender, ProducesResponseType, Task (+31 more)

### Community 29 - "IAdministratorRepository"
Cohesion: 0.06
Nodes (40): DateTime, Guid, AdminDto, Administrator, AdminMapper, IFormFile, CreateAdminCommand, CancellationToken (+32 more)

### Community 30 - ".Handle"
Cohesion: 0.17
Nodes (11): EmailResponseDto, CancellationToken, ILogger, Task, ForgotPasswordCommandHandler, Task, IEmailService, IOptions (+3 more)

### Community 31 - ".Handle"
Cohesion: 0.14
Nodes (12): DateTime, Guid, AdminMatchDto, ExpertMatchDto, RecruiterMatchDto, AdminMatchMapper, GetLatestMatchesQuery, CancellationToken (+4 more)

### Community 32 - "AdminController"
Cohesion: 0.30
Nodes (11): Authorize, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IEnumerable, ISender (+3 more)

### Community 33 - "NotificationDto"
Cohesion: 0.10
Nodes (26): Guid, CreateNotificationDto, DateTime, Guid, NotificationDto, IEnumerable, List, NotificationMapper (+18 more)

### Community 34 - "Chat"
Cohesion: 0.19
Nodes (14): ICollection, Chat, ChatType, ChatUsers, IsActive, Messages, CancellationToken, Chat (+6 more)

### Community 35 - ".Handle"
Cohesion: 0.33
Nodes (5): INotificationHandler, CancellationToken, ILogger, Task, UserProfileChangedEventHandler

### Community 36 - "UserSkill"
Cohesion: 0.14
Nodes (14): CancellationToken, Guid, List, Task, Guid, UserSkill, Skill, SkillId (+6 more)

### Community 37 - "InterestRepository"
Cohesion: 0.20
Nodes (11): CancellationToken, Expression, Func, Task, CancellationToken, Guid, IEnumerable, Interest (+3 more)

### Community 38 - "Entity Framework Core Patterns"
Cohesion: 0.05
Nodes (38): 1. Forgetting to Update When NoTracking, 2. N+1 Query Problem, 3. Tracking Conflicts with Multiple DbContext Instances, 4. Not Using Async Consistently, 5. Querying Inside Loops, Actors / Long-Lived Objects (Factory Pattern), AppHost Configuration, Applying Migrations (+30 more)

### Community 39 - "ExceptionHandlingMiddleware"
Cohesion: 0.08
Nodes (18): Trivo.API.Middlewares, Trivo.API.Extensions, IApplicationBuilder, IEndpointRouteBuilder, IHostEnvironment, ProblemDetails, RequestDelegate, IConfiguration (+10 more)

### Community 40 - "NotificationHub"
Cohesion: 0.13
Nodes (17): HttpDelete, Authorize, CancellationToken, Guid, HttpPost, HttpPut, Task, NotificationController (+9 more)

### Community 41 - "Requerimientos Funcionales"
Cohesion: 0.12
Nodes (15): 4.3.1 Análisis Preliminar y Determinación de Requerimientos, 4.3.2 Análisis y Modelado de los Requerimientos del Proyecto, 4.3 Descripción del Modelo de Desarrollo, Requerimientos Funcionales, Requerimientos Funcionales (Sección Adicional), Requerimientos No Funcionales, RF1. Registro, RF2. Inicio de sesión (+7 more)

### Community 42 - ".AddServices"
Cohesion: 0.31
Nodes (5): JwtResponse, IConfiguration, IServiceCollection, IUserIdProvider, DependencyInjection

### Community 43 - "InterestCategoryDto"
Cohesion: 0.19
Nodes (9): Guid, InterestCategoryDto, CreateInterestCategoryCommand, ILogger, CreateInterestCategoryCommandHandler, CreateInterestCategoryCommandValidator, IEnumerable, List (+1 more)

### Community 44 - "Interest"
Cohesion: 0.13
Nodes (15): Guid, InterestByCategoryIdDto, Guid, IEnumerable, Interest, InterestMapper, Guid, ICollection (+7 more)

### Community 45 - "Trivo.API.csproj"
Cohesion: 0.11
Nodes (17): Asp.Versioning.Mvc (8.1.0), AspNetCore.HealthChecks.Redis (9.0.0), Microsoft.AspNetCore.OpenApi (8.0.10), Microsoft.AspNetCore.SignalR.Core (1.2.0), Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore (8.0.10), Scalar.AspNetCore (2.17.1), Serilog.Enrichers.Environment (3.0.1), Serilog.Enrichers.Thread (4.0.0) (+9 more)

### Community 46 - "ResultT"
Cohesion: 0.06
Nodes (37): INotification, CancellationToken, Task, CancellationToken, Task, CancellationToken, Task, CancellationToken (+29 more)

### Community 47 - "IInterestRepository"
Cohesion: 0.34
Nodes (6): CancellationToken, Guid, IEnumerable, List, Task, IInterestRepository

### Community 48 - "GetMatchByUserQuery"
Cohesion: 0.18
Nodes (10): Hub, Guid, IEnumerable, GetMatchByUserQuery, GetMatchByUserValidator, Exception, ILogger, IMediator (+2 more)

### Community 49 - "Public API Design and Compatibility"
Cohesion: 0.06
Nodes (31): Anti-Patterns, API Approval Testing, API Change Guidelines, Benefits, Breaking Changes Disguised as Fixes, Chesterton's Fence, Deprecation Pattern, Encapsulation Patterns (+23 more)

### Community 50 - "Skill"
Cohesion: 0.17
Nodes (10): DateTime, Guid, ICollection, Skill, Name, RegisteredAt, SkillId, UserSkills (+2 more)

### Community 51 - "IChatRepository"
Cohesion: 0.17
Nodes (13): CancellationToken, Task, CancellationToken, Task, CancellationToken, Task, CancellationToken, Guid (+5 more)

### Community 52 - ".GetByCategoriesAsync"
Cohesion: 0.23
Nodes (11): Authorize, CancellationToken, Guid, HttpGet, HttpPost, IEnumerable, ISender, List (+3 more)

### Community 53 - "IMatchRepository"
Cohesion: 0.32
Nodes (9): AcceptedIds, CancellationToken, Guid, IEnumerable, IReadOnlyList, Match, RejectedIds, Task (+1 more)

### Community 54 - "ExpertController.cs"
Cohesion: 0.14
Nodes (9): Trivo.Application.Features.Recruiters.Commands.UpdateRecruiter, Trivo.Application.DTOs.Expert, Trivo.Application.Features.Experts.Commands.CreateExpert, Trivo.Application.Features.Recruiters, Trivo.Application.Features.Experts, Trivo.Application.Helpers, Trivo.Application.DTOs.Recruiter, Trivo.Application.Features.Experts.Commands.UpdateExpert (+1 more)

### Community 55 - "GetMatchByUserQueryHandler"
Cohesion: 0.22
Nodes (9): CancellationToken, Dictionary, Func, Guid, IEnumerable, ILogger, Match, Task (+1 more)

### Community 56 - ".CreateSkillAsync"
Cohesion: 0.24
Nodes (9): Authorize, CancellationToken, HttpGet, HttpPost, IEnumerable, ISender, ProducesResponseType, Task (+1 more)

### Community 57 - "Error"
Cohesion: 0.15
Nodes (7): PaginationError, ErrorType, Error, Code, Description, ErrorType, StatusCode

### Community 58 - ".GetRolesAsync"
Cohesion: 0.14
Nodes (12): CancellationToken, Guid, IList, Task, Administrator, CancellationToken, Expert, Guid (+4 more)

### Community 59 - "Trivo.Infrastructure.Shared.csproj"
Cohesion: 0.15
Nodes (12): CloudinaryDotNet (1.27.0), MailKit (4.17.0), Microsoft.AspNetCore.Authentication.JwtBearer (8.0.10), Microsoft.AspNetCore.SignalR (1.2.0), Microsoft.Extensions.Options (10.0.3), Microsoft.Extensions.Options.ConfigurationExtensions (8.0.0), Microsoft.IdentityModel.Tokens (8.10.0), MimeKit (4.17.0) (+4 more)

### Community 60 - "ControllerBase"
Cohesion: 0.10
Nodes (17): ControllerBase, Authorize, CancellationToken, HttpPost, ISender, Task, ChatController, Authorize (+9 more)

### Community 61 - "ChatUser"
Cohesion: 0.15
Nodes (11): DateTime, Guid, ChatUser, Chat, ChatId, ChatName, JoinedAt, LeftAt (+3 more)

### Community 62 - "Database Performance Patterns"
Cohesion: 0.07
Nodes (26): Always Apply Row Limits, Architecture, AsNoTracking for Read Queries, Avoid Cartesian Explosions, Avoid N+1 Queries, Configure Default Behavior, Constrain Column Sizes, Core Principles (+18 more)

### Community 63 - "Documento de Requerimientos de Software"
Cohesion: 0.17
Nodes (11): 1. Búsqueda y Filtros, 2. Gestión de Usuarios, 3. Sistema de Reportes, 4. Reportes y Listados PDF, Cambios básicos en la información del usuario, Documento de Requerimientos de Software, Endpoints de Listados Requeridos, Filtros personalizados para buscar recomendaciones (+3 more)

### Community 64 - "TrivoContext"
Cohesion: 0.08
Nodes (23): DbContext, DbContextOptions, DbSet, CancellationToken, ModelBuilder, Task, TrivoContext, Administrators (+15 more)

### Community 65 - "ISkillRepository"
Cohesion: 0.26
Nodes (8): ILogger, GetSkillsPaginationQueryHandler, CancellationToken, Guid, IEnumerable, List, Task, ISkillRepository

### Community 66 - "UserInterest"
Cohesion: 0.13
Nodes (18): CancellationToken, Guid, List, Task, IUserInterestRepository, Guid, UserInterest, Interest (+10 more)

### Community 67 - "SkillDto"
Cohesion: 0.17
Nodes (9): DateTime, Guid, SkillDto, Guid, CreateSkillCommand, CreateSkillValidator, Guid, IEnumerable (+1 more)

### Community 68 - "Slopwatch: LLM Anti-Cheat for .NET"
Cohesion: 0.09
Nodes (22): After Every Code Change, As a Global Tool, As a Local Tool (Recommended), Azure Pipelines, CI/CD Integration, Claude Code Hook Integration, Common Slop Patterns, Configuration (+14 more)

### Community 69 - "UserAiRecommendationDto"
Cohesion: 0.06
Nodes (36): Candidates, HasOverlap, Guid, List, UserAiRecommendationDto, Guid, GetUserRecommendationsQuery, Distance (+28 more)

### Community 70 - "Validation"
Cohesion: 0.25
Nodes (6): CancellationToken, Expression, Func, Task, IValidation, Validation

### Community 71 - "Expert"
Cohesion: 0.14
Nodes (12): Guid, ExpertMapper, Guid, ICollection, Expert, AvailableForProjects, IsHired, Matches (+4 more)

### Community 72 - "Trivo.Application.DTOs.Administrator"
Cohesion: 0.15
Nodes (8): Trivo.Application.Features.Administrator.Query.GetCompletedMatchesCount, Trivo.Application.Features.Administrator.Query.GetLatestMatches, Trivo.Application.Features.Administrator.Commands.CreateAdministrator, Trivo.Application.Features.Administrator.Query.GetReportedUsersCount, Trivo.Application.Features.Administrator.Query.GetActiveUsersCount, Trivo.Application.DTOs.Administrator, Trivo.Application.Features.Administrator, Trivo.Application.Features.Administrator.Query.GetLastBannedUsers

### Community 73 - "ICacheService"
Cohesion: 0.10
Nodes (21): IDatabase, IReadOnlyList, CacheEntryOptions, AbsoluteExpiration, Tags, CacheProfiles, Cold, Hot (+13 more)

### Community 74 - "GoogleGeminiEmbeddingService"
Cohesion: 0.16
Nodes (14): ContentPart, EmbedContentResponse, EmbeddingValues, HttpClient, CancellationToken, ILogger, Task, ContentPart (+6 more)

### Community 75 - "Módulo de Matchmaking con IA — Implementación"
Cohesion: 0.14
Nodes (13): 1. Problema de negocio, 2.1 Abstracción del proveedor — `IEmbeddingService`, 2.2 Construcción del texto — `UserProfileTextBuilder`, 2.3 Cuándo se regenera el embedding — `UserProfileChangedEvent`, 2. Arquitectura, 3. Cambios de esquema (tablas), 4. El algoritmo de recomendación, 5. Qué NO se cachea, y por qué (+5 more)

### Community 76 - "Plan de implementación — Embeddings vía API externa para emparejamiento por afinidad"
Cohesion: 0.14
Nodes (13): 0. Decisión de proveedor y por qué la interfaz debe ser agnóstica, 10. Pruebas, 1. Infraestructura de base de datos (bloqueante, va primero), 2. Dominio (`Trivo.Domain`), 3. Application (`Trivo.Application`), 4. Infrastructure.Shared — implementación del proveedor, 5. Infrastructure.Persistence — columna, mapping e índice, 6. Cuándo se genera/regenera el embedding (+5 more)

### Community 77 - "Trivo.Application.DTOs.Authentication"
Cohesion: 0.12
Nodes (10): Trivo.Application.Features.Users.Commands.LoginUser, Trivo.Infrastructure.Shared.Services, Trivo.Domain.Configurations, Trivo.Application.DTOs.Authentication, Trivo.Infrastructure.Shared, Trivo.Application.Features.Administrator.Commands.LoginAdmin, Trivo.Application.Services, Trivo.Application (+2 more)

### Community 78 - "SkillRepository"
Cohesion: 0.29
Nodes (6): CancellationToken, Guid, IEnumerable, List, Task, SkillRepository

### Community 79 - "Nullable Attributes Reference"
Cohesion: 0.17
Nodes (12): Attribute Catalog, `[DoesNotReturn]`, `[DoesNotReturnIf(bool)]`, Helper methods: `MemberNotNull` and `MemberNotNullWhen`, `[MaybeNull]`, `[MemberNotNull]`, `[MemberNotNullWhen(bool)]`, `[NotNull]` (+4 more)

### Community 80 - "MatchRepository"
Cohesion: 0.34
Nodes (9): AcceptedIds, CancellationToken, Guid, IEnumerable, IReadOnlyList, Match, RejectedIds, Task (+1 more)

### Community 81 - "Modern C# Coding Standards"
Cohesion: 0.17
Nodes (12): Additional Resources, Avoid Reflection-Based Metaprogramming, Best Practices Summary, Code Organization, Composition Over Inheritance, Core Principles, DO's, DON'Ts (+4 more)

### Community 82 - ".Handle"
Cohesion: 0.20
Nodes (8): GetPaginatedInterestCategoriesQuery, CancellationToken, ILogger, Task, GetPaginatedInterestCategoriesQueryHandler, GetPaginatedInterestCategoriesValidator, ILogger, PaginationValidator

### Community 83 - ".MapToInterests"
Cohesion: 0.47
Nodes (4): ICollection, List, User, UserMapper

### Community 84 - ".Handle"
Cohesion: 0.21
Nodes (8): Guid, InterestDto, GetInterestsPaginationQuery, CancellationToken, ILogger, Task, GetInterestsPaginationQueryHandler, GetInterestsPaginationValidator

### Community 85 - "Plantillas copy-paste — feature CQRS de Trivo"
Cohesion: 0.18
Nodes (10): 1. DTO, 2. Command (con respuesta) — ejemplo real: `CreateInterestCommand`, 3. Validator (co-ubicado con el Command), 4. Handler — orquesta repos + UnitOfWork, nunca lanza excepciones de negocio, 5. Query con paginación + cache — ejemplo real: `GetInterestsPaginationQuery`, 6. Mapper — extensiones estáticas, una clase por feature, 7. Repositorio — patrón MANDATORIO para entidades nuevas (extiende `IGenericRepository<T>`), 8. DI — registrar el repo nuevo (+2 more)

### Community 86 - "ICloudinaryService"
Cohesion: 0.21
Nodes (10): CancellationToken, Stream, Task, ICloudinaryService, CancellationToken, IOptions, Stream, Task (+2 more)

### Community 87 - "SkillWithIdDto"
Cohesion: 0.12
Nodes (18): Guid, SkillWithIdDto, IEnumerable, SearchSkillsByNameQuery, CancellationToken, IEnumerable, ILogger, Task (+10 more)

### Community 88 - "Polyfilling the nullable attributes for older target frameworks"
Cohesion: 0.20
Nodes (10): Candidate packages (evaluate, do not default to one), Decision rules, File-level `#nullable` directives, Incremental Adoption Strategy, Is a polyfill needed at all?, Options and tradeoffs, Polyfilling the nullable attributes for older target frameworks, Project-level (+2 more)

### Community 89 - "IQuery"
Cohesion: 0.14
Nodes (16): IRequest, IRequestHandler, IQuery, IQueryHandler, ActiveUsersCountDto, ReportedUsersCountDto, GetActiveUsersCountQuery, CancellationToken (+8 more)

### Community 90 - "CreateMatchingCommandHandler"
Cohesion: 0.09
Nodes (23): Guid, CreateMatchingCommand, Dictionary, expertStatus, ILogger, recruiterStatus, CreateMatchingCommandHandler, Guid (+15 more)

### Community 91 - ".ValidateEmailAsync"
Cohesion: 0.22
Nodes (8): IServiceCollection, CancellationToken, Task, IEmailValidationService, CancellationToken, ILogger, Task, EmailValidationService

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
Cohesion: 0.18
Nodes (8): EmbeddingClient, CancellationToken, Task, IEmbeddingService, CancellationToken, ILogger, Task, OpenAiEmbeddingService

### Community 96 - "NRT Migration Playbook Reference"
Cohesion: 0.22
Nodes (6): Full Generation Checklist, Gradual annotation of a library, Legacy and Unannotated API Interop, NRT Migration Playbook Reference, Trust annotated libraries, Wrapping unannotated or legacy APIs

### Community 97 - ".Validation"
Cohesion: 0.06
Nodes (32): Guid, List, ExpertAiRecommendationDto, DateTime, Guid, MatchDto, Guid, List (+24 more)

### Community 98 - "UpdateInterestCommand"
Cohesion: 0.33
Nodes (5): Trivo.Application.Features.Interests.Commands.UpdateInterest, Guid, IReadOnlyList, UpdateInterestCommand, UpdateInterestValidator

### Community 99 - "Anti-Patterns to Avoid"
Cohesion: 0.25
Nodes (8): Anti-Patterns to Avoid, Don't: Block on async code, Don't: Create deep inheritance hierarchies, Don't: Forget CancellationToken in async methods, Don't: Return List<T> when you mean IReadOnlyList<T>, Don't: Use byte[] when ReadOnlySpan<byte> works, Don't: Use classes for value objects, Don't: Use mutable DTOs

### Community 100 - "AuthenticationService"
Cohesion: 0.06
Nodes (34): CancellationToken, HttpPost, ProducesResponseType, SwaggerOperation, Task, AuthController, RefreshTokenRequest, RefreshToken (+26 more)

### Community 101 - "Report"
Cohesion: 0.15
Nodes (11): Guid, Report, Message, MessageId, Note, ReportedById, ReportId, ReportStatus (+3 more)

### Community 102 - "UpdateProfilePictureCommand"
Cohesion: 0.33
Nodes (5): Trivo.Application.Features.Users.Commands.UpdateProfilePicture, Guid, IFormFile, UpdateProfilePictureCommand, UpdateProfilePictureValidator

### Community 103 - "UpdateSkillCommand"
Cohesion: 0.33
Nodes (5): Trivo.Application.Features.Skills.Commands.UpdateSkill, Guid, List, UpdateSkillCommand, UpdateSkillValidator

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

### Community 109 - "UpdateMatchingCommand"
Cohesion: 0.14
Nodes (15): Authorize, CancellationToken, HttpPost, HttpPut, ISender, Task, MatchController, Guid (+7 more)

### Community 110 - "Performance and API Design Patterns"
Cohesion: 0.29
Nodes (6): Accept Abstractions, Return Appropriately Specific, API Design Principles, Contents, Method Signatures Best Practices, Performance and API Design Patterns, Span<T> and Memory<T> for Zero-Allocation Code

### Community 111 - "Value Objects and Pattern Matching"
Cohesion: 0.29
Nodes (7): Constraint-Enforcing Value Objects, Contents, No Implicit Conversions, Pattern Matching (C# 8-12), TypeConverter Support for Configuration Binding, Value Objects and Pattern Matching, Value Objects as readonly record struct

### Community 113 - "PagedResult"
Cohesion: 0.17
Nodes (12): Guid, IEnumerable, GetInterestsByCategoryIdQuery, ILogger, GetInterestsByCategoryIdQueryHandler, GetInterestsByCategoryIdValidator, IEnumerable, PagedResult (+4 more)

### Community 114 - "ICommand"
Cohesion: 0.18
Nodes (9): Trivo.Application.Features.Users.Commands.ResetPassword, Trivo.Application.Features.Administrator.Commands.BanUser, IBaseCommand, ICommand, Guid, BanUserCommand, BanUserValidator, ResetPasswordCommand (+1 more)

### Community 115 - ".CreateInterestCategoryAsync"
Cohesion: 0.24
Nodes (8): Authorize, CancellationToken, HttpGet, HttpPost, ISender, ProducesResponseType, Task, InterestCategoryController

### Community 116 - ".Handle"
Cohesion: 0.22
Nodes (9): Guid, IFormFile, List, CreateUserCommand, CancellationToken, ILogger, IPublisher, Task (+1 more)

### Community 117 - "User"
Cohesion: 0.27
Nodes (5): IEnumerable, UserProfileTextBuilder, UserHelper, User, EntityTypeBuilder

### Community 118 - "CreateReportCommandHandler.cs"
Cohesion: 0.38
Nodes (3): Trivo.Application.Features.Reports.Commands.CreateReport, Trivo.Application.Features.Reports, Trivo.Application.DTOs.Reports

### Community 119 - ".GetExpertIdAsync"
Cohesion: 0.25
Nodes (6): CancellationToken, Guid, Task, CancellationToken, Guid, Task

### Community 120 - ".GetRecruiterIdAsync"
Cohesion: 0.25
Nodes (6): CancellationToken, Guid, Task, CancellationToken, Guid, Task

### Community 121 - "UnbanUserCommand"
Cohesion: 0.40
Nodes (4): Trivo.Application.Features.Administrator.Commands.UnbanUser, Guid, UnbanUserCommand, UnbanUserCommandValidator

### Community 122 - "EmailSetting"
Cohesion: 0.25
Nodes (7): EmailSetting, DisplayName, EmailFrom, SmtpHost, SmtpPassword, SmtpPort, SmtpUser

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

### Community 127 - "ChatDto"
Cohesion: 0.10
Nodes (21): DateTime, Guid, List, ChatDto, Guid, UserChatDto, Chat, Guid (+13 more)

### Community 128 - ".ToEntity"
Cohesion: 0.33
Nodes (4): Trivo.Application.Features.Administrator.Commands.CreateAdministrator.Mappings, Administrator, CreateAdminCommand, AdminMappingExtensions

### Community 129 - "UserMappingExtensions.cs"
Cohesion: 0.33
Nodes (4): Trivo.Application.Features.Users.Commands.CreateUser.Mappings, CreateUserCommand, User, UserMappingExtensions

### Community 130 - "IChatHub"
Cohesion: 0.33
Nodes (4): Guid, IEnumerable, Task, IChatHub

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
Cohesion: 0.40
Nodes (4): UserStatus, Active, Banned, Inactive

### Community 141 - "ChatType"
Cohesion: 0.50
Nodes (3): ChatType, Group, Private

### Community 142 - "MatchFault"
Cohesion: 0.50
Nodes (3): MatchFault, Expert, Recruiter

### Community 143 - ".Handle"
Cohesion: 0.31
Nodes (7): UserBiographyDto, Guid, GetUserBiographyQuery, CancellationToken, ILogger, Task, GetUserBiographyQueryHandler

### Community 144 - "ReportStatus"
Cohesion: 0.50
Nodes (3): ReportStatus, Pending, Resolved

### Community 145 - "IGenericRepository"
Cohesion: 0.32
Nodes (6): CancellationToken, Expression, Func, Guid, Task, IGenericRepository

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

### Community 150 - ".Handle"
Cohesion: 0.31
Nodes (7): UserProfilePictureDto, Guid, GetUserProfilePictureQuery, CancellationToken, ILogger, Task, GetUserProfilePictureQueryHandler

### Community 151 - ".Handle"
Cohesion: 0.28
Nodes (8): Guid, IEnumerable, GetUserInterestsQuery, CancellationToken, IEnumerable, ILogger, Task, GetUserInterestsQueryHandler

### Community 152 - "Known Static-Analysis Limitations and Safe Patterns"
Cohesion: 0.50
Nodes (4): Arrays and default values, Known Static-Analysis Limitations and Safe Patterns, Other limitations to keep in mind, Structs with non-nullable fields

### Community 153 - "Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`"
Cohesion: 0.50
Nodes (4): Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`, `[MaybeNullWhen(bool)]`, `[NotNullIfNotNull(string)]`, `[NotNullWhen(bool)]`

### Community 154 - "InterestCategoryRepository"
Cohesion: 0.44
Nodes (4): CancellationToken, Guid, Task, InterestCategoryRepository

### Community 155 - "Preconditions: `AllowNull` and `DisallowNull`"
Cohesion: 0.67
Nodes (3): `[AllowNull]`, `[DisallowNull]`, Preconditions: `AllowNull` and `DisallowNull`

### Community 156 - "Performance Patterns"
Cohesion: 0.67
Nodes (3): Async/Await Best Practices, Performance Patterns, Span<T> and Memory<T>

### Community 158 - "ChatHub"
Cohesion: 0.28
Nodes (6): Exception, Guid, ILogger, IMediator, Task, ChatHub

### Community 159 - "ExpertRepository"
Cohesion: 0.36
Nodes (7): CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task, ExpertRepository

### Community 160 - "CreateInterestCommandHandler"
Cohesion: 0.22
Nodes (7): Guid, InterestDetailsDto, Guid, CreateInterestCommand, ILogger, CreateInterestCommandHandler, CreateInterestValidator

### Community 161 - "ForgotPasswordCommand"
Cohesion: 0.50
Nodes (3): Trivo.Application.Features.Users.Commands.ForgotPassword, ForgotPasswordCommand, ForgotPasswordValidator

### Community 162 - ".Handle"
Cohesion: 0.11
Nodes (15): Guid, IEnumerable, CacheKeys, List, ExpertDetailsDto, List, RecruiterDetailsDto, List (+7 more)

### Community 163 - "SendImageCommand"
Cohesion: 0.25
Nodes (7): Guid, IFormFile, SendImageCommand, UserId, ILogger, SendImageCommandHandler, SendImageValidator

### Community 164 - "AiSetting"
Cohesion: 0.40
Nodes (4): AiSetting, ApiKey, EmbeddingModel, Provider

### Community 165 - "IExpertRepository"
Cohesion: 0.28
Nodes (9): ILogger, UpdateExpertCommandHandler, CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task (+1 more)

### Community 166 - "IMatchHub"
Cohesion: 0.43
Nodes (4): Guid, IEnumerable, Task, IMatchHub

### Community 167 - "MessageDto"
Cohesion: 0.10
Nodes (26): Authorize, CancellationToken, HttpPost, ISender, Task, MessageController, DateTime, Guid (+18 more)

### Community 168 - ".Handle"
Cohesion: 0.33
Nodes (4): GetSkillsPaginationQuery, CancellationToken, Task, GetSkillsPaginationValidator

### Community 169 - "UpdateUserCommand"
Cohesion: 0.40
Nodes (5): UpdateUserDto, Guid, UpdateUserCommand, ILogger, UpdateUserCommandHandler

### Community 170 - ".Handle"
Cohesion: 0.40
Nodes (5): ResendConfirmationCodeCommand, CancellationToken, ILogger, Task, ResendConfirmationCodeCommandHandler

### Community 172 - "ConfirmAccountCommand"
Cohesion: 0.40
Nodes (4): Trivo.Application.Features.Users.Commands.ConfirmAccount, Guid, ConfirmAccountCommand, ConfirmAccountValidator

### Community 173 - "ICommandHandler"
Cohesion: 0.09
Nodes (29): ICommandHandler, ILogger, BanUserCommandHandler, ILogger, AdminLoginCommandHandler, ILogger, UnbanUserCommandHandler, ILogger (+21 more)

### Community 174 - "InterestWithIdDto"
Cohesion: 0.21
Nodes (9): Guid, InterestWithIdDto, IEnumerable, SearchInterestsByNameQuery, CancellationToken, IEnumerable, ILogger, Task (+1 more)

### Community 175 - ".Handle"
Cohesion: 0.36
Nodes (6): CompletedMatchesCountDto, GetCompletedMatchesCountQuery, CancellationToken, ILogger, Task, GetCompletedMatchesCountQueryHandler

### Community 177 - "AbstractValidator"
Cohesion: 0.12
Nodes (9): AbstractValidator, CreateAdminValidator, CreateMatchValidator, UpdateMatchingValidation, SendMessageValidator, CreateUserValidator, LoginUserCommandValidator, ResendConfirmationCodeValidator (+1 more)

### Community 178 - "IUserOwnedRequest"
Cohesion: 0.14
Nodes (11): Trivo.Application.Features.Users.Commands.UpdatePassword, Trivo.Application.Features.Users.Commands.UpdateBiography, Guid, IUserOwnedRequest, UserId, Guid, UpdateBiographyCommand, UpdateBiographyValidator (+3 more)

## Knowledge Gaps
- **600 isolated node(s):** `$schema`, `windowsAuthentication`, `anonymousAuthentication`, `applicationUrl`, `sslPort` (+595 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **5 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `ResultT` connect `ResultT` to `UserController`, `ExpertDto`, `.Handle`, `.Handle`, `.Handle`, `Result`, `UserDto`, `Recruiter`, `IAdministratorRepository`, `.Handle`, `.Handle`, `AdminController`, `NotificationDto`, `.Handle`, `MessageDto`, `NotificationHub`, `.Handle`, `.Handle`, `ICommandHandler`, `InterestWithIdDto`, `.Handle`, `IChatRepository`, `.GetByCategoriesAsync`, `.CreateSkillAsync`, `Error`, `ControllerBase`, `.Handle`, `.Handle`, `SkillWithIdDto`, `IQuery`, `.ValidateEmailAsync`, `.Validation`, `AuthenticationService`, `UpdateMatchingCommand`, `ICommand`, `.CreateInterestCategoryAsync`, `.Handle`?**
  _High betweenness centrality (0.132) - this node is a cross-community bridge._
- **Why does `Trivo.Domain.Models` connect `Trivo.Domain.Models` to `Trivo.Application.Abstractions.Messages`, `Message`, `InterestCategory`, `Trivo.Application.DTOs.Users`, `Trivo.Application.Pagination`, `Trivo.Infrastructure.Persistence.Configurations`, `Trivo.Application.Interfaces.SignalR`, `Notification`, `Match`, `Code`, `Trivo.Domain.Enums`, `Recruiter`, `Chat`, `UserSkill`, `Skill`, `ExpertController.cs`, `ChatUser`, `UserInterest`, `Expert`, `Trivo.Application.DTOs.Administrator`, `Trivo.Application.DTOs.Authentication`, `Report`, `CreateReportCommandHandler.cs`?**
  _High betweenness centrality (0.048) - this node is a cross-community bridge._
- **Why does `Trivo.Domain.Enums` connect `Trivo.Domain.Enums` to `Trivo.Application.Abstractions.Messages`, `UserMappingExtensions.cs`, `ErrorType`, `Trivo.Application.DTOs.Users`, `Trivo.Domain.Models`, `ExpertStatus`, `Level`, `MatchStatus`, `MessageType`, `Trivo.Application.Interfaces.SignalR`, `NotificationType`, `RecruiterStatus`, `ChatType`, `MatchFault`, `UserStatus`, `ReportStatus`, `Result`, `Trivo.Application.DTOs.Authentication`, `CreateMatchingCommandHandler`, `UpdateMatchingCommand`, `MessageStatus`?**
  _High betweenness centrality (0.044) - this node is a cross-community bridge._
- **What connects `$schema`, `windowsAuthentication`, `anonymousAuthentication` to the rest of the system?**
  _600 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Trivo.Application.Abstractions.Messages` be split into smaller, more focused modules?**
  _Cohesion score 0.13060109289617486 - nodes in this community are weakly interconnected._
- **Should `Message` be split into smaller, more focused modules?**
  _Cohesion score 0.10810810810810811 - nodes in this community are weakly interconnected._
- **Should `Trivo.Application.DTOs.Users` be split into smaller, more focused modules?**
  _Cohesion score 0.0653061224489796 - nodes in this community are weakly interconnected._