namespace We.Result;

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
