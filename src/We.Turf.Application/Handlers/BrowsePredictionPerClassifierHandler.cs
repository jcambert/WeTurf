using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class BrowsePredictionPerClassifierHandler
    : AbpHandler.With<
          BrowsePredictionPerClassifierQuery,
          BrowsePredictionPerClassifierResponse,
          PredictionPerClassifier,
          PredictionPerClassifierDto
      >
{
    public BrowsePredictionPerClassifierHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }

#if MEDIATOR
    public override async ValueTask<Result<BrowsePredictionPerClassifierResponse>> Handle(
        BrowsePredictionPerClassifierQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<BrowsePredictionPerClassifierResponse>> Handle(
        BrowsePredictionPerClassifierQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        var query = await Repository.GetQueryableAsync();
        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowsePredictionPerClassifierResponse(MapToDtoList(result));
    }
}
