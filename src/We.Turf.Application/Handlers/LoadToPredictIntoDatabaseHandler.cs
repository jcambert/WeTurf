
using We.AbpExtensions;
using We.Results;

namespace We.Turf.Handlers;

public class LoadToPredictIntoDatabaseHandler : BaseHandler<LoadToPredictIntoDatabaseQuery, LoadToPredictIntoDatabaseResponse>
{
    public LoadToPredictIntoDatabaseHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override Task<Result<LoadToPredictIntoDatabaseResponse>> Handle(LoadToPredictIntoDatabaseQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

 
}
