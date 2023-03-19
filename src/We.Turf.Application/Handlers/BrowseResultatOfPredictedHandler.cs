using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class BrowseResultatOfPredictedHandler : AbpHandler.With<BrowseResultatOfPredictedQuery, BrowseResultatOfPredictedResponse>
{

    IRepository<ResultatOfPredicted, Guid> _repository => GetRequiredService<IRepository<ResultatOfPredicted, Guid>>();

    public BrowseResultatOfPredictedHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result< BrowseResultatOfPredictedResponse>> Handle(BrowseResultatOfPredictedQuery request, CancellationToken cancellationToken)
    {
        var query=await _repository.GetQueryableAsync();
        var result=await AsyncExecuter.ToListAsync(query, cancellationToken);
        return  new BrowseResultatOfPredictedResponse(ObjectMapper.Map<List<ResultatOfPredicted>, List<ResultatOfPredictedDto>>(result));
    }
}
