using We.Processes;

namespace We.Turf.Handlers;

public class ScrapHandler : AbpHandler.With<ScrapQuery, ScrapResponse>
{
    protected IPythonExecutor Python => GetRequiredService<IPythonExecutor>();

    public ScrapHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider) { }

    protected override async Task<Result<ScrapResponse>> InternalHandle(
        ScrapQuery request,
        CancellationToken cancellationToken
    )
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
