using MediatR;
using SharedKernel.Application.Models.Result;

namespace SharedKernel.Application.CQRS;

public interface ICommand<TResponse> : IRequest<ApplicationResult>;

//public interface ICommand : IRequest;