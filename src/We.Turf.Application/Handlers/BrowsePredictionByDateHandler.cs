namespace We.Turf.Handlers;


file class PredictionByDateSpec : Specification<PredictionByDate>
{
    public PredictionByDateSpec(DateOnly date) : base(e => e.Date == date) { }
}

file class PredictionByReunionSpec : Specification<PredictionByDate>
{
    
    public PredictionByReunionSpec(int reunion,int course) : base(e => e.Reunion == reunion && e.Course==course) { }
}
public class BrowsePredictionByDateHandler : AbpHandler.With<BrowsePredictionByDateQuery, BrowsePredictionByDateResponse, PredictionByDate, PredictionByDateDto>
{
    public BrowsePredictionByDateHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override async Task<Result<BrowsePredictionByDateResponse>> InternalHandle(BrowsePredictionByDateQuery request, CancellationToken cancellationToken)
    {
        LogTrace($"{nameof(BrowsePredictionByDateHandler)}");
        var date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);

        var query = await Repository.GetQueryableAsync();
        query = query.GetQuery(new PredictionByDateSpec(date));

        if (request.Reunion is not null && request.Course is not null)
            query = query.GetQuery(new PredictionByReunionSpec((int)request.Reunion, (int)request.Course));

        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowsePredictionByDateResponse(MapToDtoList(result));
    }
}
