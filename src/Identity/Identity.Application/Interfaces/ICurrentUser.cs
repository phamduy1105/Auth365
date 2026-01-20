namespace Identity.Application.Interfaces;

public interface ICurrentUser
{
    public string UserId { get; }
    public List<string> Roles { get; }
}