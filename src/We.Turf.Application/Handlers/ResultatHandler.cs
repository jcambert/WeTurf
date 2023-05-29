using We.Processes;

namespace We.Turf.Handlers;

public class ResultatHandler : AbpHandler.With<ResultatQuery, ResultatResponse>
{
    protected IPythonExecutor Python => GetRequiredService<IPythonExecutor>();

    public ResultatHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider) { }

    protected override async Task<Result<ResultatResponse>> InternalHandle(
        ResultatQuery request,
        CancellationToken cancellationToken
    )
    {
        var exe = Python;
        string args = string.Empty;

        string date_start = request.Start.ToString("ddMMyyyy");
        string date_end = request.End.ToString("ddMMyyyy");
        if (!string.IsNullOrEmpty(request.UseFolder))
        {
            args = $"usefolder={request.UseFolder}";
        }

        if (request.DeleteFilesIfExists)
        {
            args = $"{args} mode=w";
        }
        args = $"{args} start={date_start} end={date_end}";
        var res = await exe.SendAsync(@$"resultat.py {args}", cancellationToken);
        if (res.IsSuccess)
            return Result.Success<ResultatResponse>();
        return Result.Failure<ResultatResponse>(res.Errors.ToArray());
    }
}
