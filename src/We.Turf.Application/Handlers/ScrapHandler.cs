using We.AbpExtensions;
using We.Processes;
using We.Results;
using We.Turf.Queries;

namespace We.Turf.Handlers;

public class ScrapHandler : AbpHandler.With<ScrapQuery, ScrapResponse>
{
    protected IPythonExecutor Python => GetRequiredService<IPythonExecutor>();

    public ScrapHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider) { }
#if MEDIATOR
    public override async ValueTask<Result<ScrapResponse>> Handle(
        ScrapQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<ScrapResponse>> Handle(
        ScrapQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        var exe = Python;
        string args = string.Empty;
        if (!string.IsNullOrEmpty(request.UseFolder))
            args = $"usefolder={request.UseFolder}";
        string date_start = request.Start.ToString("ddMMyyyy");
        string date_end = request.End.ToString("ddMMyyyy");
        if (request.DeleteFilesIfExists)
        {
            args = $"{args} mode=w";
        }
        args = $"{args} start={date_start} end={date_end}";
        var res = await exe.SendAsync(@$"scrap.py {args}");
        if (res.IsSuccess)
            return Result.Success<ScrapResponse>();
        return Result.Failure<ScrapResponse>(res.Errors.ToArray());
    }
}
