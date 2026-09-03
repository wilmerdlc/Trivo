using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Http;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Helpers;
using Trivo.Application.Utils;

namespace Trivo.Application.Behaviors;

public sealed class AuthorizationBehavior<TRequest, TResponse>(IHttpContextAccessor httpContextAccessor)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is IUserOwnedRequest ownedRequest)
        {
            Guid? callerId;
            try
            {
                callerId = httpContextAccessor.HttpContext?.GetUserId();
            }
            catch (UnauthorizedAccessException)
            {
                callerId = null;
            }

            if (callerId != ownedRequest.UserId)
            {
                var error = Error.Unauthorized("403", "You can only access or modify your own resources.");

                // TResponse is always a closed Result/ResultT<T> here (every ICommand/IQuery in this
                // codebase resolves to one) — both expose a public static Failure(Error) factory.
                var failureMethod = typeof(TResponse).GetMethod(
                    nameof(Result.Failure), BindingFlags.Public | BindingFlags.Static, null, [typeof(Error)], null)!;

                return (TResponse)failureMethod.Invoke(null, [error])!;
            }
        }

        return await next().ConfigureAwait(false);
    }
}
