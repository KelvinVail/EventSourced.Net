namespace EventSourced.Net;

public interface IAggregateRoot<T>
    where T : class, IAggregateRoot<T>
{
    string AggregateId { get; }

    IReadOnlyList<DomainEvent<T>> Events { get; }

    void ClearEvents();
}