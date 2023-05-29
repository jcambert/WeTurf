using We.Processes;

namespace We.Turf.Handlers;

public class PredictHandler : AbpHandler.With<PredictQuery, PredictResponse>
{
    protected IPythonExecutor Python => GetRequiredService<IPythonExecutor>();

    public PredictHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider) { }

    protected override async Task<Result<PredictResponse>> InternalHandle(
        PredictQuery request,
        CancellationToken cancellationToken
    )
    {
        var exe = Python;
        string args = string.Empty;
        if (!string.IsNullOrEmpty(request.UseFolder))
        {
            args = $"usefolder={request.UseFolder}";
        }
        if (request.DeleteFilesIfExists)
        {
            args = $"{args} mode=w";
        }

        var res = await exe.SendAsync($"predicter.py {args}", cancellationToken);
        if (res.IsSuccess)
            return Result.Success<PredictResponse>();
        return Result.Failure<PredictResponse>(res.Errors.ToArray());
    }
}
