using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class BrowseResultatOfPredictedHandler : AbpHandler.With<BrowseResultatOfPredictedQuery, BrowseResultatOfPredictedResponse, ResultatOfPredicted, ResultatOfPredictedDto, Guid>
{

  
    public BrowseResultatOfPredictedHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result< BrowseResultatOfPredictedResponse>> Handle(BrowseResultatOfPredictedQuery request, CancellationToken cancellationToken)
    {
        var query=await Repository.GetQueryableAsync();
        var result=await AsyncExecuter.ToListAsync(query, cancellationToken);
        return  new BrowseResultatOfPredictedResponse(MapToDtoList(result));
    }
}
