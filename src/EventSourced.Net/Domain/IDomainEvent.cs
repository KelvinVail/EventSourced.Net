namespace EventSourced.Net.Domain;

public interface IDomainEvent
{
    public Guid EventId { get; init; }

    public Guid CorrelationId { get; init; }
}