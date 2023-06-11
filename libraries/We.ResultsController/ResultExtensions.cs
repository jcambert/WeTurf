using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace We.Results;

public static class ResultExtensions
{
    public static async Task<IActionResult> Match(
        this Task<Result> resultTask,
        Func<IActionResult> onSuccess,
        Func<Result, IActionResult> onFailure
    )
    {
        Result result = await resultTask;
        return result.IsSuccess ? onSuccess() : onFailure(result);
    }

    public static async Task<IActionResult> Match<T>(
        this Task<Result<T>> resultTask,
        Func<T, IActionResult> onSuccess,
        Func<Result, IActionResult> onFailure
    )
    {
        Result<T> result = await resultTask;
        return result ? onSuccess(result.Value) : onFailure(result);
    }

    public static async Task<IActionResult> MatchAsync<T>(
        this Task<Result<T>> resultTask,
        Func<T, Task<IActionResult>> onSuccess,
        Func<Result, Task<IActionResult>> onFailure
    )
    {
        Result<T> result = await resultTask;
        return result ? await onSuccess(result.Value) : await onFailure(result);
    }

    

    public static IActionResult HandleFailure(this ControllerBase controller, Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            { IsFailure: true }
              => controller.BadRequest(
                  CreateProblemDetails(
                      "Error",
                      StatusCodes.Status400BadRequest,
                      new Error("Bad Request", "An Error happened"),
                      result.Errors.ToArray()
                  )
              ),
            _ => controller.BadRequest()
        };

    public static IActionResult Handle<T>(
        this ControllerBase controller,
        Result<T> result,
        Func<Result, IActionResult> onSuccess
    ) =>
        result switch
        {
            { IsSuccess: true } => onSuccess(result),
            { IsFailure: true } => controller.HandleFailure(result),
            _ => controller.BadRequest()
        };

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null
    ) =>
        new()
        {
            Title = title,
            Type = error.Failure,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };

    public static IActionResult AsActionResult(this Result r) =>
        r.IsSuccess ? new NoContentResult() : new BadRequestResult();

    public static IActionResult AsActionResult<T>(this T r) => new NoContentResult();

    public static Task<IActionResult> AsActionResultAsync<T>(this T r) =>
        Task.FromResult<IActionResult>(new NoContentResult());

    public static Task<IActionResult> AsActionResultAsync(this Result r) =>
        r.IsSuccess
            ? Task.FromResult<IActionResult>(new NoContentResult())
            : Task.FromResult<IActionResult>(new BadRequestResult());
}
