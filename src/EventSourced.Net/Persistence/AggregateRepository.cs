using EventSourced.Net.Domain;

namespace EventSourced.Net.Persistence;

public abstract class AggregateRepository<T>
    where T : AggregateRoot<T>
{
    private readonly IDomainEventPublisher<T> _publisher;

    protected AggregateRepository(IDomainEventPublisher<T> publisher) =>
        _publisher = publisher;

    public async Task Add(T root, CancellationToken cancellationToken)
    {
        if (root is null) return;

        foreach (var domainEvent in root.Events)
        {
            await AddDomainEvent(domainEvent, cancellationToken);
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        root.Events.Clear();
    }

    public async Task<Maybe<T>> GetById(string id, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(id)) return Maybe<T>.None;

        var domainEvents = await GetDomainEvents(id, cancellationToken);

        var root = Maybe<T>.None;
        foreach (var domainEvent in domainEvents.OrderBy(x => x.Sequence))
            root = domainEvent.Apply(root);

        if (root.HasValue)
            root.Value.Events.Clear();

        return root;
    }

    protected abstract Task AddDomainEvent(DomainEvent<T> domainEvent, CancellationToken cancellationToken);

    protected abstract Task<List<DomainEvent<T>>> GetDomainEvents(string aggregateId, CancellationToken cancellationToken);
}