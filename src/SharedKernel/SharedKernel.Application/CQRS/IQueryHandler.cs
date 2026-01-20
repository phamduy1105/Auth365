using MediatR;
using SharedKernel.Application.Models;

namespace SharedKernel.Application.CQRS;

public interface IQueryHandler<in TQuery, TResponse> :
    IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
