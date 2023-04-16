using We.AbpExtensions;
using We.Processes;
using We.Results;

namespace We.Turf.Handlers;

public class PredictHandler : AbpHandler.With<PredictQuery, PredictResponse>
{
    protected IPythonExecutor Python => GetRequiredService<IPythonExecutor>();
    public PredictHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async  Task<Result<PredictResponse>> Handle(PredictQuery request, CancellationToken cancellationToken)
    {
        var exe = Python;
        string args = string.Empty;
        if(request.DeleteFilesIfExists)
        {
            args = $"mode=w";
        }

        var res = await exe.SendAsync($"predicter.py {args}");
        if (res.IsSuccess)
            return Result.Success< PredictResponse>();
        return Result.Failure<PredictResponse>(res.Errors.ToArray());
    }
}
