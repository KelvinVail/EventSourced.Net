namespace EventSourced.Net;

public interface IDomainEventPublisher<T>
    where T : class, IAggregateRoot<T>
{
    Task Publish(DomainEvent<T> domainEvent, CancellationToken cancellationToken = default);
}