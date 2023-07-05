using System.Linq.Expressions;
using EventSourced.Net.Persistence;

namespace EventSourced.Net.Domain;

public abstract class Specification<T>
    where T : IProjection
{
    public bool IsSatisfiedBy(T entity)
    {
        var predicate = ToExpression().Compile();
        return predicate(entity);
    }

    public abstract Expression<Func<T, bool>> ToExpression();
}