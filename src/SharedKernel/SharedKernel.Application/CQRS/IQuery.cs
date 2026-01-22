using MediatR;
using SharedKernel.Application.Models.Result;

namespace SharedKernel.Application.CQRS;

public interface IQuery<TResponse> : IRequest<ApplicationResult>;
