using We.Turf.Entities;
using We.AbpExtensions;
using We.Results;

namespace We.Turf.Handlers;

internal class PredictionByDate : Specification<Predicted>
{
    public PredictionByDate(DateOnly date) : base(e => e.Date == date) { }
}

public class BrowsePredictionHandler
    : AbpHandler.With<
          BrowsePredictionQuery,
          BrowsePredictionResponse,
          Predicted,
          PredictedDto,
          Guid
      >
{
    public BrowsePredictionHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }
#if MEDIATOR
    public override async ValueTask<Result<BrowsePredictionResponse>> Handle(
        BrowsePredictionQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<BrowsePredictionResponse>> Handle(
        BrowsePredictionQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        LogTrace($"{nameof(BrowsePredictionHandler)}");
        var date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);

        var query = await Repository.GetQueryableAsync();
        query = query.GetQuery(new PredictionByDate(date));

        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowsePredictionResponse(MapToDtoList(result));
    }
}
