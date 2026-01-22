namespace SharedKernel.Application.Models.Result;

public sealed record PagingApplicationResult<TResponse> : ApplicationResult where TResponse : class
{
    public List<ValueApplicationResult<TResponse>> Results { get; set; } = [];
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}