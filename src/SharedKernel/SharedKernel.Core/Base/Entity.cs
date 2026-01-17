using SharedKernel.Core.Abstractions;

namespace SharedKernel.Core.Base;

public class Entity : IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        if (Id == Guid.Empty || other.Id == Guid.Empty)
            return false;

        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }
}