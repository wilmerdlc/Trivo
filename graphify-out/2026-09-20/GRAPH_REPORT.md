# Graph Report - Trivo  (2026-09-20)

## Corpus Check
- 445 files · ~81,422 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 3137 nodes · 7141 edges · 190 communities (182 shown, 8 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 441 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `545d7ae4`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- IUserRepository
- IAdministratorRepository
- Message
- InterestCategory
- Trivo.Application.Abstractions.Messages
- Trivo.Domain.Models
- CreateInterestCategoryCommandHandler.cs
- Trivo.Infrastructure.Persistence.Configurations
- .Handle
- .Handle
- Trivo.Application.Interfaces.SignalR
- https
- UserController
- AddProfileTextHash
- Notification
- ControllerBase
- BaseEntity
- .AddRepositories
- IRealTimeNotifier
- CodeRepository
- BHD.ResultPattern — Guía de arquitectura e implementación
- MatchDto
- User
- Dependency Injection Patterns
- Trivo.Domain.Enums
- Result
- SkillWithIdDto
- PagedResult
- Recruiter
- AdministratorRepository
- .Handle
- .Handle
- AdminController
- .Validation
- ChatRepository
- .Handle
- UserSkill
- InterestRepository
- Entity Framework Core Patterns
- ExceptionHandlingMiddleware
- NotificationHub
- Requerimientos Funcionales
- .AddAiService
- IUserOwnedRequest
- Interest
- Trivo.API.csproj
- ResultT
- IInterestRepository
- MatchHub
- Public API Design and Compatibility
- Skill
- MessageDto
- .GetByCategoriesAsync
- IMatchRepository
- Trivo.Application.Utils
- .Handle
- .CreateSkillAsync
- Error
- CreateMatchingCommandHandler
- Trivo.Infrastructure.Shared.csproj
- Code
- ChatUser
- Database Performance Patterns
- Documento de Requerimientos de Software
- TrivoContext
- ISkillRepository
- UserRecommendationHub
- SkillDto
- Slopwatch: LLM Anti-Cheat for .NET
- UserAiRecommendationDto
- .Validate
- Expert
- Trivo.Application.DTOs.Administrator
- .GetOrSetAsync
- GoogleGeminiEmbeddingService
- Módulo de Matchmaking con IA — Implementación
- Plan de implementación — Embeddings vía API externa para emparejamiento por afinidad
- Trivo.Application.Interfaces.Services
- SkillRepository
- Nullable Attributes Reference
- MatchRepository
- Modern C# Coding Standards
- .Handle
- .MapToInterests
- .Handle
- Plantillas copy-paste — feature CQRS de Trivo
- ICloudinaryService
- .Handle
- Polyfilling the nullable attributes for older target frameworks
- IQuery
- CreateMatchRejectionCommand
- Match
- .Handle
- Trivo.Application.csproj
- C# Nullable Reference Types
- OpenAiEmbeddingService
- NRT Migration Playbook Reference
- User
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
- MatchDetailsDto
- Performance and API Design Patterns
- Value Objects and Pattern Matching
- RF9-administrar-aplicacion.md
- .Handle
- ICommand
- .ValidateAsync
- CreateUserCommand
- Chat
- InitialCreate
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
- .CreateMatchAsync
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
- .Handle
- ReportStatus
- IGenericRepository
- Core Nullability Model
- Arquitectura CQRS de Trivo — mandato para nuevas features
- Composition and Error Handling
- Language Patterns
- .Handle
- ExpertDto
- Known Static-Analysis Limitations and Safe Patterns
- Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`
- Administrator
- Preconditions: `AllowNull` and `DisallowNull`
- Performance Patterns
- CLAUDE.md
- Trivo.Infrastructure.Persistence.Migrations
- IExpertRepository
- .Handle
- .ToDto
- .Handle
- SendImageCommand
- AddUniqueExpertRecruiterUserId
- .GetByUserIdAsync
- .BuildModel
- .SendFileAsync
- GetActiveUsersCountQueryHandler
- UpdateBiographyCommand
- ResendConfirmationCodeCommand
- BanUserCommand
- ConfirmAccountCommand
- ICommandHandler
- InterestWithIdDto
- RequestEmailChangeCommand
- CloudinarySetting
- AbstractValidator
- UpdatePasswordCommand
- ConfirmEmailChangeCommand
- SendFileCommand
- JwtSetting
- .BuildTargetModel
- .BuildTargetModel
- .ToRecruiterEntity
- ExpertConfig
- MessageConfig
- ReportConfig

## God Nodes (most connected - your core abstractions)
1. `ResultT` - 146 edges
2. `Trivo.Application.Abstractions.Messages` - 118 edges
3. `Trivo.Domain.Models` - 92 edges
4. `Trivo.Application.Utils` - 91 edges
5. `Trivo.Application.Interfaces.Services` - 74 edges
6. `PagedResult` - 70 edges
7. `IUserRepository` - 62 edges
8. `TrivoContext` - 61 edges
9. `Trivo.Application.Interfaces.Repository.Account` - 58 edges
10. `Trivo.Application.Pagination` - 57 edges

## Surprising Connections (you probably didn't know these)
- `UserController` --references--> `ICodeService`  [EXTRACTED]
  src/API/Trivo.API/Controllers/V1/UserController.cs → src/Application/Trivo.Application/Interfaces/Services/ICodeService.cs
- `ICommand` --references--> `Result`  [EXTRACTED]
  src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs → src/Application/Trivo.Application/Utils/Result.cs
- `ICommand` --references--> `ResultT`  [EXTRACTED]
  src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs → src/Application/Trivo.Application/Utils/Result.cs
- `BanUserCommand` --implements--> `ICommand`  [EXTRACTED]
  src/Application/Trivo.Application/Features/Administrator/Commands/BanUser/BanUserCommand.cs → src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs
- `CreateAdminCommand` --implements--> `ICommand`  [EXTRACTED]
  src/Application/Trivo.Application/Features/Administrator/Commands/CreateAdministrator/CreateAdminCommand.cs → src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs

## Import Cycles
- None detected.

## Communities (190 total, 8 thin omitted)

### Community 0 - "IUserRepository"
Cohesion: 0.06
Nodes (42): CancellationToken, Task, CancellationToken, Distance, Guid, IEnumerable, IReadOnlyList, List (+34 more)

### Community 1 - "IAdministratorRepository"
Cohesion: 0.26
Nodes (5): CancellationToken, Guid, IEnumerable, Task, IAdministratorRepository

### Community 2 - "Message"
Cohesion: 0.05
Nodes (48): Trivo.Application.Features.Reports.Commands.CreateReport, Trivo.Application.Features.Reports, Trivo.Application.DTOs.Reports, Authorize, CancellationToken, HttpPost, ISender, ProducesResponseType (+40 more)

### Community 3 - "InterestCategory"
Cohesion: 0.05
Nodes (41): Authorize, CancellationToken, HttpGet, HttpPost, ISender, ProducesResponseType, Task, InterestCategoryController (+33 more)

### Community 4 - "Trivo.Application.Abstractions.Messages"
Cohesion: 0.06
Nodes (25): Trivo.Application.Features.Skills.Query.GetSkillsPagination, Trivo.Application.Abstractions.Messages, Trivo.Application.Features.Users.Commands.CreateUser, Trivo.Application.Features.Interests.Commands.CreateInterest, Trivo.Application.Features.Users.Query.GetUserDetails, Trivo.Application.Features.Skills, Trivo.Application.Features.Users, Trivo.Application.Features.Users.Query.GetUserInterests (+17 more)

### Community 5 - "Trivo.Domain.Models"
Cohesion: 0.09
Nodes (13): Trivo.Domain.Common, Trivo.Infrastructure.Persistence.Context, Trivo.Infrastructure.Persistence.Repository.Account, Trivo.Application.Interfaces.Repository.Base, Trivo.Infrastructure.Persistence.Services, Trivo.Infrastructure.Persistence.Repository, Trivo.Domain.Models, Trivo.Application.Interfaces.Repository (+5 more)

### Community 6 - "CreateInterestCategoryCommandHandler.cs"
Cohesion: 0.27
Nodes (4): Trivo.Application.DTOs.InterestCategories, Trivo.Application.Features.InterestCategories.Commands.CreateInterestCategory, Trivo.Application.Features.InterestCategories.Query.GetPaginatedInterestCategories, Trivo.Application.Features.InterestCategories

### Community 7 - "Trivo.Infrastructure.Persistence.Configurations"
Cohesion: 0.09
Nodes (15): Trivo.Infrastructure.Persistence.Configurations, IEntityTypeConfiguration, EntityTypeBuilder, AdministratorConfig, ChatUserConfig, EntityTypeBuilder, CodeConfig, EntityTypeBuilder (+7 more)

### Community 8 - ".Handle"
Cohesion: 0.19
Nodes (13): Candidates, HasOverlap, Guid, GetUserRecommendationsQuery, CancellationToken, Distance, Guid, ILogger (+5 more)

### Community 9 - ".Handle"
Cohesion: 0.15
Nodes (12): DateTime, Guid, AdminDto, Administrator, AdminMapper, IFormFile, CreateAdminCommand, CancellationToken (+4 more)

### Community 10 - "Trivo.Application.Interfaces.SignalR"
Cohesion: 0.07
Nodes (12): Trivo.Application.Features.Messages.Commands.SendImage, Trivo.Application.Features.Messages.Query.GetMessagePagination, Trivo.Application.Features.Chat.Query.GetChatPagination, Trivo.Application.Features.Chat, Trivo.Application.DTOs.Notifications, Trivo.Application.Interfaces.SignalR, Trivo.Application.Features.Chat.Commands.CreateChat, Trivo.Infrastructure.Shared.SignalR (+4 more)

### Community 11 - "https"
Cohesion: 0.10
Nodes (21): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, launchUrl, commandName (+13 more)

### Community 12 - "UserController"
Cohesion: 0.06
Nodes (42): Trivo.API.Controllers.V1.Requests, ChangePasswordRequest, ConfirmEmailChangeRequest, Guid, List, FilterUsersByInterestsAndSkillsRequest, RequestEmailChangeRequest, UpdateBiographyRequest (+34 more)

### Community 13 - "AddProfileTextHash"
Cohesion: 0.20
Nodes (6): MigrationBuilder, DateTime, Guid, ModelBuilder, Vector, AddProfileTextHash

### Community 14 - "Notification"
Cohesion: 0.08
Nodes (25): Trivo.Application.Features.Notifications, IEnumerable, List, NotificationMapper, CancellationToken, Guid, Task, INotificationRepository (+17 more)

### Community 15 - "ControllerBase"
Cohesion: 0.06
Nodes (31): ControllerBase, Trivo.Application.Features.Experts.Commands.UpdateExpert, Authorize, CancellationToken, HttpPost, ISender, Task, ChatController (+23 more)

### Community 16 - "BaseEntity"
Cohesion: 0.13
Nodes (13): DateTime, Guid, BaseEntity, CreatedAt, Id, UpdatedAt, Guid, ICollection (+5 more)

### Community 17 - ".AddRepositories"
Cohesion: 0.20
Nodes (12): IReportRepository, IGetExpertIdService, IGetRecruiterIdService, IUserRoleService, IConfiguration, IConnectionMultiplexer, IServiceCollection, DependencyInjection (+4 more)

### Community 18 - "IRealTimeNotifier"
Cohesion: 0.10
Nodes (19): Guid, IEnumerable, Task, IChatHub, Guid, IEnumerable, Task, IRealTimeNotifier (+11 more)

### Community 19 - "CodeRepository"
Cohesion: 0.21
Nodes (8): CancellationToken, Guid, Task, ICodeRepository, CancellationToken, Guid, Task, CodeRepository

### Community 20 - "BHD.ResultPattern — Guía de arquitectura e implementación"
Cohesion: 0.06
Nodes (31): 1. Arquitectura general, 2.1. Estructura de carpetas, 2.2. `BHD.ResultPattern.csproj`, 2.3. `Result.cs`, 2.4. `Error.cs`, 2. Núcleo: `BHD.ResultPattern` (sin dependencias), 3.1. Estructura de carpetas, 3.2. `BHD.ResultPattern.AspNetCore.csproj` (+23 more)

### Community 21 - "MatchDto"
Cohesion: 0.20
Nodes (12): DateTime, Guid, MatchDto, Guid, IEnumerable, Task, IMatchNotifier, Guid (+4 more)

### Community 22 - "User"
Cohesion: 0.07
Nodes (28): ICollection, Vector, User, Biography, ChatUsers, Codes, Email, Experts (+20 more)

### Community 23 - "Dependency Injection Patterns"
Cohesion: 0.05
Nodes (38): Advanced DI Patterns, Akka.DependencyInjection Reference, Akka.Hosting.TestKit, Akka.NET Actor Scope Management, Common Patterns, Conditional Registration, Contents, Factory-Based Registration (+30 more)

### Community 24 - "Trivo.Domain.Enums"
Cohesion: 0.16
Nodes (6): Trivo.Application.Features.Matching.Commands.UpdateMatch, Trivo.Application.Features.Matching.Commands.CreateMatch, Trivo.Domain.Enums, Trivo.Application.Features.Matching.Commands.CreateMatchRejection, Trivo.Application.Features.Matching.Query.GetMatchByUser, Trivo.Application.DTOs.Matching

### Community 25 - "Result"
Cohesion: 0.11
Nodes (21): DateTime, Guid, CodeDto, CancellationToken, Guid, Task, ICodeService, CodeGenerator (+13 more)

### Community 26 - "SkillWithIdDto"
Cohesion: 0.16
Nodes (12): Guid, SkillWithIdDto, List, ExpertDetailsDto, List, RecruiterDetailsDto, List, UserDetailsDto (+4 more)

### Community 27 - "PagedResult"
Cohesion: 0.10
Nodes (21): Guid, UserDto, IEnumerable, GetLast10BannedUsersQuery, CancellationToken, IEnumerable, ILogger, Task (+13 more)

### Community 28 - "Recruiter"
Cohesion: 0.21
Nodes (15): CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task, IRecruiterRepository, Recruiter (+7 more)

### Community 29 - "AdministratorRepository"
Cohesion: 0.23
Nodes (8): Administrator, CancellationToken, Guid, IEnumerable, Match, Task, User, AdministratorRepository

### Community 30 - ".Handle"
Cohesion: 0.11
Nodes (12): EmailResponseDto, CancellationToken, Task, CancellationToken, Task, CancellationToken, Task, CancellationToken (+4 more)

### Community 31 - ".Handle"
Cohesion: 0.15
Nodes (11): DateTime, Guid, AdminMatchDto, ExpertMatchDto, RecruiterMatchDto, GetLatestMatchesQuery, CancellationToken, ILogger (+3 more)

### Community 32 - "AdminController"
Cohesion: 0.30
Nodes (11): Authorize, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IEnumerable, ISender (+3 more)

### Community 33 - ".Validation"
Cohesion: 0.09
Nodes (32): HttpDelete, Authorize, CancellationToken, Guid, HttpPost, HttpPut, Task, NotificationController (+24 more)

### Community 34 - "ChatRepository"
Cohesion: 0.31
Nodes (8): CancellationToken, Chat, Guid, IEnumerable, IReadOnlyList, Task, User, ChatRepository

### Community 35 - ".Handle"
Cohesion: 0.13
Nodes (13): INotification, INotificationHandler, Guid, UserProfileChangedEvent, CancellationToken, ILogger, Task, UserProfileChangedEventHandler (+5 more)

### Community 36 - "UserSkill"
Cohesion: 0.14
Nodes (16): CancellationToken, Guid, List, Task, IUserSkillRepository, Guid, UserSkill, Skill (+8 more)

### Community 37 - "InterestRepository"
Cohesion: 0.27
Nodes (7): CancellationToken, Guid, IEnumerable, Interest, List, Task, InterestRepository

### Community 38 - "Entity Framework Core Patterns"
Cohesion: 0.05
Nodes (38): 1. Forgetting to Update When NoTracking, 2. N+1 Query Problem, 3. Tracking Conflicts with Multiple DbContext Instances, 4. Not Using Async Consistently, 5. Querying Inside Loops, Actors / Long-Lived Objects (Factory Pattern), AppHost Configuration, Applying Migrations (+30 more)

### Community 39 - "ExceptionHandlingMiddleware"
Cohesion: 0.08
Nodes (18): Trivo.API.Middlewares, Trivo.API.Extensions, IApplicationBuilder, IEndpointRouteBuilder, IHostEnvironment, ProblemDetails, RequestDelegate, IConfiguration (+10 more)

### Community 40 - "NotificationHub"
Cohesion: 0.17
Nodes (9): Guid, IEnumerable, Task, INotificationHub, Exception, Guid, ILogger, Task (+1 more)

### Community 41 - "Requerimientos Funcionales"
Cohesion: 0.12
Nodes (15): 4.3.1 Análisis Preliminar y Determinación de Requerimientos, 4.3.2 Análisis y Modelado de los Requerimientos del Proyecto, 4.3 Descripción del Modelo de Desarrollo, Requerimientos Funcionales, Requerimientos Funcionales (Sección Adicional), Requerimientos No Funcionales, RF1. Registro, RF2. Inicio de sesión (+7 more)

### Community 42 - ".AddAiService"
Cohesion: 0.21
Nodes (8): JwtResponse, AiSetting, ApiKey, EmbeddingModel, Provider, IConfiguration, IServiceCollection, DependencyInjection

### Community 43 - "IUserOwnedRequest"
Cohesion: 0.14
Nodes (11): Guid, IUserOwnedRequest, UserId, Guid, CreateChatCommand, UserId, CreateChatValidator, Guid (+3 more)

### Community 44 - "Interest"
Cohesion: 0.10
Nodes (18): Guid, InterestDetailsDto, Guid, CreateInterestCommand, CreateInterestValidator, Guid, IEnumerable, Interest (+10 more)

### Community 45 - "Trivo.API.csproj"
Cohesion: 0.11
Nodes (17): Asp.Versioning.Mvc (8.1.0), AspNetCore.HealthChecks.Redis (9.0.0), Microsoft.AspNetCore.OpenApi (8.0.10), Microsoft.AspNetCore.SignalR.Core (1.2.0), Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore (8.0.10), Scalar.AspNetCore (2.17.1), Serilog.Enrichers.Environment (3.0.1), Serilog.Enrichers.Thread (4.0.0) (+9 more)

### Community 46 - "ResultT"
Cohesion: 0.05
Nodes (43): CancellationToken, Task, CancellationToken, Task, CancellationToken, Task, CancellationToken, Task (+35 more)

### Community 47 - "IInterestRepository"
Cohesion: 0.15
Nodes (12): CancellationToken, Guid, IEnumerable, List, Task, IInterestRepository, IValidation, CancellationToken (+4 more)

### Community 48 - "MatchHub"
Cohesion: 0.17
Nodes (10): Hub, Guid, IEnumerable, Task, IMatchHub, Exception, ILogger, IMediator (+2 more)

### Community 49 - "Public API Design and Compatibility"
Cohesion: 0.06
Nodes (31): Anti-Patterns, API Approval Testing, API Change Guidelines, Benefits, Breaking Changes Disguised as Fixes, Chesterton's Fence, Deprecation Pattern, Encapsulation Patterns (+23 more)

### Community 50 - "Skill"
Cohesion: 0.22
Nodes (8): DateTime, Guid, ICollection, Skill, Name, RegisteredAt, SkillId, UserSkills

### Community 51 - "MessageDto"
Cohesion: 0.10
Nodes (28): DateTime, Guid, MessageDto, CancellationToken, ILogger, Task, SendFileCommandHandler, CancellationToken (+20 more)

### Community 52 - ".GetByCategoriesAsync"
Cohesion: 0.23
Nodes (11): Authorize, CancellationToken, Guid, HttpGet, HttpPost, IEnumerable, ISender, List (+3 more)

### Community 53 - "IMatchRepository"
Cohesion: 0.32
Nodes (9): AcceptedIds, CancellationToken, Guid, IEnumerable, IReadOnlyList, Match, RejectedIds, Task (+1 more)

### Community 54 - "Trivo.Application.Utils"
Cohesion: 0.10
Nodes (10): Trivo.Application.Features.Recruiters.Commands.UpdateRecruiter, Trivo.API.Controllers.V1, Trivo.Application.DTOs.Expert, Trivo.Application.Features.Experts.Commands.CreateExpert, Trivo.Application.Features.Recruiters, Trivo.Application.Features.Experts, Trivo.Application.Utils, Trivo.Application.Helpers (+2 more)

### Community 55 - ".Handle"
Cohesion: 0.19
Nodes (13): Guid, IEnumerable, GetMatchByUserQuery, CancellationToken, Dictionary, Func, Guid, IEnumerable (+5 more)

### Community 56 - ".CreateSkillAsync"
Cohesion: 0.24
Nodes (9): Authorize, CancellationToken, HttpGet, HttpPost, IEnumerable, ISender, ProducesResponseType, Task (+1 more)

### Community 57 - "Error"
Cohesion: 0.15
Nodes (7): PaginationError, ErrorType, Error, Code, Description, ErrorType, StatusCode

### Community 58 - "CreateMatchingCommandHandler"
Cohesion: 0.08
Nodes (24): Guid, CreateMatchingCommand, Dictionary, expertStatus, ILogger, recruiterStatus, CreateMatchingCommandHandler, CancellationToken (+16 more)

### Community 59 - "Trivo.Infrastructure.Shared.csproj"
Cohesion: 0.15
Nodes (12): CloudinaryDotNet (1.27.0), MailKit (4.17.0), Microsoft.AspNetCore.Authentication.JwtBearer (8.0.10), Microsoft.AspNetCore.SignalR (1.2.0), Microsoft.Extensions.Options (10.0.3), Microsoft.Extensions.Options.ConfigurationExtensions (8.0.0), Microsoft.IdentityModel.Tokens (8.10.0), MimeKit (4.17.0) (+4 more)

### Community 60 - "Code"
Cohesion: 0.14
Nodes (13): DateTime, Guid, Code, CodeId, CreatedAt, ExpiresAt, IsRevoked, IsUsed (+5 more)

### Community 61 - "ChatUser"
Cohesion: 0.17
Nodes (10): DateTime, Guid, ChatUser, Chat, ChatId, ChatName, JoinedAt, LeftAt (+2 more)

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
Cohesion: 0.16
Nodes (12): GetSkillsPaginationQuery, CancellationToken, ILogger, Task, GetSkillsPaginationQueryHandler, GetSkillsPaginationValidator, CancellationToken, Guid (+4 more)

### Community 66 - "UserRecommendationHub"
Cohesion: 0.19
Nodes (8): IEnumerable, Task, IUserRecommendationHub, Exception, ILogger, IMediator, Task, UserRecommendationHub

### Community 67 - "SkillDto"
Cohesion: 0.17
Nodes (9): DateTime, Guid, SkillDto, Guid, CreateSkillCommand, CreateSkillValidator, Guid, IEnumerable (+1 more)

### Community 68 - "Slopwatch: LLM Anti-Cheat for .NET"
Cohesion: 0.09
Nodes (22): After Every Code Change, As a Global Tool, As a Local Tool (Recommended), Azure Pipelines, CI/CD Integration, Claude Code Hook Integration, Common Slop Patterns, Configuration (+14 more)

### Community 69 - "UserAiRecommendationDto"
Cohesion: 0.22
Nodes (12): Guid, List, UserAiRecommendationDto, Guid, IEnumerable, Task, IAiNotifier, Guid (+4 more)

### Community 70 - ".Validate"
Cohesion: 0.40
Nodes (4): CancellationToken, Expression, Func, Task

### Community 71 - "Expert"
Cohesion: 0.18
Nodes (10): Guid, ExpertMapper, Guid, ICollection, Expert, AvailableForProjects, IsHired, Matches (+2 more)

### Community 72 - "Trivo.Application.DTOs.Administrator"
Cohesion: 0.12
Nodes (9): Trivo.Application.Features.Administrator.Query.GetCompletedMatchesCount, Trivo.Application.Features.Administrator.Query.GetLatestMatches, Trivo.Application.Features.Administrator.Query.GetLatestUsersPaged, Trivo.Application.Features.Administrator.Commands.CreateAdministrator, Trivo.Application.Features.Administrator.Query.GetReportedUsersCount, Trivo.Application.Features.Administrator.Query.GetActiveUsersCount, Trivo.Application.DTOs.Administrator, Trivo.Application.Features.Administrator (+1 more)

### Community 73 - ".GetOrSetAsync"
Cohesion: 0.10
Nodes (20): IDatabase, IReadOnlyList, CacheEntryOptions, AbsoluteExpiration, Tags, CacheProfiles, Cold, Hot (+12 more)

### Community 74 - "GoogleGeminiEmbeddingService"
Cohesion: 0.16
Nodes (14): ContentPart, EmbedContentResponse, EmbeddingValues, HttpClient, CancellationToken, ILogger, Task, ContentPart (+6 more)

### Community 75 - "Módulo de Matchmaking con IA — Implementación"
Cohesion: 0.14
Nodes (13): 1. Problema de negocio, 2.1 Abstracción del proveedor — `IEmbeddingService`, 2.2 Construcción del texto — `UserProfileTextBuilder`, 2.3 Cuándo se regenera el embedding — `UserProfileChangedEvent`, 2. Arquitectura, 3. Cambios de esquema (tablas), 4. El algoritmo de recomendación, 5. Qué NO se cachea, y por qué (+5 more)

### Community 76 - "Plan de implementación — Embeddings vía API externa para emparejamiento por afinidad"
Cohesion: 0.14
Nodes (13): 0. Decisión de proveedor y por qué la interfaz debe ser agnóstica, 10. Pruebas, 1. Infraestructura de base de datos (bloqueante, va primero), 2. Dominio (`Trivo.Domain`), 3. Application (`Trivo.Application`), 4. Infrastructure.Shared — implementación del proveedor, 5. Infrastructure.Persistence — columna, mapping e índice, 6. Cuándo se genera/regenera el embedding (+5 more)

### Community 77 - "Trivo.Application.Interfaces.Services"
Cohesion: 0.09
Nodes (16): Trivo.Application.Interfaces.UnitOfWork, Trivo.Application.DTOs.Email, Trivo.Application.Features.Users.Commands.LoginUser, Trivo.Infrastructure.Shared.Services, Trivo.Domain.Configurations, Trivo.Application.Features.Users.Commands.ForgotPassword, Trivo.Application.DTOs.Authentication, Trivo.Application.Features.Users.Events (+8 more)

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
Cohesion: 0.21
Nodes (10): Guid, RecruiterDto, Guid, CreateRecruiterCommand, CancellationToken, ILogger, Task, CreateRecruiterCommandHandler (+2 more)

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

### Community 87 - ".Handle"
Cohesion: 0.22
Nodes (8): IEnumerable, SearchSkillsByNameQuery, CancellationToken, IEnumerable, ILogger, Task, SearchSkillsByNameQueryHandler, SearchSkillsByNameValidator

### Community 88 - "Polyfilling the nullable attributes for older target frameworks"
Cohesion: 0.20
Nodes (10): Candidate packages (evaluate, do not default to one), Decision rules, File-level `#nullable` directives, Incremental Adoption Strategy, Is a polyfill needed at all?, Options and tradeoffs, Polyfilling the nullable attributes for older target frameworks, Project-level (+2 more)

### Community 89 - "IQuery"
Cohesion: 0.10
Nodes (22): IRequest, IRequestHandler, IQuery, IQueryHandler, CompletedMatchesCountDto, ReportedUsersCountDto, GetCompletedMatchesCountQuery, CancellationToken (+14 more)

### Community 90 - "CreateMatchRejectionCommand"
Cohesion: 0.29
Nodes (6): Guid, CreateMatchRejectionCommand, CreatedBy, ExpertId, RecruiterId, CreateMatchRejectionValidator

### Community 91 - "Match"
Cohesion: 0.18
Nodes (10): Guid, Match, Expert, ExpertId, ExpertStatus, MatchStatus, RecruiterId, RecruiterStatus (+2 more)

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

### Community 98 - "UpdateInterestCommand"
Cohesion: 0.33
Nodes (5): Trivo.Application.Features.Interests.Commands.UpdateInterest, Guid, IReadOnlyList, UpdateInterestCommand, UpdateInterestValidator

### Community 99 - "Anti-Patterns to Avoid"
Cohesion: 0.25
Nodes (8): Anti-Patterns to Avoid, Don't: Block on async code, Don't: Create deep inheritance hierarchies, Don't: Forget CancellationToken in async methods, Don't: Return List<T> when you mean IReadOnlyList<T>, Don't: Use byte[] when ReadOnlySpan<byte> works, Don't: Use classes for value objects, Don't: Use mutable DTOs

### Community 100 - "AuthenticationService"
Cohesion: 0.07
Nodes (32): CancellationToken, HttpPost, ProducesResponseType, SwaggerOperation, Task, AuthController, RefreshTokenRequest, RefreshToken (+24 more)

### Community 101 - "Report"
Cohesion: 0.20
Nodes (9): Guid, Report, Message, MessageId, Note, ReportedById, ReportId, ReportStatus (+1 more)

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

### Community 109 - "MatchDetailsDto"
Cohesion: 0.13
Nodes (16): DateTime, Guid, MatchDetailsDto, Guid, UpdateMatchingCommand, CancellationToken, Guid, ILogger (+8 more)

### Community 110 - "Performance and API Design Patterns"
Cohesion: 0.29
Nodes (6): Accept Abstractions, Return Appropriately Specific, API Design Principles, Contents, Method Signatures Best Practices, Performance and API Design Patterns, Span<T> and Memory<T> for Zero-Allocation Code

### Community 111 - "Value Objects and Pattern Matching"
Cohesion: 0.29
Nodes (7): Constraint-Enforcing Value Objects, Contents, No Implicit Conversions, Pattern Matching (C# 8-12), TypeConverter Support for Configuration Binding, Value Objects and Pattern Matching, Value Objects as readonly record struct

### Community 113 - ".Handle"
Cohesion: 0.18
Nodes (10): Guid, InterestByCategoryIdDto, Guid, IEnumerable, GetInterestsByCategoryIdQuery, CancellationToken, ILogger, Task (+2 more)

### Community 114 - "ICommand"
Cohesion: 0.22
Nodes (7): Trivo.Application.Features.Users.Commands.ResetPassword, IBaseCommand, ICommand, ForgotPasswordCommand, ForgotPasswordValidator, ResetPasswordCommand, ResetPasswordValidator

### Community 115 - ".ValidateAsync"
Cohesion: 0.32
Nodes (6): CancellationToken, Expression, Func, Guid, Task, GenericRepository

### Community 116 - "CreateUserCommand"
Cohesion: 0.33
Nodes (5): Guid, IFormFile, List, CreateUserCommand, CreateUserValidator

### Community 117 - "Chat"
Cohesion: 0.20
Nodes (8): ICollection, Chat, ChatType, ChatUsers, IsActive, Messages, EntityTypeBuilder, ChatConfig

### Community 118 - "InitialCreate"
Cohesion: 0.22
Nodes (6): Migration, DateTime, Guid, MigrationBuilder, Vector, InitialCreate

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

### Community 127 - "ChatDto"
Cohesion: 0.16
Nodes (14): DateTime, Guid, List, ChatDto, CancellationToken, ILogger, Task, CreateChatCommandHandler (+6 more)

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

### Community 133 - ".AddServices"
Cohesion: 0.29
Nodes (4): HubConnectionContext, IUserIdProvider, IUserIdProvider, CustomUserIdProvider

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
Cohesion: 0.24
Nodes (8): Guid, IEnumerable, GetUserSkillsQuery, CancellationToken, IEnumerable, ILogger, Task, GetUserSkillsQueryHandler

### Community 151 - "ExpertDto"
Cohesion: 0.27
Nodes (8): Guid, ExpertDto, Guid, CreateExpertCommand, CancellationToken, ILogger, Task, CreateExpertCommandHandler

### Community 152 - "Known Static-Analysis Limitations and Safe Patterns"
Cohesion: 0.50
Nodes (4): Arrays and default values, Known Static-Analysis Limitations and Safe Patterns, Other limitations to keep in mind, Structs with non-nullable fields

### Community 153 - "Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`"
Cohesion: 0.50
Nodes (4): Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`, `[MaybeNullWhen(bool)]`, `[NotNullIfNotNull(string)]`, `[NotNullWhen(bool)]`

### Community 154 - "Administrator"
Cohesion: 0.20
Nodes (10): Administrator, Biography, Email, FirstName, IsActive, LastName, LinkedIn, PasswordHash (+2 more)

### Community 155 - "Preconditions: `AllowNull` and `DisallowNull`"
Cohesion: 0.67
Nodes (3): `[AllowNull]`, `[DisallowNull]`, Preconditions: `AllowNull` and `DisallowNull`

### Community 156 - "Performance Patterns"
Cohesion: 0.67
Nodes (3): Async/Await Best Practices, Performance Patterns, Span<T> and Memory<T>

### Community 158 - "Trivo.Infrastructure.Persistence.Migrations"
Cohesion: 0.28
Nodes (3): Trivo.Infrastructure.Persistence.Migrations, MigrationBuilder, AddPendingEmailToUser

### Community 159 - "IExpertRepository"
Cohesion: 0.32
Nodes (8): IExpertRepository, CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task, ExpertRepository

### Community 160 - ".Handle"
Cohesion: 0.31
Nodes (7): UpdateNameDto, Guid, UpdateNameCommand, CancellationToken, ILogger, Task, UpdateNameCommandHandler

### Community 161 - ".ToDto"
Cohesion: 0.25
Nodes (6): Guid, UserChatDto, Chat, Guid, User, ChatMapper

### Community 162 - ".Handle"
Cohesion: 0.24
Nodes (5): Guid, IEnumerable, CacheKeys, CancellationToken, Task

### Community 163 - "SendImageCommand"
Cohesion: 0.33
Nodes (5): Guid, IFormFile, SendImageCommand, UserId, SendImageValidator

### Community 164 - "AddUniqueExpertRecruiterUserId"
Cohesion: 0.25
Nodes (6): MigrationBuilder, DateTime, Guid, ModelBuilder, Vector, AddUniqueExpertRecruiterUserId

### Community 165 - ".GetByUserIdAsync"
Cohesion: 0.35
Nodes (6): CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task

### Community 166 - ".BuildModel"
Cohesion: 0.25
Nodes (6): ModelSnapshot, DateTime, Guid, ModelBuilder, Vector, TrivoContextModelSnapshot

### Community 167 - ".SendFileAsync"
Cohesion: 0.44
Nodes (6): Authorize, CancellationToken, HttpPost, ISender, Task, MessageController

### Community 168 - "GetActiveUsersCountQueryHandler"
Cohesion: 0.36
Nodes (6): ActiveUsersCountDto, GetActiveUsersCountQuery, CancellationToken, ILogger, Task, GetActiveUsersCountQueryHandler

### Community 169 - "UpdateBiographyCommand"
Cohesion: 0.40
Nodes (4): Trivo.Application.Features.Users.Commands.UpdateBiography, Guid, UpdateBiographyCommand, UpdateBiographyValidator

### Community 170 - "ResendConfirmationCodeCommand"
Cohesion: 0.50
Nodes (3): Trivo.Application.Features.Users.Commands.ResendConfirmationCode, ResendConfirmationCodeCommand, ResendConfirmationCodeValidator

### Community 171 - "BanUserCommand"
Cohesion: 0.40
Nodes (4): Trivo.Application.Features.Administrator.Commands.BanUser, Guid, BanUserCommand, BanUserValidator

### Community 172 - "ConfirmAccountCommand"
Cohesion: 0.25
Nodes (7): Guid, ConfirmAccountCommand, CancellationToken, ILogger, Task, ConfirmAccountCommandHandler, ConfirmAccountValidator

### Community 173 - "ICommandHandler"
Cohesion: 0.06
Nodes (56): ICommandHandler, UpdateUsernameDto, ILogger, BanUserCommandHandler, ILogger, UnbanUserCommandHandler, ILogger, UpdateExpertCommandHandler (+48 more)

### Community 174 - "InterestWithIdDto"
Cohesion: 0.13
Nodes (17): Guid, InterestWithIdDto, IEnumerable, SearchInterestsByNameQuery, CancellationToken, IEnumerable, ILogger, Task (+9 more)

### Community 175 - "RequestEmailChangeCommand"
Cohesion: 0.40
Nodes (4): Trivo.Application.Features.Users.Commands.RequestEmailChange, Guid, RequestEmailChangeCommand, RequestEmailChangeValidator

### Community 177 - "AbstractValidator"
Cohesion: 0.11
Nodes (11): AbstractValidator, GetChatPaginationValidator, CreateExpertValidator, CreateMatchValidator, UpdateMatchingValidation, GetMessagePaginationValidator, CreateRecruiterValidator, UpdateRecruiterValidator (+3 more)

### Community 178 - "UpdatePasswordCommand"
Cohesion: 0.40
Nodes (4): Trivo.Application.Features.Users.Commands.UpdatePassword, Guid, UpdatePasswordCommand, UpdatePasswordValidator

### Community 179 - "ConfirmEmailChangeCommand"
Cohesion: 0.40
Nodes (4): Trivo.Application.Features.Users.Commands.ConfirmEmailChange, Guid, ConfirmEmailChangeCommand, ConfirmEmailChangeValidator

### Community 180 - "SendFileCommand"
Cohesion: 0.33
Nodes (5): Guid, IFormFile, SendFileCommand, UserId, SendFileValidator

### Community 181 - "JwtSetting"
Cohesion: 0.33
Nodes (5): JwtSetting, Audience, DurationInMinutes, Issuer, Key

### Community 182 - ".BuildTargetModel"
Cohesion: 0.40
Nodes (4): DateTime, Guid, ModelBuilder, Vector

### Community 183 - ".BuildTargetModel"
Cohesion: 0.40
Nodes (4): DateTime, Guid, ModelBuilder, Vector

## Knowledge Gaps
- **602 isolated node(s):** `$schema`, `windowsAuthentication`, `anonymousAuthentication`, `applicationUrl`, `sslPort` (+597 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **8 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `ResultT` connect `ResultT` to `IUserRepository`, `.CreateMatchAsync`, `InterestCategory`, `Message`, `.Handle`, `.Handle`, `UserController`, `ControllerBase`, `.Handle`, `.Handle`, `ExpertDto`, `Result`, `PagedResult`, `.Handle`, `.Handle`, `AdminController`, `.Validation`, `.Handle`, `.Handle`, `.SendFileAsync`, `GetActiveUsersCountQueryHandler`, `ConfirmAccountCommand`, `ICommandHandler`, `InterestWithIdDto`, `MessageDto`, `.GetByCategoriesAsync`, `.Handle`, `.CreateSkillAsync`, `Error`, `ISkillRepository`, `.Handle`, `.Handle`, `.Handle`, `IQuery`, `AuthenticationService`, `MatchDetailsDto`, `.Handle`, `ICommand`, `ChatDto`?**
  _High betweenness centrality (0.128) - this node is a cross-community bridge._
- **Why does `Trivo.Domain.Models` connect `Trivo.Domain.Models` to `IUserRepository`, `Message`, `InterestCategory`, `Trivo.Application.Abstractions.Messages`, `CreateInterestCategoryCommandHandler.cs`, `Trivo.Infrastructure.Persistence.Configurations`, `Trivo.Application.Interfaces.SignalR`, `Notification`, `BaseEntity`, `Trivo.Domain.Enums`, `UserSkill`, `Skill`, `Trivo.Application.Utils`, `ExpertConfig`, `MessageConfig`, `ReportConfig`, `Code`, `ChatUser`, `Trivo.Application.DTOs.Administrator`, `Trivo.Application.Interfaces.Services`, `Match`, `User`, `Report`, `Chat`?**
  _High betweenness centrality (0.067) - this node is a cross-community bridge._
- **Why does `Trivo.Domain.Enums` connect `Trivo.Domain.Enums` to `UserMappingExtensions.cs`, `ErrorType`, `NotificationType`, `Trivo.Domain.Models`, `ExpertStatus`, `Level`, `MatchStatus`, `MessageType`, `Trivo.Application.Interfaces.SignalR`, `RecruiterStatus`, `UserStatus`, `ChatType`, `MatchFault`, `ReportStatus`, `Result`, `CreateMatchingCommandHandler`, `Trivo.Application.Interfaces.Services`, `MatchDetailsDto`, `MessageStatus`?**
  _High betweenness centrality (0.060) - this node is a cross-community bridge._
- **What connects `$schema`, `windowsAuthentication`, `anonymousAuthentication` to the rest of the system?**
  _602 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `IUserRepository` be split into smaller, more focused modules?**
  _Cohesion score 0.0649736902310684 - nodes in this community are weakly interconnected._
- **Should `Message` be split into smaller, more focused modules?**
  _Cohesion score 0.05029838022165388 - nodes in this community are weakly interconnected._
- **Should `InterestCategory` be split into smaller, more focused modules?**
  _Cohesion score 0.05478750640040963 - nodes in this community are weakly interconnected._