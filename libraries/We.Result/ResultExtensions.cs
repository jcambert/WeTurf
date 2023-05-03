using Dawn;

namespace We.Results;

/// <summary>
/// Result Extensions
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="resultTask"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onFailure"></param>
    /// <returns></returns>
    public static async Task<TOut> Match<TOut>(
        this Task<Result> resultTask,
        Func<TOut> onSuccess,
        Func<Result, TOut> onFailure
    )
    {
        Result result = await resultTask;
        return result.IsSuccess ? onSuccess() : onFailure(result);
    }

    /*  public static async Task<TOut> Match<TIn,TOut>(
          this Task<Result<TIn>> resultTask,
          Func<TIn, TOut> onSuccess,
          Func<Result, TOut> onFailure)
      {
          Result<TIn> result = await resultTask;
          return result ? onSuccess(result.Value) : onFailure(result);
      }*/

    /// <summary>
    /// Ensure that a result statisfy predicate
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <param name="predicate"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Error error)
        where T : notnull
    {
        Guard.Argument(result).NotNull();
        Guard.Argument(predicate).NotNull();
        if (!result)
        {
            return result;
        }
        return predicate(result.Value) ? result : Result.Failure<T>(error);
    }

    /// <summary>
    /// Map a success result into another Success result mapped by mapping function
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="result"></param>
    /// <param name="mappingFunc"></param>
    /// <returns></returns>
    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> mappingFunc)
        where TIn : notnull
        where TOut : notnull
    {
        Guard.Argument(result).NotNull();
        Guard.Argument(mappingFunc).NotNull();
        return result
          ? Result.Success(mappingFunc(result.Value))
          : Result.Failure<TOut>(result.Errors.ToArray());
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    public static Result<TOut> MapFailureTo<TIn, TOut>(this Result<TIn> result)
    {
        Guard.Argument(result).NotNull();
        Guard.Argument(result).Member(x => x.HasError, y => y.True());
        return Result.Failure<TOut>(result.Errors.ToArray());
    }

    /// <summary>
    /// Bind a result by function
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <param name="result"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static Task<Result> Bind<TIn>(this Result<TIn> result, Func<TIn, Task<Result>> func)
    {
        Guard.Argument(result).NotNull();
        Guard.Argument(func).NotNull();
        if (!result)
            return Task.FromResult(Result.Failure(result.Errors.ToArray()));
        return func(result.Value);
    }

    /// <summary>
    /// Bind a result by function
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="result"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static Task<Result<TOut>> Bind<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, Task<Result<TOut>>> func
    )
    {
        Guard.Argument(result).NotNull();
        Guard.Argument(func).NotNull();
        if (!result)
            return Task.FromResult(Result.Failure<TOut>(result.Errors.ToArray()));
        return func(result.Value);
    }

    /// <summary>
    /// Return a result as a task
    /// usefull for async method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    public static Task<Result<T>> AsTask<T>(this Result<T> result)
    {
        return Task.FromResult(result);
    }

    /// <summary>
    /// Append an error to result
    /// usefull for fluent error validation
    /// </summary>
    /// <param name="result"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Result AddError(this Result result, Error error)
    {
        result.AddError(error);
        return result;
    }

    /// <summary>
    /// Append errors to result
    /// usefull for fluent error validation
    /// </summary>
    /// <param name="result"></param>
    /// <param name="errors"></param>
    /// <returns></returns>
    public static Result AddErrors(this Result result, IEnumerable<Error> errors)
    {
        result.AddErrors(errors);
        return result;
    }

    /// <summary>
    /// Append an error to result
    /// usefull for fluent error validation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Result<T> AddError<T>(this Result<T> result, Error error)
    {
        result.AddError(error);
        return result;
    }

    /// <summary>
    /// Append an error to result
    /// usefull for fluent error validation
    /// </summary>
    /// <param name="result"></param>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static Result AddError(this Result result, Exception ex)
    {
        result.AddError(Error.Create(ex));
        return result;
    }

    /// <summary>
    /// Append an error to result
    /// usefull for fluent error validation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static Result<T> AddError<T>(this Result<T> result, Exception ex)
    {
        result.AddError(Error.Create(ex));
        return result;
    }
}
