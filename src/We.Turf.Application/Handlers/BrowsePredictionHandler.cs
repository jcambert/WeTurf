namespace We.Turf.Handlers;

file class PredictionByDate : Specification<Predicted>
{
    public PredictionByDate(DateOnly date) : base(e => e.Date == date) { }
}

file class PredictionByClassifier : Specification<Predicted>
{
    public PredictionByClassifier(string classifier) : base(e => e.Classifier == classifier) { }
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

    protected override async Task<Result<BrowsePredictionResponse>> InternalHandle(BrowsePredictionQuery request, CancellationToken cancellationToken)
    {
        LogTrace($"{nameof(BrowsePredictionHandler)}");
        var date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);

        var query = await Repository.GetQueryableAsync();
        query = query.GetQuery(new PredictionByDate(date));

        if (!string.IsNullOrEmpty(request.Classifier))
            query = query.GetQuery(new PredictionByClassifier(request.Classifier));

        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowsePredictionResponse(MapToDtoList(result));
    }
}
