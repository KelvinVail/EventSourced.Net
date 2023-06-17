namespace EventSourced.Net;

public abstract class DomainEvent<T> : IDomainEvent
    where T : class, IAggregateRoot<T>
{
    protected DomainEvent()
    {
        EventId = Guid.NewGuid();
        CorrelationId = EventId;
        CausationId = EventId;
    }

    protected DomainEvent(IDomainEvent parent)
        : this()
    {
        if (parent is null) return;
        CorrelationId = parent.CorrelationId;
        CausationId = parent.EventId;
    }

    public Guid EventId { get; init; }

    public Guid CorrelationId { get; init; }

    public Guid CausationId { get; init; }

    public string EventType => GetType().Name;

    public string AggregateId { get; init; } = string.Empty;

    public long Sequence { get; init; }

    public DateTimeOffset TimeStamp { get; init; } = DateTimeOffset.UtcNow;

    public string CreatedBy { get; set; } = string.Empty;

    public abstract T Play(Maybe<T> root);
}