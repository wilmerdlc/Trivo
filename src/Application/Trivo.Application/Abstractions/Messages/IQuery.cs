using MediatR;
using Trivo.Application.Utils;

namespace Trivo.Application.Abstractions.Messages;

public interface IQuery<TResponse> : IRequest<ResultT<TResponse>>;