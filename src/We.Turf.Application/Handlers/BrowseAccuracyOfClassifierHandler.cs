using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class BrowseAccuracyOfClassifierHandler : BaseHandler<BrowseAccuracyOfClassifierQuery, BrowseAccuracyOfClassifierResponse>
{
    IRepository<AccuracyPerClassifier> repository => GetRequiredService<IRepository<AccuracyPerClassifier>>();


    public BrowseAccuracyOfClassifierHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result<BrowseAccuracyOfClassifierResponse>> Handle(BrowseAccuracyOfClassifierQuery request, CancellationToken cancellationToken)
    {
        var res = await repository.ToListAsync();
        return new BrowseAccuracyOfClassifierResponse(ObjectMapper.Map<List<AccuracyPerClassifier>, List<AccuracyPerClassifierDto>>(res));
    }
}
