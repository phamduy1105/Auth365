namespace SharedKernel.Core.Abstractions;

public interface ISoftDelete
{
    bool IsDeleted { get; }
    DateTimeOffset? DeletedAt { get; }

    void UndoDelete();
}