using We.Turf.Entities;
using We.AbpExtensions;
using We.Results;

namespace We.Turf.Handlers;
internal class PredictionByDate : Specification<Predicted>
{
    public PredictionByDate(DateOnly date):base(e=>e.Date==date)
    {
    }
}
public class BrowsePredictionHandler : BaseHandler<BrowsePredictionQuery,BrowsePredictionResponse>
{
    public BrowsePredictionHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    IRepository<Predicted,Guid> repository=> GetRequiredService<IRepository<Predicted,Guid>>();
    public override async Task<Result< BrowsePredictionResponse>> Handle(BrowsePredictionQuery request, CancellationToken cancellationToken)
    {
        LogTrace($"{nameof(BrowsePredictionHandler)}");
        var date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);
        
        var query = await repository.GetQueryableAsync();
        query=query.GetQuery(new PredictionByDate(date));

        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return  new BrowsePredictionResponse(ObjectMapper.Map<List<Predicted>, List<PredictedDto>>(result));
    }
}
