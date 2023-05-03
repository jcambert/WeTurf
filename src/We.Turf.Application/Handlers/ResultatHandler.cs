using We.AbpExtensions;
using We.Processes;
using We.Results;

namespace We.Turf.Handlers;

public class ResultatHandler : AbpHandler.With<ResultatQuery, ResultatResponse>
{
    protected IPythonExecutor Python => GetRequiredService<IPythonExecutor>();
    public ResultatHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

#if MEDIATOR
    public override async ValueTask<Result<ResultatResponse>> Handle(ResultatQuery request, CancellationToken cancellationToken)
#else
    public override async Task<Result<ResultatResponse>> Handle(ResultatQuery request, CancellationToken cancellationToken)
#endif
    {
        var exe = Python;
        string args = string.Empty;

        string date_start = request.Start.ToString("ddMMyyyy");
        string date_end = request.End.ToString("ddMMyyyy");
        if (request.DeleteFilesIfExists)
        {
            args = $"mode=w";
        }
        args = $"{args} start={date_start} end={date_end}";
        var res = await exe.SendAsync(@$"resultat.py {args}",cancellationToken);
        if (res.IsSuccess)
            return Result.Success<ResultatResponse>();
        return Result.Failure<ResultatResponse>(res.Errors.ToArray());
    }
}
