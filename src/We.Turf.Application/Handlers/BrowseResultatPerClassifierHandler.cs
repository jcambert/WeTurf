using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class BrowseResultatPerClassifierHandler : AbpHandler.With<BrowseResultatPerClassifierQuery, BrowseResultatPerClassifierResponse, ResultatPerClassifier, ResultatPerClassifierDto>
{
    public BrowseResultatPerClassifierHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
#if MEDIATOR
    public override async ValueTask<Result< BrowseResultatPerClassifierResponse>> Handle(BrowseResultatPerClassifierQuery request, CancellationToken cancellationToken)
#else
    public override async Task<Result< BrowseResultatPerClassifierResponse>> Handle(BrowseResultatPerClassifierQuery request, CancellationToken cancellationToken)
#endif
    {
        var query = await Repository.GetQueryableAsync();
        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return  new BrowseResultatPerClassifierResponse(MapToDtoList(result));
    }
}
