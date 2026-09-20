# Graph Report - Trivo  (2026-09-12)

## Corpus Check
- 424 files · ~76,157 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 3005 nodes · 6848 edges · 185 communities (175 shown, 10 thin omitted)
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
- .RefreshTokenAsync
- Trivo.Infrastructure.Persistence.Configurations
- .Handle
- IUserRepository
- Trivo.Application.Interfaces.SignalR
- .UpdateExpertAsync
- ResultT
- Trivo.Infrastructure.Persistence.Migrations
- Notification
- IExpertRepository
- Recruiter
- .AddRepositories
- ChatDto
- CodeConfig
- BHD.ResultPattern — Guía de arquitectura e implementación
- MatchDetailsDto
- User
- Dependency Injection Patterns
- Trivo.Domain.Enums
- Code
- ICacheService
- UserDto
- Recruiter
- AdministratorRepository
- .Handle
- PagedResult
- AdminController
- .Validation
- Chat
- MatchDto
- .AddSkillsToUserAsync
- InterestRepository
- Entity Framework Core Patterns
- Error
- .CreateMatchNotificationAsync
- MessageDto
- .AddAiService
- IAdministratorRepository
- Interest
- Trivo.API.csproj
- .NotFound
- IInterestRepository
- MatchHub
- Public API Design and Compatibility
- CacheKeys
- IChatRepository
- .GetByCategoriesAsync
- IMatchRepository
- RecruiterController.cs
- .Handle
- .Handle
- Trivo.API.Controllers.V1.Requests
- Roles
- Trivo.Infrastructure.Shared.csproj
- ControllerBase
- .Handle
- Database Performance Patterns
- InterestCategoryDto
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
- .ValidateEmailAsync
- .Handle
- Plantillas copy-paste — feature CQRS de Trivo
- ICloudinaryService
- SkillWithIdDto
- Polyfilling the nullable attributes for older target frameworks
- .CreateInterestCategoryAsync
- IRealTimeNotifier
- Trivo.Application.DTOs.InterestCategories
- .Handle
- Trivo.Application.csproj
- C# Nullable Reference Types
- .Handle
- NRT Migration Playbook Reference
- User
- .UpdateRecruiterAsync
- Anti-Patterns to Avoid
- TokenResponseDto
- Report
- ChatHub
- .GetDetailsByUserIdsAsync
- ResultFilter
- Anti-Patterns and Reflection Avoidance
- .GetUserId
- Avoid Reflection-Based Metaprogramming
- Trivo
- .CreateMatchAsync
- Performance and API Design Patterns
- Value Objects and Pattern Matching
- .Handle
- .Handle
- InterestCategoryRepository
- IQuery
- GenericRepository
- IInterestCategoryRepository
- .Handle
- .GetExpertIdAsync
- .GetRecruiterIdAsync
- AuthenticationService
- EmailSetting
- Trivo.Infrastructure.Persistence.csproj
- The `field` Keyword (C# 14 / .NET 10) and Nullability
- MessageStatus
- Trivo.Domain.csproj
- .Handle
- .ToEntity
- UserMappingExtensions.cs
- .Handle
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
- UserInterestConfig
- Preconditions: `AllowNull` and `DisallowNull`
- Performance Patterns
- CLAUDE.md
- .Handle
- ExpertRepository
- IAuthenticationService
- ForgotPasswordCommand
- .Handle
- .Handle
- AiSetting
- .GetDetailsByUserIdsAsync
- .ToRecruiterEntity
- .Handle
- ExpertController.cs
- NotificationHub
- EmailTemplate
- .Handle
- MatchUpdateStatus
- .Conflict
- InterestWithIdDto
- IQueryHandler
- AdministratorConfig
- AbstractValidator
- InterestCategoryConfig
- RecruiterConfig
- ReportConfig
- BaseEntity
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
- `UserController` --references--> `IEmailValidationService`  [EXTRACTED]
  src/API/Trivo.API/Controllers/V1/UserController.cs → src/Application/Trivo.Application/Interfaces/Services/IEmailValidationService.cs
- `ICommand` --references--> `Result`  [EXTRACTED]
  src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs → src/Application/Trivo.Application/Utils/Result.cs
- `ICommand` --references--> `ResultT`  [EXTRACTED]
  src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs → src/Application/Trivo.Application/Utils/Result.cs

## Import Cycles
- None detected.

## Communities (185 total, 10 thin omitted)

### Community 0 - "UserRepository"
Cohesion: 0.18
Nodes (14): Expression, Func, CancellationToken, Distance, Expert, Guid, IEnumerable, IReadOnlyList (+6 more)

### Community 1 - "Trivo.Application.Abstractions.Messages"
Cohesion: 0.13
Nodes (8): Trivo.Application.Abstractions.Messages, Trivo.Application.Interfaces.UnitOfWork, Trivo.Application.DTOs.Email, Trivo.Application.Utils, Trivo.Application.Caching, Trivo.Application.Features.Users.Events, Trivo.Application.Interfaces.Repository.Account, Trivo.Application.Interfaces.Services

### Community 2 - "Message"
Cohesion: 0.09
Nodes (31): ILogger, CreateReportCommandHandler, CancellationToken, Guid, List, Task, IMessageRepository, DateTime (+23 more)

### Community 3 - "InterestCategory"
Cohesion: 0.15
Nodes (11): IEnumerable, List, InterestCategoryMapper, DateTime, Guid, ICollection, InterestCategory, CategoryId (+3 more)

### Community 4 - "Trivo.Application.DTOs.Users"
Cohesion: 0.05
Nodes (24): Trivo.Application.Features.Skills.Query.GetSkillsPagination, Trivo.Application.Features.Users.Commands.CreateUser, Trivo.Application.Features.Interests.Commands.CreateInterest, Trivo.Application.Features.Users.Query.GetUserDetails, Trivo.Application.Features.Skills, Trivo.Application.Features.Users.Commands.UpdateUser, Trivo.Application.Features.Users.Commands.ResetPassword, Trivo.Application.Features.Users.Query.GetUserInterests (+16 more)

### Community 5 - "Trivo.Domain.Models"
Cohesion: 0.09
Nodes (12): Trivo.Domain.Common, Trivo.Infrastructure.Persistence.Context, Trivo.Infrastructure.Persistence.Repository.Account, Trivo.Application.Interfaces.Repository.Base, Trivo.Infrastructure.Persistence.Services, Trivo.Application.Features.Administrator.Query.GetLatestUsersPaged, Trivo.Application.Features.Users, Trivo.Infrastructure.Persistence.Repository (+4 more)

### Community 6 - ".RefreshTokenAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, HttpPost, ProducesResponseType, SwaggerOperation, Task, RefreshTokenRequest, RefreshToken

### Community 7 - "Trivo.Infrastructure.Persistence.Configurations"
Cohesion: 0.09
Nodes (14): Trivo.Infrastructure.Persistence.Configurations, IEntityTypeConfiguration, EntityTypeBuilder, ChatConfig, EntityTypeBuilder, ChatUserConfig, EntityTypeBuilder, InterestConfig (+6 more)

### Community 8 - ".Handle"
Cohesion: 0.13
Nodes (13): Trivo.Application.Features.Administrator.Commands.CreateAdministrator, DateTime, Guid, AdminDto, Administrator, AdminMapper, IFormFile, CreateAdminCommand (+5 more)

### Community 9 - "IUserRepository"
Cohesion: 0.22
Nodes (10): CancellationToken, Distance, Guid, IEnumerable, IReadOnlyList, List, Task, User (+2 more)

### Community 10 - "Trivo.Application.Interfaces.SignalR"
Cohesion: 0.07
Nodes (14): Trivo.Application.Features.Messages.Commands.SendImage, Trivo.Application.Features.Messages.Query.GetMessagePagination, Trivo.Application.Features.Chat.Query.GetChatPagination, Trivo.Application.Features.Chat, Trivo.API.Controllers.V1, Trivo.Application.DTOs.Notifications, Trivo.Application.Features.Notifications, Trivo.Application.Interfaces.SignalR (+6 more)

### Community 11 - ".UpdateExpertAsync"
Cohesion: 0.18
Nodes (10): Authorize, CancellationToken, Guid, HttpPost, HttpPut, ISender, ProducesResponseType, Task (+2 more)

### Community 12 - "ResultT"
Cohesion: 0.22
Nodes (13): Authorize, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IEnumerable, ISender (+5 more)

### Community 13 - "Trivo.Infrastructure.Persistence.Migrations"
Cohesion: 0.05
Nodes (29): Trivo.Infrastructure.Persistence.Migrations, Migration, ModelSnapshot, DateTime, Guid, MigrationBuilder, Vector, DateTime (+21 more)

### Community 14 - "Notification"
Cohesion: 0.13
Nodes (19): CancellationToken, Guid, Task, INotificationRepository, DateTime, Guid, Notification, Content (+11 more)

### Community 15 - "IExpertRepository"
Cohesion: 0.06
Nodes (34): Guid, ExpertDto, Guid, RecruiterDto, Guid, CreateExpertCommand, CancellationToken, ILogger (+26 more)

### Community 16 - "Recruiter"
Cohesion: 0.25
Nodes (7): Guid, ICollection, Recruiter, CompanyName, Matches, User, UserId

### Community 17 - ".AddRepositories"
Cohesion: 0.20
Nodes (12): IReportRepository, IGetExpertIdService, IGetRecruiterIdService, IUserRoleService, IConfiguration, IConnectionMultiplexer, IServiceCollection, DependencyInjection (+4 more)

### Community 18 - "ChatDto"
Cohesion: 0.12
Nodes (14): DateTime, Guid, List, ChatDto, Guid, UserChatDto, Chat, Guid (+6 more)

### Community 20 - "BHD.ResultPattern — Guía de arquitectura e implementación"
Cohesion: 0.06
Nodes (31): 1. Arquitectura general, 2.1. Estructura de carpetas, 2.2. `BHD.ResultPattern.csproj`, 2.3. `Result.cs`, 2.4. `Error.cs`, 2. Núcleo: `BHD.ResultPattern` (sin dependencias), 3.1. Estructura de carpetas, 3.2. `BHD.ResultPattern.AspNetCore.csproj` (+23 more)

### Community 21 - "MatchDetailsDto"
Cohesion: 0.12
Nodes (20): DateTime, Guid, MatchDetailsDto, Guid, UpdateMatchingCommand, CancellationToken, Guid, ILogger (+12 more)

### Community 22 - "User"
Cohesion: 0.07
Nodes (27): ICollection, Vector, User, Biography, ChatUsers, Codes, Email, Experts (+19 more)

### Community 23 - "Dependency Injection Patterns"
Cohesion: 0.05
Nodes (38): Advanced DI Patterns, Akka.DependencyInjection Reference, Akka.Hosting.TestKit, Akka.NET Actor Scope Management, Common Patterns, Conditional Registration, Contents, Factory-Based Registration (+30 more)

### Community 24 - "Trivo.Domain.Enums"
Cohesion: 0.21
Nodes (6): Trivo.Application.Features.Matching.Commands.UpdateMatch, Trivo.Application.Features.Matching.Commands.CreateMatch, Trivo.Domain.Enums, Trivo.Application.Features.Matching.Commands.CreateMatchRejection, Trivo.Application.Features.Matching.Query.GetMatchByUser, Trivo.Application.DTOs.Matching

### Community 25 - "Code"
Cohesion: 0.05
Nodes (48): DateTime, Guid, CodeDto, Guid, ConfirmAccountCommand, CancellationToken, ILogger, Task (+40 more)

### Community 26 - "ICacheService"
Cohesion: 0.09
Nodes (32): ICommandHandler, ILogger, BanUserCommandHandler, ILogger, UnbanUserCommandHandler, ILogger, CreateChatCommandHandler, CreateInterestCategoryCommand (+24 more)

### Community 27 - "UserDto"
Cohesion: 0.15
Nodes (15): Guid, UserDto, IEnumerable, GetLast10BannedUsersQuery, CancellationToken, IEnumerable, ILogger, Task (+7 more)

### Community 28 - "Recruiter"
Cohesion: 0.37
Nodes (8): Recruiter, CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task, RecruiterRepository

### Community 29 - "AdministratorRepository"
Cohesion: 0.23
Nodes (8): Administrator, CancellationToken, Guid, IEnumerable, Match, Task, User, AdministratorRepository

### Community 30 - ".Handle"
Cohesion: 0.17
Nodes (11): EmailResponseDto, CancellationToken, ILogger, Task, ForgotPasswordCommandHandler, Task, IEmailService, IOptions (+3 more)

### Community 31 - "PagedResult"
Cohesion: 0.11
Nodes (17): DateTime, Guid, AdminMatchDto, ExpertMatchDto, RecruiterMatchDto, GetLatestMatchesQuery, CancellationToken, ILogger (+9 more)

### Community 32 - "AdminController"
Cohesion: 0.30
Nodes (11): Authorize, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IEnumerable, ISender (+3 more)

### Community 33 - ".Validation"
Cohesion: 0.10
Nodes (26): Guid, CreateNotificationDto, DateTime, Guid, NotificationDto, IEnumerable, List, NotificationMapper (+18 more)

### Community 34 - "Chat"
Cohesion: 0.12
Nodes (23): ICollection, Chat, ChatType, ChatUsers, IsActive, Messages, DateTime, Guid (+15 more)

### Community 35 - "MatchDto"
Cohesion: 0.22
Nodes (10): Guid, List, ExpertAiRecommendationDto, DateTime, Guid, MatchDto, Guid, List (+2 more)

### Community 36 - ".AddSkillsToUserAsync"
Cohesion: 0.21
Nodes (10): CancellationToken, Guid, List, Task, IUserSkillRepository, CancellationToken, Guid, List (+2 more)

### Community 37 - "InterestRepository"
Cohesion: 0.31
Nodes (7): CancellationToken, Guid, IEnumerable, Interest, List, Task, InterestRepository

### Community 38 - "Entity Framework Core Patterns"
Cohesion: 0.05
Nodes (38): 1. Forgetting to Update When NoTracking, 2. N+1 Query Problem, 3. Tracking Conflicts with Multiple DbContext Instances, 4. Not Using Async Consistently, 5. Querying Inside Loops, Actors / Long-Lived Objects (Factory Pattern), AppHost Configuration, Applying Migrations (+30 more)

### Community 39 - "Error"
Cohesion: 0.05
Nodes (25): Trivo.API.Middlewares, Trivo.API.Extensions, IApplicationBuilder, IEndpointRouteBuilder, IHostEnvironment, ProblemDetails, RequestDelegate, IConfiguration (+17 more)

### Community 40 - ".CreateMatchNotificationAsync"
Cohesion: 0.29
Nodes (8): HttpDelete, Authorize, CancellationToken, Guid, HttpPost, HttpPut, Task, NotificationController

### Community 41 - "MessageDto"
Cohesion: 0.10
Nodes (26): Authorize, CancellationToken, HttpPost, ISender, Task, MessageController, DateTime, Guid (+18 more)

### Community 42 - ".AddAiService"
Cohesion: 0.15
Nodes (11): EmbeddingClient, JwtResponse, CloudinarySetting, CloudinaryUrl, IConfiguration, IServiceCollection, DependencyInjection, CancellationToken (+3 more)

### Community 43 - "IAdministratorRepository"
Cohesion: 0.27
Nodes (5): CancellationToken, Guid, IEnumerable, Task, IAdministratorRepository

### Community 44 - "Interest"
Cohesion: 0.12
Nodes (15): Guid, InterestDetailsDto, Guid, IEnumerable, Interest, InterestMapper, Guid, ICollection (+7 more)

### Community 45 - "Trivo.API.csproj"
Cohesion: 0.11
Nodes (17): Asp.Versioning.Mvc (8.1.0), AspNetCore.HealthChecks.Redis (9.0.0), Microsoft.AspNetCore.OpenApi (8.0.10), Microsoft.AspNetCore.SignalR.Core (1.2.0), Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore (8.0.10), Scalar.AspNetCore (2.17.1), Serilog.Enrichers.Environment (3.0.1), Serilog.Enrichers.Thread (4.0.0) (+9 more)

### Community 46 - ".NotFound"
Cohesion: 0.08
Nodes (24): INotification, CancellationToken, Task, CancellationToken, Task, CancellationToken, Task, CancellationToken (+16 more)

### Community 47 - "IInterestRepository"
Cohesion: 0.34
Nodes (6): CancellationToken, Guid, IEnumerable, List, Task, IInterestRepository

### Community 48 - "MatchHub"
Cohesion: 0.17
Nodes (10): Hub, Guid, IEnumerable, Task, IMatchHub, Exception, ILogger, IMediator (+2 more)

### Community 49 - "Public API Design and Compatibility"
Cohesion: 0.06
Nodes (31): Anti-Patterns, API Approval Testing, API Change Guidelines, Benefits, Breaking Changes Disguised as Fixes, Chesterton's Fence, Deprecation Pattern, Encapsulation Patterns (+23 more)

### Community 50 - "CacheKeys"
Cohesion: 0.24
Nodes (3): Guid, IEnumerable, CacheKeys

### Community 51 - "IChatRepository"
Cohesion: 0.13
Nodes (17): CancellationToken, ILogger, Task, GetChatPaginationQueryHandler, CancellationToken, Task, CancellationToken, Task (+9 more)

### Community 52 - ".GetByCategoriesAsync"
Cohesion: 0.23
Nodes (11): Authorize, CancellationToken, Guid, HttpGet, HttpPost, IEnumerable, ISender, List (+3 more)

### Community 53 - "IMatchRepository"
Cohesion: 0.32
Nodes (9): AcceptedIds, CancellationToken, Guid, IEnumerable, IReadOnlyList, Match, RejectedIds, Task (+1 more)

### Community 54 - "RecruiterController.cs"
Cohesion: 0.10
Nodes (12): Trivo.API.Filters, Trivo.Application.Features.Recruiters.Commands.UpdateRecruiter, Trivo.Application.Features.Recruiters, Trivo.Application.Helpers, Trivo.Infrastructure.Shared, Trivo.Application.Behaviors, Trivo.Application.DTOs.Recruiter, Trivo.Application.Services (+4 more)

### Community 55 - ".Handle"
Cohesion: 0.19
Nodes (13): Guid, IEnumerable, GetMatchByUserQuery, CancellationToken, Dictionary, Func, Guid, IEnumerable (+5 more)

### Community 56 - ".Handle"
Cohesion: 0.09
Nodes (23): Candidates, HasOverlap, INotificationHandler, CancellationToken, ILogger, Task, UserProfileChangedEventHandler, Guid (+15 more)

### Community 57 - "Trivo.API.Controllers.V1.Requests"
Cohesion: 0.08
Nodes (16): Trivo.API.Controllers.V1.Requests, ChangePasswordRequest, Guid, List, FilterUsersByInterestsAndSkillsRequest, UpdateBiographyRequest, UpdatePasswordRequest, IFormFile (+8 more)

### Community 58 - "Roles"
Cohesion: 0.11
Nodes (17): CancellationToken, Guid, IList, Task, Roles, Administrator, Expert, Recruiter (+9 more)

### Community 59 - "Trivo.Infrastructure.Shared.csproj"
Cohesion: 0.15
Nodes (12): CloudinaryDotNet (1.27.0), MailKit (4.17.0), Microsoft.AspNetCore.Authentication.JwtBearer (8.0.10), Microsoft.AspNetCore.SignalR (1.2.0), Microsoft.Extensions.Options (10.0.3), Microsoft.Extensions.Options.ConfigurationExtensions (8.0.0), Microsoft.IdentityModel.Tokens (8.10.0), MimeKit (4.17.0) (+4 more)

### Community 60 - "ControllerBase"
Cohesion: 0.12
Nodes (14): ControllerBase, Authorize, CancellationToken, HttpPost, ISender, Task, ChatController, Authorize (+6 more)

### Community 61 - ".Handle"
Cohesion: 0.22
Nodes (9): Guid, IFormFile, List, CreateUserCommand, CancellationToken, ILogger, IPublisher, Task (+1 more)

### Community 62 - "Database Performance Patterns"
Cohesion: 0.07
Nodes (26): Always Apply Row Limits, Architecture, AsNoTracking for Read Queries, Avoid Cartesian Explosions, Avoid N+1 Queries, Configure Default Behavior, Constrain Column Sizes, Core Principles (+18 more)

### Community 63 - "InterestCategoryDto"
Cohesion: 0.18
Nodes (10): Guid, InterestCategoryDto, GetPaginatedInterestCategoriesQuery, CancellationToken, ILogger, Task, GetPaginatedInterestCategoriesQueryHandler, GetPaginatedInterestCategoriesValidator (+2 more)

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
Cohesion: 0.12
Nodes (20): Guid, List, UserAiRecommendationDto, Guid, IEnumerable, Task, IAiNotifier, IEnumerable (+12 more)

### Community 70 - ".Validate"
Cohesion: 0.15
Nodes (10): CancellationToken, Expression, Func, Task, IValidation, CancellationToken, Expression, Func (+2 more)

### Community 71 - "Expert"
Cohesion: 0.14
Nodes (12): Guid, ExpertMapper, Guid, ICollection, Expert, AvailableForProjects, IsHired, Matches (+4 more)

### Community 72 - "Trivo.Application.DTOs.Administrator"
Cohesion: 0.12
Nodes (10): Trivo.Application.Features.Administrator.Query.GetCompletedMatchesCount, Trivo.Application.Features.Administrator.Query.GetLatestMatches, Trivo.Application.Features.Administrator.Query.GetReportedUsersCount, Trivo.Application.Features.Administrator.Query.GetActiveUsersCount, Trivo.Application.DTOs.Administrator, Trivo.Application.Features.Administrator.Commands.UnbanUser, Trivo.Application.Features.Administrator.Commands.BanUser, Trivo.Application.Features.Administrator (+2 more)

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
Cohesion: 0.23
Nodes (4): Trivo.Infrastructure.Shared.Services, Trivo.Domain.Configurations, Trivo.Application.DTOs.Authentication, Trivo.Application.Features.Administrator.Commands.LoginAdmin

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
Cohesion: 0.18
Nodes (10): Administrator, Biography, Email, FirstName, IsActive, LastName, LinkedIn, PasswordHash (+2 more)

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

### Community 87 - "SkillWithIdDto"
Cohesion: 0.21
Nodes (10): Guid, SkillWithIdDto, IEnumerable, SearchSkillsByNameQuery, CancellationToken, IEnumerable, ILogger, Task (+2 more)

### Community 88 - "Polyfilling the nullable attributes for older target frameworks"
Cohesion: 0.20
Nodes (10): Candidate packages (evaluate, do not default to one), Decision rules, File-level `#nullable` directives, Incremental Adoption Strategy, Is a polyfill needed at all?, Options and tradeoffs, Polyfilling the nullable attributes for older target frameworks, Project-level (+2 more)

### Community 89 - ".CreateInterestCategoryAsync"
Cohesion: 0.24
Nodes (8): Authorize, CancellationToken, HttpGet, HttpPost, ISender, ProducesResponseType, Task, InterestCategoryController

### Community 90 - "IRealTimeNotifier"
Cohesion: 0.24
Nodes (9): Guid, IEnumerable, Task, IRealTimeNotifier, Guid, IEnumerable, IHubContext, Task (+1 more)

### Community 91 - "Trivo.Application.DTOs.InterestCategories"
Cohesion: 0.27
Nodes (4): Trivo.Application.DTOs.InterestCategories, Trivo.Application.Features.InterestCategories.Commands.CreateInterestCategory, Trivo.Application.Features.InterestCategories.Query.GetPaginatedInterestCategories, Trivo.Application.Features.InterestCategories

### Community 92 - ".Handle"
Cohesion: 0.14
Nodes (12): IHttpContextAccessor, IPipelineBehavior, IValidator, CancellationToken, RequestHandlerDelegate, Task, AuthorizationBehavior, CancellationToken (+4 more)

### Community 93 - "Trivo.Application.csproj"
Cohesion: 0.20
Nodes (9): BCrypt.Net-Next (4.0.3), FluentValidation (11.10.0), FluentValidation.DependencyInjectionExtensions (11.10.0), Microsoft.Extensions.DependencyInjection.Abstractions (8.0.2), net8.0, MediatR (12.2.0), Microsoft.Extensions.Caching.StackExchangeRedis (8.0.10), Serilog.AspNetCore (8.0.0) (+1 more)

### Community 94 - "C# Nullable Reference Types"
Cohesion: 0.20
Nodes (10): API Design Rules (Signatures), C# Nullable Reference Types, Core Goals, Generation Checklist (Summary), Project Configuration, Public API compatibility for libraries, Reference Files, References (+2 more)

### Community 95 - ".Handle"
Cohesion: 0.36
Nodes (6): CompletedMatchesCountDto, GetCompletedMatchesCountQuery, CancellationToken, ILogger, Task, GetCompletedMatchesCountQueryHandler

### Community 96 - "NRT Migration Playbook Reference"
Cohesion: 0.22
Nodes (6): Full Generation Checklist, Gradual annotation of a library, Legacy and Unannotated API Interop, NRT Migration Playbook Reference, Trust annotated libraries, Wrapping unannotated or legacy APIs

### Community 97 - "User"
Cohesion: 0.20
Nodes (8): ICollection, List, User, UserMapper, UserHelper, User, EntityTypeBuilder, UserConfig

### Community 98 - ".UpdateRecruiterAsync"
Cohesion: 0.19
Nodes (10): Authorize, CancellationToken, Guid, HttpPost, HttpPut, ISender, ProducesResponseType, Task (+2 more)

### Community 99 - "Anti-Patterns to Avoid"
Cohesion: 0.25
Nodes (8): Anti-Patterns to Avoid, Don't: Block on async code, Don't: Create deep inheritance hierarchies, Don't: Forget CancellationToken in async methods, Don't: Return List<T> when you mean IReadOnlyList<T>, Don't: Use byte[] when ReadOnlySpan<byte> works, Don't: Use classes for value objects, Don't: Use mutable DTOs

### Community 100 - "TokenResponseDto"
Cohesion: 0.19
Nodes (9): TokenResponseDto, AccessToken, RefreshToken, AdminLoginCommand, CancellationToken, ILogger, Task, AdminLoginCommandHandler (+1 more)

### Community 101 - "Report"
Cohesion: 0.20
Nodes (9): Guid, Report, Message, MessageId, Note, ReportedById, ReportId, ReportStatus (+1 more)

### Community 102 - "ChatHub"
Cohesion: 0.20
Nodes (9): Guid, GetChatPaginationQuery, GetChatPaginationValidator, Exception, Guid, ILogger, IMediator, Task (+1 more)

### Community 103 - ".GetDetailsByUserIdsAsync"
Cohesion: 0.36
Nodes (6): CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task

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

### Community 112 - ".Handle"
Cohesion: 0.24
Nodes (8): Guid, IEnumerable, GetUserSkillsQuery, CancellationToken, IEnumerable, ILogger, Task, GetUserSkillsQueryHandler

### Community 113 - ".Handle"
Cohesion: 0.19
Nodes (10): Guid, InterestByCategoryIdDto, Guid, IEnumerable, GetInterestsByCategoryIdQuery, CancellationToken, ILogger, Task (+2 more)

### Community 114 - "InterestCategoryRepository"
Cohesion: 0.38
Nodes (4): CancellationToken, Guid, Task, InterestCategoryRepository

### Community 115 - "IQuery"
Cohesion: 0.15
Nodes (14): IRequest, IQuery, ActiveUsersCountDto, ReportedUsersCountDto, GetActiveUsersCountQuery, CancellationToken, ILogger, Task (+6 more)

### Community 116 - "GenericRepository"
Cohesion: 0.44
Nodes (4): CancellationToken, Guid, Task, GenericRepository

### Community 117 - "IInterestCategoryRepository"
Cohesion: 0.46
Nodes (4): CancellationToken, Guid, Task, IInterestCategoryRepository

### Community 118 - ".Handle"
Cohesion: 0.11
Nodes (16): Trivo.Application.Features.Reports.Commands.CreateReport, Trivo.Application.Features.Reports, Trivo.Application.DTOs.Reports, DateTime, Guid, MessageReportDto, Guid, ReportDto (+8 more)

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

### Community 127 - ".Handle"
Cohesion: 0.33
Nodes (5): UserProfilePictureDto, Guid, GetUserProfilePictureQuery, CancellationToken, Task

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

### Community 158 - ".Handle"
Cohesion: 0.24
Nodes (7): Trivo.Application.Features.Users.Commands.LoginUser, LoginUserCommand, CancellationToken, ILogger, Task, LoginUserCommandHandler, LoginUserCommandValidator

### Community 159 - "ExpertRepository"
Cohesion: 0.36
Nodes (7): CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task, ExpertRepository

### Community 160 - "IAuthenticationService"
Cohesion: 0.47
Nodes (4): AuthController, CancellationToken, Task, IAuthenticationService

### Community 161 - "ForgotPasswordCommand"
Cohesion: 0.50
Nodes (3): Trivo.Application.Features.Users.Commands.ForgotPassword, ForgotPasswordCommand, ForgotPasswordValidator

### Community 162 - ".Handle"
Cohesion: 0.18
Nodes (10): List, ExpertDetailsDto, List, RecruiterDetailsDto, List, UserDetailsDto, Guid, GetUserDetailsQuery (+2 more)

### Community 163 - ".Handle"
Cohesion: 0.24
Nodes (8): UpdateUserDto, Guid, UpdateUserCommand, CancellationToken, ILogger, Task, UpdateUserCommandHandler, UpdateUserValidator

### Community 164 - "AiSetting"
Cohesion: 0.40
Nodes (4): AiSetting, ApiKey, EmbeddingModel, Provider

### Community 165 - ".GetDetailsByUserIdsAsync"
Cohesion: 0.36
Nodes (6): CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task

### Community 167 - ".Handle"
Cohesion: 0.33
Nodes (6): Guid, GetMessagePaginationQuery, CancellationToken, ILogger, Task, GetMessagePaginationQueryHandler

### Community 168 - "ExpertController.cs"
Cohesion: 0.36
Nodes (4): Trivo.Application.DTOs.Expert, Trivo.Application.Features.Experts.Commands.CreateExpert, Trivo.Application.Features.Experts, Trivo.Application.Features.Experts.Commands.UpdateExpert

### Community 169 - "NotificationHub"
Cohesion: 0.17
Nodes (9): Guid, IEnumerable, Task, INotificationHub, Exception, Guid, ILogger, Task (+1 more)

### Community 171 - ".Handle"
Cohesion: 0.31
Nodes (7): UserBiographyDto, Guid, GetUserBiographyQuery, CancellationToken, ILogger, Task, GetUserBiographyQueryHandler

### Community 172 - "MatchUpdateStatus"
Cohesion: 0.50
Nodes (3): MatchUpdateStatus, Completed, Rejected

### Community 173 - ".Conflict"
Cohesion: 0.11
Nodes (14): Trivo.Application.Features.Users.Commands.UpdatePassword, Guid, CreateChatCommand, UserId, CancellationToken, Task, CreateChatValidator, Guid (+6 more)

### Community 174 - "InterestWithIdDto"
Cohesion: 0.13
Nodes (17): Guid, InterestWithIdDto, IEnumerable, SearchInterestsByNameQuery, CancellationToken, IEnumerable, ILogger, Task (+9 more)

### Community 175 - "IQueryHandler"
Cohesion: 0.14
Nodes (14): IRequestHandler, IQueryHandler, ILogger, GetUserDetailsQueryHandler, ILogger, GetUserProfilePictureQueryHandler, Guid, List (+6 more)

### Community 177 - "AbstractValidator"
Cohesion: 0.04
Nodes (43): AbstractValidator, Trivo.Application.Features.Users.Commands.UpdateBiography, Trivo.Application.Features.Interests.Commands.UpdateInterest, Trivo.Application.Features.Users.Commands.UpdateProfilePicture, Trivo.Application.Features.Skills.Commands.UpdateSkill, IBaseCommand, ICommand, Guid (+35 more)

### Community 181 - "BaseEntity"
Cohesion: 0.29
Nodes (6): DateTime, Guid, BaseEntity, CreatedAt, Id, UpdatedAt

### Community 186 - "MissingByMatching"
Cohesion: 0.50
Nodes (3): MissingByMatching, Expert, Recruiter

## Knowledge Gaps
- **565 isolated node(s):** `net8.0`, `Asp.Versioning.Mvc (8.1.0)`, `AspNetCore.HealthChecks.Redis (9.0.0)`, `MediatR (12.2.0)`, `Microsoft.AspNetCore.OpenApi (8.0.10)` (+560 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **10 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `ResultT` connect `ResultT` to `.Handle`, `.RefreshTokenAsync`, `.Handle`, `.UpdateExpertAsync`, `IExpertRepository`, `MatchDetailsDto`, `Code`, `ICacheService`, `UserDto`, `.Handle`, `PagedResult`, `AdminController`, `.Handle`, `.Handle`, `.Handle`, `IAuthenticationService`, `.Validation`, `.Handle`, `.CreateMatchNotificationAsync`, `MessageDto`, `Error`, `.Handle`, `.Conflict`, `.NotFound`, `IQueryHandler`, `InterestWithIdDto`, `AbstractValidator`, `IChatRepository`, `.GetByCategoriesAsync`, `.Handle`, `.Handle`, `ControllerBase`, `.Handle`, `InterestCategoryDto`, `.Handle`, `Skill`, `.ValidateEmailAsync`, `.Handle`, `SkillWithIdDto`, `.CreateInterestCategoryAsync`, `.Handle`, `.UpdateRecruiterAsync`, `TokenResponseDto`, `.CreateMatchAsync`, `.Handle`, `.Handle`, `IQuery`, `.Handle`, `AuthenticationService`, `.Handle`?**
  _High betweenness centrality (0.153) - this node is a cross-community bridge._
- **Why does `Trivo.Domain.Models` connect `Trivo.Domain.Models` to `Trivo.Application.Abstractions.Messages`, `Message`, `InterestCategory`, `Trivo.Application.DTOs.Users`, `Trivo.Infrastructure.Persistence.Configurations`, `Trivo.Application.Interfaces.SignalR`, `Notification`, `Recruiter`, `CodeConfig`, `UserSkill`, `Trivo.Domain.Enums`, `Code`, `UserInterestConfig`, `Chat`, `ExpertController.cs`, `AdministratorConfig`, `InterestCategoryConfig`, `RecruiterConfig`, `ReportConfig`, `RecruiterController.cs`, `UserInterest`, `Skill`, `Expert`, `Trivo.Application.DTOs.Administrator`, `Trivo.Application.DTOs.Authentication`, `Match`, `Trivo.Application.DTOs.InterestCategories`, `User`, `Report`, `.Handle`?**
  _High betweenness centrality (0.064) - this node is a cross-community bridge._
- **Why does `TrivoContext` connect `TrivoContext` to `UserRepository`, `Message`, `InterestCategory`, `Trivo.Domain.Models`, `Notification`, `.AddRepositories`, `UserSkill`, `Code`, `ICacheService`, `Recruiter`, `AdministratorRepository`, `ExpertRepository`, `Chat`, `.AddSkillsToUserAsync`, `InterestRepository`, `Error`, `Interest`, `UserInterest`, `Skill`, `.Validate`, `Expert`, `Match`, `MatchRepository`, `Administrator`, `User`, `Report`, `InterestCategoryRepository`, `GenericRepository`?**
  _High betweenness centrality (0.053) - this node is a cross-community bridge._
- **What connects `net8.0`, `Asp.Versioning.Mvc (8.1.0)`, `AspNetCore.HealthChecks.Redis (9.0.0)` to the rest of the system?**
  _565 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Trivo.Application.Abstractions.Messages` be split into smaller, more focused modules?**
  _Cohesion score 0.1285034373347435 - nodes in this community are weakly interconnected._
- **Should `Message` be split into smaller, more focused modules?**
  _Cohesion score 0.08773784355179703 - nodes in this community are weakly interconnected._
- **Should `Trivo.Application.DTOs.Users` be split into smaller, more focused modules?**
  _Cohesion score 0.05191256830601093 - nodes in this community are weakly interconnected._