namespace We.Result;
public abstract class Result<T>:IResult
    where T : notnull,new()
{
    private readonly T result;

    public Result(T result)
    {
        this.result = result;
    }

    public static Result<T> Failure(T result, params Error[] errors) => new Failure<T>(result);
    public static Result<T> Failure(T result, IEnumerable<Error> errors) => new Failure<T>(result);
    public static Result<T> Sucess(T result) => new Valid<T>(result);
    public static Result<T> Sucess() => new Valid<T>(new T());
    public static Result<T> Create(T result, params Error[] errors)
        =>Create(result, errors.ToList());
    public static Result<T> Create(T result, IEnumerable<Error> errors)
    {
        var _errors = errors?.Where(e => e != null && !string.IsNullOrEmpty(e.Failure)).ToList() ?? new List<Error>();
        if(_errors.Any() ) 
            return Failure(result, _errors);
        return Sucess(result);
    }
    public T Value => result;
    public abstract bool IsValid { get; }
    public abstract IReadOnlyList<Error> Errors { get; init; }
}
