namespace We.Results;

public class Error
{
    public Error(string failure):this(failure,string.Empty) { }
    public Error(string failure, string message) {
        if (string.IsNullOrEmpty(failure))
            throw new ArgumentNullException($"{nameof(failure)} cannot be empty");
        Failure = failure;
        Message = message;
    }

    public string Failure { get; init; }
    public string Message { get; init; }
}

