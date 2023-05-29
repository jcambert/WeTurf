namespace We.Turf.Handlers;

file class ResultatOfPredictedByDate : Specification<ResultatOfPredicted>
{
    public ResultatOfPredictedByDate(DateOnly date) : base(e => e.Date == date) { }
}

public class BrowseResultatOfPredictedHandler
    : AbpHandler.With<
          BrowseResultatOfPredictedQuery,
          BrowseResultatOfPredictedResponse,
          ResultatOfPredicted,
          ResultatOfPredictedDto,
          Guid
      >
{
    public BrowseResultatOfPredictedHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }

    protected override async Task<Result<BrowseResultatOfPredictedResponse>> InternalHandle(BrowseResultatOfPredictedQuery request, CancellationToken cancellationToken)
    {
        var date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);

        var query = await Repository.GetQueryableAsync();
        query = query.GetQuery(new ResultatOfPredictedByDate(date));

        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowseResultatOfPredictedResponse(MapToDtoList(result));
    }
}
