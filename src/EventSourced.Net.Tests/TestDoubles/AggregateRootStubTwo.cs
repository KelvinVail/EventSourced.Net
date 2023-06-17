namespace EventSourced.Net.Tests.TestDoubles;

public class AggregateRootStubTwo : IAggregateRoot<AggregateRootStubTwo>
{
    private readonly List<DomainEvent<AggregateRootStubTwo>> _events = new ();

    public string AggregateId { get; } = "id";

    public IReadOnlyList<DomainEvent<AggregateRootStubTwo>> Events => _events;

    public void ClearEvents() => _events.Clear();
}