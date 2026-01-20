using MediatR;
using SharedKernel.Application.Models;

namespace SharedKernel.Application.CQRS;

public interface ICommandHandler<in TCommand, TResponse> :
    IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;
