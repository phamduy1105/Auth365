using SharedKernel.Core.Abstractions;

namespace SharedKernel.Core.Base;

public abstract class AggregateRoot :
    Entity,
    IAggregateRoot,
    ISoftDelete,
    IAuditable
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
    
    public bool IsDeleted { get; private set; } = true;
    public DateTimeOffset? DeletedAt { get; private set; } = DateTimeOffset.UtcNow;
    public void UndoDelete()
    {
        IsDeleted = false;
        DeletedAt = null;
    }

    public DateTimeOffset CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; }
    public Guid? LastModifiedBy { get; set; }
}