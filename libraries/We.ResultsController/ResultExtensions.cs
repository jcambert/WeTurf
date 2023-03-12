using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace We.Results;

public static class ResultExtensions
{
    public static async Task<IActionResult> Match(
        this Task<Result> resultTask,
        Func<IActionResult> onSuccess,
        Func<Result,IActionResult> onFailure)
    {
        Result result=await resultTask;
        return result.IsSucess?onSuccess():onFailure(result);
    }

    public static async Task<IActionResult> Match<T>(
        this Task<Result<T>> resultTask,
        Func<T,IActionResult> onSuccess,
        Func<Result, IActionResult> onFailure)
    {
        Result<T> result = await resultTask;
        return result.IsSucess ? onSuccess(result.Value) : onFailure(result);
    }

    
    public static IActionResult HandleFailure(this ControllerBase controller, Result result)
    => result switch
    {
        { IsSucess: true } => throw new InvalidOperationException(),
        { IsFailure: true } => controller.BadRequest(CreateProblemDetails("Error",StatusCodes.Status400BadRequest,new Error("Bad Request","An Error happened"),result.Errors.ToArray())),
        _ => controller.BadRequest()
    };

    public static IActionResult Handle<T>(this ControllerBase controller, Result<T> result,Func<Result,IActionResult> onSuccess)
    => result switch
    {
        { IsSucess: true } => onSuccess(result),
        { IsFailure: true } => controller.HandleFailure(result),
        _ => controller.BadRequest()
    };

    private static ProblemDetails CreateProblemDetails(string title, int status, Error error, Error[]? errors = null)
        => new()
        {
            Title = title,
            Type = error.Failure,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}