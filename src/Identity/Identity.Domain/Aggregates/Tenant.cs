using SharedKernel.Core.Abstractions;
using SharedKernel.Core.Base;

namespace Identity.Domain.Aggregates;

public class Tenant(string tenantName, string description) : Entity, IAggregateRoot, ISoftDelete
{
    public string TenantName { get; private set; } = tenantName;
    public string Description { get; private set; } = description;
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public void UndoDelete()
    {
        IsDeleted = false;
        DeletedAt = null;
    }

    public static Tenant Create(string tenantName, string description)
    {
        return new Tenant(tenantName, description);
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTimeOffset.UtcNow;
    }
}