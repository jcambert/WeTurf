using Dawn;


namespace We.Results;

public static class ResultExtensions
{

    public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Error error)
        where T : notnull
    {
        Guard.Argument(result).NotNull();
        Guard.Argument(predicate).NotNull();
        if (result.IsFailure)
        {
            return result;
        }
        return predicate(result.Value) ? result : Result.Failure<T>(error);
    }

    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result,Func<TIn,TOut> mappingFunc) 
        where TIn : notnull
        where TOut : notnull
    {
        Guard.Argument(result).NotNull();
        Guard.Argument(mappingFunc).NotNull();
        return result.IsSucess ? Result.Sucess(mappingFunc(result.Value)) :Result.Failure<TOut>( result.Errors.ToArray());
    }

    public static Task< Result> Bind<TIn>(this Result<TIn> result,Func<TIn,Task<Result>> func)
    {
        Guard.Argument(result).NotNull();
        Guard.Argument(func).NotNull();
        if (result.IsFailure) 
            return Task.FromResult(  Result.Failure(result.Errors.ToArray()));
        return  func(result.Value);
    }
   

    public static  Task<Result<TOut>> Bind<TIn,TOut>(this Result<TIn> result, Func<TIn, Task<Result<TOut>>> func)
    {
        
        Guard.Argument(result).NotNull();
        Guard.Argument(func).NotNull();
        if (result.IsFailure)
            return Task.FromResult( Result.Failure<TOut>(result.Errors.ToArray()));
        return func(result.Value);
    }

    public static Task<Result<T>> AsTask<T>(this Result<T> result) {
        return Task.FromResult(result);
    }
}
