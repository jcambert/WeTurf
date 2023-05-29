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

    protected override async Task<Result<BrowsePredictionPerClassifierResponse>> InternalHandle(
        BrowsePredictionPerClassifierQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await Repository.GetQueryableAsync();
        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowsePredictionPerClassifierResponse(MapToDtoList(result));
    }
}
