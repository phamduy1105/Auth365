using MediatR;
using SharedKernel.Application.Models.Result;

namespace SharedKernel.Application.CQRS;

public interface IQueryHandler<in TQuery, TResponse> :
    IRequestHandler<TQuery, ApplicationResult>
    where TQuery : IQuery<TResponse>;
