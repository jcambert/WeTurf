namespace We.Result;
public interface IResult
{
     bool IsValid { get; }
     IReadOnlyList<Error> Errors { get; init; }
}
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
public interface IFailure:IResult
{

}
public sealed class Failure<T> : Result<T>,IFailure where T : notnull,new()
{
    internal Failure(T result, params Error[] errors) : this(result, errors.ToList())
    { }

    internal Failure(T result, IEnumerable<Error> errors) : base(result)
    {
        Errors = errors?.Where(e => e != null && !string.IsNullOrEmpty(e.Failure)).ToList() ?? new();
    }

    public override bool IsValid => !Errors.Any();

    public override IReadOnlyList<Error> Errors { get; init; }
}
public interface IValid : IResult { }
public class Valid<T> : Result<T>,IValid where T : notnull, new()
{
    internal Valid(T result) : base(result)
    {

    }

    public override bool IsValid => false;

    public override IReadOnlyList<Error> Errors { get; init; }=new List<Error>();
}

public class Error
{
    public Error(string failure, string message) => (Failure, Message) = (failure, message);

    public string Failure { get; init; }
    public string Message { get; init; }
}