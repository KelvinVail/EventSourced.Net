using CSharpFunctionalExtensions;

namespace EventSourced.Net.Tests.TestDoubles;

public class DomainEventDummy : DomainEvent<AggregateRootStub>
{
    public DomainEventDummy()
    {
    }

    public DomainEventDummy(IDomainEvent parent)
        : base(parent)
    {
    }

    public override AggregateRootStub Play(Maybe<AggregateRootStub> root) => throw new NotImplementedException();
}