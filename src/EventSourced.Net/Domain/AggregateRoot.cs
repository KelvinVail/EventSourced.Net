namespace EventSourced.Net.Domain;

public abstract class AggregateRoot<T> : Entity<string>
    where T : AggregateRoot<T>
{
    internal List<DomainEvent<T>> Events { get; } = new();
}