using System.Text.Json;
using EventSourced.Net.Tests.TestDoubles;

namespace EventSourced.Net.Tests;

public class DomainEventTests
{
    private readonly DomainEventDummy _domainEvent;

    public DomainEventTests() =>
        _domainEvent = new DomainEventDummy { AggregateId = "id" };

    [Fact]
    public void EventTypeIsSet() =>
        _domainEvent.EventType.Should().Be(nameof(DomainEventDummy));

    [Fact]
    public void EventIdIsGivenANewGuid() =>
        _domainEvent.EventId.Should().NotBeEmpty();

    [Fact]
    public void CorrelationIdIsDefaultedToEventId() =>
        Assert.Equal(_domainEvent.EventId, _domainEvent.CorrelationId);

    [Fact]
    public void CausationIdIsDefaultedToEventId() =>
        Assert.Equal(_domainEvent.EventId, _domainEvent.CausationId);

    [Fact]
    public void EventTypeIsSetOnAChildEvent()
    {
        var childEvent = new DomainEventDummy(_domainEvent);

        childEvent.EventType.Should().Be(nameof(DomainEventDummy));
    }

    [Fact]
    public void CorrelationIdCanBeSetToParentsCorrelationId()
    {
        var childEvent = new DomainEventDummy(_domainEvent);

        childEvent.CorrelationId.Should().Be(_domainEvent.CorrelationId);
    }

    [Fact]
    public void CausationIdCanBeSetToParentsEventId()
    {
        var childEvent = new DomainEventDummy(_domainEvent);

        childEvent.CausationId.Should().Be(_domainEvent.EventId);
    }

    [Fact]
    public void ChildEventIsGivenANewEventId()
    {
        var childEvent = new DomainEventDummy(_domainEvent);

        childEvent.EventId.Should().NotBeEmpty();
    }

    [Fact]
    public void ParameterLessConstructorIsUsedIfParentIsNull()
    {
        var childEvent = new DomainEventDummy(null!);

        childEvent.EventId.Should().NotBeEmpty();
    }

    [Fact]
    public void ParentEventCanBeFromAnotherAggregate()
    {
        var parentFromDifferentAggregate = new AggTwoDomainEvent();
        var childEvent = new DomainEventDummy(parentFromDifferentAggregate);

        childEvent.CorrelationId.Should().Be(parentFromDifferentAggregate.CorrelationId);
        childEvent.CausationId.Should().Be(parentFromDifferentAggregate.EventId);
    }

    [Fact]
    public void AggregateIdIsDefaultedToAnEmptyString()
    {
        var domainEvent = new DomainEventDummy();

        domainEvent.AggregateId.Should().Be(string.Empty);
    }

    [Theory]
    [InlineData("2")]
    [InlineData("id")]
    [InlineData("945-54da9-f54")]
    public void AggregateIdCanBeSet(string id)
    {
        var domainEvent = new DomainEventDummy { AggregateId = id };

        Assert.Equal(id, domainEvent.AggregateId);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(999)]
    [InlineData(9876543210)]
    public void SequenceCanBeSet(long sequence)
    {
        var domainEvent = new DomainEventDummy { AggregateId = "id", Sequence = sequence };

        Assert.Equal(sequence, domainEvent.Sequence);
    }

    [Fact]
    public void TimeStampIsDefaultedToNow() =>
        _domainEvent.TimeStamp.Should()
            .BeOnOrBefore(DateTimeOffset.UtcNow)
            .And.BeAfter(DateTimeOffset.UtcNow.AddSeconds(-1));

    [Theory]
    [InlineData("2022-06-16")]
    [InlineData("2022-06-16 12:46:15.145638")]
    public void TimeStampCanBeSet(DateTimeOffset timestamp)
    {
        var domainEvent = new DomainEventDummy { TimeStamp = timestamp };

        domainEvent.TimeStamp.Should().BeExactly(timestamp);
    }

    [Fact]
    public void CreatedByIsDefaultedToAnEmptyString() =>
        _domainEvent.CreatedBy.Should().Be(string.Empty);

    [Theory]
    [InlineData("CreatedBy")]
    [InlineData("Me")]
    public void CreatedByCanBeSet(string name)
    {
        var domainEvent = new DomainEventDummy { CreatedBy = name };

        domainEvent.CreatedBy.Should().Be(name);
    }

    [Fact]
    public void CanBeSerializedAndDeserialized()
    {
        var eventString = JsonSerializer.Serialize(_domainEvent);
        var deserializedEvent =
            JsonSerializer.Deserialize<DomainEventDummy>(eventString);

        deserializedEvent.Should().BeEquivalentTo(_domainEvent);
    }
}