using MediatR;
using Trivo.Application.Utils;

namespace Trivo.Application.Abstractions.Messages;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<ResultT<TResponse>>, IBaseCommand;

public interface IBaseCommand;