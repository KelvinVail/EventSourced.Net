using EventSourced.Net.Domain;

namespace EventSourced.Net.Persistence;

public interface IProjectionRepository<T>
    where T : IProjection
{
    Task<UnitResult<ErrorResult>> AddOrReplace(T row, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<T>> Query(Specification<T> spec, CancellationToken cancellationToken = default);

    Task Delete(string id, CancellationToken cancellationToken = default);
}