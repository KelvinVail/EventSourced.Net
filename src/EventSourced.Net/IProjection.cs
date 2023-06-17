namespace EventSourced.Net;

public interface IProjection
{
    public string Id { get; }

    public DateTimeOffset Timestamp { get; }
}