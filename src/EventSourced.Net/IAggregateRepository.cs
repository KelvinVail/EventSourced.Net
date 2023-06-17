namespace EventSourced.Net;

public interface IAggregateRepository<T>
    where T : class, IAggregateRoot<T>
{
    public Task Add(T root, CancellationToken cancellationToken);

    public Task<Maybe<T>> GetById(string id, CancellationToken cancellationToken);

    public Task Delete(string id, CancellationToken cancellationToken);
}