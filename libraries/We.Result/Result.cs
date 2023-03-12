namespace We.Results;

public abstract class Result : IResult
{
    private readonly List<Error> _errors;
    protected Result(bool success,params string[] errors)
    {
        IsSucess = success;
        _errors=errors.Where(x=>!string.IsNullOrEmpty(x)).Distinct().Select(x=>new Error(x)).ToList();
    }
    protected Result(bool success,params Error[] errors)
    {
        IsSucess=success;
        _errors=errors.DistinctBy(x=>x.Failure).ToList();
    }
    public bool IsSucess { get; }
    public bool IsFailure =>!IsSucess;
    public IReadOnlyList<Error> Errors => _errors;

    //public static Result Failure(params string[] errors) => new Failure(errors);
    public static Result Failure(params Error[] errors) => new Failure(errors);
    public static Result<T> Failure<T>( params Error[] errors)=> new Failure<T>( errors);

    //public static Result<T> Failure<T>( params string[] errors)=> new Failure<T>(errors);


    public static Result Success() => new Valid();
    public static Result<T> Sucess<T>(T result)=> new Valid<T>(result);
    public static Result<T> Sucess<T>()=> new Valid<T>();
    /*public static Result<T> Create<T>(T result, params Error[] errors)
         where T : notnull
        => Create(result, errors.ToList());*/


    public static Result<T> Create<T>(T result/*, IEnumerable<Error> errors*/)
    {
        /*Error[] _errors = errors?.Where(e => e != null && !string.IsNullOrEmpty(e.Failure)).ToArray() ?? new Error[] { };
        if (_errors.Any())
            return Failure( _errors);*/
        return Sucess(result);
    }
}
public abstract class Result<T> : Result,IResult<T>
{
    private readonly T? result;

    protected Result(T? result) : this(result, true, new Error[] { })
    {
    }
   /* public Result(T result,bool success, params string[] errors):base(success,errors)
    {
        this.result = result;
    }*/
    protected Result(T? result, bool success, params Error[] errors) : base(success, errors)
    {
        this.result = result;
    }

    
    public T? Value => result ;
}

