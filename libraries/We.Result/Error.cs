using Dawn;

namespace We.Results;

/// <summary>
/// Represent an error tha t can be embeded in Failure Result
/// </summary>
public static class HttpStatusCode
{
    public const int DEFAULT = -1;
    public const int BAD_REQUEST = 400;
    public const int UNAUTHORIZED = 401;
    public const int FORBIDDEN = 403;
    public const int NOT_FOUND = 404;
    public const int INTERNAL_SERVER_ERROR = 500;

}
public class Error
{
    internal static Error Create(Exception ex) => new Error(ex);
    internal static Error[] Create(params Exception[] ex) => ex.Select(ex => new Error(ex)).ToArray();
    public Error(Exception ex)
    {
        Guard.Argument(ex).NotNull();
        Failure = ex.Source;
        Message = ex.Message;
        Exception = ex;
        Code = HttpStatusCode.DEFAULT;
    }
    public Error(string failure, string message) : this(HttpStatusCode.DEFAULT, failure, message) { }
    public Error(string failure) : this(HttpStatusCode.DEFAULT, failure, string.Empty) { }
    public Error(int code, string failure) : this(code, failure, string.Empty) { }
    public Error(int code, string failure, string message)
    {
        Guard.Argument(failure).NotNull().NotEmpty();
        Code = code;
        Failure = failure;
        Message = message;
        Exception = default(Exception);
    }

    /// <summary>
    /// Failure String
    /// </summary>
    public string Failure { get; init; }

    /// <summary>
    /// Message string
    /// </summary>
    public string Message { get; init; }

    /// <summary>
    /// Inner Exception
    /// </summary>
    public Exception Exception { get; init; }

    /// <summary>
    /// Code
    /// </summary>
    public int Code { get; init; }
}

