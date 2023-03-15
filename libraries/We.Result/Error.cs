using Dawn;

namespace We.Results;

/// <summary>
/// Represent an error tha t can be embeded in Failure Result
/// </summary>
public class Error
{
    internal static Error Create(Exception ex)=>new Error(ex);
    internal static Error[] Create(params Exception[] ex)=>ex.Select(ex=>new Error(ex)).ToArray();   
    public Error(Exception ex)
    {
        Guard.Argument(ex).NotNull();
        Failure = ex.Source;
        Message = ex.Message;
        Exception= ex;  
    }
    public Error(string failure):this(failure,string.Empty) { }
    public Error(string failure, string message) {
        Guard.Argument(failure).NotNull().NotEmpty();
        
        Failure = failure;
        Message = message;
        Exception=default(Exception);
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
}

