namespace SharedKernel.Application.Models.Result;

public sealed record ValueApplicationResult<TResponse>(TResponse Value) :
    ApplicationResult;