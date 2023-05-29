using Dawn;
using System.Text.Json.Serialization;

namespace We.Results;

/// <summary>
/// Represent an error tha t can be embeded in Failure Result
/// </summary>
public static class HttpStatusCode
{
    public const string DEFAULT = "ERR000";
    public const string BAD_REQUEST = "ERR400";
    public const string UNAUTHORIZED = "ERR401";
    public const string FORBIDDEN = "ERR403";
    public const string NOT_FOUND = "ERR404";
    public const string INTERNAL_SERVER_ERROR = "ERR500";
}

public class Error
{
    internal static Error Create(Exception ex) => new Error(ex);

    internal static Error[] Create(params Exception[] ex) => ex.Select(x => new Error(x)).ToArray();

    public Error(string failure, Exception ex)
    {
        Guard.Argument(ex).NotNull();
        Failure = failure;
        Message = ex.Message;
        Exception = ex;
        Code = HttpStatusCode.DEFAULT;
    }

    public Error(string code, string failure, string message)
    {
        Guard.Argument(failure).NotNull().NotEmpty();
        Code = code;
        Failure = failure;
        Message = message;
        Exception = default(Exception);
    }

    public Error(Exception ex) : this(ex.Source, ex) { }

    public Error(string failure, string message) : this(HttpStatusCode.DEFAULT, failure, message)
    { }

    public Error(string failure) : this(HttpStatusCode.DEFAULT, failure, string.Empty) { }

    //public Error(string code, string failure) : this(code, failure, string.Empty) { }


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
    [JsonIgnore]
    public Exception Exception { get; init; }

    /// <summary>
    /// Code
    /// </summary>
    public string Code { get; init; }
}
