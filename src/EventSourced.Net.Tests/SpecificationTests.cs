using System.Linq.Expressions;
using EventSourced.Net.Tests.TestDoubles;

namespace EventSourced.Net.Tests;

public class SpecificationTests : Specification<ProjectionStub>
{
    public override Expression<Func<ProjectionStub, bool>> ToExpression() =>
        x => x.Id == string.Empty;

    [Fact]
    public void TrueIfExpressionIsTrue()
    {
        var projection = new ProjectionStub();

        IsSatisfiedBy(projection).Should().BeTrue();
    }
}