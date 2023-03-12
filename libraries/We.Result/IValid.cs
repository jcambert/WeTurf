namespace We.Results;

public interface IValid : IResult { }

public interface IsValid<T>:IValid,IResult<T>
{ }
