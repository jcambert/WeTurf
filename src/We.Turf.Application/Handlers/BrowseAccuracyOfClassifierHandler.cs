using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class BrowseAccuracyOfClassifierHandler :AbpHandler.With<BrowseAccuracyOfClassifierQuery, BrowseAccuracyOfClassifierResponse, AccuracyPerClassifier, AccuracyPerClassifierDto>
{
 

    public BrowseAccuracyOfClassifierHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result<BrowseAccuracyOfClassifierResponse>> Handle(BrowseAccuracyOfClassifierQuery request, CancellationToken cancellationToken)
    {
        var res = await Repository.ToListAsync();
        return new BrowseAccuracyOfClassifierResponse(MapToDtoList(res));
    }
}
