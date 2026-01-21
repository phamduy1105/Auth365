using Identity.Domain.Aggregates;

namespace Identity.Domain.Abstractions;

public interface IUserRepository
{
    public Task<User?> GetUserByLoginAsync(string userId, string passwordHash);
    public Task<User?> GetUserInfoAsync(string userId);
}