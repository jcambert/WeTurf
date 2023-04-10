using We.AbpExtensions;
using We.Processes;
using We.Results;

namespace We.Turf.Handlers;

public class ScrapHandler : AbpHandler.With<ScrapQuery, ScrapResponse>
{
    protected IPythonExecutor Python=>GetRequiredService<IPythonExecutor>();
    public ScrapHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result<ScrapResponse>> Handle(ScrapQuery request, CancellationToken cancellationToken)
    {
        var exe = Python;
        return new ScrapResponse();
    }
}
