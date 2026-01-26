using SharedKernel.Core.Abstractions;

namespace SharedKernel.Core.Base;

public abstract class AggregateRoot :
    Entity,
    IAggregateRoot,
    ISoftDelete,
    IAuditable,
    IMultiTenant
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }
    
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTimeOffset.UtcNow;
    }
    
    public void UndoDelete()
    {
        IsDeleted = false;
        DeletedAt = null;
    }

    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public Guid? CreatedBy { get; private set; }
    public DateTimeOffset? LastModifiedAt { get; private set; }
    public Guid? LastModifiedBy { get; private set; }
    
    public Guid TenantId { get; protected init; }
}