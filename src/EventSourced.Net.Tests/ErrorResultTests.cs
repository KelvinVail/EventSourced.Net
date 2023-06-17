using CSharpFunctionalExtensions;

namespace EventSourced.Net.Tests;

public sealed class ErrorResultTests
{
    [Fact]
    public void EmptyErrorReturnsDefaultValues()
    {
        var error = ErrorResult.Empty();

        error.Code.Should().Be("value.must.not.be.empty");
        error.Message.Should().Be("'Value' must not be empty.");
    }

    [Theory]
    [InlineData("parameterName", "Parameter Name")]
    [InlineData("input", "Input")]
    [InlineData("variable", "Variable")]
    public void EmptyErrorDisplaysParameterNameInTitleCase(string parameterName, string expected)
    {
        var error = ErrorResult.Empty(parameterName);

        error.Message.Should().Be($"'{expected}' must not be empty.");
    }

    [Fact]
    public void TwoErrorsWithTheSameErrorCodeAreEqual()
    {
        var error1 = ErrorResult.Empty();
        var error2 = ErrorResult.Empty("ParameterName");

        error1.Should().Be(error2);
        error1.Should().BeRankedEquallyTo(error2);
    }

    [Fact]
    public void TwoErrorsWithDifferentErrorCodesAreNotEqual()
    {
        var error1 = ErrorResult.Empty();
        var error2 = ErrorResult.Invalid();

        error1.Should().NotBe(error2);
        error1.Should().NotBeRankedEquallyTo(error2);
    }

    [Fact]
    public void TwoErrorsCanBeCombined()
    {
        var error1 = ErrorResult.Empty();
        var error2 = ErrorResult.Invalid();

        var combined = error1.Combine(error2) as ErrorResult;

        combined!.Code.Should().Be("value.must.not.be.empty|value.must.be.valid");
        combined!.Message.Should().Be("'Value' must not be empty.|'Value' must be valid.");
    }

    [Fact]
    public void ReturnOriginalErrorIfCombinedWithANullError()
    {
        var error1 = ErrorResult.Empty();

        var combined = error1.Combine(null!) as ErrorResult;

        combined!.Code.Should().Be("value.must.not.be.empty");
        combined!.Message.Should().Be("'Value' must not be empty.");
    }

    [Fact]
    public void ReturnOriginalErrorIfCombinedWithAnInvalidObject()
    {
        var error1 = ErrorResult.Empty();
        var invalidObject = new InvalidICombine();

        var combined = error1.Combine(invalidObject) as ErrorResult;

        combined!.Code.Should().Be("value.must.not.be.empty");
        combined!.Message.Should().Be("'Value' must not be empty.");
    }

    [Fact]
    public void NotFoundErrorReturnsDefaultValues()
    {
        var error = ErrorResult.NotFound();

        error.Code.Should().Be("value.not.found");
        Assert.Equal("'Value' not found.", error.Message);
    }

    [Theory]
    [InlineData("parameterValue")]
    [InlineData("input")]
    [InlineData("variable")]
    public void NotFoundErrorReturnsParameterValue(string parameterValue)
    {
        var error = ErrorResult.NotFound(parameterValue);

        error.Message.Should().Be($"'{parameterValue}' not found.");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(999)]
    [InlineData(329)]
    public void NotFoundErrorDisplaysParameterValueIfInt(int parameterValue)
    {
        var error = ErrorResult.NotFound(parameterValue);

        error.Message.Should().Be($"'{parameterValue}' not found.", error.Message);
    }

    [Fact]
    public void InvalidErrorReturnsDefaultValues()
    {
        var error = ErrorResult.Invalid();

        error.Code.Should().Be("value.must.be.valid");
        error.Message.Should().Be("'Value' must be valid.");
    }

    [Theory]
    [InlineData("parameterName", "Parameter Name")]
    [InlineData("input", "Input")]
    [InlineData("variable", "Variable")]
    public void InvalidErrorDisplaysParameterNameInTitleCase(string parameterName, string expected)
    {
        var error = ErrorResult.Invalid(parameterName);

        error.Message.Should().Be($"'{expected}' must be valid.");
    }

    [Theory]
    [InlineData("message.")]
    [InlineData("try again.")]
    public void InvalidErrorDisplayCustomMessage(string message)
    {
        var error = ErrorResult.Invalid("Name", message);

        error.Message.Should().Be($"'Name' {message}");
    }

    [Fact]
    public void UnauthorizedReturnsDefaultValues()
    {
        var error = ErrorResult.Unauthorized();

        error.Code.Should().Be("unauthorized");
        error.Message.Should().Be("Unauthorized.");
    }

    [Fact]
    public void ForbiddenReturnsDefaultValues()
    {
        var error = ErrorResult.Forbidden();

        error.Code.Should().Be("forbidden");
        error.Message.Should().Be("Forbidden.");
    }

    [Theory]
    [InlineData("Message.")]
    [InlineData("Forbidden.")]
    public void ForbiddenDisplaysCustomMessage(string message)
    {
        var error = ErrorResult.Forbidden(message);

        error.Code.Should().Be("forbidden");
        error.Message.Should().Be(message);
    }

    [Fact]
    public void AlreadyExistsErrorReturnsDefaultValues()
    {
        var error = ErrorResult.AlreadyExists();

        error.Code.Should().Be("already.exists");
        error.Message.Should().Be("'Value' already exists.");
    }

    [Theory]
    [InlineData("parameterName", "Parameter Name")]
    [InlineData("input", "Input")]
    [InlineData("variable", "Variable")]
    public void AlreadyExistsDisplaysParameterNameInTitleCase(string paramName, string expected)
    {
        var error = ErrorResult.AlreadyExists(paramName);

        error.Code.Should().Be("already.exists");
        error.Message.Should().Be($"'{expected}' already exists.");
    }

    [Theory]
    [InlineData("resource already exists.")]
    [InlineData("resource already exists. Try a different value.")]
    public void AlreadyExistsDisplaysACustomMessage(string message)
    {
        var error = ErrorResult.AlreadyExists(message: message);

        error.Code.Should().Be("already.exists");
        error.Message.Should().Be($"'Value' {message}");
    }

    private class InvalidICombine : ICombine
    {
        public ICombine Combine(ICombine value) => throw new NotImplementedException();
    }
}