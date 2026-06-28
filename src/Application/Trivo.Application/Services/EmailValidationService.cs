using Microsoft.Extensions.Logging;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Utils;

namespace Trivo.Application.Services;

public class EmailValidationService(
    ILogger<EmailValidationService> logger,
    IUserRepository userRepository
    ) : IEmailValidationService
{
    public async Task<ResultT<bool>> ValidateEmailAsync(string email, CancellationToken cancellationToken)
    {
        var exists = await userRepository.EmailExistsAsync(email, cancellationToken);

        if (exists)
        {
            logger.LogInformation("The email '{Email}' is already registered.", email);
            return ResultT<bool>.Failure(Error.Conflict("409", $"This email {email} is already in use"));
        }

        logger.LogInformation("This email is available for registration.");

        return ResultT<bool>.Success(true);
    }
}
