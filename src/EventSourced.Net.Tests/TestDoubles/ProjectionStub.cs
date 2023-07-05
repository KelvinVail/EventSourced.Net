using EventSourced.Net.Persistence;

namespace EventSourced.Net.Tests.TestDoubles;

public class ProjectionStub : IProjection
{
    public string Id { get; init; } = string.Empty;

    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;
}