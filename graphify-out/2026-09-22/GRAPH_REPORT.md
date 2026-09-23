# Graph Report - Trivo  (2026-09-20)

## Corpus Check
- 495 files · ~90,010 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 3458 nodes · 8045 edges · 210 communities (207 shown, 3 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 487 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `881ca2ce`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- UserRepository
- IUserRepository
- Message
- InterestCategory
- Trivo.Application.DTOs.Interests
- Trivo.Domain.Models
- CreateInterestCategoryCommandHandler.cs
- Trivo.Infrastructure.Persistence.Configurations
- .Handle
- Trivo.Application.DTOs.Users
- Trivo.Domain.Enums
- https
- ResultT
- AddProfileTextHash
- Notification
- Expert
- Recruiter
- .AddRepositories
- ChatDto
- .NotFound
- BHD.ResultPattern — Guía de arquitectura e implementación
- MatchDetailsDto
- ICommandHandler
- Dependency Injection Patterns
- User
- MessageDto
- CacheKeys
- UserDto
- User
- Code
- .Handle
- .Handle
- AdminController
- NotificationService
- Chat
- UserInterest
- UserSkill
- IInterestRepository
- Entity Framework Core Patterns
- Error
- NotificationHub
- Requerimientos Funcionales
- .AddAiService
- Trivo.Application.Utils
- Interest
- Trivo.API.csproj
- .SaveChangesAsync
- .Handle
- IMatchHub
- Public API Design and Compatibility
- Skill
- IChatRepository
- .GetByCategoriesAsync
- IMatchRepository
- UpdateExpertCommandHandler.cs
- .Handle
- .CreateSkillAsync
- AdministratorRepository
- .GetRolesAsync
- Trivo.Infrastructure.Shared.csproj
- Trivo.API.Controllers.V1.Requests
- ResolveReportCommandHandler
- Database Performance Patterns
- Documento de Requerimientos de Software
- TrivoContext
- ISkillRepository
- .ValidateEmailAsync
- SkillDto
- Slopwatch: LLM Anti-Cheat for .NET
- AiNotifier
- IChatHub
- Trivo.Application.DTOs.Notifications
- .CreateMatchNotificationAsync
- RedisCacheService
- GoogleGeminiEmbeddingService
- Módulo de Matchmaking con IA — Implementación
- Plan de implementación — Embeddings vía API externa para emparejamiento por afinidad
- Trivo.Application.Abstractions.Messages
- SkillRepository
- Nullable Attributes Reference
- MatchRepository
- Modern C# Coding Standards
- .UpdateRecruiterAsync
- IAdministratorRepository
- .Handle
- Plantillas copy-paste — feature CQRS de Trivo
- ICloudinaryService
- SkillWithIdDto
- Polyfilling the nullable attributes for older target frameworks
- .Handle
- ReportDetailDto
- Match
- .Handle
- Trivo.Application.csproj
- C# Nullable Reference Types
- UserProfileChangedEventHandler
- NRT Migration Playbook Reference
- .MapToExpertDto
- .Handle
- Anti-Patterns to Avoid
- AuthenticationService
- Report
- AccountAccessService
- PagedResult
- ResultFilter
- Anti-Patterns and Reflection Avoidance
- .GetUserId
- Avoid Reflection-Based Metaprogramming
- Trivo
- MatchUpdateStatus
- Performance and API Design Patterns
- Value Objects and Pattern Matching
- RF9-administrar-aplicacion.md
- .Handle
- Sanction
- .Handle
- ReportDto
- SendFileCommand
- InitialCreate
- .GetExpertIdAsync
- .GetRecruiterIdAsync
- Sanction
- EmailSetting
- Trivo.Infrastructure.Persistence.csproj
- The `field` Keyword (C# 14 / .NET 10) and Nullability
- MessageStatus
- Trivo.Domain.csproj
- Roles
- .ToEntity
- UserMappingExtensions.cs
- CreateMatchingCommand
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
- ReportListItemDto
- .Handle
- IGenericRepository
- Core Nullability Model
- Arquitectura CQRS de Trivo — mandato para nuevas features
- Composition and Error Handling
- Language Patterns
- AiSetting
- .AddServices
- Known Static-Analysis Limitations and Safe Patterns
- Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`
- SanctionDto
- Preconditions: `AllowNull` and `DisallowNull`
- Performance Patterns
- CLAUDE.md
- Trivo.Infrastructure.Persistence.Migrations
- ReportMapper
- UpdateNameCommand
- .ToDto
- UpdateRecruiterCommandHandler.cs
- AbstractValidator
- AddUniqueExpertRecruiterUserId
- CodeRepository
- .BuildModel
- ControllerBase
- .UpdateExpertAsync
- .Validate
- ExpertRepository
- MatchDto
- ICodeService
- IExpertRepository
- InterestWithIdDto
- IUserOwnedRequest
- .ValidateAsync
- .Handle
- .Handle
- .Handle
- GetExpertsPagedQuery
- Administrator
- .BuildTargetModel
- .BuildTargetModel
- UserAiRecommendationDto
- GetRecruitersPagedQuery
- MatchHub
- IQuery
- .Handle
- IReportRepository
- ICodeRepository
- IQueryHandler
- NotificationDto
- UserRecommendationHub
- .GetDetailsByUserIdsAsync
- CacheEntryOptions
- ResolveReportCommand
- ReportRepository
- MissingByMatching
- ChatHub
- .Handle
- ISanctionRepository
- AddReportSanctions
- BaseEntity
- JwtSetting
- .GetOrSetAsync
- .BuildTargetModel
- ReportType

## God Nodes (most connected - your core abstractions)
1. `ResultT` - 168 edges
2. `Trivo.Application.Abstractions.Messages` - 138 edges
3. `PagedResult` - 104 edges
4. `Trivo.Application.Utils` - 103 edges
5. `Trivo.Domain.Models` - 100 edges
6. `Trivo.Application.Pagination` - 79 edges
7. `Trivo.Application.Interfaces.Services` - 76 edges
8. `IUserRepository` - 67 edges
9. `Trivo.Application.Interfaces.Repository.Account` - 65 edges
10. `TrivoContext` - 64 edges

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

## Communities (210 total, 3 thin omitted)

### Community 0 - "UserRepository"
Cohesion: 0.23
Nodes (9): CancellationToken, Expert, Guid, IEnumerable, List, Recruiter, Task, User (+1 more)

### Community 1 - "IUserRepository"
Cohesion: 0.18
Nodes (13): CancellationToken, Distance, Guid, IEnumerable, IReadOnlyCollection, IReadOnlyList, Items, List (+5 more)

### Community 2 - "Message"
Cohesion: 0.09
Nodes (29): CancellationToken, Guid, List, Task, IMessageRepository, DateTime, Guid, ICollection (+21 more)

### Community 3 - "InterestCategory"
Cohesion: 0.06
Nodes (39): Authorize, CancellationToken, HttpGet, HttpPost, ISender, ProducesResponseType, Task, InterestCategoryController (+31 more)

### Community 4 - "Trivo.Application.DTOs.Interests"
Cohesion: 0.07
Nodes (13): Trivo.Application.Features.Skills.Query.GetSkillsPagination, Trivo.Application.Features.Interests.Commands.CreateInterest, Trivo.Application.Features.Skills, Trivo.Application.Features.Users.Query.GetUserInterests, Trivo.Application.Features.Interests.Query.GetInterestsByCategoryId, Trivo.Application.DTOs.Skills, Trivo.Application.Features.Skills.Query.SearchSkillsByName, Trivo.Application.Features.Skills.Commands.CreateSkill (+5 more)

### Community 5 - "Trivo.Domain.Models"
Cohesion: 0.11
Nodes (10): Trivo.Domain.Common, Trivo.Infrastructure.Persistence.Context, Trivo.Infrastructure.Persistence.Repository.Account, Trivo.Application.Interfaces.Repository.Base, Trivo.Infrastructure.Persistence.Services, Trivo.Infrastructure.Persistence.Repository, Trivo.Domain.Models, Trivo.Application.Interfaces.Repository (+2 more)

### Community 6 - "CreateInterestCategoryCommandHandler.cs"
Cohesion: 0.27
Nodes (4): Trivo.Application.DTOs.InterestCategories, Trivo.Application.Features.InterestCategories.Commands.CreateInterestCategory, Trivo.Application.Features.InterestCategories.Query.GetPaginatedInterestCategories, Trivo.Application.Features.InterestCategories

### Community 7 - "Trivo.Infrastructure.Persistence.Configurations"
Cohesion: 0.11
Nodes (11): Trivo.Infrastructure.Persistence.Configurations, IEntityTypeConfiguration, EntityTypeBuilder, AdministratorConfig, EntityTypeBuilder, ChatConfig, EntityTypeBuilder, ChatUserConfig (+3 more)

### Community 8 - ".Handle"
Cohesion: 0.17
Nodes (11): Candidates, HasOverlap, CancellationToken, Distance, Guid, IReadOnlyList, List, Task (+3 more)

### Community 9 - "Trivo.Application.DTOs.Users"
Cohesion: 0.07
Nodes (14): Trivo.Application.Features.Users.Commands.CreateUser, Trivo.Application.Features.Users.Query.SearchUsers, Trivo.Application.Features.Users.Query.GetUserDetails, Trivo.Application.Features.Administrator.Query.GetLatestUsersPaged, Trivo.Application.Features.Users, Trivo.Application.Features.Users.Commands.UpdateUser, Trivo.Application.DTOs.Users, Trivo.Application.Features.Matching (+6 more)

### Community 10 - "Trivo.Domain.Enums"
Cohesion: 0.06
Nodes (18): Trivo.Application.Features.Messages.Commands.SendImage, Trivo.Application.Features.Messages.Query.GetMessagePagination, Trivo.Application.Features.Chat.Query.GetChatPagination, Trivo.Application.Features.Chat, Trivo.API.Controllers.V1, Trivo.Application.Features.Matching.Commands.UpdateMatch, Trivo.Application.Features.Matching.Commands.CreateMatch, Trivo.Application.Interfaces.SignalR (+10 more)

### Community 11 - "https"
Cohesion: 0.10
Nodes (21): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, launchUrl, commandName (+13 more)

### Community 12 - "ResultT"
Cohesion: 0.20
Nodes (13): Authorize, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IEnumerable, ISender (+5 more)

### Community 13 - "AddProfileTextHash"
Cohesion: 0.22
Nodes (6): MigrationBuilder, DateTime, Guid, ModelBuilder, Vector, AddProfileTextHash

### Community 14 - "Notification"
Cohesion: 0.11
Nodes (21): CancellationToken, Guid, Task, INotificationRepository, DateTime, Guid, Notification, Content (+13 more)

### Community 15 - "Expert"
Cohesion: 0.14
Nodes (12): Guid, ExpertMapper, Guid, ICollection, Expert, AvailableForProjects, IsHired, Matches (+4 more)

### Community 16 - "Recruiter"
Cohesion: 0.25
Nodes (7): Guid, ICollection, Recruiter, CompanyName, Matches, User, UserId

### Community 17 - ".AddRepositories"
Cohesion: 0.20
Nodes (12): IUserSkillRepository, IGetExpertIdService, IGetRecruiterIdService, IUserRoleService, IConfiguration, IConnectionMultiplexer, IServiceCollection, DependencyInjection (+4 more)

### Community 18 - "ChatDto"
Cohesion: 0.17
Nodes (15): DateTime, Guid, List, ChatDto, ILogger, CreateChatCommandHandler, Guid, IEnumerable (+7 more)

### Community 19 - ".NotFound"
Cohesion: 0.14
Nodes (16): CancellationToken, Task, CancellationToken, Task, CancellationToken, Task, CodeGenerator, Result (+8 more)

### Community 20 - "BHD.ResultPattern — Guía de arquitectura e implementación"
Cohesion: 0.06
Nodes (31): 1. Arquitectura general, 2.1. Estructura de carpetas, 2.2. `BHD.ResultPattern.csproj`, 2.3. `Result.cs`, 2.4. `Error.cs`, 2. Núcleo: `BHD.ResultPattern` (sin dependencias), 3.1. Estructura de carpetas, 3.2. `BHD.ResultPattern.AspNetCore.csproj` (+23 more)

### Community 21 - "MatchDetailsDto"
Cohesion: 0.12
Nodes (20): DateTime, Guid, MatchDetailsDto, Guid, UpdateMatchingCommand, CancellationToken, Guid, ILogger (+12 more)

### Community 22 - "ICommandHandler"
Cohesion: 0.08
Nodes (38): ICommandHandler, ILogger, BanUserCommandHandler, ILogger, UnbanUserCommandHandler, ILogger, CreateInterestCategoryCommandHandler, ILogger (+30 more)

### Community 23 - "Dependency Injection Patterns"
Cohesion: 0.05
Nodes (38): Advanced DI Patterns, Akka.DependencyInjection Reference, Akka.Hosting.TestKit, Akka.NET Actor Scope Management, Common Patterns, Conditional Registration, Contents, Factory-Based Registration (+30 more)

### Community 24 - "User"
Cohesion: 0.18
Nodes (8): CancellationToken, Task, IEnumerable, UserProfileTextBuilder, UserHelper, User, EntityTypeBuilder, UserConfig

### Community 25 - "MessageDto"
Cohesion: 0.13
Nodes (17): DateTime, Guid, MessageDto, Guid, IFormFile, SendImageCommand, UserId, CancellationToken (+9 more)

### Community 26 - "CacheKeys"
Cohesion: 0.27
Nodes (3): Guid, IEnumerable, CacheKeys

### Community 27 - "UserDto"
Cohesion: 0.14
Nodes (16): DateTime, Guid, UserDto, IEnumerable, GetLast10BannedUsersQuery, CancellationToken, IEnumerable, ILogger (+8 more)

### Community 28 - "User"
Cohesion: 0.05
Nodes (48): Guid, RecruiterMapper, CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task (+40 more)

### Community 29 - "Code"
Cohesion: 0.12
Nodes (15): DateTime, Guid, Code, CodeId, CreatedAt, ExpiresAt, IsRevoked, IsUsed (+7 more)

### Community 30 - ".Handle"
Cohesion: 0.05
Nodes (35): Trivo.Application.Features.Users.Commands.ForgotPassword, Trivo.Application.Features.Users.Commands.RequestEmailChange, Trivo.Application.Features.Users.Commands.ResendConfirmationCode, Trivo.Application.Features.Users.Commands.ConfirmEmailChange, EmailResponseDto, Guid, ConfirmEmailChangeCommand, CancellationToken (+27 more)

### Community 31 - ".Handle"
Cohesion: 0.15
Nodes (11): DateTime, Guid, AdminMatchDto, ExpertMatchDto, RecruiterMatchDto, GetLatestMatchesQuery, CancellationToken, ILogger (+3 more)

### Community 32 - "AdminController"
Cohesion: 0.27
Nodes (12): Authorize, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IEnumerable, ISender (+4 more)

### Community 33 - "NotificationService"
Cohesion: 0.33
Nodes (6): IServiceCollection, CancellationToken, Guid, ILogger, Task, NotificationService

### Community 34 - "Chat"
Cohesion: 0.11
Nodes (23): ICollection, Chat, ChatType, ChatUsers, IsActive, Messages, DateTime, Guid (+15 more)

### Community 35 - "UserInterest"
Cohesion: 0.11
Nodes (20): CancellationToken, Task, CancellationToken, Guid, List, Task, IUserInterestRepository, Guid (+12 more)

### Community 36 - "UserSkill"
Cohesion: 0.12
Nodes (15): CancellationToken, Guid, List, Task, Guid, UserSkill, Skill, SkillId (+7 more)

### Community 37 - "IInterestRepository"
Cohesion: 0.13
Nodes (19): CancellationToken, Guid, IEnumerable, List, Task, IInterestRepository, IValidation, CancellationToken (+11 more)

### Community 38 - "Entity Framework Core Patterns"
Cohesion: 0.05
Nodes (38): 1. Forgetting to Update When NoTracking, 2. N+1 Query Problem, 3. Tracking Conflicts with Multiple DbContext Instances, 4. Not Using Async Consistently, 5. Querying Inside Loops, Actors / Long-Lived Objects (Factory Pattern), AppHost Configuration, Applying Migrations (+30 more)

### Community 39 - "Error"
Cohesion: 0.05
Nodes (27): Trivo.API.Middlewares, Trivo.API.Extensions, IApplicationBuilder, IEndpointRouteBuilder, IHostEnvironment, IReadOnlyDictionary, ProblemDetails, RequestDelegate (+19 more)

### Community 40 - "NotificationHub"
Cohesion: 0.22
Nodes (9): CancellationToken, Guid, Task, INotificationService, Exception, Guid, ILogger, Task (+1 more)

### Community 41 - "Requerimientos Funcionales"
Cohesion: 0.12
Nodes (15): 4.3.1 Análisis Preliminar y Determinación de Requerimientos, 4.3.2 Análisis y Modelado de los Requerimientos del Proyecto, 4.3 Descripción del Modelo de Desarrollo, Requerimientos Funcionales, Requerimientos Funcionales (Sección Adicional), Requerimientos No Funcionales, RF1. Registro, RF2. Inicio de sesión (+7 more)

### Community 42 - ".AddAiService"
Cohesion: 0.39
Nodes (4): JwtResponse, IConfiguration, IServiceCollection, DependencyInjection

### Community 43 - "Trivo.Application.Utils"
Cohesion: 0.08
Nodes (18): Trivo.API.Filters, Trivo.Application.Interfaces.UnitOfWork, Trivo.Application.DTOs.Email, Trivo.Infrastructure.Shared.Services, Trivo.Domain.Configurations, Trivo.Application.Utils, Trivo.Application.DTOs.Authentication, Trivo.Application.Caching (+10 more)

### Community 44 - "Interest"
Cohesion: 0.12
Nodes (15): Guid, InterestDetailsDto, Guid, IEnumerable, Interest, InterestMapper, Guid, ICollection (+7 more)

### Community 45 - "Trivo.API.csproj"
Cohesion: 0.11
Nodes (17): Asp.Versioning.Mvc (8.1.0), AspNetCore.HealthChecks.Redis (9.0.0), Microsoft.AspNetCore.OpenApi (8.0.10), Microsoft.AspNetCore.SignalR.Core (1.2.0), Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore (8.0.10), Scalar.AspNetCore (2.17.1), Serilog.Enrichers.Environment (3.0.1), Serilog.Enrichers.Thread (4.0.0) (+9 more)

### Community 46 - ".SaveChangesAsync"
Cohesion: 0.07
Nodes (26): INotification, CancellationToken, Task, CancellationToken, Task, CancellationToken, Task, CancellationToken (+18 more)

### Community 47 - ".Handle"
Cohesion: 0.24
Nodes (8): UpdateUserDto, Guid, UpdateUserCommand, CancellationToken, ILogger, Task, UpdateUserCommandHandler, UpdateUserValidator

### Community 48 - "IMatchHub"
Cohesion: 0.43
Nodes (4): Guid, IEnumerable, Task, IMatchHub

### Community 49 - "Public API Design and Compatibility"
Cohesion: 0.06
Nodes (31): Anti-Patterns, API Approval Testing, API Change Guidelines, Benefits, Breaking Changes Disguised as Fixes, Chesterton's Fence, Deprecation Pattern, Encapsulation Patterns (+23 more)

### Community 50 - "Skill"
Cohesion: 0.18
Nodes (10): DateTime, Guid, ICollection, Skill, Name, RegisteredAt, SkillId, UserSkills (+2 more)

### Community 51 - "IChatRepository"
Cohesion: 0.14
Nodes (18): CancellationToken, ILogger, Task, SendFileCommandHandler, Guid, SendMessageCommand, UserId, CancellationToken (+10 more)

### Community 52 - ".GetByCategoriesAsync"
Cohesion: 0.23
Nodes (11): Authorize, CancellationToken, Guid, HttpGet, HttpPost, IEnumerable, ISender, List (+3 more)

### Community 53 - "IMatchRepository"
Cohesion: 0.32
Nodes (9): AcceptedIds, CancellationToken, Guid, IEnumerable, IReadOnlyList, Match, RejectedIds, Task (+1 more)

### Community 54 - "UpdateExpertCommandHandler.cs"
Cohesion: 0.18
Nodes (6): Trivo.Application.DTOs.Expert, Trivo.Application.Features.Experts.Commands.CreateExpert, Trivo.Application.Features.Experts, Trivo.Application.Helpers, Trivo.Application.Behaviors, Trivo.Application.Features.Experts.Commands.UpdateExpert

### Community 55 - ".Handle"
Cohesion: 0.19
Nodes (13): Guid, IEnumerable, GetMatchByUserQuery, CancellationToken, Dictionary, Func, Guid, IEnumerable (+5 more)

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

### Community 60 - "Trivo.API.Controllers.V1.Requests"
Cohesion: 0.06
Nodes (21): Trivo.API.Controllers.V1.Requests, ChangePasswordRequest, ConfirmEmailChangeRequest, Guid, List, FilterUsersByInterestsAndSkillsRequest, RequestEmailChangeRequest, ResolveReportRequest (+13 more)

### Community 61 - "ResolveReportCommandHandler"
Cohesion: 0.20
Nodes (12): DateTime, Guid, ReportResolutionDto, CancellationToken, DateTime, Guid, ILogger, Sanction (+4 more)

### Community 62 - "Database Performance Patterns"
Cohesion: 0.07
Nodes (26): Always Apply Row Limits, Architecture, AsNoTracking for Read Queries, Avoid Cartesian Explosions, Avoid N+1 Queries, Configure Default Behavior, Constrain Column Sizes, Core Principles (+18 more)

### Community 63 - "Documento de Requerimientos de Software"
Cohesion: 0.17
Nodes (11): 1. Búsqueda y Filtros, 2. Gestión de Usuarios, 3. Sistema de Reportes, 4. Reportes y Listados PDF, Cambios básicos en la información del usuario, Documento de Requerimientos de Software, Endpoints de Listados Requeridos, Filtros personalizados para buscar recomendaciones (+3 more)

### Community 64 - "TrivoContext"
Cohesion: 0.08
Nodes (24): DbContext, DbContextOptions, DbSet, CancellationToken, ModelBuilder, Task, TrivoContext, Administrators (+16 more)

### Community 65 - "ISkillRepository"
Cohesion: 0.16
Nodes (12): GetSkillsPaginationQuery, CancellationToken, ILogger, Task, GetSkillsPaginationQueryHandler, GetSkillsPaginationValidator, CancellationToken, Guid (+4 more)

### Community 66 - ".ValidateEmailAsync"
Cohesion: 0.25
Nodes (7): CancellationToken, Task, IEmailValidationService, CancellationToken, ILogger, Task, EmailValidationService

### Community 67 - "SkillDto"
Cohesion: 0.17
Nodes (9): DateTime, Guid, SkillDto, Guid, CreateSkillCommand, CreateSkillValidator, Guid, IEnumerable (+1 more)

### Community 68 - "Slopwatch: LLM Anti-Cheat for .NET"
Cohesion: 0.09
Nodes (22): After Every Code Change, As a Global Tool, As a Local Tool (Recommended), Azure Pipelines, CI/CD Integration, Claude Code Hook Integration, Common Slop Patterns, Configuration (+14 more)

### Community 69 - "AiNotifier"
Cohesion: 0.26
Nodes (9): Guid, IEnumerable, Task, IAiNotifier, Guid, IEnumerable, IHubContext, Task (+1 more)

### Community 70 - "IChatHub"
Cohesion: 0.33
Nodes (4): Guid, IEnumerable, Task, IChatHub

### Community 71 - "Trivo.Application.DTOs.Notifications"
Cohesion: 0.14
Nodes (7): Trivo.Application.DTOs.Notifications, Trivo.Application.Features.Notifications, Guid, CreateNotificationDto, IEnumerable, List, NotificationMapper

### Community 72 - ".CreateMatchNotificationAsync"
Cohesion: 0.29
Nodes (8): HttpDelete, Authorize, CancellationToken, Guid, HttpPost, HttpPut, Task, NotificationController

### Community 73 - "RedisCacheService"
Cohesion: 0.26
Nodes (8): IDatabase, CancellationToken, Func, IConnectionMultiplexer, IEnumerable, ILogger, Task, RedisCacheService

### Community 74 - "GoogleGeminiEmbeddingService"
Cohesion: 0.16
Nodes (14): ContentPart, EmbedContentResponse, EmbeddingValues, HttpClient, CancellationToken, ILogger, Task, ContentPart (+6 more)

### Community 75 - "Módulo de Matchmaking con IA — Implementación"
Cohesion: 0.14
Nodes (13): 1. Problema de negocio, 2.1 Abstracción del proveedor — `IEmbeddingService`, 2.2 Construcción del texto — `UserProfileTextBuilder`, 2.3 Cuándo se regenera el embedding — `UserProfileChangedEvent`, 2. Arquitectura, 3. Cambios de esquema (tablas), 4. El algoritmo de recomendación, 5. Qué NO se cachea, y por qué (+5 more)

### Community 76 - "Plan de implementación — Embeddings vía API externa para emparejamiento por afinidad"
Cohesion: 0.14
Nodes (13): 0. Decisión de proveedor y por qué la interfaz debe ser agnóstica, 10. Pruebas, 1. Infraestructura de base de datos (bloqueante, va primero), 2. Dominio (`Trivo.Domain`), 3. Application (`Trivo.Application`), 4. Infrastructure.Shared — implementación del proveedor, 5. Infrastructure.Persistence — columna, mapping e índice, 6. Cuándo se genera/regenera el embedding (+5 more)

### Community 77 - "Trivo.Application.Abstractions.Messages"
Cohesion: 0.06
Nodes (22): Trivo.Application.Abstractions.Messages, Trivo.Application.Features.Administrator.Query.GetCompletedMatchesCount, Trivo.Application.Features.Reports.Query.GetLatestReports, Trivo.Application.Features.Administrator.Query.GetExpertsPaged, Trivo.Application.Features.Reports.Query.GetReportsPaged, Trivo.Application.Features.Administrator.Query.GetLatestMatches, Trivo.Application.Features.Reports.Commands.CreateReport, Trivo.Application.Features.Administrator.Commands.CreateAdministrator (+14 more)

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

### Community 82 - ".UpdateRecruiterAsync"
Cohesion: 0.19
Nodes (10): Authorize, CancellationToken, Guid, HttpPost, HttpPut, ISender, ProducesResponseType, Task (+2 more)

### Community 83 - "IAdministratorRepository"
Cohesion: 0.23
Nodes (5): CancellationToken, Guid, IEnumerable, Task, IAdministratorRepository

### Community 84 - ".Handle"
Cohesion: 0.21
Nodes (8): Guid, InterestDto, GetInterestsPaginationQuery, CancellationToken, ILogger, Task, GetInterestsPaginationQueryHandler, GetInterestsPaginationValidator

### Community 85 - "Plantillas copy-paste — feature CQRS de Trivo"
Cohesion: 0.18
Nodes (10): 1. DTO, 2. Command (con respuesta) — ejemplo real: `CreateInterestCommand`, 3. Validator (co-ubicado con el Command), 4. Handler — orquesta repos + UnitOfWork, nunca lanza excepciones de negocio, 5. Query con paginación + cache — ejemplo real: `GetInterestsPaginationQuery`, 6. Mapper — extensiones estáticas, una clase por feature, 7. Repositorio — patrón MANDATORIO para entidades nuevas (extiende `IGenericRepository<T>`), 8. DI — registrar el repo nuevo (+2 more)

### Community 86 - "ICloudinaryService"
Cohesion: 0.16
Nodes (12): CancellationToken, Stream, Task, ICloudinaryService, CloudinarySetting, CloudinaryUrl, CancellationToken, IOptions (+4 more)

### Community 87 - "SkillWithIdDto"
Cohesion: 0.23
Nodes (10): Guid, SkillWithIdDto, Guid, IEnumerable, GetUserSkillsQuery, CancellationToken, IEnumerable, ILogger (+2 more)

### Community 88 - "Polyfilling the nullable attributes for older target frameworks"
Cohesion: 0.20
Nodes (10): Candidate packages (evaluate, do not default to one), Decision rules, File-level `#nullable` directives, Incremental Adoption Strategy, Is a polyfill needed at all?, Options and tradeoffs, Polyfilling the nullable attributes for older target frameworks, Project-level (+2 more)

### Community 89 - ".Handle"
Cohesion: 0.31
Nodes (7): UserBiographyDto, Guid, GetUserBiographyQuery, CancellationToken, ILogger, Task, GetUserBiographyQueryHandler

### Community 90 - "ReportDetailDto"
Cohesion: 0.14
Nodes (11): DateTime, Guid, ReportDetailDto, DateTime, Guid, ReportUserDetailDto, Guid, GetReportByIdQuery (+3 more)

### Community 91 - "Match"
Cohesion: 0.18
Nodes (10): Guid, Match, Expert, ExpertId, ExpertStatus, MatchStatus, RecruiterId, RecruiterStatus (+2 more)

### Community 92 - ".Handle"
Cohesion: 0.14
Nodes (12): IHttpContextAccessor, IPipelineBehavior, IValidator, CancellationToken, RequestHandlerDelegate, Task, AuthorizationBehavior, CancellationToken (+4 more)

### Community 93 - "Trivo.Application.csproj"
Cohesion: 0.20
Nodes (9): BCrypt.Net-Next (4.0.3), FluentValidation (11.10.0), FluentValidation.DependencyInjectionExtensions (11.10.0), Microsoft.Extensions.DependencyInjection.Abstractions (8.0.2), net8.0, MediatR (12.2.0), Microsoft.Extensions.Caching.StackExchangeRedis (8.0.10), Serilog.AspNetCore (8.0.0) (+1 more)

### Community 94 - "C# Nullable Reference Types"
Cohesion: 0.20
Nodes (10): API Design Rules (Signatures), C# Nullable Reference Types, Core Goals, Generation Checklist (Summary), Project Configuration, Public API compatibility for libraries, Reference Files, References (+2 more)

### Community 95 - "UserProfileChangedEventHandler"
Cohesion: 0.18
Nodes (9): EmbeddingClient, INotificationHandler, ILogger, UserProfileChangedEventHandler, IEmbeddingService, CancellationToken, ILogger, Task (+1 more)

### Community 96 - "NRT Migration Playbook Reference"
Cohesion: 0.22
Nodes (6): Full Generation Checklist, Gradual annotation of a library, Legacy and Unannotated API Interop, NRT Migration Playbook Reference, Trust annotated libraries, Wrapping unannotated or legacy APIs

### Community 97 - ".MapToExpertDto"
Cohesion: 0.30
Nodes (5): User, ICollection, List, User, UserMapper

### Community 98 - ".Handle"
Cohesion: 0.15
Nodes (12): DateTime, Guid, AdminDto, Administrator, AdminMapper, IFormFile, CreateAdminCommand, CancellationToken (+4 more)

### Community 99 - "Anti-Patterns to Avoid"
Cohesion: 0.25
Nodes (8): Anti-Patterns to Avoid, Don't: Block on async code, Don't: Create deep inheritance hierarchies, Don't: Forget CancellationToken in async methods, Don't: Return List<T> when you mean IReadOnlyList<T>, Don't: Use byte[] when ReadOnlySpan<byte> works, Don't: Use classes for value objects, Don't: Use mutable DTOs

### Community 100 - "AuthenticationService"
Cohesion: 0.06
Nodes (36): Trivo.Application.Features.Users.Commands.LoginUser, CancellationToken, HttpPost, ProducesResponseType, SwaggerOperation, Task, AuthController, RefreshTokenRequest (+28 more)

### Community 101 - "Report"
Cohesion: 0.08
Nodes (22): DateTime, Guid, Report, CreatedAt, FinalReason, Message, MessageId, Note (+14 more)

### Community 102 - "AccountAccessService"
Cohesion: 0.16
Nodes (11): CancellationToken, DateTime, ILogger, Task, TimeSpan, User, AccountAccessService, SanctionType (+3 more)

### Community 103 - "PagedResult"
Cohesion: 0.18
Nodes (12): GetBannedUsersPagedQuery, CancellationToken, ILogger, Task, GetBannedUsersPagedQueryHandler, GetBannedUsersPagedValidator, IEnumerable, PagedResult (+4 more)

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

### Community 109 - "MatchUpdateStatus"
Cohesion: 0.50
Nodes (3): MatchUpdateStatus, Completed, Rejected

### Community 110 - "Performance and API Design Patterns"
Cohesion: 0.29
Nodes (6): Accept Abstractions, Return Appropriately Specific, API Design Principles, Contents, Method Signatures Best Practices, Performance and API Design Patterns, Span<T> and Memory<T> for Zero-Allocation Code

### Community 111 - "Value Objects and Pattern Matching"
Cohesion: 0.29
Nodes (7): Constraint-Enforcing Value Objects, Contents, No Implicit Conversions, Pattern Matching (C# 8-12), TypeConverter Support for Configuration Binding, Value Objects and Pattern Matching, Value Objects as readonly record struct

### Community 113 - ".Handle"
Cohesion: 0.18
Nodes (10): Guid, InterestByCategoryIdDto, Guid, IEnumerable, GetInterestsByCategoryIdQuery, CancellationToken, ILogger, Task (+2 more)

### Community 114 - "Sanction"
Cohesion: 0.14
Nodes (13): DateTime, Guid, Sanction, Admin, AdminId, ExpiresAt, Reason, Report (+5 more)

### Community 115 - ".Handle"
Cohesion: 0.31
Nodes (7): UserProfilePictureDto, Guid, GetUserProfilePictureQuery, CancellationToken, ILogger, Task, GetUserProfilePictureQueryHandler

### Community 116 - "ReportDto"
Cohesion: 0.20
Nodes (8): DateTime, Guid, MessageReportDto, DateTime, Guid, ReportDto, Guid, UserReportDto

### Community 117 - "SendFileCommand"
Cohesion: 0.26
Nodes (10): Authorize, CancellationToken, HttpPost, ISender, Task, MessageController, Guid, IFormFile (+2 more)

### Community 118 - "InitialCreate"
Cohesion: 0.25
Nodes (6): Migration, DateTime, Guid, MigrationBuilder, Vector, InitialCreate

### Community 119 - ".GetExpertIdAsync"
Cohesion: 0.25
Nodes (6): CancellationToken, Guid, Task, CancellationToken, Guid, Task

### Community 120 - ".GetRecruiterIdAsync"
Cohesion: 0.25
Nodes (6): CancellationToken, Guid, Task, CancellationToken, Guid, Task

### Community 121 - "Sanction"
Cohesion: 0.29
Nodes (8): Sanction, EntityTypeBuilder, SanctionConfig, CancellationToken, DateTime, Guid, Task, SanctionRepository

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

### Community 127 - "Roles"
Cohesion: 0.15
Nodes (11): Roles, Administrator, Expert, Recruiter, User, Distance, IReadOnlyCollection, IReadOnlyList (+3 more)

### Community 128 - ".ToEntity"
Cohesion: 0.33
Nodes (4): Trivo.Application.Features.Administrator.Commands.CreateAdministrator.Mappings, Administrator, CreateAdminCommand, AdminMappingExtensions

### Community 129 - "UserMappingExtensions.cs"
Cohesion: 0.33
Nodes (4): Trivo.Application.Features.Users.Commands.CreateUser.Mappings, CreateUserCommand, User, UserMappingExtensions

### Community 130 - "CreateMatchingCommand"
Cohesion: 0.22
Nodes (10): Authorize, CancellationToken, HttpPost, HttpPut, ISender, Task, MatchController, Guid (+2 more)

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

### Community 143 - "ReportListItemDto"
Cohesion: 0.23
Nodes (10): DateTime, Guid, ReportListItemDto, IEnumerable, GetLatestReportsQuery, CancellationToken, IEnumerable, ILogger (+2 more)

### Community 144 - ".Handle"
Cohesion: 0.17
Nodes (11): GetReportsPagedQuery, CancellationToken, ILogger, Task, GetReportsPagedQueryHandler, GetReportsPagedValidator, ReportStatus, Approved (+3 more)

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

### Community 150 - "AiSetting"
Cohesion: 0.40
Nodes (4): AiSetting, ApiKey, EmbeddingModel, Provider

### Community 151 - ".AddServices"
Cohesion: 0.20
Nodes (10): Guid, IEnumerable, Task, INotificationNotifier, IUserIdProvider, Guid, IEnumerable, IHubContext (+2 more)

### Community 152 - "Known Static-Analysis Limitations and Safe Patterns"
Cohesion: 0.50
Nodes (4): Arrays and default values, Known Static-Analysis Limitations and Safe Patterns, Other limitations to keep in mind, Structs with non-nullable fields

### Community 153 - "Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`"
Cohesion: 0.50
Nodes (4): Conditional postconditions: `NotNullWhen`, `MaybeNullWhen`, `NotNullIfNotNull`, `[MaybeNullWhen(bool)]`, `[NotNullIfNotNull(string)]`, `[NotNullWhen(bool)]`

### Community 154 - "SanctionDto"
Cohesion: 0.21
Nodes (10): DateTime, Guid, SanctionDto, Guid, GetSanctionsHistoryQuery, CancellationToken, ILogger, Task (+2 more)

### Community 155 - "Preconditions: `AllowNull` and `DisallowNull`"
Cohesion: 0.67
Nodes (3): `[AllowNull]`, `[DisallowNull]`, Preconditions: `AllowNull` and `DisallowNull`

### Community 156 - "Performance Patterns"
Cohesion: 0.67
Nodes (3): Async/Await Best Practices, Performance Patterns, Span<T> and Memory<T>

### Community 158 - "Trivo.Infrastructure.Persistence.Migrations"
Cohesion: 0.28
Nodes (3): Trivo.Infrastructure.Persistence.Migrations, MigrationBuilder, AddPendingEmailToUser

### Community 159 - "ReportMapper"
Cohesion: 0.32
Nodes (4): DateTime, Message, User, ReportMapper

### Community 160 - "UpdateNameCommand"
Cohesion: 0.24
Nodes (8): UpdateNameDto, Guid, UpdateNameCommand, CancellationToken, ILogger, Task, UpdateNameCommandHandler, UpdateNameValidator

### Community 161 - ".ToDto"
Cohesion: 0.25
Nodes (6): Guid, UserChatDto, Chat, Guid, User, ChatMapper

### Community 162 - "UpdateRecruiterCommandHandler.cs"
Cohesion: 0.31
Nodes (4): Trivo.Application.Features.Recruiters.Commands.UpdateRecruiter, Trivo.Application.Features.Recruiters, Trivo.Application.DTOs.Recruiter, Trivo.Application.Features.Recruiters.Commands.CreateRecruiter

### Community 163 - "AbstractValidator"
Cohesion: 0.04
Nodes (44): AbstractValidator, Trivo.Application.Features.Administrator.Commands.BanUser, Trivo.Application.Features.Skills.Commands.UpdateSkill, IBaseCommand, ICommand, Guid, BanUserCommand, BanUserValidator (+36 more)

### Community 164 - "AddUniqueExpertRecruiterUserId"
Cohesion: 0.20
Nodes (6): MigrationBuilder, DateTime, Guid, ModelBuilder, Vector, AddUniqueExpertRecruiterUserId

### Community 165 - "CodeRepository"
Cohesion: 0.38
Nodes (4): CancellationToken, Guid, Task, CodeRepository

### Community 166 - ".BuildModel"
Cohesion: 0.25
Nodes (6): ModelSnapshot, DateTime, Guid, ModelBuilder, Vector, TrivoContextModelSnapshot

### Community 167 - "ControllerBase"
Cohesion: 0.12
Nodes (14): ControllerBase, Authorize, CancellationToken, HttpPost, ISender, Task, ChatController, Authorize (+6 more)

### Community 168 - ".UpdateExpertAsync"
Cohesion: 0.18
Nodes (10): Authorize, CancellationToken, Guid, HttpPost, HttpPut, ISender, ProducesResponseType, Task (+2 more)

### Community 169 - ".Validate"
Cohesion: 0.40
Nodes (4): CancellationToken, Expression, Func, Task

### Community 170 - "ExpertRepository"
Cohesion: 0.36
Nodes (7): CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task, ExpertRepository

### Community 171 - "MatchDto"
Cohesion: 0.22
Nodes (10): Guid, List, ExpertAiRecommendationDto, DateTime, Guid, MatchDto, Guid, List (+2 more)

### Community 172 - "ICodeService"
Cohesion: 0.09
Nodes (24): Trivo.Application.Features.Users.Commands.ResetPassword, DateTime, Guid, CodeDto, Guid, ConfirmAccountCommand, CancellationToken, ILogger (+16 more)

### Community 173 - "IExpertRepository"
Cohesion: 0.07
Nodes (33): Guid, ExpertDto, Guid, RecruiterDto, Guid, CreateExpertCommand, CancellationToken, ILogger (+25 more)

### Community 174 - "InterestWithIdDto"
Cohesion: 0.13
Nodes (17): Guid, InterestWithIdDto, IEnumerable, SearchInterestsByNameQuery, CancellationToken, IEnumerable, ILogger, Task (+9 more)

### Community 175 - "IUserOwnedRequest"
Cohesion: 0.07
Nodes (26): Trivo.Application.Features.Users.Commands.UpdateBiography, Trivo.Application.Features.Interests.Commands.UpdateInterest, Trivo.Application.Features.Users.Commands.UpdateProfilePicture, ReportType, Guid, IUserOwnedRequest, UserId, Guid (+18 more)

### Community 176 - ".ValidateAsync"
Cohesion: 0.32
Nodes (6): CancellationToken, Expression, Func, Guid, Task, GenericRepository

### Community 177 - ".Handle"
Cohesion: 0.24
Nodes (8): UpdateUsernameDto, Guid, UpdateUsernameCommand, CancellationToken, ILogger, Task, UpdateUsernameCommandHandler, UpdateUsernameValidator

### Community 178 - ".Handle"
Cohesion: 0.22
Nodes (8): Trivo.Application.Features.Users.Commands.UpdatePassword, Guid, UpdatePasswordCommand, CancellationToken, ILogger, Task, UpdatePasswordCommandHandler, UpdatePasswordValidator

### Community 179 - ".Handle"
Cohesion: 0.22
Nodes (8): IEnumerable, SearchSkillsByNameQuery, CancellationToken, IEnumerable, ILogger, Task, SearchSkillsByNameQueryHandler, SearchSkillsByNameValidator

### Community 180 - "GetExpertsPagedQuery"
Cohesion: 0.24
Nodes (9): DateTime, Guid, AdminExpertDto, GetExpertsPagedQuery, CancellationToken, ILogger, Task, GetExpertsPagedQueryHandler (+1 more)

### Community 181 - "Administrator"
Cohesion: 0.20
Nodes (10): Administrator, Biography, Email, FirstName, IsActive, LastName, LinkedIn, PasswordHash (+2 more)

### Community 182 - ".BuildTargetModel"
Cohesion: 0.40
Nodes (4): DateTime, Guid, ModelBuilder, Vector

### Community 183 - ".BuildTargetModel"
Cohesion: 0.40
Nodes (4): DateTime, Guid, ModelBuilder, Vector

### Community 184 - "UserAiRecommendationDto"
Cohesion: 0.13
Nodes (18): Guid, List, UserAiRecommendationDto, Guid, List, GetUsersByInterestsAndSkillsQuery, CancellationToken, ILogger (+10 more)

### Community 185 - "GetRecruitersPagedQuery"
Cohesion: 0.24
Nodes (9): DateTime, Guid, AdminRecruiterDto, GetRecruitersPagedQuery, CancellationToken, ILogger, Task, GetRecruitersPagedQueryHandler (+1 more)

### Community 186 - "MatchHub"
Cohesion: 0.29
Nodes (6): Hub, Exception, ILogger, IMediator, Task, MatchHub

### Community 187 - "IQuery"
Cohesion: 0.12
Nodes (17): IRequest, IQuery, ActiveUsersCountDto, ReportedUsersCountDto, GetActiveUsersCountQuery, CancellationToken, ILogger, Task (+9 more)

### Community 188 - ".Handle"
Cohesion: 0.22
Nodes (8): List, ExpertDetailsDto, List, RecruiterDetailsDto, List, UserDetailsDto, CancellationToken, Task

### Community 189 - "IReportRepository"
Cohesion: 0.31
Nodes (7): ILogger, GetReportByIdQueryHandler, CancellationToken, Guid, IReadOnlyList, Task, IReportRepository

### Community 190 - "ICodeRepository"
Cohesion: 0.40
Nodes (4): CancellationToken, Guid, Task, ICodeRepository

### Community 191 - "IQueryHandler"
Cohesion: 0.22
Nodes (9): IRequestHandler, IQueryHandler, Guid, GetChatPaginationQuery, CancellationToken, ILogger, Task, GetChatPaginationQueryHandler (+1 more)

### Community 194 - "NotificationDto"
Cohesion: 0.31
Nodes (7): DateTime, Guid, NotificationDto, Guid, IEnumerable, Task, INotificationHub

### Community 195 - "UserRecommendationHub"
Cohesion: 0.19
Nodes (8): IEnumerable, Task, IUserRecommendationHub, Exception, ILogger, IMediator, Task, UserRecommendationHub

### Community 196 - ".GetDetailsByUserIdsAsync"
Cohesion: 0.36
Nodes (6): CancellationToken, Guid, IEnumerable, IReadOnlyList, List, Task

### Community 197 - "CacheEntryOptions"
Cohesion: 0.18
Nodes (9): IReadOnlyList, TimeSpan, CacheEntryOptions, AbsoluteExpiration, Tags, CacheProfiles, Cold, Hot (+1 more)

### Community 198 - "ResolveReportCommand"
Cohesion: 0.28
Nodes (6): Guid, ResolveReportCommand, ResolveReportValidator, ReportDecision, Approved, Rejected

### Community 199 - "ReportRepository"
Cohesion: 0.42
Nodes (5): CancellationToken, Guid, IReadOnlyList, Task, ReportRepository

### Community 200 - "MissingByMatching"
Cohesion: 0.50
Nodes (3): MissingByMatching, Expert, Recruiter

### Community 201 - "ChatHub"
Cohesion: 0.28
Nodes (6): Exception, Guid, ILogger, IMediator, Task, ChatHub

### Community 202 - ".Handle"
Cohesion: 0.36
Nodes (6): CompletedMatchesCountDto, GetCompletedMatchesCountQuery, CancellationToken, ILogger, Task, GetCompletedMatchesCountQueryHandler

### Community 203 - "ISanctionRepository"
Cohesion: 0.50
Nodes (5): CancellationToken, DateTime, Guid, Task, ISanctionRepository

### Community 204 - "AddReportSanctions"
Cohesion: 0.32
Nodes (4): DateTime, Guid, MigrationBuilder, AddReportSanctions

### Community 205 - "BaseEntity"
Cohesion: 0.29
Nodes (6): DateTime, Guid, BaseEntity, CreatedAt, Id, UpdatedAt

### Community 206 - "JwtSetting"
Cohesion: 0.33
Nodes (5): JwtSetting, Audience, DurationInMinutes, Issuer, Key

### Community 207 - ".GetOrSetAsync"
Cohesion: 0.50
Nodes (3): CancellationToken, Func, Task

### Community 208 - ".BuildTargetModel"
Cohesion: 0.40
Nodes (4): DateTime, Guid, ModelBuilder, Vector

### Community 209 - "ReportType"
Cohesion: 0.50
Nodes (3): ReportType, Message, Profile

## Knowledge Gaps
- **636 isolated node(s):** `$schema`, `windowsAuthentication`, `anonymousAuthentication`, `applicationUrl`, `sslPort` (+631 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **3 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `ResultT` connect `ResultT` to `CreateMatchingCommand`, `InterestCategory`, `.Handle`, `ReportListItemDto`, `.Handle`, `.NotFound`, `MatchDetailsDto`, `ICommandHandler`, `MessageDto`, `SanctionDto`, `UserDto`, `.Handle`, `.Handle`, `AdminController`, `UpdateNameCommand`, `NotificationService`, `AbstractValidator`, `UserInterest`, `ControllerBase`, `.UpdateExpertAsync`, `NotificationHub`, `Error`, `ICodeService`, `IExpertRepository`, `.SaveChangesAsync`, `InterestWithIdDto`, `.Handle`, `.Handle`, `.Handle`, `IChatRepository`, `.GetByCategoriesAsync`, `GetExpertsPagedQuery`, `.Handle`, `.Handle`, `.CreateSkillAsync`, `GetRecruitersPagedQuery`, `UserAiRecommendationDto`, `IQuery`, `.Handle`, `ResolveReportCommandHandler`, `IQueryHandler`, `ISkillRepository`, `.ValidateEmailAsync`, `.CreateMatchNotificationAsync`, `.Handle`, `.UpdateRecruiterAsync`, `.Handle`, `SkillWithIdDto`, `.Handle`, `ReportDetailDto`, `.Handle`, `AuthenticationService`, `PagedResult`, `.Handle`, `.Handle`, `SendFileCommand`?**
  _High betweenness centrality (0.140) - this node is a cross-community bridge._
- **Why does `Trivo.Domain.Models` connect `Trivo.Domain.Models` to `Message`, `InterestCategory`, `Trivo.Application.DTOs.Interests`, `CreateInterestCategoryCommandHandler.cs`, `Trivo.Infrastructure.Persistence.Configurations`, `Trivo.Application.DTOs.Users`, `Trivo.Domain.Enums`, `Notification`, `Expert`, `Recruiter`, `User`, `Code`, `UpdateRecruiterCommandHandler.cs`, `Chat`, `UserInterest`, `UserSkill`, `Trivo.Application.Utils`, `Skill`, `UpdateExpertCommandHandler.cs`, `Trivo.Application.DTOs.Notifications`, `Trivo.Application.Abstractions.Messages`, `Match`, `Report`, `Sanction`, `Sanction`?**
  _High betweenness centrality (0.074) - this node is a cross-community bridge._
- **Why does `PagedResult` connect `PagedResult` to `Message`, `InterestCategory`, `.Handle`, `ResultT`, `Notification`, `.Handle`, `IGenericRepository`, `MessageDto`, `SanctionDto`, `UserDto`, `.Handle`, `AdminController`, `NotificationService`, `Chat`, `IInterestRepository`, `NotificationHub`, `IExpertRepository`, `.ValidateAsync`, `IChatRepository`, `.GetByCategoriesAsync`, `GetExpertsPagedQuery`, `.CreateSkillAsync`, `GetRecruitersPagedQuery`, `UserAiRecommendationDto`, `IQuery`, `AdministratorRepository`, `IReportRepository`, `IQueryHandler`, `ISkillRepository`, `ReportRepository`, `ISanctionRepository`, `SkillRepository`, `IAdministratorRepository`, `.Handle`, `.Handle`, `Sanction`?**
  _High betweenness centrality (0.067) - this node is a cross-community bridge._
- **What connects `$schema`, `windowsAuthentication`, `anonymousAuthentication` to the rest of the system?**
  _636 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Message` be split into smaller, more focused modules?**
  _Cohesion score 0.09407665505226481 - nodes in this community are weakly interconnected._
- **Should `InterestCategory` be split into smaller, more focused modules?**
  _Cohesion score 0.05628415300546448 - nodes in this community are weakly interconnected._
- **Should `Trivo.Application.DTOs.Interests` be split into smaller, more focused modules?**
  _Cohesion score 0.07200929152148665 - nodes in this community are weakly interconnected._