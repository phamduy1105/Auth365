using SharedKernel.Core.Abstractions;
using SharedKernel.Core.Base;

namespace Identity.Domain.Aggregates;

public sealed class User : AggregateRoot
{
    private User(Guid tenantId,
        string email,
        string passwordHash)
    {
        TenantId = tenantId;
        Email = email;
        PasswordHash = passwordHash;
    }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Subject => $"Auth|{Id}";
    
    private readonly List<string> _roles = [];
    public IReadOnlyCollection<string> Roles => _roles.AsReadOnly();

    public void AddRole(string role)
    {
        if (!_roles.Contains(role))
            _roles.Add(role);
    }

    public static User Create(Guid tenantId, 
        string email, 
        string passwordHash)
    {
        var user = new User(tenantId,
            email,
            passwordHash);
        
        return user;
    }
}