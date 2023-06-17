namespace EventSourced.Net;

public sealed class ErrorResult : ValueObject, ICombine
{
    private ErrorResult(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; private set; }

    public string Message { get; private set; }

    public static ErrorResult Empty(string? paramName = null) =>
        new (
            "value.must.not.be.empty",
            $"'{Humanize(paramName)}' must not be empty.");

    public static ErrorResult NotFound(object? value = null) =>
        new (
            "value.not.found",
            $"'{value?.ToString() ?? "Value"}' not found.");

    public static ErrorResult Invalid(string? paramName = null, string? message = null) =>
        new (
            "value.must.be.valid",
            $"'{Humanize(paramName)}' {message ?? "must be valid."}");

    public static ErrorResult Unauthorized() =>
        new ("unauthorized", "Unauthorized.");

    public static ErrorResult Forbidden(string? message = null) =>
        new ("forbidden", message ?? "Forbidden.");

    public static ErrorResult AlreadyExists(string? paramName = null, string? message = null) =>
        new ("already.exists", $"'{Humanize(paramName)}' {message ?? "already exists."}");

    public ICombine Combine(ICombine value)
    {
        if (value is not ErrorResult errorIn) return this;

        return new ErrorResult($"{Code}|{errorIn!.Code}", $"{Message}|{errorIn.Message}");
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Code;
    }

    private static string Humanize(string? paramName = null) =>
        paramName?.Humanize().Transform(To.TitleCase) ?? "Value";
}