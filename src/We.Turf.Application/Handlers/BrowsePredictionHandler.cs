using We.Turf.Entities;
using System.Linq;
namespace We.Turf.Handlers;

public class BrowsePredictionHandler : BaseHandler<BrowsePredictionQuery, BrowsePredictionResponse>
{
    public BrowsePredictionHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    IRepository<Predicted,Guid> repository=> GetRequiredService<IRepository<Predicted,Guid>>();
    public override async Task<BrowsePredictionResponse> Handle(BrowsePredictionQuery request, CancellationToken cancellationToken)
    {
        
        if(request.Date==DateOnly.MinValue)
            request.Date= DateOnly.FromDateTime(DateTime.Now);
        var query = await repository.GetQueryableAsync();
        query = from q in query where q.Date == request.Date select q;
        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowsePredictionResponse(ObjectMapper.Map<List<Predicted>, List<PredictedDto>>(result));
    }
}
