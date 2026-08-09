using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;

namespace Trivo.API.Filters;

public class ResultFilter(ILogger<ResultFilter> logger) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var executedContext = await next();

        if (executedContext.Result is not ObjectResult { Value: Result result } objectResult)
        {
            return;
        }

        if (result.IsSuccess)
        {
            var valueProperty = objectResult.Value!.GetType().GetProperty(nameof(ResultT<object>.Value));

            executedContext.Result = valueProperty is not null
                ? new OkObjectResult(valueProperty.GetValue(objectResult.Value))
                : new OkResult();

            return;
        }

        var statusCode = MapToStatusCode(result.Error!.ErrorType);

        logger.LogWarning(
            "Operation failed with code {Code} and message: {Message}",
            result.Error.Code,
            result.Error.Description
        );

        var errorResponse = new
        {
            code = result.Error.Code,
            description = result.Error.Description
        };

        executedContext.Result = new ObjectResult(errorResponse)
        {
            StatusCode = statusCode
        };
    }

    private static int MapToStatusCode(ErrorType errorType) => errorType switch
    {
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
        _ => StatusCodes.Status400BadRequest
    };
}
