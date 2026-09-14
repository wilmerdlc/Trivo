# Graph Report - Trivo  (2026-09-12)

## Corpus Check
- 424 files · ~76,247 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 3008 nodes · 6851 edges · 186 communities (183 shown, 3 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 419 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `b785c5a5`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- UserRepository
- Trivo.Application.Abstractions.Messages
- Message
- InterestCategory
- Trivo.Application.DTOs.Users
- Trivo.Domain.Models
- IAuthenticationService
- Trivo.Infrastructure.Persistence.Configurations
- .Handle
- IUserRepository
- Trivo.Application.Interfaces.SignalR
- .UpdateExpertAsync
- ResultT
- Trivo.Infrastructure.Persistence.Migrations
- Notification
- ExpertDto
- Recruiter
- .AddRepositories
- IChatHub
- Code
- BHD.ResultPattern — Guía de arquitectura e implementación
- MatchDetailsDto
- User
- Dependency Injection Patterns
- Trivo.Domain.Enums
- .Conflict
- ICommandHandler
- UserDto
- Recruiter
- IAdministratorRepository
- .AddServices
- NotificationService
- AdminController
- NotificationNotifier
- ChatRepository
- Match
- UserSkill
- InterestRepository
- Entity Framework Core Patterns
- ExceptionHandlingMiddleware
- .CreateMatchNotificationAsync
- MessageDto
- .AddAiService
- AbstractValidator
- Interest
- Trivo.API.csproj
- ICacheService
- IInterestRepository
- MatchHub
- Public API Design and Compatibility
- CacheKeys
- IChatRepository
- .GetByCategoriesAsync
- .SaveChangesAsync
- RecruiterController.cs
- .Handle
- .Handle
- Trivo.API.Controllers.V1.Requests
- Roles
- Trivo.Infrastructure.Shared.csproj
- IUnitOfWork
- .Handle
- Database Performance Patterns
- .Handle
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
- Error
- UserRecommendationHub
- Nullable Attributes Reference
- IMatchRepository
- Modern C# Coding Standards
- .RestrictToBestStructuralMatches
- .ValidateEmailAsync
- .Handle
- Plantillas copy-paste — feature CQRS de Trivo
- ICloudinaryService
- .Handle
- Polyfilling the nullable attributes for older target frameworks
- .Handle
- ChatDto
- Trivo.Application.Pagination
- .Handle
- Trivo.Application.csproj
- C# Nullable Reference Types
- IQuery
- NRT Migration Playbook Reference
- .MapToInterests
- ControllerBase
- Anti-Patterns to Avoid
- TokenResponseDto
- Report
- ChatHub
- IMatchHub
- ResultFilter
- Anti-Patterns and Reflection Avoidance
- .GetUserId
- Avoid Reflection-Based Metaprogramming
- Trivo
- .CreateMatchAsync
- Performance and API Design Patterns
- Value Objects and Pattern Matching
- SkillWithIdDto
- PagedResult
- MatchNotifier
- GetActiveUsersCountQueryHandler
- .Handle
- .Handle
- .CreateReportAsync
- .GetExpertIdAsync
- .GetRecruiterIdAsync
- AuthenticationService
- EmailSetting
- Trivo.Infrastructure.Persistence.csproj
- The `field` Keyword (C# 14 / .NET 10) and Nullability
- MessageStatus
- Trivo.Domain.csproj
- OpenAiEmbeddingService
- .ToEntity
- UserMappingExtensions.cs
- .Handle
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
- JwtSetting
- ReportStatus
- IGenericRepository
- Core Nullability Model
- Arquitectura CQRS de Trivo — mandato para nuevas features
- Composition and Error Handling
- Language Patterns
- CacheEntryOptions
- Chat
- Known Static-Analysis Limitations and Safe Patterns
- Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`
- UserInterestConfig
- Preconditions: `AllowNull` and `DisallowNull`
- Performance Patterns
- CLAUDE.md
- .Handle
- ExpertRepository
- User
- .NotFound
- .Handle
- .Handle
- UnbanUserCommandHandler
- .GetDetailsByUserIdsAsync
- .Handle
- IMessageRepository
- ExpertController.cs
- NotificationHub
- GetReportedUsersCountQueryHandler
- .Handle
- .ToDto
- .Handle
- InterestWithIdDto
- GetUsersByInterestsAndSkillsQuery
- SendFileCommand
- ICommand
- IUserRecommendationHub
- IMatchNotifier
- NotificationDto
- BaseEntity
- MissingByMatching
- ChatUser

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
- `UserController` --references--> `IEmailValidationService`  [EXTRACTED]
  src/API/Trivo.API/Controllers/V1/UserController.cs → src/Application/Trivo.Application/Interfaces/Services/IEmailValidationService.cs
- `ICommand` --references--> `Result`  [EXTRACTED]
  src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs → src/Application/Trivo.Application/Utils/Result.cs
- `ICommand` --references--> `ResultT`  [EXTRACTED]
  src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs → src/Application/Trivo.Application/Utils/Result.cs

## Import Cycles
- None detected.

## Communities (186 total, 3 thin omitted)

### Community 0 - "UserRepository"
Cohesion: 0.20
Nodes (12): CancellationToken, Distance, Expert, Guid, IEnumerable, IReadOnlyList, List, Recruiter (+4 more)

### Community 1 - "Trivo.Application.Abstractions.Messages"
Cohesion: 0.12
Nodes (8): Trivo.Application.Abstractions.Messages, Trivo.Application.Interfaces.UnitOfWork, Trivo.Application.DTOs.Email, Trivo.Application.Utils, Trivo.Application.Caching, Trivo.Application.Features.Users.Events, Trivo.Application.Interfaces.Repository.Account, Trivo.Application.Interfaces.Services

### Community 2 - "Message"
Cohesion: 0.10
Nodes (24): DateTime, Guid, ICollection, Message, Chat, ChatId, Content, CreatedAt (+16 more)

### Community 3 - "InterestCategory"
Cohesion: 0.06
Nodes (39): Authorize, CancellationToken, HttpGet, HttpPost, ISender, ProducesResponseType, Task, InterestCategoryController (+31 more)

### Community 4 - "Trivo.Application.DTOs.Users"
Cohesion: 0.07
Nodes (19): Trivo.Application.Features.Users.Commands.UpdatePassword, Trivo.Application.Features.Users.Commands.CreateUser, Trivo.Application.Features.Users.Commands.UpdateBiography, Trivo.Application.Features.Interests.Commands.CreateInterest, Trivo.Application.Features.Users.Query.GetUserDetails, Trivo.Application.Features.Skills, Trivo.Application.Features.Users.Commands.UpdateProfilePicture, Trivo.Application.Features.Users.Commands.UpdateUser (+11 more)

### Community 5 - "Trivo.Domain.Models"
Cohesion: 0.12
Nodes (9): Trivo.Domain.Common, Trivo.Infrastructure.Persistence.Context, Trivo.Infrastructure.Persistence.Repository.Account, Trivo.Application.Interfaces.Repository.Base, Trivo.Infrastructure.Persistence.Services, Trivo.Infrastructure.Persistence.Repository, Trivo.Domain.Models, Trivo.Application.Interfaces.Repository (+1 more)

### Community 6 - "IAuthenticationService"
Cohesion: 0.16
Nodes (11): CancellationToken, HttpPost, ProducesResponseType, SwaggerOperation, Task, AuthController, RefreshTokenRequest, RefreshToken (+3 more)

### Community 7 - "Trivo.Infrastructure.Persistence.Configurations"
Cohesion: 0.11
Nodes (12): Trivo.Infrastructure.Persistence.Configurations, IEntityTypeConfiguration, EntityTypeBuilder, AdministratorConfig, EntityTypeBuilder, InterestConfig, EntityTypeBuilder, MatchConfig (+4 more)

### Community 8 - ".Handle"
Cohesion: 0.16
Nodes (12): DateTime, Guid, AdminDto, Administrator, AdminMapper, IFormFile, CreateAdminCommand, CancellationToken (+4 more)

### Community 9 - "IUserRepository"
Cohesion: 0.22
Nodes (10): CancellationToken, Distance, Guid, IEnumerable, IReadOnlyList, List, Task, User (+2 more)

### Community 10 - "Trivo.Application.Interfaces.SignalR"
Cohesion: 0.09
Nodes (11): Trivo.Application.Features.Messages.Commands.SendImage, Trivo.Application.Features.Messages.Query.GetMessagePagination, Trivo.Application.Features.Chat.Query.GetChatPagination, Trivo.Application.Features.Chat, Trivo.Application.Interfaces.SignalR, Trivo.Application.Features.Chat.Commands.CreateChat, Trivo.Infrastructure.Shared.SignalR, Trivo.Application.Features.Messages.Commands.SendMessage (+3 more)

### Community 11 - ".UpdateExpertAsync"
Cohesion: 0.18
Nodes (10): Authorize, CancellationToken, Guid, HttpPost, HttpPut, ISender, ProducesResponseType, Task (+2 more)

### Community 12 - "ResultT"
Cohesion: 0.21
Nodes (13): Authorize, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IEnumerable, ISender (+5 more)

### Community 13 - "Trivo.Infrastructure.Persistence.Migrations"
Cohesion: 0.05
Nodes (29): Trivo.Infrastructure.Persistence.Migrations, Migration, ModelSnapshot, DateTime, Guid, MigrationBuilder, Vector, DateTime (+21 more)

### Community 14 - "Notification"
Cohesion: 0.11
Nodes (21): CancellationToken, Guid, Task, INotificationRepository, DateTime, Guid, Notification, Content (+13 more)

### Community 15 - "ExpertDto"
Cohesion: 0.12
Nodes (16): Guid, ExpertDto, Guid, CreateExpertCommand, CancellationToken, ILogger, Task, CreateExpertCommandHandler (+8 more)

### Community 16 - "Recruiter"
Cohesion: 0.25
Nodes (7): Guid, ICollection, Recruiter, CompanyName, Matches, User, UserId

### Community 17 - ".AddRepositories"
Cohesion: 0.20
Nodes (12): IUserSkillRepository, IGetExpertIdService, IGetRecruiterIdService, IUserRoleService, IConfiguration, IConnectionMultiplexer, IServiceCollection, DependencyInjection (+4 more)

### Community 18 - "IChatHub"
Cohesion: 0.33
Nodes (4): Guid, IEnumerable, Task, IChatHub

### Community 19 - "Code"
Cohesion: 0.10
Nodes (23): CancellationToken, Guid, Task, ICodeRepository, DateTime, Guid, Code, CodeId (+15 more)

### Community 20 - "BHD.ResultPattern — Guía de arquitectura e implementación"
Cohesion: 0.06
Nodes (31): 1. Arquitectura general, 2.1. Estructura de carpetas, 2.2. `BHD.ResultPattern.csproj`, 2.3. `Result.cs`, 2.4. `Error.cs`, 2. Núcleo: `BHD.ResultPattern` (sin dependencias), 3.1. Estructura de carpetas, 3.2. `BHD.ResultPattern.AspNetCore.csproj` (+23 more)

### Community 21 - "MatchDetailsDto"
Cohesion: 0.24
Nodes (10): DateTime, Guid, MatchDetailsDto, Guid, UpdateMatchingCommand, CancellationToken, Guid, ILogger (+2 more)

### Community 22 - "User"
Cohesion: 0.07
Nodes (27): ICollection, Vector, User, Biography, ChatUsers, Codes, Email, Experts (+19 more)

### Community 23 - "Dependency Injection Patterns"
Cohesion: 0.05
Nodes (38): Advanced DI Patterns, Akka.DependencyInjection Reference, Akka.Hosting.TestKit, Akka.NET Actor Scope Management, Common Patterns, Conditional Registration, Contents, Factory-Based Registration (+30 more)

### Community 24 - "Trivo.Domain.Enums"
Cohesion: 0.13
Nodes (7): Trivo.Application.Features.Matching.Commands.UpdateMatch, Trivo.Application.Features.Users, Trivo.Application.Features.Matching.Commands.CreateMatch, Trivo.Domain.Enums, Trivo.Application.Features.Matching.Commands.CreateMatchRejection, Trivo.Application.Features.Matching.Query.GetMatchByUser, Trivo.Application.DTOs.Matching

### Community 25 - ".Conflict"
Cohesion: 0.11
Nodes (20): DateTime, Guid, CodeDto, CancellationToken, Guid, Task, ICodeService, CodeGenerator (+12 more)

### Community 26 - "ICommandHandler"
Cohesion: 0.14
Nodes (14): ICommandHandler, Guid, CreateChatCommand, UserId, CancellationToken, ILogger, Task, CreateChatCommandHandler (+6 more)

### Community 27 - "UserDto"
Cohesion: 0.15
Nodes (15): Guid, UserDto, IEnumerable, GetLast10BannedUsersQuery, CancellationToken, IEnumerable, ILogger, Task (+7 more)

### Community 28 - "Recruiter"
Cohesion: 0.14
Nodes (19): Guid, RecruiterMapper, CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task (+11 more)

### Community 29 - "IAdministratorRepository"
Cohesion: 0.05
Nodes (41): DateTime, Guid, AdminMatchDto, ExpertMatchDto, RecruiterMatchDto, AdminMatchMapper, GetLatestMatchesQuery, CancellationToken (+33 more)

### Community 30 - ".AddServices"
Cohesion: 0.12
Nodes (15): Trivo.Application.Features.Users.Commands.ForgotPassword, EmailResponseDto, ForgotPasswordCommand, CancellationToken, ILogger, Task, ForgotPasswordCommandHandler, ForgotPasswordValidator (+7 more)

### Community 31 - "NotificationService"
Cohesion: 0.29
Nodes (8): Obsolete, Guid, CreateNotificationDto, CancellationToken, Guid, ILogger, Task, NotificationService

### Community 32 - "AdminController"
Cohesion: 0.30
Nodes (11): Authorize, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IEnumerable, ISender (+3 more)

### Community 33 - "NotificationNotifier"
Cohesion: 0.24
Nodes (9): Guid, IEnumerable, Task, INotificationNotifier, Guid, IEnumerable, IHubContext, Task (+1 more)

### Community 34 - "ChatRepository"
Cohesion: 0.31
Nodes (8): CancellationToken, Chat, Guid, IEnumerable, IReadOnlyList, Task, User, ChatRepository

### Community 35 - "Match"
Cohesion: 0.12
Nodes (18): Guid, List, ExpertAiRecommendationDto, DateTime, Guid, MatchDto, Guid, List (+10 more)

### Community 36 - "UserSkill"
Cohesion: 0.14
Nodes (14): CancellationToken, Guid, List, Task, Guid, UserSkill, Skill, SkillId (+6 more)

### Community 37 - "InterestRepository"
Cohesion: 0.31
Nodes (7): CancellationToken, Guid, IEnumerable, Interest, List, Task, InterestRepository

### Community 38 - "Entity Framework Core Patterns"
Cohesion: 0.05
Nodes (38): 1. Forgetting to Update When NoTracking, 2. N+1 Query Problem, 3. Tracking Conflicts with Multiple DbContext Instances, 4. Not Using Async Consistently, 5. Querying Inside Loops, Actors / Long-Lived Objects (Factory Pattern), AppHost Configuration, Applying Migrations (+30 more)

### Community 39 - "ExceptionHandlingMiddleware"
Cohesion: 0.08
Nodes (18): Trivo.API.Middlewares, Trivo.API.Extensions, IApplicationBuilder, IEndpointRouteBuilder, IHostEnvironment, ProblemDetails, RequestDelegate, IConfiguration (+10 more)

### Community 40 - ".CreateMatchNotificationAsync"
Cohesion: 0.29
Nodes (8): HttpDelete, Authorize, CancellationToken, Guid, HttpPost, HttpPut, Task, NotificationController

### Community 41 - "MessageDto"
Cohesion: 0.12
Nodes (18): DateTime, Guid, MessageDto, Guid, IFormFile, SendImageCommand, UserId, CancellationToken (+10 more)

### Community 42 - ".AddAiService"
Cohesion: 0.16
Nodes (10): JwtResponse, AiSetting, ApiKey, EmbeddingModel, Provider, CloudinarySetting, CloudinaryUrl, IConfiguration (+2 more)

### Community 43 - "AbstractValidator"
Cohesion: 0.12
Nodes (9): AbstractValidator, CreateChatValidator, UpdateMatchingValidation, SendFileValidator, SendImageValidator, SendMessageValidator, GetMessagePaginationValidator, CreateRecruiterValidator (+1 more)

### Community 44 - "Interest"
Cohesion: 0.12
Nodes (15): Guid, InterestByCategoryIdDto, Guid, IEnumerable, Interest, InterestMapper, Guid, ICollection (+7 more)

### Community 45 - "Trivo.API.csproj"
Cohesion: 0.11
Nodes (17): Asp.Versioning.Mvc (8.1.0), AspNetCore.HealthChecks.Redis (9.0.0), Microsoft.AspNetCore.OpenApi (8.0.10), Microsoft.AspNetCore.SignalR.Core (1.2.0), Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore (8.0.10), Scalar.AspNetCore (2.17.1), Serilog.Enrichers.Environment (3.0.1), Serilog.Enrichers.Thread (4.0.0) (+9 more)

### Community 46 - "ICacheService"
Cohesion: 0.07
Nodes (29): Guid, IUserOwnedRequest, UserId, CancellationToken, ILogger, IPublisher, Task, UpdateInterestCommandHandler (+21 more)

### Community 47 - "IInterestRepository"
Cohesion: 0.34
Nodes (6): CancellationToken, Guid, IEnumerable, List, Task, IInterestRepository

### Community 48 - "MatchHub"
Cohesion: 0.29
Nodes (6): Hub, Exception, ILogger, IMediator, Task, MatchHub

### Community 49 - "Public API Design and Compatibility"
Cohesion: 0.06
Nodes (31): Anti-Patterns, API Approval Testing, API Change Guidelines, Benefits, Breaking Changes Disguised as Fixes, Chesterton's Fence, Deprecation Pattern, Encapsulation Patterns (+23 more)

### Community 50 - "CacheKeys"
Cohesion: 0.27
Nodes (3): Guid, IEnumerable, CacheKeys

### Community 51 - "IChatRepository"
Cohesion: 0.25
Nodes (9): CancellationToken, Task, CancellationToken, Guid, IEnumerable, IReadOnlyList, Task, IChatRepository (+1 more)

### Community 52 - ".GetByCategoriesAsync"
Cohesion: 0.23
Nodes (11): Authorize, CancellationToken, Guid, HttpGet, HttpPost, IEnumerable, ISender, List (+3 more)

### Community 53 - ".SaveChangesAsync"
Cohesion: 0.12
Nodes (15): Guid, BanUserCommand, CancellationToken, ILogger, Task, BanUserCommandHandler, BanUserValidator, Guid (+7 more)

### Community 54 - "RecruiterController.cs"
Cohesion: 0.10
Nodes (12): Trivo.API.Filters, Trivo.Application.Features.Recruiters.Commands.UpdateRecruiter, Trivo.Application.Features.Recruiters, Trivo.Application.Helpers, Trivo.Infrastructure.Shared, Trivo.Application.Behaviors, Trivo.Application.DTOs.Recruiter, Trivo.Application.Services (+4 more)

### Community 55 - ".Handle"
Cohesion: 0.19
Nodes (13): Guid, IEnumerable, GetMatchByUserQuery, CancellationToken, Dictionary, Func, Guid, IEnumerable (+5 more)

### Community 56 - ".Handle"
Cohesion: 0.15
Nodes (11): INotification, INotificationHandler, Guid, UserProfileChangedEvent, CancellationToken, ILogger, Task, UserProfileChangedEventHandler (+3 more)

### Community 57 - "Trivo.API.Controllers.V1.Requests"
Cohesion: 0.08
Nodes (16): Trivo.API.Controllers.V1.Requests, ChangePasswordRequest, Guid, List, FilterUsersByInterestsAndSkillsRequest, UpdateBiographyRequest, UpdatePasswordRequest, IFormFile (+8 more)

### Community 58 - "Roles"
Cohesion: 0.11
Nodes (17): CancellationToken, Guid, IList, Task, Roles, Administrator, Expert, Recruiter (+9 more)

### Community 59 - "Trivo.Infrastructure.Shared.csproj"
Cohesion: 0.15
Nodes (12): CloudinaryDotNet (1.27.0), MailKit (4.17.0), Microsoft.AspNetCore.Authentication.JwtBearer (8.0.10), Microsoft.AspNetCore.SignalR (1.2.0), Microsoft.Extensions.Options (10.0.3), Microsoft.Extensions.Options.ConfigurationExtensions (8.0.0), Microsoft.IdentityModel.Tokens (8.10.0), MimeKit (4.17.0) (+4 more)

### Community 60 - "IUnitOfWork"
Cohesion: 0.11
Nodes (19): Guid, RecruiterDto, Guid, CreateRecruiterCommand, CancellationToken, ILogger, Task, CreateRecruiterCommandHandler (+11 more)

### Community 61 - ".Handle"
Cohesion: 0.13
Nodes (11): Guid, IFormFile, List, CreateUserCommand, CancellationToken, ILogger, IPublisher, Task (+3 more)

### Community 62 - "Database Performance Patterns"
Cohesion: 0.07
Nodes (26): Always Apply Row Limits, Architecture, AsNoTracking for Read Queries, Avoid Cartesian Explosions, Avoid N+1 Queries, Configure Default Behavior, Constrain Column Sizes, Core Principles (+18 more)

### Community 63 - ".Handle"
Cohesion: 0.28
Nodes (7): IEnumerable, SearchInterestsByNameQuery, CancellationToken, IEnumerable, ILogger, Task, SearchInterestsByNameQueryHandler

### Community 64 - "TrivoContext"
Cohesion: 0.08
Nodes (23): DbContext, DbContextOptions, DbSet, CancellationToken, ModelBuilder, Task, TrivoContext, Administrators (+15 more)

### Community 65 - ".Handle"
Cohesion: 0.14
Nodes (13): Guid, CreateMatchRejectionCommand, CreatedBy, ExpertId, RecruiterId, CancellationToken, Dictionary, expertStatus (+5 more)

### Community 66 - "UserInterest"
Cohesion: 0.16
Nodes (16): CancellationToken, Guid, List, Task, IUserInterestRepository, Guid, UserInterest, Interest (+8 more)

### Community 67 - "Skill"
Cohesion: 0.05
Nodes (43): Authorize, CancellationToken, HttpGet, HttpPost, IEnumerable, ISender, ProducesResponseType, Task (+35 more)

### Community 68 - "Slopwatch: LLM Anti-Cheat for .NET"
Cohesion: 0.09
Nodes (22): After Every Code Change, As a Global Tool, As a Local Tool (Recommended), Azure Pipelines, CI/CD Integration, Claude Code Hook Integration, Common Slop Patterns, Configuration (+14 more)

### Community 69 - "UserAiRecommendationDto"
Cohesion: 0.22
Nodes (12): Guid, List, UserAiRecommendationDto, Guid, IEnumerable, Task, IAiNotifier, Guid (+4 more)

### Community 70 - ".Validate"
Cohesion: 0.15
Nodes (10): CancellationToken, Expression, Func, Task, IValidation, CancellationToken, Expression, Func (+2 more)

### Community 71 - "Expert"
Cohesion: 0.14
Nodes (12): Guid, ExpertMapper, Guid, ICollection, Expert, AvailableForProjects, IsHired, Matches (+4 more)

### Community 72 - "Trivo.Application.DTOs.Administrator"
Cohesion: 0.09
Nodes (14): Trivo.API.Controllers.V1, Trivo.Application.Features.Administrator.Query.GetCompletedMatchesCount, Trivo.Infrastructure.Shared.Services, Trivo.Application.Features.Administrator.Commands.CreateAdministrator, Trivo.Domain.Configurations, Trivo.Application.Features.Administrator.Query.GetReportedUsersCount, Trivo.Application.Features.Administrator.Query.GetActiveUsersCount, Trivo.Application.DTOs.Administrator (+6 more)

### Community 73 - ".GetOrSetAsync"
Cohesion: 0.18
Nodes (11): IDatabase, CancellationToken, Func, Task, CancellationToken, Func, IConnectionMultiplexer, IEnumerable (+3 more)

### Community 74 - "GoogleGeminiEmbeddingService"
Cohesion: 0.16
Nodes (14): ContentPart, EmbedContentResponse, EmbeddingValues, HttpClient, CancellationToken, ILogger, Task, ContentPart (+6 more)

### Community 75 - "Módulo de Matchmaking con IA — Implementación"
Cohesion: 0.14
Nodes (13): 1. Problema de negocio, 2.1 Abstracción del proveedor — `IEmbeddingService`, 2.2 Construcción del texto — `UserProfileTextBuilder`, 2.3 Cuándo se regenera el embedding — `UserProfileChangedEvent`, 2. Arquitectura, 3. Cambios de esquema (tablas), 4. El algoritmo de recomendación, 5. Qué NO se cachea, y por qué (+5 more)

### Community 76 - "Plan de implementación — Embeddings vía API externa para emparejamiento por afinidad"
Cohesion: 0.14
Nodes (13): 0. Decisión de proveedor y por qué la interfaz debe ser agnóstica, 10. Pruebas, 1. Infraestructura de base de datos (bloqueante, va primero), 2. Dominio (`Trivo.Domain`), 3. Application (`Trivo.Application`), 4. Infrastructure.Shared — implementación del proveedor, 5. Infrastructure.Persistence — columna, mapping e índice, 6. Cuándo se genera/regenera el embedding (+5 more)

### Community 77 - "Error"
Cohesion: 0.15
Nodes (7): PaginationError, ErrorType, Error, Code, Description, ErrorType, StatusCode

### Community 78 - "UserRecommendationHub"
Cohesion: 0.29
Nodes (5): Exception, ILogger, IMediator, Task, UserRecommendationHub

### Community 79 - "Nullable Attributes Reference"
Cohesion: 0.17
Nodes (12): Attribute Catalog, `[DoesNotReturn]`, `[DoesNotReturnIf(bool)]`, Helper methods: `MemberNotNull` and `MemberNotNullWhen`, `[MaybeNull]`, `[MemberNotNull]`, `[MemberNotNullWhen(bool)]`, `[NotNull]` (+4 more)

### Community 80 - "IMatchRepository"
Cohesion: 0.15
Nodes (21): AcceptedIds, CancellationToken, Guid, IEnumerable, IReadOnlyList, Match, RejectedIds, Task (+13 more)

### Community 81 - "Modern C# Coding Standards"
Cohesion: 0.17
Nodes (12): Additional Resources, Avoid Reflection-Based Metaprogramming, Best Practices Summary, Code Organization, Composition Over Inheritance, Core Principles, DO's, DON'Ts (+4 more)

### Community 82 - ".RestrictToBestStructuralMatches"
Cohesion: 0.29
Nodes (7): Candidates, HasOverlap, Distance, Guid, IReadOnlyList, List, User

### Community 83 - ".ValidateEmailAsync"
Cohesion: 0.22
Nodes (8): IServiceCollection, CancellationToken, Task, IEmailValidationService, CancellationToken, ILogger, Task, EmailValidationService

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

### Community 89 - ".Handle"
Cohesion: 0.18
Nodes (10): Trivo.Application.Features.Skills.Commands.UpdateSkill, Guid, List, UpdateSkillCommand, CancellationToken, ILogger, IPublisher, Task (+2 more)

### Community 90 - "ChatDto"
Cohesion: 0.14
Nodes (20): DateTime, Guid, List, ChatDto, Guid, GetChatPaginationQuery, CancellationToken, ILogger (+12 more)

### Community 91 - "Trivo.Application.Pagination"
Cohesion: 0.07
Nodes (14): Trivo.Application.Features.Skills.Query.GetSkillsPagination, Trivo.Application.DTOs.InterestCategories, Trivo.Application.Features.Administrator.Query.GetLatestMatches, Trivo.Application.Features.Administrator.Query.GetLatestUsersPaged, Trivo.Application.Features.Interests.Query.GetInterestsByCategoryId, Trivo.Application.Features.Matching, Trivo.Application.Features.InterestCategories.Commands.CreateInterestCategory, Trivo.Application.Pagination (+6 more)

### Community 92 - ".Handle"
Cohesion: 0.14
Nodes (12): IHttpContextAccessor, IPipelineBehavior, IValidator, CancellationToken, RequestHandlerDelegate, Task, AuthorizationBehavior, CancellationToken (+4 more)

### Community 93 - "Trivo.Application.csproj"
Cohesion: 0.20
Nodes (9): BCrypt.Net-Next (4.0.3), FluentValidation (11.10.0), FluentValidation.DependencyInjectionExtensions (11.10.0), Microsoft.Extensions.DependencyInjection.Abstractions (8.0.2), net8.0, MediatR (12.2.0), Microsoft.Extensions.Caching.StackExchangeRedis (8.0.10), Serilog.AspNetCore (8.0.0) (+1 more)

### Community 94 - "C# Nullable Reference Types"
Cohesion: 0.20
Nodes (10): API Design Rules (Signatures), C# Nullable Reference Types, Core Goals, Generation Checklist (Summary), Project Configuration, Public API compatibility for libraries, Reference Files, References (+2 more)

### Community 95 - "IQuery"
Cohesion: 0.13
Nodes (17): IRequest, IRequestHandler, IQuery, IQueryHandler, CompletedMatchesCountDto, UserProfilePictureDto, GetCompletedMatchesCountQuery, CancellationToken (+9 more)

### Community 96 - "NRT Migration Playbook Reference"
Cohesion: 0.22
Nodes (6): Full Generation Checklist, Gradual annotation of a library, Legacy and Unannotated API Interop, NRT Migration Playbook Reference, Trust annotated libraries, Wrapping unannotated or legacy APIs

### Community 97 - ".MapToInterests"
Cohesion: 0.47
Nodes (4): ICollection, List, User, UserMapper

### Community 98 - "ControllerBase"
Cohesion: 0.11
Nodes (17): ControllerBase, Authorize, CancellationToken, HttpPost, ISender, Task, ChatController, Authorize (+9 more)

### Community 99 - "Anti-Patterns to Avoid"
Cohesion: 0.25
Nodes (8): Anti-Patterns to Avoid, Don't: Block on async code, Don't: Create deep inheritance hierarchies, Don't: Forget CancellationToken in async methods, Don't: Return List<T> when you mean IReadOnlyList<T>, Don't: Use byte[] when ReadOnlySpan<byte> works, Don't: Use classes for value objects, Don't: Use mutable DTOs

### Community 100 - "TokenResponseDto"
Cohesion: 0.19
Nodes (9): TokenResponseDto, AccessToken, RefreshToken, AdminLoginCommand, CancellationToken, ILogger, Task, AdminLoginCommandHandler (+1 more)

### Community 101 - "Report"
Cohesion: 0.18
Nodes (11): IReportRepository, Guid, Report, Message, MessageId, Note, ReportedById, ReportId (+3 more)

### Community 102 - "ChatHub"
Cohesion: 0.28
Nodes (6): Exception, Guid, ILogger, IMediator, Task, ChatHub

### Community 103 - "IMatchHub"
Cohesion: 0.36
Nodes (4): Guid, IEnumerable, Task, IMatchHub

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
Cohesion: 0.20
Nodes (9): Building the Docker image, Health checks, Logging, Prerequisites, Project structure, Running locally, Running the full stack in "production" mode, Trivo (+1 more)

### Community 109 - ".CreateMatchAsync"
Cohesion: 0.36
Nodes (7): Authorize, CancellationToken, HttpPost, HttpPut, ISender, Task, MatchController

### Community 110 - "Performance and API Design Patterns"
Cohesion: 0.29
Nodes (6): Accept Abstractions, Return Appropriately Specific, API Design Principles, Contents, Method Signatures Best Practices, Performance and API Design Patterns, Span<T> and Memory<T> for Zero-Allocation Code

### Community 111 - "Value Objects and Pattern Matching"
Cohesion: 0.29
Nodes (7): Constraint-Enforcing Value Objects, Contents, No Implicit Conversions, Pattern Matching (C# 8-12), TypeConverter Support for Configuration Binding, Value Objects and Pattern Matching, Value Objects as readonly record struct

### Community 112 - "SkillWithIdDto"
Cohesion: 0.23
Nodes (10): Guid, SkillWithIdDto, Guid, IEnumerable, GetUserSkillsQuery, CancellationToken, IEnumerable, ILogger (+2 more)

### Community 113 - "PagedResult"
Cohesion: 0.15
Nodes (14): Guid, IEnumerable, GetInterestsByCategoryIdQuery, CancellationToken, ILogger, Task, GetInterestsByCategoryIdQueryHandler, GetInterestsByCategoryIdValidator (+6 more)

### Community 114 - "MatchNotifier"
Cohesion: 0.42
Nodes (5): Guid, IEnumerable, IHubContext, Task, MatchNotifier

### Community 115 - "GetActiveUsersCountQueryHandler"
Cohesion: 0.36
Nodes (6): ActiveUsersCountDto, GetActiveUsersCountQuery, CancellationToken, ILogger, Task, GetActiveUsersCountQueryHandler

### Community 116 - ".Handle"
Cohesion: 0.24
Nodes (7): Guid, GetUserRecommendationsQuery, CancellationToken, Task, GetUserRecommendationsValidator, IEnumerable, UserProfileTextBuilder

### Community 117 - ".Handle"
Cohesion: 0.19
Nodes (9): Guid, InterestDetailsDto, Guid, CreateInterestCommand, CancellationToken, ILogger, Task, CreateInterestCommandHandler (+1 more)

### Community 118 - ".CreateReportAsync"
Cohesion: 0.09
Nodes (19): Trivo.Application.Features.Reports.Commands.CreateReport, Trivo.Application.Features.Reports, Trivo.Application.DTOs.Reports, Authorize, CancellationToken, HttpPost, ISender, ProducesResponseType (+11 more)

### Community 119 - ".GetExpertIdAsync"
Cohesion: 0.25
Nodes (6): CancellationToken, Guid, Task, CancellationToken, Guid, Task

### Community 120 - ".GetRecruiterIdAsync"
Cohesion: 0.25
Nodes (6): CancellationToken, Guid, Task, CancellationToken, Guid, Task

### Community 121 - "AuthenticationService"
Cohesion: 0.26
Nodes (6): Administrator, CancellationToken, IOptions, Task, User, AuthenticationService

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

### Community 127 - "OpenAiEmbeddingService"
Cohesion: 0.33
Nodes (5): EmbeddingClient, CancellationToken, ILogger, Task, OpenAiEmbeddingService

### Community 128 - ".ToEntity"
Cohesion: 0.33
Nodes (4): Trivo.Application.Features.Administrator.Commands.CreateAdministrator.Mappings, Administrator, CreateAdminCommand, AdminMappingExtensions

### Community 129 - "UserMappingExtensions.cs"
Cohesion: 0.33
Nodes (4): Trivo.Application.Features.Users.Commands.CreateUser.Mappings, CreateUserCommand, User, UserMappingExtensions

### Community 130 - ".Handle"
Cohesion: 0.18
Nodes (10): Guid, CreateMatchingCommand, CancellationToken, Dictionary, expertStatus, ILogger, recruiterStatus, Task (+2 more)

### Community 131 - "ErrorType"
Cohesion: 0.14
Nodes (13): ErrorType, Conflict, Custom, ExternalService, Failure, Forbidden, NotFound, Timeout (+5 more)

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

### Community 150 - "CacheEntryOptions"
Cohesion: 0.18
Nodes (9): IReadOnlyList, CacheEntryOptions, AbsoluteExpiration, Tags, CacheProfiles, Cold, Hot, Warm (+1 more)

### Community 151 - "Chat"
Cohesion: 0.20
Nodes (8): ICollection, Chat, ChatType, ChatUsers, IsActive, Messages, EntityTypeBuilder, ChatConfig

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

### Community 158 - ".Handle"
Cohesion: 0.24
Nodes (7): Trivo.Application.Features.Users.Commands.LoginUser, LoginUserCommand, CancellationToken, ILogger, Task, LoginUserCommandHandler, LoginUserCommandValidator

### Community 159 - "ExpertRepository"
Cohesion: 0.36
Nodes (7): CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task, ExpertRepository

### Community 160 - "User"
Cohesion: 0.27
Nodes (4): UserHelper, User, EntityTypeBuilder, UserConfig

### Community 161 - ".NotFound"
Cohesion: 0.20
Nodes (8): Trivo.Application.Features.Users.Commands.ConfirmAccount, Guid, ConfirmAccountCommand, CancellationToken, ILogger, Task, ConfirmAccountCommandHandler, ConfirmAccountValidator

### Community 162 - ".Handle"
Cohesion: 0.22
Nodes (8): List, ExpertDetailsDto, List, RecruiterDetailsDto, List, UserDetailsDto, CancellationToken, Task

### Community 163 - ".Handle"
Cohesion: 0.24
Nodes (8): UpdateUserDto, Guid, UpdateUserCommand, CancellationToken, ILogger, Task, UpdateUserCommandHandler, UpdateUserValidator

### Community 164 - "UnbanUserCommandHandler"
Cohesion: 0.25
Nodes (7): Guid, UnbanUserCommand, CancellationToken, ILogger, Task, UnbanUserCommandHandler, UnbanUserCommandValidator

### Community 165 - ".GetDetailsByUserIdsAsync"
Cohesion: 0.36
Nodes (6): CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task

### Community 166 - ".Handle"
Cohesion: 0.25
Nodes (7): Guid, CreateSkillCommand, CancellationToken, ILogger, Task, CreateSkillCommandHandler, CreateSkillValidator

### Community 167 - "IMessageRepository"
Cohesion: 0.19
Nodes (13): ILogger, SendFileCommandHandler, Guid, GetMessagePaginationQuery, CancellationToken, ILogger, Task, GetMessagePaginationQueryHandler (+5 more)

### Community 168 - "ExpertController.cs"
Cohesion: 0.36
Nodes (4): Trivo.Application.DTOs.Expert, Trivo.Application.Features.Experts.Commands.CreateExpert, Trivo.Application.Features.Experts, Trivo.Application.Features.Experts.Commands.UpdateExpert

### Community 169 - "NotificationHub"
Cohesion: 0.22
Nodes (9): CancellationToken, Guid, Task, INotificationService, Exception, Guid, ILogger, Task (+1 more)

### Community 170 - "GetReportedUsersCountQueryHandler"
Cohesion: 0.36
Nodes (6): ReportedUsersCountDto, GetReportedUsersCountQuery, CancellationToken, ILogger, Task, GetReportedUsersCountQueryHandler

### Community 171 - ".Handle"
Cohesion: 0.31
Nodes (7): UserBiographyDto, Guid, GetUserBiographyQuery, CancellationToken, ILogger, Task, GetUserBiographyQueryHandler

### Community 172 - ".ToDto"
Cohesion: 0.25
Nodes (6): Guid, UserChatDto, Chat, Guid, User, ChatMapper

### Community 173 - ".Handle"
Cohesion: 0.25
Nodes (7): Guid, UpdatePasswordCommand, CancellationToken, ILogger, Task, UpdatePasswordCommandHandler, UpdatePasswordValidator

### Community 174 - "InterestWithIdDto"
Cohesion: 0.23
Nodes (10): Guid, InterestWithIdDto, Guid, IEnumerable, GetUserInterestsQuery, CancellationToken, IEnumerable, ILogger (+2 more)

### Community 175 - "GetUsersByInterestsAndSkillsQuery"
Cohesion: 0.25
Nodes (8): Guid, List, GetUsersByInterestsAndSkillsQuery, CancellationToken, ILogger, Task, GetUsersByInterestsAndSkillsQueryHandler, GetUsersByInterestsAndSkillsValidator

### Community 176 - "SendFileCommand"
Cohesion: 0.26
Nodes (10): Authorize, CancellationToken, HttpPost, ISender, Task, MessageController, Guid, IFormFile (+2 more)

### Community 177 - "ICommand"
Cohesion: 0.13
Nodes (13): Trivo.Application.Features.Interests.Commands.UpdateInterest, IBaseCommand, ICommand, Guid, IReadOnlyList, UpdateInterestCommand, UpdateInterestValidator, ResetPasswordCommand (+5 more)

### Community 178 - "IUserRecommendationHub"
Cohesion: 0.60
Nodes (3): IEnumerable, Task, IUserRecommendationHub

### Community 179 - "IMatchNotifier"
Cohesion: 0.50
Nodes (4): Guid, IEnumerable, Task, IMatchNotifier

### Community 180 - "NotificationDto"
Cohesion: 0.13
Nodes (12): Trivo.Application.DTOs.Notifications, Trivo.Application.Features.Notifications, DateTime, Guid, NotificationDto, IEnumerable, List, NotificationMapper (+4 more)

### Community 181 - "BaseEntity"
Cohesion: 0.29
Nodes (6): DateTime, Guid, BaseEntity, CreatedAt, Id, UpdatedAt

### Community 186 - "MissingByMatching"
Cohesion: 0.50
Nodes (3): MissingByMatching, Expert, Recruiter

### Community 187 - "ChatUser"
Cohesion: 0.15
Nodes (11): DateTime, Guid, ChatUser, Chat, ChatId, ChatName, JoinedAt, LeftAt (+3 more)

## Knowledge Gaps
- **566 isolated node(s):** `net8.0`, `Asp.Versioning.Mvc (8.1.0)`, `AspNetCore.HealthChecks.Redis (9.0.0)`, `MediatR (12.2.0)`, `Microsoft.AspNetCore.OpenApi (8.0.10)` (+561 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **3 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `ResultT` connect `ResultT` to `.Handle`, `InterestCategory`, `IAuthenticationService`, `.Handle`, `.UpdateExpertAsync`, `ExpertDto`, `MatchDetailsDto`, `.Conflict`, `ICommandHandler`, `UserDto`, `IAdministratorRepository`, `.AddServices`, `.Handle`, `AdminController`, `.NotFound`, `.Handle`, `.Handle`, `UnbanUserCommandHandler`, `NotificationService`, `.Handle`, `IMessageRepository`, `.CreateMatchNotificationAsync`, `MessageDto`, `GetReportedUsersCountQueryHandler`, `.Handle`, `NotificationHub`, `.Handle`, `ICacheService`, `InterestWithIdDto`, `SendFileCommand`, `ICommand`, `GetUsersByInterestsAndSkillsQuery`, `IChatRepository`, `.GetByCategoriesAsync`, `.SaveChangesAsync`, `.Handle`, `IUnitOfWork`, `.Handle`, `.Handle`, `.Handle`, `Skill`, `Error`, `.ValidateEmailAsync`, `.Handle`, `.Handle`, `.Handle`, `ChatDto`, `IQuery`, `ControllerBase`, `TokenResponseDto`, `.CreateMatchAsync`, `SkillWithIdDto`, `PagedResult`, `GetActiveUsersCountQueryHandler`, `.Handle`, `.Handle`, `.CreateReportAsync`, `AuthenticationService`?**
  _High betweenness centrality (0.159) - this node is a cross-community bridge._
- **Why does `Trivo.Domain.Models` connect `Trivo.Domain.Models` to `Trivo.Application.Abstractions.Messages`, `Message`, `InterestCategory`, `Trivo.Application.DTOs.Users`, `Trivo.Infrastructure.Persistence.Configurations`, `Trivo.Application.Interfaces.SignalR`, `Notification`, `Recruiter`, `Code`, `Chat`, `Trivo.Domain.Enums`, `UserInterestConfig`, `Recruiter`, `User`, `UserSkill`, `ExpertController.cs`, `NotificationDto`, `RecruiterController.cs`, `ChatUser`, `UserInterest`, `Skill`, `Expert`, `Trivo.Application.DTOs.Administrator`, `Trivo.Application.Pagination`, `Report`, `.CreateReportAsync`?**
  _High betweenness centrality (0.061) - this node is a cross-community bridge._
- **Why does `TrivoContext` connect `TrivoContext` to `UserRepository`, `Message`, `InterestCategory`, `Trivo.Domain.Models`, `Notification`, `.AddRepositories`, `Code`, `Chat`, `Recruiter`, `IAdministratorRepository`, `ExpertRepository`, `User`, `ChatRepository`, `Match`, `UserSkill`, `InterestRepository`, `ExceptionHandlingMiddleware`, `Interest`, `ChatUser`, `IUnitOfWork`, `UserInterest`, `Skill`, `.Validate`, `Expert`, `IMatchRepository`, `Report`?**
  _High betweenness centrality (0.053) - this node is a cross-community bridge._
- **What connects `net8.0`, `Asp.Versioning.Mvc (8.1.0)`, `AspNetCore.HealthChecks.Redis (9.0.0)` to the rest of the system?**
  _566 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Trivo.Application.Abstractions.Messages` be split into smaller, more focused modules?**
  _Cohesion score 0.12202380952380952 - nodes in this community are weakly interconnected._
- **Should `Message` be split into smaller, more focused modules?**
  _Cohesion score 0.09879032258064516 - nodes in this community are weakly interconnected._
- **Should `InterestCategory` be split into smaller, more focused modules?**
  _Cohesion score 0.05628415300546448 - nodes in this community are weakly interconnected._