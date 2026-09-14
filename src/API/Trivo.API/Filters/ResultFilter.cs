using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Trivo.Application.Utils;

namespace Trivo.API.Filters;

/// <summary>
///
/// </summary>
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
                ? new ObjectResult(valueProperty.GetValue(objectResult.Value))
                {
                    StatusCode = result.SuccessStatusCode ?? StatusCodes.Status200OK
                }
                : new StatusCodeResult(result.SuccessStatusCode ?? StatusCodes.Status204NoContent);

            return;
        }

        var error = result.Error!;
        var statusCode = error.StatusCode ?? error.ErrorType.ToDefaultStatusCode();

        logger.LogWarning(
            "Operation failed with code {Code} and message: {Message}",
            error.Code,
            error.Description
        );

        executedContext.Result = new ObjectResult(error.ToProblemDetails(statusCode))
        {
            StatusCode = statusCode
        };
    }
}
