# Graph Report - Trivo  (2026-08-29)

## Corpus Check
- 377 files · ~37,598 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2444 nodes · 5996 edges · 147 communities (143 shown, 4 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 372 edges (avg confidence: 0.85)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `ce27e84b`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- IUserRepository
- Trivo.Application.Abstractions.Messages
- Message
- InterestCategory
- Trivo.Application.DTOs.Users
- Trivo.Domain.Models
- TokenResponseDto
- Trivo.Infrastructure.Persistence.Configurations
- .Handle
- Trivo.Application.Interfaces.Services
- Trivo.Application.Interfaces.SignalR
- ExpertDto
- ResultT
- Expert
- Notification
- .NotFound
- Match
- .AddRepositories
- ChatDto
- Code
- Trivo.Domain.Enums
- MatchDetailsDto
- User
- Trivo.API.Controllers.V1.Requests
- PagedResult
- ICodeService
- AbstractValidator
- IQuery
- Recruiter
- AdministratorRepository
- .AddServices
- .Failure
- AdminController
- NotificationDto
- Chat
- .Handle
- IAdministratorRepository
- InterestRepository
- CreateRecruiterCommand
- ServiceExtensions
- .CreateMatchNotificationAsync
- ICommand
- JwtSetting
- MessageDto
- Interest
- Trivo.API.csproj
- .SaveChangesAsync
- IInterestRepository
- MatchHub
- .Build
- .Handle
- .GetCompletionAsync
- .GetByCategoriesAsync
- ICommandHandler
- SkillDto
- .Handle
- .Handle
- .AddSkillsToUserAsync
- .GetRolesAsync
- Trivo.Infrastructure.Shared.csproj
- .CreateSkillAsync
- .Handle
- .Handle
- InterestWithIdDto
- SkillWithIdDto
- CreateMatchingCommandHandler
- .Handle
- ISkillRepository
- SkillRepository
- AiNotifier
- .Validate
- UpdateInterestCommand
- .Handle
- .Handle
- UserAiRecommendationDto
- ICodeRepository
- IGenericRepository
- IMatchRepository
- UserRecommendationHub
- .ValidateAsync
- MatchRepository
- .Handle
- .UpdateRecruiterAsync
- .ValidateEmailAsync
- GetInterestsPaginationQueryHandler
- RecruiterDto
- .Handle
- .Handle
- IChatRepository
- Administrator
- RealTimeNotifier
- GetPaginatedInterestCategoriesQueryHandler.cs
- ValidationBehavior
- Trivo.Application.csproj
- GetUserBiographyQueryHandler
- GetUserProfilePictureQueryHandler
- .Handle
- .MapToInterests
- IChatHub
- IRealTimeNotifier
- ChatUser
- Report
- ChatHub
- NotificationHub
- ResultFilter
- ControllerBase
- .GetUserId
- .GetOrCreateAsync
- Trivo
- .CreateMatchAsync
- .SendFileAsync
- UnbanUserCommandHandler
- UpdateBiographyCommand
- Skill
- MatchNotifier
- GetActiveUsersCountQueryHandler
- .Handle
- GetReportedUsersCountQueryHandler
- .Handle
- .GetExpertIdAsync
- .GetRecruiterIdAsync
- INotificationHub
- EmailSetting
- Trivo.Infrastructure.Persistence.csproj
- BanUserCommandHandler
- MessageStatus
- Trivo.sln
- Trivo.Application.DTOs.Reports
- .ToEntity
- UserMappingExtensions.cs
- GetSkillsPaginationQueryHandler
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
- MissingByMatching
- ReportStatus
- CodeGenerator.cs
- CreateMatchValidator

## God Nodes (most connected - your core abstractions)
1. `ResultT` - 136 edges
2. `Trivo.Application.Abstractions.Messages` - 110 edges
3. `Trivo.Domain.Models` - 89 edges
4. `Trivo.Application.Utils` - 87 edges
5. `PagedResult` - 69 edges
6. `IUserRepository` - 57 edges
7. `Trivo.Application.Interfaces.Repository.Account` - 54 edges
8. `Trivo.Domain.Enums` - 48 edges
9. `Trivo.Application.Interfaces.Repository` - 46 edges
10. `Trivo.Application.Pagination` - 46 edges

## Surprising Connections (you probably didn't know these)
- `UserController` --references--> `ICodeService`  [EXTRACTED]
  src/API/Trivo.API/Controllers/V1/UserController.cs → src/Application/Trivo.Application/Interfaces/Services/ICodeService.cs
- `UserController` --references--> `IEmailValidationService`  [EXTRACTED]
  src/API/Trivo.API/Controllers/V1/UserController.cs → src/Application/Trivo.Application/Interfaces/Services/IEmailValidationService.cs
- `ICommand` --references--> `Result`  [EXTRACTED]
  src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs → src/Application/Trivo.Application/Utils/Result.cs
- `ICommand` --references--> `ResultT`  [EXTRACTED]
  src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs → src/Application/Trivo.Application/Utils/Result.cs
- `BanUserCommand` --implements--> `ICommand`  [EXTRACTED]
  src/Application/Trivo.Application/Features/Administrator/Commands/BanUser/BanUserCommand.cs → src/Application/Trivo.Application/Abstractions/Messages/ICommand.cs

## Import Cycles
- None detected.

## Communities (147 total, 4 thin omitted)

### Community 0 - "IUserRepository"
Cohesion: 0.05
Nodes (52): Trivo.Application.Features.Users.Commands.ChangePassword, ChangePasswordCommand, CancellationToken, ILogger, Task, ChangePasswordCommandHandler, ChangePasswordValidator, Guid (+44 more)

### Community 1 - "Trivo.Application.Abstractions.Messages"
Cohesion: 0.07
Nodes (20): Trivo.Application.Abstractions.Messages, Trivo.Application.Features.Users.Commands.UpdatePassword, Trivo.Application.Features.Administrator.Query.GetCompletedMatchesCount, Trivo.Application.Features.Users.Commands.UpdateBiography, Trivo.Application.Interfaces.UnitOfWork, Trivo.Application.Features.Administrator.Query.GetLatestMatches, Trivo.Application.Features.Administrator.Query.GetLatestUsersPaged, Trivo.Application.Features.Administrator.Commands.CreateAdministrator (+12 more)

### Community 2 - "Message"
Cohesion: 0.06
Nodes (43): DateTime, Guid, MessageReportDto, Guid, ReportDto, Guid, UserReportDto, Guid (+35 more)

### Community 3 - "InterestCategory"
Cohesion: 0.06
Nodes (37): Authorize, CancellationToken, HttpGet, HttpPost, ISender, ProducesResponseType, Task, InterestCategoryController (+29 more)

### Community 4 - "Trivo.Application.DTOs.Users"
Cohesion: 0.06
Nodes (20): Trivo.Application.Features.Skills.Query.GetSkillsPagination, Trivo.Application.Features.Users.Commands.CreateUser, Trivo.Application.Features.Interests.Commands.CreateInterest, Trivo.Application.Features.Users.Query.GetUserDetails, Trivo.Application.Features.Skills, Trivo.Application.Features.Users.Commands.UpdateUser, Trivo.Application.Features.Users.Query.GetUserInterests, Trivo.Application.Features.Interests.Query.GetInterestsByCategoryId (+12 more)

### Community 5 - "Trivo.Domain.Models"
Cohesion: 0.12
Nodes (9): Trivo.Domain.Common, Trivo.Infrastructure.Persistence.Context, Trivo.Infrastructure.Persistence.Repository.Account, Trivo.Application.Interfaces.Repository.Base, Trivo.Infrastructure.Persistence.Repository, Trivo.Domain.Models, Trivo.Application.Interfaces.Repository, Trivo.Infrastructure.Persistence.Base (+1 more)

### Community 6 - "TokenResponseDto"
Cohesion: 0.07
Nodes (32): CancellationToken, HttpPost, ProducesResponseType, SwaggerOperation, Task, AuthController, RefreshTokenRequest, RefreshToken (+24 more)

### Community 7 - "Trivo.Infrastructure.Persistence.Configurations"
Cohesion: 0.05
Nodes (24): Trivo.Infrastructure.Persistence.Configurations, IEntityTypeConfiguration, EntityTypeBuilder, AdministratorConfig, EntityTypeBuilder, ChatConfig, EntityTypeBuilder, ChatUserConfig (+16 more)

### Community 8 - ".Handle"
Cohesion: 0.06
Nodes (32): DateTime, Guid, AdminDto, Administrator, AdminMapper, IFormFile, CreateAdminCommand, CancellationToken (+24 more)

### Community 9 - "Trivo.Application.Interfaces.Services"
Cohesion: 0.07
Nodes (17): Trivo.API.Filters, Trivo.API.Controllers.V1, Trivo.Application.DTOs.Notifications, Trivo.Application.DTOs.Email, Trivo.Application.Features.Users.Commands.LoginUser, Trivo.Infrastructure.Shared.Services, Trivo.Infrastructure.Persistence.Services, Trivo.Domain.Configurations (+9 more)

### Community 10 - "Trivo.Application.Interfaces.SignalR"
Cohesion: 0.09
Nodes (11): Trivo.Application.Features.Messages.Commands.SendImage, Trivo.Application.Features.Messages.Query.GetMessagePagination, Trivo.Application.Features.Chat.Query.GetChatPagination, Trivo.Application.Features.Chat, Trivo.Application.Interfaces.SignalR, Trivo.Application.Features.Chat.Commands.CreateChat, Trivo.Infrastructure.Shared.SignalR, Trivo.Application.Features.Messages.Commands.SendMessage (+3 more)

### Community 11 - "ExpertDto"
Cohesion: 0.08
Nodes (24): Trivo.Application.DTOs.Expert, Trivo.Application.Features.Experts.Commands.CreateExpert, Trivo.Application.Features.Experts, Authorize, CancellationToken, Guid, HttpPost, HttpPut (+16 more)

### Community 12 - "ResultT"
Cohesion: 0.24
Nodes (13): Authorize, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IEnumerable, ISender (+5 more)

### Community 13 - "Expert"
Cohesion: 0.12
Nodes (20): CancellationToken, Guid, IEnumerable, List, Task, Guid, ICollection, Expert (+12 more)

### Community 14 - "Notification"
Cohesion: 0.11
Nodes (21): CancellationToken, Guid, Task, INotificationRepository, DateTime, Guid, Notification, Content (+13 more)

### Community 15 - ".NotFound"
Cohesion: 0.16
Nodes (14): PaginationError, ErrorType, Error, Code, Description, ErrorType, Result, Error (+6 more)

### Community 16 - "Match"
Cohesion: 0.08
Nodes (23): DateTime, Guid, BaseEntity, CreatedAt, Id, UpdatedAt, Guid, Match (+15 more)

### Community 17 - ".AddRepositories"
Cohesion: 0.15
Nodes (18): DbContext, DbContextOptions, IExpertRepository, IReportRepository, IGetExpertIdService, IGetRecruiterIdService, IUserRoleService, CancellationToken (+10 more)

### Community 18 - "ChatDto"
Cohesion: 0.10
Nodes (21): CancellationToken, HttpPost, ISender, Task, ChatController, DateTime, Guid, List (+13 more)

### Community 19 - "Code"
Cohesion: 0.14
Nodes (17): DateTime, Guid, Code, CodeId, CreatedAt, ExpiresAt, IsRevoked, IsUsed (+9 more)

### Community 20 - "Trivo.Domain.Enums"
Cohesion: 0.13
Nodes (8): Trivo.Application.Features.Matching.Commands.UpdateMatch, Trivo.Application.Features.Matching.Commands.CreateMatch, Trivo.Application.Features.Matching, Trivo.Domain.Enums, Trivo.Application.Features.Matching.Commands.CreateMatchRejection, Trivo.Application.Features.Users.Query.GetUserRecommendations, Trivo.Application.Features.Matching.Query.GetMatchByUser, Trivo.Application.DTOs.Matching

### Community 21 - "MatchDetailsDto"
Cohesion: 0.14
Nodes (17): DateTime, Guid, MatchDetailsDto, Guid, UpdateMatchingCommand, CancellationToken, Guid, ILogger (+9 more)

### Community 22 - "User"
Cohesion: 0.08
Nodes (25): ICollection, User, Biography, ChatUsers, Codes, Email, Experts, FirstName (+17 more)

### Community 23 - "Trivo.API.Controllers.V1.Requests"
Cohesion: 0.08
Nodes (16): Trivo.API.Controllers.V1.Requests, ChangePasswordRequest, Guid, List, FilterUsersByInterestsAndSkillsRequest, UpdateBiographyRequest, UpdatePasswordRequest, IFormFile (+8 more)

### Community 24 - "PagedResult"
Cohesion: 0.11
Nodes (18): DateTime, Guid, AdminMatchDto, ExpertMatchDto, RecruiterMatchDto, AdminMatchMapper, GetLatestMatchesQuery, CancellationToken (+10 more)

### Community 25 - "ICodeService"
Cohesion: 0.13
Nodes (16): DateTime, Guid, CodeDto, Guid, ConfirmAccountCommand, CancellationToken, ILogger, Task (+8 more)

### Community 26 - "AbstractValidator"
Cohesion: 0.09
Nodes (12): AbstractValidator, BanUserValidator, CreateChatValidator, CreateMatchRejectionValidator, UpdateMatchingValidation, SendFileValidator, SendImageValidator, SendMessageValidator (+4 more)

### Community 27 - "IQuery"
Cohesion: 0.12
Nodes (20): IRequest, IRequestHandler, IQuery, IQueryHandler, Guid, UserDto, IEnumerable, GetLast10BannedUsersQuery (+12 more)

### Community 28 - "Recruiter"
Cohesion: 0.24
Nodes (13): CancellationToken, Guid, IEnumerable, List, Task, IRecruiterRepository, Recruiter, CancellationToken (+5 more)

### Community 29 - "AdministratorRepository"
Cohesion: 0.23
Nodes (8): Administrator, CancellationToken, Guid, IEnumerable, Match, Task, User, AdministratorRepository

### Community 30 - ".AddServices"
Cohesion: 0.12
Nodes (15): Trivo.Application.Features.Users.Commands.ForgotPassword, EmailResponseDto, ForgotPasswordCommand, CancellationToken, ILogger, Task, ForgotPasswordCommandHandler, ForgotPasswordValidator (+7 more)

### Community 31 - ".Failure"
Cohesion: 0.20
Nodes (10): Guid, CreateNotificationDto, IEnumerable, List, NotificationMapper, CancellationToken, Guid, ILogger (+2 more)

### Community 32 - "AdminController"
Cohesion: 0.29
Nodes (11): Authorize, CancellationToken, Guid, HttpGet, HttpPost, HttpPut, IEnumerable, ISender (+3 more)

### Community 33 - "NotificationDto"
Cohesion: 0.20
Nodes (12): DateTime, Guid, NotificationDto, Guid, IEnumerable, Task, INotificationNotifier, Guid (+4 more)

### Community 34 - "Chat"
Cohesion: 0.24
Nodes (13): ICollection, Chat, ChatType, ChatUsers, IsActive, Messages, CancellationToken, Chat (+5 more)

### Community 35 - ".Handle"
Cohesion: 0.16
Nodes (12): Guid, List, ExpertAiRecommendationDto, DateTime, Guid, MatchDto, Guid, List (+4 more)

### Community 36 - "IAdministratorRepository"
Cohesion: 0.26
Nodes (5): CancellationToken, Guid, IEnumerable, Task, IAdministratorRepository

### Community 37 - "InterestRepository"
Cohesion: 0.30
Nodes (7): CancellationToken, Guid, IEnumerable, Interest, List, Task, InterestRepository

### Community 38 - "CreateRecruiterCommand"
Cohesion: 0.13
Nodes (11): Trivo.Application.Features.Recruiters.Commands.UpdateRecruiter, Trivo.Application.Features.Recruiters, Trivo.Application.DTOs.Recruiter, Trivo.Application.Features.Recruiters.Commands.CreateRecruiter, ISender, RecruiterController, Guid, CreateRecruiterCommand (+3 more)

### Community 39 - "ServiceExtensions"
Cohesion: 0.12
Nodes (12): Trivo.API.Middlewares, Trivo.API.Extensions, IApplicationBuilder, IEndpointRouteBuilder, RequestDelegate, IConfiguration, IServiceCollection, ServiceExtensions (+4 more)

### Community 40 - ".CreateMatchNotificationAsync"
Cohesion: 0.21
Nodes (12): HttpDelete, Authorize, CancellationToken, Guid, HttpPost, HttpPut, Task, NotificationController (+4 more)

### Community 41 - "ICommand"
Cohesion: 0.13
Nodes (15): IBaseCommand, ICommand, Guid, IFormFile, SendFileCommand, CancellationToken, ILogger, Task (+7 more)

### Community 42 - "JwtSetting"
Cohesion: 0.14
Nodes (12): JwtResponse, AiSetting, ApiKey, Model, JwtSetting, Audience, DurationInMinutes, Issuer (+4 more)

### Community 43 - "MessageDto"
Cohesion: 0.14
Nodes (16): DateTime, Guid, MessageDto, Guid, IFormFile, SendImageCommand, CancellationToken, ILogger (+8 more)

### Community 44 - "Interest"
Cohesion: 0.14
Nodes (14): Guid, IEnumerable, Interest, InterestMapper, Guid, ICollection, Interest, Categories (+6 more)

### Community 45 - "Trivo.API.csproj"
Cohesion: 0.11
Nodes (17): Asp.Versioning.Mvc (8.1.0), AspNetCore.HealthChecks.Redis (9.0.0), Microsoft.AspNetCore.OpenApi (8.0.10), Microsoft.AspNetCore.SignalR.Core (1.2.0), Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore (8.0.10), Scalar.AspNetCore (2.17.1), Serilog.Enrichers.Environment (3.0.1), Serilog.Enrichers.Thread (4.0.0) (+9 more)

### Community 46 - ".SaveChangesAsync"
Cohesion: 0.14
Nodes (13): CreateInterestCategoryCommand, CancellationToken, ILogger, Task, CreateInterestCategoryCommandHandler, CreateInterestCategoryCommandValidator, CancellationToken, ILogger (+5 more)

### Community 47 - "IInterestRepository"
Cohesion: 0.34
Nodes (6): CancellationToken, Guid, IEnumerable, List, Task, IInterestRepository

### Community 48 - "MatchHub"
Cohesion: 0.17
Nodes (10): Hub, Guid, IEnumerable, Task, IMatchHub, Exception, ILogger, IMediator (+2 more)

### Community 49 - ".Build"
Cohesion: 0.19
Nodes (9): IReadOnlyCollection, IEnumerable, IReadOnlyList, User, UserRecommendationPromptBuilder, AiChatMessage, AiChatRole, System (+1 more)

### Community 50 - ".Handle"
Cohesion: 0.17
Nodes (13): List, ExpertDetailsDto, List, RecruiterDetailsDto, List, UserDetailsDto, Guid, GetUserDetailsQuery (+5 more)

### Community 51 - ".GetCompletionAsync"
Cohesion: 0.14
Nodes (11): ChatClient, ChatMessage, CancellationToken, IEnumerable, Task, IAiCompletionService, CancellationToken, IEnumerable (+3 more)

### Community 52 - ".GetByCategoriesAsync"
Cohesion: 0.23
Nodes (11): Authorize, CancellationToken, Guid, HttpGet, HttpPost, IEnumerable, ISender, List (+3 more)

### Community 53 - "ICommandHandler"
Cohesion: 0.14
Nodes (12): ICommandHandler, CancellationToken, ILogger, Task, CreateSkillCommandHandler, Guid, UpdatePasswordCommand, CancellationToken (+4 more)

### Community 54 - "SkillDto"
Cohesion: 0.17
Nodes (9): DateTime, Guid, SkillDto, Guid, CreateSkillCommand, CreateSkillValidator, Guid, IEnumerable (+1 more)

### Community 55 - ".Handle"
Cohesion: 0.19
Nodes (13): Guid, IEnumerable, GetMatchByUserQuery, CancellationToken, Dictionary, Func, Guid, IEnumerable (+5 more)

### Community 56 - ".Handle"
Cohesion: 0.19
Nodes (11): GeneratedRegex, Regex, Guid, GetUserRecommendationsQuery, CancellationToken, IDistributedCache, ILogger, List (+3 more)

### Community 57 - ".AddSkillsToUserAsync"
Cohesion: 0.21
Nodes (10): CancellationToken, Guid, List, Task, IUserSkillRepository, CancellationToken, Guid, List (+2 more)

### Community 58 - ".GetRolesAsync"
Cohesion: 0.14
Nodes (12): CancellationToken, Guid, IList, Task, Administrator, CancellationToken, Expert, Guid (+4 more)

### Community 59 - "Trivo.Infrastructure.Shared.csproj"
Cohesion: 0.15
Nodes (12): CloudinaryDotNet (1.27.0), MailKit (4.17.0), Microsoft.AspNetCore.Authentication.JwtBearer (8.0.10), Microsoft.AspNetCore.SignalR (1.2.0), Microsoft.Extensions.Options (10.0.3), Microsoft.Extensions.Options.ConfigurationExtensions (8.0.0), Microsoft.IdentityModel.Tokens (8.10.0), MimeKit (4.17.0) (+4 more)

### Community 60 - ".CreateSkillAsync"
Cohesion: 0.22
Nodes (10): Authorize, CancellationToken, HttpGet, HttpPost, IEnumerable, ISender, ProducesResponseType, Task (+2 more)

### Community 61 - ".Handle"
Cohesion: 0.19
Nodes (10): Guid, InterestByCategoryIdDto, Guid, IEnumerable, GetInterestsByCategoryIdQuery, CancellationToken, IDistributedCache, ILogger (+2 more)

### Community 62 - ".Handle"
Cohesion: 0.19
Nodes (9): Guid, InterestDetailsDto, Guid, CreateInterestCommand, CancellationToken, ILogger, Task, CreateInterestCommandHandler (+1 more)

### Community 63 - "InterestWithIdDto"
Cohesion: 0.21
Nodes (10): Guid, InterestWithIdDto, IEnumerable, SearchInterestsByNameQuery, CancellationToken, IDistributedCache, IEnumerable, ILogger (+2 more)

### Community 64 - "SkillWithIdDto"
Cohesion: 0.21
Nodes (11): Guid, SkillWithIdDto, Guid, IEnumerable, GetUserSkillsQuery, CancellationToken, IDistributedCache, IEnumerable (+3 more)

### Community 65 - "CreateMatchingCommandHandler"
Cohesion: 0.17
Nodes (12): Guid, CreateMatchingCommand, Dictionary, expertStatus, ILogger, recruiterStatus, CreateMatchingCommandHandler, Roles (+4 more)

### Community 66 - ".Handle"
Cohesion: 0.17
Nodes (12): Guid, CreateMatchRejectionCommand, CreatedBy, ExpertId, RecruiterId, CancellationToken, Dictionary, expertStatus (+4 more)

### Community 67 - "ISkillRepository"
Cohesion: 0.31
Nodes (6): CancellationToken, Guid, IEnumerable, List, Task, ISkillRepository

### Community 68 - "SkillRepository"
Cohesion: 0.32
Nodes (6): CancellationToken, Guid, IEnumerable, List, Task, SkillRepository

### Community 69 - "AiNotifier"
Cohesion: 0.26
Nodes (9): Guid, IEnumerable, Task, IAiNotifier, Guid, IEnumerable, IHubContext, Task (+1 more)

### Community 70 - ".Validate"
Cohesion: 0.15
Nodes (10): CancellationToken, Expression, Func, Task, IValidation, CancellationToken, Expression, Func (+2 more)

### Community 71 - "UpdateInterestCommand"
Cohesion: 0.20
Nodes (9): Trivo.Application.Features.Interests.Commands.UpdateInterest, Guid, IReadOnlyList, UpdateInterestCommand, CancellationToken, ILogger, Task, UpdateInterestCommandHandler (+1 more)

### Community 72 - ".Handle"
Cohesion: 0.20
Nodes (8): Trivo.Application.Features.Users.Commands.ResetPassword, Guid, ResetPasswordCommand, CancellationToken, ILogger, Task, ResetPasswordCommandHandler, ResetPasswordValidator

### Community 73 - ".Handle"
Cohesion: 0.20
Nodes (9): Trivo.Application.Features.Skills.Commands.UpdateSkill, Guid, List, UpdateSkillCommand, CancellationToken, ILogger, Task, UpdateSkillCommandHandler (+1 more)

### Community 74 - "UserAiRecommendationDto"
Cohesion: 0.21
Nodes (11): Guid, List, UserAiRecommendationDto, Guid, List, GetUsersByInterestsAndSkillsQuery, CancellationToken, IDistributedCache (+3 more)

### Community 75 - "ICodeRepository"
Cohesion: 0.38
Nodes (4): CancellationToken, Guid, Task, ICodeRepository

### Community 76 - "IGenericRepository"
Cohesion: 0.32
Nodes (6): CancellationToken, Expression, Func, Guid, Task, IGenericRepository

### Community 77 - "IMatchRepository"
Cohesion: 0.47
Nodes (6): CancellationToken, Guid, IEnumerable, Match, Task, IMatchRepository

### Community 78 - "UserRecommendationHub"
Cohesion: 0.21
Nodes (8): IEnumerable, Task, IUserRecommendationHub, Exception, ILogger, IMediator, Task, UserRecommendationHub

### Community 79 - ".ValidateAsync"
Cohesion: 0.32
Nodes (6): CancellationToken, Expression, Func, Guid, Task, GenericRepository

### Community 80 - "MatchRepository"
Cohesion: 0.48
Nodes (6): CancellationToken, Guid, IEnumerable, Match, Task, MatchRepository

### Community 81 - ".Handle"
Cohesion: 0.22
Nodes (8): Trivo.Application.Features.Experts.Commands.UpdateExpert, Guid, UpdateExpertCommand, CancellationToken, ILogger, Task, UpdateExpertCommandHandler, UpdateExpertValidator

### Community 82 - ".UpdateRecruiterAsync"
Cohesion: 0.22
Nodes (8): Authorize, CancellationToken, Guid, HttpPost, HttpPut, ProducesResponseType, Task, UpdateRecruiterRequest

### Community 83 - ".ValidateEmailAsync"
Cohesion: 0.22
Nodes (8): IServiceCollection, CancellationToken, Task, IEmailValidationService, CancellationToken, ILogger, Task, EmailValidationService

### Community 84 - "GetInterestsPaginationQueryHandler"
Cohesion: 0.24
Nodes (8): Guid, InterestDto, GetInterestsPaginationQuery, CancellationToken, IDistributedCache, ILogger, Task, GetInterestsPaginationQueryHandler

### Community 85 - "RecruiterDto"
Cohesion: 0.24
Nodes (9): Guid, RecruiterDto, Guid, UpdateRecruiterCommand, CancellationToken, ILogger, Task, UpdateRecruiterCommandHandler (+1 more)

### Community 86 - ".Handle"
Cohesion: 0.24
Nodes (8): UpdateUserDto, Guid, UpdateUserCommand, CancellationToken, ILogger, Task, UpdateUserCommandHandler, UpdateUserValidator

### Community 87 - ".Handle"
Cohesion: 0.22
Nodes (9): IEnumerable, SearchSkillsByNameQuery, CancellationToken, IDistributedCache, IEnumerable, ILogger, Task, SearchSkillsByNameQueryHandler (+1 more)

### Community 88 - "IChatRepository"
Cohesion: 0.45
Nodes (5): CancellationToken, Guid, IEnumerable, Task, IChatRepository

### Community 89 - "Administrator"
Cohesion: 0.18
Nodes (10): Administrator, Biography, Email, FirstName, IsActive, LastName, LinkedIn, PasswordHash (+2 more)

### Community 90 - "RealTimeNotifier"
Cohesion: 0.40
Nodes (5): Guid, IEnumerable, IHubContext, Task, RealTimeNotifier

### Community 91 - "GetPaginatedInterestCategoriesQueryHandler.cs"
Cohesion: 0.31
Nodes (4): Trivo.Application.DTOs.InterestCategories, Trivo.Application.Features.InterestCategories.Commands.CreateInterestCategory, Trivo.Application.Features.InterestCategories.Query.GetPaginatedInterestCategories, Trivo.Application.Features.InterestCategories

### Community 92 - "ValidationBehavior"
Cohesion: 0.20
Nodes (8): Trivo.Application.Behaviors, IPipelineBehavior, IValidator, RequestHandlerDelegate, CancellationToken, IEnumerable, Task, ValidationBehavior

### Community 93 - "Trivo.Application.csproj"
Cohesion: 0.20
Nodes (9): BCrypt.Net-Next (4.0.3), FluentValidation (11.10.0), FluentValidation.DependencyInjectionExtensions (11.10.0), Microsoft.Extensions.DependencyInjection.Abstractions (8.0.2), net8.0, MediatR (12.2.0), Microsoft.Extensions.Caching.StackExchangeRedis (8.0.10), Serilog.AspNetCore (8.0.0) (+1 more)

### Community 94 - "GetUserBiographyQueryHandler"
Cohesion: 0.27
Nodes (8): UserBiographyDto, Guid, GetUserBiographyQuery, CancellationToken, IDistributedCache, ILogger, Task, GetUserBiographyQueryHandler

### Community 95 - "GetUserProfilePictureQueryHandler"
Cohesion: 0.27
Nodes (8): UserProfilePictureDto, Guid, GetUserProfilePictureQuery, CancellationToken, IDistributedCache, ILogger, Task, GetUserProfilePictureQueryHandler

### Community 96 - ".Handle"
Cohesion: 0.24
Nodes (9): Guid, IEnumerable, GetUserInterestsQuery, CancellationToken, IDistributedCache, IEnumerable, ILogger, Task (+1 more)

### Community 97 - ".MapToInterests"
Cohesion: 0.47
Nodes (4): ICollection, List, User, UserMapper

### Community 98 - "IChatHub"
Cohesion: 0.33
Nodes (4): Guid, IEnumerable, Task, IChatHub

### Community 99 - "IRealTimeNotifier"
Cohesion: 0.47
Nodes (4): Guid, IEnumerable, Task, IRealTimeNotifier

### Community 100 - "ChatUser"
Cohesion: 0.20
Nodes (9): DateTime, Guid, ChatUser, Chat, ChatId, ChatName, JoinedAt, LeftAt (+1 more)

### Community 101 - "Report"
Cohesion: 0.20
Nodes (9): Guid, Report, Message, MessageId, Note, ReportedById, ReportId, ReportStatus (+1 more)

### Community 102 - "ChatHub"
Cohesion: 0.27
Nodes (6): Exception, Guid, ILogger, IMediator, Task, ChatHub

### Community 103 - "NotificationHub"
Cohesion: 0.31
Nodes (5): Exception, Guid, ILogger, Task, NotificationHub

### Community 104 - "ResultFilter"
Cohesion: 0.25
Nodes (7): ActionExecutingContext, ActionExecutionDelegate, IAsyncActionFilter, ErrorType, ILogger, Task, ResultFilter

### Community 105 - "ControllerBase"
Cohesion: 0.22
Nodes (8): ControllerBase, Authorize, CancellationToken, HttpPost, ISender, ProducesResponseType, Task, ReportController

### Community 106 - ".GetUserId"
Cohesion: 0.22
Nodes (5): Trivo.Application.Helpers, Guid, HttpContext, AuthenticatedUserHelper, UserHelper

### Community 107 - ".GetOrCreateAsync"
Cohesion: 0.25
Nodes (7): DistributedCacheEntryOptions, CancellationToken, Func, IDistributedCache, Task, DistributedCacheExtensions, DefaultExpiration

### Community 108 - "Trivo"
Cohesion: 0.22
Nodes (8): Building the Docker image, Health checks, Logging, Prerequisites, Project structure, Running locally, Running the full stack in "production" mode, Trivo

### Community 109 - ".CreateMatchAsync"
Cohesion: 0.36
Nodes (6): CancellationToken, HttpPost, HttpPut, ISender, Task, MatchController

### Community 110 - ".SendFileAsync"
Cohesion: 0.44
Nodes (6): Authorize, CancellationToken, HttpPost, ISender, Task, MessageController

### Community 111 - "UnbanUserCommandHandler"
Cohesion: 0.25
Nodes (7): Guid, UnbanUserCommand, CancellationToken, ILogger, Task, UnbanUserCommandHandler, UnbanUserCommandValidator

### Community 112 - "UpdateBiographyCommand"
Cohesion: 0.25
Nodes (7): Guid, UpdateBiographyCommand, CancellationToken, ILogger, Task, UpdateBiographyCommandHandler, UpdateBiographyValidator

### Community 113 - "Skill"
Cohesion: 0.22
Nodes (8): DateTime, Guid, ICollection, Skill, Name, RegisteredAt, SkillId, UserSkills

### Community 114 - "MatchNotifier"
Cohesion: 0.42
Nodes (5): Guid, IEnumerable, IHubContext, Task, MatchNotifier

### Community 115 - "GetActiveUsersCountQueryHandler"
Cohesion: 0.36
Nodes (6): ActiveUsersCountDto, GetActiveUsersCountQuery, CancellationToken, ILogger, Task, GetActiveUsersCountQueryHandler

### Community 116 - ".Handle"
Cohesion: 0.36
Nodes (6): CompletedMatchesCountDto, GetCompletedMatchesCountQuery, CancellationToken, ILogger, Task, GetCompletedMatchesCountQueryHandler

### Community 117 - "GetReportedUsersCountQueryHandler"
Cohesion: 0.36
Nodes (6): ReportedUsersCountDto, GetReportedUsersCountQuery, CancellationToken, ILogger, Task, GetReportedUsersCountQueryHandler

### Community 118 - ".Handle"
Cohesion: 0.29
Nodes (6): Guid, GetChatPaginationQuery, CancellationToken, ILogger, Task, GetChatPaginationQueryHandler

### Community 119 - ".GetExpertIdAsync"
Cohesion: 0.25
Nodes (6): CancellationToken, Guid, Task, CancellationToken, Guid, Task

### Community 120 - ".GetRecruiterIdAsync"
Cohesion: 0.25
Nodes (6): CancellationToken, Guid, Task, CancellationToken, Guid, Task

### Community 121 - "INotificationHub"
Cohesion: 0.39
Nodes (4): Guid, IEnumerable, Task, INotificationHub

### Community 122 - "EmailSetting"
Cohesion: 0.25
Nodes (7): EmailSetting, DisplayName, EmailFrom, SmtpHost, SmtpPassword, SmtpPort, SmtpUser

### Community 123 - "Trivo.Infrastructure.Persistence.csproj"
Cohesion: 0.29
Nodes (6): Microsoft.EntityFrameworkCore (8.0.10), Npgsql.EntityFrameworkCore.PostgreSQL (8.0.10), net8.0, Microsoft.EntityFrameworkCore.Design (8.0.10), Microsoft.Extensions.Caching.StackExchangeRedis (8.0.10), Microsoft.NET.Sdk

### Community 124 - "BanUserCommandHandler"
Cohesion: 0.33
Nodes (6): Guid, BanUserCommand, CancellationToken, ILogger, Task, BanUserCommandHandler

### Community 125 - "MessageStatus"
Cohesion: 0.29
Nodes (6): MessageStatus, Deleted, Delivered, Seen, Sent, Updated

### Community 127 - "Trivo.Application.DTOs.Reports"
Cohesion: 0.40
Nodes (3): Trivo.Application.Features.Reports.Commands.CreateReport, Trivo.Application.Features.Reports, Trivo.Application.DTOs.Reports

### Community 128 - ".ToEntity"
Cohesion: 0.33
Nodes (4): Trivo.Application.Features.Administrator.Commands.CreateAdministrator.Mappings, Administrator, CreateAdminCommand, AdminMappingExtensions

### Community 129 - "UserMappingExtensions.cs"
Cohesion: 0.33
Nodes (4): Trivo.Application.Features.Users.Commands.CreateUser.Mappings, CreateUserCommand, User, UserMappingExtensions

### Community 130 - "GetSkillsPaginationQueryHandler"
Cohesion: 0.33
Nodes (5): CancellationToken, IDistributedCache, ILogger, Task, GetSkillsPaginationQueryHandler

### Community 131 - "ErrorType"
Cohesion: 0.33
Nodes (5): ErrorType, Conflict, Failure, NotFound, Unauthorized

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

### Community 143 - "MissingByMatching"
Cohesion: 0.50
Nodes (3): MissingByMatching, Expert, Recruiter

### Community 144 - "ReportStatus"
Cohesion: 0.50
Nodes (3): ReportStatus, Pending, Resolved

## Knowledge Gaps
- **262 isolated node(s):** `net8.0`, `Asp.Versioning.Mvc (8.1.0)`, `AspNetCore.HealthChecks.Redis (9.0.0)`, `MediatR (12.2.0)`, `Microsoft.AspNetCore.OpenApi (8.0.10)` (+257 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **4 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `ResultT` connect `ResultT` to `IUserRepository`, `Message`, `InterestCategory`, `GetSkillsPaginationQueryHandler`, `TokenResponseDto`, `.Handle`, `ExpertDto`, `.NotFound`, `ChatDto`, `MatchDetailsDto`, `PagedResult`, `ICodeService`, `IQuery`, `.AddServices`, `.Failure`, `AdminController`, `.Handle`, `.CreateMatchNotificationAsync`, `ICommand`, `MessageDto`, `.SaveChangesAsync`, `.Handle`, `.GetByCategoriesAsync`, `ICommandHandler`, `.Handle`, `.Handle`, `.CreateSkillAsync`, `.Handle`, `.Handle`, `InterestWithIdDto`, `SkillWithIdDto`, `.Handle`, `UpdateInterestCommand`, `.Handle`, `.Handle`, `UserAiRecommendationDto`, `.Handle`, `.UpdateRecruiterAsync`, `.ValidateEmailAsync`, `GetInterestsPaginationQueryHandler`, `RecruiterDto`, `.Handle`, `.Handle`, `GetUserBiographyQueryHandler`, `GetUserProfilePictureQueryHandler`, `.Handle`, `ControllerBase`, `.CreateMatchAsync`, `.SendFileAsync`, `UnbanUserCommandHandler`, `UpdateBiographyCommand`, `GetActiveUsersCountQueryHandler`, `.Handle`, `GetReportedUsersCountQueryHandler`, `.Handle`, `BanUserCommandHandler`?**
  _High betweenness centrality (0.254) - this node is a cross-community bridge._
- **Why does `Trivo.Domain.Enums` connect `Trivo.Domain.Enums` to `UserMappingExtensions.cs`, `Trivo.Application.Abstractions.Messages`, `ErrorType`, `NotificationType`, `Trivo.Domain.Models`, `ExpertStatus`, `Level`, `MatchStatus`, `Trivo.Application.Interfaces.Services`, `Trivo.Application.Interfaces.SignalR`, `MessageType`, `RecruiterStatus`, `ChatType`, `MatchFault`, `MissingByMatching`, `ReportStatus`, `UserStatus`, `MatchDetailsDto`, `ICodeService`, `CreateMatchingCommandHandler`, `MessageStatus`?**
  _High betweenness centrality (0.065) - this node is a cross-community bridge._
- **Why does `Trivo.Domain.Models` connect `Trivo.Domain.Models` to `IUserRepository`, `Trivo.Application.Abstractions.Messages`, `Message`, `InterestCategory`, `Trivo.Application.DTOs.Users`, `Trivo.Infrastructure.Persistence.Configurations`, `Trivo.Application.Interfaces.Services`, `Trivo.Application.Interfaces.SignalR`, `ExpertDto`, `Expert`, `Notification`, `Match`, `Code`, `Trivo.Domain.Enums`, `CreateRecruiterCommand`, `Administrator`, `GetPaginatedInterestCategoriesQueryHandler.cs`, `ChatUser`, `Report`, `.GetUserId`, `Skill`, `Trivo.Application.DTOs.Reports`?**
  _High betweenness centrality (0.064) - this node is a cross-community bridge._
- **What connects `net8.0`, `Asp.Versioning.Mvc (8.1.0)`, `AspNetCore.HealthChecks.Redis (9.0.0)` to the rest of the system?**
  _262 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `IUserRepository` be split into smaller, more focused modules?**
  _Cohesion score 0.05283724091063541 - nodes in this community are weakly interconnected._
- **Should `Trivo.Application.Abstractions.Messages` be split into smaller, more focused modules?**
  _Cohesion score 0.0710868079289132 - nodes in this community are weakly interconnected._
- **Should `Message` be split into smaller, more focused modules?**
  _Cohesion score 0.05817028027498678 - nodes in this community are weakly interconnected._