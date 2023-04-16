using We.AbpExtensions;
using We.Processes;
using We.Results;
using We.Turf.Queries;

namespace We.Turf.Handlers;

public class ScrapHandler : AbpHandler.With<ScrapQuery, ScrapResponse>
{
    protected IPythonExecutor Python => GetRequiredService<IPythonExecutor>();
    public ScrapHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result<ScrapResponse>> Handle(ScrapQuery request, CancellationToken cancellationToken)
    {
        var exe = Python;
        string args = string.Empty;

        string date_start = request.Start.ToString("ddMMyyyy");
        string date_end = request.End.ToString("ddMMyyyy");
        if(request.DeleteFilesIfExists)
        {
            args = $"mode=w";
        }
        args = $"{args} start={date_start} end={date_end}";
        var res = await exe.SendAsync(@$"scrap.py {args}");
        if (res.IsSuccess)
            return Result.Success<ScrapResponse>();
        return Result.Failure<ScrapResponse>(res.Errors.ToArray());
        
    }
}
