namespace We.Results;

/// <summary>
/// Base Result
/// </summary>
public interface IResult
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
    IReadOnlyList<Error> Errors { get; }
}

/// <summary>
/// Base Result
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IResult<T> : IResult
{
    T Value { get; }
}