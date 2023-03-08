namespace We.Result;

public interface IResult
{
     bool IsValid { get; }
     IReadOnlyList<Error> Errors { get; init; }
}
