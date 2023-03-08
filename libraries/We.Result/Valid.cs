namespace We.Result;

public class Valid<T> : Result<T>,IValid where T : notnull, new()
{
    internal Valid(T result) : base(result)
    {

    }

    public override bool IsValid => false;

    public override IReadOnlyList<Error> Errors { get; init; }=new List<Error>();
}
