namespace We.Results;

/// <summary>
/// Failure Result
/// </summary>
public interface IFailure : IResult { }

/// <summary>
/// Failure Result
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IFailure<T> : IFailure, IResult<T> { }
