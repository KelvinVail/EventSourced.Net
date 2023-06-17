using CSharpFunctionalExtensions;

namespace EventSourced.Net.Tests.TestDoubles;

public class AggTwoDomainEvent : DomainEvent<AggregateRootStubTwo>
{
    public AggTwoDomainEvent()
    {
    }

    public AggTwoDomainEvent(IDomainEvent parent)
        : base(parent)
    {
    }

    public override AggregateRootStubTwo Play(Maybe<AggregateRootStubTwo> root) => throw new NotImplementedException();
}