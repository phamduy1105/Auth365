using Identity.Domain.Abstractions;
using Identity.Domain.Aggregates;

namespace Identity.Infrastructure.Implements;

public sealed class UserRepository : IUserRepository
{
    private readonly List<User> _users;
    public UserRepository()
    {
        _ = Guid.TryParse("11111111-1111-1111-1111-111111111111", out var tenantId);
        var user1 = User.Create(tenantId,
            "user1@gmail.com",
            "123123");
        user1.AddRole("product.view");
        user1.AddRole("product.create");
        user1.AddRole("product.update");
        user1.AddRole("product.delete");
        user1.AddRole("openid");
        user1.AddRole("email");
    
        _users = [user1];
    }
    public async Task<User?> GetUserByLoginAsync(string userId, string passwordHash)
    {
        return await Task.FromResult(_users.FirstOrDefault(u => u.Email == userId && u.PasswordHash == passwordHash));
    }

    public async Task<User?> GetUserInfoAsync(string userId)
    {
        return await Task.FromResult(_users.FirstOrDefault(u => u.Email == userId));
    }
}