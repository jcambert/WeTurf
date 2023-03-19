using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class BrowseResultatPerClassifierHandler : AbpHandler.With<BrowseResultatPerClassifierQuery, BrowseResultatPerClassifierResponse>
{
    IRepository<ResultatPerClassifier> repository => GetRequiredService<IRepository<ResultatPerClassifier>>();
    public BrowseResultatPerClassifierHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result< BrowseResultatPerClassifierResponse>> Handle(BrowseResultatPerClassifierQuery request, CancellationToken cancellationToken)
    {
        var query = await repository.GetQueryableAsync();
        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return  new BrowseResultatPerClassifierResponse(ObjectMapper.Map<List<ResultatPerClassifier>, List<ResultatPerClassifierDto>>(result));
    }
}
