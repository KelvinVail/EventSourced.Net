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
        var projection = new ProjectionStub { Id = string.Empty };

        IsSatisfiedBy(projection).Should().BeTrue();
    }

    [Fact]
    public void FalseIfExpressionIsFalse()
    {
        var projection = new ProjectionStub { Id = "Id" };

        IsSatisfiedBy(projection).Should().BeFalse();
    }
}