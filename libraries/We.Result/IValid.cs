namespace We.Results;

/// <summary>
/// Base Success result
/// </summary>
public interface IValid : IResult { }

/// <summary>
/// Base Success result
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IsValid<T>:IValid,IResult<T>
{ }
