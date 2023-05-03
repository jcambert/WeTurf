namespace We.Results;

/// <summary>
/// Base Success result
/// </summary>
public interface IValid : IResult { }

/// <summary>
/// Base Success result
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IValid<T> : IValid, IResult<T> { }

public interface IValidWithFailure : IResult { }

public interface IValidWithFailure<T> : IValid<T>, IFailure<T> { }
