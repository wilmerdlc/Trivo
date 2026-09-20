# Graph Report - Trivo  (2026-09-20)

## Corpus Check
- 427 files · ~78,845 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 3061 nodes · 6901 edges · 183 communities (159 shown, 24 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 419 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `1dd3d9f8`
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
- .Handle
- IUserRepository
- Trivo.Application.Interfaces.SignalR
- https
- UserController
- Trivo.Infrastructure.Persistence.Migrations
- Notification
- IRecruiterRepository
- BaseEntity
- .AddRepositories
- ChatDto
- Code
- BHD.ResultPattern — Guía de arquitectura e implementación
- MatchDto
- User
- Dependency Injection Patterns
- Trivo.Domain.Enums
- .SaveChangesAsync
- .Handle
- UserDto
- Recruiter
- AdministratorRepository
- .Handle
- PagedResult
- AdminController
- .NotFound
- Chat
- .Handle
- .AddSkillsToUserAsync
- InterestRepository
- Entity Framework Core Patterns
- ExceptionHandlingMiddleware
- ResultT
- Requerimientos Funcionales
- .AddAiService
- IAdministratorRepository
- Interest
- Trivo.API.csproj
- ICacheService
- IInterestRepository
- MatchHub
- Public API Design and Compatibility
- CacheKeys
- MessageDto
- .GetByCategoriesAsync
- IMatchRepository
- Trivo.API.Controllers.V1
- .Handle
- .Handle
- Error
- CreateMatchingCommandHandler
- Trivo.Infrastructure.Shared.csproj
- ControllerBase
- ChatUser
- Database Performance Patterns
- Documento de Requerimientos de Software
- TrivoContext
- .Handle
- UserInterest
- Skill
- Slopwatch: LLM Anti-Cheat for .NET
- UserAiRecommendationDto
- .Validate
- Expert
- Trivo.Application.DTOs.Administrator
- .GetOrSetAsync
- GoogleGeminiEmbeddingService
- Módulo de Matchmaking con IA — Implementación
- Plan de implementación — Embeddings vía API externa para emparejamiento por afinidad
- Trivo.Application.DTOs.Authentication
- Match
- Nullable Attributes Reference
- MatchRepository
- Modern C# Coding Standards
- Administrator
- .MapToInterests
- .Handle
- Plantillas copy-paste — feature CQRS de Trivo
- ICloudinaryService
- SkillWithIdDto
- Polyfilling the nullable attributes for older target frameworks
- GetReportedUsersCountQueryHandler
- CreateMatchRejectionCommand
- CreateInterestCategoryCommandHandler.cs
- .Handle
- Trivo.Application.csproj
- C# Nullable Reference Types
- OpenAiEmbeddingService
- NRT Migration Playbook Reference
- User
- Trivo.Application.Features.Interests.Commands.UpdateInterest
- Anti-Patterns to Avoid
- AuthenticationService
- Report
- Trivo.Application.Features.Users.Commands.UpdateProfilePicture
- Trivo.Application.Features.Skills.Commands.UpdateSkill
- ResultFilter
- Anti-Patterns and Reflection Avoidance
- .GetUserId
- Avoid Reflection-Based Metaprogramming
- Trivo
- MatchDetailsDto
- Performance and API Design Patterns
- Value Objects and Pattern Matching
- RF9-administrar-aplicacion.md
- GetInterestsByCategoryIdQuery
- BanUserValidator
- GetActiveUsersCountQueryHandler
- GenericRepository
- CreateAdminValidator
- CreateReportCommandHandler.cs
- .GetExpertIdAsync
- .GetRecruiterIdAsync
- UnbanUserCommandValidator
- EmailSetting
- Trivo.Infrastructure.Persistence.csproj
- The `field` Keyword (C# 14 / .NET 10) and Nullability
- MessageStatus
- Trivo.Domain.csproj
- CreateChatValidator
- .ToEntity
- .ToEntity
- CreateMatchValidator
- ErrorType
- NotificationType
- .AddServices
- ExpertStatus
- Level
- MatchStatus
- MessageType
- RecruiterStatus
- UserStatus
- PaginationExtensions.cs
- ChatType
- MatchFault
- JwtSetting
- ReportStatus
- IGenericRepository
- Core Nullability Model
- Arquitectura CQRS de Trivo — mandato para nuevas features
- Composition and Error Handling
- Language Patterns
- CacheProfiles
- UserSkill
- Known Static-Analysis Limitations and Safe Patterns
- Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`
- CreateExpertValidator
- Preconditions: `AllowNull` and `DisallowNull`
- Performance Patterns
- CLAUDE.md
- UpdateExpertValidator
- ExpertRepository
- CreateInterestValidator
- ForgotPasswordCommand
- .Handle
- UpdateUserValidator
- AiSetting
- IExpertRepository
- UpdateMatchingValidation
- SendFileValidator
- UpdateRecruiterValidator
- NotificationHub
- CreateReportValidator
- CreateSkillValidator
- ConfirmAccountValidator
- ICommand
- InterestWithIdDto
- IQuery
- CreateUserValidator
- AbstractValidator
- UpdateBiographyValidator
- UpdatePasswordValidator
- MissingByMatching

## God Nodes (most connected - your core abstractions)
1. `ResultT` - 138 edges
2. `Trivo.Application.Abstractions.Messages` - 110 edges
3. `Trivo.Domain.Models` - 92 edges
4. `Trivo.Application.Utils` - 87 edges
5. `Trivo.Application.Interfaces.Services` - 70 edges
6. `PagedResult` - 70 edges
7. `TrivoContext` - 61 edges
8. `IUserRepository` - 58 edges
9. `Trivo.Application.Pagination` - 57 edges
10. `Trivo.Application.Interfaces.Repository.Account` - 54 edges

## Surprising Connections (you probably didn't know these)
- `NotificationController` --references--> `INotificationService`  [EXTRACTED]
  src/API/Trivo.API/Controllers/V1/NotificationController.cs → src/Application/Trivo.Application/Interfaces/Services/INotificationService.cs
- `UserController` --references--> `ICodeService`  [EXTRACTED]
  src/API/Trivo.API/Controllers/V1/UserController.cs → src/Application/Trivo.Application/Interfaces/Services/ICodeService.cs
- `ICommand` --references--> `Result`  [EXTRACTED]
  src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs → src/Application/Trivo.Application/Utils/Result.cs
- `ICommand` --references--> `ResultT`  [EXTRACTED]
  src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs → src/Application/Trivo.Application/Utils/Result.cs
- `BanUserCommand` --implements--> `ICommand`  [EXTRACTED]
  src/Application/Trivo.Application/Features/Administrator/Commands/BanUser/BanUserCommand.cs → src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs

## Import Cycles
- None detected.

## Communities (183 total, 24 thin omitted)

### Community 0 - "UserRepository"
Cohesion: 0.18
Nodes (14): Expression, Func, CancellationToken, Distance, Expert, Guid, IEnumerable, IReadOnlyList (+6 more)

### Community 1 - "Trivo.Application.Abstractions.Messages"
Cohesion: 0.13
Nodes (9): Trivo.Application.Abstractions.Messages, Trivo.Application.Interfaces.UnitOfWork, Trivo.Application.DTOs.Email, Trivo.Application.Utils, Trivo.Application.Caching, Trivo.Application.Features.Users.Events, Trivo.Application.Interfaces.Repository.Account, Trivo.Application.Services (+1 more)

### Community 2 - "Message"
Cohesion: 0.06
Nodes (37): DateTime, Guid, MessageReportDto, Guid, ReportDto, Guid, UserReportDto, ReportMapper (+29 more)

### Community 3 - "InterestCategory"
Cohesion: 0.06
Nodes (35): Authorize, CancellationToken, HttpGet, HttpPost, ISender, ProducesResponseType, Task, InterestCategoryController (+27 more)

### Community 4 - "Trivo.Application.DTOs.Users"
Cohesion: 0.07
Nodes (19): Trivo.Application.Features.Users.Commands.UpdatePassword, Trivo.Application.Features.Users.Commands.CreateUser, Trivo.Application.Features.Users.Commands.UpdateBiography, Trivo.Application.Features.Interests.Commands.CreateInterest, Trivo.Application.Features.Users.Query.GetUserDetails, Trivo.Application.Features.Skills, Trivo.Application.Features.Users.Commands.UpdateUser, Trivo.Application.Features.Users.Commands.ResetPassword (+11 more)

### Community 5 - "Trivo.Domain.Models"
Cohesion: 0.09
Nodes (13): Trivo.Domain.Common, Trivo.Infrastructure.Persistence.Context, Trivo.Infrastructure.Persistence.Repository.Account, Trivo.Application.Interfaces.Repository.Base, Trivo.Infrastructure.Persistence.Services, Trivo.Infrastructure.Persistence.Repository, Trivo.Domain.Models, Trivo.Application.Interfaces.Repository (+5 more)

### Community 6 - "Trivo.Application.Pagination"
Cohesion: 0.09
Nodes (10): Trivo.Application.Features.Messages.Query.GetMessagePagination, Trivo.Application.Features.Skills.Query.GetSkillsPagination, Trivo.Application.Features.Administrator.Query.GetLatestUsersPaged, Trivo.Application.Features.Interests.Query.GetInterestsByCategoryId, Trivo.Application.Features.Matching, Trivo.Application.Pagination, Trivo.Application.Features.Users.Query.GetUsersByInterestsAndSkills, Trivo.Application.Features.Interests.Query.GetInterestsPagination (+2 more)

### Community 7 - "Trivo.Infrastructure.Persistence.Configurations"
Cohesion: 0.12
Nodes (10): Trivo.Infrastructure.Persistence.Configurations, IEntityTypeConfiguration, EntityTypeBuilder, AdministratorConfig, EntityTypeBuilder, ChatConfig, EntityTypeBuilder, InterestConfig (+2 more)

### Community 8 - ".Handle"
Cohesion: 0.19
Nodes (11): DateTime, Guid, AdminDto, Administrator, AdminMapper, IFormFile, CreateAdminCommand, CancellationToken (+3 more)

### Community 9 - "IUserRepository"
Cohesion: 0.22
Nodes (10): CancellationToken, Distance, Guid, IEnumerable, IReadOnlyList, List, Task, User (+2 more)

### Community 10 - "Trivo.Application.Interfaces.SignalR"
Cohesion: 0.10
Nodes (10): Trivo.Application.Features.Messages.Commands.SendImage, Trivo.Application.Features.Chat.Query.GetChatPagination, Trivo.Application.Features.Chat, Trivo.Application.Interfaces.SignalR, Trivo.Application.Features.Chat.Commands.CreateChat, Trivo.Infrastructure.Shared.SignalR, Trivo.Application.Features.Messages.Commands.SendMessage, Trivo.Application.DTOs.Chat (+2 more)

### Community 11 - "https"
Cohesion: 0.10
Nodes (21): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, launchUrl, commandName (+13 more)

### Community 12 - "UserController"
Cohesion: 0.05
Nodes (49): Trivo.API.Controllers.V1.Requests, ChangePasswordRequest, Guid, List, FilterUsersByInterestsAndSkillsRequest, UpdateBiographyRequest, UpdatePasswordRequest, IFormFile (+41 more)

### Community 13 - "Trivo.Infrastructure.Persistence.Migrations"
Cohesion: 0.05
Nodes (29): Trivo.Infrastructure.Persistence.Migrations, Migration, ModelSnapshot, DateTime, Guid, MigrationBuilder, Vector, DateTime (+21 more)

### Community 14 - "Notification"
Cohesion: 0.11
Nodes (21): CancellationToken, Guid, Task, INotificationRepository, DateTime, Guid, Notification, Content (+13 more)

### Community 15 - "IRecruiterRepository"
Cohesion: 0.08
Nodes (29): Guid, ExpertDto, Guid, RecruiterDto, Guid, CreateExpertCommand, CancellationToken, ILogger (+21 more)

### Community 16 - "BaseEntity"
Cohesion: 0.13
Nodes (13): DateTime, Guid, BaseEntity, CreatedAt, Id, UpdatedAt, Guid, ICollection (+5 more)

### Community 17 - ".AddRepositories"
Cohesion: 0.20
Nodes (12): IReportRepository, IGetExpertIdService, IGetRecruiterIdService, IUserRoleService, IConfiguration, IConnectionMultiplexer, IServiceCollection, DependencyInjection (+4 more)

### Community 18 - "ChatDto"
Cohesion: 0.06
Nodes (34): DateTime, Guid, List, ChatDto, Guid, UserChatDto, Chat, Guid (+26 more)

### Community 19 - "Code"
Cohesion: 0.10
Nodes (23): CancellationToken, Guid, Task, ICodeRepository, DateTime, Guid, Code, CodeId (+15 more)

### Community 20 - "BHD.ResultPattern — Guía de arquitectura e implementación"
Cohesion: 0.06
Nodes (31): 1. Arquitectura general, 2.1. Estructura de carpetas, 2.2. `BHD.ResultPattern.csproj`, 2.3. `Result.cs`, 2.4. `Error.cs`, 2. Núcleo: `BHD.ResultPattern` (sin dependencias), 3.1. Estructura de carpetas, 3.2. `BHD.ResultPattern.AspNetCore.csproj` (+23 more)

### Community 21 - "MatchDto"
Cohesion: 0.20
Nodes (12): DateTime, Guid, MatchDto, Guid, IEnumerable, Task, IMatchNotifier, Guid (+4 more)

### Community 22 - "User"
Cohesion: 0.07
Nodes (27): ICollection, Vector, User, Biography, ChatUsers, Codes, Email, Experts (+19 more)

### Community 23 - "Dependency Injection Patterns"
Cohesion: 0.05
Nodes (38): Advanced DI Patterns, Akka.DependencyInjection Reference, Akka.Hosting.TestKit, Akka.NET Actor Scope Management, Common Patterns, Conditional Registration, Contents, Factory-Based Registration (+30 more)

### Community 24 - "Trivo.Domain.Enums"
Cohesion: 0.12
Nodes (9): Trivo.API.Filters, Trivo.Application.Features.Matching.Commands.UpdateMatch, Trivo.Application.Features.Users, Trivo.Application.Features.Matching.Commands.CreateMatch, Trivo.Application.Features.Users.Commands.CreateUser.Mappings, Trivo.Domain.Enums, Trivo.Application.Features.Matching.Commands.CreateMatchRejection, Trivo.Application.Features.Matching.Query.GetMatchByUser (+1 more)

### Community 25 - ".SaveChangesAsync"
Cohesion: 0.11
Nodes (22): DateTime, Guid, CodeDto, CancellationToken, Guid, Task, ICodeService, CancellationToken (+14 more)

### Community 26 - ".Handle"
Cohesion: 0.14
Nodes (15): CreateInterestCategoryCommand, CancellationToken, ILogger, Task, CreateInterestCategoryCommandHandler, Guid, CreateInterestCommand, CancellationToken (+7 more)

### Community 27 - "UserDto"
Cohesion: 0.15
Nodes (15): Guid, UserDto, IEnumerable, GetLast10BannedUsersQuery, CancellationToken, IEnumerable, ILogger, Task (+7 more)

### Community 28 - "Recruiter"
Cohesion: 0.14
Nodes (18): Guid, RecruiterMapper, CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task (+10 more)

### Community 29 - "AdministratorRepository"
Cohesion: 0.23
Nodes (8): Administrator, CancellationToken, Guid, IEnumerable, Match, Task, User, AdministratorRepository

### Community 30 - ".Handle"
Cohesion: 0.18
Nodes (6): EmailResponseDto, CancellationToken, Task, Task, EmailTemplate, Task

### Community 31 - "PagedResult"
Cohesion: 0.10
Nodes (18): DateTime, Guid, AdminMatchDto, ExpertMatchDto, RecruiterMatchDto, AdminMatchMapper, GetLatestMatchesQuery, CancellationToken (+10 more)

### Community 32 - "AdminController"
Cohesion: 0.30
Nodes (11): Authorize, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IEnumerable, ISender (+3 more)

### Community 33 - ".NotFound"
Cohesion: 0.11
Nodes (26): Guid, CreateNotificationDto, DateTime, Guid, NotificationDto, IEnumerable, List, NotificationMapper (+18 more)

### Community 34 - "Chat"
Cohesion: 0.19
Nodes (14): ICollection, Chat, ChatType, ChatUsers, IsActive, Messages, CancellationToken, Chat (+6 more)

### Community 35 - ".Handle"
Cohesion: 0.13
Nodes (13): INotification, INotificationHandler, Guid, UserProfileChangedEvent, CancellationToken, ILogger, Task, UserProfileChangedEventHandler (+5 more)

### Community 36 - ".AddSkillsToUserAsync"
Cohesion: 0.21
Nodes (10): CancellationToken, Guid, List, Task, IUserSkillRepository, CancellationToken, Guid, List (+2 more)

### Community 37 - "InterestRepository"
Cohesion: 0.24
Nodes (7): CancellationToken, Guid, IEnumerable, Interest, List, Task, InterestRepository

### Community 38 - "Entity Framework Core Patterns"
Cohesion: 0.05
Nodes (38): 1. Forgetting to Update When NoTracking, 2. N+1 Query Problem, 3. Tracking Conflicts with Multiple DbContext Instances, 4. Not Using Async Consistently, 5. Querying Inside Loops, Actors / Long-Lived Objects (Factory Pattern), AppHost Configuration, Applying Migrations (+30 more)

### Community 39 - "ExceptionHandlingMiddleware"
Cohesion: 0.08
Nodes (18): Trivo.API.Middlewares, Trivo.API.Extensions, IApplicationBuilder, IEndpointRouteBuilder, IHostEnvironment, ProblemDetails, RequestDelegate, IConfiguration (+10 more)

### Community 40 - "ResultT"
Cohesion: 0.11
Nodes (18): HttpDelete, Authorize, CancellationToken, HttpPost, ISender, Task, MessageController, Authorize (+10 more)

### Community 41 - "Requerimientos Funcionales"
Cohesion: 0.12
Nodes (15): 4.3.1 Análisis Preliminar y Determinación de Requerimientos, 4.3.2 Análisis y Modelado de los Requerimientos del Proyecto, 4.3 Descripción del Modelo de Desarrollo, Requerimientos Funcionales, Requerimientos Funcionales (Sección Adicional), Requerimientos No Funcionales, RF1. Registro, RF2. Inicio de sesión (+7 more)

### Community 42 - ".AddAiService"
Cohesion: 0.26
Nodes (6): JwtResponse, CloudinarySetting, CloudinaryUrl, IConfiguration, IServiceCollection, DependencyInjection

### Community 43 - "IAdministratorRepository"
Cohesion: 0.26
Nodes (5): CancellationToken, Guid, IEnumerable, Task, IAdministratorRepository

### Community 44 - "Interest"
Cohesion: 0.13
Nodes (13): Guid, IEnumerable, Interest, InterestMapper, Guid, ICollection, Interest, Category (+5 more)

### Community 45 - "Trivo.API.csproj"
Cohesion: 0.11
Nodes (17): Asp.Versioning.Mvc (8.1.0), AspNetCore.HealthChecks.Redis (9.0.0), Microsoft.AspNetCore.OpenApi (8.0.10), Microsoft.AspNetCore.SignalR.Core (1.2.0), Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore (8.0.10), Scalar.AspNetCore (2.17.1), Serilog.Enrichers.Environment (3.0.1), Serilog.Enrichers.Thread (4.0.0) (+9 more)

### Community 46 - "ICacheService"
Cohesion: 0.04
Nodes (54): UpdateUserDto, Guid, BanUserCommand, CancellationToken, ILogger, Task, BanUserCommandHandler, Guid (+46 more)

### Community 47 - "IInterestRepository"
Cohesion: 0.22
Nodes (10): CancellationToken, ILogger, Task, GetInterestsByCategoryIdQueryHandler, CancellationToken, Guid, IEnumerable, List (+2 more)

### Community 48 - "MatchHub"
Cohesion: 0.17
Nodes (10): Hub, Guid, IEnumerable, Task, IMatchHub, Exception, ILogger, IMediator (+2 more)

### Community 49 - "Public API Design and Compatibility"
Cohesion: 0.06
Nodes (31): Anti-Patterns, API Approval Testing, API Change Guidelines, Benefits, Breaking Changes Disguised as Fixes, Chesterton's Fence, Deprecation Pattern, Encapsulation Patterns (+23 more)

### Community 50 - "CacheKeys"
Cohesion: 0.27
Nodes (3): Guid, IEnumerable, CacheKeys

### Community 51 - "MessageDto"
Cohesion: 0.06
Nodes (43): Guid, IUserOwnedRequest, UserId, DateTime, Guid, MessageDto, Guid, IFormFile (+35 more)

### Community 52 - ".GetByCategoriesAsync"
Cohesion: 0.18
Nodes (13): Authorize, CancellationToken, Guid, HttpGet, HttpPost, IEnumerable, ISender, List (+5 more)

### Community 53 - "IMatchRepository"
Cohesion: 0.32
Nodes (9): AcceptedIds, CancellationToken, Guid, IEnumerable, IReadOnlyList, Match, RejectedIds, Task (+1 more)

### Community 54 - "Trivo.API.Controllers.V1"
Cohesion: 0.09
Nodes (12): Trivo.Application.Features.Recruiters.Commands.UpdateRecruiter, Trivo.API.Controllers.V1, Trivo.Application.DTOs.Notifications, Trivo.Application.DTOs.Expert, Trivo.Application.Features.Experts.Commands.CreateExpert, Trivo.Application.Features.Recruiters, Trivo.Application.Features.Experts, Trivo.Application.Features.Notifications (+4 more)

### Community 55 - ".Handle"
Cohesion: 0.19
Nodes (13): Guid, IEnumerable, GetMatchByUserQuery, CancellationToken, Dictionary, Func, Guid, IEnumerable (+5 more)

### Community 56 - ".Handle"
Cohesion: 0.18
Nodes (14): Candidates, HasOverlap, Guid, GetUserRecommendationsQuery, CancellationToken, Distance, Guid, ILogger (+6 more)

### Community 57 - "Error"
Cohesion: 0.15
Nodes (7): PaginationError, ErrorType, Error, Code, Description, ErrorType, StatusCode

### Community 58 - "CreateMatchingCommandHandler"
Cohesion: 0.08
Nodes (24): Guid, CreateMatchingCommand, Dictionary, expertStatus, ILogger, recruiterStatus, CreateMatchingCommandHandler, CancellationToken (+16 more)

### Community 59 - "Trivo.Infrastructure.Shared.csproj"
Cohesion: 0.15
Nodes (12): CloudinaryDotNet (1.27.0), MailKit (4.17.0), Microsoft.AspNetCore.Authentication.JwtBearer (8.0.10), Microsoft.AspNetCore.SignalR (1.2.0), Microsoft.Extensions.Options (10.0.3), Microsoft.Extensions.Options.ConfigurationExtensions (8.0.0), Microsoft.IdentityModel.Tokens (8.10.0), MimeKit (4.17.0) (+4 more)

### Community 60 - "ControllerBase"
Cohesion: 0.05
Nodes (34): ControllerBase, Authorize, CancellationToken, HttpPost, ISender, Task, ChatController, Authorize (+26 more)

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

### Community 65 - ".Handle"
Cohesion: 0.25
Nodes (7): CancellationToken, Dictionary, expertStatus, ILogger, recruiterStatus, Task, CreateMatchRejectionCommandHandler

### Community 66 - "UserInterest"
Cohesion: 0.09
Nodes (27): Guid, IFormFile, List, CreateUserCommand, CancellationToken, ILogger, IPublisher, Task (+19 more)

### Community 67 - "Skill"
Cohesion: 0.05
Nodes (45): Authorize, CancellationToken, HttpGet, HttpPost, IEnumerable, ISender, ProducesResponseType, Task (+37 more)

### Community 68 - "Slopwatch: LLM Anti-Cheat for .NET"
Cohesion: 0.09
Nodes (22): After Every Code Change, As a Global Tool, As a Local Tool (Recommended), Azure Pipelines, CI/CD Integration, Claude Code Hook Integration, Common Slop Patterns, Configuration (+14 more)

### Community 69 - "UserAiRecommendationDto"
Cohesion: 0.11
Nodes (20): Guid, List, UserAiRecommendationDto, Guid, IEnumerable, Task, IAiNotifier, IEnumerable (+12 more)

### Community 70 - ".Validate"
Cohesion: 0.15
Nodes (10): CancellationToken, Expression, Func, Task, IValidation, CancellationToken, Expression, Func (+2 more)

### Community 71 - "Expert"
Cohesion: 0.14
Nodes (12): Guid, ExpertMapper, Guid, ICollection, Expert, AvailableForProjects, IsHired, Matches (+4 more)

### Community 72 - "Trivo.Application.DTOs.Administrator"
Cohesion: 0.11
Nodes (10): Trivo.Application.Features.Administrator.Query.GetCompletedMatchesCount, Trivo.Application.Features.Administrator.Query.GetLatestMatches, Trivo.Application.Features.Administrator.Commands.CreateAdministrator, Trivo.Application.Features.Administrator.Query.GetReportedUsersCount, Trivo.Application.Features.Administrator.Query.GetActiveUsersCount, Trivo.Application.DTOs.Administrator, Trivo.Application.Features.Administrator.Commands.UnbanUser, Trivo.Application.Features.Administrator.Commands.BanUser (+2 more)

### Community 73 - ".GetOrSetAsync"
Cohesion: 0.13
Nodes (16): IDatabase, IReadOnlyList, CacheEntryOptions, AbsoluteExpiration, Tags, CancellationToken, Func, Task (+8 more)

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
Cohesion: 0.21
Nodes (5): Trivo.Application.Features.Users.Commands.LoginUser, Trivo.Infrastructure.Shared.Services, Trivo.Domain.Configurations, Trivo.Application.DTOs.Authentication, Trivo.Application.Features.Administrator.Commands.LoginAdmin

### Community 78 - "Match"
Cohesion: 0.18
Nodes (10): Guid, Match, Expert, ExpertId, ExpertStatus, MatchStatus, RecruiterId, RecruiterStatus (+2 more)

### Community 79 - "Nullable Attributes Reference"
Cohesion: 0.17
Nodes (12): Attribute Catalog, `[DoesNotReturn]`, `[DoesNotReturnIf(bool)]`, Helper methods: `MemberNotNull` and `MemberNotNullWhen`, `[MaybeNull]`, `[MemberNotNull]`, `[MemberNotNullWhen(bool)]`, `[NotNull]` (+4 more)

### Community 80 - "MatchRepository"
Cohesion: 0.34
Nodes (9): AcceptedIds, CancellationToken, Guid, IEnumerable, IReadOnlyList, Match, RejectedIds, Task (+1 more)

### Community 81 - "Modern C# Coding Standards"
Cohesion: 0.17
Nodes (12): Additional Resources, Avoid Reflection-Based Metaprogramming, Best Practices Summary, Code Organization, Composition Over Inheritance, Core Principles, DO's, DON'Ts (+4 more)

### Community 82 - "Administrator"
Cohesion: 0.20
Nodes (10): Administrator, Biography, Email, FirstName, IsActive, LastName, LinkedIn, PasswordHash (+2 more)

### Community 83 - ".MapToInterests"
Cohesion: 0.47
Nodes (4): ICollection, List, User, UserMapper

### Community 84 - ".Handle"
Cohesion: 0.24
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

### Community 89 - "GetReportedUsersCountQueryHandler"
Cohesion: 0.36
Nodes (6): ReportedUsersCountDto, GetReportedUsersCountQuery, CancellationToken, ILogger, Task, GetReportedUsersCountQueryHandler

### Community 90 - "CreateMatchRejectionCommand"
Cohesion: 0.29
Nodes (6): Guid, CreateMatchRejectionCommand, CreatedBy, ExpertId, RecruiterId, CreateMatchRejectionValidator

### Community 91 - "CreateInterestCategoryCommandHandler.cs"
Cohesion: 0.27
Nodes (4): Trivo.Application.DTOs.InterestCategories, Trivo.Application.Features.InterestCategories.Commands.CreateInterestCategory, Trivo.Application.Features.InterestCategories.Query.GetPaginatedInterestCategories, Trivo.Application.Features.InterestCategories

### Community 92 - ".Handle"
Cohesion: 0.12
Nodes (13): Trivo.Application.Behaviors, IHttpContextAccessor, IPipelineBehavior, IValidator, CancellationToken, RequestHandlerDelegate, Task, AuthorizationBehavior (+5 more)

### Community 93 - "Trivo.Application.csproj"
Cohesion: 0.20
Nodes (9): BCrypt.Net-Next (4.0.3), FluentValidation (11.10.0), FluentValidation.DependencyInjectionExtensions (11.10.0), Microsoft.Extensions.DependencyInjection.Abstractions (8.0.2), net8.0, MediatR (12.2.0), Microsoft.Extensions.Caching.StackExchangeRedis (8.0.10), Serilog.AspNetCore (8.0.0) (+1 more)

### Community 94 - "C# Nullable Reference Types"
Cohesion: 0.20
Nodes (10): API Design Rules (Signatures), C# Nullable Reference Types, Core Goals, Generation Checklist (Summary), Project Configuration, Public API compatibility for libraries, Reference Files, References (+2 more)

### Community 95 - "OpenAiEmbeddingService"
Cohesion: 0.33
Nodes (5): EmbeddingClient, CancellationToken, ILogger, Task, OpenAiEmbeddingService

### Community 96 - "NRT Migration Playbook Reference"
Cohesion: 0.22
Nodes (6): Full Generation Checklist, Gradual annotation of a library, Legacy and Unannotated API Interop, NRT Migration Playbook Reference, Trust annotated libraries, Wrapping unannotated or legacy APIs

### Community 97 - "User"
Cohesion: 0.15
Nodes (11): Guid, List, ExpertAiRecommendationDto, Guid, List, RecruiterAiRecommendationDto, MatchMapper, UserHelper (+3 more)

### Community 99 - "Anti-Patterns to Avoid"
Cohesion: 0.25
Nodes (8): Anti-Patterns to Avoid, Don't: Block on async code, Don't: Create deep inheritance hierarchies, Don't: Forget CancellationToken in async methods, Don't: Return List<T> when you mean IReadOnlyList<T>, Don't: Use byte[] when ReadOnlySpan<byte> works, Don't: Use classes for value objects, Don't: Use mutable DTOs

### Community 100 - "AuthenticationService"
Cohesion: 0.07
Nodes (32): CancellationToken, HttpPost, ProducesResponseType, SwaggerOperation, Task, AuthController, RefreshTokenRequest, RefreshToken (+24 more)

### Community 101 - "Report"
Cohesion: 0.15
Nodes (11): Guid, Report, Message, MessageId, Note, ReportedById, ReportId, ReportStatus (+3 more)

### Community 104 - "ResultFilter"
Cohesion: 0.29
Nodes (6): ActionExecutingContext, ActionExecutionDelegate, IAsyncActionFilter, ILogger, Task, ResultFilter

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

### Community 109 - "MatchDetailsDto"
Cohesion: 0.12
Nodes (20): Authorize, CancellationToken, HttpPost, HttpPut, ISender, Task, MatchController, DateTime (+12 more)

### Community 110 - "Performance and API Design Patterns"
Cohesion: 0.29
Nodes (6): Accept Abstractions, Return Appropriately Specific, API Design Principles, Contents, Method Signatures Best Practices, Performance and API Design Patterns, Span<T> and Memory<T> for Zero-Allocation Code

### Community 111 - "Value Objects and Pattern Matching"
Cohesion: 0.29
Nodes (7): Constraint-Enforcing Value Objects, Contents, No Implicit Conversions, Pattern Matching (C# 8-12), TypeConverter Support for Configuration Binding, Value Objects and Pattern Matching, Value Objects as readonly record struct

### Community 113 - "GetInterestsByCategoryIdQuery"
Cohesion: 0.29
Nodes (6): Guid, InterestByCategoryIdDto, Guid, IEnumerable, GetInterestsByCategoryIdQuery, GetInterestsByCategoryIdValidator

### Community 115 - "GetActiveUsersCountQueryHandler"
Cohesion: 0.36
Nodes (6): ActiveUsersCountDto, GetActiveUsersCountQuery, CancellationToken, ILogger, Task, GetActiveUsersCountQueryHandler

### Community 116 - "GenericRepository"
Cohesion: 0.44
Nodes (4): CancellationToken, Guid, Task, GenericRepository

### Community 118 - "CreateReportCommandHandler.cs"
Cohesion: 0.38
Nodes (3): Trivo.Application.Features.Reports.Commands.CreateReport, Trivo.Application.Features.Reports, Trivo.Application.DTOs.Reports

### Community 119 - ".GetExpertIdAsync"
Cohesion: 0.25
Nodes (6): CancellationToken, Guid, Task, CancellationToken, Guid, Task

### Community 120 - ".GetRecruiterIdAsync"
Cohesion: 0.25
Nodes (6): CancellationToken, Guid, Task, CancellationToken, Guid, Task

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

### Community 128 - ".ToEntity"
Cohesion: 0.33
Nodes (4): Trivo.Application.Features.Administrator.Commands.CreateAdministrator.Mappings, Administrator, CreateAdminCommand, AdminMappingExtensions

### Community 129 - ".ToEntity"
Cohesion: 0.50
Nodes (3): CreateUserCommand, User, UserMappingExtensions

### Community 131 - "ErrorType"
Cohesion: 0.15
Nodes (12): ErrorType, Conflict, Custom, ExternalService, Forbidden, NotFound, Timeout, TooManyRequests (+4 more)

### Community 132 - "NotificationType"
Cohesion: 0.33
Nodes (5): NotificationType, Alert, Match, Message, Reminder

### Community 133 - ".AddServices"
Cohesion: 0.17
Nodes (10): HubConnectionContext, IUserIdProvider, ILogger, ForgotPasswordCommandHandler, IEmailService, IUserIdProvider, IOptions, EmailService (+2 more)

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

### Community 143 - "JwtSetting"
Cohesion: 0.33
Nodes (5): JwtSetting, Audience, DurationInMinutes, Issuer, Key

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

### Community 150 - "CacheProfiles"
Cohesion: 0.40
Nodes (4): CacheProfiles, Cold, Hot, Warm

### Community 151 - "UserSkill"
Cohesion: 0.29
Nodes (6): Guid, UserSkill, Skill, SkillId, User, UserId

### Community 152 - "Known Static-Analysis Limitations and Safe Patterns"
Cohesion: 0.50
Nodes (4): Arrays and default values, Known Static-Analysis Limitations and Safe Patterns, Other limitations to keep in mind, Structs with non-nullable fields

### Community 153 - "Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`"
Cohesion: 0.50
Nodes (4): Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`, `[MaybeNullWhen(bool)]`, `[NotNullIfNotNull(string)]`, `[NotNullWhen(bool)]`

### Community 155 - "Preconditions: `AllowNull` and `DisallowNull`"
Cohesion: 0.67
Nodes (3): `[AllowNull]`, `[DisallowNull]`, Preconditions: `AllowNull` and `DisallowNull`

### Community 156 - "Performance Patterns"
Cohesion: 0.67
Nodes (3): Async/Await Best Practices, Performance Patterns, Span<T> and Memory<T>

### Community 159 - "ExpertRepository"
Cohesion: 0.36
Nodes (7): CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task, ExpertRepository

### Community 161 - "ForgotPasswordCommand"
Cohesion: 0.50
Nodes (3): Trivo.Application.Features.Users.Commands.ForgotPassword, ForgotPasswordCommand, ForgotPasswordValidator

### Community 162 - ".Handle"
Cohesion: 0.19
Nodes (10): List, ExpertDetailsDto, List, RecruiterDetailsDto, List, UserDetailsDto, Guid, GetUserDetailsQuery (+2 more)

### Community 164 - "AiSetting"
Cohesion: 0.40
Nodes (4): AiSetting, ApiKey, EmbeddingModel, Provider

### Community 165 - "IExpertRepository"
Cohesion: 0.36
Nodes (7): CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task, IExpertRepository

### Community 169 - "NotificationHub"
Cohesion: 0.16
Nodes (9): Guid, IEnumerable, Task, INotificationHub, Exception, Guid, ILogger, Task (+1 more)

### Community 173 - "ICommand"
Cohesion: 0.06
Nodes (39): IBaseCommand, ICommand, ICommandHandler, Guid, UnbanUserCommand, CancellationToken, ILogger, Task (+31 more)

### Community 174 - "InterestWithIdDto"
Cohesion: 0.13
Nodes (17): Guid, InterestWithIdDto, IEnumerable, SearchInterestsByNameQuery, CancellationToken, IEnumerable, ILogger, Task (+9 more)

### Community 175 - "IQuery"
Cohesion: 0.12
Nodes (18): IRequest, IRequestHandler, IQuery, IQueryHandler, CompletedMatchesCountDto, GetCompletedMatchesCountQuery, CancellationToken, ILogger (+10 more)

### Community 177 - "AbstractValidator"
Cohesion: 0.20
Nodes (6): AbstractValidator, CreateInterestCategoryCommandValidator, SendImageValidator, SendMessageValidator, CreateRecruiterValidator, ResetPasswordValidator

### Community 186 - "MissingByMatching"
Cohesion: 0.50
Nodes (3): MissingByMatching, Expert, Recruiter

## Knowledge Gaps
- **600 isolated node(s):** `$schema`, `windowsAuthentication`, `anonymousAuthentication`, `applicationUrl`, `sslPort` (+595 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **24 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `ResultT` connect `ResultT` to `InterestCategory`, `.Handle`, `UserController`, `IRecruiterRepository`, `.SaveChangesAsync`, `.Handle`, `UserDto`, `.Handle`, `PagedResult`, `AdminController`, `.NotFound`, `.Handle`, `ICommand`, `ICacheService`, `IQuery`, `IInterestRepository`, `InterestWithIdDto`, `MessageDto`, `.GetByCategoriesAsync`, `.Handle`, `.Handle`, `Error`, `ControllerBase`, `.Handle`, `UserInterest`, `Skill`, `.Handle`, `SkillWithIdDto`, `GetReportedUsersCountQueryHandler`, `AuthenticationService`, `MatchDetailsDto`, `GetActiveUsersCountQueryHandler`?**
  _High betweenness centrality (0.144) - this node is a cross-community bridge._
- **Why does `TrivoContext` connect `TrivoContext` to `UserRepository`, `Message`, `InterestCategory`, `Trivo.Domain.Models`, `Notification`, `.AddRepositories`, `Code`, `UserSkill`, `Recruiter`, `AdministratorRepository`, `ExpertRepository`, `Chat`, `.AddSkillsToUserAsync`, `InterestRepository`, `ExceptionHandlingMiddleware`, `Interest`, `ICacheService`, `ChatUser`, `UserInterest`, `Skill`, `.Validate`, `Expert`, `Match`, `MatchRepository`, `Administrator`, `User`, `Report`, `GenericRepository`?**
  _High betweenness centrality (0.046) - this node is a cross-community bridge._
- **Why does `Trivo.Application.Abstractions.Messages` connect `Trivo.Application.Abstractions.Messages` to `ForgotPasswordCommand`, `Trivo.Application.Features.Interests.Commands.UpdateInterest`, `Trivo.Application.DTOs.Users`, `Trivo.Application.Pagination`, `Trivo.Application.Features.Skills.Commands.UpdateSkill`, `Trivo.Application.DTOs.Administrator`, `Trivo.Application.Features.Users.Commands.UpdateProfilePicture`, `Trivo.Application.Interfaces.SignalR`, `Trivo.Application.DTOs.Authentication`, `ICommand`, `MessageDto`, `Trivo.API.Controllers.V1`, `CreateReportCommandHandler.cs`, `Trivo.Domain.Enums`, `CreateInterestCategoryCommandHandler.cs`?**
  _High betweenness centrality (0.040) - this node is a cross-community bridge._
- **What connects `$schema`, `windowsAuthentication`, `anonymousAuthentication` to the rest of the system?**
  _600 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Trivo.Application.Abstractions.Messages` be split into smaller, more focused modules?**
  _Cohesion score 0.12786885245901639 - nodes in this community are weakly interconnected._
- **Should `Message` be split into smaller, more focused modules?**
  _Cohesion score 0.06493506493506493 - nodes in this community are weakly interconnected._
- **Should `InterestCategory` be split into smaller, more focused modules?**
  _Cohesion score 0.05580693815987934 - nodes in this community are weakly interconnected._