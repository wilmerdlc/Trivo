using MediatR;
using Trivo.Application.Utils;

namespace Trivo.Application.Abstractions.Messages;

public interface IQueryHandler<in TQuery, TResponse> :
    IRequestHandler<TQuery, ResultT<TResponse>>
    where TQuery : IQuery<TResponse>;