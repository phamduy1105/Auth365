namespace Identity.Application.Interfaces;

public interface ICurrentUser
{
    public string UserId { get; }
    public bool IsAuthenticated { get; }
}