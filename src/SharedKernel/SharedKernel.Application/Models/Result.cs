namespace SharedKernel.Application.Models;

public class Result<T> : IResult<T>
{
    private Result(bool isSuccess,
        T value,
        string? code,
        string? message)
    {
        IsSuccess = isSuccess;
        Value = value;
        Code = code;
        Message = message;
    }
    
    public bool IsSuccess { get; init; }
    public T Value { get; init; }
    public string? Code { get; init; }
    public string? Message { get; init; }
    
    public static Result<T> Success(T value) => new(true, value, null, null);
    public static Result<T> Failure(string code, string message) => new(false, default!, code, message);
}