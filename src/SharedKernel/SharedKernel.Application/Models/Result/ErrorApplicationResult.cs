namespace SharedKernel.Application.Models.Result;

public sealed record ErrorApplicationResult(string ErrorMessage) : ApplicationResult;