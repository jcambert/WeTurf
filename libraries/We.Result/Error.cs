namespace We.Result;

public class Error
{
    public Error(string failure, string message) => (Failure, Message) = (failure, message);

    public string Failure { get; init; }
    public string Message { get; init; }
}