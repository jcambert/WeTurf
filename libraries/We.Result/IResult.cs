namespace We.Results;

public interface IResult
{
    bool IsSucess { get; }
    bool IsFailure { get; }
    IReadOnlyList<Error> Errors { get; }
}
public interface IResult<T> : IResult
{
    T Value { get; }
}