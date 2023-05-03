namespace We.Results;

/// <summary>
/// Base Class of result
/// It represent an abstract Result concept
/// </summary>
public abstract class Result : IResult
{
    protected readonly List<Error> _errors;
    private readonly List<Error> _empty = new();

    protected Result(bool success, params string[] errors)
    {
        IsSuccess = success;
        _errors = errors
            .Where(x => !string.IsNullOrEmpty(x))
            .Distinct()
            .Select(x => new Error(x))
            .ToList();
    }

    protected Result(bool success, params Error[] errors)
    {
        IsSuccess = success;

        _errors =
            errors /*.DistinctBy(x => x.Failure)*/
            .ToList();
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
    /// Has some result errors
    /// </summary>
    public bool HasError => Errors.Any();

    /// <summary>
    ///
    /// </summary>
    public virtual bool IsValidFailure => false;

    /// <summary>
    /// Errors if result is failure, empty if success
    /// </summary>
    public virtual IReadOnlyList<Error> Errors => IsFailure ? _errors : _empty;

    internal void AddError(Error error)
    {
        if (IsFailure || IsValidFailure)
            _errors.Add(error);
    }

    internal void AddErrors(IEnumerable<Error> errors)
    {
        if (IsFailure || IsValidFailure)
            _errors.AddRange(errors);
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
    public static Result<T> Failure<T>(params Error[] errors) => new Failure<T>(errors);

    /// <summary>
    /// Create a failure Result based on Exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exceptions"></param>
    /// <returns></returns>
    public static Result<T> Failure<T>(params Exception[] exceptions) => new Failure<T>(exceptions);

    public static Result<T> Failure<T>(string failure) => new Failure<T>(new Error(failure));

    public static Result<T> Failure<T>(string failure, string message) =>
        new Failure<T>(new Error(failure, message));

    public static Result<T> Failure<T>(int code, string failure, string message) =>
        new Failure<T>(new Error(code, failure, message));

    //public static Result<T> ValidWithFailure<T>(T result,params Exception[] exceptions) => new ValidWithFailure<T>(result,exceptions);

    //public static Result ValidWithFailure<T>(params T[] exceptions) where T:Exception => new ValidWithFailure(exceptions);

    /// <summary>
    /// Create a Success with failure Result based on Exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <param name="errors"></param>
    /// <returns></returns>
    public static Result<T> ValidWithFailure<T>(T result, params Error[] errors) =>
        new ValidWithFailure<T>(result, errors);

    public static Result ValidWithFailure(params Error[] errors) => new ValidWithFailure(errors);

    public static Result<T> ValidWithFailure<T>(Error errors) => new ValidWithFailure<T>(errors);

    public static Result<T> ValidWithFailure<T>(Exception exception) =>
        new ValidWithFailure<T>(exception);

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
    public static Result<T> Success<T>(T result) => new Valid<T>(result);

    /// <summary>
    /// Create a success Result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>a Success Result</returns>
    public static Result<T> Success<T>() => new Valid<T>();

    /// <summary>
    /// Create a Success default result with inner value
    /// Can be use to be bind or Matched by an extension method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <returns>A Success result</returns>
    public static Result<T> Create<T>(T result) => Success(result);
}

/// <summary>
/// Base Class of result
/// It represent an abstract Result concept
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Result<T> : Result, IResult<T>
{
    private readonly T result;

    protected Result(T result) : this(result, true, Array.Empty<Error>()) { }

    protected Result(T result, bool success, params Error[] errors) : base(success, errors)
    {
        this.result = result;
    }

    /// <summary>
    /// Inner Value
    /// </summary>
    public T Value => result;

    public void Deconstruct(out bool success, out T value) => (success, value) = (IsSuccess, Value);

    public IEnumerable<Error> GetErrors()
    {
        return Errors;
    }

    public void Deconstruct(
        out bool success,
        out T value,
        out Exception exception,
        IEnumerable<Error> errors
    ) => (success, value, exception) = (IsSuccess, Value, errors.FirstOrDefault()?.Exception);

    public static implicit operator bool(Result<T> result) => result.IsSuccess;

    public static implicit operator Task<Result<T>>(Result<T> result) => result.AsTask();

    public static implicit operator Result<T>(T value) => Result.Create<T>(value);

    public static implicit operator Result<T>(Exception exception) => Result.Failure<T>(exception);
}
