using MediatR;
using SharedKernel.Application.Models;

namespace SharedKernel.Application.CQRS;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
