using We.AbpExtensions;
using We.Processes;
using We.Results;

namespace We.Turf.Handlers;

public class PredictHandler : AbpHandler.With<PredictQuery, PredictResponse>
{
    protected IPythonExecutor Python => GetRequiredService<IPythonExecutor>();

    public PredictHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider) { }
#if MEDIATOR
    public override async ValueTask<Result<PredictResponse>> Handle(
        PredictQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<PredictResponse>> Handle(
        PredictQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        var exe = Python;
        string args = string.Empty;
        if (request.DeleteFilesIfExists)
        {
            args = $"mode=w";
        }

        var res = await exe.SendAsync($"predicter.py {args}", cancellationToken);
        if (res.IsSuccess)
            return Result.Success<PredictResponse>();
        return Result.Failure<PredictResponse>(res.Errors.ToArray());
    }
}
