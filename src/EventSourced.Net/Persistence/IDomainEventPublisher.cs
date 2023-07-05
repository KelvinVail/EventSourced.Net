using EventSourced.Net.Domain;

namespace EventSourced.Net.Persistence;

public interface IDomainEventPublisher<T>
    where T : AggregateRoot<T>
{
    Task Publish(DomainEvent<T> domainEvent, CancellationToken cancellationToken = default);
}