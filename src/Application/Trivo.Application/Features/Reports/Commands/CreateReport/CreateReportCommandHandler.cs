using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Reports;

namespace Trivo.Application.Features.Reports.Commands.CreateReport;

internal sealed class CreateReportCommandHandler(
    IReportRepository reportRepository,
    IMessageRepository messageRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILogger<CreateReportCommandHandler> logger
) : ICommandHandler<CreateReportCommand, ReportDto>
{
    public async Task<ResultT<ReportDto>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var reportingUser = await userRepository.GetByIdAsync(request.ReportedById, cancellationToken);
        if (reportingUser is null)
        {
            logger.LogError("The user with id {UserId} does not exist.", request.ReportedById);

            return ResultT<ReportDto>.Failure(Error.NotFound("404", "The reporting user was not found."));
        }

        var messageWithUsers = await messageRepository.GetWithUsersByIdAsync(request.MessageId, cancellationToken);
        if (messageWithUsers is null)
        {
            logger.LogError("The message with id {MessageId} does not exist.", request.MessageId);

            return ResultT<ReportDto>.Failure(Error.NotFound("404", "The message to report was not found."));
        }

        var report = new Report
        {
            ReportId = Guid.NewGuid(),
            ReportedById = request.ReportedById,
            MessageId = request.MessageId,
            ReportStatus = Domain.Enums.ReportStatus.Pending.ToString(),
            Note = request.Note
        };

        await reportRepository.CreateAsync(report, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Report '{ReportId}' created successfully by user '{UserId}'.", report.ReportId, request.ReportedById);

        var reportedUser = messageWithUsers.SenderId == reportingUser.Id
            ? messageWithUsers.Receiver
            : messageWithUsers.Sender;

        return ResultT<ReportDto>.Success(report.ToReportDto(messageWithUsers, reportingUser, reportedUser));
    }
}
