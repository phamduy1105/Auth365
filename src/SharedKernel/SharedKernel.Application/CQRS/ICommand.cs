using MediatR;
using SharedKernel.Application.Models;

namespace SharedKernel.Application.CQRS;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;

//public interface ICommand : IRequest;