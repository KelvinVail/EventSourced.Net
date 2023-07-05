using CSharpFunctionalExtensions;
using EventSourced.Net.Domain;

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

    public override AggregateRootStubTwo Apply(Maybe<AggregateRootStubTwo> root) => throw new NotImplementedException();
}