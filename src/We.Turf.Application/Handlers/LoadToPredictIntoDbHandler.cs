
using We.AbpExtensions;
using We.Results;

namespace We.Turf.Handlers;

public class LoadToPredictIntoDbHandler : AbpHandler.With<LoadToPredictIntoDbQuery, LoadToPredictIntoDatabaseResponse>
{
    public LoadToPredictIntoDbHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override Task<Result<LoadToPredictIntoDatabaseResponse>> Handle(LoadToPredictIntoDbQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

 
}
