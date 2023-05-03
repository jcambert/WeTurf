namespace We.Results;

public static class ErrorExtensions
{
    public enum AsStringFormat
    {
        Failure,
        Message
    }

    public static string AsString(
        this IReadOnlyList<Error> errors,
        string prefix = "",
        string separator = "\n",
        AsStringFormat fmt = AsStringFormat.Message
    ) =>
        errors
            .Select(
                error =>
                    prefix
                    + (
                        fmt == AsStringFormat.Failure
                            ? error.Failure ?? error.Message
                            : error.Message
                    )
            )
            .JoinAsString(separator);

    private static string JoinAsString(this IEnumerable<string> source, string separator) =>
        string.Join(separator, source);
}
