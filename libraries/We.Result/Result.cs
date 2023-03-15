namespace We.Results;

/// <summary>
/// Base Class of result 
/// It represent an abstract Result concept
/// </summary>
public abstract class Result : IResult
{
    private readonly List<Error> _errors;
    private readonly List<Error> _empty = new();
    protected Result(bool success, params string[] errors)
    {
        IsSuccess = success;
        _errors = errors.Where(x => !string.IsNullOrEmpty(x)).Distinct().Select(x => new Error(x)).ToList();
    }
    protected Result(bool success, params Error[] errors)
    {
        IsSuccess = success;
        _errors = errors.DistinctBy(x => x.Failure).ToList();
    }
    /// <summary>
    /// Is a success result
    /// </summary>
    public bool IsSuccess { get; }
    /// <summary>
    /// Is a failure result
    /// </summary>
    public bool IsFailure => !IsSuccess;
    /// <summary>
    /// Errors if result is failure, empty if success
    /// </summary>
    public IReadOnlyList<Error> Errors => IsFailure ? _errors : _empty;

    internal void AddError(Error error)
    {
        if (IsFailure) 
            _errors.Add(error);
    }

    /// <summary>
    /// Create a failure Result
    /// </summary>
    /// <param name="errors"></param>
    /// <returns>a Failure Result</returns>
    public static Result Failure(params Error[] errors) => new Failure(errors);
    /// <summary>
    /// Create a failure Result based on Exception
    /// </summary>
    /// <param name="exceptions"></param>
    /// <returns></returns>
    public static Result Failure(params Exception[] exceptions) => new Failure(exceptions);
    /// <summary>
    /// Create a failure Result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="errors">Errors relative to this failure</param>
    /// <returns>a Failure Result</returns>
    public static Result<T> Failure<T>( params Error[] errors)=> new Failure<T>( errors);

    /// <summary>
    /// Create a failure Result based on Exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exceptions"></param>
    /// <returns></returns>
    public static Result<T> Failure<T>(params Exception[] exceptions)=> new Failure<T>(exceptions);

    /// <summary>
    /// Create a success Result
    /// </summary>
    /// <returns>a Success Result</returns>
    public static Result Success() => new Valid();
    /// <summary>
    /// Create a success Result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result">Inner value</param>
    /// <returns>a Success Result</returns>
    public static Result<T> Sucess<T>(T result)=> new Valid<T>(result);
    /// <summary>
    /// Create a success Result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>a Success Result</returns>
    public static Result<T> Sucess<T>()=> new Valid<T>();

    /// <summary>
    /// Create a Success default result with inner value
    /// Can be use to be bind or Matched by an extension method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <returns>A Success result</returns>
    public static Result<T> Create<T>(T result)
        =>Sucess(result);


}
/// <summary>
/// Base Class of result 
/// It represent an abstract Result concept
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Result<T> : Result,IResult<T>
{
    private readonly T result;

    protected Result(T result) : this(result, true, new Error[] { })
    {
    }
 
    protected Result(T result, bool success, params Error[] errors) : base(success, errors)
    {
        this.result = result;
    }

    /// <summary>
    /// Inner Value 
    /// </summary>
    public T Value => result;

    public void Deconstruct(out bool success, out T value)
        => (success, value) = (IsSuccess, Value);

    public void Deconstruct(out bool success, out T value, out Exception exception)
        => (success, value, exception) = (IsSuccess, Value,Errors.FirstOrDefault()?.Exception);

    public static implicit operator bool(Result<T> result)
        => result.IsSuccess;

    public static implicit operator Task<Result<T>>(Result<T> result)
        => result.AsTask();

    public static implicit operator Result<T>(T value)
        =>Result.Create<T>(value);

    public static implicit operator Result<T>(Exception exception)
        =>Result.Failure<T>(exception);
}

