namespace EventSourced.Net.Tests.TestDoubles;

public class AggregateRootStub : IAggregateRoot<AggregateRootStub>
{
    private readonly List<DomainEvent<AggregateRootStub>> _events = new ();

    public string AggregateId { get; } = "id";

    public IReadOnlyList<DomainEvent<AggregateRootStub>> Events => _events;

    public void ClearEvents() => _events.Clear();
}